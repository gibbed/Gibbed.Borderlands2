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
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Data;
using Caliburn.Micro;
using Gibbed.Borderlands2.FileFormats.Items;
using Gibbed.Borderlands2.GameInfo;

namespace Gibbed.Borderlands2.SaveEdit
{
    internal class BaseItemViewModel : PropertyChangedBase, IBaseSlotViewModel
    {
        private static readonly EnumerableComparer<object> _AssetListComparer;

        static BaseItemViewModel()
        {
            _AssetListComparer = new EnumerableComparer<object>();
        }

        private readonly BaseItem _Item;
        private string _DisplayName = "Unknown Item";

        public IPackableSlot BaseSlot
        {
            get { return this._Item; }
        }

        public BaseItemViewModel(BaseItem item)
        {
            this._Item = item ?? throw new ArgumentNullException(nameof(item));

            IEnumerable<ItemDefinition> resources;
            resources = InfoManager.ItemBalance.Items
                .Where(kv => kv.Value.Item != null)
                .Select(kv => kv.Value.Item);
            resources = resources.Concat(
                InfoManager.ItemBalance.Items
                    .Where(kv => kv.Value.Items != null)
                    .SelectMany(bd => bd.Value.Items));

            var itemTypeFilters = new List<ItemTypeFilter>()
            {
                new ItemTypeFilter("All", ItemType.Unknown),
                new ItemTypeFilter(GetDisplayNameForItemType(ItemType.Artifact), ItemType.Artifact),
                new ItemTypeFilter(GetDisplayNameForItemType(ItemType.ClassMod),
                                   ItemType.ClassMod,
                                   ItemType.CrossDLCClassMod),
                new ItemTypeFilter(GetDisplayNameForItemType(ItemType.GrenadeMod), ItemType.GrenadeMod),
                new ItemTypeFilter(GetDisplayNameForItemType(ItemType.MissionItem), ItemType.MissionItem),
                new ItemTypeFilter(GetDisplayNameForItemType(ItemType.Shield), ItemType.Shield),
                new ItemTypeFilter(GetDisplayNameForItemType(ItemType.UsableCustomizationItem),
                                   ItemType.UsableCustomizationItem),
                new ItemTypeFilter(GetDisplayNameForItemType(ItemType.UsableItem), ItemType.UsableItem),
            };
            this._ItemTypeFilters = itemTypeFilters;

            this.ItemAssets = CreateAssetList(resources.Distinct().Select(t => t.ResourcePath));
            this.BuildBalanceAssets();
            this.UpdateDisplayName();

            this._FilteredItemAssets = CollectionViewSource.GetDefaultView(this.ItemAssets);
            this._FilteredItemAssets.Filter += this.FilterItem;

            ItemTypeFilter selectedItemFilter = null;
            if (InfoManager.Items.TryGetValue(item.Item, out var itemDef) == true)
            {
                selectedItemFilter = itemTypeFilters.SingleOrDefault(
                    itf =>
                    itf.Value == itemDef.Type ||
                    (itf.SecondValue != ItemType.Unknown && itf.SecondValue == itemDef.Type));
            }
            this.SelectedItemTypeFilter = selectedItemFilter ?? itemTypeFilters.First();
        }

        private bool FilterItem(object o)
        {
            var itemPath = o as string;
            if (itemPath == null)
            {
                return false;
            }

            if (itemPath == "None")
            {
                return true;
            }

            if (this._SelectedItemTypeFilter == null)
            {
                return false;
            }

            var selectedItemType = this._SelectedItemTypeFilter.Value;
            var selectedSecondItemType = this._SelectedItemTypeFilter.SecondValue;
            if (selectedItemType == ItemType.Unknown)
            {
                return true;
            }

            if (InfoManager.Items.TryGetValue(itemPath, out var item) == false)
            {
                return false;
            }

            if (selectedItemType == item.Type)
            {
                return true;
            }

            if (selectedSecondItemType != ItemType.Unknown && selectedSecondItemType == item.Type)
            {
                return true;
            }

            return false;
        }

        private static string GenerateDisplayName(string itemPath, string prefixPartPath, string titlePartPath)
        {
            string name = null;
            bool hasFullName = false;

            if (itemPath != "None" && InfoManager.Items.TryGetValue(itemPath, out var item) == true)
            {
                name = item.Name;
                hasFullName = item.HasFullName;
            }

            if (hasFullName == false &&
                titlePartPath != "None" &&
                InfoManager.ItemNameParts.TryGetValue(titlePartPath, out var titlePart) == true)
            {
                name = titlePart.Name;
            }

            if (name != null &&
                prefixPartPath != "None" &&
                InfoManager.ItemNameParts.TryGetValue(prefixPartPath, out var prefixPart) == true &&
                string.IsNullOrEmpty(prefixPart.Name) == false)
            {
                name = prefixPart.Name + " " + name;
            }

            if (string.IsNullOrEmpty(name) == false)
            {
                return name;
            }

            return "Unknown Item";
        }

        private void UpdateDisplayName()
        {
            this.DisplayName = GenerateDisplayName(this.Item, this.PrefixPart, this.TitlePart);
        }

        #region Properties
        public string Item
        {
            get { return this._Item.Item; }
            set
            {
                this._Item.Item = value;
                this.NotifyOfPropertyChange(nameof(Item));
                this.BuildBalanceAssets();
                this.UpdateDisplayName();
            }
        }

        public string Balance
        {
            get { return this._Item.Balance; }
            set
            {
                this._Item.Balance = value;
                this.NotifyOfPropertyChange(nameof(Balance));
                this.BuildPartAssets();
            }
        }

        public string Manufacturer
        {
            get { return this._Item.Manufacturer; }
            set
            {
                this._Item.Manufacturer = value;
                this.NotifyOfPropertyChange(nameof(Manufacturer));
            }
        }

        public int ManufacturerGradeIndex
        {
            get { return this._Item.ManufacturerGradeIndex; }
            set
            {
                this._Item.ManufacturerGradeIndex = value;
                this.NotifyOfPropertyChange(nameof(ManufacturerGradeIndex));
            }
        }

        public string AlphaPart
        {
            get { return this._Item.AlphaPart; }
            set
            {
                this._Item.AlphaPart = value;
                this.NotifyOfPropertyChange(nameof(AlphaPart));
            }
        }

        public string BetaPart
        {
            get { return this._Item.BetaPart; }
            set
            {
                this._Item.BetaPart = value;
                this.NotifyOfPropertyChange(nameof(BetaPart));
            }
        }

        public string GammaPart
        {
            get { return this._Item.GammaPart; }
            set
            {
                this._Item.GammaPart = value;
                this.NotifyOfPropertyChange(nameof(GammaPart));
            }
        }

        public string DeltaPart
        {
            get { return this._Item.DeltaPart; }
            set
            {
                this._Item.DeltaPart = value;
                this.NotifyOfPropertyChange(nameof(DeltaPart));
            }
        }

        public string EpsilonPart
        {
            get { return this._Item.EpsilonPart; }
            set
            {
                this._Item.EpsilonPart = value;
                this.NotifyOfPropertyChange(nameof(EpsilonPart));
            }
        }

        public string ZetaPart
        {
            get { return this._Item.ZetaPart; }
            set
            {
                this._Item.ZetaPart = value;
                this.NotifyOfPropertyChange(nameof(ZetaPart));
            }
        }

        public string EtaPart
        {
            get { return this._Item.EtaPart; }
            set
            {
                this._Item.EtaPart = value;
                this.NotifyOfPropertyChange(nameof(EtaPart));
            }
        }

        public string ThetaPart
        {
            get { return this._Item.ThetaPart; }
            set
            {
                this._Item.ThetaPart = value;
                this.NotifyOfPropertyChange(nameof(ThetaPart));
            }
        }

        public string MaterialPart
        {
            get { return this._Item.MaterialPart; }
            set
            {
                this._Item.MaterialPart = value;
                this.NotifyOfPropertyChange(nameof(MaterialPart));
            }
        }

        public string PrefixPart
        {
            get { return this._Item.PrefixPart; }
            set
            {
                this._Item.PrefixPart = value;
                this.NotifyOfPropertyChange(nameof(PrefixPart));
                this.UpdateDisplayName();
            }
        }

        public string TitlePart
        {
            get { return this._Item.TitlePart; }
            set
            {
                this._Item.TitlePart = value;
                this.NotifyOfPropertyChange(nameof(TitlePart));
                this.UpdateDisplayName();
            }
        }

        public int GameStage
        {
            get { return this._Item.GameStage; }
            set
            {
                this._Item.GameStage = value;
                this.NotifyOfPropertyChange(nameof(GameStage));
            }
        }

        public int UniqueId
        {
            get { return this._Item.UniqueId; }
            set
            {
                this._Item.UniqueId = value;
                this.NotifyOfPropertyChange(nameof(UniqueId));
            }
        }

        public int AssetLibrarySetId
        {
            get { return this._Item.AssetLibrarySetId; }
            set
            {
                this._Item.AssetLibrarySetId = value;
                this.NotifyOfPropertyChange(nameof(AssetLibrarySetId));
            }
        }
        #endregion Properties

        #region Display Properties
        public string AssetLibrarySetName
        {
            get
            {
                if (this.AssetLibrarySetId == 0)
                {
                    return "Base Game";
                }

                var package = InfoManager.DownloadablePackages
                    .Items
                    .Select(kv => kv.Value)
                    .FirstOrDefault(dp => dp.Id == this.AssetLibrarySetId);
                if (package == null)
                {
                    return $"(unknown #{this.AssetLibrarySetId})";
                }

                return $"{package.DisplayName} (#{this.AssetLibrarySetId})";
            }
        }

        public virtual string DisplayName
        {
            get { return this._DisplayName; }
            private set
            {
                this._DisplayName = value;
                this.NotifyOfPropertyChange(nameof(DisplayName));
            }
        }

        public virtual string DisplayGroup
        {
            get
            {
                if (InfoManager.Items.TryGetValue(this.Item, out var item) == false)
                {
                    return "Unknown";
                }
                return GetDisplayNameForItemType(item.Type);
            }
        }
        #endregion

        private static string GetDisplayNameForItemType(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Artifact:
                {
                    return "Relics";
                }

                case ItemType.ClassMod:
                case ItemType.CrossDLCClassMod:
                {
                    return "Class Mods";
                }

                case ItemType.GrenadeMod:
                {
                    return "Grenade Mods";
                }

                case ItemType.MissionItem:
                {
                    return "Mission Items";
                }

                case ItemType.Shield:
                {
                    return "Shields";
                }

                case ItemType.UsableCustomizationItem:
                {
                    return "Customizations";
                }

                case ItemType.UsableItem:
                {
                    return "Personal";
                }
            }

            return "Unknown";
        }

        #region Assets
        private static IEnumerable<string> CreateAssetList(IEnumerable<string> items)
        {
            var list = new List<string>()
            {
                "None",
            };

            if (items != null)
            {
                object convert(string str)
                {
                    if (int.TryParse(str, out var value) == true)
                    {
                        return value;
                    }
                    return str;
                }
                list.AddRange(items
                    .Distinct()
                    .OrderBy(s => Regex.Split(s.Replace(" ", ""), "([0-9]+)").Select(convert), _AssetListComparer));
            }

            return list;
        }

        #region Fields
        private IEnumerable<string> _ItemAssets;
        private readonly IEnumerable<ItemTypeFilter> _ItemTypeFilters;
        private ItemTypeFilter _SelectedItemTypeFilter;
        private readonly ICollectionView _FilteredItemAssets;

        private IEnumerable<string> _BalanceAssets;
        private IEnumerable<string> _ManufacturerAssets;
        private IEnumerable<string> _AlphaPartAssets;
        private IEnumerable<string> _BetaPartAssets;
        private IEnumerable<string> _GammaPartAssets;
        private IEnumerable<string> _DeltaPartAssets;
        private IEnumerable<string> _EpsilonPartAssets;
        private IEnumerable<string> _ZetaPartAssets;
        private IEnumerable<string> _EtaPartAssets;
        private IEnumerable<string> _ThetaPartAssets;
        private IEnumerable<string> _MaterialPartAssets;
        #endregion

        #region Properties
        public IEnumerable<ItemTypeFilter> ItemTypeFilters
        {
            get { return this._ItemTypeFilters; }
        }

        public ItemTypeFilter SelectedItemTypeFilter
        {
            get { return this._SelectedItemTypeFilter; }
            set
            {
                this._SelectedItemTypeFilter = value;
                this.NotifyOfPropertyChange(nameof(SelectedItemTypeFilter));
                this.FilteredItemAssets.Refresh();
            }
        }

        public IEnumerable<string> ItemAssets
        {
            get { return this._ItemAssets; }
            private set
            {
                this._ItemAssets = value;
                this.NotifyOfPropertyChange(nameof(ItemAssets));
            }
        }

        public ICollectionView FilteredItemAssets
        {
            get { return this._FilteredItemAssets; }
        }

        public IEnumerable<string> BalanceAssets
        {
            get { return this._BalanceAssets; }
            private set
            {
                this._BalanceAssets = value;
                this.NotifyOfPropertyChange(nameof(BalanceAssets));
            }
        }

        public IEnumerable<string> ManufacturerAssets
        {
            get { return this._ManufacturerAssets; }
            private set
            {
                this._ManufacturerAssets = value;
                this.NotifyOfPropertyChange(nameof(ManufacturerAssets));
            }
        }

        public IEnumerable<string> AlphaPartAssets
        {
            get { return this._AlphaPartAssets; }
            private set
            {
                this._AlphaPartAssets = value;
                this.NotifyOfPropertyChange(nameof(AlphaPartAssets));
            }
        }

        public IEnumerable<string> BetaPartAssets
        {
            get { return this._BetaPartAssets; }
            private set
            {
                this._BetaPartAssets = value;
                this.NotifyOfPropertyChange(nameof(BetaPartAssets));
            }
        }

        public IEnumerable<string> GammaPartAssets
        {
            get { return this._GammaPartAssets; }
            private set
            {
                this._GammaPartAssets = value;
                this.NotifyOfPropertyChange(nameof(GammaPartAssets));
            }
        }

        public IEnumerable<string> DeltaPartAssets
        {
            get { return this._DeltaPartAssets; }
            private set
            {
                this._DeltaPartAssets = value;
                this.NotifyOfPropertyChange(nameof(DeltaPartAssets));
            }
        }

        public IEnumerable<string> EpsilonPartAssets
        {
            get { return this._EpsilonPartAssets; }
            private set
            {
                this._EpsilonPartAssets = value;
                this.NotifyOfPropertyChange(nameof(EpsilonPartAssets));
            }
        }

        public IEnumerable<string> ZetaPartAssets
        {
            get { return this._ZetaPartAssets; }
            private set
            {
                this._ZetaPartAssets = value;
                this.NotifyOfPropertyChange(nameof(ZetaPartAssets));
            }
        }

        public IEnumerable<string> EtaPartAssets
        {
            get { return this._EtaPartAssets; }
            private set
            {
                this._EtaPartAssets = value;
                this.NotifyOfPropertyChange(nameof(EtaPartAssets));
            }
        }

        public IEnumerable<string> ThetaPartAssets
        {
            get { return this._ThetaPartAssets; }
            private set
            {
                this._ThetaPartAssets = value;
                this.NotifyOfPropertyChange(nameof(ThetaPartAssets));
            }
        }

        public IEnumerable<string> MaterialPartAssets
        {
            get { return this._MaterialPartAssets; }
            private set
            {
                this._MaterialPartAssets = value;
                this.NotifyOfPropertyChange(nameof(MaterialPartAssets));
            }
        }
        #endregion

        private readonly string[] _NoneAssets = new[]
        {
            "None"
        };

        private void BuildBalanceAssets()
        {
            if (InfoManager.Items.TryGetValue(this.Item, out var item) == true)
            {
                this.BalanceAssets = CreateAssetList(InfoManager.ItemBalance.Items
                    .Where(kv => kv.Value.IsSuitableFor(item) == true)
                    .Select(kv => kv.Key));
            }
            else
            {
                this.BalanceAssets = CreateAssetList(null);
            }

            this.BuildPartAssets();
        }

        private void BuildPartAssets()
        {
            if (InfoManager.Items.TryGetValue(this.Item, out var item) == false ||
                this.BalanceAssets.Contains(this.Balance) == false ||
                InfoManager.ItemBalance.TryGetValue(this.Balance, out var itemBalance) == false ||
                this.Balance == "None")
            {
                this.ManufacturerAssets = _NoneAssets;
                this.AlphaPartAssets = _NoneAssets;
                this.AlphaPartAssets = _NoneAssets;
                this.BetaPartAssets = _NoneAssets;
                this.GammaPartAssets = _NoneAssets;
                this.DeltaPartAssets = _NoneAssets;
                this.EpsilonPartAssets = _NoneAssets;
                this.ZetaPartAssets = _NoneAssets;
                this.EtaPartAssets = _NoneAssets;
                this.ThetaPartAssets = _NoneAssets;
                this.MaterialPartAssets = _NoneAssets;
            }
            else
            {
                var balance = itemBalance.Create(item);
                this.ManufacturerAssets = CreateAssetList(balance.Manufacturers);
                this.AlphaPartAssets = CreateAssetList(balance.Parts.AlphaParts);
                this.BetaPartAssets = CreateAssetList(balance.Parts.BetaParts);
                this.GammaPartAssets = CreateAssetList(balance.Parts.GammaParts);
                this.DeltaPartAssets = CreateAssetList(balance.Parts.DeltaParts);
                this.EpsilonPartAssets = CreateAssetList(balance.Parts.EpsilonParts);
                this.ZetaPartAssets = CreateAssetList(balance.Parts.ZetaParts);
                this.EtaPartAssets = CreateAssetList(balance.Parts.EtaParts);
                this.ThetaPartAssets = CreateAssetList(balance.Parts.ThetaParts);
                this.MaterialPartAssets = CreateAssetList(balance.Parts.MaterialParts);
            }
        }
        #endregion
    }
}
