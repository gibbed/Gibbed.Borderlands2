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

using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Caliburn.Micro;
using Gibbed.Borderlands2.GameInfo;
using Gibbed.Borderlands2.ProtoBufFormats.WillowTwoSave;

namespace Gibbed.Borderlands2.SaveEdit
{
    [Export(typeof(FastTravelViewModel))]
    internal class FastTravelViewModel : PropertyChangedBase
    {
        #region Fields
        private string _LastVisitedTeleporter;
        #endregion

        #region Properties
        public string LastVisitedTeleporter
        {
            get { return this._LastVisitedTeleporter; }
            set
            {
                this._LastVisitedTeleporter = value;
                this.NotifyOfPropertyChange(() => this.LastVisitedTeleporter);
            }
        }

        public ObservableCollection<AssetDisplay> AvailableTeleporters { get; private set; }
        public ObservableCollection<VisitedTeleporterDisplay> VisitedTeleporters { get; private set; }
        #endregion

        [ImportingConstructor]
        public FastTravelViewModel()
        {
            this.AvailableTeleporters = new ObservableCollection<AssetDisplay>();
            this.AvailableTeleporters.Add(new AssetDisplay("None", "None", "Base Game"));

            this.VisitedTeleporters = new ObservableCollection<VisitedTeleporterDisplay>();

            var fastTravelStations = InfoManager.TravelStations.Items
                .Where(kv => kv.Value is FastTravelStationDefinition)
                .Select(kv => kv.Value)
                .Cast<FastTravelStationDefinition>()
                .ToList();

            var levelTravelStations = InfoManager.TravelStations.Items
                .Where(kv => kv.Value is LevelTravelStationDefinition)
                .Select(kv => kv.Value)
                .Cast<LevelTravelStationDefinition>()
                .ToList();

            foreach (var kv in InfoManager.FastTravelStationOrdering.Items)
            {
                string group = kv.Value.DLCExpansion == null ? "Base Game" : kv.Value.DLCExpansion.Package.DisplayName;
                foreach (var station in kv.Value.Stations)
                {
                    string displayName = string.IsNullOrEmpty(station.Sign) == false
                                             ? station.Sign
                                             : station.StationDisplayName;
                    this.AvailableTeleporters.Add(new AssetDisplay(displayName, station.ResourceName, group));
                    this.VisitedTeleporters.Add(new VisitedTeleporterDisplay()
                    {
                        DisplayName = displayName,
                        ResourceName = station.ResourceName,
                        Visited = false,
                        Custom = false,
                        Group = group,
                    });
                    fastTravelStations.Remove(station);
                }
            }

            foreach (var fastTravelStation in fastTravelStations)
            {
                string displayName = string.IsNullOrEmpty(fastTravelStation.Sign) == false
                                         ? fastTravelStation.Sign
                                         : fastTravelStation.StationDisplayName;
                var group = "Unknown";
                this.AvailableTeleporters.Add(new AssetDisplay(fastTravelStation.StationDisplayName,
                                                               fastTravelStation.ResourceName,
                                                               group));
                this.VisitedTeleporters.Add(new VisitedTeleporterDisplay()
                {
                    DisplayName = displayName,
                    ResourceName = fastTravelStation.ResourceName,
                    Visited = false,
                    Custom = false,
                    Group = group,
                });
            }

            foreach (var levelTravelStation in levelTravelStations.OrderBy(lts => lts.ResourceName))
            {
                var displayName = string.IsNullOrEmpty(levelTravelStation.DisplayName) == false
                                      ? levelTravelStation.DisplayName
                                      : levelTravelStation.ResourceName;
                this.AvailableTeleporters.Add(new AssetDisplay(displayName,
                                                               levelTravelStation.ResourceName,
                                                               "Level Transitions"));
            }
        }

        public void ImportData(WillowTwoPlayerSaveGame saveGame)
        {
            foreach (var teleporter in this.AvailableTeleporters.Where(t => t.Custom == true).ToList())
            {
                this.AvailableTeleporters.Remove(teleporter);
            }

            if (this.AvailableTeleporters.Any(t => t.Path == saveGame.LastVisitedTeleporter) == false)
            {
                this.AvailableTeleporters.Add(new AssetDisplay(saveGame.LastVisitedTeleporter,
                                                               saveGame.LastVisitedTeleporter,
                                                               "Unknown",
                                                               true));
            }

            this.LastVisitedTeleporter = saveGame.LastVisitedTeleporter;

            var visitedStations = saveGame.VisitedTeleporters.ToList();
            foreach (var travelStation in this.VisitedTeleporters.ToArray())
            {
                if (visitedStations.Contains(travelStation.ResourceName) == true)
                {
                    travelStation.Visited = true;
                    visitedStations.Remove(travelStation.ResourceName);
                }
                else if (travelStation.Custom == true)
                {
                    this.VisitedTeleporters.Remove(travelStation);
                }
                else
                {
                    travelStation.Visited = false;
                }
            }

            foreach (var visitedStation in visitedStations)
            {
                this.VisitedTeleporters.Add(new VisitedTeleporterDisplay()
                {
                    DisplayName = visitedStation,
                    ResourceName = visitedStation,
                    Visited = true,
                    Custom = true,
                    Group = "Unknown",
                });
            }
        }

        public void ExportData(WillowTwoPlayerSaveGame saveGame)
        {
            saveGame.LastVisitedTeleporter = this.LastVisitedTeleporter;
            saveGame.VisitedTeleporters.Clear();
            foreach (var travelStation in this.VisitedTeleporters)
            {
                if (travelStation.Visited == true)
                {
                    saveGame.VisitedTeleporters.Add(travelStation.ResourceName);
                }
            }
        }

        #region TravelStationDisplay
        public class VisitedTeleporterDisplay : PropertyChangedBase
        {
            #region Fields
            private string _DisplayName;
            private string _ResourceName;
            private bool _Visited;
            private bool _Custom;
            private string _Group;
            #endregion

            #region Properties
            public string DisplayName
            {
                get { return this._DisplayName; }
                set
                {
                    this._DisplayName = value;
                    this.NotifyOfPropertyChange(() => this.DisplayName);
                }
            }

            public string ResourceName
            {
                get { return this._ResourceName; }
                set
                {
                    this._ResourceName = value;
                    this.NotifyOfPropertyChange(() => this.ResourceName);
                }
            }

            public bool Visited
            {
                get { return this._Visited; }
                set
                {
                    this._Visited = value;
                    this.NotifyOfPropertyChange(() => this.Visited);
                }
            }

            public bool Custom
            {
                get { return this._Custom; }
                set
                {
                    this._Custom = value;
                    this.NotifyOfPropertyChange(() => this.Custom);
                }
            }

            public string Group
            {
                get { return this._Group; }
                set
                {
                    this._Group = value;
                    this.NotifyOfPropertyChange(() => this.Group);
                }
            }
            #endregion
        }
        #endregion
    }
}
