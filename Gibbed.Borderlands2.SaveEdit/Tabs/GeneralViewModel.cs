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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Gibbed.Borderlands2.GameInfo;
using Gibbed.Borderlands2.ProtoBufFormats.WillowTwoSave;
using Gibbed.IO;

namespace Gibbed.Borderlands2.SaveEdit
{
    [Export(typeof(GeneralViewModel))]
    internal class GeneralViewModel : PropertyChangedBase
    {
        #region Fields
        private Endian _Endian;
        private int _SaveGameId;
        private string _PlayerClassDefinition = "GD_Assassin.Character.CharClass_Assassin";
        private int _ExpLevel = 1;
        private int _ExpPoints;
        private int _GeneralSkillPoints;
        private int _SpecialistSkillPoints;
        private string _CharacterName = "Zer0";
        private string _SelectedHead;
        private string _SelectedSkin;
        #endregion

        #region Properties
        public Endian Endian
        {
            get { return this._Endian; }
            set
            {
                if (this._Endian != value)
                {
                    this._Endian = value;
                    this.NotifyOfPropertyChange(() => this.Endian);
                }
            }
        }

        public int SaveGameId
        {
            get { return this._SaveGameId; }
            set
            {
                this._SaveGameId = value;
                this.NotifyOfPropertyChange(() => this.SaveGameId);
            }
        }

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

        public ObservableCollection<EndianDisplay> Endians { get; private set; }
        public ObservableCollection<AssetDisplay> PlayerClasses { get; private set; }
        public ObservableCollection<AssetDisplay> HeadAssets { get; private set; }
        public ObservableCollection<AssetDisplay> SkinAssets { get; private set; }
        #endregion

        internal class EndianDisplay
        {
            public string Name { get; private set; }
            public Endian Value { get; private set; }

            public EndianDisplay(string name, Endian value)
            {
                this.Name = name;
                this.Value = value;
            }
        }

        private void UpdateClassDefinitions()
        {
            this.PlayerClasses = new ObservableCollection<AssetDisplay>(
                InfoManager.PlayerClasses.Items
                    .OrderBy(kv => kv.Value.SortOrder)
                    .Select(kv => GetPlayerClass(kv.Value)));
        }

        private static AssetDisplay GetPlayerClass(PlayerClassDefinition playerClass)
        {
            return new AssetDisplay(string.Format("{0} ({1})",
                                                  playerClass.Name,
                                                  playerClass.Class),
                                    playerClass.ResourcePath);
        }

        [ImportingConstructor]
        public GeneralViewModel()
        {
            this.Endians = new ObservableCollection<EndianDisplay>
            {
                new EndianDisplay("Little (PC)", Endian.Little),
                new EndianDisplay("Big (360, PS3)", Endian.Big)
            };

            this.UpdateClassDefinitions();

            this.HeadAssets = new ObservableCollection<AssetDisplay>();
            this.SkinAssets = new ObservableCollection<AssetDisplay>();

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

        private CustomizationUsage GetCustomizationUsage()
        {
            switch (this.PlayerClass)
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
            }

            return CustomizationUsage.Unknown;
        }

        private void BuildCustomizationAssets()
        {
            var usage = this.GetCustomizationUsage();

            var headAssets = new List<AssetDisplay>();
            foreach (
                var kv in
                    InfoManager.Customizations.Items.Where(
                        kv => kv.Value.Type == CustomizationType.Head && kv.Value.Usage.Contains(usage) == true).OrderBy
                        (cd => cd.Value.Name))
            {
                string group = "Base Game";

                if (kv.Value.DLC != null)
                {
                    if (kv.Value.DLC.Package != null)
                    {
                        group = kv.Value.DLC.Package.DisplayName;
                    }
                    else
                    {
                        group = "??? " + kv.Value.DLC.ResourcePath + " ???";
                    }
                }

                headAssets.Add(new AssetDisplay(kv.Value.Name, kv.Key, group));
            }

            var skinAssets = new List<AssetDisplay>();
            foreach (
                var kv in
                    InfoManager.Customizations.Items.Where(
                        kv => kv.Value.Type == CustomizationType.Skin && kv.Value.Usage.Contains(usage) == true).OrderBy
                        (cd => cd.Value.Name))
            {
                string group = "Base Game";

                if (kv.Value.DLC != null)
                {
                    if (kv.Value.DLC.Package != null)
                    {
                        group = kv.Value.DLC.Package.DisplayName;
                    }
                    else
                    {
                        group = "??? " + kv.Value.DLC.ResourcePath + " ???";
                    }
                }

                skinAssets.Add(new AssetDisplay(kv.Value.Name, kv.Key, group));
            }

            var selectedHead = this.SelectedHead;
            this.HeadAssets.Clear();
            headAssets.ForEach(a => this.HeadAssets.Add(a));
            this.SelectedHead = selectedHead;

            var selectedSkin = this.SelectedSkin;
            this.SkinAssets.Clear();
            skinAssets.ForEach(a => this.SkinAssets.Add(a));
            this.SelectedSkin = selectedSkin;
        }

        public void ImportData(WillowTwoPlayerSaveGame saveGame, Endian endian)
        {
            this.Endian = endian;
            this.SaveGameId = saveGame.SaveGameId;
            this.PlayerClass = saveGame.PlayerClassDefinition;
            this.ExpLevel = saveGame.ExpLevel;
            this.ExpPoints = saveGame.ExpPoints;
            this.GeneralSkillPoints = saveGame.GeneralSkillPoints;
            this.SpecialistSkillPoints = saveGame.SpecialistSkillPoints;
            this.CharacterName = Encoding.UTF8.GetString(saveGame.UIPreferences.CharacterName);
            this.SelectedHead = saveGame.AppliedCustomizations[0];
            this.SelectedSkin = saveGame.AppliedCustomizations[4];
            this.BuildCustomizationAssets();
        }

        public void ExportData(WillowTwoPlayerSaveGame saveGame, out Endian endian)
        {
            endian = this.Endian;
            saveGame.SaveGameId = this.SaveGameId;
            saveGame.PlayerClassDefinition = this.PlayerClass;
            saveGame.ExpLevel = this.ExpLevel;
            saveGame.ExpPoints = this.ExpPoints;
            saveGame.GeneralSkillPoints = this.GeneralSkillPoints;
            saveGame.SpecialistSkillPoints = this.SpecialistSkillPoints;
            saveGame.UIPreferences.CharacterName = Encoding.UTF8.GetBytes(this.CharacterName);
            saveGame.AppliedCustomizations[0] = this.SelectedHead;
            saveGame.AppliedCustomizations[4] = this.SelectedSkin;
        }
    }
}
