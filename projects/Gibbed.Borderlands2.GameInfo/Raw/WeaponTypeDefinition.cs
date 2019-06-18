/* Copyright (c) 2019 Rick (rick 'at' gibbed 'dot' us)
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

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gibbed.Borderlands2.GameInfo.Raw
{
    [JsonObject(MemberSerialization.OptIn)]
    internal sealed class WeaponTypeDefinition
    {
        public WeaponTypeDefinition()
        {
            this.Titles = new List<string>();
            this.Prefixes = new List<string>();
        }

        [JsonProperty(PropertyName = "type", Required = Required.Always)]
        public WeaponType Type { get; set; }

        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "titles")]
        public List<string> Titles { get; set; }

        [JsonProperty(PropertyName = "prefixes")]
        public List<string> Prefixes { get; set; }

        [JsonProperty(PropertyName = "body_parts")]
        public string BodyParts { get; set; }

        [JsonProperty(PropertyName = "grip_parts")]
        public string GripParts { get; set; }

        [JsonProperty(PropertyName = "barrel_parts")]
        public string BarrelParts { get; set; }

        [JsonProperty(PropertyName = "sight_parts")]
        public string SightParts { get; set; }

        [JsonProperty(PropertyName = "stock_parts")]
        public string StockParts { get; set; }

        [JsonProperty(PropertyName = "elemental_parts")]
        public string ElementalParts { get; set; }

        [JsonProperty(PropertyName = "accessory1_parts")]
        public string Accessory1Parts { get; set; }

        [JsonProperty(PropertyName = "accessory2_parts")]
        public string Accessory2Parts { get; set; }

        [JsonProperty(PropertyName = "material_parts")]
        public string MaterialParts { get; set; }
    }
}

#pragma warning restore 649
