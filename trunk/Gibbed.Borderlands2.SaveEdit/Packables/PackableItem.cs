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

using Caliburn.Micro;
using Gibbed.Borderlands2.FileFormats;
using Gibbed.Borderlands2.GameInfo;

namespace Gibbed.Borderlands2.SaveEdit.Packables
{
    internal class PackableItem : PropertyChangedBase, IPackable
    {
        #region Fields
        private string _ItemDefinition = "None";
        private string _BalanceDefinition = "None";
        private string _ManufacturerDefinition = "None";
        private int _ManufacturerGradeIndex;
        private string _AlphaItemPartDefinition = "None";
        private string _BetaItemPartDefinition = "None";
        private string _GammaItemPartDefinition = "None";
        private string _DeltaItemPartDefinition = "None";
        private string _EpsilonItemPartDefinition = "None";
        private string _ZetaItemPartDefinition = "None";
        private string _EtaItemPartDefinition = "None";
        private string _ThetaItemPartDefinition = "None";
        private string _MaterialItemPartDefinition = "None";
        private string _PrefixItemNamePartDefinition = "None";
        private string _TitleItemNamePartDefinition = "None";
        private int _GameStage;
        private int _UniqueId;
        private int _AssetLibrarySetId;
        #endregion

        public void Read(BitReader reader)
        {
            var alm = InfoManager.AssetLibraryManager;

            this.ItemDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemTypes);
            this.BalanceDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.Balances);
            this.ManufacturerDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.Manufacturers);
            this.ManufacturerGradeIndex = reader.ReadInt32(7);
            this.GameStage = reader.ReadInt32(7);
            this.AlphaItemPartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.BetaItemPartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.GammaItemPartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.DeltaItemPartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.EpsilonItemPartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.ZetaItemPartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.EtaItemPartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.ThetaItemPartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.MaterialItemPartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.PrefixItemNamePartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.TitleItemNamePartDefinition = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
        }

        public void Write(BitWriter writer)
        {
            var alm = InfoManager.AssetLibraryManager;

            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemTypes, this.ItemDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.Balances, this.BalanceDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.Manufacturers, this.ManufacturerDefinition);
            writer.WriteInt32(this.ManufacturerGradeIndex, 7);
            writer.WriteInt32(this.GameStage, 7);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.AlphaItemPartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.BetaItemPartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.GammaItemPartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.DeltaItemPartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.EpsilonItemPartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.ZetaItemPartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.EtaItemPartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.ThetaItemPartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.MaterialItemPartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.PrefixItemNamePartDefinition);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.TitleItemNamePartDefinition);
        }

        #region Properties
        public string ItemDefinition
        {
            get { return this._ItemDefinition; }
            set
            {
                if (value != this._ItemDefinition)
                {
                    this._ItemDefinition = value;
                    this.NotifyOfPropertyChange(() => this.ItemDefinition);
                    this.NotifyOfPropertyChange(() => this.DisplayGroup);
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
                    this.NotifyOfPropertyChange(() => this.BalanceDefinition);
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
                    this.NotifyOfPropertyChange(() => this.ManufacturerDefinition);
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
                    this.NotifyOfPropertyChange(() => this.ManufacturerGradeIndex);
                }
            }
        }

        public string AlphaItemPartDefinition
        {
            get { return this._AlphaItemPartDefinition; }
            set
            {
                if (value != this._AlphaItemPartDefinition)
                {
                    this._AlphaItemPartDefinition = value;
                    this.NotifyOfPropertyChange(() => this.AlphaItemPartDefinition);
                }
            }
        }

        public string BetaItemPartDefinition
        {
            get { return this._BetaItemPartDefinition; }
            set
            {
                if (value != this._BetaItemPartDefinition)
                {
                    this._BetaItemPartDefinition = value;
                    this.NotifyOfPropertyChange(() => this.BetaItemPartDefinition);
                }
            }
        }

        public string GammaItemPartDefinition
        {
            get { return this._GammaItemPartDefinition; }
            set
            {
                if (value != this._GammaItemPartDefinition)
                {
                    this._GammaItemPartDefinition = value;
                    this.NotifyOfPropertyChange(() => this.GammaItemPartDefinition);
                }
            }
        }

        public string DeltaItemPartDefinition
        {
            get { return this._DeltaItemPartDefinition; }
            set
            {
                if (value != this._DeltaItemPartDefinition)
                {
                    this._DeltaItemPartDefinition = value;
                    this.NotifyOfPropertyChange(() => this.DeltaItemPartDefinition);
                }
            }
        }

        public string EpsilonItemPartDefinition
        {
            get { return this._EpsilonItemPartDefinition; }
            set
            {
                if (value != this._EpsilonItemPartDefinition)
                {
                    this._EpsilonItemPartDefinition = value;
                    this.NotifyOfPropertyChange(() => this.EpsilonItemPartDefinition);
                }
            }
        }

        public string ZetaItemPartDefinition
        {
            get { return this._ZetaItemPartDefinition; }
            set
            {
                if (value != this._ZetaItemPartDefinition)
                {
                    this._ZetaItemPartDefinition = value;
                    this.NotifyOfPropertyChange(() => this.ZetaItemPartDefinition);
                }
            }
        }

        public string EtaItemPartDefinition
        {
            get { return this._EtaItemPartDefinition; }
            set
            {
                if (value != this._EtaItemPartDefinition)
                {
                    this._EtaItemPartDefinition = value;
                    this.NotifyOfPropertyChange(() => this.EtaItemPartDefinition);
                }
            }
        }

        public string ThetaItemPartDefinition
        {
            get { return this._ThetaItemPartDefinition; }
            set
            {
                if (value != this._ThetaItemPartDefinition)
                {
                    this._ThetaItemPartDefinition = value;
                    this.NotifyOfPropertyChange(() => this.ThetaItemPartDefinition);
                }
            }
        }

        public string MaterialItemPartDefinition
        {
            get { return this._MaterialItemPartDefinition; }
            set
            {
                if (value != this._MaterialItemPartDefinition)
                {
                    this._MaterialItemPartDefinition = value;
                    this.NotifyOfPropertyChange(() => this.MaterialItemPartDefinition);
                }
            }
        }

        public string PrefixItemNamePartDefinition
        {
            get { return this._PrefixItemNamePartDefinition; }
            set
            {
                if (value != this._PrefixItemNamePartDefinition)
                {
                    this._PrefixItemNamePartDefinition = value;
                    this.NotifyOfPropertyChange(() => this.PrefixItemNamePartDefinition);
                }
            }
        }

        public string TitleItemNamePartDefinition
        {
            get { return this._TitleItemNamePartDefinition; }
            set
            {
                if (value != this._TitleItemNamePartDefinition)
                {
                    this._TitleItemNamePartDefinition = value;
                    this.NotifyOfPropertyChange(() => this.TitleItemNamePartDefinition);
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
                    this.NotifyOfPropertyChange(() => this.GameStage);
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
                    this.NotifyOfPropertyChange(() => this.UniqueId);
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
                    this.NotifyOfPropertyChange(() => this.AssetLibrarySetId);
                }
            }
        }
        #endregion

        #region ICloneable Members
        public virtual object Clone()
        {
            return new PackableItem()
            {
                ItemDefinition = this.ItemDefinition,
                BalanceDefinition = this.BalanceDefinition,
                ManufacturerDefinition = this.ManufacturerDefinition,
                ManufacturerGradeIndex = this.ManufacturerGradeIndex,
                AlphaItemPartDefinition = this.AlphaItemPartDefinition,
                BetaItemPartDefinition = this.BetaItemPartDefinition,
                GammaItemPartDefinition = this.GammaItemPartDefinition,
                DeltaItemPartDefinition = this.DeltaItemPartDefinition,
                EpsilonItemPartDefinition = this.EpsilonItemPartDefinition,
                ZetaItemPartDefinition = this.ZetaItemPartDefinition,
                EtaItemPartDefinition = this.EtaItemPartDefinition,
                ThetaItemPartDefinition = this.ThetaItemPartDefinition,
                MaterialItemPartDefinition = this.MaterialItemPartDefinition,
                PrefixItemNamePartDefinition = this.PrefixItemNamePartDefinition,
                TitleItemNamePartDefinition = this.TitleItemNamePartDefinition,
                GameStage = this.GameStage,
                UniqueId = this.UniqueId,
                AssetLibrarySetId = this.AssetLibrarySetId,
            };
        }
        #endregion

        public virtual string DisplayGroup
        {
            get
            {
                if (InfoManager.ItemTypes.ContainsKey(this.ItemDefinition) == false)
                {
                    return "Unknown";
                }

                switch (InfoManager.ItemTypes[this.ItemDefinition])
                {
                    case ItemType.Artifact:
                    {
                        return "Relic";
                    }

                    case ItemType.ClassMod:
                    {
                        return "Class Mod";
                    }

                    case ItemType.GrenadeMod:
                    {
                        return "Grenade Mod";
                    }

                    case ItemType.Shield:
                    {
                        return "Shield";
                    }

                    case ItemType.UsableCustomizationItem:
                    {
                        return "Customization";
                    }

                    case ItemType.UsableItem:
                    {
                        return "Personal";
                    }
                }

                return "Unknown";
            }
        }
    }
}
