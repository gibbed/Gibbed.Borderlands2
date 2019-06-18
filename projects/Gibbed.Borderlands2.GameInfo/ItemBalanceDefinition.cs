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

namespace Gibbed.Borderlands2.GameInfo
{
    public sealed class ItemBalanceDefinition
    {
        internal ItemBalanceDefinition()
        {
        }

        public string ResourcePath { get; internal set; }
        public ItemBalanceDefinition Base { get; internal set; }
        public ItemDefinition Item { get; internal set; }
        public List<ItemDefinition> Items { get; internal set; }
        public List<string> Manufacturers { get; internal set; }
        public ItemBalancePartCollection Parts { get; internal set; }

        public bool IsSuitableFor(ItemDefinition item)
        {
            var current = this;
            do
            {
                if (current.Item != null && current.Item == item)
                {
                    return true;
                }

                if (current.Items != null && current.Items.Contains(item) == true)
                {
                    return true;
                }

                current = current.Base;
            }
            while (current != null);

            return false;
        }

        public ItemBalanceDefinition Create(ItemDefinition item)
        {
            var balances = this.GetBalances();

            ItemDefinition balanceItem = null;
            foreach (var balance in balances)
            {
                if (balance.Item != null)
                {
                    balanceItem = balance.Item;
                }
            }

            var wantBalanceParts = item.Type == ItemType.ClassMod ||
                                   item.Type == ItemType.CrossDLCClassMod ||
                                   item == balanceItem;

            List<string> getList(IEnumerable<string> enumerable)
            {
                return enumerable == null
                    ? new List<string>()
                    : enumerable.ToList();
            }
            var result = new ItemBalanceDefinition()
            {
                ResourcePath = this.ResourcePath,
                Item = balanceItem,
                Parts = new ItemBalancePartCollection()
                {
                    Mode = PartReplacementMode.Complete,
                    AlphaParts = getList(item.AlphaParts),
                    BetaParts = getList(item.BetaParts),
                    GammaParts = getList(item.GammaParts),
                    DeltaParts = getList(item.DeltaParts),
                    EpsilonParts = getList(item.EpsilonParts),
                    ZetaParts = getList(item.ZetaParts),
                    EtaParts = getList(item.EtaParts),
                    ThetaParts = getList(item.ThetaParts),
                    MaterialParts = getList(item.MaterialParts),
                },
            };

            if (wantBalanceParts == true)
            {
                foreach (var balance in balances)
                {
                    if (balance.Items != null)
                    {
                        result.Items = balance.Items.ToList();
                    }

                    if (balance.Manufacturers != null)
                    {
                        result.Manufacturers = balance.Manufacturers.ToList();
                    }

                    if (balance.Parts == null)
                    {
                        continue;
                    }

                    if (balance.Parts.Item != null)
                    {
                        result.Parts.Item = balance.Parts.Item;
                    }

                    AddPartList(balance.Parts.AlphaParts, balance.Parts.Mode, result.Parts.AlphaParts);
                    AddPartList(balance.Parts.BetaParts, balance.Parts.Mode, result.Parts.BetaParts);
                    AddPartList(balance.Parts.GammaParts, balance.Parts.Mode, result.Parts.GammaParts);
                    AddPartList(balance.Parts.DeltaParts, balance.Parts.Mode, result.Parts.DeltaParts);
                    AddPartList(balance.Parts.EpsilonParts, balance.Parts.Mode, result.Parts.EpsilonParts);
                    AddPartList(balance.Parts.ZetaParts, balance.Parts.Mode, result.Parts.ZetaParts);
                    AddPartList(balance.Parts.EtaParts, balance.Parts.Mode, result.Parts.EtaParts);
                    AddPartList(balance.Parts.ThetaParts, balance.Parts.Mode, result.Parts.ThetaParts);
                    AddPartList(balance.Parts.MaterialParts, balance.Parts.Mode, result.Parts.MaterialParts);
                }
            }

            if (result.Item != item && result.Items.Contains(item) == false)
            {
                throw new ResourceNotFoundException($"item type '{item.ResourcePath}' is not valid for '{this.ResourcePath}'");
            }

            return result;
        }

        private List<ItemBalanceDefinition> GetBalances()
        {
            var balances = new List<ItemBalanceDefinition>();
            var current = this;
            do
            {
                balances.Insert(0, current);
                current = current.Base;
            }
            while (current != null);
            return balances;
        }

        private static void AddPartList(IEnumerable<string> source, PartReplacementMode mode, List<string> destination)
        {
            switch (mode)
            {
                case PartReplacementMode.Additive:
                {
                    if (source != null)
                    {
                        destination.AddRange(source);
                    }
                    break;
                }

                case PartReplacementMode.Selective:
                {
                    if (source != null)
                    {
                        destination.Clear();
                        destination.AddRange(source);
                    }
                    break;
                }

                case PartReplacementMode.Complete:
                {
                    destination.Clear();
                    if (source != null)
                    {
                        destination.AddRange(source);
                    }
                    break;
                }

                default:
                {
                    throw new NotSupportedException();
                }
            }
        }
    }
}
