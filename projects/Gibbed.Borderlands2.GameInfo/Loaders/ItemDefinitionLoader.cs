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
    internal static class ItemDefinitionLoader
    {
        public static InfoDictionary<ItemDefinition> Load()
        {
            try
            {
                var partLists = LoaderHelper.DeserializeDump<Dictionary<string, List<string>>>(
                    "Item Part Lists");
                var raws = LoaderHelper.DeserializeDump<Dictionary<string, Raw.ItemDefinition>>(
                    "Items");
                return new InfoDictionary<ItemDefinition>(
                    raws.ToDictionary(
                        kv => kv.Key,
                        kv => CreateItem(kv, partLists)));
            }
            catch (Exception e)
            {
                throw new InfoLoadException("failed to load items", e);
            }
        }

        private static ItemDefinition CreateItem(
            KeyValuePair<string, Raw.ItemDefinition> kv,
            Dictionary<string, List<string>> partLists)
        {
            var raw = kv.Value;
            return new ItemDefinition()
            {
                ResourcePath = kv.Key,
                Type = raw.Type,
                Name = raw.Name,
                HasFullName = raw.HasFullName,
                Titles = raw.Titles,
                Prefixes = raw.Prefixes,
                AlphaParts = GetPartList(raw.AlphaParts, partLists),
                BetaParts = GetPartList(raw.BetaParts, partLists),
                GammaParts = GetPartList(raw.GammaParts, partLists),
                DeltaParts = GetPartList(raw.DeltaParts, partLists),
                EpsilonParts = GetPartList(raw.EpsilonParts, partLists),
                ZetaParts = GetPartList(raw.ZetaParts, partLists),
                EtaParts = GetPartList(raw.EtaParts, partLists),
                ThetaParts = GetPartList(raw.ThetaParts, partLists),
                MaterialParts = GetPartList(raw.MaterialParts, partLists),
            };
        }

        private static List<string> GetPartList(string path, Dictionary<string, List<string>> partLists)
        {
            if (path == null)
            {
                return null;
            }
            if (partLists.TryGetValue(path, out var partList) == true)
            {
                return partList;
            }
            throw ResourceNotFoundException.Create("item part list", path);
        }
    }
}
