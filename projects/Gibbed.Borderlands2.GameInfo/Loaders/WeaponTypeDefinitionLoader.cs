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

using System;
using System.Collections.Generic;
using System.Linq;

namespace Gibbed.Borderlands2.GameInfo.Loaders
{
    internal static class WeaponTypeDefinitionLoader
    {
        public static InfoDictionary<WeaponTypeDefinition> Load()
        {
            try
            {
                var partLists = LoaderHelper.DeserializeDump<Dictionary<string, List<string>>>(
                    "Weapon Part Lists");
                var raws = LoaderHelper.DeserializeDump<Dictionary<string, Raw.WeaponTypeDefinition>>(
                    "Weapon Types");
                return new InfoDictionary<WeaponTypeDefinition>(
                    raws.ToDictionary(
                        kv => kv.Key,
                        kv => CreateWeaponTypeDefinition(kv, partLists)));
            }
            catch (Exception e)
            {
                throw new InfoLoadException("failed to load weapon types", e);
            }
        }

        private static WeaponTypeDefinition CreateWeaponTypeDefinition(
            KeyValuePair<string, Raw.WeaponTypeDefinition> kv,
            Dictionary<string, List<string>> partLists)
        {
            var raw = kv.Value;
            return new WeaponTypeDefinition()
            {
                ResourcePath = kv.Key,
                Type = raw.Type,
                Name = raw.Name,
                Titles = raw.Titles,
                Prefixes = raw.Prefixes,
                BodyParts = GetPartList(raw.BodyParts, partLists),
                GripParts = GetPartList(raw.GripParts, partLists),
                BarrelParts = GetPartList(raw.BarrelParts, partLists),
                SightParts = GetPartList(raw.SightParts, partLists),
                StockParts = GetPartList(raw.StockParts, partLists),
                ElementalParts = GetPartList(raw.ElementalParts, partLists),
                Accessory1Parts = GetPartList(raw.Accessory1Parts, partLists),
                Accessory2Parts = GetPartList(raw.Accessory2Parts, partLists),
                MaterialParts = GetPartList(raw.MaterialParts, partLists),
            };
        }

        private static List<string> GetPartList(string path, Dictionary<string, List<string>> partLists)
        {
            if (path == null)
            {
                return new List<string>();
            }
            if (partLists.TryGetValue(path, out var partList) == true)
            {
                return partList;
            }
            throw ResourceNotFoundException.Create("weapon type part list", path);
        }
    }
}
