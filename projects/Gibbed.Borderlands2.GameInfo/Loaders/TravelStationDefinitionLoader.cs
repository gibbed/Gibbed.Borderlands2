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

using System;
using System.Collections.Generic;
using System.Linq;

namespace Gibbed.Borderlands2.GameInfo.Loaders
{
    internal static class TravelStationDefinitionLoader
    {
        public static InfoDictionary<TravelStationDefinition> Load(
            InfoDictionary<DownloadableContentDefinition> downloadableContents)
        {
            try
            {
                var raws = LoaderHelper.DeserializeDump<Dictionary<string, Raw.TravelStationDefinition>>(
                    "Travel Stations");
                var stations = new InfoDictionary<TravelStationDefinition>(
                    raws.ToDictionary(
                        kv => kv.Key,
                        kv => CreateTravelStation(downloadableContents, kv)));

                foreach (var kv in raws)
                {
                    var stationPath = kv.Value.PreviousStation;
                    if (string.IsNullOrEmpty(stationPath) == true)
                    {
                        continue;
                    }
                    if (stations.TryGetValue(stationPath, out var station) == false)
                    {
                        throw ResourceNotFoundException.Create("travel station", stationPath);
                    }
                    stations[kv.Key].PreviousStation = station;
                }

                foreach (var kv in raws.Where(kv => kv.Value is Raw.LevelTravelStationDefinition))
                {
                    var raw = (Raw.LevelTravelStationDefinition)kv.Value;
                    if (string.IsNullOrEmpty(raw.DestinationStation) == true)
                    {
                        continue;
                    }
                    if (stations.TryGetValue(raw.DestinationStation, out var destination) == false)
                    {
                        throw ResourceNotFoundException.Create("level travel station", raw.DestinationStation);
                    }
                    var station = (LevelTravelStationDefinition)stations[kv.Key];
                    station.DestinationStation = (LevelTravelStationDefinition)destination;
                }
                return stations;
            }
            catch (Exception e)
            {
                throw new InfoLoadException("failed to load travel stations", e);
            }
        }

        private static TravelStationDefinition CreateTravelStation(
            InfoDictionary<DownloadableContentDefinition> downloadableContents,
            KeyValuePair<string, Raw.TravelStationDefinition> kv)
        {
            var raw = kv.Value;

            if (raw is Raw.FastTravelStationDefinition rawFastTravelStation)
            {
                return CreateFastTravelStation(downloadableContents, kv.Key, rawFastTravelStation);
            }

            if (raw is Raw.LevelTravelStationDefinition rawLevelTravelStation)
            {
                return CreateLevelTravelStation(downloadableContents, kv.Key, rawLevelTravelStation);
            }

            throw new NotSupportedException($"unsupported type '{raw.GetType()}'");
        }

        private static TravelStationDefinition CreateFastTravelStation(
            InfoDictionary<DownloadableContentDefinition> downloadableContents,
            string path,
            Raw.FastTravelStationDefinition raw)
        {
            DownloadableContentDefinition dlcExpansion = null;
            if (string.IsNullOrEmpty(raw.DLCExpansion) == false)
            {
                if (downloadableContents.TryGetValue(raw.DLCExpansion, out dlcExpansion) == false)
                {
                    throw ResourceNotFoundException.Create("downloadable content", raw.DLCExpansion);
                }
            }
            return new FastTravelStationDefinition()
            {
                ResourcePath = path,
                ResourceName = raw.ResourceName,
                LevelName = raw.LevelName,
                DLCExpansion = dlcExpansion,
                StationDisplayName = raw.StationDisplayName,
                MissionDependencies = CreateMissionStatusData(raw.MissionDependencies),
                InitiallyActive = raw.InitiallyActive,
                SendOnly = raw.SendOnly,
                Description = raw.Description,
                Sign = raw.Sign,
                InaccessibleObjective = raw.InaccessibleObjective,
                AccessibleObjective = raw.AccessibleObjective,
            };
        }

        private static TravelStationDefinition CreateLevelTravelStation(
            InfoDictionary<DownloadableContentDefinition> downloadableContents,
            string path,
            Raw.LevelTravelStationDefinition raw)
        {
            DownloadableContentDefinition dlcExpansion = null;
            if (string.IsNullOrEmpty(raw.DLCExpansion) == false)
            {
                if (downloadableContents.TryGetValue(raw.DLCExpansion, out dlcExpansion) == false)
                {
                    throw ResourceNotFoundException.Create("downloadable content", raw.DLCExpansion);
                }
            }
            return new LevelTravelStationDefinition()
            {
                ResourcePath = path,
                ResourceName = raw.ResourceName,
                LevelName = raw.LevelName,
                DLCExpansion = dlcExpansion,
                StationDisplayName = raw.StationDisplayName,
                MissionDependencies = CreateMissionStatusData(raw.MissionDependencies),
            };
        }

        private static List<MissionStatusData> CreateMissionStatusData(IEnumerable<Raw.MissionStatusData> raws)
        {
            if (raws == null)
            {
                return null;
            }
            return raws.Select(raw => new MissionStatusData()
            {
                MissionDefinition = raw.MissionDefinition,
                MissionStatus = raw.MissionStatus,
                IsObjectiveSpecific = raw.IsObjectiveSpecific,
                ObjectiveDefinition = raw.ObjectiveDefinition,
                ObjectiveStatus = raw.ObjectiveStatus,
            }).ToList();
        }
    }
}
