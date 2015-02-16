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
    public class WillowTwoPlayerSaveGame : INotifyPropertyChanged
    {
        #region Fields
        private string _PlayerClass;
        private int _ExpLevel = 1;
        private int _ExpPoints;
        private int _GeneralSkillPoints;
        private int _SpecialistSkillPoints;
        private List<int> _CurrencyOnHand = new List<int>();
        private int _PlaythroughsCompleted;
        private List<SkillData> _SkillData = new List<SkillData>();
        private List<int> _Unknown9 = new List<int>();
        private List<int> _Unknown10 = new List<int>();
        private List<ResourceData> _ResourceData = new List<ResourceData>();
        private List<ItemData> _ItemData = new List<ItemData>();
        private InventorySlotData _InventorySlotData;
        private List<WeaponData> _WeaponData = new List<WeaponData>();
        private byte[] _StatsData;
        private List<string> _VisitedTeleporters = new List<string>();
        private string _LastVisitedTeleporter;
        private List<MissionPlaythroughData> _MissionPlaythroughs = new List<MissionPlaythroughData>();
        private UIPreferencesData _UIPreferences;
        private int _SaveGameId;
        private int _PlotMissionNumber;
        private int _Unknown22;
        private List<int> _UsedMarketingCodes = new List<int>();
        private List<int> _MarketingCodesNeedingNotification = new List<int>();
        private int _TotalPlayTime;
        private string _LastSavedDate;
        private List<DLCExpansionData> _DLCExpansionData = new List<DLCExpansionData>();
        private List<string> _Unknown28 = new List<string>();
        private List<RegionGameStageData> _RegionGameStages = new List<RegionGameStageData>();
        private List<WorldDiscoveryData> _WorldDiscoveryList = new List<WorldDiscoveryData>();
        private bool _IsBadassModeSaveGame;
        private List<WeaponMemento> _WeaponMementos = new List<WeaponMemento>();
        private List<ItemMemento> _ItemMementos = new List<ItemMemento>();
        private Guid _SaveGuid;
        private List<string> _AppliedCustomizations = new List<string>();
        private List<int> _BlackMarketUpgrades = new List<int>();
        private int _ActiveMissionNumber;
        private List<ChallengeData> _ChallengeList = new List<ChallengeData>();
        private List<int> _LevelChallengeUnlocks = new List<int>();
        private List<OneOffLevelChallengeData> _OneOffLevelChallengeCompletion = new List<OneOffLevelChallengeData>();
        private List<BankSlot> _BankSlots = new List<BankSlot>();
        private int _NumChallengePrestiges;
        private List<LockoutData> _LockoutList = new List<LockoutData>();
        private bool _IsDLCPlayerClass;
        private int _DLCPlayerClassPackageId;
        private List<string> _FullyExploredAreas = new List<string>();
        private List<GoldenKeys> _Unknown47 = new List<GoldenKeys>();
        private int _NumGoldenKeysNotified;
        private int _LastPlaythroughNumber;
        private bool _ShowNewPlaythroughNotification;
        private bool _ReceivedDefaultWeapon;
        private List<string> _QueuedTrainingMessages = new List<string>();
        private List<PackedItemData> _PackedItemData = new List<PackedItemData>();
        private List<PackedWeaponData> _PackedWeaponData = new List<PackedWeaponData>();
        private bool _AwesomeSkillDisabled;
        private int _MaxBankSlots;
        private List<ChosenVehicleCustomization> _ChosenVehicleCustomizations = new List<ChosenVehicleCustomization>();
        private int? _PlayerHasPlayedInPlaythroughThree;
        private int? _NumOverpowerLevelsUnlocked;
        private int? _LastOverpowerChoice;
        #endregion

        #region Serialization
        [ProtoAfterDeserialization]
        // ReSharper disable UnusedMember.Local
        private void OnDeserialization()
            // ReSharper restore UnusedMember.Local
        {
            this._CurrencyOnHand = this._CurrencyOnHand ?? new List<int>();
            this._SkillData = this._SkillData ?? new List<SkillData>();
            this._Unknown9 = this._Unknown9 ?? new List<int>();
            this._Unknown10 = this._Unknown10 ?? new List<int>();
            this._ResourceData = this._ResourceData ?? new List<ResourceData>();
            this._ItemData = this._ItemData ?? new List<ItemData>();
            this._WeaponData = this._WeaponData ?? new List<WeaponData>();
            this._VisitedTeleporters = this._VisitedTeleporters ?? new List<string>();
            this._MissionPlaythroughs = this._MissionPlaythroughs ?? new List<MissionPlaythroughData>();
            this._UsedMarketingCodes = this._UsedMarketingCodes ?? new List<int>();
            this._MarketingCodesNeedingNotification = this._MarketingCodesNeedingNotification ?? new List<int>();
            this._DLCExpansionData = this._DLCExpansionData ?? new List<DLCExpansionData>();
            this._Unknown28 = this._Unknown28 ?? new List<string>();
            this._RegionGameStages = this._RegionGameStages ?? new List<RegionGameStageData>();
            this._WorldDiscoveryList = this._WorldDiscoveryList ?? new List<WorldDiscoveryData>();
            this._WeaponMementos = this._WeaponMementos ?? new List<WeaponMemento>();
            this._ItemMementos = this._ItemMementos ?? new List<ItemMemento>();
            this._AppliedCustomizations = this._AppliedCustomizations ?? new List<string>();
            this._BlackMarketUpgrades = this._BlackMarketUpgrades ?? new List<int>();
            this._ChallengeList = this._ChallengeList ?? new List<ChallengeData>();
            this._LevelChallengeUnlocks = this._LevelChallengeUnlocks ?? new List<int>();
            this._OneOffLevelChallengeCompletion = this._OneOffLevelChallengeCompletion ??
                                                   new List<OneOffLevelChallengeData>();
            this._BankSlots = this._BankSlots ?? new List<BankSlot>();
            this._LockoutList = this._LockoutList ?? new List<LockoutData>();
            this._FullyExploredAreas = this._FullyExploredAreas ?? new List<string>();
            this._Unknown47 = this._Unknown47 ?? new List<GoldenKeys>();
            this._QueuedTrainingMessages = this._QueuedTrainingMessages ?? new List<string>();
            this._PackedItemData = this._PackedItemData ?? new List<PackedItemData>();
            this._PackedWeaponData = this._PackedWeaponData ?? new List<PackedWeaponData>();
            this._ChosenVehicleCustomizations = this._ChosenVehicleCustomizations ??
                                                new List<ChosenVehicleCustomization>();
        }

        private bool ShouldSerializeCurrencyOnHand()
        {
            return this._CurrencyOnHand != null &&
                   this._CurrencyOnHand.Count > 0;
        }

        private bool ShouldSerializeSkillData()
        {
            return this._SkillData != null &&
                   this._SkillData.Count > 0;
        }

        private bool ShouldSerializeUnknown9()
        {
            return this._Unknown9 != null &&
                   this._Unknown9.Count > 0;
        }

        private bool ShouldSerializeUnknown10()
        {
            return this._Unknown10 != null &&
                   this._Unknown10.Count > 0;
        }

        private bool ShouldSerializeResourceData()
        {
            return this._ResourceData != null &&
                   this._ResourceData.Count > 0;
        }

        private bool ShouldSerializeItemData()
        {
            return this._ItemData != null &&
                   this._ItemData.Count > 0;
        }

        private bool ShouldSerializeWeaponData()
        {
            return this._WeaponData != null &&
                   this._WeaponData.Count > 0;
        }

        private bool ShouldSerializeVisitedTeleporters()
        {
            return this._VisitedTeleporters != null &&
                   this._VisitedTeleporters.Count > 0;
        }

        private bool ShouldSerializeMissionPlaythroughs()
        {
            return this._MissionPlaythroughs != null &&
                   this._MissionPlaythroughs.Count > 0;
        }

        private bool ShouldSerializeUsedMarketingCodes()
        {
            return this._UsedMarketingCodes != null &&
                   this._UsedMarketingCodes.Count > 0;
        }

        private bool ShouldSerializeMarketingCodesNeedingNotification()
        {
            return this._MarketingCodesNeedingNotification != null &&
                   this._MarketingCodesNeedingNotification.Count > 0;
        }

        private bool ShouldSerializeDLCExpansionData()
        {
            return this._DLCExpansionData != null &&
                   this._DLCExpansionData.Count > 0;
        }

        private bool ShouldSerializeUnknown28()
        {
            return this._Unknown28 != null &&
                   this._Unknown28.Count > 0;
        }

        private bool ShouldSerializeRegionGameStages()
        {
            return this._RegionGameStages != null &&
                   this._RegionGameStages.Count > 0;
        }

        private bool ShouldSerializeWorldDiscoveryList()
        {
            return this._WorldDiscoveryList != null &&
                   this._WorldDiscoveryList.Count > 0;
        }

        private bool ShouldSerializeWeaponMementos()
        {
            return this._WeaponMementos != null &&
                   this._WeaponMementos.Count > 0;
        }

        private bool ShouldSerializeItemMementos()
        {
            return this._ItemMementos != null &&
                   this._ItemMementos.Count > 0;
        }

        private bool ShouldSerializeAppliedCustomizations()
        {
            return this._AppliedCustomizations != null &&
                   this._AppliedCustomizations.Count > 0;
        }

        private bool ShouldSerializeBlackMarketUpgrades()
        {
            return this._BlackMarketUpgrades != null &&
                   this._BlackMarketUpgrades.Count > 0;
        }

        private bool ShouldSerializeChallengeList()
        {
            return this._ChallengeList != null &&
                   this._ChallengeList.Count > 0;
        }

        private bool ShouldSerializeLevelChallengeUnlocks()
        {
            return this._LevelChallengeUnlocks != null &&
                   this._LevelChallengeUnlocks.Count > 0;
        }

        private bool ShouldSerializeOneOffLevelChallengeCompletion()
        {
            return this._OneOffLevelChallengeCompletion != null &&
                   this._OneOffLevelChallengeCompletion.Count > 0;
        }

        private bool ShouldSerializeBankSlots()
        {
            return this._BankSlots != null &&
                   this._BankSlots.Count > 0;
        }

        private bool ShouldSerializeLockoutList()
        {
            return this._LockoutList != null &&
                   this._LockoutList.Count > 0;
        }

        private bool ShouldSerializeFullyExploredAreas()
        {
            return this._FullyExploredAreas != null &&
                   this._FullyExploredAreas.Count > 0;
        }

        private bool ShouldSerializeUnknown47()
        {
            return this._Unknown47 != null &&
                   this._Unknown47.Count > 0;
        }

        private bool ShouldSerializeQueuedTrainingMessages()
        {
            return this._QueuedTrainingMessages != null &&
                   this._QueuedTrainingMessages.Count > 0;
        }

        private bool ShouldSerializePackedItemData()
        {
            return this._PackedItemData != null &&
                   this._PackedItemData.Count > 0;
        }

        private bool ShouldSerializePackedWeaponData()
        {
            return this._PackedWeaponData != null &&
                   this._PackedWeaponData.Count > 0;
        }

        private bool ShouldSerializeChosenVehicleCustomizations()
        {
            return this._ChosenVehicleCustomizations != null &&
                   this._ChosenVehicleCustomizations.Count > 0;
        }
        #endregion

        public string Name
        {
            get { return "WillowTwoPlayerSaveGame"; }
        }

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
        public List<WeaponMemento> WeaponMementos
        {
            get { return this._WeaponMementos; }
            set
            {
                if (value != this._WeaponMementos)
                {
                    this._WeaponMementos = value;
                    this.NotifyPropertyChanged("WeaponMementos");
                }
            }
        }

        [ProtoMember(33, IsRequired = true)]
        public List<ItemMemento> ItemMementos
        {
            get { return this._ItemMementos; }
            set
            {
                if (value != this._ItemMementos)
                {
                    this._ItemMementos = value;
                    this.NotifyPropertyChanged("ItemMementos");
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

        [ProtoMember(57, IsRequired = false)]
        public List<ChosenVehicleCustomization> ChosenVehicleCustomizations
        {
            get { return this._ChosenVehicleCustomizations; }
            set
            {
                if (value != this._ChosenVehicleCustomizations)
                {
                    this._ChosenVehicleCustomizations = value;
                    this.NotifyPropertyChanged("ChosenVehicleCustomizations");
                }
            }
        }

        public int? PlayerHasPlayedInPlaythroughThree
        {
            get { return this._PlayerHasPlayedInPlaythroughThree; }
            set
            {
                if (value != this._PlayerHasPlayedInPlaythroughThree)
                {
                    this._PlayerHasPlayedInPlaythroughThree = value;
                    this.NotifyPropertyChanged("PlayerHasPlayedInPlaythroughThree");
                }
            }
        }

        public int? NumOverpowerLevelsUnlocked
        {
            get { return this._NumOverpowerLevelsUnlocked; }
            set
            {
                if (value != this._NumOverpowerLevelsUnlocked)
                {
                    this._NumOverpowerLevelsUnlocked = value;
                    this.NotifyPropertyChanged("NumOverpowerLevelsUnlocked");
                }
            }
        }

        public int? LastOverpowerChoice
        {
            get { return this._LastOverpowerChoice; }
            set
            {
                if (value != this._LastOverpowerChoice)
                {
                    this._LastOverpowerChoice = value;
                    this.NotifyPropertyChanged("LastOverpowerChoice");
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
