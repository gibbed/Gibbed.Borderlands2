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
using Newtonsoft.Json;

namespace Gibbed.Borderlands2.GameInfo
{
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class AssetSublibrary
    {
        internal AssetSublibrary()
        {
        }

        [JsonProperty(PropertyName = "description")]
        public string Description;

        /// <summary>
        /// The UnrealEngine package that contains the assets.
        /// </summary>
        [JsonProperty(PropertyName = "package")]
        public string Package;

        /// <summary>
        /// Paths of assets within the package.
        /// </summary>
        [JsonProperty(PropertyName = "assets")]
        public List<string> Assets = new List<string>();

        public string GetAsset(int index)
        {
            if (index < 0 || index >= this.Assets.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return this.Package + "." + this.Assets[index];
        }
    }
}

#pragma warning restore 649
