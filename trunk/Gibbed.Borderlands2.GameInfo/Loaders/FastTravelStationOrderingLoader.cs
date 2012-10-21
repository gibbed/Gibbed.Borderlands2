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
using System.Linq;

namespace Gibbed.Borderlands2.GameInfo.Loaders
{
    internal static class FastTravelStationOrderingLoader
    {
        public static InfoDictionary<FastTravelStationOrdering> Load(
            InfoDictionary<TravelStationDefinition> travelStations,
            InfoDictionary<DownloadableContentDefinition> downloadableContents)
        {
            try
            {
                var raws = LoaderHelper
                    .DeserializeJson<Dictionary<string, Raw.FastTravelStationOrdering>>("Fast Travel Station Ordering");
                return new InfoDictionary<FastTravelStationOrdering>(
                    raws.ToDictionary(kv => kv.Key,
                                      kv => GetFastTravelStationOrdering(travelStations, downloadableContents, kv)));
            }
            catch (Exception e)
            {
                throw new InfoLoadException("failed to load fast travel station ordering", e);
            }
        }

        private static FastTravelStationOrdering GetFastTravelStationOrdering(
            InfoDictionary<TravelStationDefinition> travelStations,
            InfoDictionary<DownloadableContentDefinition> downloadableContents,
            KeyValuePair<string, Raw.FastTravelStationOrdering> kv)
        {
            DownloadableContentDefinition dlcExpansion = null;
            if (string.IsNullOrEmpty(kv.Value.DLCExpansion) == false)
            {
                if (downloadableContents.ContainsKey(kv.Value.DLCExpansion) == false)
                {
                    throw ResourceNotFoundException.Create("downloadable content", kv.Value.DLCExpansion);
                }
                dlcExpansion = downloadableContents[kv.Value.DLCExpansion];
            }

            return new FastTravelStationOrdering()
            {
                ResourcePath = kv.Key,
                Stations = GetStations(travelStations, kv.Value.Stations),
                DLCExpansion = dlcExpansion,
            };
        }

        private static List<FastTravelStationDefinition> GetStations(
            InfoDictionary<TravelStationDefinition> travelStations, List<string> raws)
        {
            if (raws == null)
            {
                return null;
            }

            return raws.Select(raw =>
            {
                if (travelStations.ContainsKey(raw) == false)
                {
                    throw ResourceNotFoundException.Create("fast travel station", raw);
                }

                var travelStation = travelStations[raw];
                if ((travelStation is FastTravelStationDefinition) == false)
                {
                    throw new InvalidOperationException(string.Format("'{0}' is not a FastTravelStationDefinition", raw));
                }

                return (FastTravelStationDefinition)travelStation;
            }).ToList();
        }
    }
}
