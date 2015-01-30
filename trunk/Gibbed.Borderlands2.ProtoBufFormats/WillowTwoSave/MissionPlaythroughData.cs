/* Copyright (c) 2015 Rick (rick 'at' gibbed 'dot' us)
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
    public class MissionPlaythroughData : INotifyPropertyChanged
    {
        #region Fields
        private int? _PlayThroughNumber;
        private string _ActiveMission;
        private List<MissionData> _MissionData = new List<MissionData>();
        private List<PendingMissionRewards> _PendingMissionRewards = new List<PendingMissionRewards>();
        private List<string> _FilteredMissions = new List<string>();
        #endregion

        #region Serialization
        [ProtoAfterDeserialization]
        // ReSharper disable UnusedMember.Local
        private void OnDeserialization()
            // ReSharper restore UnusedMember.Local
        {
            this._MissionData = this._MissionData ?? new List<MissionData>();
            this._PendingMissionRewards = this._PendingMissionRewards ?? new List<PendingMissionRewards>();
            this._FilteredMissions = this._FilteredMissions ?? new List<string>();
        }

        private bool ShouldSerializeMissionData()
        {
            return this._MissionData != null &&
                   this._MissionData.Count > 0;
        }

        private bool ShouldSerializePendingMissionRewards()
        {
            return this._PendingMissionRewards != null &&
                   this._PendingMissionRewards.Count > 0;
        }

        private bool ShouldSerializeFilteredMissions()
        {
            return this._FilteredMissions != null &&
                   this._FilteredMissions.Count > 0;
        }
        #endregion

        #region Properties
        [ProtoMember(1, IsRequired = false)]
        public int? PlayThroughNumber
        {
            get { return this._PlayThroughNumber; }
            set
            {
                if (value != this._PlayThroughNumber)
                {
                    this._PlayThroughNumber = value;
                    this.NotifyPropertyChanged("PlayThroughNumber");
                }
            }
        }

        [ProtoMember(2, IsRequired = true)]
        public string ActiveMission
        {
            get { return this._ActiveMission; }
            set
            {
                if (value != this._ActiveMission)
                {
                    this._ActiveMission = value;
                    this.NotifyPropertyChanged("ActiveMission");
                }
            }
        }

        [ProtoMember(3, IsRequired = true)]
        public List<MissionData> MissionData
        {
            get { return this._MissionData; }
            set
            {
                if (value != this._MissionData)
                {
                    this._MissionData = value;
                    this.NotifyPropertyChanged("MissionData");
                }
            }
        }

        [ProtoMember(4, IsRequired = true)]
        public List<PendingMissionRewards> PendingMissionRewards
        {
            get { return this._PendingMissionRewards; }
            set
            {
                if (value != this._PendingMissionRewards)
                {
                    this._PendingMissionRewards = value;
                    this.NotifyPropertyChanged("PendingMissionRewards");
                }
            }
        }

        [ProtoMember(5, IsRequired = true)]
        public List<string> FilteredMissions
        {
            get { return this._FilteredMissions; }
            set
            {
                if (value != this._FilteredMissions)
                {
                    this._FilteredMissions = value;
                    this.NotifyPropertyChanged("FilteredMissions");
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
