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
    internal static class WeaponBalanceDefinitionLoader
    {
        public static InfoDictionary<WeaponBalanceDefinition> Load(InfoDictionary<WeaponTypeDefinition> weaponTypes)
        {
            try
            {
                var rawPartLists = LoaderHelper.DeserializeDump<Dictionary<string, Raw.WeaponBalancePartCollection>>(
                    "Weapon Balance Part Lists");
                var partLists = new InfoDictionary<WeaponBalancePartCollection>(
                    rawPartLists.ToDictionary(
                        kv => kv.Key,
                        kv => CreateWeaponBalancePartCollection(weaponTypes, kv)));

                var raws = LoaderHelper.DeserializeDump<Dictionary<string, Raw.WeaponBalanceDefinition>>(
                    "Weapon Balance");
                var balances = new InfoDictionary<WeaponBalanceDefinition>(
                    raws.ToDictionary(
                        kv => kv.Key,
                        kv => CreateWeaponBalance(weaponTypes, kv, partLists)));

                foreach (var kv in raws.Where(kv => string.IsNullOrEmpty(kv.Value.Base) == false))
                {
                    if (balances.TryGetValue(kv.Value.Base, out var baseBalance) == false)
                    {
                        throw ResourceNotFoundException.Create("weapon balance", kv.Value.Base);
                    }
                    balances[kv.Key].Base = baseBalance;
                }

                return balances;
            }
            catch (Exception e)
            {
                throw new InfoLoadException("failed to load weapon balance", e);
            }
        }

        private static WeaponBalanceDefinition CreateWeaponBalance(
            InfoDictionary<WeaponTypeDefinition> weaponTypes,
            KeyValuePair<string, Raw.WeaponBalanceDefinition> kv,
            InfoDictionary<WeaponBalancePartCollection> partLists)
        {
            var raw = kv.Value;
            return new WeaponBalanceDefinition()
            {
                ResourcePath = kv.Key,
                WeaponType = GetWeaponType(weaponTypes, raw.WeaponType),
                Manufacturers = GetManufacturers(raw.Manufacturers),
                Parts = GetWeaponBalancePartCollection(partLists, raw.Parts),
            };
        }

        private static WeaponBalancePartCollection CreateWeaponBalancePartCollection(
            InfoDictionary<WeaponTypeDefinition> weaponTypes,
            KeyValuePair<string, Raw.WeaponBalancePartCollection> kv)
        {
            var raw = kv.Value;

            WeaponTypeDefinition type = null;
            if (string.IsNullOrEmpty(raw.WeaponType) == false)
            {
                if (weaponTypes.TryGetValue(raw.WeaponType, out type) == false)
                {
                    throw ResourceNotFoundException.Create("weapon type", raw.WeaponType);
                }
            }

            return new WeaponBalancePartCollection()
            {
                WeaponType = type,
                Mode = raw.Mode,
                BodyParts = raw.BodyParts,
                GripParts = raw.GripParts,
                BarrelParts = raw.BarrelParts,
                SightParts = raw.SightParts,
                StockParts = raw.StockParts,
                ElementalParts = raw.ElementalParts,
                Accessory1Parts = raw.Accessory1Parts,
                Accessory2Parts = raw.Accessory2Parts,
                MaterialParts = raw.MaterialParts,
            };
        }

        private static WeaponTypeDefinition GetWeaponType(InfoDictionary<WeaponTypeDefinition> types, string path)
        {
            if (string.IsNullOrEmpty(path) == true)
            {
                return null;
            }
            if (types.TryGetValue(path, out var type) == true)
            {
                return type;
            }
            throw ResourceNotFoundException.Create("weapon type", path);
        }

        private static WeaponBalancePartCollection GetWeaponBalancePartCollection(
            InfoDictionary<WeaponBalancePartCollection> partLists,
            string partListPath)
        {
            if (string.IsNullOrEmpty(partListPath) == true)
            {
                return null;
            }
            if (partLists.TryGetValue(partListPath, out var partList) == true)
            {
                return partList;
            }
            throw ResourceNotFoundException.Create("weapon balance part list", partListPath);
        }

        private static List<string> GetManufacturers(IEnumerable<string> manufacturers)
        {
            if (manufacturers == null)
            {
                return null;
            }
            return manufacturers.ToList();
        }
    }
}
