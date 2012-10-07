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

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gibbed.Borderlands2.GameInfo
{
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class WeaponBalancePartCollection
    {
        internal WeaponBalancePartCollection()
        {
        }

        [JsonProperty(PropertyName = "type")]
        public string TypeDefinition;

        [JsonProperty(PropertyName = "mode")]
        public PartReplacementMode Mode;

        [JsonProperty(PropertyName = "body")]
        public List<string> BodyDefinitions;

        [JsonProperty(PropertyName = "grip")]
        public List<string> GripDefinitions;

        [JsonProperty(PropertyName = "barrel")]
        public List<string> BarrelDefinitions;

        [JsonProperty(PropertyName = "sight")]
        public List<string> SightDefinitions;

        [JsonProperty(PropertyName = "stock")]
        public List<string> StockDefinitions;

        [JsonProperty(PropertyName = "elemental")]
        public List<string> ElementalDefinitions;

        [JsonProperty(PropertyName = "accessory1")]
        public List<string> Accessory1Definitions;

        [JsonProperty(PropertyName = "accessory2")]
        public List<string> Accessory2Definitions;

        [JsonProperty(PropertyName = "material")]
        public List<string> MaterialDefinitions;
    }
}
