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
using System.Linq;

namespace Gibbed.Borderlands2.GameInfo.Loaders
{
    internal static class PlatformConfigurationLoader
    {
        public static InfoDictionary<Platform, PlatformConfiguration> Load()
        {
            try
            {
                var raws = LoaderHelper
                    .DeserializeJson<Dictionary<Platform, Raw.PlatformConfiguration>>("Platform Configurations");
                return new InfoDictionary<Platform, PlatformConfiguration>(
                    raws.ToDictionary(kv => kv.Key,
                                      GetPlatformConfiguration));
            }
            catch (Exception e)
            {
                throw new InfoLoadException("failed to load platform configurations", e);
            }
        }

        private static PlatformConfiguration GetPlatformConfiguration(
            KeyValuePair<Platform, Raw.PlatformConfiguration> kv)
        {
            var raw = kv.Value;
            return new PlatformConfiguration()
            {
                Platform = kv.Key,
                AssetLibrarySets = raw.AssetLibrarySets.Select(GetPlatformAssetLibrarySet).ToList(),
            };
        }

        private static PlatformAssetLibrarySet GetPlatformAssetLibrarySet(Raw.PlatformAssetLibrarySet raw)
        {
            return new PlatformAssetLibrarySet()
            {
                Id = raw.Id,
                Libraries = raw.Libraries.ToDictionary(kv => kv.Key, GetPlatformAssetLibraryConfiguration),
            };
        }

        private static PlatformAssetLibraryConfiguration GetPlatformAssetLibraryConfiguration(
            KeyValuePair<AssetGroup, Raw.PlatformAssetLibraryConfiguration> kv)
        {
            return new PlatformAssetLibraryConfiguration()
            {
                Group = kv.Key,
                SublibraryRemappingAtoB =
                    kv.Value.SublibraryRemapping.ToDictionary(pair => pair.Key, pair => pair.Value),
                SublibraryRemappingBtoA =
                    kv.Value.SublibraryRemapping.ToDictionary(pair => pair.Value, pair => pair.Key),
            };
        }
    }
}
