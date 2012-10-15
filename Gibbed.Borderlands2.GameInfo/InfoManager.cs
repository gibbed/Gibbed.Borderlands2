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
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace Gibbed.Borderlands2.GameInfo
{
    public static class InfoManager
    {
        private static UnmanagedMemoryStream GetUnmanagedMemoryStream(string embeddedResourceName)
        {
            if (embeddedResourceName == null)
            {
                throw new ArgumentNullException();
            }

            var path = "Gibbed.Borderlands2.GameInfo.Resources." + embeddedResourceName + ".json";

            var assembly = Assembly.GetExecutingAssembly();
            var stream = (UnmanagedMemoryStream)assembly.GetManifestResourceStream(path);
            if (stream == null)
            {
                throw new ArgumentException("The specified embedded resource could not be found.",
                                            "embeddedResourceName");
            }
            return stream;
        }

        private static TType DeserializeJson<TType>(string embeddedResourceName)
        {
            var settings = new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Error,
            };
            settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            var serializer = JsonSerializer.Create(settings);

            using (var input = GetUnmanagedMemoryStream(embeddedResourceName))
            using (var textReader = new StreamReader(input))
            using (var jsonReader = new JsonTextReader(textReader))
            {
                return serializer.Deserialize<TType>(jsonReader);
            }
        }

        #region AssetLibraryManager
        private static AssetLibraryManager _AssetLibraryManager;

        public static AssetLibraryManager AssetLibraryManager
        {
            get
            {
                if (_AssetLibraryManager == null)
                {
                    return _AssetLibraryManager = LoadAssetLibraryManager();
                }

                return _AssetLibraryManager;
            }
        }

        private static AssetLibraryManager LoadAssetLibraryManager()
        {
            try
            {
                return DeserializeJson<AssetLibraryManager>("Asset Library Manager");
            }
            catch (Exception e)
            {
                throw new InfoLoadException("failed to load asset library manager", e);
            }
        }
        #endregion

        #region WeaponTypes
        private static InfoDictionary<WeaponType> _WeaponTypes;

        public static InfoDictionary<WeaponType> WeaponTypes
        {
            get
            {
                if (_WeaponTypes == null)
                {
                    return _WeaponTypes = new InfoDictionary<WeaponType>(LoadWeaponTypes());
                }

                return _WeaponTypes;
            }
        }

        private static Dictionary<string, WeaponType> LoadWeaponTypes()
        {
            try
            {
                return DeserializeJson<Dictionary<string, WeaponType>>("Weapon Types");
            }
            catch (Exception e)
            {
                throw new InfoLoadException("failed to load weapon types", e);
            }
        }
        #endregion

        #region ItemTypes
        private static InfoDictionary<ItemType> _ItemTypes;

        public static InfoDictionary<ItemType> ItemTypes
        {
            get
            {
                if (_ItemTypes == null)
                {
                    return _ItemTypes = new InfoDictionary<ItemType>(LoadItemTypes());
                }

                return _ItemTypes;
            }
        }

        private static Dictionary<string, ItemType> LoadItemTypes()
        {
            try
            {
                return DeserializeJson<Dictionary<string, ItemType>>("Item Types");
            }
            catch (Exception e)
            {
                throw new InfoLoadException("failed to load item types", e);
            }
        }
        #endregion

        #region WeaponBalanceDefinitions
        private static InfoDictionary<WeaponBalanceDefinition> _WeaponBalanceDefinitions;

        public static InfoDictionary<WeaponBalanceDefinition> WeaponBalanceDefinitions
        {
            get
            {
                if (_WeaponBalanceDefinitions == null)
                {
                    return
                        _WeaponBalanceDefinitions =
                        new InfoDictionary<WeaponBalanceDefinition>(LoadWeaponBalanceDefinitions());
                }

                return _WeaponBalanceDefinitions;
            }
        }

        private static Dictionary<string, WeaponBalanceDefinition> LoadWeaponBalanceDefinitions()
        {
            try
            {
                var definitions = DeserializeJson<Dictionary<string, WeaponBalanceDefinition>>("Weapon Balance");
                var mergedDefinitions = new Dictionary<string, WeaponBalanceDefinition>();
                foreach (var definition in definitions)
                {
                    mergedDefinitions.Add(definition.Key, MergeWeaponBalanceDefinition(definitions, definition.Value));
                }
                return mergedDefinitions;
            }
            catch (Exception e)
            {
                throw new InfoLoadException("failed to load weapon balances", e);
            }
        }

        private static WeaponBalanceDefinition MergeWeaponBalanceDefinition(
            Dictionary<string, WeaponBalanceDefinition> definitions, WeaponBalanceDefinition definition)
        {
            var list = new List<WeaponBalanceDefinition>();

            var current = definition;
            while (current.Base != null)
            {
                list.Insert(0, definitions[current.Base]);
                current = definitions[current.Base];
            }
            list.Add(definition);

            var merged = new WeaponBalanceDefinition()
            {
                Parts = new WeaponBalancePartCollection()
                {
                    Mode = PartReplacementMode.Complete,
                    BodyDefinitions = new List<string>(),
                    GripDefinitions = new List<string>(),
                    BarrelDefinitions = new List<string>(),
                    SightDefinitions = new List<string>(),
                    StockDefinitions = new List<string>(),
                    ElementalDefinitions = new List<string>(),
                    Accessory1Definitions = new List<string>(),
                    Accessory2Definitions = new List<string>(),
                    MaterialDefinitions = new List<string>(),
                },
            };

            foreach (var source in list)
            {
                if (source.Types != null)
                {
                    merged.Types = new List<string>();
                    merged.Types.AddRange(source.Types);
                }

                if (source.Manufacturers != null)
                {
                    merged.Manufacturers = new List<string>();
                    merged.Manufacturers.AddRange(source.Manufacturers);
                }

                if (source.Parts != null)
                {
                    if (source.Parts.TypeDefinition != null)
                    {
                        merged.Parts.TypeDefinition = source.Parts.TypeDefinition;
                    }

                    switch (source.Parts.Mode)
                    {
                        case PartReplacementMode.Additive:
                        {
                            if (source.Parts.BodyDefinitions != null)
                            {
                                merged.Parts.BodyDefinitions.AddRange(source.Parts.BodyDefinitions);
                            }

                            if (source.Parts.GripDefinitions != null)
                            {
                                merged.Parts.GripDefinitions.AddRange(source.Parts.GripDefinitions);
                            }

                            if (source.Parts.BarrelDefinitions != null)
                            {
                                merged.Parts.BarrelDefinitions.AddRange(source.Parts.BarrelDefinitions);
                            }

                            if (source.Parts.SightDefinitions != null)
                            {
                                merged.Parts.SightDefinitions.AddRange(source.Parts.SightDefinitions);
                            }

                            if (source.Parts.StockDefinitions != null)
                            {
                                merged.Parts.StockDefinitions.AddRange(source.Parts.StockDefinitions);
                            }

                            if (source.Parts.ElementalDefinitions != null)
                            {
                                merged.Parts.ElementalDefinitions.AddRange(source.Parts.ElementalDefinitions);
                            }

                            if (source.Parts.Accessory1Definitions != null)
                            {
                                merged.Parts.Accessory1Definitions.AddRange(source.Parts.Accessory1Definitions);
                            }

                            if (source.Parts.Accessory2Definitions != null)
                            {
                                merged.Parts.Accessory2Definitions.AddRange(source.Parts.Accessory2Definitions);
                            }

                            if (source.Parts.MaterialDefinitions != null)
                            {
                                merged.Parts.MaterialDefinitions.AddRange(source.Parts.MaterialDefinitions);
                            }

                            break;
                        }

                        case PartReplacementMode.Selective:
                        {
                            if (source.Parts.BodyDefinitions != null)
                            {
                                merged.Parts.BodyDefinitions.Clear();
                                merged.Parts.BodyDefinitions.AddRange(source.Parts.BodyDefinitions);
                            }

                            if (source.Parts.GripDefinitions != null)
                            {
                                merged.Parts.GripDefinitions.Clear();
                                merged.Parts.GripDefinitions.AddRange(source.Parts.GripDefinitions);
                            }

                            if (source.Parts.BarrelDefinitions != null)
                            {
                                merged.Parts.BarrelDefinitions.Clear();
                                merged.Parts.BarrelDefinitions.AddRange(source.Parts.BarrelDefinitions);
                            }

                            if (source.Parts.SightDefinitions != null)
                            {
                                merged.Parts.SightDefinitions.Clear();
                                merged.Parts.SightDefinitions.AddRange(source.Parts.SightDefinitions);
                            }

                            if (source.Parts.StockDefinitions != null)
                            {
                                merged.Parts.StockDefinitions.Clear();
                                merged.Parts.StockDefinitions.AddRange(source.Parts.StockDefinitions);
                            }

                            if (source.Parts.ElementalDefinitions != null)
                            {
                                merged.Parts.ElementalDefinitions.Clear();
                                merged.Parts.ElementalDefinitions.AddRange(source.Parts.ElementalDefinitions);
                            }

                            if (source.Parts.Accessory1Definitions != null)
                            {
                                merged.Parts.Accessory1Definitions.Clear();
                                merged.Parts.Accessory1Definitions.AddRange(source.Parts.Accessory1Definitions);
                            }

                            if (source.Parts.Accessory2Definitions != null)
                            {
                                merged.Parts.Accessory2Definitions.Clear();
                                merged.Parts.Accessory2Definitions.AddRange(source.Parts.Accessory2Definitions);
                            }

                            if (source.Parts.MaterialDefinitions != null)
                            {
                                merged.Parts.MaterialDefinitions.Clear();
                                merged.Parts.MaterialDefinitions.AddRange(source.Parts.MaterialDefinitions);
                            }

                            break;
                        }

                        case PartReplacementMode.Complete:
                        {
                            merged.Parts.BodyDefinitions.Clear();
                            if (source.Parts.BodyDefinitions != null)
                            {
                                merged.Parts.BodyDefinitions.AddRange(source.Parts.BodyDefinitions);
                            }

                            merged.Parts.GripDefinitions.Clear();
                            if (source.Parts.GripDefinitions != null)
                            {
                                merged.Parts.GripDefinitions.AddRange(source.Parts.GripDefinitions);
                            }

                            merged.Parts.BarrelDefinitions.Clear();
                            if (source.Parts.BarrelDefinitions != null)
                            {
                                merged.Parts.BarrelDefinitions.AddRange(source.Parts.BarrelDefinitions);
                            }

                            merged.Parts.SightDefinitions.Clear();
                            if (source.Parts.SightDefinitions != null)
                            {
                                merged.Parts.SightDefinitions.AddRange(source.Parts.SightDefinitions);
                            }

                            merged.Parts.StockDefinitions.Clear();
                            if (source.Parts.StockDefinitions != null)
                            {
                                merged.Parts.StockDefinitions.AddRange(source.Parts.StockDefinitions);
                            }

                            merged.Parts.ElementalDefinitions.Clear();
                            if (source.Parts.ElementalDefinitions != null)
                            {
                                merged.Parts.ElementalDefinitions.AddRange(source.Parts.ElementalDefinitions);
                            }

                            merged.Parts.Accessory1Definitions.Clear();
                            if (source.Parts.Accessory1Definitions != null)
                            {
                                merged.Parts.Accessory1Definitions.AddRange(source.Parts.Accessory1Definitions);
                            }

                            merged.Parts.Accessory2Definitions.Clear();
                            if (source.Parts.Accessory2Definitions != null)
                            {
                                merged.Parts.Accessory2Definitions.AddRange(source.Parts.Accessory2Definitions);
                            }

                            merged.Parts.MaterialDefinitions.Clear();
                            if (source.Parts.MaterialDefinitions != null)
                            {
                                merged.Parts.MaterialDefinitions.AddRange(source.Parts.MaterialDefinitions);
                            }

                            break;
                        }

                        default:
                        {
                            throw new NotImplementedException();
                        }
                    }
                }
            }

            return merged;
        }
        #endregion

        #region ItemBalanceDefinitions
        private static InfoDictionary<ItemBalanceDefinition> _ItemBalanceDefinitions;

        public static InfoDictionary<ItemBalanceDefinition> ItemBalanceDefinitions
        {
            get
            {
                if (_ItemBalanceDefinitions == null)
                {
                    return
                        _ItemBalanceDefinitions =
                        new InfoDictionary<ItemBalanceDefinition>(LoadItemBalanceDefinitions());
                }

                return _ItemBalanceDefinitions;
            }
        }

        private static Dictionary<string, ItemBalanceDefinition> LoadItemBalanceDefinitions()
        {
            try
            {
                var definitions = DeserializeJson<Dictionary<string, ItemBalanceDefinition>>("Item Balance");
                var mergedDefinitions = new Dictionary<string, ItemBalanceDefinition>();
                foreach (var definition in definitions)
                {
                    mergedDefinitions.Add(definition.Key, MergeItemBalanceDefinition(definitions, definition.Value));
                }
                return mergedDefinitions;
            }
            catch (Exception e)
            {
                throw new InfoLoadException("failed to load item balances", e);
            }
        }

        private static ItemBalanceDefinition MergeItemBalanceDefinition(
            Dictionary<string, ItemBalanceDefinition> definitions, ItemBalanceDefinition definition)
        {
            var list = new List<ItemBalanceDefinition>();

            var current = definition;
            while (current.Base != null)
            {
                list.Insert(0, definitions[current.Base]);
                current = definitions[current.Base];
            }
            list.Add(definition);

            var merged = new ItemBalanceDefinition()
            {
                Parts = new ItemBalancePartCollection()
                {
                    Mode = PartReplacementMode.Complete,
                    AlphaDefinitions = new List<string>(),
                    BetaDefinitions = new List<string>(),
                    GammaDefinitions = new List<string>(),
                    DeltaDefinitions = new List<string>(),
                    EpsilonDefinitions = new List<string>(),
                    ZetaDefinitions = new List<string>(),
                    EtaDefinitions = new List<string>(),
                    ThetaDefinitions = new List<string>(),
                    MaterialDefinitions = new List<string>(),
                },
            };

            foreach (var source in list)
            {
                if (source.Types != null)
                {
                    merged.Types = new List<string>();
                    merged.Types.AddRange(source.Types);
                }

                if (source.Manufacturers != null)
                {
                    merged.Manufacturers = new List<string>();
                    merged.Manufacturers.AddRange(source.Manufacturers);
                }

                if (source.Parts != null)
                {
                    if (source.Parts.TypeDefinition != null)
                    {
                        merged.Parts.TypeDefinition = source.Parts.TypeDefinition;
                    }

                    switch (source.Parts.Mode)
                    {
                        case PartReplacementMode.Additive:
                        {
                            if (source.Parts.AlphaDefinitions != null)
                            {
                                merged.Parts.AlphaDefinitions.AddRange(source.Parts.AlphaDefinitions);
                            }

                            if (source.Parts.BetaDefinitions != null)
                            {
                                merged.Parts.BetaDefinitions.AddRange(source.Parts.BetaDefinitions);
                            }

                            if (source.Parts.GammaDefinitions != null)
                            {
                                merged.Parts.GammaDefinitions.AddRange(source.Parts.GammaDefinitions);
                            }

                            if (source.Parts.DeltaDefinitions != null)
                            {
                                merged.Parts.DeltaDefinitions.AddRange(source.Parts.DeltaDefinitions);
                            }

                            if (source.Parts.EpsilonDefinitions != null)
                            {
                                merged.Parts.EpsilonDefinitions.AddRange(source.Parts.EpsilonDefinitions);
                            }

                            if (source.Parts.ZetaDefinitions != null)
                            {
                                merged.Parts.ZetaDefinitions.AddRange(source.Parts.ZetaDefinitions);
                            }

                            if (source.Parts.EtaDefinitions != null)
                            {
                                merged.Parts.EtaDefinitions.AddRange(source.Parts.EtaDefinitions);
                            }

                            if (source.Parts.ThetaDefinitions != null)
                            {
                                merged.Parts.ThetaDefinitions.AddRange(source.Parts.ThetaDefinitions);
                            }

                            if (source.Parts.MaterialDefinitions != null)
                            {
                                merged.Parts.MaterialDefinitions.AddRange(source.Parts.MaterialDefinitions);
                            }

                            break;
                        }

                        case PartReplacementMode.Selective:
                        {
                            if (source.Parts.AlphaDefinitions != null)
                            {
                                merged.Parts.AlphaDefinitions.Clear();
                                merged.Parts.AlphaDefinitions.AddRange(source.Parts.AlphaDefinitions);
                            }

                            if (source.Parts.BetaDefinitions != null)
                            {
                                merged.Parts.BetaDefinitions.Clear();
                                merged.Parts.BetaDefinitions.AddRange(source.Parts.BetaDefinitions);
                            }

                            if (source.Parts.GammaDefinitions != null)
                            {
                                merged.Parts.GammaDefinitions.Clear();
                                merged.Parts.GammaDefinitions.AddRange(source.Parts.GammaDefinitions);
                            }

                            if (source.Parts.DeltaDefinitions != null)
                            {
                                merged.Parts.DeltaDefinitions.Clear();
                                merged.Parts.DeltaDefinitions.AddRange(source.Parts.DeltaDefinitions);
                            }

                            if (source.Parts.EpsilonDefinitions != null)
                            {
                                merged.Parts.EpsilonDefinitions.Clear();
                                merged.Parts.EpsilonDefinitions.AddRange(source.Parts.EpsilonDefinitions);
                            }

                            if (source.Parts.ZetaDefinitions != null)
                            {
                                merged.Parts.ZetaDefinitions.Clear();
                                merged.Parts.ZetaDefinitions.AddRange(source.Parts.ZetaDefinitions);
                            }

                            if (source.Parts.EtaDefinitions != null)
                            {
                                merged.Parts.EtaDefinitions.Clear();
                                merged.Parts.EtaDefinitions.AddRange(source.Parts.EtaDefinitions);
                            }

                            if (source.Parts.ThetaDefinitions != null)
                            {
                                merged.Parts.ThetaDefinitions.Clear();
                                merged.Parts.ThetaDefinitions.AddRange(source.Parts.ThetaDefinitions);
                            }

                            if (source.Parts.MaterialDefinitions != null)
                            {
                                merged.Parts.MaterialDefinitions.Clear();
                                merged.Parts.MaterialDefinitions.AddRange(source.Parts.MaterialDefinitions);
                            }

                            break;
                        }

                        case PartReplacementMode.Complete:
                        {
                            merged.Parts.AlphaDefinitions.Clear();
                            if (source.Parts.AlphaDefinitions != null)
                            {
                                merged.Parts.AlphaDefinitions.AddRange(source.Parts.AlphaDefinitions);
                            }

                            merged.Parts.BetaDefinitions.Clear();
                            if (source.Parts.BetaDefinitions != null)
                            {
                                merged.Parts.BetaDefinitions.AddRange(source.Parts.BetaDefinitions);
                            }

                            merged.Parts.GammaDefinitions.Clear();
                            if (source.Parts.GammaDefinitions != null)
                            {
                                merged.Parts.GammaDefinitions.AddRange(source.Parts.GammaDefinitions);
                            }

                            merged.Parts.DeltaDefinitions.Clear();
                            if (source.Parts.DeltaDefinitions != null)
                            {
                                merged.Parts.DeltaDefinitions.AddRange(source.Parts.DeltaDefinitions);
                            }

                            merged.Parts.EpsilonDefinitions.Clear();
                            if (source.Parts.EpsilonDefinitions != null)
                            {
                                merged.Parts.EpsilonDefinitions.AddRange(source.Parts.EpsilonDefinitions);
                            }

                            merged.Parts.ZetaDefinitions.Clear();
                            if (source.Parts.ZetaDefinitions != null)
                            {
                                merged.Parts.ZetaDefinitions.AddRange(source.Parts.ZetaDefinitions);
                            }

                            merged.Parts.EtaDefinitions.Clear();
                            if (source.Parts.EtaDefinitions != null)
                            {
                                merged.Parts.EtaDefinitions.AddRange(source.Parts.EtaDefinitions);
                            }

                            merged.Parts.ThetaDefinitions.Clear();
                            if (source.Parts.ThetaDefinitions != null)
                            {
                                merged.Parts.ThetaDefinitions.AddRange(source.Parts.ThetaDefinitions);
                            }

                            merged.Parts.MaterialDefinitions.Clear();
                            if (source.Parts.MaterialDefinitions != null)
                            {
                                merged.Parts.MaterialDefinitions.AddRange(source.Parts.MaterialDefinitions);
                            }

                            break;
                        }


                        default:
                        {
                            throw new NotImplementedException();
                        }
                    }
                }
            }

            return merged;
        }
        #endregion

        #region Customizations
        private static InfoDictionary<CustomizationDefinition> _Customizations;

        public static InfoDictionary<CustomizationDefinition> Customizations
        {
            get
            {
                if (_Customizations == null)
                {
                    return _Customizations = new InfoDictionary<CustomizationDefinition>(LoadCustomizationDefinitions());
                }

                return _Customizations;
            }
        }

        private static Dictionary<string, CustomizationDefinition> LoadCustomizationDefinitions()
        {
            try
            {
                return DeserializeJson<Dictionary<string, CustomizationDefinition>>("Customizations");
            }
            catch (Exception e)
            {
                throw new InfoLoadException("failed to load customization definitions", e);
            }
        }
        #endregion

        #region Customization Sets
        private static InfoDictionary<CustomizationSet> _CustomizationSets;

        public static InfoDictionary<CustomizationSet> CustomizationSets
        {
            get
            {
                if (_CustomizationSets == null)
                {
                    return _CustomizationSets = new InfoDictionary<CustomizationSet>(LoadCustomizationSets());
                }

                return _CustomizationSets;
            }
        }

        private static Dictionary<string, CustomizationSet> LoadCustomizationSets()
        {
            try
            {
                return DeserializeJson<Dictionary<string, CustomizationSet>>("Customization Sets");
            }
            catch (Exception e)
            {
                throw new InfoLoadException("failed to load customization sets", e);
            }
        }
        #endregion
    }
}
