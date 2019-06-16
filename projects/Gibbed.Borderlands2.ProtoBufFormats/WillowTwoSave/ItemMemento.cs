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
using ProtoBuf;

namespace Gibbed.Borderlands2.ProtoBufFormats.WillowTwoSave
{
    [ProtoContract]
    public class ItemMemento : INotifyPropertyChanged
    {
        #region Fields
        private string _Unknown1;
        private string _Unknown2;
        private string _Unknown3;
        private int _Unknown4;
        private string _Unknown5;
        private string _Unknown6;
        private string _Unknown7;
        private string _Unknown8;
        private string _Unknown9;
        private string _Unknown10;
        private string _Unknown11;
        private string _Unknown12;
        private string _Unknown13;
        private string _Unknown14;
        private string _Unknown15;
        private int _Unknown16;
        private int _Unknown17;
        private bool _Unknown18;
        private bool _Unknown19;
        private bool _Unknown20;
        #endregion

        #region Properties
        [ProtoMember(1, IsRequired = true)]
        public string Unknown1
        {
            get { return this._Unknown1; }
            set
            {
                if (value != this._Unknown1)
                {
                    this._Unknown1 = value;
                    this.NotifyOfPropertyChange(nameof(Unknown1));
                }
            }
        }

        [ProtoMember(2, IsRequired = true)]
        public string Unknown2
        {
            get { return this._Unknown2; }
            set
            {
                if (value != this._Unknown2)
                {
                    this._Unknown2 = value;
                    this.NotifyOfPropertyChange(nameof(Unknown2));
                }
            }
        }

        [ProtoMember(3, IsRequired = true)]
        public string Unknown3
        {
            get { return this._Unknown3; }
            set
            {
                if (value != this._Unknown3)
                {
                    this._Unknown3 = value;
                    this.NotifyOfPropertyChange(nameof(Unknown3));
                }
            }
        }

        [ProtoMember(4, IsRequired = true)]
        public int Unknown4
        {
            get { return this._Unknown4; }
            set
            {
                if (value != this._Unknown4)
                {
                    this._Unknown4 = value;
                    this.NotifyOfPropertyChange(nameof(Unknown4));
                }
            }
        }

        [ProtoMember(5, IsRequired = true)]
        public string Unknown5
        {
            get { return this._Unknown5; }
            set
            {
                if (value != this._Unknown5)
                {
                    this._Unknown5 = value;
                    this.NotifyOfPropertyChange(nameof(Unknown5));
                }
            }
        }

        [ProtoMember(6, IsRequired = true)]
        public string Unknown6
        {
            get { return this._Unknown6; }
            set
            {
                if (value != this._Unknown6)
                {
                    this._Unknown6 = value;
                    this.NotifyOfPropertyChange(nameof(Unknown6));
                }
            }
        }

        [ProtoMember(7, IsRequired = true)]
        public string Unknown7
        {
            get { return this._Unknown7; }
            set
            {
                if (value != this._Unknown7)
                {
                    this._Unknown7 = value;
                    this.NotifyOfPropertyChange(nameof(Unknown7));
                }
            }
        }

        [ProtoMember(8, IsRequired = true)]
        public string Unknown8
        {
            get { return this._Unknown8; }
            set
            {
                if (value != this._Unknown8)
                {
                    this._Unknown8 = value;
                    this.NotifyOfPropertyChange(nameof(Unknown8));
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
                    this.NotifyOfPropertyChange(nameof(Unknown9));
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
                    this.NotifyOfPropertyChange(nameof(Unknown10));
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
                    this.NotifyOfPropertyChange(nameof(Unknown11));
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
                    this.NotifyOfPropertyChange(nameof(Unknown12));
                }
            }
        }

        [ProtoMember(13, IsRequired = true)]
        public string Unknown13
        {
            get { return this._Unknown13; }
            set
            {
                if (value != this._Unknown13)
                {
                    this._Unknown13 = value;
                    this.NotifyOfPropertyChange(nameof(Unknown13));
                }
            }
        }

        [ProtoMember(14, IsRequired = true)]
        public string Unknown14
        {
            get { return this._Unknown14; }
            set
            {
                if (value != this._Unknown14)
                {
                    this._Unknown14 = value;
                    this.NotifyOfPropertyChange(nameof(Unknown14));
                }
            }
        }

        [ProtoMember(15, IsRequired = true)]
        public string Unknown15
        {
            get { return this._Unknown15; }
            set
            {
                if (value != this._Unknown15)
                {
                    this._Unknown15 = value;
                    this.NotifyOfPropertyChange(nameof(Unknown15));
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
                    this.NotifyOfPropertyChange(nameof(Unknown16));
                }
            }
        }

        [ProtoMember(17, IsRequired = true)]
        public int Unknown17
        {
            get { return this._Unknown17; }
            set
            {
                if (value != this._Unknown17)
                {
                    this._Unknown17 = value;
                    this.NotifyOfPropertyChange(nameof(Unknown17));
                }
            }
        }

        [ProtoMember(18, IsRequired = true)]
        public bool Unknown18
        {
            get { return this._Unknown18; }
            set
            {
                if (value != this._Unknown18)
                {
                    this._Unknown18 = value;
                    this.NotifyOfPropertyChange(nameof(Unknown18));
                }
            }
        }

        [ProtoMember(19, IsRequired = true)]
        public bool Unknown19
        {
            get { return this._Unknown19; }
            set
            {
                if (value != this._Unknown19)
                {
                    this._Unknown19 = value;
                    this.NotifyOfPropertyChange(nameof(Unknown19));
                }
            }
        }

        [ProtoMember(20, IsRequired = true)]
        public bool Unknown20
        {
            get { return this._Unknown20; }
            set
            {
                if (value != this._Unknown20)
                {
                    this._Unknown20 = value;
                    this.NotifyOfPropertyChange(nameof(Unknown20));
                }
            }
        }
        #endregion

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyOfPropertyChange(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
