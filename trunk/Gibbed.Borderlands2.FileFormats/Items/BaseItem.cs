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
    public class BaseItem : IBaseSlot, IPackable, INotifyPropertyChanged
    {
        #region Fields
        private string _TypeDefinition = "None";
        private string _BalanceDefinition = "None";
        private string _ManufacturerDefinition = "None";
        private int _ManufacturerGradeIndex;
        private string _AlphaPartDefinition = "None";
        private string _BetaPartDefinition = "None";
        private string _GammaPartDefinition = "None";
        private string _DeltaPartDefinition = "None";
        private string _EpsilonPartDefinition = "None";
        private string _ZetaPartDefinition = "None";
        private string _EtaPartDefinition = "None";
        private string _ThetaPartDefinition = "None";
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

            this.TypeDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemTypes);
            this.BalanceDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.BalanceDefs);
            this.ManufacturerDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.Manufacturers);
            this.ManufacturerGradeIndex = reader.ReadInt32(7);
            this.GameStage = reader.ReadInt32(7);
            this.AlphaPartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.BetaPartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.GammaPartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.DeltaPartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.EpsilonPartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.ZetaPartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.EtaPartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.ThetaPartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.MaterialPartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.PrefixPartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.TitlePartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
        }

        public void Write(BitWriter writer)
        {
            var alm = InfoManager.AssetLibraryManager;

            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemTypes, this.TypeDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.BalanceDefs, this.BalanceDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.Manufacturers, this.ManufacturerDefinition);
            writer.WriteInt32(this.ManufacturerGradeIndex, 7);
            writer.WriteInt32(this.GameStage, 7);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.AlphaPartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.BetaPartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.GammaPartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.DeltaPartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.EpsilonPartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.ZetaPartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.EtaPartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.ThetaPartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.MaterialPartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.PrefixPartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.TitlePartDefinition);
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
                    this.NotifyPropertyChanged("Type");
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
                    this.NotifyPropertyChanged("Balance");
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

        public string AlphaPartDefinition
        {
            get { return this._AlphaPartDefinition; }
            set
            {
                if (value != this._AlphaPartDefinition)
                {
                    this._AlphaPartDefinition = value;
                    this.NotifyPropertyChanged("AlphaPartDefinition");
                }
            }
        }

        public string BetaPartDefinition
        {
            get { return this._BetaPartDefinition; }
            set
            {
                if (value != this._BetaPartDefinition)
                {
                    this._BetaPartDefinition = value;
                    this.NotifyPropertyChanged("BetaPartDefinition");
                }
            }
        }

        public string GammaPartDefinition
        {
            get { return this._GammaPartDefinition; }
            set
            {
                if (value != this._GammaPartDefinition)
                {
                    this._GammaPartDefinition = value;
                    this.NotifyPropertyChanged("GammaPartDefinition");
                }
            }
        }

        public string DeltaPartDefinition
        {
            get { return this._DeltaPartDefinition; }
            set
            {
                if (value != this._DeltaPartDefinition)
                {
                    this._DeltaPartDefinition = value;
                    this.NotifyPropertyChanged("DeltaPartDefinition");
                }
            }
        }

        public string EpsilonPartDefinition
        {
            get { return this._EpsilonPartDefinition; }
            set
            {
                if (value != this._EpsilonPartDefinition)
                {
                    this._EpsilonPartDefinition = value;
                    this.NotifyPropertyChanged("EpsilonPartDefinition");
                }
            }
        }

        public string ZetaPartDefinition
        {
            get { return this._ZetaPartDefinition; }
            set
            {
                if (value != this._ZetaPartDefinition)
                {
                    this._ZetaPartDefinition = value;
                    this.NotifyPropertyChanged("ZetaPartDefinition");
                }
            }
        }

        public string EtaPartDefinition
        {
            get { return this._EtaPartDefinition; }
            set
            {
                if (value != this._EtaPartDefinition)
                {
                    this._EtaPartDefinition = value;
                    this.NotifyPropertyChanged("EtaPartDefinition");
                }
            }
        }

        public string ThetaPartDefinition
        {
            get { return this._ThetaPartDefinition; }
            set
            {
                if (value != this._ThetaPartDefinition)
                {
                    this._ThetaPartDefinition = value;
                    this.NotifyPropertyChanged("ThetaPartDefinition");
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
            return new BaseItem()
            {
                TypeDefinition = this.TypeDefinition,
                BalanceDefinition = this.BalanceDefinition,
                ManufacturerDefinition = this.ManufacturerDefinition,
                ManufacturerGradeIndex = this.ManufacturerGradeIndex,
                AlphaPartDefinition = this.AlphaPartDefinition,
                BetaPartDefinition = this.BetaPartDefinition,
                GammaPartDefinition = this.GammaPartDefinition,
                DeltaPartDefinition = this.DeltaPartDefinition,
                EpsilonPartDefinition = this.EpsilonPartDefinition,
                ZetaPartDefinition = this.ZetaPartDefinition,
                EtaPartDefinition = this.EtaPartDefinition,
                ThetaPartDefinition = this.ThetaPartDefinition,
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
