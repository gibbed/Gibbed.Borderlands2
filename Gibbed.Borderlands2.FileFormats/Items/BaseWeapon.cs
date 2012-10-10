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

using System.ComponentModel;
using Gibbed.Borderlands2.GameInfo;

namespace Gibbed.Borderlands2.FileFormats.Items
{
    public class BaseWeapon : IPackable, INotifyPropertyChanged
    {
        #region Fields
        private string _TypeDefinition = "None";
        private string _BalanceDefinition = "None";
        private string _ManufacturerDefinition = "None";
        private int _ManufacturerGradeIndex;
        private string _BodyPartDefinition = "None";
        private string _GripPartDefinition = "None";
        private string _BarrelPartDefinition = "None";
        private string _SightPartDefinition = "None";
        private string _StockPartDefinition = "None";
        private string _ElementalPartDefinition = "None";
        private string _Accessory1PartDefinition = "None";
        private string _Accessory2PartDefinition = "None";
        private string _MaterialPartDefinition = "None";
        private string _PrefixPartDefinition = "None";
        private string _TitlePartDefinition = "None";
        private int _GameStage;
        private int _UniqueId;
        private int _AssetLibrarySetId;
        #endregion

        #region IPackable Members
        public void Read(BitReader reader)
        {
            var alm = InfoManager.AssetLibraryManager;

            this.TypeDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.WeaponTypes);
            this.BalanceDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.Balances);
            this.ManufacturerDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.Manufacturers);
            this.ManufacturerGradeIndex = reader.ReadInt32(7);
            this.GameStage = reader.ReadInt32(7);
            this.BodyPartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.WeaponParts);
            this.GripPartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.WeaponParts);
            this.BarrelPartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.WeaponParts);
            this.SightPartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.WeaponParts);
            this.StockPartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.WeaponParts);
            this.ElementalPartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.WeaponParts);
            this.Accessory1PartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.WeaponParts);
            this.Accessory2PartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.WeaponParts);
            this.MaterialPartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.WeaponParts);
            this.PrefixPartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.WeaponParts);
            this.TitlePartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.WeaponParts);
        }

        public void Write(BitWriter writer)
        {
            var alm = InfoManager.AssetLibraryManager;

            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.WeaponTypes, this.TypeDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.Balances, this.BalanceDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.Manufacturers, this.ManufacturerDefinition);
            writer.WriteInt32(this.ManufacturerGradeIndex, 7);
            writer.WriteInt32(this.GameStage, 7);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.WeaponParts, this.BodyPartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.WeaponParts, this.GripPartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.WeaponParts, this.BarrelPartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.WeaponParts, this.SightPartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.WeaponParts, this.StockPartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.WeaponParts, this.ElementalPartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.WeaponParts, this.Accessory1PartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.WeaponParts, this.Accessory2PartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.WeaponParts, this.MaterialPartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.WeaponParts, this.PrefixPartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.WeaponParts, this.TitlePartDefinition);
        }
        #endregion

        #region Properties
        public string TypeDefinition
        {
            get { return this._TypeDefinition; }
            set
            {
                if (value != this._TypeDefinition)
                {
                    this._TypeDefinition = value;
                    this.NotifyPropertyChanged("TypeDefinition");
                }
            }
        }

        public string BalanceDefinition
        {
            get { return this._BalanceDefinition; }
            set
            {
                if (value != this._BalanceDefinition)
                {
                    this._BalanceDefinition = value;
                    this.NotifyPropertyChanged("BalanceDefinition");
                }
            }
        }

        public string ManufacturerDefinition
        {
            get { return this._ManufacturerDefinition; }
            set
            {
                if (value != this._ManufacturerDefinition)
                {
                    this._ManufacturerDefinition = value;
                    this.NotifyPropertyChanged("ManufacturerDefinition");
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

        public string BodyPartDefinition
        {
            get { return this._BodyPartDefinition; }
            set
            {
                if (value != this._BodyPartDefinition)
                {
                    this._BodyPartDefinition = value;
                    this.NotifyPropertyChanged("BodyPartDefinition");
                }
            }
        }

        public string GripPartDefinition
        {
            get { return this._GripPartDefinition; }
            set
            {
                if (value != this._GripPartDefinition)
                {
                    this._GripPartDefinition = value;
                    this.NotifyPropertyChanged("GripPartDefinition");
                }
            }
        }

        public string BarrelPartDefinition
        {
            get { return this._BarrelPartDefinition; }
            set
            {
                if (value != this._BarrelPartDefinition)
                {
                    this._BarrelPartDefinition = value;
                    this.NotifyPropertyChanged("BarrelPartDefinition");
                }
            }
        }

        public string SightPartDefinition
        {
            get { return this._SightPartDefinition; }
            set
            {
                if (value != this._SightPartDefinition)
                {
                    this._SightPartDefinition = value;
                    this.NotifyPropertyChanged("SightPartDefinition");
                }
            }
        }

        public string StockPartDefinition
        {
            get { return this._StockPartDefinition; }
            set
            {
                if (value != this._StockPartDefinition)
                {
                    this._StockPartDefinition = value;
                    this.NotifyPropertyChanged("StockPartDefinition");
                }
            }
        }

        public string ElementalPartDefinition
        {
            get { return this._ElementalPartDefinition; }
            set
            {
                if (value != this._ElementalPartDefinition)
                {
                    this._ElementalPartDefinition = value;
                    this.NotifyPropertyChanged("ElementalPartDefinition");
                }
            }
        }

        public string Accessory1PartDefinition
        {
            get { return this._Accessory1PartDefinition; }
            set
            {
                if (value != this._Accessory1PartDefinition)
                {
                    this._Accessory1PartDefinition = value;
                    this.NotifyPropertyChanged("Accessory1PartDefinition");
                }
            }
        }

        public string Accessory2PartDefinition
        {
            get { return this._Accessory2PartDefinition; }
            set
            {
                if (value != this._Accessory2PartDefinition)
                {
                    this._Accessory2PartDefinition = value;
                    this.NotifyPropertyChanged("Accessory2PartDefinition");
                }
            }
        }

        public string MaterialPartDefinition
        {
            get { return this._MaterialPartDefinition; }
            set
            {
                if (value != this._MaterialPartDefinition)
                {
                    this._MaterialPartDefinition = value;
                    this.NotifyPropertyChanged("MaterialPartDefinition");
                }
            }
        }

        public string PrefixPartDefinition
        {
            get { return this._PrefixPartDefinition; }
            set
            {
                if (value != this._PrefixPartDefinition)
                {
                    this._PrefixPartDefinition = value;
                    this.NotifyPropertyChanged("PrefixPartDefinition");
                }
            }
        }

        public string TitlePartDefinition
        {
            get { return this._TitlePartDefinition; }
            set
            {
                if (value != this._TitlePartDefinition)
                {
                    this._TitlePartDefinition = value;
                    this.NotifyPropertyChanged("TitlePartDefinition");
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

        public int UniqueId
        {
            get { return this._UniqueId; }
            set
            {
                if (value != this._UniqueId)
                {
                    this._UniqueId = value;
                    this.NotifyPropertyChanged("UniqueId");
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
                    this.NotifyPropertyChanged("AssetLibrarySetId");
                }
            }
        }
        #endregion

        #region ICloneable Members
        public virtual object Clone()
        {
            return new BaseWeapon()
            {
                TypeDefinition = this.TypeDefinition,
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
