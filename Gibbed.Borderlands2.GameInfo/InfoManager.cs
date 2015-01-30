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

namespace Gibbed.Borderlands2.GameInfo
{
    public static class InfoManager
    {
        public static InfoDictionary<DownloadablePackageDefinition> DownloadablePackages { get; private set; }
        public static InfoDictionary<DownloadableContentDefinition> DownloadableContents { get; private set; }
        public static InfoDictionary<Platform, PlatformConfiguration> PlatformConfigurations { get; private set; }
        public static AssetLibraryManager AssetLibraryManager { get; private set; }
        public static InfoDictionary<PlayerClassDefinition> PlayerClasses { get; private set; }
        public static InfoDictionary<WeaponTypeDefinition> WeaponTypes { get; private set; }
        public static InfoDictionary<WeaponNamePartDefinition> WeaponNameParts { get; private set; }
        public static InfoDictionary<WeaponBalanceDefinition> WeaponBalance { get; private set; }
        public static InfoDictionary<ItemTypeDefinition> ItemTypes { get; private set; }
        public static InfoDictionary<ItemNamePartDefinition> ItemNameParts { get; private set; }
        public static InfoDictionary<ItemBalanceDefinition> ItemBalance { get; private set; }
        public static InfoDictionary<CustomizationDefinition> Customizations { get; private set; }
        public static InfoDictionary<TravelStationDefinition> TravelStations { get; private set; }
        public static InfoDictionary<FastTravelStationOrdering> FastTravelStationOrdering { get; private set; }

        static InfoManager()
        {
            DownloadablePackages = Loaders.DownloadablePackageDefinitionLoader.Load();
            DownloadableContents = Loaders.DownloadableContentDefinitionLoader.Load(DownloadablePackages);

            PlatformConfigurations = Loaders.PlatformConfigurationLoader.Load();
            AssetLibraryManager = Loaders.AssetLibraryManagerLoader.Load();
            PlayerClasses = Loaders.PlayerClassDefinitionLoader.Load(DownloadableContents);

            WeaponTypes = Loaders.WeaponTypeDefinitionLoader.Load();
            WeaponNameParts = Loaders.WeaponNamePartDefinitionLoader.Load();
            WeaponBalance = Loaders.WeaponBalanceDefinitionLoader.Load(WeaponTypes);

            ItemTypes = Loaders.ItemTypeDefinitionLoader.Load();
            ItemNameParts = Loaders.ItemNamePartDefinitionLoader.Load();
            ItemBalance = Loaders.ItemBalanceDefinitionLoader.Load(ItemTypes);

            Customizations = Loaders.CustomizationDefinitionLoader.Load(DownloadableContents);

            TravelStations = Loaders.TravelStationDefinitionLoader.Load(DownloadableContents);
            FastTravelStationOrdering = Loaders.FastTravelStationOrderingLoader.Load(TravelStations,
                                                                                     DownloadableContents);
        }

        // Just a way to get the static initializer called.
        public static void Touch()
        {
        }
    }
}
