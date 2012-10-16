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

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gibbed.Borderlands2.GameInfo
{
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class ItemTypeDefinition
    {
        internal ItemTypeDefinition()
        {
        }

        [JsonProperty(PropertyName = "type", Required = Required.Always)]
        public ItemType Type;

        [JsonProperty(PropertyName = "name")]
        public string Name;

        [JsonProperty(PropertyName = "titles")]
        public List<string> Titles = new List<string>();

        [JsonProperty(PropertyName = "prefixes")]
        public List<string> Prefixes = new List<string>();

        [JsonProperty(PropertyName = "alpha_parts")]
        public List<string> AlphaParts = new List<string>();

        [JsonProperty(PropertyName = "beta_parts")]
        public List<string> BetaParts = new List<string>();

        [JsonProperty(PropertyName = "gamma_parts")]
        public List<string> GammaParts = new List<string>();

        [JsonProperty(PropertyName = "delta_parts")]
        public List<string> DeltaParts = new List<string>();

        [JsonProperty(PropertyName = "epsilon_parts")]
        public List<string> EpsilonParts = new List<string>();

        [JsonProperty(PropertyName = "zeta_parts")]
        public List<string> ZetaParts = new List<string>();

        [JsonProperty(PropertyName = "eta_parts")]
        public List<string> EtaParts = new List<string>();

        [JsonProperty(PropertyName = "theta_parts")]
        public List<string> ThetaParts = new List<string>();

        [JsonProperty(PropertyName = "material_parts")]
        public List<string> MaterialParts = new List<string>();
    }
}

#pragma warning restore 649
