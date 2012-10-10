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
using Newtonsoft.Json;
using Gibbed.Borderlands2.FileFormats;

namespace Gibbed.Borderlands2.GameInfo
{
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class AssetLibraryManager
    {
        internal AssetLibraryManager()
        {
        }

        [JsonProperty(PropertyName = "version", Required = Required.Always)]
        public int Version;

        [JsonProperty(PropertyName = "configs")]
        public Dictionary<AssetGroup, AssetLibraryConfiguration> Configurations = new Dictionary<AssetGroup, AssetLibraryConfiguration>();

        [JsonProperty(PropertyName = "sets")]
        public List<AssetLibrarySet> Sets = new List<AssetLibrarySet>();

        public AssetLibrarySet GetSet(int id)
        {
            return this.Sets.SingleOrDefault(s => s.Id == id);
        }

        private bool GetIndex(int setId, AssetGroup group, string package, string asset, out uint index)
        {
            var config = this.Configurations[group];

            var set = this.GetSet(setId);
            var library = set.Libraries[group];

            var sublibrary = library.Sublibraries.FirstOrDefault(sl => sl.Package == package && sl.Assets.Contains(asset));
            if (sublibrary == null)
            {
                index = 0;
                return false;
            }

            var sublibraryIndex = library.Sublibraries.IndexOf(sublibrary);
            var assetIndex = sublibrary.Assets.IndexOf(asset);

            index = 0;
            index |= (((uint)assetIndex) & config.AssetMask) << 0;
            index |= (((uint)sublibraryIndex) & config.SublibraryMask) << config.AssetBits;

            if (setId != 0)
            {
                index |= 1u << config.AssetBits + config.SublibraryBits - 1;
            }

            return true;
        }

        public void Encode(BitWriter writer, int setId, AssetGroup group, string value)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }

            if (string.IsNullOrEmpty(value) == true)
            {
                throw new ArgumentNullException("value");
            }

            var config = this.Configurations[group];

            uint index;
            if (value == "None")
            {
                index = config.NoneIndex;
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

                if (this.GetIndex(setId, group, package, asset, out index) == false)
                {
                    if (this.GetIndex(0, group, package, asset, out index) == false)
                    {
                        throw new ArgumentException("unsupported asset");
                    }
                }
            }

            writer.WriteUInt32(index, config.SublibraryBits + config.AssetBits);
        }

        public string Decode(BitReader reader, int setId, AssetGroup group)
        {
            var config = this.Configurations[group];

            var index = reader.ReadUInt32(config.SublibraryBits + config.AssetBits);
            if (index == config.NoneIndex)
            {
                return "None";
            }

            var assetIndex = (int)((index >> 0) & config.AssetMask);
            var sublibraryIndex = (int)((index >> config.AssetBits) & config.SublibraryMask);
            var useSetId = ((index >> config.AssetBits) & config.UseSetIdMask) != 0;

            var set = this.GetSet(useSetId == false ? 0 : setId);
            var library = set.Libraries[group];

            if (sublibraryIndex < 0 || sublibraryIndex >= library.Sublibraries.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return library.Sublibraries[sublibraryIndex].GetAsset(assetIndex);
        }
    }
}

#pragma warning restore 649
