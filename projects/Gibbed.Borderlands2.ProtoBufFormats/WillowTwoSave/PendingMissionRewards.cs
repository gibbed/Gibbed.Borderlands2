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

using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Gibbed.Borderlands2.ProtoBufFormats.WillowTwoSave
{
    [ProtoContract]
    public class PendingMissionRewards : INotifyPropertyChanged
    {
        #region Fields
        private string _Mission;
        private List<WeaponData> _WeaponRewards = new List<WeaponData>();
        private List<ItemData> _ItemRewards = new List<ItemData>();
        private List<PackedWeaponDataOptional> _PackedWeaponRewards = new List<PackedWeaponDataOptional>();
        private List<PackedItemDataOptional> _PackedItemRewards = new List<PackedItemDataOptional>();
        private bool _IsFromDLC;
        private int _DLCPackageId;
        #endregion

        #region Serialization
        [ProtoAfterDeserialization]
        private void OnDeserialization()
        {
            this._WeaponRewards = this._WeaponRewards ?? new List<WeaponData>();
            this._ItemRewards = this._ItemRewards ?? new List<ItemData>();
            this._PackedWeaponRewards = this._PackedWeaponRewards ?? new List<PackedWeaponDataOptional>();
            this._PackedItemRewards = this._PackedItemRewards ?? new List<PackedItemDataOptional>();
        }

        private bool ShouldSerializeWeaponRewards()
        {
            return this._WeaponRewards != null &&
                   this._WeaponRewards.Count > 0;
        }

        private bool ShouldSerializeItemRewards()
        {
            return this._ItemRewards != null &&
                   this._ItemRewards.Count > 0;
        }

        private bool ShouldSerializePackedWeaponRewards()
        {
            return this._PackedWeaponRewards != null &&
                   this._PackedWeaponRewards.Count > 0;
        }

        private bool ShouldSerializePackedItemRewards()
        {
            return this._PackedItemRewards != null &&
                   this._PackedItemRewards.Count > 0;
        }
        #endregion

        #region Properties
        [ProtoMember(1, IsRequired = true)]
        public string Mission
        {
            get { return this._Mission; }
            set
            {
                if (value != this._Mission)
                {
                    this._Mission = value;
                    this.NotifyOfPropertyChange(nameof(Mission));
                }
            }
        }

        [ProtoMember(2, IsRequired = false)]
        public List<WeaponData> WeaponRewards
        {
            get { return this._WeaponRewards; }
            set
            {
                if (value != this._WeaponRewards)
                {
                    this._WeaponRewards = value;
                    this.NotifyOfPropertyChange(nameof(WeaponRewards));
                }
            }
        }

        [ProtoMember(3, IsRequired = false)]
        public List<ItemData> ItemRewards
        {
            get { return this._ItemRewards; }
            set
            {
                if (value != this._ItemRewards)
                {
                    this._ItemRewards = value;
                    this.NotifyOfPropertyChange(nameof(ItemRewards));
                }
            }
        }

        [ProtoMember(4, IsRequired = true)]
        public List<PackedWeaponDataOptional> PackedWeaponRewards
        {
            get { return this._PackedWeaponRewards; }
            set
            {
                if (value != this._PackedWeaponRewards)
                {
                    this._PackedWeaponRewards = value;
                    this.NotifyOfPropertyChange(nameof(PackedWeaponRewards));
                }
            }
        }

        [ProtoMember(5, IsRequired = true)]
        public List<PackedItemDataOptional> PackedItemRewards
        {
            get { return this._PackedItemRewards; }
            set
            {
                if (value != this._PackedItemRewards)
                {
                    this._PackedItemRewards = value;
                    this.NotifyOfPropertyChange(nameof(PackedItemRewards));
                }
            }
        }

        [ProtoMember(6, IsRequired = true)]
        public bool IsFromDLC
        {
            get { return this._IsFromDLC; }
            set
            {
                if (value != this._IsFromDLC)
                {
                    this._IsFromDLC = value;
                    this.NotifyOfPropertyChange(nameof(IsFromDLC));
                }
            }
        }

        [ProtoMember(7, IsRequired = true)]
        public int DLCPackageId
        {
            get { return this._DLCPackageId; }
            set
            {
                if (value != this._DLCPackageId)
                {
                    this._DLCPackageId = value;
                    this.NotifyOfPropertyChange(nameof(DLCPackageId));
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
