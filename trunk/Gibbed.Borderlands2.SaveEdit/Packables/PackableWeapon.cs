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
    internal class PackableWeapon : PropertyChangedBase, IPackable
    {
        #region Fields
        private string _WeaponTypeDefinition = "None";
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

        public void Read(BitReader reader)
        {
            var set = InfoManager.AssetLibraryManager.GetSet(this.AssetLibrarySetId);

            this.WeaponTypeDefinition = set.WeaponTypes.Decode(reader);
            this.BalanceDefinition = set.BalanceDefs.Decode(reader);
            this.ManufacturerDefinition = set.Manufacturers.Decode(reader);
            this.ManufacturerGradeIndex = reader.ReadInt32(7);
            this.GameStage = reader.ReadInt32(7);
            this.BodyPartDefinition = set.WeaponParts.Decode(reader);
            this.GripPartDefinition = set.WeaponParts.Decode(reader);
            this.BarrelPartDefinition = set.WeaponParts.Decode(reader);
            this.SightPartDefinition = set.WeaponParts.Decode(reader);
            this.StockPartDefinition = set.WeaponParts.Decode(reader);
            this.ElementalPartDefinition = set.WeaponParts.Decode(reader);
            this.Accessory1PartDefinition = set.WeaponParts.Decode(reader);
            this.Accessory2PartDefinition = set.WeaponParts.Decode(reader);
            this.MaterialPartDefinition = set.WeaponParts.Decode(reader);
            this.PrefixPartDefinition = set.WeaponParts.Decode(reader);
            this.TitlePartDefinition = set.WeaponParts.Decode(reader);
        }

        public void Write(BitWriter writer)
        {
            var set = InfoManager.AssetLibraryManager.GetSet(this.AssetLibrarySetId);

            set.WeaponTypes.Encode(writer, this.WeaponTypeDefinition);
            set.BalanceDefs.Encode(writer, this.BalanceDefinition);
            set.Manufacturers.Encode(writer, this.ManufacturerDefinition);
            writer.WriteInt32(this.ManufacturerGradeIndex, 7);
            writer.WriteInt32(this.GameStage, 7);
            set.WeaponParts.Encode(writer, this.BodyPartDefinition);
            set.WeaponParts.Encode(writer, this.GripPartDefinition);
            set.WeaponParts.Encode(writer, this.BarrelPartDefinition);
            set.WeaponParts.Encode(writer, this.SightPartDefinition);
            set.WeaponParts.Encode(writer, this.StockPartDefinition);
            set.WeaponParts.Encode(writer, this.ElementalPartDefinition);
            set.WeaponParts.Encode(writer, this.Accessory1PartDefinition);
            set.WeaponParts.Encode(writer, this.Accessory2PartDefinition);
            set.WeaponParts.Encode(writer, this.MaterialPartDefinition);
            set.WeaponParts.Encode(writer, this.PrefixPartDefinition);
            set.WeaponParts.Encode(writer, this.TitlePartDefinition);
        }

        #region Properties
        public string WeaponTypeDefinition
        {
            get { return this._WeaponTypeDefinition; }
            set
            {
                if (value != this._WeaponTypeDefinition)
                {
                    this._WeaponTypeDefinition = value;
                    this.NotifyOfPropertyChange(() => this.WeaponTypeDefinition);
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

        public string BodyPartDefinition
        {
            get { return this._BodyPartDefinition; }
            set
            {
                if (value != this._BodyPartDefinition)
                {
                    this._BodyPartDefinition = value;
                    this.NotifyOfPropertyChange(() => this.BodyPartDefinition);
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
                    this.NotifyOfPropertyChange(() => this.GripPartDefinition);
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
                    this.NotifyOfPropertyChange(() => this.BarrelPartDefinition);
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
                    this.NotifyOfPropertyChange(() => this.SightPartDefinition);
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
                    this.NotifyOfPropertyChange(() => this.StockPartDefinition);
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
                    this.NotifyOfPropertyChange(() => this.ElementalPartDefinition);
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
                    this.NotifyOfPropertyChange(() => this.Accessory1PartDefinition);
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
                    this.NotifyOfPropertyChange(() => this.Accessory2PartDefinition);
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
                    this.NotifyOfPropertyChange(() => this.MaterialPartDefinition);
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
                    this.NotifyOfPropertyChange(() => this.PrefixPartDefinition);
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
                    this.NotifyOfPropertyChange(() => this.TitlePartDefinition);
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
            return new PackableWeapon()
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
            };
        }
        #endregion
    }
}
