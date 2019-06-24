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
    public sealed class WeaponBalanceDefinition
    {
        internal WeaponBalanceDefinition()
        {
        }

        public string ResourcePath { get; internal set; }
        public WeaponBalanceDefinition Base { get; internal set; }
        public WeaponTypeDefinition WeaponType { get; internal set; }
        public List<string> Manufacturers { get; internal set; }
        public WeaponBalancePartCollection Parts { get; internal set; }

        public bool IsSuitableFor(WeaponTypeDefinition weaponType)
        {
            var current = this;
            do
            {
                if (current.WeaponType != null)
                {
                    if (current.WeaponType == weaponType)
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

        public WeaponBalanceDefinition Create(WeaponTypeDefinition weaponType)
        {
            var result = new WeaponBalanceDefinition()
            {
                ResourcePath = this.ResourcePath,
                Parts = new WeaponBalancePartCollection()
                {
                    Mode = PartReplacementMode.Complete,
                    BodyParts = new List<string>(),
                    GripParts = new List<string>(),
                    BarrelParts = new List<string>(),
                    SightParts = new List<string>(),
                    StockParts = new List<string>(),
                    ElementalParts = new List<string>(),
                    Accessory1Parts = new List<string>(),
                    Accessory2Parts = new List<string>(),
                    MaterialParts = new List<string>(),
                },
            };

            var balances = new List<WeaponBalanceDefinition>();

            var current = this;
            do
            {
                balances.Insert(0, current);
                current = current.Base;
            }
            while (current != null);

            foreach (var balance in balances)
            {
                if (balance.WeaponType != null)
                {
                    result.WeaponType = balance.WeaponType;
                }

                if (balance.Manufacturers != null)
                {
                    result.Manufacturers = balance.Manufacturers.ToList();
                }

                if (balance.Parts == null)
                {
                    continue;
                }

                if (balance.Parts.WeaponType != null)
                {
                    result.Parts.WeaponType = balance.Parts.WeaponType;
                }

                AddPartList(balance.Parts.BodyParts, balance.Parts.Mode, result.Parts.BodyParts);
                AddPartList(balance.Parts.GripParts, balance.Parts.Mode, result.Parts.GripParts);
                AddPartList(balance.Parts.BarrelParts, balance.Parts.Mode, result.Parts.BarrelParts);
                AddPartList(balance.Parts.SightParts, balance.Parts.Mode, result.Parts.SightParts);
                AddPartList(balance.Parts.StockParts, balance.Parts.Mode, result.Parts.StockParts);
                AddPartList(balance.Parts.ElementalParts, balance.Parts.Mode, result.Parts.ElementalParts);
                AddPartList(balance.Parts.Accessory1Parts, balance.Parts.Mode, result.Parts.Accessory1Parts);
                AddPartList(balance.Parts.Accessory2Parts, balance.Parts.Mode, result.Parts.Accessory2Parts);
                AddPartList(balance.Parts.MaterialParts, balance.Parts.Mode, result.Parts.MaterialParts);
            }

            if (result.WeaponType != weaponType)
            {
                throw new ResourceNotFoundException($"weapon type '{weaponType.ResourcePath}' is not valid for '{this.ResourcePath}'");
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
                        destination.AddRange(source.Where(s => s != null));
                    }
                    break;
                }

                case PartReplacementMode.Selective:
                {
                    if (source != null)
                    {
                        destination.Clear();
                        destination.AddRange(source.Where(s => s != null));
                    }
                    break;
                }

                case PartReplacementMode.Complete:
                {
                    destination.Clear();
                    if (source != null)
                    {
                        destination.AddRange(source.Where(s => s != null));
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
