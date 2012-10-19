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
    public class ItemData : IComposable, INotifyPropertyChanged
    {
        #region Fields
        private string _Balance;
        private string _ItemDefinition;
        private string _AlphaItemPartDefinition;
        private string _BetaItemPartDefinition;
        private string _GammaItemPartDefinition;
        private string _DeltaItemPartDefinition;
        private string _EpsilonItemPartDefinition;
        private string _ZetaItemPartDefinition;
        private string _EtaItemPartDefinition;
        private string _ThetaItemPartDefinition;
        private string _MaterialItemPartDefinition;
        private string _ManufacturerDefinition;
        private string _PrefixItemNamePartDefinition;
        private string _TitleItemNamePartDefinition;
        private int _Quantity;
        private int _ManufacturerGradeIndex;
        private bool _Equipped;
        private PlayerMark _Mark;
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
        public string Unknown1
        {
            get { return this._Balance; }
            set
            {
                if (value != this._Balance)
                {
                    this._Balance = value;
                    this.NotifyPropertyChanged("Unknown1");
                }
            }
        }

        [ProtoMember(2, IsRequired = true)]
        public string Unknown2
        {
            get { return this._ItemDefinition; }
            set
            {
                if (value != this._ItemDefinition)
                {
                    this._ItemDefinition = value;
                    this.NotifyPropertyChanged("Unknown2");
                }
            }
        }

        [ProtoMember(3, IsRequired = true)]
        public string Unknown3
        {
            get { return this._AlphaItemPartDefinition; }
            set
            {
                if (value != this._AlphaItemPartDefinition)
                {
                    this._AlphaItemPartDefinition = value;
                    this.NotifyPropertyChanged("Unknown3");
                }
            }
        }

        [ProtoMember(4, IsRequired = true)]
        public string Unknown4
        {
            get { return this._BetaItemPartDefinition; }
            set
            {
                if (value != this._BetaItemPartDefinition)
                {
                    this._BetaItemPartDefinition = value;
                    this.NotifyPropertyChanged("Unknown4");
                }
            }
        }

        [ProtoMember(5, IsRequired = true)]
        public string Unknown5
        {
            get { return this._GammaItemPartDefinition; }
            set
            {
                if (value != this._GammaItemPartDefinition)
                {
                    this._GammaItemPartDefinition = value;
                    this.NotifyPropertyChanged("Unknown5");
                }
            }
        }

        [ProtoMember(6, IsRequired = true)]
        public string Unknown6
        {
            get { return this._DeltaItemPartDefinition; }
            set
            {
                if (value != this._DeltaItemPartDefinition)
                {
                    this._DeltaItemPartDefinition = value;
                    this.NotifyPropertyChanged("Unknown6");
                }
            }
        }

        [ProtoMember(7, IsRequired = true)]
        public string Unknown7
        {
            get { return this._EpsilonItemPartDefinition; }
            set
            {
                if (value != this._EpsilonItemPartDefinition)
                {
                    this._EpsilonItemPartDefinition = value;
                    this.NotifyPropertyChanged("Unknown7");
                }
            }
        }

        [ProtoMember(8, IsRequired = true)]
        public string Unknown8
        {
            get { return this._ZetaItemPartDefinition; }
            set
            {
                if (value != this._ZetaItemPartDefinition)
                {
                    this._ZetaItemPartDefinition = value;
                    this.NotifyPropertyChanged("Unknown8");
                }
            }
        }

        [ProtoMember(9, IsRequired = true)]
        public string Unknown9
        {
            get { return this._EtaItemPartDefinition; }
            set
            {
                if (value != this._EtaItemPartDefinition)
                {
                    this._EtaItemPartDefinition = value;
                    this.NotifyPropertyChanged("Unknown9");
                }
            }
        }

        [ProtoMember(10, IsRequired = true)]
        public string Unknown10
        {
            get { return this._ThetaItemPartDefinition; }
            set
            {
                if (value != this._ThetaItemPartDefinition)
                {
                    this._ThetaItemPartDefinition = value;
                    this.NotifyPropertyChanged("Unknown10");
                }
            }
        }

        [ProtoMember(11, IsRequired = true)]
        public string Unknown11
        {
            get { return this._MaterialItemPartDefinition; }
            set
            {
                if (value != this._MaterialItemPartDefinition)
                {
                    this._MaterialItemPartDefinition = value;
                    this.NotifyPropertyChanged("Unknown11");
                }
            }
        }

        [ProtoMember(12, IsRequired = true)]
        public string Unknown12
        {
            get { return this._ManufacturerDefinition; }
            set
            {
                if (value != this._ManufacturerDefinition)
                {
                    this._ManufacturerDefinition = value;
                    this.NotifyPropertyChanged("Unknown12");
                }
            }
        }

        [ProtoMember(13, IsRequired = true)]
        public string Unknown13
        {
            get { return this._PrefixItemNamePartDefinition; }
            set
            {
                if (value != this._PrefixItemNamePartDefinition)
                {
                    this._PrefixItemNamePartDefinition = value;
                    this.NotifyPropertyChanged("Unknown13");
                }
            }
        }

        [ProtoMember(14, IsRequired = true)]
        public string Unknown14
        {
            get { return this._TitleItemNamePartDefinition; }
            set
            {
                if (value != this._TitleItemNamePartDefinition)
                {
                    this._TitleItemNamePartDefinition = value;
                    this.NotifyPropertyChanged("Unknown14");
                }
            }
        }

        [ProtoMember(15, IsRequired = true)]
        public int Unknown15
        {
            get { return this._Quantity; }
            set
            {
                if (value != this._Quantity)
                {
                    this._Quantity = value;
                    this.NotifyPropertyChanged("Unknown15");
                }
            }
        }

        [ProtoMember(16, IsRequired = true)]
        public int Unknown16
        {
            get { return this._ManufacturerGradeIndex; }
            set
            {
                if (value != this._ManufacturerGradeIndex)
                {
                    this._ManufacturerGradeIndex = value;
                    this.NotifyPropertyChanged("Unknown16");
                }
            }
        }

        [ProtoMember(17, IsRequired = true)]
        public bool Unknown17
        {
            get { return this._Equipped; }
            set
            {
                if (value != this._Equipped)
                {
                    this._Equipped = value;
                    this.NotifyPropertyChanged("Unknown17");
                }
            }
        }

        [ProtoMember(18, IsRequired = true)]
        public PlayerMark Unknown18
        {
            get { return this._Mark; }
            set
            {
                if (value != this._Mark)
                {
                    this._Mark = value;
                    this.NotifyPropertyChanged("Unknown18");
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
