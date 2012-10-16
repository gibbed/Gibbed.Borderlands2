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
using Caliburn.Micro;
using Gibbed.Borderlands2.FileFormats.Items;
using Gibbed.Borderlands2.GameInfo;

namespace Gibbed.Borderlands2.SaveEdit
{
    internal class BaseItemViewModel : PropertyChangedBase, IBaseSlotViewModel
    {
        private readonly BaseItem _Item;

        public IBaseSlot BaseSlot
        {
            get { return this._Item; }
        }

        public BaseItemViewModel(BaseItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            this._Item = item;

            this.TypeAssets =
                CreateAssetList(
                    InfoManager.ItemBalanceDefinitions.Items.Where(bd => bd.Value.Types != null).SelectMany(
                        bd => bd.Value.Types).Distinct().OrderBy(s => s));
            this.BuildBalanceAssets();
        }

        #region Properties
        public string TypeDefinition
        {
            get { return this._Item.TypeDefinition; }
            set
            {
                this._Item.TypeDefinition = value;
                this.NotifyOfPropertyChange(() => this.TypeDefinition);
                this.BuildBalanceAssets();
            }
        }

        public string BalanceDefinition
        {
            get { return this._Item.BalanceDefinition; }
            set
            {
                this._Item.BalanceDefinition = value;
                this.NotifyOfPropertyChange(() => this.BalanceDefinition);
                this.BuildPartAssets();
            }
        }

        public string ManufacturerDefinition
        {
            get { return this._Item.ManufacturerDefinition; }
            set
            {
                this._Item.ManufacturerDefinition = value;
                this.NotifyOfPropertyChange(() => this.ManufacturerDefinition);
            }
        }

        public int ManufacturerGradeIndex
        {
            get { return this._Item.ManufacturerGradeIndex; }
            set
            {
                this._Item.ManufacturerGradeIndex = value;
                this.NotifyOfPropertyChange(() => this.ManufacturerGradeIndex);
            }
        }

        public string AlphaPartDefinition
        {
            get { return this._Item.AlphaPartDefinition; }
            set
            {
                this._Item.AlphaPartDefinition = value;
                this.NotifyOfPropertyChange(() => this.AlphaPartDefinition);
            }
        }

        public string BetaPartDefinition
        {
            get { return this._Item.BetaPartDefinition; }
            set
            {
                this._Item.BetaPartDefinition = value;
                this.NotifyOfPropertyChange(() => this.BetaPartDefinition);
            }
        }

        public string GammaPartDefinition
        {
            get { return this._Item.GammaPartDefinition; }
            set
            {
                this._Item.GammaPartDefinition = value;
                this.NotifyOfPropertyChange(() => this.GammaPartDefinition);
            }
        }

        public string DeltaPartDefinition
        {
            get { return this._Item.DeltaPartDefinition; }
            set
            {
                this._Item.DeltaPartDefinition = value;
                this.NotifyOfPropertyChange(() => this.DeltaPartDefinition);
            }
        }

        public string EpsilonPartDefinition
        {
            get { return this._Item.EpsilonPartDefinition; }
            set
            {
                this._Item.EpsilonPartDefinition = value;
                this.NotifyOfPropertyChange(() => this.EpsilonPartDefinition);
            }
        }

        public string ZetaPartDefinition
        {
            get { return this._Item.ZetaPartDefinition; }
            set
            {
                this._Item.ZetaPartDefinition = value;
                this.NotifyOfPropertyChange(() => this.ZetaPartDefinition);
            }
        }

        public string EtaPartDefinition
        {
            get { return this._Item.EtaPartDefinition; }
            set
            {
                this._Item.EtaPartDefinition = value;
                this.NotifyOfPropertyChange(() => this.EtaPartDefinition);
            }
        }

        public string ThetaPartDefinition
        {
            get { return this._Item.ThetaPartDefinition; }
            set
            {
                this._Item.ThetaPartDefinition = value;
                this.NotifyOfPropertyChange(() => this.ThetaPartDefinition);
            }
        }

        public string MaterialPartDefinition
        {
            get { return this._Item.MaterialPartDefinition; }
            set
            {
                this._Item.MaterialPartDefinition = value;
                this.NotifyOfPropertyChange(() => this.MaterialPartDefinition);
            }
        }

        public string PrefixPartDefinition
        {
            get { return this._Item.PrefixPartDefinition; }
            set
            {
                this._Item.PrefixPartDefinition = value;
                this.NotifyOfPropertyChange(() => this.PrefixPartDefinition);
            }
        }

        public string TitlePartDefinition
        {
            get { return this._Item.TitlePartDefinition; }
            set
            {
                this._Item.TitlePartDefinition = value;
                this.NotifyOfPropertyChange(() => this.TitlePartDefinition);
            }
        }

        public int GameStage
        {
            get { return this._Item.GameStage; }
            set
            {
                this._Item.GameStage = value;
                this.NotifyOfPropertyChange(() => this.GameStage);
            }
        }

        public int UniqueId
        {
            get { return this._Item.UniqueId; }
            set
            {
                this._Item.UniqueId = value;
                this.NotifyOfPropertyChange(() => this.UniqueId);
            }
        }

        public int AssetLibrarySetId
        {
            get { return this._Item.AssetLibrarySetId; }
            set
            {
                this._Item.AssetLibrarySetId = value;
                this.NotifyOfPropertyChange(() => this.AssetLibrarySetId);
            }
        }
        #endregion Properties

        #region Display Properties
        public virtual string DisplayName
        {
            get { return "Base Item"; }
        }

        public virtual string DisplayGroup
        {
            get
            {
                if (InfoManager.ItemTypes.ContainsKey(this.TypeDefinition) == false)
                {
                    return "Unknown";
                }

                switch (InfoManager.ItemTypes[this.TypeDefinition].Type)
                {
                    case ItemType.Artifact:
                    {
                        return "Relic";
                    }

                    case ItemType.ClassMod:
                    {
                        return "Class Mod";
                    }

                    case ItemType.GrenadeMod:
                    {
                        return "Grenade Mod";
                    }

                    case ItemType.Shield:
                    {
                        return "Shield";
                    }

                    case ItemType.UsableCustomizationItem:
                    {
                        return "Customization";
                    }

                    case ItemType.UsableItem:
                    {
                        return "Personal";
                    }
                }

                return "Unknown";
            }
        }
        #endregion

        #region Asset Properties
        private static IEnumerable<string> CreateAssetList(IEnumerable<string> items)
        {
            var list = new List<string>()
            {
                "None"
            };

            if (items != null)
            {
                list.AddRange(items);
            }

            return list;
        }

        public IEnumerable<string> TypeAssets { get; private set; }

        private IEnumerable<string> _BalanceAssets;

        public IEnumerable<string> BalanceAssets
        {
            get { return this._BalanceAssets; }
            private set
            {
                this._BalanceAssets = value;
                this.NotifyOfPropertyChange(() => this.BalanceAssets);
            }
        }

        private IEnumerable<string> _AlphaPartAssets;

        public IEnumerable<string> AlphaPartAssets
        {
            get { return this._AlphaPartAssets; }
            private set
            {
                this._AlphaPartAssets = value;
                this.NotifyOfPropertyChange(() => this.AlphaPartAssets);
            }
        }

        private IEnumerable<string> _BetaPartAssets;

        public IEnumerable<string> BetaPartAssets
        {
            get { return this._BetaPartAssets; }
            private set
            {
                this._BetaPartAssets = value;
                this.NotifyOfPropertyChange(() => this.BetaPartAssets);
            }
        }

        private IEnumerable<string> _GammaPartAssets;

        public IEnumerable<string> GammaPartAssets
        {
            get { return this._GammaPartAssets; }
            private set
            {
                this._GammaPartAssets = value;
                this.NotifyOfPropertyChange(() => this.GammaPartAssets);
            }
        }

        private IEnumerable<string> _DeltaPartAssets;

        public IEnumerable<string> DeltaPartAssets
        {
            get { return this._DeltaPartAssets; }
            private set
            {
                this._DeltaPartAssets = value;
                this.NotifyOfPropertyChange(() => this.DeltaPartAssets);
            }
        }

        private IEnumerable<string> _EpsilonPartAssets;

        public IEnumerable<string> EpsilonPartAssets
        {
            get { return this._EpsilonPartAssets; }
            private set
            {
                this._EpsilonPartAssets = value;
                this.NotifyOfPropertyChange(() => this.EpsilonPartAssets);
            }
        }

        private IEnumerable<string> _ZetaPartAssets;

        public IEnumerable<string> ZetaPartAssets
        {
            get { return this._ZetaPartAssets; }
            private set
            {
                this._ZetaPartAssets = value;
                this.NotifyOfPropertyChange(() => this.ZetaPartAssets);
            }
        }

        private IEnumerable<string> _EtaPartAssets;

        public IEnumerable<string> EtaPartAssets
        {
            get { return this._EtaPartAssets; }
            private set
            {
                this._EtaPartAssets = value;
                this.NotifyOfPropertyChange(() => this.EtaPartAssets);
            }
        }

        private IEnumerable<string> _ThetaPartAssets;

        public IEnumerable<string> ThetaPartAssets
        {
            get { return this._ThetaPartAssets; }
            private set
            {
                this._ThetaPartAssets = value;
                this.NotifyOfPropertyChange(() => this.ThetaPartAssets);
            }
        }

        private IEnumerable<string> _MaterialPartAssets;

        public IEnumerable<string> MaterialPartAssets
        {
            get { return this._MaterialPartAssets; }
            private set
            {
                this._MaterialPartAssets = value;
                this.NotifyOfPropertyChange(() => this.MaterialPartAssets);
            }
        }

        private readonly string[] _NoneAssets = new[]
        {
            "None"
        };

        private void BuildBalanceAssets()
        {
            this.BalanceAssets =
                CreateAssetList(
                    InfoManager.ItemBalanceDefinitions.Items.Where(
                        wbd => wbd.Value.Types != null && wbd.Value.Types.Contains(this.TypeDefinition) == true).Select(
                            wbd => wbd.Key).Distinct().OrderBy(s => s));
            this.BuildPartAssets();
        }

        private void BuildPartAssets()
        {
            if (this.BalanceAssets.Contains(this.BalanceDefinition) == false ||
                this.BalanceDefinition == "None")
            {
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
                var balanceDefinition = InfoManager.ItemBalanceDefinitions[this.BalanceDefinition];
                this.AlphaPartAssets = CreateAssetList(balanceDefinition.Parts.AlphaDefinitions.OrderBy(s => s).Distinct());
                this.BetaPartAssets = CreateAssetList(balanceDefinition.Parts.BetaDefinitions.OrderBy(s => s).Distinct());
                this.GammaPartAssets =
                    CreateAssetList(balanceDefinition.Parts.GammaDefinitions.OrderBy(s => s).Distinct());
                this.DeltaPartAssets =
                    CreateAssetList(balanceDefinition.Parts.DeltaDefinitions.OrderBy(s => s).Distinct());
                this.EpsilonPartAssets =
                    CreateAssetList(balanceDefinition.Parts.EpsilonDefinitions.OrderBy(s => s).Distinct());
                this.ZetaPartAssets =
                    CreateAssetList(balanceDefinition.Parts.ZetaDefinitions.OrderBy(s => s).Distinct());
                this.EtaPartAssets =
                    CreateAssetList(balanceDefinition.Parts.EtaDefinitions.OrderBy(s => s).Distinct());
                this.ThetaPartAssets =
                    CreateAssetList(balanceDefinition.Parts.ThetaDefinitions.OrderBy(s => s).Distinct());
                this.MaterialPartAssets =
                    CreateAssetList(balanceDefinition.Parts.MaterialDefinitions.OrderBy(s => s).Distinct());
            }
        }
        #endregion
    }
}
