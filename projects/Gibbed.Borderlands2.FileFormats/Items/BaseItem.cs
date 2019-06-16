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
            var m = InfoManager.AssetLibraryManager;
            var si = this.AssetLibrarySetId;
            this.Type = m.Lookup(packed.Type, platform, si, AssetGroup.ItemTypes);
            this.Balance = m.Lookup(packed.Balance, platform, si, AssetGroup.BalanceDefs);
            this.Manufacturer = m.Lookup(packed.Manufacturer, platform, si, AssetGroup.Manufacturers);
            this.ManufacturerGradeIndex = packed.ManufacturerGradeIndex;
            this.GameStage = packed.GameStage;
            this.AlphaPart = m.Lookup(packed.AlphaPart, platform, si, AssetGroup.ItemParts);
            this.BetaPart = m.Lookup(packed.BetaPart, platform, si, AssetGroup.ItemParts);
            this.GammaPart = m.Lookup(packed.GammaPart, platform, si, AssetGroup.ItemParts);
            this.DeltaPart = m.Lookup(packed.DeltaPart, platform, si, AssetGroup.ItemParts);
            this.EpsilonPart = m.Lookup(packed.EpsilonPart, platform, si, AssetGroup.ItemParts);
            this.ZetaPart = m.Lookup(packed.ZetaPart, platform, si, AssetGroup.ItemParts);
            this.EtaPart = m.Lookup(packed.EtaPart, platform, si, AssetGroup.ItemParts);
            this.ThetaPart = m.Lookup(packed.ThetaPart, platform, si, AssetGroup.ItemParts);
            this.MaterialPart = m.Lookup(packed.MaterialPart, platform, si, AssetGroup.ItemParts);
            this.PrefixPart = m.Lookup(packed.PrefixPart, platform, si, AssetGroup.ItemParts);
            this.TitlePart = m.Lookup(packed.TitlePart, platform, si, AssetGroup.ItemParts);
        }

        public PackedItem Pack(Platform platform)
        {
            var m = InfoManager.AssetLibraryManager;
            var si = this.AssetLibrarySetId;
            return new PackedItem
            {
                Type = m.Lookup(this.Type, platform, si, AssetGroup.ItemTypes),
                Balance = m.Lookup(this.Balance, platform, si, AssetGroup.BalanceDefs),
                Manufacturer = m.Lookup(this.Manufacturer, platform, si, AssetGroup.Manufacturers),
                ManufacturerGradeIndex = this.ManufacturerGradeIndex,
                GameStage = this.GameStage,
                AlphaPart = m.Lookup(this.AlphaPart, platform, si, AssetGroup.ItemParts),
                BetaPart = m.Lookup(this.BetaPart, platform, si, AssetGroup.ItemParts),
                GammaPart = m.Lookup(this.GammaPart, platform, si, AssetGroup.ItemParts),
                DeltaPart = m.Lookup(this.DeltaPart, platform, si, AssetGroup.ItemParts),
                EpsilonPart = m.Lookup(this.EpsilonPart, platform, si, AssetGroup.ItemParts),
                ZetaPart = m.Lookup(this.ZetaPart, platform, si, AssetGroup.ItemParts),
                EtaPart = m.Lookup(this.EtaPart, platform, si, AssetGroup.ItemParts),
                ThetaPart = m.Lookup(this.ThetaPart, platform, si, AssetGroup.ItemParts),
                MaterialPart = m.Lookup(this.MaterialPart, platform, si, AssetGroup.ItemParts),
                PrefixPart = m.Lookup(this.PrefixPart, platform, si, AssetGroup.ItemParts),
                TitlePart = m.Lookup(this.TitlePart, platform, si, AssetGroup.ItemParts),
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
                    this.NotifyOfPropertyChange(nameof(Type));
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

        public string AlphaPart
        {
            get { return this._AlphaPart; }
            set
            {
                if (value != this._AlphaPart)
                {
                    this._AlphaPart = value;
                    this.NotifyOfPropertyChange(nameof(AlphaPart));
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
                    this.NotifyOfPropertyChange(nameof(BetaPart));
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
                    this.NotifyOfPropertyChange(nameof(GammaPart));
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
                    this.NotifyOfPropertyChange(nameof(DeltaPart));
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
                    this.NotifyOfPropertyChange(nameof(EpsilonPart));
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
                    this.NotifyOfPropertyChange(nameof(ZetaPart));
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
                    this.NotifyOfPropertyChange(nameof(EtaPart));
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
                    this.NotifyOfPropertyChange(nameof(ThetaPart));
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

        protected void NotifyOfPropertyChange(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
