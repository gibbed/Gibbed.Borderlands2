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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Gibbed.Borderlands2.ProtoBufFormats.WillowTwoSave
{
    [ProtoContract]
    public class ChosenVehicleCustomization : IComposable, INotifyPropertyChanged
    {
        #region Fields
        private string _Family;
        private List<string> _Customizations = new List<string>();
        #endregion

        #region IComposable Members
        private ComposeState _ComposeState = ComposeState.Composed;

        public void Compose()
        {
            if (this._ComposeState != ComposeState.Decomposed)
            {
                throw new InvalidOperationException();
            }
            this._ComposeState = ComposeState.Composed;

            if (this.Customizations == null ||
                this.Customizations.Count == 0)
            {
                this.Customizations = null;
            }
        }

        public void Decompose()
        {
            if (this._ComposeState != ComposeState.Composed)
            {
                throw new InvalidOperationException();
            }
            this._ComposeState = ComposeState.Decomposed;

            if (this.Customizations == null)
            {
                this.Customizations = new List<string>();
            }
        }
        #endregion

        #region Properties
        [ProtoMember(1, IsRequired = true)]
        public string Family
        {
            get { return this._Family; }
            set
            {
                if (value != this._Family)
                {
                    this._Family = value;
                    this.NotifyPropertyChanged("Family");
                }
            }
        }

        [ProtoMember(2, IsRequired = true)]
        public List<string> Customizations
        {
            get { return this._Customizations; }
            set
            {
                if (value != this._Customizations)
                {
                    this._Customizations = value;
                    this.NotifyPropertyChanged("Customizations");
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
