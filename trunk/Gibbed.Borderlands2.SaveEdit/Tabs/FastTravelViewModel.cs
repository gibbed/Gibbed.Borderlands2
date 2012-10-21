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
            this.VisitedTeleporters = new ObservableCollection<VisitedTeleporterDisplay>();
            foreach (var kv in InfoManager.TravelStations.Items.OrderBy(kv => kv.Value.DisplayName))
            {
                if (kv.Value is FastTravelStationDefinition)
                {
                    var fastTravelStation = (FastTravelStationDefinition)kv.Value;
                    var displayName =
                        fastTravelStation.DisplayName = string.IsNullOrEmpty(fastTravelStation.Sign) == true
                                                            ? fastTravelStation.DisplayName
                                                            : fastTravelStation.Sign;
                    var resourceName = fastTravelStation.ResourceName;

                    this.AvailableTeleporters.Add(new AssetDisplay(displayName, resourceName));
                    this.VisitedTeleporters.Add(new VisitedTeleporterDisplay()
                    {
                        DisplayName = displayName,
                        ResourceName = resourceName,
                        Visited = false,
                        Custom = false,
                    });
                }
            }
        }

        public void ImportData(WillowTwoPlayerSaveGame saveGame)
        {
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
            #endregion
        }
        #endregion
    }
}
