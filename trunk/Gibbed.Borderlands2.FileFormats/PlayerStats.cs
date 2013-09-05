/* Copyright (c) 2013 Rick (rick 'at' gibbed 'dot' us)
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
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Gibbed.IO;

namespace Gibbed.Borderlands2.FileFormats
{
    public class PlayerStats
    {
        private const uint _Version = 4;

        public readonly List<PlayerStat> Stats = new List<PlayerStat>();

        public struct PlayerStat
        {
            public ushort Id;
            public object Value1;
            public object Value2;
        }

        public static bool IsSupportedVersion(byte[] data, Endian endian)
        {
            if (data.Length < 4)
            {
                return false;
            }

            using (var temp = new MemoryStream(data, 0, 4, false))
            {
                return temp.ReadValueU32(endian) == _Version;
            }
        }

        private void Serialize(Stream output, Endian endian)
        {
            if (this.Stats.Count > 0xFFFF)
            {
                throw new SaveCorruptionException("too many stats");
            }

            output.WriteValueU32(_Version, endian);

            output.Position = 8;
            output.WriteValueU16((ushort)this.Stats.Count, endian);

            foreach (var stat in this.Stats)
            {
                output.WriteValueU16(stat.Id, endian);
                WriteData(output, stat.Value1, endian);
                WriteData(output, stat.Value2, endian);
            }

            output.Position = 4;
            output.WriteValueU32((uint)(output.Length - 8), endian);
        }

        public byte[] Serialize(Endian endian)
        {
            using (var temp = new MemoryStream())
            {
                this.Serialize(temp, endian);
                var buffer = (byte[])temp.GetBuffer().Clone();
                Array.Resize(ref buffer, (int)temp.Length);
                return buffer;
            }
        }

        private void Deserialize(Stream input, Endian endian)
        {
            var version = input.ReadValueU32(endian);
            if (version != _Version)
            {
                throw new SaveCorruptionException("bad stats version");
            }

            var length = input.ReadValueU32(endian);
            var end = input.Position + length;
            if (end > input.Length)
            {
                throw new SaveCorruptionException("not enough data for stats");
            }

            this.Stats.Clear();
            var count = input.ReadValueU16(endian);
            for (int i = 0; i < count; i++)
            {
                var id = input.ReadValueU16(endian);
                var type1 = (DataType)input.ReadValueU8();
                var value1 = ReadData(input, type1, endian);
                var type2 = (DataType)input.ReadValueU8();
                var value2 = ReadData(input, type2, endian);

                this.Stats.Add(new PlayerStat()
                {
                    Id = id,
                    Value1 = value1,
                    Value2 = value2,
                });
            }

            if (input.Position != end)
            {
                throw new SaveCorruptionException("did not consume all stats data");
            }
        }

        public void Deserialize(byte[] data, Endian endian)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            using (var temp = new MemoryStream(data, false))
            {
                this.Deserialize(temp, endian);
            }
        }

        private enum DataType : byte
        {
            Empty = 0,
            Int32 = 1,
            Int64 = 2,
            Double = 3,
            String = 4,
            Float = 5,
            Blob = 6,
            DateTime = 7,
            Byte = 8,
        }

        private static object ReadData(Stream input, DataType type, Endian endian)
        {
            switch (type)
            {
                case DataType.Int32:
                {
                    return input.ReadValueS32(endian);
                }
            }

            throw new NotImplementedException(string.Format(CultureInfo.InvariantCulture,
                                                            "don't know how to read stat type {0}",
                                                            type));
        }

        private static void WriteData(Stream output, object value, Endian endian)
        {
            if (value == null)
            {
                output.WriteValueU8((byte)DataType.Empty);
                return;
            }

            switch (Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.Int32:
                {
                    output.WriteValueU8((byte)DataType.Int32);
                    output.WriteValueS32((int)value, endian);
                    return;
                }
            }

            throw new NotImplementedException(string.Format(CultureInfo.InvariantCulture,
                                                            "don't know how to write stat type {0}",
                                                            value.GetType().Name));
        }
    }
}
