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

using Newtonsoft.Json;

namespace Gibbed.Borderlands2.GameInfo
{
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class AssetLibraryConfiguration
    {
        internal AssetLibraryConfiguration()
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
            get { return (1u << this.SublibraryBits - 1) - 1; }
        }

        public uint UseSetIdMask
        {
            get { return (1u << this.SublibraryBits - 1); }
        }

        public uint AssetMask
        {
            get { return (1u << this.AssetBits) - 1; }
        }
    }
}

#pragma warning restore 649
