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
    public class WillowTwoPlayerSaveGame : IComposable, INotifyPropertyChanged
    {
        #region Fields
        private string _PlayerClass;
        private int _ExpLevel = 1;
        private int _ExpPoints;
        private int _GeneralSkillPoints;
        private int _SpecialistSkillPoints;
        private List<int> _CurrencyOnHand;
        private int _PlaythroughsCompleted;
        private List<SkillData> _SkillData;
        private List<int> _Unknown9;
        private List<int> _Unknown10;
        private List<ResourceData> _ResourceData;
        private List<ItemData> _ItemData;
        private InventorySlotData _InventorySlotData;
        private List<WeaponData> _WeaponData;
        private byte[] _StatsData;
        private List<string> _VisitedTeleporters;
        private string _LastVisitedTeleporter;
        private List<MissionPlaythroughData> _MissionPlaythroughs;
        private UIPreferencesData _UIPreferences;
        private int _SaveGameId;
        private int _PlotMissionNumber;
        private int _Unknown22;
        private List<int> _UsedMarketingCodes;
        private List<int> _MarketingCodesNeedingNotification;
        private int _TotalPlayTime;
        private string _LastSavedDate;
        private List<DLCExpansionData> _DLCExpansionData;
        private List<string> _Unknown28;
        private List<RegionGameStageData> _RegionGameStages;
        private List<WorldDiscoveryData> _WorldDiscoveryList;
        private bool _IsBadassModeSaveGame;
        private List<WeaponMemento> _Unknown32;
        private List<ItemMemento> _Unknown33;
        private Guid _SaveGuid;
        private List<string> _AppliedCustomizations;
        private List<int> _BlackMarketUpgrades;
        private int _ActiveMissionNumber;
        private List<ChallengeData> _ChallengeList;
        private List<int> _LevelChallengeUnlocks;
        private List<OneOffLevelChallengeData> _OneOffLevelChallengeCompletion;
        private List<BankSlot> _BankSlots;
        private int _NumChallengePrestiges;
        private List<LockoutData> _LockoutList;
        private bool _IsDLCPlayerClass;
        private int _DLCPlayerClassPackageId;
        private List<string> _FullyExploredAreas;
        private List<GoldenKeys> _Unknown47;
        private int _NumGoldenKeysNotified;
        private int _LastPlaythroughNumber;
        private bool _ShowNewPlaythroughNotification;
        private bool _ReceivedDefaultWeapon;
        private List<string> _QueuedTrainingMessages;
        private List<PackedItemData> _PackedItemData;
        private List<PackedWeaponData> _PackedWeaponData;
        private bool _AwesomeSkillDisabled;
        private int _MaxBankSlots;
        private int? _ExtraShowNewPlaythroughNotification;
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

            if (this.CurrencyOnHand == null ||
                this.CurrencyOnHand.Count == 0)
            {
                this.CurrencyOnHand = null;
            }

            if (this.SkillData == null ||
                this.SkillData.Count == 0)
            {
                this.SkillData = null;
            }
            else
            {
                this.SkillData.Compose();
            }

            if (this.Unknown9 == null ||
                this.Unknown9.Count == 0)
            {
                this.Unknown9 = null;
            }

            if (this.Unknown10 == null ||
                this.Unknown10.Count == 0)
            {
                this.Unknown10 = null;
            }

            if (this.ResourceData == null ||
                this.ResourceData.Count == 0)
            {
                this.ResourceData = null;
            }
            else
            {
                this.ResourceData.Compose();
            }

            if (this.ItemData == null ||
                this.ItemData.Count == 0)
            {
                this.ItemData = null;
            }
            else
            {
                this.ItemData.Compose();
            }

            this.InventorySlotData.Compose();

            if (this.WeaponData == null ||
                this.WeaponData.Count == 0)
            {
                this.WeaponData = null;
            }
            else
            {
                this.WeaponData.Compose();
            }

            if (this.VisitedTeleporters == null ||
                this.VisitedTeleporters.Count == 0)
            {
                this.VisitedTeleporters = null;
            }

            if (this.MissionPlaythroughs == null ||
                this.MissionPlaythroughs.Count == 0)
            {
                this.MissionPlaythroughs = null;
            }
            else
            {
                this.MissionPlaythroughs.Compose();
            }

            this.UIPreferences.Compose();

            if (this.UsedMarketingCodes == null ||
                this.UsedMarketingCodes.Count == 0)
            {
                this.UsedMarketingCodes = null;
            }

            if (this.MarketingCodesNeedingNotification == null ||
                this.MarketingCodesNeedingNotification.Count == 0)
            {
                this.MarketingCodesNeedingNotification = null;
            }

            if (this.DLCExpansionData == null ||
                this.DLCExpansionData.Count == 0)
            {
                this.DLCExpansionData = null;
            }
            else
            {
                this.DLCExpansionData.Compose();
            }

            if (this.Unknown28 == null ||
                this.Unknown28.Count == 0)
            {
                this.Unknown28 = null;
            }

            if (this.RegionGameStages == null ||
                this.RegionGameStages.Count == 0)
            {
                this.RegionGameStages = null;
            }
            else
            {
                this.RegionGameStages.Compose();
            }

            if (this.WorldDiscoveryList == null ||
                this.WorldDiscoveryList.Count == 0)
            {
                this.WorldDiscoveryList = null;
            }
            else
            {
                this.WorldDiscoveryList.Compose();
            }

            if (this.Unknown32 == null ||
                this.Unknown32.Count == 0)
            {
                this.Unknown32 = null;
            }
            else
            {
                this.Unknown32.Compose();
            }

            if (this.Unknown33 == null ||
                this.Unknown33.Count == 0)
            {
                this.Unknown33 = null;
            }
            else
            {
                this.Unknown33.Compose();
            }

            this.SaveGuid.Compose();

            if (this.AppliedCustomizations == null ||
                this.AppliedCustomizations.Count == 0)
            {
                this.AppliedCustomizations = null;
            }

            if (this.BlackMarketUpgrades == null ||
                this.BlackMarketUpgrades.Count == 0)
            {
                this.BlackMarketUpgrades = null;
            }

            if (this.ChallengeList == null ||
                this.ChallengeList.Count == 0)
            {
                this.ChallengeList = null;
            }
            else
            {
                this.ChallengeList.Compose();
            }

            if (this.LevelChallengeUnlocks == null ||
                this.LevelChallengeUnlocks.Count == 0)
            {
                this.LevelChallengeUnlocks = null;
            }

            if (this.OneOffLevelChallengeCompletion == null ||
                this.OneOffLevelChallengeCompletion.Count == 0)
            {
                this.OneOffLevelChallengeCompletion = null;
            }
            else
            {
                this.OneOffLevelChallengeCompletion.Compose();
            }

            if (this.BankSlots == null ||
                this.BankSlots.Count == 0)
            {
                this.BankSlots = null;
            }
            else
            {
                this.BankSlots.Compose();
            }

            if (this.LockoutList == null ||
                this.LockoutList.Count == 0)
            {
                this.LockoutList = null;
            }
            else
            {
                this.LockoutList.Compose();
            }

            if (this.FullyExploredAreas == null ||
                this.FullyExploredAreas.Count == 0)
            {
                this.FullyExploredAreas = null;
            }

            if (this.Unknown47 == null ||
                this.Unknown47.Count == 0)
            {
                this.Unknown47 = null;
            }
            else
            {
                this.Unknown47.Compose();
            }

            if (this.QueuedTrainingMessages == null ||
                this.QueuedTrainingMessages.Count == 0)
            {
                this.QueuedTrainingMessages = null;
            }

            if (this.PackedItemData == null ||
                this.PackedItemData.Count == 0)
            {
                this.PackedItemData = null;
            }
            else
            {
                this.PackedItemData.Compose();
            }

            if (this.PackedWeaponData == null ||
                this.PackedWeaponData.Count == 0)
            {
                this.PackedWeaponData = null;
            }
            else
            {
                this.PackedWeaponData.Compose();
            }
        }

        public void Decompose()
        {
            if (this._ComposeState != ComposeState.Composed)
            {
                throw new InvalidOperationException();
            }
            this._ComposeState = ComposeState.Decomposed;

            if (this.CurrencyOnHand == null)
            {
                this.CurrencyOnHand = new List<int>();
            }

            if (this.SkillData == null)
            {
                this.SkillData = new List<SkillData>();
            }
            else
            {
                this.SkillData.Decompose();
            }

            if (this.Unknown9 == null)
            {
                this.Unknown9 = new List<int>();
            }

            if (this.Unknown10 == null)
            {
                this.Unknown10 = new List<int>();
            }

            if (this.ResourceData == null)
            {
                this.ResourceData = new List<ResourceData>();
            }
            else
            {
                this.ResourceData.Decompose();
            }

            if (this.ItemData == null)
            {
                this.ItemData = new List<ItemData>();
            }
            else
            {
                this.ItemData.Decompose();
            }

            this.InventorySlotData.Decompose();

            if (this.WeaponData == null)
            {
                this.WeaponData = new List<WeaponData>();
            }
            else
            {
                this.WeaponData.Decompose();
            }

            if (this.VisitedTeleporters == null)
            {
                this.VisitedTeleporters = new List<string>();
            }

            if (this.MissionPlaythroughs == null)
            {
                this.MissionPlaythroughs = new List<MissionPlaythroughData>();
            }
            else
            {
                this.MissionPlaythroughs.Decompose();
            }

            this.UIPreferences.Decompose();

            if (this.UsedMarketingCodes == null)
            {
                this.UsedMarketingCodes = new List<int>();
            }

            if (this.MarketingCodesNeedingNotification == null)
            {
                this.MarketingCodesNeedingNotification = new List<int>();
            }

            if (this.DLCExpansionData == null)
            {
                this.DLCExpansionData = new List<DLCExpansionData>();
            }
            else
            {
                this.DLCExpansionData.Decompose();
            }

            if (this.Unknown28 == null)
            {
                this.Unknown28 = new List<string>();
            }

            if (this.RegionGameStages == null)
            {
                this.RegionGameStages = new List<RegionGameStageData>();
            }
            else
            {
                this.RegionGameStages.Decompose();
            }

            if (this.WorldDiscoveryList == null)
            {
                this.WorldDiscoveryList = new List<WorldDiscoveryData>();
            }
            else
            {
                this.WorldDiscoveryList.Decompose();
            }

            if (this.Unknown32 == null)
            {
                this.Unknown32 = new List<WeaponMemento>();
            }
            else
            {
                this.Unknown32.Decompose();
            }

            if (this.Unknown33 == null)
            {
                this.Unknown33 = new List<ItemMemento>();
            }
            else
            {
                this.Unknown33.Decompose();
            }

            this.SaveGuid.Decompose();

            if (this.AppliedCustomizations == null)
            {
                this.AppliedCustomizations = new List<string>();
            }

            if (this.BlackMarketUpgrades == null)
            {
                this.BlackMarketUpgrades = new List<int>();
            }

            if (this.ChallengeList == null)
            {
                this.ChallengeList = new List<ChallengeData>();
            }
            else
            {
                this.ChallengeList.Decompose();
            }

            if (this.LevelChallengeUnlocks == null)
            {
                this.LevelChallengeUnlocks = new List<int>();
            }

            if (this.OneOffLevelChallengeCompletion == null)
            {
                this.OneOffLevelChallengeCompletion = new List<OneOffLevelChallengeData>();
            }
            else
            {
                this.OneOffLevelChallengeCompletion.Decompose();
            }

            if (this.BankSlots == null)
            {
                this.BankSlots = new List<BankSlot>();
            }
            else
            {
                this.BankSlots.Decompose();
            }

            if (this.LockoutList == null)
            {
                this.LockoutList = new List<LockoutData>();
            }
            else
            {
                this.LockoutList.Decompose();
            }

            if (this.FullyExploredAreas == null)
            {
                this.FullyExploredAreas = new List<string>();
            }

            if (this.Unknown47 == null)
            {
                this.Unknown47 = new List<GoldenKeys>();
            }
            else
            {
                this.Unknown47.Decompose();
            }

            if (this.QueuedTrainingMessages == null)
            {
                this.QueuedTrainingMessages = new List<string>();
            }

            if (this.PackedItemData == null)
            {
                this.PackedItemData = new List<PackedItemData>();
            }
            else
            {
                this.PackedItemData.Decompose();
            }

            if (this.PackedWeaponData == null)
            {
                this.PackedWeaponData = new List<PackedWeaponData>();
            }
            else
            {
                this.PackedWeaponData.Decompose();
            }
        }
        #endregion

        #region Properties
        [ProtoMember(1, IsRequired = true)]
        public string PlayerClass
        {
            get { return this._PlayerClass; }
            set
            {
                if (value != this._PlayerClass)
                {
                    this._PlayerClass = value;
                    this.NotifyPropertyChanged("PlayerClass");
                }
            }
        }

        [ProtoMember(2, IsRequired = true)]
        public int ExpLevel
        {
            get { return this._ExpLevel; }
            set
            {
                if (value != this._ExpLevel)
                {
                    this._ExpLevel = value;
                    this.NotifyPropertyChanged("ExpLevel");
                }
            }
        }

        [ProtoMember(3, IsRequired = true)]
        public int ExpPoints
        {
            get { return this._ExpPoints; }
            set
            {
                if (value != this._ExpPoints)
                {
                    this._ExpPoints = value;
                    this.NotifyPropertyChanged("ExpPoints");
                }
            }
        }

        [ProtoMember(4, IsRequired = true)]
        public int GeneralSkillPoints
        {
            get { return this._GeneralSkillPoints; }
            set
            {
                if (value != this._GeneralSkillPoints)
                {
                    this._GeneralSkillPoints = value;
                    this.NotifyPropertyChanged("GeneralSkillPoints");
                }
            }
        }

        [ProtoMember(5, IsRequired = true)]
        public int SpecialistSkillPoints
        {
            get { return this._SpecialistSkillPoints; }
            set
            {
                if (value != this._SpecialistSkillPoints)
                {
                    this._SpecialistSkillPoints = value;
                    this.NotifyPropertyChanged("SpecialistSkillPoints");
                }
            }
        }

        [ProtoMember(6, IsRequired = true, IsPacked = true)]
        public List<int> CurrencyOnHand
        {
            get { return this._CurrencyOnHand; }
            set
            {
                if (value != this._CurrencyOnHand)
                {
                    this._CurrencyOnHand = value;
                    this.NotifyPropertyChanged("CurrencyOnHand");
                }
            }
        }

        [ProtoMember(7, IsRequired = true)]
        public int PlaythroughsCompleted
        {
            get { return this._PlaythroughsCompleted; }
            set
            {
                if (value != this._PlaythroughsCompleted)
                {
                    this._PlaythroughsCompleted = value;
                    this.NotifyPropertyChanged("PlaythroughsCompleted");
                }
            }
        }

        [ProtoMember(8, IsRequired = true)]
        public List<SkillData> SkillData
        {
            get { return this._SkillData; }
            set
            {
                if (value != this._SkillData)
                {
                    this._SkillData = value;
                    this.NotifyPropertyChanged("SkillData");
                }
            }
        }

        // Deprecated?
        [ProtoMember(9, IsRequired = true)]
        public List<int> Unknown9
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

        // Deprecated?
        [ProtoMember(10, IsRequired = true)]
        public List<int> Unknown10
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
        public List<ResourceData> ResourceData
        {
            get { return this._ResourceData; }
            set
            {
                if (value != this._ResourceData)
                {
                    this._ResourceData = value;
                    this.NotifyPropertyChanged("ResourceData");
                }
            }
        }

        [ProtoMember(12, IsRequired = true)]
        public List<ItemData> ItemData
        {
            get { return this._ItemData; }
            set
            {
                if (value != this._ItemData)
                {
                    this._ItemData = value;
                    this.NotifyPropertyChanged("ItemData");
                }
            }
        }

        [ProtoMember(13, IsRequired = true)]
        public InventorySlotData InventorySlotData
        {
            get { return this._InventorySlotData; }
            set
            {
                if (value != this._InventorySlotData)
                {
                    this._InventorySlotData = value;
                    this.NotifyPropertyChanged("InventorySlotData");
                }
            }
        }

        [ProtoMember(14, IsRequired = true)]
        public List<WeaponData> WeaponData
        {
            get { return this._WeaponData; }
            set
            {
                if (value != this._WeaponData)
                {
                    this._WeaponData = value;
                    this.NotifyPropertyChanged("WeaponData");
                }
            }
        }

        [ProtoMember(15, IsRequired = true)]
        public byte[] StatsData
        {
            get { return this._StatsData; }
            set
            {
                if (value != this._StatsData)
                {
                    this._StatsData = value;
                    this.NotifyPropertyChanged("StatsData");
                }
            }
        }

        [ProtoMember(16, IsRequired = true)]
        public List<string> VisitedTeleporters
        {
            get { return this._VisitedTeleporters; }
            set
            {
                if (value != this._VisitedTeleporters)
                {
                    this._VisitedTeleporters = value;
                    this.NotifyPropertyChanged("VisitedTeleporters");
                }
            }
        }

        [ProtoMember(17, IsRequired = true)]
        public string LastVisitedTeleporter
        {
            get { return this._LastVisitedTeleporter; }
            set
            {
                if (value != this._LastVisitedTeleporter)
                {
                    this._LastVisitedTeleporter = value;
                    this.NotifyPropertyChanged("LastVisitedTeleporter");
                }
            }
        }

        [ProtoMember(18, IsRequired = true)]
        public List<MissionPlaythroughData> MissionPlaythroughs
        {
            get { return this._MissionPlaythroughs; }
            set
            {
                if (value != this._MissionPlaythroughs)
                {
                    this._MissionPlaythroughs = value;
                    this.NotifyPropertyChanged("MissionPlaythroughs");
                }
            }
        }

        [ProtoMember(19, IsRequired = true)]
        public UIPreferencesData UIPreferences
        {
            get { return this._UIPreferences; }
            set
            {
                if (value != this._UIPreferences)
                {
                    this._UIPreferences = value;
                    this.NotifyPropertyChanged("UIPreferences");
                }
            }
        }

        [ProtoMember(20, IsRequired = true)]
        public int SaveGameId
        {
            get { return this._SaveGameId; }
            set
            {
                if (value != this._SaveGameId)
                {
                    this._SaveGameId = value;
                    this.NotifyPropertyChanged("SaveGameId");
                }
            }
        }

        [ProtoMember(21, IsRequired = true)]
        public int PlotMissionNumber
        {
            get { return this._PlotMissionNumber; }
            set
            {
                if (value != this._PlotMissionNumber)
                {
                    this._PlotMissionNumber = value;
                    this.NotifyPropertyChanged("PlotMissionNumber");
                }
            }
        }

        // Deprecated?
        [ProtoMember(22, IsRequired = false)]
        public int Unknown22
        {
            get { return this._Unknown22; }
            set
            {
                if (value != this._Unknown22)
                {
                    this._Unknown22 = value;
                    this.NotifyPropertyChanged("Unknown22");
                }
            }
        }

        [ProtoMember(23, IsRequired = false, IsPacked = true)]
        public List<int> UsedMarketingCodes
        {
            get { return this._UsedMarketingCodes; }
            set
            {
                if (value != this._UsedMarketingCodes)
                {
                    this._UsedMarketingCodes = value;
                    this.NotifyPropertyChanged("UsedMarketingCodes");
                }
            }
        }

        [ProtoMember(24, IsRequired = false, IsPacked = true)]
        public List<int> MarketingCodesNeedingNotification
        {
            get { return this._MarketingCodesNeedingNotification; }
            set
            {
                if (value != this._MarketingCodesNeedingNotification)
                {
                    this._MarketingCodesNeedingNotification = value;
                    this.NotifyPropertyChanged("MarketingCodesNeedingNotification");
                }
            }
        }

        [ProtoMember(25, IsRequired = true)]
        public int TotalPlayTime
        {
            get { return this._TotalPlayTime; }
            set
            {
                if (value != this._TotalPlayTime)
                {
                    this._TotalPlayTime = value;
                    this.NotifyPropertyChanged("TotalPlayTime");
                }
            }
        }

        [ProtoMember(26, IsRequired = true)]
        public string LastSavedDate
        {
            get { return this._LastSavedDate; }
            set
            {
                if (value != this._LastSavedDate)
                {
                    this._LastSavedDate = value;
                    this.NotifyPropertyChanged("LastSavedDate");
                }
            }
        }

        [ProtoMember(27, IsRequired = true)]
        public List<DLCExpansionData> DLCExpansionData
        {
            get { return this._DLCExpansionData; }
            set
            {
                if (value != this._DLCExpansionData)
                {
                    this._DLCExpansionData = value;
                    this.NotifyPropertyChanged("DLCExpansionData");
                }
            }
        }

        // Deprecated?
        [ProtoMember(28, IsRequired = true)]
        public List<string> Unknown28
        {
            get { return this._Unknown28; }
            set
            {
                if (value != this._Unknown28)
                {
                    this._Unknown28 = value;
                    this.NotifyPropertyChanged("Unknown28");
                }
            }
        }

        [ProtoMember(29, IsRequired = true)]
        public List<RegionGameStageData> RegionGameStages
        {
            get { return this._RegionGameStages; }
            set
            {
                if (value != this._RegionGameStages)
                {
                    this._RegionGameStages = value;
                    this.NotifyPropertyChanged("RegionGameStages");
                }
            }
        }

        [ProtoMember(30, IsRequired = true)]
        public List<WorldDiscoveryData> WorldDiscoveryList
        {
            get { return this._WorldDiscoveryList; }
            set
            {
                if (value != this._WorldDiscoveryList)
                {
                    this._WorldDiscoveryList = value;
                    this.NotifyPropertyChanged("WorldDiscoveryList");
                }
            }
        }

        [ProtoMember(31, IsRequired = true)]
        public bool IsBadassModeSaveGame
        {
            get { return this._IsBadassModeSaveGame; }
            set
            {
                if (value != this._IsBadassModeSaveGame)
                {
                    this._IsBadassModeSaveGame = value;
                    this.NotifyPropertyChanged("IsBadassModeSaveGame");
                }
            }
        }

        [ProtoMember(32, IsRequired = true)]
        public List<WeaponMemento> Unknown32
        {
            get { return this._Unknown32; }
            set
            {
                if (value != this._Unknown32)
                {
                    this._Unknown32 = value;
                    this.NotifyPropertyChanged("Unknown32");
                }
            }
        }

        [ProtoMember(33, IsRequired = true)]
        public List<ItemMemento> Unknown33
        {
            get { return this._Unknown33; }
            set
            {
                if (value != this._Unknown33)
                {
                    this._Unknown33 = value;
                    this.NotifyPropertyChanged("Unknown33");
                }
            }
        }

        [ProtoMember(34, IsRequired = true)]
        public Guid SaveGuid
        {
            get { return this._SaveGuid; }
            set
            {
                if (value != this._SaveGuid)
                {
                    this._SaveGuid = value;
                    this.NotifyPropertyChanged("SaveGuid");
                }
            }
        }

        [ProtoMember(35, IsRequired = true)]
        public List<string> AppliedCustomizations
        {
            get { return this._AppliedCustomizations; }
            set
            {
                if (value != this._AppliedCustomizations)
                {
                    this._AppliedCustomizations = value;
                    this.NotifyPropertyChanged("AppliedCustomizations");
                }
            }
        }

        [ProtoMember(36, IsRequired = true, IsPacked = true)]
        public List<int> BlackMarketUpgrades
        {
            get { return this._BlackMarketUpgrades; }
            set
            {
                if (value != this._BlackMarketUpgrades)
                {
                    this._BlackMarketUpgrades = value;
                    this.NotifyPropertyChanged("BlackMarketUpgrades");
                }
            }
        }

        [ProtoMember(37, IsRequired = true)]
        public int ActiveMissionNumber
        {
            get { return this._ActiveMissionNumber; }
            set
            {
                if (value != this._ActiveMissionNumber)
                {
                    this._ActiveMissionNumber = value;
                    this.NotifyPropertyChanged("ActiveMissionNumber");
                }
            }
        }

        [ProtoMember(38, IsRequired = true)]
        public List<ChallengeData> ChallengeList
        {
            get { return this._ChallengeList; }
            set
            {
                if (value != this._ChallengeList)
                {
                    this._ChallengeList = value;
                    this.NotifyPropertyChanged("ChallengeList");
                }
            }
        }

        [ProtoMember(39, IsRequired = true, IsPacked = true)]
        public List<int> LevelChallengeUnlocks
        {
            get { return this._LevelChallengeUnlocks; }
            set
            {
                if (value != this._LevelChallengeUnlocks)
                {
                    this._LevelChallengeUnlocks = value;
                    this.NotifyPropertyChanged("LevelChallengeUnlocks");
                }
            }
        }

        [ProtoMember(40, IsRequired = true)]
        public List<OneOffLevelChallengeData> OneOffLevelChallengeCompletion
        {
            get { return this._OneOffLevelChallengeCompletion; }
            set
            {
                if (value != this._OneOffLevelChallengeCompletion)
                {
                    this._OneOffLevelChallengeCompletion = value;
                    this.NotifyPropertyChanged("OneOffLevelChallengeCompletion");
                }
            }
        }

        [ProtoMember(41, IsRequired = true)]
        public List<BankSlot> BankSlots
        {
            get { return this._BankSlots; }
            set
            {
                if (value != this._BankSlots)
                {
                    this._BankSlots = value;
                    this.NotifyPropertyChanged("BankSlots");
                }
            }
        }

        [ProtoMember(42, IsRequired = true)]
        public int NumChallengePrestiges
        {
            get { return this._NumChallengePrestiges; }
            set
            {
                if (value != this._NumChallengePrestiges)
                {
                    this._NumChallengePrestiges = value;
                    this.NotifyPropertyChanged("NumChallengePrestiges");
                }
            }
        }

        [ProtoMember(43, IsRequired = true)]
        public List<LockoutData> LockoutList
        {
            get { return this._LockoutList; }
            set
            {
                if (value != this._LockoutList)
                {
                    this._LockoutList = value;
                    this.NotifyPropertyChanged("LockoutList");
                }
            }
        }

        [ProtoMember(44, IsRequired = false)]
        public bool IsDLCPlayerClass
        {
            get { return this._IsDLCPlayerClass; }
            set
            {
                if (value != this._IsDLCPlayerClass)
                {
                    this._IsDLCPlayerClass = value;
                    this.NotifyPropertyChanged("IsDLCPlayerClass");
                }
            }
        }

        [ProtoMember(45, IsRequired = false)]
        public int DLCPlayerClassPackageId
        {
            get { return this._DLCPlayerClassPackageId; }
            set
            {
                if (value != this._DLCPlayerClassPackageId)
                {
                    this._DLCPlayerClassPackageId = value;
                    this.NotifyPropertyChanged("DLCPlayerClassPackageId");
                }
            }
        }

        [ProtoMember(46, IsRequired = false)]
        public List<string> FullyExploredAreas
        {
            get { return this._FullyExploredAreas; }
            set
            {
                if (value != this._FullyExploredAreas)
                {
                    this._FullyExploredAreas = value;
                    this.NotifyPropertyChanged("FullyExploredAreas");
                }
            }
        }

        // Deprecated?
        [ProtoMember(47, IsRequired = false)]
        public List<GoldenKeys> Unknown47
        {
            get { return this._Unknown47; }
            set
            {
                if (value != this._Unknown47)
                {
                    this._Unknown47 = value;
                    this.NotifyPropertyChanged("Unknown47");
                }
            }
        }

        [ProtoMember(48, IsRequired = true)]
        public int NumGoldenKeysNotified
        {
            get { return this._NumGoldenKeysNotified; }
            set
            {
                if (value != this._NumGoldenKeysNotified)
                {
                    this._NumGoldenKeysNotified = value;
                    this.NotifyPropertyChanged("NumGoldenKeysNotified");
                }
            }
        }

        [ProtoMember(49, IsRequired = true)]
        public int LastPlaythroughNumber
        {
            get { return this._LastPlaythroughNumber; }
            set
            {
                if (value != this._LastPlaythroughNumber)
                {
                    this._LastPlaythroughNumber = value;
                    this.NotifyPropertyChanged("LastPlaythroughNumber");
                }
            }
        }

        [ProtoMember(50, IsRequired = true)]
        public bool ShowNewPlaythroughNotification
        {
            get { return this._ShowNewPlaythroughNotification; }
            set
            {
                if (value != this._ShowNewPlaythroughNotification)
                {
                    this._ShowNewPlaythroughNotification = value;
                    this.NotifyPropertyChanged("ShowNewPlaythroughNotification");
                }
            }
        }

        [ProtoMember(51, IsRequired = true)]
        public bool ReceivedDefaultWeapon
        {
            get { return this._ReceivedDefaultWeapon; }
            set
            {
                if (value != this._ReceivedDefaultWeapon)
                {
                    this._ReceivedDefaultWeapon = value;
                    this.NotifyPropertyChanged("ReceivedDefaultWeapon");
                }
            }
        }

        [ProtoMember(52, IsRequired = true)]
        public List<string> QueuedTrainingMessages
        {
            get { return this._QueuedTrainingMessages; }
            set
            {
                if (value != this._QueuedTrainingMessages)
                {
                    this._QueuedTrainingMessages = value;
                    this.NotifyPropertyChanged("QueuedTrainingMessages");
                }
            }
        }

        [ProtoMember(53, IsRequired = true)]
        public List<PackedItemData> PackedItemData
        {
            get { return this._PackedItemData; }
            set
            {
                if (value != this._PackedItemData)
                {
                    this._PackedItemData = value;
                    this.NotifyPropertyChanged("PackedItemData");
                }
            }
        }

        [ProtoMember(54, IsRequired = true)]
        public List<PackedWeaponData> PackedWeaponData
        {
            get { return this._PackedWeaponData; }
            set
            {
                if (value != this._PackedWeaponData)
                {
                    this._PackedWeaponData = value;
                    this.NotifyPropertyChanged("PackedWeaponData");
                }
            }
        }

        [ProtoMember(55, IsRequired = true)]
        public bool AwesomeSkillDisabled
        {
            get { return this._AwesomeSkillDisabled; }
            set
            {
                if (value != this._AwesomeSkillDisabled)
                {
                    this._AwesomeSkillDisabled = value;
                    this.NotifyPropertyChanged("AwesomeSkillDisabled");
                }
            }
        }

        [ProtoMember(56, IsRequired = true)]
        public int MaxBankSlots
        {
            get { return this._MaxBankSlots; }
            set
            {
                if (value != this._MaxBankSlots)
                {
                    this._MaxBankSlots = value;
                    this.NotifyPropertyChanged("MaxBankSlots");
                }
            }
        }

        public int? ExtraShowNewPlaythroughNotification
        {
            get { return this._ExtraShowNewPlaythroughNotification; }
            set
            {
                if (value != this._ExtraShowNewPlaythroughNotification)
                {
                    this._ExtraShowNewPlaythroughNotification = value;
                    this.NotifyPropertyChanged("ExtraShowNewPlaythroughNotification");
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
