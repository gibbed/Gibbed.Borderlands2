/* Copyright (c) 2013 Rick (rick 'at' gibbed 'dot' us)
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
using Caliburn.Micro;
using Gibbed.Borderlands2.GameInfo;
using Gibbed.Borderlands2.ProtoBufFormats.WillowTwoSave;

namespace Gibbed.Borderlands2.SaveEdit
{
    [Export(typeof(VehicleViewModel))]
    internal class VehicleViewModel : PropertyChangedBase
    {
        #region Fields
        private string _SelectedRunner1 = "None";
        private string _SelectedRunner2 = "None";
        private readonly List<string> _ExtraRunner = new List<string>();

        private string _SelectedBanditTech1 = "None";
        private string _SelectedBanditTech2 = "None";
        private readonly List<string> _ExtraBanditTech = new List<string>();

        private string _SelectedHovercraft1 = "None";
        private string _SelectedHovercraft2 = "None";
        private readonly List<string> _ExtraHovercraft = new List<string>();

        private string _SelectedFanBoat1 = "None";
        private string _SelectedFanBoat2 = "None";
        private readonly List<string> _ExtraFanBoat = new List<string>();
        #endregion

        #region Properties
        public string SelectedRunner1
        {
            get { return this._SelectedRunner1; }
            set
            {
                this._SelectedRunner1 = value;
                this.NotifyOfPropertyChange(() => this.SelectedRunner1);
            }
        }

        public string SelectedRunner2
        {
            get { return this._SelectedRunner2; }
            set
            {
                this._SelectedRunner2 = value;
                this.NotifyOfPropertyChange(() => this.SelectedRunner2);
            }
        }

        public List<string> ExtraRunner
        {
            get { return this._ExtraRunner; }
        }

        public string SelectedBanditTech1
        {
            get { return this._SelectedBanditTech1; }
            set
            {
                this._SelectedBanditTech1 = value;
                this.NotifyOfPropertyChange(() => this.SelectedBanditTech1);
            }
        }

        public string SelectedBanditTech2
        {
            get { return this._SelectedBanditTech2; }
            set
            {
                this._SelectedBanditTech2 = value;
                this.NotifyOfPropertyChange(() => this.SelectedBanditTech2);
            }
        }

        public List<string> ExtraBanditTech
        {
            get { return this._ExtraBanditTech; }
        }

        public string SelectedHovercraft1
        {
            get { return this._SelectedHovercraft1; }
            set
            {
                this._SelectedHovercraft1 = value;
                this.NotifyOfPropertyChange(() => this.SelectedHovercraft1);
            }
        }

        public string SelectedHovercraft2
        {
            get { return this._SelectedHovercraft2; }
            set
            {
                this._SelectedHovercraft2 = value;
                this.NotifyOfPropertyChange(() => this.SelectedHovercraft2);
            }
        }

        public List<string> ExtraHovercraft
        {
            get { return this._ExtraHovercraft; }
        }

        public string SelectedFanBoat1
        {
            get { return this._SelectedFanBoat1; }
            set
            {
                this._SelectedFanBoat1 = value;
                this.NotifyOfPropertyChange(() => this.SelectedFanBoat1);
            }
        }

        public string SelectedFanBoat2
        {
            get { return this._SelectedFanBoat2; }
            set
            {
                this._SelectedFanBoat2 = value;
                this.NotifyOfPropertyChange(() => this.SelectedFanBoat2);
            }
        }

        public List<string> ExtraFanBoat
        {
            get { return this._ExtraFanBoat; }
        }

        public ObservableCollection<AssetDisplay> RunnerAssets { get; private set; }
        public ObservableCollection<AssetDisplay> BanditTechAssets { get; private set; }
        public ObservableCollection<AssetDisplay> HovercraftAssets { get; private set; }
        public ObservableCollection<AssetDisplay> FanBoatAssets { get; private set; }
        #endregion

        [ImportingConstructor]
        public VehicleViewModel()
        {
            this.RunnerAssets = new ObservableCollection<AssetDisplay>();
            this.BanditTechAssets = new ObservableCollection<AssetDisplay>();
            this.HovercraftAssets = new ObservableCollection<AssetDisplay>();
            this.FanBoatAssets = new ObservableCollection<AssetDisplay>();

            BuildCustomizationAssets(CustomizationUsage.Runner, this.RunnerAssets);
            BuildCustomizationAssets(CustomizationUsage.BanditTech, this.BanditTechAssets);
            BuildCustomizationAssets(CustomizationUsage.Hovercraft, this.HovercraftAssets);
            BuildCustomizationAssets(CustomizationUsage.FanBoat, this.FanBoatAssets);
        }

        private static void BuildCustomizationAssets(CustomizationUsage usage, ObservableCollection<AssetDisplay> target)
        {
            target.Clear();
            target.Add(new AssetDisplay("None", "None", "Base Game"));
            var assets = new List<KeyValuePair<AssetDisplay, int>>();
            foreach (var kv in
                InfoManager.Customizations.Items.Where(
                    kv => kv.Value.Type == CustomizationType.Skin && kv.Value.Usage.Contains(usage) == true).OrderBy
                    (cd => cd.Value.Name))
            {
                string group;
                int priority;

                if (kv.Value.DLC != null)
                {
                    if (kv.Value.DLC.Package != null)
                    {
                        group = kv.Value.DLC.Package.DisplayName;
                        priority = kv.Value.DLC.Package.Id;
                    }
                    else
                    {
                        group = "??? " + kv.Value.DLC.ResourcePath + " ???";
                        priority = int.MaxValue;
                    }
                }
                else
                {
                    group = "Base Game";
                    priority = int.MinValue;
                }

                assets.Add(new KeyValuePair<AssetDisplay, int>(new AssetDisplay(kv.Value.Name, kv.Key, group),
                                                               priority));
            }
            assets.OrderBy(kv => kv.Value).Apply(kv => target.Add(kv.Key));
        }

        private static void ImportTarget(string name,
                                         IEnumerable<ChosenVehicleCustomization> customizations,
                                         Action<string> skin1,
                                         Action<string> skin2,
                                         Action<string> extra)
        {
            skin1("None");
            skin2("None");

            var customization = customizations.FirstOrDefault(c => c.Family == name);
            if (customization != null &&
                customization.Customizations != null)
            {
                if (customization.Customizations.Count > 0)
                {
                    skin1(customization.Customizations[0]);
                }

                if (customization.Customizations.Count > 1)
                {
                    skin2(customization.Customizations[1]);
                }

                if (customization.Customizations.Count > 2)
                {
                    customization.Customizations.Skip(2).Apply(extra);
                }
            }
        }

        public void ImportData(WillowTwoPlayerSaveGame saveGame)
        {
            this.ExtraRunner.Clear();
            ImportTarget("GD_Globals.VehicleSpawnStation.VehicleFamily_Runner",
                         saveGame.ChosenVehicleCustomizations,
                         s => this.SelectedRunner1 = s,
                         s => this.SelectedRunner2 = s,
                         s => this.ExtraRunner.Add(s));

            this.ExtraBanditTech.Clear();
            ImportTarget("GD_Globals.VehicleSpawnStation.VehicleFamily_BanditTechnical",
                         saveGame.ChosenVehicleCustomizations,
                         s => this.SelectedBanditTech1 = s,
                         s => this.SelectedBanditTech2 = s,
                         s => this.ExtraBanditTech.Add(s));

            this.ExtraBanditTech.Clear();
            ImportTarget("GD_OrchidPackageDef.Vehicles.VehicleFamily_Hovercraft",
                         saveGame.ChosenVehicleCustomizations,
                         s => this.SelectedHovercraft1 = s,
                         s => this.SelectedHovercraft2 = s,
                         s => this.ExtraHovercraft.Add(s));

            this.ExtraBanditTech.Clear();
            ImportTarget("GD_SagePackageDef.Vehicles.VehicleFamily_FanBoat",
                         saveGame.ChosenVehicleCustomizations,
                         s => this.SelectedFanBoat1 = s,
                         s => this.SelectedFanBoat2 = s,
                         s => this.ExtraFanBoat.Add(s));
        }

        private static void ExportTarget(string name,
                                         List<ChosenVehicleCustomization> customizations,
                                         string skin1,
                                         string skin2,
                                         IEnumerable<string> extras)
        {
            customizations.RemoveAll(c => c.Family == name);

            var skins = extras.ToArray();
            if (skin1 != "None" ||
                skin2 != "None" ||
                skins.Length > 0)
            {
                var customization = new ChosenVehicleCustomization()
                {
                    Family = name,
                };
                customization.Customizations.Add(skin1);
                customization.Customizations.Add(skin2);
                customization.Customizations.AddRange(skins);
            }
        }

        public void ExportData(WillowTwoPlayerSaveGame saveGame)
        {
            ExportTarget("GD_Globals.VehicleSpawnStation.VehicleFamily_Runner",
                         saveGame.ChosenVehicleCustomizations,
                         this.SelectedRunner1,
                         this.SelectedRunner2,
                         this.ExtraRunner);
            ExportTarget("GD_Globals.VehicleSpawnStation.VehicleFamily_BanditTechnical",
                         saveGame.ChosenVehicleCustomizations,
                         this.SelectedBanditTech1,
                         this.SelectedBanditTech2,
                         this.ExtraBanditTech);

            ExportTarget("GD_OrchidPackageDef.Vehicles.VehicleFamily_Hovercraft",
                         saveGame.ChosenVehicleCustomizations,
                         this.SelectedHovercraft1,
                         this.SelectedHovercraft2,
                         this.ExtraHovercraft);

            ExportTarget("GD_SagePackageDef.Vehicles.VehicleFamily_FanBoat",
                         saveGame.ChosenVehicleCustomizations,
                         this.SelectedFanBoat1,
                         this.SelectedFanBoat2,
                         this.ExtraFanBoat);
        }
    }
}
