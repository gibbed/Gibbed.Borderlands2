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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Gibbed.Borderlands2.GameInfo;
using Gibbed.Borderlands2.ProtoBufFormats.WillowTwoSave;

namespace Gibbed.Borderlands2.SaveEdit
{
    [Export(typeof(CharacterViewModel))]
    internal class CharacterViewModel : PropertyChangedBase
    {
        #region Imports
        private ShellViewModel _Shell;

        [Import(typeof(ShellViewModel))]
        public ShellViewModel Shell
        {
            get { return this._Shell; }
            set
            {
                this._Shell = value;
                this.NotifyOfPropertyChange(() => this.Shell);
            }
        }
        #endregion

        #region Fields
        private string _PlayerClassDefinition = "GD_Assassin.Character.CharClass_Assassin";
        private int _ExpLevel = 1;
        private int _ExpPoints;
        private int _GeneralSkillPoints;
        private int _SpecialistSkillPoints;
        private int _OverpowerLevel;
        private string _CharacterName = "Zer0";
        private string _SelectedHead;
        private string _SelectedSkin;
        #endregion

        #region Properties
        public string PlayerClass
        {
            get { return this._PlayerClassDefinition; }
            set
            {
                if (this._PlayerClassDefinition != value)
                {
                    this._PlayerClassDefinition = value;
                    this.NotifyOfPropertyChange(() => this.PlayerClass);
                    this.BuildCustomizationAssets();
                }
            }
        }

        public int ExpLevel
        {
            get { return this._ExpLevel; }
            set
            {
                this._ExpLevel = value;
                this.NotifyOfPropertyChange(() => this.ExpLevel);
            }
        }

        public int ExpPoints
        {
            get { return this._ExpPoints; }
            set
            {
                this._ExpPoints = value;
                this.NotifyOfPropertyChange(() => this.ExpPoints);
            }
        }

        public int OverpowerLevel
        {
            get { return this._OverpowerLevel; }
            set
            {
                this._OverpowerLevel = value;
                this.NotifyOfPropertyChange(() => this.OverpowerLevel);
            }
        }

        public int SyncLevel
        {
            get { return Math.Min(Math.Max(0, this.ExpLevel + this.OverpowerLevel), 127); }
        }

        public int GeneralSkillPoints
        {
            get { return this._GeneralSkillPoints; }
            set
            {
                this._GeneralSkillPoints = value;
                this.NotifyOfPropertyChange(() => this.GeneralSkillPoints);
            }
        }

        public int SpecialistSkillPoints
        {
            get { return this._SpecialistSkillPoints; }
            set
            {
                this._SpecialistSkillPoints = value;
                this.NotifyOfPropertyChange(() => this.SpecialistSkillPoints);
            }
        }

        public string CharacterName
        {
            get { return this._CharacterName; }
            set
            {
                this._CharacterName = value;
                this.NotifyOfPropertyChange(() => this.CharacterName);
            }
        }

        public string SelectedHead
        {
            get { return this._SelectedHead; }
            set
            {
                this._SelectedHead = value;
                this.NotifyOfPropertyChange(() => this.SelectedHead);
            }
        }

        public string SelectedSkin
        {
            get { return this._SelectedSkin; }
            set
            {
                this._SelectedSkin = value;
                this.NotifyOfPropertyChange(() => this.SelectedSkin);
            }
        }

        public ObservableCollection<AssetDisplay> PlayerClasses { get; private set; }
        public ObservableCollection<AssetDisplay> HeadAssets { get; private set; }
        public ObservableCollection<AssetDisplay> SkinAssets { get; private set; }
        #endregion

        private static string GetDLCName(DownloadableContentDefinition dlc, out int priority)
        {
            if (dlc == null)
            {
                priority = int.MinValue;
                return "Base Game";
            }

            if (dlc.Package != null)
            {
                priority = dlc.Package.Id;
                return dlc.Package.DisplayName;
            }

            priority = int.MaxValue;
            return "??? " + dlc.ResourcePath + " ???";
        }

        private void BuildPlayerClasses()
        {
            var playerClasses = new List<KeyValuePair<AssetDisplay, int>>();
            foreach (var kv in InfoManager.PlayerClasses.Items.OrderBy(kv => kv.Value.SortOrder))
            {
                int priority;
                var group = GetDLCName(kv.Value.DLC, out priority);
                playerClasses.Add(
                    new KeyValuePair<AssetDisplay, int>(
                        new AssetDisplay(string.Format("{0} ({1})", kv.Value.Name, kv.Value.Class), kv.Key, group),
                        priority));
            }

            this.PlayerClasses.Clear();
            playerClasses.OrderBy(kv => kv.Value).Apply(kv => this.PlayerClasses.Add(kv.Key));
        }

        [ImportingConstructor]
        public CharacterViewModel()
        {
            this.PlayerClasses = new ObservableCollection<AssetDisplay>();
            this.HeadAssets = new ObservableCollection<AssetDisplay>();
            this.SkinAssets = new ObservableCollection<AssetDisplay>();

            this.BuildPlayerClasses();
            this.BuildCustomizationAssets();

            var firstHead = this.HeadAssets.FirstOrDefault();
            if (firstHead != null)
            {
                this.SelectedHead = firstHead.Path;
            }

            var firstSkin = this.SkinAssets.FirstOrDefault();
            if (firstSkin != null)
            {
                this.SelectedSkin = firstSkin.Path;
            }
        }

        public static CustomizationUsage GetCustomizationUsage(string playerClass)
        {
            switch (playerClass)
            {
                case "GD_Soldier.Character.CharClass_Soldier":
                {
                    return CustomizationUsage.Soldier;
                }

                case "GD_Assassin.Character.CharClass_Assassin":
                {
                    return CustomizationUsage.Assassin;
                }

                case "GD_Siren.Character.CharClass_Siren":
                {
                    return CustomizationUsage.Siren;
                }

                case "GD_Mercenary.Character.CharClass_Mercenary":
                {
                    return CustomizationUsage.Mercenary;
                }

                case "GD_Tulip_Mechromancer.Character.CharClass_Mechromancer":
                {
                    return CustomizationUsage.Mechromancer;
                }

                case "GD_Lilac_PlayerClass.Character.CharClass_LilacPlayerClass":
                {
                    return CustomizationUsage.Psycho;
                }
            }

            return CustomizationUsage.Unknown;
        }

        private CustomizationUsage GetCustomizationUsage()
        {
            return GetCustomizationUsage(this.PlayerClass);
        }

        private void BuildCustomizationAssets()
        {
            var usage = this.GetCustomizationUsage();

            var headAssets = new List<KeyValuePair<AssetDisplay, int>>();
            foreach (var kv in InfoManager.Customizations.Items
                                          .Where(kv => kv.Value.Type == CustomizationType.Head &&
                                                       kv.Value.Usage.Contains(usage) == true)
                                          .OrderBy(cd => cd.Value.Name))
            {
                int priority;
                var group = GetDLCName(kv.Value.DLC, out priority);
                headAssets.Add(new KeyValuePair<AssetDisplay, int>(new AssetDisplay(kv.Value.Name, kv.Key, group),
                                                                   priority));
            }

            var skinAssets = new List<KeyValuePair<AssetDisplay, int>>();
            foreach (var kv in InfoManager.Customizations.Items
                                          .Where(kv => kv.Value.Type == CustomizationType.Skin &&
                                                       kv.Value.Usage.Contains(usage) == true)
                                          .OrderBy(cd => cd.Value.Name))
            {
                int priority;
                var group = GetDLCName(kv.Value.DLC, out priority);
                skinAssets.Add(new KeyValuePair<AssetDisplay, int>(new AssetDisplay(kv.Value.Name, kv.Key, group),
                                                                   priority));
            }

            var selectedHead = this.SelectedHead;
            this.HeadAssets.Clear();
            this.HeadAssets.Add(new AssetDisplay("None", "None", "Base Game"));
            headAssets.OrderBy(kv => kv.Value).Apply(kv => this.HeadAssets.Add(kv.Key));
            this.SelectedHead = selectedHead;

            var selectedSkin = this.SelectedSkin;
            this.SkinAssets.Clear();
            this.SkinAssets.Add(new AssetDisplay("None", "None", "Base Game"));
            skinAssets.OrderBy(kv => kv.Value).Apply(kv => this.SkinAssets.Add(kv.Key));
            this.SelectedSkin = selectedSkin;
        }


        public void DoSynchronizeExpLevel()
        {
            this.ExpLevel = Experience.GetLevelForPoints(this.ExpPoints);
        }

        public void DoSynchronizeExpPoints()
        {
            var minimum = Experience.GetPointsForLevel(this.ExpLevel + 0);
            var maximum = Experience.GetPointsForLevel(this.ExpLevel + 1) - 1;

            if (this.ExpPoints < minimum)
            {
                this.ExpPoints = minimum;
            }
            else if (this.ExpPoints > maximum)
            {
                this.ExpPoints = maximum;
            }
        }

        public void ImportData(WillowTwoPlayerSaveGame saveGame)
        {
            this.PlayerClass = saveGame.PlayerClass;

            var expLevel = saveGame.ExpLevel;
            var expPoints = saveGame.ExpPoints;

            if (expPoints < 0)
            {
                expPoints = 0;
            }

            if (expLevel <= 0)
            {
                expLevel = Math.Max(1, Experience.GetLevelForPoints(expPoints));
            }

            this.ExpLevel = expLevel;
            this.ExpPoints = expPoints;
            this.OverpowerLevel = saveGame.NumOverpowerLevelsUnlocked.HasValue == false
                                      ? 0
                                      : saveGame.NumOverpowerLevelsUnlocked.Value;
            this.GeneralSkillPoints = saveGame.GeneralSkillPoints;
            this.SpecialistSkillPoints = saveGame.SpecialistSkillPoints;
            this.CharacterName = Encoding.UTF8.GetString(saveGame.UIPreferences.CharacterName);
            this.SelectedHead = saveGame.AppliedCustomizations.Count > 0 ? saveGame.AppliedCustomizations[0] : "None";
            this.SelectedSkin = saveGame.AppliedCustomizations.Count > 4 ? saveGame.AppliedCustomizations[4] : "None";
            this.BuildCustomizationAssets();
        }

        public void ExportData(WillowTwoPlayerSaveGame saveGame)
        {
            saveGame.PlayerClass = this.PlayerClass;

            var playerClass = InfoManager.PlayerClasses.Items.FirstOrDefault(p => p.Key == this.PlayerClass).Value;
            if (playerClass != null && playerClass.DLC != null)
            {
                saveGame.IsDLCPlayerClass = true;
                saveGame.DLCPlayerClassPackageId = playerClass.DLC.Package.Id;
            }
            else
            {
                saveGame.IsDLCPlayerClass = false;
                saveGame.DLCPlayerClassPackageId = 0;
            }

            saveGame.ExpLevel = this.ExpLevel;
            saveGame.ExpPoints = this.ExpPoints;
            saveGame.NumOverpowerLevelsUnlocked = this.OverpowerLevel <= 0 ? (int?)null : this.OverpowerLevel;
            saveGame.GeneralSkillPoints = this.GeneralSkillPoints;
            saveGame.SpecialistSkillPoints = this.SpecialistSkillPoints;
            saveGame.UIPreferences.CharacterName = Encoding.UTF8.GetBytes(this.CharacterName);

            while (saveGame.AppliedCustomizations.Count < 5)
            {
                saveGame.AppliedCustomizations.Add("");
            }

            saveGame.AppliedCustomizations[0] = this.SelectedHead;
            saveGame.AppliedCustomizations[4] = this.SelectedSkin;
        }
    }
}
