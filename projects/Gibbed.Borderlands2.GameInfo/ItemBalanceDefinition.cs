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
        public ItemTypeDefinition Type { get; internal set; }
        public List<ItemTypeDefinition> Types { get; internal set; }
        public List<string> Manufacturers { get; internal set; }
        public ItemBalancePartCollection Parts { get; internal set; }

        public bool IsSuitableFor(ItemTypeDefinition type)
        {
            var current = this;
            do
            {
                if (current.Type != null ||
                    current.Types != null)
                {
                    if (current.Type == type ||
                        (current.Types != null && current.Types.Contains(type) == true))
                    {
                        return true;
                    }

                    return false;
                }

                current = current.Base;
            }
            while (current != null);

            return false;
        }

        public ItemBalanceDefinition Create(ItemTypeDefinition type)
        {
            var result = new ItemBalanceDefinition()
            {
                ResourcePath = this.ResourcePath,
                Parts = new ItemBalancePartCollection()
                {
                    Mode = PartReplacementMode.Complete,
                    AlphaParts = type.AlphaParts.ToList(),
                    BetaParts = type.BetaParts.ToList(),
                    GammaParts = type.GammaParts.ToList(),
                    DeltaParts = type.DeltaParts.ToList(),
                    EpsilonParts = type.EpsilonParts.ToList(),
                    ZetaParts = type.ZetaParts.ToList(),
                    EtaParts = type.EtaParts.ToList(),
                    ThetaParts = type.ThetaParts.ToList(),
                    MaterialParts = type.MaterialParts.ToList(),
                },
            };

            var balances = new List<ItemBalanceDefinition>();
            var current = this;
            do
            {
                balances.Insert(0, current);
                current = current.Base;
            }
            while (current != null);

            foreach (var balance in balances)
            {
                if (balance.Type != null)
                {
                    result.Type = balance.Type;
                }

                if (balance.Types != null)
                {
                    result.Types = balance.Types.ToList();
                }

                if (balance.Manufacturers != null)
                {
                    result.Manufacturers = balance.Manufacturers.ToList();
                }

                if (balance.Parts == null)
                {
                    continue;
                }

                if (balance.Parts.Type != null)
                {
                    result.Parts.Type = balance.Parts.Type;
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

            if (result.Type != type && result.Types.Contains(type) == false)
            {
                throw new ResourceNotFoundException(string.Format(
                    "item type '{0}' is not valid for '{1}'",
                    type.ResourcePath,
                    this.ResourcePath));
            }

            return result;
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
