/* Copyright (c) 2012 Rick (rick 'at' gibbed 'dot' us)
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

#pragma warning disable 649

using System;
using System.Collections.Generic;
using System.Linq;
using Gibbed.Borderlands2.FileFormats;
using Newtonsoft.Json;

namespace Gibbed.Borderlands2.GameInfo
{
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class AssetLibrary
    {
        internal AssetLibrary()
        {
        }

        /// <summary>
        /// The Native/UnrealScript type of assets in the library.
        /// </summary>
        [JsonProperty(PropertyName = "type", Required = Required.Always)]
        public string Type;

        /// <summary>
        /// The number of bits required to represent the sublibrary index.
        /// </summary>
        [JsonProperty(PropertyName = "sublibrary_bits", Required = Required.Always)]
        public int SublibraryBits;

        /// <summary>
        /// The number of bits required to represent the sublibrary assets index.
        /// </summary>
        [JsonProperty(PropertyName = "asset_bits", Required = Required.Always)]
        public int AssetBits;

        public uint NoneIndex
        {
            get { return (1u << this.SublibraryBits + this.AssetBits) - 1; }
        }

        public uint SublibraryMask
        {
            get { return (1u << this.SublibraryBits) - 1; }
        }

        public uint AssetMask
        {
            get { return (1u << this.AssetBits) - 1; }
        }

        public int MostAssets
        {
            get { return this.Sublibraries.Max(sl => sl.Assets.Count); }
        }

        [JsonProperty(PropertyName = "sublibraries")]
        public List<AssetSublibrary> Sublibraries = new List<AssetSublibrary>();

        public void Encode(BitWriter writer, string value)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }

            if (string.IsNullOrEmpty(value) == true)
            {
                throw new ArgumentNullException("value");
            }

            uint index;
            if (value == "None")
            {
                index = this.NoneIndex;
            }
            else
            {
                var parts = value.Split(new[]
                {
                    '.'
                },
                                        2);
                if (parts.Length != 2)
                {
                    throw new ArgumentException();
                }

                var package = parts[0];
                var asset = parts[1];

                var sublibrary =
                    this.Sublibraries.FirstOrDefault(sl => sl.Package == package && sl.Assets.Contains(asset));
                if (sublibrary == null)
                {
                    throw new ArgumentException("unsupported asset");
                }

                var sublibraryIndex = this.Sublibraries.IndexOf(sublibrary);
                var assetIndex = sublibrary.Assets.IndexOf(asset);

                index = 0;
                index |= (((uint)assetIndex) & this.AssetMask) << 0;
                index |= (((uint)sublibraryIndex) & this.SublibraryMask) << this.AssetBits;
            }

            writer.WriteUInt32(index, this.SublibraryBits + this.AssetBits);
        }

        public string Decode(BitReader reader)
        {
            var index = reader.ReadUInt32(this.SublibraryBits + this.AssetBits);
            if (index == this.NoneIndex)
            {
                return "None";
            }

            var assetIndex = (int)((index >> 0) & this.AssetMask);
            var sublibraryIndex = (int)((index >> this.AssetBits) & this.SublibraryMask);

            if (sublibraryIndex < 0 || sublibraryIndex >= this.Sublibraries.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return this.Sublibraries[sublibraryIndex].GetAsset(assetIndex);
        }
    }
}

#pragma warning restore 649
