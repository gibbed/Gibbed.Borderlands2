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
    internal static class PlayerClassDefinitionLoader
    {
        public static InfoDictionary<PlayerClassDefinition> Load(
            InfoDictionary<DownloadableContentDefinition> downloadableContents)
        {
            try
            {
                var raws = LoaderHelper.DeserializeDump<Dictionary<string, Raw.PlayerClassDefinition>>(
                    "Player Classes");
                return new InfoDictionary<PlayerClassDefinition>(
                    raws.ToDictionary(
                        kv => kv.Key,
                        kv => CreatePlayerClass(downloadableContents, kv)));
            }
            catch (Exception e)
            {
                throw new InfoLoadException("failed to load player classes", e);
            }
        }

        private static PlayerClassDefinition CreatePlayerClass(
            InfoDictionary<DownloadableContentDefinition> downloadableContents,
            KeyValuePair<string, Raw.PlayerClassDefinition> kv)
        {
            var raw = kv.Value;

            DownloadableContentDefinition downloadableContent = null;
            if (string.IsNullOrEmpty(raw.DLC) == false)
            {
                if (downloadableContents.TryGetValue(kv.Value.DLC, out downloadableContent) == false)
                {
                    throw ResourceNotFoundException.Create("downloadable content", raw.DLC);
                }
            }

            return new PlayerClassDefinition()
            {
                ResourcePath = kv.Key,
                Name = raw.Name,
                Class = raw.Class,
                SortOrder = raw.SortOrder,
                DLC = downloadableContent,
            };
        }
    }
}
