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
    internal static class ItemBalanceDefinitionLoader
    {
        public static InfoDictionary<ItemBalanceDefinition> Load(InfoDictionary<ItemDefinition> items)
        {
            try
            {
                var rawPartLists = LoaderHelper.DeserializeDump<Dictionary<string, Raw.ItemBalancePartCollection>>(
                    "Item Balance Part Lists");
                var partLists = new InfoDictionary<ItemBalancePartCollection>(
                    rawPartLists.ToDictionary(
                        kv => kv.Key,
                        kv => CreateItemBalancePartCollection(items, kv)));

                var raws = LoaderHelper.DeserializeDump<Dictionary<string, Raw.ItemBalanceDefinition>>(
                    "Item Balance");
                var balances = new InfoDictionary<ItemBalanceDefinition>(
                    raws.ToDictionary(
                        kv => kv.Key,
                        kv => CreateItemBalance(items, kv, partLists)));

                foreach (var kv in raws.Where(kv => string.IsNullOrEmpty(kv.Value.Base) == false))
                {
                    if (balances.TryGetValue(kv.Value.Base, out var baseBalance) == false)
                    {
                        throw ResourceNotFoundException.Create("item balance", kv.Value.Base);
                    }
                    balances[kv.Key].Base = baseBalance;
                }

                return balances;
            }
            catch (Exception e)
            {
                throw new InfoLoadException("failed to load item balance", e);
            }
        }

        private static ItemBalanceDefinition CreateItemBalance(
            InfoDictionary<ItemDefinition> items,
            KeyValuePair<string, Raw.ItemBalanceDefinition> kv,
            InfoDictionary<ItemBalancePartCollection> partLists)
        {
            var raw = kv.Value;
            return new ItemBalanceDefinition()
            {
                ResourcePath = kv.Key,
                Item = GetItem(items, raw.Item),
                Items = GetItems(items, raw.Items),
                Manufacturers = GetManufacturers(raw.Manufacturers),
                Parts = GetItemBalancePartCollection(partLists, raw.Parts),
            };
        }

        private static ItemBalancePartCollection CreateItemBalancePartCollection(
            InfoDictionary<ItemDefinition> items,
            KeyValuePair<string, Raw.ItemBalancePartCollection> kv)
        {
            var raw = kv.Value;

            ItemDefinition type = null;
            if (string.IsNullOrEmpty(raw.Item) == false)
            {
                if (items.TryGetValue(raw.Item, out type) == false)
                {
                    throw ResourceNotFoundException.Create("item type", raw.Item);
                }
            }

            return new ItemBalancePartCollection()
            {
                Item = type,
                Mode = raw.Mode,
                AlphaParts = raw.AlphaParts,
                BetaParts = raw.BetaParts,
                GammaParts = raw.GammaParts,
                DeltaParts = raw.DeltaParts,
                EpsilonParts = raw.EpsilonParts,
                ZetaParts = raw.ZetaParts,
                EtaParts = raw.EtaParts,
                ThetaParts = raw.ThetaParts,
                MaterialParts = raw.MaterialParts,
            };
        }

        private static ItemDefinition GetItem(InfoDictionary<ItemDefinition> items, string itemPath)
        {
            if (string.IsNullOrEmpty(itemPath) == true)
            {
                return null;
            }
            if (items.TryGetValue(itemPath, out var item) == true)
            {
                return item;
            }
            throw ResourceNotFoundException.Create("item", itemPath);
        }

        private static List<ItemDefinition> GetItems(
            InfoDictionary<ItemDefinition> items,
            IEnumerable<string> itemPaths)
        {
            if (itemPaths == null)
            {
                return null;
            }
            return itemPaths.Select(t =>
            {
                if (items.TryGetValue(t, out var item) == false)
                {
                    throw ResourceNotFoundException.Create("item type", t);
                }
                return item;
            }).ToList();
        }

        private static ItemBalancePartCollection GetItemBalancePartCollection(
            InfoDictionary<ItemBalancePartCollection> partLists,
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
            throw ResourceNotFoundException.Create("item balance part list", partListPath);
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
