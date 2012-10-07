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
using ProtoBuf;

namespace Gibbed.Borderlands2.ProtoBufFormats.WillowTwoSave
{
    [ProtoContract]
    public class WeaponData : IComposable, INotifyPropertyChanged
    {
        #region Fields
        private string _BalanceDefinition;
        private string _ManufacturerDefinition;
        private string _WeaponTypeDefinition;
        private string _BodyPartDefinition;
        private string _GripPartDefinition;
        private string _BarrelPartDefinition;
        private string _SightPartDefinition;
        private string _StockPartDefinition;
        private string _Unknown9; // unused?
        private string _Unknown10; // unused?
        private string _Unknown11; // unused?
        private string _Unknown12; // unused?
        private string _MaterialPartDefinition;
        private string _PrefixPartDefinition;
        private string _TitlePartDefinition;
        private int _Unknown16; // unused?
        private int _ManufacturerGradeIndex;
        private QuickWeaponSlot _QuickSlot;
        private PlayerMark _Mark;
        private string _ElementalPartDefinition;
        private string _Accessory1PartDefinition;
        private string _Accessory2PartDefinition;
        #endregion

        #region IComposable Members
        public void Compose()
        {
        }

        public void Decompose()
        {
        }
        #endregion

        #region Properties
        [ProtoMember(1, IsRequired = true)]
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

        [ProtoMember(2, IsRequired = true)]
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

        [ProtoMember(3, IsRequired = true)]
        public string WeaponTypeDefinition
        {
            get { return this._WeaponTypeDefinition; }
            set
            {
                if (value != this._WeaponTypeDefinition)
                {
                    this._WeaponTypeDefinition = value;
                    this.NotifyPropertyChanged("WeaponTypeDefinition");
                }
            }
        }

        [ProtoMember(4, IsRequired = true)]
        public string BodyPartDefinition
        {
            get { return this._BodyPartDefinition; }
            set
            {
                if (value != this._BodyPartDefinition)
                {
                    this._BodyPartDefinition = value;
                    this.NotifyPropertyChanged("Unknown4");
                }
            }
        }

        [ProtoMember(5, IsRequired = true)]
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

        [ProtoMember(6, IsRequired = true)]
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

        [ProtoMember(7, IsRequired = true)]
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

        [ProtoMember(8, IsRequired = true)]
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

        [ProtoMember(9, IsRequired = true)]
        public string Unknown9
        {
            get { return this._Unknown9; }
            set
            {
                if (value != this._Unknown9)
                {
                    this._Unknown9 = value;
                    this.NotifyPropertyChanged("Unknown9");
                }
            }
        }

        [ProtoMember(10, IsRequired = true)]
        public string Unknown10
        {
            get { return this._Unknown10; }
            set
            {
                if (value != this._Unknown10)
                {
                    this._Unknown10 = value;
                    this.NotifyPropertyChanged("Unknown10");
                }
            }
        }

        [ProtoMember(11, IsRequired = true)]
        public string Unknown11
        {
            get { return this._Unknown11; }
            set
            {
                if (value != this._Unknown11)
                {
                    this._Unknown11 = value;
                    this.NotifyPropertyChanged("Unknown11");
                }
            }
        }

        [ProtoMember(12, IsRequired = true)]
        public string Unknown12
        {
            get { return this._Unknown12; }
            set
            {
                if (value != this._Unknown12)
                {
                    this._Unknown12 = value;
                    this.NotifyPropertyChanged("Unknown12");
                }
            }
        }

        [ProtoMember(13, IsRequired = true)]
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

        [ProtoMember(14, IsRequired = true)]
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

        [ProtoMember(15, IsRequired = true)]
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

        [ProtoMember(16, IsRequired = true)]
        public int Unknown16
        {
            get { return this._Unknown16; }
            set
            {
                if (value != this._Unknown16)
                {
                    this._Unknown16 = value;
                    this.NotifyPropertyChanged("Unknown16");
                }
            }
        }

        [ProtoMember(17, IsRequired = true)]
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

        [ProtoMember(18, IsRequired = true)]
        public QuickWeaponSlot QuickSlot
        {
            get { return this._QuickSlot; }
            set
            {
                if (value != this._QuickSlot)
                {
                    this._QuickSlot = value;
                    this.NotifyPropertyChanged("QuickSlot");
                }
            }
        }

        [ProtoMember(19, IsRequired = true)]
        public PlayerMark Mark
        {
            get { return this._Mark; }
            set
            {
                if (value != this._Mark)
                {
                    this._Mark = value;
                    this.NotifyPropertyChanged("Mark");
                }
            }
        }

        [ProtoMember(20, IsRequired = true)]
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

        [ProtoMember(21, IsRequired = true)]
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

        [ProtoMember(22, IsRequired = true)]
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
        #endregion

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
