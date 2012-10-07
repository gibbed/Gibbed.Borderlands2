﻿/* Copyright (c) 2012 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Gibbed.IO;
using WillowTwoSave = Gibbed.Borderlands2.ProtoBufFormats.WillowTwoSave;

namespace Gibbed.Borderlands2.FileFormats
{
    public class SaveFile : INotifyPropertyChanged
    {
        #region Fields
        private Endian _Endian;
        private WillowTwoSave.WillowTwoPlayerSaveGame _SaveGame;
        #endregion

        #region Properties
        public Endian Endian
        {
            get { return this._Endian; }
            set
            {
                if (value != this._Endian)
                {
                    this._Endian = value;
                    this.NotifyPropertyChanged("Endian");
                }
            }
        }

        public WillowTwoSave.WillowTwoPlayerSaveGame SaveGame
        {
            get { return this._SaveGame; }
            set
            {
                if (value != this._SaveGame)
                {
                    this._SaveGame = value;
                    this.NotifyPropertyChanged("SaveGame");
                }
            }
        }
        #endregion

        public void Serialize(Stream output)
        {
            throw new NotImplementedException();
        }

        [Flags]
        public enum DeserializeSettings
        {
            None = 0,
            IgnoreSha1Mismatch = 1 << 0,
            IgnoreCrc32Mismatch = 1 << 1,
            IgnoreReencodeMismatch = 1 << 2,
        }

        public static SaveFile Deserialize(Stream input, DeserializeSettings settings)
        {
            if (input.Position + 20 > input.Length)
            {
                throw new SaveCorruptionException("not enough data for save header");
            }

            var readSha1Hash = input.ReadBytes(20);
            using (var data = input.ReadToMemoryStream(input.Length - 20))
            {
                byte[] computedSha1Hash;
                using (var sha1 = new System.Security.Cryptography.SHA1Managed())
                {
                    computedSha1Hash = sha1.ComputeHash(data);
                }

                if ((settings & DeserializeSettings.IgnoreSha1Mismatch) == 0 &&
                    Enumerable.SequenceEqual(readSha1Hash, computedSha1Hash) == false)
                {
                    throw new SaveCorruptionException("invalid SHA1 hash");
                }

                data.Position = 0;
                var uncompressedSize = data.ReadValueU32(Endian.Big);
                var actualUncompressedSize = uncompressedSize;
                var uncompressedBytes = new byte[uncompressedSize];
                var compressedSize = (uint)(data.Length - 4);
                var compressedBytes = data.ReadBytes(compressedSize);
                var result = LZO.Decompress(compressedBytes,
                                            compressedSize,
                                            uncompressedBytes,
                                            ref actualUncompressedSize);
                if (result != 0)
                {
                    throw new SaveCorruptionException(string.Format("LZO decompression failure ({0})", result));
                }

                using (var outerData = new MemoryStream(uncompressedBytes))
                {
                    var innerSize = outerData.ReadValueU32(Endian.Big);
                    var magic = outerData.ReadString(3);
                    if (magic != "WSG")
                    {
                        throw new SaveCorruptionException("invalid magic");
                    }

                    var version = outerData.ReadValueU32(Endian.Little);
                    if (version != 2)
                    {
                        throw new SaveCorruptionException("invalid or unsupported version");
                    }

                    var readCRC32Hash = outerData.ReadValueU32(Endian.Little);
                    var innerUncompressedSize = outerData.ReadValueS32(Endian.Little);

                    var innerCompressedBytes = outerData.ReadBytes(innerSize - 3 - 4 - 4 - 4);
                    var innerUncompressedBytes = Huffman.Decoder.Decode(innerCompressedBytes,
                                                                        innerUncompressedSize);
                    if (innerUncompressedBytes.Length != innerUncompressedSize)
                    {
                        throw new SaveCorruptionException("huffman decompression failure");
                    }

                    var computedCRC32Hash = CRC32.Hash(innerUncompressedBytes, 0, innerUncompressedBytes.Length);
                    if ((settings & DeserializeSettings.IgnoreCrc32Mismatch) == 0 &&
                        computedCRC32Hash != readCRC32Hash)
                    {
                        throw new SaveCorruptionException("invalid CRC32 hash");
                    }

                    using (var innerUncompressedData = new MemoryStream(innerUncompressedBytes))
                    {
                        var saveGame =
                            ProtoBuf.Serializer.Deserialize<WillowTwoSave.WillowTwoPlayerSaveGame>(innerUncompressedData);

                        if ((settings & DeserializeSettings.IgnoreReencodeMismatch) == 0)
                        {
                            using (var testData = new MemoryStream())
                            {
                                ProtoBuf.Serializer.Serialize(testData, saveGame);

                                testData.Position = 0;
                                var testBytes = testData.ReadBytes((uint)testData.Length);
                                if (Enumerable.SequenceEqual(innerUncompressedBytes, testBytes) == false)
                                {
                                    throw new SaveCorruptionException("reencode mismatch");
                                }
                            }
                        }

                        saveGame.Decompose();
                        return new SaveFile()
                        {
                            Endian = Endian.Little,
                            SaveGame = saveGame,
                        };
                    }
                }
            }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}