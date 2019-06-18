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
    internal static class FastTravelStationOrderingLoader
    {
        public static InfoDictionary<FastTravelStationOrdering> Load(
            InfoDictionary<TravelStationDefinition> stations,
            InfoDictionary<DownloadableContentDefinition> downloadableContents)
        {
            try
            {
                var raws = LoaderHelper.DeserializeDump<Dictionary<string, Raw.FastTravelStationOrdering>>(
                    "Fast Travel Station Ordering");
                return new InfoDictionary<FastTravelStationOrdering>(
                    raws.ToDictionary(
                        kv => kv.Key,
                        kv => CreateFastTravelStationOrdering(
                            stations,
                            downloadableContents,
                            kv)));
            }
            catch (Exception e)
            {
                throw new InfoLoadException("failed to load fast travel station ordering", e);
            }
        }

        private static FastTravelStationOrdering CreateFastTravelStationOrdering(
            InfoDictionary<TravelStationDefinition> stations,
            InfoDictionary<DownloadableContentDefinition> downloadableContents,
            KeyValuePair<string, Raw.FastTravelStationOrdering> kv)
        {
            var raw = kv.Value;

            DownloadableContentDefinition dlcExpansion = null;
            if (string.IsNullOrEmpty(raw.DLCExpansion) == false)
            {
                if (downloadableContents.TryGetValue(raw.DLCExpansion, out dlcExpansion) == false)
                {
                    throw ResourceNotFoundException.Create("downloadable content", kv.Value.DLCExpansion);
                }
            }

            return new FastTravelStationOrdering()
            {
                ResourcePath = kv.Key,
                Stations = GetStations(stations, raw.Stations),
                DLCExpansion = dlcExpansion,
            };
        }

        private static List<FastTravelStationDefinition> GetStations(
            InfoDictionary<TravelStationDefinition> stations,
            IEnumerable<string> paths)
        {
            if (paths == null)
            {
                return null;
            }

            return paths.Select(path =>
            {
                if (stations.TryGetValue(path, out var station) == false)
                {
                    throw ResourceNotFoundException.Create("fast travel station", path);
                }

                if (station is FastTravelStationDefinition fastTravelStation)
                {
                    return fastTravelStation;
                }

                throw new InvalidOperationException($"'{path}' is not a {nameof(FastTravelStationDefinition)}");
            }).ToList();
        }
    }
}
