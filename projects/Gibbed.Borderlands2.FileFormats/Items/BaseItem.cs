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
    public class BaseItem : IPackableItem, INotifyPropertyChanged
    {
        public BaseItem()
        {
        }

        public BaseItem(BaseItem other)
        {
            this.Type = other.Type;
            this.Balance = other.Balance;
            this.Manufacturer = other.Manufacturer;
            this.ManufacturerGradeIndex = other.ManufacturerGradeIndex;
            this.AlphaPart = other.AlphaPart;
            this.BetaPart = other.BetaPart;
            this.GammaPart = other.GammaPart;
            this.DeltaPart = other.DeltaPart;
            this.EpsilonPart = other.EpsilonPart;
            this.ZetaPart = other.ZetaPart;
            this.EtaPart = other.EtaPart;
            this.ThetaPart = other.ThetaPart;
            this.MaterialPart = other.MaterialPart;
            this.PrefixPart = other.PrefixPart;
            this.TitlePart = other.TitlePart;
            this.GameStage = other.GameStage;
            this.UniqueId = other.UniqueId;
            this.AssetLibrarySetId = other.AssetLibrarySetId;
        }

        #region Fields
        private string _Type = "None";
        private string _Balance = "None";
        private string _Manufacturer = "None";
        private int _ManufacturerGradeIndex;
        private string _AlphaPart = "None";
        private string _BetaPart = "None";
        private string _GammaPart = "None";
        private string _DeltaPart = "None";
        private string _EpsilonPart = "None";
        private string _ZetaPart = "None";
        private string _EtaPart = "None";
        private string _ThetaPart = "None";
        private string _MaterialPart = "None";
        private string _PrefixPart = "None";
        private string _TitlePart = "None";
        private int _GameStage;
        private int _UniqueId;
        private int _AssetLibrarySetId;
        #endregion

        #region IPackable Members
        public void Unpack(PackedItem packed, Platform platform)
        {
            var alm = InfoManager.AssetLibraryManager;

            this.Type = alm.Lookup(packed.Type, platform, this.AssetLibrarySetId, AssetGroup.ItemTypes);
            this.Balance = alm.Lookup(packed.Balance, platform, this.AssetLibrarySetId, AssetGroup.BalanceDefs);
            this.Manufacturer =
                alm.Lookup(packed.Manufacturer, platform, this.AssetLibrarySetId, AssetGroup.Manufacturers);
            this.ManufacturerGradeIndex = packed.ManufacturerGradeIndex;
            this.GameStage = packed.GameStage;
            this.AlphaPart = alm.Lookup(packed.AlphaPart, platform, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.BetaPart = alm.Lookup(packed.BetaPart, platform, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.GammaPart = alm.Lookup(packed.GammaPart, platform, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.DeltaPart = alm.Lookup(packed.DeltaPart, platform, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.EpsilonPart = alm.Lookup(packed.EpsilonPart, platform, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.ZetaPart = alm.Lookup(packed.ZetaPart, platform, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.EtaPart = alm.Lookup(packed.EtaPart, platform, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.ThetaPart = alm.Lookup(packed.ThetaPart, platform, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.MaterialPart = alm.Lookup(packed.MaterialPart, platform, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.PrefixPart = alm.Lookup(packed.PrefixPart, platform, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.TitlePart = alm.Lookup(packed.TitlePart, platform, this.AssetLibrarySetId, AssetGroup.ItemParts);
        }

        public PackedItem Pack(Platform platform)
        {
            var alm = InfoManager.AssetLibraryManager;
            return new PackedItem
            {
                Type = alm.Lookup(this.Type, platform, this.AssetLibrarySetId, AssetGroup.ItemTypes),
                Balance = alm.Lookup(this.Balance, platform, this.AssetLibrarySetId, AssetGroup.BalanceDefs),
                Manufacturer = alm.Lookup(this.Manufacturer, platform, this.AssetLibrarySetId, AssetGroup.Manufacturers),
                ManufacturerGradeIndex = this.ManufacturerGradeIndex,
                GameStage = this.GameStage,
                AlphaPart = alm.Lookup(this.AlphaPart, platform, this.AssetLibrarySetId, AssetGroup.ItemParts),
                BetaPart = alm.Lookup(this.BetaPart, platform, this.AssetLibrarySetId, AssetGroup.ItemParts),
                GammaPart = alm.Lookup(this.GammaPart, platform, this.AssetLibrarySetId, AssetGroup.ItemParts),
                DeltaPart = alm.Lookup(this.DeltaPart, platform, this.AssetLibrarySetId, AssetGroup.ItemParts),
                EpsilonPart = alm.Lookup(this.EpsilonPart, platform, this.AssetLibrarySetId, AssetGroup.ItemParts),
                ZetaPart = alm.Lookup(this.ZetaPart, platform, this.AssetLibrarySetId, AssetGroup.ItemParts),
                EtaPart = alm.Lookup(this.EtaPart, platform, this.AssetLibrarySetId, AssetGroup.ItemParts),
                ThetaPart = alm.Lookup(this.ThetaPart, platform, this.AssetLibrarySetId, AssetGroup.ItemParts),
                MaterialPart = alm.Lookup(this.MaterialPart, platform, this.AssetLibrarySetId, AssetGroup.ItemParts),
                PrefixPart = alm.Lookup(this.PrefixPart, platform, this.AssetLibrarySetId, AssetGroup.ItemParts),
                TitlePart = alm.Lookup(this.TitlePart, platform, this.AssetLibrarySetId, AssetGroup.ItemParts),
            };
        }
        #endregion

        #region Properties
        public string Type
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

        public string Balance
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

        public string Manufacturer
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

        public string AlphaPart
        {
            get { return this._AlphaPart; }
            set
            {
                if (value != this._AlphaPart)
                {
                    this._AlphaPart = value;
                    this.NotifyPropertyChanged("AlphaPart");
                }
            }
        }

        public string BetaPart
        {
            get { return this._BetaPart; }
            set
            {
                if (value != this._BetaPart)
                {
                    this._BetaPart = value;
                    this.NotifyPropertyChanged("BetaPart");
                }
            }
        }

        public string GammaPart
        {
            get { return this._GammaPart; }
            set
            {
                if (value != this._GammaPart)
                {
                    this._GammaPart = value;
                    this.NotifyPropertyChanged("GammaPart");
                }
            }
        }

        public string DeltaPart
        {
            get { return this._DeltaPart; }
            set
            {
                if (value != this._DeltaPart)
                {
                    this._DeltaPart = value;
                    this.NotifyPropertyChanged("DeltaPart");
                }
            }
        }

        public string EpsilonPart
        {
            get { return this._EpsilonPart; }
            set
            {
                if (value != this._EpsilonPart)
                {
                    this._EpsilonPart = value;
                    this.NotifyPropertyChanged("EpsilonPart");
                }
            }
        }

        public string ZetaPart
        {
            get { return this._ZetaPart; }
            set
            {
                if (value != this._ZetaPart)
                {
                    this._ZetaPart = value;
                    this.NotifyPropertyChanged("ZetaPart");
                }
            }
        }

        public string EtaPart
        {
            get { return this._EtaPart; }
            set
            {
                if (value != this._EtaPart)
                {
                    this._EtaPart = value;
                    this.NotifyPropertyChanged("EtaPart");
                }
            }
        }

        public string ThetaPart
        {
            get { return this._ThetaPart; }
            set
            {
                if (value != this._ThetaPart)
                {
                    this._ThetaPart = value;
                    this.NotifyPropertyChanged("ThetaPart");
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
                    this.NotifyPropertyChanged("MaterialPart");
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
                    this.NotifyPropertyChanged("PrefixPart");
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
            return new BaseItem()
            {
                Type = this.Type,
                Balance = this.Balance,
                Manufacturer = this.Manufacturer,
                ManufacturerGradeIndex = this.ManufacturerGradeIndex,
                AlphaPart = this.AlphaPart,
                BetaPart = this.BetaPart,
                GammaPart = this.GammaPart,
                DeltaPart = this.DeltaPart,
                EpsilonPart = this.EpsilonPart,
                ZetaPart = this.ZetaPart,
                EtaPart = this.EtaPart,
                ThetaPart = this.ThetaPart,
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
