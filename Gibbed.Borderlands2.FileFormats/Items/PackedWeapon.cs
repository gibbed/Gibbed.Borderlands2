/* Copyright (c) 2015 Rick (rick 'at' gibbed 'dot' us)
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
    public class PackedWeapon : IPackedSlot
    {
        #region Fields
        private PackedAssetReference _Type = PackedAssetReference.None;
        private PackedAssetReference _Balance = PackedAssetReference.None;
        private PackedAssetReference _Manufacturer = PackedAssetReference.None;
        private int _ManufacturerGradeIndex;
        private PackedAssetReference _BodyPart = PackedAssetReference.None;
        private PackedAssetReference _GripPart = PackedAssetReference.None;
        private PackedAssetReference _BarrelPart = PackedAssetReference.None;
        private PackedAssetReference _SightPart = PackedAssetReference.None;
        private PackedAssetReference _StockPart = PackedAssetReference.None;
        private PackedAssetReference _ElementalPart = PackedAssetReference.None;
        private PackedAssetReference _Accessory1Part = PackedAssetReference.None;
        private PackedAssetReference _Accessory2Part = PackedAssetReference.None;
        private PackedAssetReference _MaterialPart = PackedAssetReference.None;
        private PackedAssetReference _PrefixPart = PackedAssetReference.None;
        private PackedAssetReference _TitlePart = PackedAssetReference.None;
        private int _GameStage;
        #endregion

        public void Read(BitReader reader, Platform platform)
        {
            var alm = InfoManager.AssetLibraryManager;

            this.Type = alm.Decode(reader, platform, AssetGroup.WeaponTypes);
            this.Balance = alm.Decode(reader, platform, AssetGroup.BalanceDefs);
            this.Manufacturer = alm.Decode(reader, platform, AssetGroup.Manufacturers);
            this.ManufacturerGradeIndex = reader.ReadInt32(7);
            this.GameStage = reader.ReadInt32(7);
            this.BodyPart = alm.Decode(reader, platform, AssetGroup.WeaponParts);
            this.GripPart = alm.Decode(reader, platform, AssetGroup.WeaponParts);
            this.BarrelPart = alm.Decode(reader, platform, AssetGroup.WeaponParts);
            this.SightPart = alm.Decode(reader, platform, AssetGroup.WeaponParts);
            this.StockPart = alm.Decode(reader, platform, AssetGroup.WeaponParts);
            this.ElementalPart = alm.Decode(reader, platform, AssetGroup.WeaponParts);
            this.Accessory1Part = alm.Decode(reader, platform, AssetGroup.WeaponParts);
            this.Accessory2Part = alm.Decode(reader, platform, AssetGroup.WeaponParts);
            this.MaterialPart = alm.Decode(reader, platform, AssetGroup.WeaponParts);
            this.PrefixPart = alm.Decode(reader, platform, AssetGroup.WeaponParts);
            this.TitlePart = alm.Decode(reader, platform, AssetGroup.WeaponParts);
        }

        public void Write(BitWriter writer, Platform platform)
        {
            var alm = InfoManager.AssetLibraryManager;

            alm.Encode(writer, platform, AssetGroup.WeaponTypes, this.Type);
            alm.Encode(writer, platform, AssetGroup.BalanceDefs, this.Balance);
            alm.Encode(writer, platform, AssetGroup.Manufacturers, this.Manufacturer);
            writer.WriteInt32(this.ManufacturerGradeIndex, 7);
            writer.WriteInt32(this.GameStage, 7);
            alm.Encode(writer, platform, AssetGroup.WeaponParts, this.BodyPart);
            alm.Encode(writer, platform, AssetGroup.WeaponParts, this.GripPart);
            alm.Encode(writer, platform, AssetGroup.WeaponParts, this.BarrelPart);
            alm.Encode(writer, platform, AssetGroup.WeaponParts, this.SightPart);
            alm.Encode(writer, platform, AssetGroup.WeaponParts, this.StockPart);
            alm.Encode(writer, platform, AssetGroup.WeaponParts, this.ElementalPart);
            alm.Encode(writer, platform, AssetGroup.WeaponParts, this.Accessory1Part);
            alm.Encode(writer, platform, AssetGroup.WeaponParts, this.Accessory2Part);
            alm.Encode(writer, platform, AssetGroup.WeaponParts, this.MaterialPart);
            alm.Encode(writer, platform, AssetGroup.WeaponParts, this.PrefixPart);
            alm.Encode(writer, platform, AssetGroup.WeaponParts, this.TitlePart);
        }

        #region Properties
        public PackedAssetReference Type
        {
            get { return this._Type; }
            set
            {
                if (value != this._Type)
                {
                    this._Type = value;
                    this.NotifyPropertyChanged("Type");
                }
            }
        }

        public PackedAssetReference Balance
        {
            get { return this._Balance; }
            set
            {
                if (value != this._Balance)
                {
                    this._Balance = value;
                    this.NotifyPropertyChanged("Balance");
                }
            }
        }

        public PackedAssetReference Manufacturer
        {
            get { return this._Manufacturer; }
            set
            {
                if (value != this._Manufacturer)
                {
                    this._Manufacturer = value;
                    this.NotifyPropertyChanged("Manufacturer");
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
                    this.NotifyPropertyChanged("ManufacturerGradeIndex");
                }
            }
        }

        public PackedAssetReference BodyPart
        {
            get { return this._BodyPart; }
            set
            {
                if (value != this._BodyPart)
                {
                    this._BodyPart = value;
                    this.NotifyPropertyChanged("BodyPart");
                }
            }
        }

        public PackedAssetReference GripPart
        {
            get { return this._GripPart; }
            set
            {
                if (value != this._GripPart)
                {
                    this._GripPart = value;
                    this.NotifyPropertyChanged("GripPart");
                }
            }
        }

        public PackedAssetReference BarrelPart
        {
            get { return this._BarrelPart; }
            set
            {
                if (value != this._BarrelPart)
                {
                    this._BarrelPart = value;
                    this.NotifyPropertyChanged("BarrelPart");
                }
            }
        }

        public PackedAssetReference SightPart
        {
            get { return this._SightPart; }
            set
            {
                if (value != this._SightPart)
                {
                    this._SightPart = value;
                    this.NotifyPropertyChanged("SightPart");
                }
            }
        }

        public PackedAssetReference StockPart
        {
            get { return this._StockPart; }
            set
            {
                if (value != this._StockPart)
                {
                    this._StockPart = value;
                    this.NotifyPropertyChanged("StockPart");
                }
            }
        }

        public PackedAssetReference ElementalPart
        {
            get { return this._ElementalPart; }
            set
            {
                if (value != this._ElementalPart)
                {
                    this._ElementalPart = value;
                    this.NotifyPropertyChanged("ElementalPart");
                }
            }
        }

        public PackedAssetReference Accessory1Part
        {
            get { return this._Accessory1Part; }
            set
            {
                if (value != this._Accessory1Part)
                {
                    this._Accessory1Part = value;
                    this.NotifyPropertyChanged("Accessory1Part");
                }
            }
        }

        public PackedAssetReference Accessory2Part
        {
            get { return this._Accessory2Part; }
            set
            {
                if (value != this._Accessory2Part)
                {
                    this._Accessory2Part = value;
                    this.NotifyPropertyChanged("Accessory2Part");
                }
            }
        }

        public PackedAssetReference MaterialPart
        {
            get { return this._MaterialPart; }
            set
            {
                if (value != this._MaterialPart)
                {
                    this._MaterialPart = value;
                    this.NotifyPropertyChanged("MaterialPart");
                }
            }
        }

        public PackedAssetReference PrefixPart
        {
            get { return this._PrefixPart; }
            set
            {
                if (value != this._PrefixPart)
                {
                    this._PrefixPart = value;
                    this.NotifyPropertyChanged("PrefixPart");
                }
            }
        }

        public PackedAssetReference TitlePart
        {
            get { return this._TitlePart; }
            set
            {
                if (value != this._TitlePart)
                {
                    this._TitlePart = value;
                    this.NotifyPropertyChanged("TitlePart");
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
                    this.NotifyPropertyChanged("GameStage");
                }
            }
        }
        #endregion

        #region ICloneable Members
        public virtual object Clone()
        {
            return new PackedWeapon()
            {
                Type = this.Type,
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
            };
        }
        #endregion

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
