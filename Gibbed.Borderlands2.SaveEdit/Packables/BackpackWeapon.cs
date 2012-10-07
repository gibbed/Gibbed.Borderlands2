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

using Gibbed.Borderlands2.ProtoBufFormats.WillowTwoSave;

namespace Gibbed.Borderlands2.SaveEdit.Packables
{
    internal sealed class BackpackWeapon : PackableWeapon, IBackpackSlot
    {
        #region Fields
        private QuickWeaponSlot _QuickSlot;
        private PlayerMark _Mark;
        #endregion

        #region Properties
        public QuickWeaponSlot QuickSlot
        {
            get { return this._QuickSlot; }
            set
            {
                if (value != this._QuickSlot)
                {
                    this._QuickSlot = value;
                    this.NotifyOfPropertyChange(() => this.QuickSlot);
                    this.NotifyOfPropertyChange(() => this.DisplayGroup);
                }
            }
        }

        public PlayerMark Mark
        {
            get { return this._Mark; }
            set
            {
                if (value != this._Mark)
                {
                    this._Mark = value;
                    this.NotifyOfPropertyChange(() => this.Mark);
                }
            }
        }
        #endregion

        #region ICloneable Members
        public override object Clone()
        {
            return new BackpackWeapon()
            {
                WeaponTypeDefinition = this.WeaponTypeDefinition,
                BalanceDefinition = this.BalanceDefinition,
                ManufacturerDefinition = this.ManufacturerDefinition,
                ManufacturerGradeIndex = this.ManufacturerGradeIndex,
                BodyPartDefinition = this.BodyPartDefinition,
                GripPartDefinition = this.GripPartDefinition,
                BarrelPartDefinition = this.BarrelPartDefinition,
                SightPartDefinition = this.SightPartDefinition,
                StockPartDefinition = this.StockPartDefinition,
                ElementalPartDefinition = this.ElementalPartDefinition,
                Accessory1PartDefinition = this.Accessory1PartDefinition,
                Accessory2PartDefinition = this.Accessory2PartDefinition,
                MaterialPartDefinition = this.MaterialPartDefinition,
                PrefixPartDefinition = this.PrefixPartDefinition,
                TitlePartDefinition = this.TitlePartDefinition,
                GameStage = this.GameStage,
                UniqueId = this.UniqueId,
                AssetLibrarySetId = this.AssetLibrarySetId,
                QuickSlot = this.QuickSlot,
                Mark = this.Mark,
            };
        }
        #endregion

        public string DisplayGroup
        {
            get
            {
                if (this.QuickSlot != QuickWeaponSlot.None)
                {
                    return "Equipped";
                }

                return "Weapons";
            }
        }
    }
}
