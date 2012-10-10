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

using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Gibbed.Borderlands2.FileFormats.Items;
using Gibbed.Borderlands2.GameInfo;

namespace Gibbed.Borderlands2.SaveEdit
{
    internal class BaseWeaponViewModel : PropertyChangedBase
    {
        private readonly BackpackWeapon _Weapon;

        public BaseWeaponViewModel(BackpackWeapon weapon)
        {
            if (weapon == null)
            {
                throw new ArgumentNullException("weapon");
            }

            this._Weapon = weapon;

            this.TypeAssets =
                CreateAssetList(
                    InfoManager.WeaponBalanceDefinitions.Items.Where(wbd => wbd.Value.Types != null).SelectMany(
                        wbd => wbd.Value.Types).Distinct().OrderBy(s => s));
            this.BuildBalanceAssets();
        }

        #region Properties
        public string TypeDefinition
        {
            get { return this._Weapon.TypeDefinition; }
            set
            {
                this._Weapon.TypeDefinition = value;
                this.NotifyOfPropertyChange(() => this.TypeDefinition);
                this.BuildBalanceAssets();
            }
        }

        public string BalanceDefinition
        {
            get { return this._Weapon.BalanceDefinition; }
            set
            {
                this._Weapon.BalanceDefinition = value;
                this.NotifyOfPropertyChange(() => this.BalanceDefinition);
                this.BuildPartAssets();
            }
        }

        public string ManufacturerDefinition
        {
            get { return this._Weapon.ManufacturerDefinition; }
            set
            {
                this._Weapon.ManufacturerDefinition = value;
                this.NotifyOfPropertyChange(() => this.ManufacturerDefinition);
            }
        }

        public int ManufacturerGradeIndex
        {
            get { return this._Weapon.ManufacturerGradeIndex; }
            set
            {
                this._Weapon.ManufacturerGradeIndex = value;
                this.NotifyOfPropertyChange(() => this.ManufacturerGradeIndex);
            }
        }

        public string BodyPartDefinition
        {
            get { return this._Weapon.BodyPartDefinition; }
            set
            {
                this._Weapon.BodyPartDefinition = value;
                this.NotifyOfPropertyChange(() => this.BodyPartDefinition);
            }
        }

        public string GripPartDefinition
        {
            get { return this._Weapon.GripPartDefinition; }
            set
            {
                this._Weapon.GripPartDefinition = value;
                this.NotifyOfPropertyChange(() => this.GripPartDefinition);
            }
        }

        public string BarrelPartDefinition
        {
            get { return this._Weapon.BarrelPartDefinition; }
            set
            {
                this._Weapon.BarrelPartDefinition = value;
                this.NotifyOfPropertyChange(() => this.BarrelPartDefinition);
            }
        }

        public string SightPartDefinition
        {
            get { return this._Weapon.SightPartDefinition; }
            set
            {
                this._Weapon.SightPartDefinition = value;
                this.NotifyOfPropertyChange(() => this.SightPartDefinition);
            }
        }

        public string StockPartDefinition
        {
            get { return this._Weapon.StockPartDefinition; }
            set
            {
                this._Weapon.StockPartDefinition = value;
                this.NotifyOfPropertyChange(() => this.StockPartDefinition);
            }
        }

        public string ElementalPartDefinition
        {
            get { return this._Weapon.ElementalPartDefinition; }
            set
            {
                this._Weapon.ElementalPartDefinition = value;
                this.NotifyOfPropertyChange(() => this.ElementalPartDefinition);
            }
        }

        public string Accessory1PartDefinition
        {
            get { return this._Weapon.Accessory1PartDefinition; }
            set
            {
                this._Weapon.Accessory1PartDefinition = value;
                this.NotifyOfPropertyChange(() => this.Accessory1PartDefinition);
            }
        }

        public string Accessory2PartDefinition
        {
            get { return this._Weapon.Accessory2PartDefinition; }
            set
            {
                this._Weapon.Accessory2PartDefinition = value;
                this.NotifyOfPropertyChange(() => this.Accessory2PartDefinition);
            }
        }

        public string MaterialPartDefinition
        {
            get { return this._Weapon.MaterialPartDefinition; }
            set
            {
                this._Weapon.MaterialPartDefinition = value;
                this.NotifyOfPropertyChange(() => this.MaterialPartDefinition);
            }
        }

        public string PrefixPartDefinition
        {
            get { return this._Weapon.PrefixPartDefinition; }
            set
            {
                this._Weapon.PrefixPartDefinition = value;
                this.NotifyOfPropertyChange(() => this.PrefixPartDefinition);
            }
        }

        public string TitlePartDefinition
        {
            get { return this._Weapon.TitlePartDefinition; }
            set
            {
                this._Weapon.TitlePartDefinition = value;
                this.NotifyOfPropertyChange(() => this.TitlePartDefinition);
            }
        }

        public int GameStage
        {
            get { return this._Weapon.GameStage; }
            set
            {
                this._Weapon.GameStage = value;
                this.NotifyOfPropertyChange(() => this.GameStage);
            }
        }

        public int UniqueId
        {
            get { return this._Weapon.UniqueId; }
            set
            {
                this._Weapon.UniqueId = value;
                this.NotifyOfPropertyChange(() => this.UniqueId);
            }
        }

        public int AssetLibrarySetId
        {
            get { return this._Weapon.AssetLibrarySetId; }
            set
            {
                this._Weapon.AssetLibrarySetId = value;
                this.NotifyOfPropertyChange(() => this.AssetLibrarySetId);
            }
        }
        #endregion

        #region Display Properties
        public virtual string DisplayName
        {
            get { return "sheep"; }
        }

        public virtual string DisplayGroup
        {
            get { return "Weapons"; }
        }
        #endregion

        #region Asset Properties
        private static IEnumerable<string> CreateAssetList(IEnumerable<string> items)
        {
            var list = new List<string>();
            list.Add("None");
            if (items != null)
            {
                list.AddRange(items);
            }
            return list;
        }

        public IEnumerable<string> TypeAssets { get; private set; }

        private IEnumerable<string> _BalanceAssets;

        public IEnumerable<string> BalanceAssets
        {
            get { return this._BalanceAssets; }
            private set
            {
                this._BalanceAssets = value;
                this.NotifyOfPropertyChange(() => this.BalanceAssets);
            }
        }

        private IEnumerable<string> _BodyPartAssets;

        public IEnumerable<string> BodyPartAssets
        {
            get { return this._BodyPartAssets; }
            private set
            {
                this._BodyPartAssets = value;
                this.NotifyOfPropertyChange(() => this.BodyPartAssets);
            }
        }

        private IEnumerable<string> _GripPartAssets;

        public IEnumerable<string> GripPartAssets
        {
            get { return this._GripPartAssets; }
            private set
            {
                this._GripPartAssets = value;
                this.NotifyOfPropertyChange(() => this.GripPartAssets);
            }
        }

        private IEnumerable<string> _BarrelPartAssets;

        public IEnumerable<string> BarrelPartAssets
        {
            get { return this._BarrelPartAssets; }
            private set
            {
                this._BarrelPartAssets = value;
                this.NotifyOfPropertyChange(() => this.BarrelPartAssets);
            }
        }

        private IEnumerable<string> _SightPartAssets;

        public IEnumerable<string> SightPartAssets
        {
            get { return this._SightPartAssets; }
            private set
            {
                this._SightPartAssets = value;
                this.NotifyOfPropertyChange(() => this.SightPartAssets);
            }
        }

        private IEnumerable<string> _StockPartAssets;

        public IEnumerable<string> StockPartAssets
        {
            get { return this._StockPartAssets; }
            private set
            {
                this._StockPartAssets = value;
                this.NotifyOfPropertyChange(() => this.StockPartAssets);
            }
        }

        private IEnumerable<string> _ElementalPartAssets;

        public IEnumerable<string> ElementalPartAssets
        {
            get { return this._ElementalPartAssets; }
            private set
            {
                this._ElementalPartAssets = value;
                this.NotifyOfPropertyChange(() => this.ElementalPartAssets);
            }
        }

        private IEnumerable<string> _Accessory1PartAssets;

        public IEnumerable<string> Accessory1PartAssets
        {
            get { return this._Accessory1PartAssets; }
            private set
            {
                this._Accessory1PartAssets = value;
                this.NotifyOfPropertyChange(() => this.Accessory1PartAssets);
            }
        }

        private IEnumerable<string> _Accessory2PartAssets;

        public IEnumerable<string> Accessory2PartAssets
        {
            get { return this._Accessory2PartAssets; }
            private set
            {
                this._Accessory2PartAssets = value;
                this.NotifyOfPropertyChange(() => this.Accessory2PartAssets);
            }
        }

        private IEnumerable<string> _MaterialPartAssets;

        public IEnumerable<string> MaterialPartAssets
        {
            get { return this._MaterialPartAssets; }
            private set
            {
                this._MaterialPartAssets = value;
                this.NotifyOfPropertyChange(() => this.MaterialPartAssets);
            }
        }

        private readonly string[] _NoneAssets = new[]
        {
            "None"
        };

        private void BuildBalanceAssets()
        {
            this.BalanceAssets =
                CreateAssetList(
                    InfoManager.WeaponBalanceDefinitions.Items.Where(
                        wbd => wbd.Value.Types != null && wbd.Value.Types.Contains(this.TypeDefinition) == true).Select(
                            wbd => wbd.Key).Distinct().OrderBy(s => s));
            this.BuildPartAssets();
        }

        private void BuildPartAssets()
        {
            if (this.BalanceAssets.Contains(this.BalanceDefinition) == false ||
                this.BalanceDefinition == "None")
            {
                this.BodyPartAssets = _NoneAssets;
                this.BodyPartAssets = _NoneAssets;
                this.GripPartAssets = _NoneAssets;
                this.BarrelPartAssets = _NoneAssets;
                this.SightPartAssets = _NoneAssets;
                this.StockPartAssets = _NoneAssets;
                this.ElementalPartAssets = _NoneAssets;
                this.Accessory1PartAssets = _NoneAssets;
                this.Accessory2PartAssets = _NoneAssets;
                this.MaterialPartAssets = _NoneAssets;
            }
            else
            {
                var balanceDefinition = InfoManager.WeaponBalanceDefinitions[this.BalanceDefinition];
                this.BodyPartAssets = CreateAssetList(balanceDefinition.Parts.BodyDefinitions.OrderBy(s => s).Distinct());
                this.GripPartAssets = CreateAssetList(balanceDefinition.Parts.GripDefinitions.OrderBy(s => s).Distinct());
                this.BarrelPartAssets =
                    CreateAssetList(balanceDefinition.Parts.BarrelDefinitions.OrderBy(s => s).Distinct());
                this.SightPartAssets =
                    CreateAssetList(balanceDefinition.Parts.SightDefinitions.OrderBy(s => s).Distinct());
                this.StockPartAssets =
                    CreateAssetList(balanceDefinition.Parts.StockDefinitions.OrderBy(s => s).Distinct());
                this.ElementalPartAssets =
                    CreateAssetList(balanceDefinition.Parts.ElementalDefinitions.OrderBy(s => s).Distinct());
                this.Accessory1PartAssets =
                    CreateAssetList(balanceDefinition.Parts.Accessory1Definitions.OrderBy(s => s).Distinct());
                this.Accessory2PartAssets =
                    CreateAssetList(balanceDefinition.Parts.Accessory2Definitions.OrderBy(s => s).Distinct());
                this.MaterialPartAssets =
                    CreateAssetList(balanceDefinition.Parts.MaterialDefinitions.OrderBy(s => s).Distinct());
            }
        }
        #endregion
    }
}
