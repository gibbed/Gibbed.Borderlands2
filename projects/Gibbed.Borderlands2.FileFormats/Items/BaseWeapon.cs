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

using System.ComponentModel;
using Gibbed.Borderlands2.GameInfo;

namespace Gibbed.Borderlands2.FileFormats.Items
{
    public class BaseWeapon : IPackableWeapon, INotifyPropertyChanged
    {
        public BaseWeapon()
        {
        }

        public BaseWeapon(BaseWeapon other)
        {
            this.WeaponType = other.WeaponType;
            this.Balance = other.Balance;
            this.Manufacturer = other.Manufacturer;
            this.ManufacturerGradeIndex = other.ManufacturerGradeIndex;
            this.BodyPart = other.BodyPart;
            this.GripPart = other.GripPart;
            this.BarrelPart = other.BarrelPart;
            this.SightPart = other.SightPart;
            this.StockPart = other.StockPart;
            this.ElementalPart = other.ElementalPart;
            this.Accessory1Part = other.Accessory1Part;
            this.Accessory2Part = other.Accessory2Part;
            this.MaterialPart = other.MaterialPart;
            this.PrefixPart = other.PrefixPart;
            this.TitlePart = other.TitlePart;
            this.GameStage = other.GameStage;
            this.UniqueId = other.UniqueId;
            this.AssetLibrarySetId = other.AssetLibrarySetId;
        }

        #region Fields
        private string _WeaponType = "None";
        private string _Balance = "None";
        private string _Manufacturer = "None";
        private int _ManufacturerGradeIndex;
        private string _BodyPart = "None";
        private string _GripPart = "None";
        private string _BarrelPart = "None";
        private string _SightPart = "None";
        private string _StockPart = "None";
        private string _ElementalPart = "None";
        private string _Accessory1Part = "None";
        private string _Accessory2Part = "None";
        private string _MaterialPart = "None";
        private string _PrefixPart = "None";
        private string _TitlePart = "None";
        private int _GameStage;
        private int _UniqueId;
        private int _AssetLibrarySetId;
        #endregion

        #region IPackable Members
        public void Unpack(PackedWeapon packed, Platform platform)
        {
            var m = InfoManager.AssetLibraryManager;
            var si = this.AssetLibrarySetId;
            this.WeaponType = m.Lookup(packed.WeaponType, platform, si, AssetGroup.WeaponTypes);
            this.Balance = m.Lookup(packed.Balance, platform, si, AssetGroup.BalanceDefs);
            this.Manufacturer = m.Lookup(packed.Manufacturer, platform, si, AssetGroup.Manufacturers);
            this.ManufacturerGradeIndex = packed.ManufacturerGradeIndex;
            this.GameStage = packed.GameStage;
            this.BodyPart = m.Lookup(packed.BodyPart, platform, si, AssetGroup.WeaponParts);
            this.GripPart = m.Lookup(packed.GripPart, platform, si, AssetGroup.WeaponParts);
            this.BarrelPart = m.Lookup(packed.BarrelPart, platform, si, AssetGroup.WeaponParts);
            this.SightPart = m.Lookup(packed.SightPart, platform, si, AssetGroup.WeaponParts);
            this.StockPart = m.Lookup(packed.StockPart, platform, si, AssetGroup.WeaponParts);
            this.ElementalPart = m.Lookup(packed.ElementalPart, platform, si, AssetGroup.WeaponParts);
            this.Accessory1Part = m.Lookup(packed.Accessory1Part, platform, si, AssetGroup.WeaponParts);
            this.Accessory2Part = m.Lookup(packed.Accessory2Part, platform, si, AssetGroup.WeaponParts);
            this.MaterialPart = m.Lookup(packed.MaterialPart, platform, si, AssetGroup.WeaponParts);
            this.PrefixPart = m.Lookup(packed.PrefixPart, platform, si, AssetGroup.WeaponParts);
            this.TitlePart = m.Lookup(packed.TitlePart, platform, si, AssetGroup.WeaponParts);
        }

        public PackedWeapon Pack(Platform platform)
        {
            var m = InfoManager.AssetLibraryManager;
            var si = this.AssetLibrarySetId;
            return new PackedWeapon()
            {
                WeaponType = m.Lookup(this.WeaponType, platform, si, AssetGroup.WeaponTypes),
                Balance = m.Lookup(this.Balance, platform, si, AssetGroup.BalanceDefs),
                Manufacturer = m.Lookup(this.Manufacturer, platform, si, AssetGroup.Manufacturers),
                ManufacturerGradeIndex = this.ManufacturerGradeIndex,
                GameStage = this.GameStage,
                BodyPart = m.Lookup(this.BodyPart, platform, si, AssetGroup.WeaponParts),
                GripPart = m.Lookup(this.GripPart, platform, si, AssetGroup.WeaponParts),
                BarrelPart = m.Lookup(this.BarrelPart, platform, si, AssetGroup.WeaponParts),
                SightPart = m.Lookup(this.SightPart, platform, si, AssetGroup.WeaponParts),
                StockPart = m.Lookup(this.StockPart, platform, si, AssetGroup.WeaponParts),
                ElementalPart = m.Lookup(this.ElementalPart, platform, si, AssetGroup.WeaponParts),
                Accessory1Part = m.Lookup(this.Accessory1Part, platform, si, AssetGroup.WeaponParts),
                Accessory2Part = m.Lookup(this.Accessory2Part, platform, si, AssetGroup.WeaponParts),
                MaterialPart = m.Lookup(this.MaterialPart, platform, si, AssetGroup.WeaponParts),
                PrefixPart = m.Lookup(this.PrefixPart, platform, si, AssetGroup.WeaponParts),
                TitlePart = m.Lookup(this.TitlePart, platform, si, AssetGroup.WeaponParts)
            };
        }
        #endregion

        #region Properties
        public string WeaponType
        {
            get { return this._WeaponType; }
            set
            {
                if (value != this._WeaponType)
                {
                    this._WeaponType = value;
                    this.NotifyOfPropertyChange(nameof(WeaponType));
                }
            }
        }

        public string Balance
        {
            get { return this._Balance; }
            set
            {
                if (value != this._Balance)
                {
                    this._Balance = value;
                    this.NotifyOfPropertyChange(nameof(Balance));
                }
            }
        }

        public string Manufacturer
        {
            get { return this._Manufacturer; }
            set
            {
                if (value != this._Manufacturer)
                {
                    this._Manufacturer = value;
                    this.NotifyOfPropertyChange(nameof(Manufacturer));
                }
            }
        }

        public int ManufacturerGradeIndex
        {
            get { return this._ManufacturerGradeIndex; }
            set
            {
                if (value != this._ManufacturerGradeIndex)
                {
                    this._ManufacturerGradeIndex = value;
                    this.NotifyOfPropertyChange(nameof(ManufacturerGradeIndex));
                }
            }
        }

        public string BodyPart
        {
            get { return this._BodyPart; }
            set
            {
                if (value != this._BodyPart)
                {
                    this._BodyPart = value;
                    this.NotifyOfPropertyChange(nameof(BodyPart));
                }
            }
        }

        public string GripPart
        {
            get { return this._GripPart; }
            set
            {
                if (value != this._GripPart)
                {
                    this._GripPart = value;
                    this.NotifyOfPropertyChange(nameof(GripPart));
                }
            }
        }

        public string BarrelPart
        {
            get { return this._BarrelPart; }
            set
            {
                if (value != this._BarrelPart)
                {
                    this._BarrelPart = value;
                    this.NotifyOfPropertyChange(nameof(BarrelPart));
                }
            }
        }

        public string SightPart
        {
            get { return this._SightPart; }
            set
            {
                if (value != this._SightPart)
                {
                    this._SightPart = value;
                    this.NotifyOfPropertyChange(nameof(SightPart));
                }
            }
        }

        public string StockPart
        {
            get { return this._StockPart; }
            set
            {
                if (value != this._StockPart)
                {
                    this._StockPart = value;
                    this.NotifyOfPropertyChange(nameof(StockPart));
                }
            }
        }

        public string ElementalPart
        {
            get { return this._ElementalPart; }
            set
            {
                if (value != this._ElementalPart)
                {
                    this._ElementalPart = value;
                    this.NotifyOfPropertyChange(nameof(ElementalPart));
                }
            }
        }

        public string Accessory1Part
        {
            get { return this._Accessory1Part; }
            set
            {
                if (value != this._Accessory1Part)
                {
                    this._Accessory1Part = value;
                    this.NotifyOfPropertyChange(nameof(Accessory1Part));
                }
            }
        }

        public string Accessory2Part
        {
            get { return this._Accessory2Part; }
            set
            {
                if (value != this._Accessory2Part)
                {
                    this._Accessory2Part = value;
                    this.NotifyOfPropertyChange(nameof(Accessory2Part));
                }
            }
        }

        public string MaterialPart
        {
            get { return this._MaterialPart; }
            set
            {
                if (value != this._MaterialPart)
                {
                    this._MaterialPart = value;
                    this.NotifyOfPropertyChange(nameof(MaterialPart));
                }
            }
        }

        public string PrefixPart
        {
            get { return this._PrefixPart; }
            set
            {
                if (value != this._PrefixPart)
                {
                    this._PrefixPart = value;
                    this.NotifyOfPropertyChange(nameof(PrefixPart));
                }
            }
        }

        public string TitlePart
        {
            get { return this._TitlePart; }
            set
            {
                if (value != this._TitlePart)
                {
                    this._TitlePart = value;
                    this.NotifyOfPropertyChange(nameof(TitlePart));
                }
            }
        }

        public int GameStage
        {
            get { return this._GameStage; }
            set
            {
                if (value != this._GameStage)
                {
                    this._GameStage = value;
                    this.NotifyOfPropertyChange(nameof(GameStage));
                }
            }
        }

        public int UniqueId
        {
            get { return this._UniqueId; }
            set
            {
                if (value != this._UniqueId)
                {
                    this._UniqueId = value;
                    this.NotifyOfPropertyChange(nameof(UniqueId));
                }
            }
        }

        public int AssetLibrarySetId
        {
            get { return this._AssetLibrarySetId; }
            set
            {
                if (value != this._AssetLibrarySetId)
                {
                    this._AssetLibrarySetId = value;
                    this.NotifyOfPropertyChange(nameof(AssetLibrarySetId));
                }
            }
        }
        #endregion

        #region ICloneable Members
        public virtual object Clone()
        {
            return new BaseWeapon()
            {
                WeaponType = this.WeaponType,
                Balance = this.Balance,
                Manufacturer = this.Manufacturer,
                ManufacturerGradeIndex = this.ManufacturerGradeIndex,
                BodyPart = this.BodyPart,
                GripPart = this.GripPart,
                BarrelPart = this.BarrelPart,
                SightPart = this.SightPart,
                StockPart = this.StockPart,
                ElementalPart = this.ElementalPart,
                Accessory1Part = this.Accessory1Part,
                Accessory2Part = this.Accessory2Part,
                MaterialPart = this.MaterialPart,
                PrefixPart = this.PrefixPart,
                TitlePart = this.TitlePart,
                GameStage = this.GameStage,
                UniqueId = this.UniqueId,
                AssetLibrarySetId = this.AssetLibrarySetId,
            };
        }
        #endregion

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyOfPropertyChange(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
