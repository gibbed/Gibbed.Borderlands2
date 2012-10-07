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

using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Gibbed.Borderlands2.GameInfo;

namespace Gibbed.Borderlands2.SaveEdit
{
    public partial class PackableItemView
    {
        public PackableItemView()
        {
            this.InitializeComponent();

            this.TypeDefinitionComboBox.ItemsSource =
                CreateAssetList(InfoManager.ItemBalanceDefinitions.Items.Where(wbd => wbd.Value.Types != null).
                                    SelectMany(
                                        wbd => wbd.Value.Types).Distinct().OrderBy(s => s));
        }

        private static IEnumerable<string> CreateAssetList(IEnumerable<string> items)
        {
            var list = new List<string>();
            list.Add("None");
            if (items != null)
            {
                list.AddRange(items);
            }
            return list;
        }

        private void ClearComboBoxPartItems()
        {
            this.AlphaItemPartDefinitionComboBox.ItemsSource = CreateAssetList(null);
            this.BetaItemPartDefinitionComboBox.ItemsSource = CreateAssetList(null);
            this.GammaItemPartDefinitionComboBox.ItemsSource = CreateAssetList(null);
            this.DeltaItemPartDefinitionComboBox.ItemsSource = CreateAssetList(null);
            this.EpsilonItemPartDefinitionComboBox.ItemsSource = CreateAssetList(null);
            this.ZetaItemPartDefinitionComboBox.ItemsSource = CreateAssetList(null);
            this.EtaItemPartDefinitionComboBox.ItemsSource = CreateAssetList(null);
            this.ThetaItemPartDefinitionComboBox.ItemsSource = CreateAssetList(null);
            this.MaterialItemPartDefinitionComboBox.ItemsSource = CreateAssetList(null);
        }

        private void UpdateComboBoxPartItems()
        {
            var balanceDefinition = InfoManager.ItemBalanceDefinitions[this.BalanceDefinitionComboBox.Text];
            this.AlphaItemPartDefinitionComboBox.ItemsSource =
                CreateAssetList(balanceDefinition.Parts.AlphaDefinitions.OrderBy(s => s).Distinct());
            this.BetaItemPartDefinitionComboBox.ItemsSource =
                CreateAssetList(balanceDefinition.Parts.BetaDefinitions.OrderBy(s => s).Distinct());
            this.GammaItemPartDefinitionComboBox.ItemsSource =
                CreateAssetList(balanceDefinition.Parts.GammaDefinitions.OrderBy(s => s).Distinct());
            this.DeltaItemPartDefinitionComboBox.ItemsSource =
                CreateAssetList(balanceDefinition.Parts.DeltaDefinitions.OrderBy(s => s).Distinct());
            this.EpsilonItemPartDefinitionComboBox.ItemsSource =
                CreateAssetList(balanceDefinition.Parts.EpsilonDefinitions.OrderBy(s => s).Distinct());
            this.ZetaItemPartDefinitionComboBox.ItemsSource =
                CreateAssetList(balanceDefinition.Parts.ZetaDefinitions.OrderBy(s => s).Distinct());
            this.EtaItemPartDefinitionComboBox.ItemsSource =
                CreateAssetList(balanceDefinition.Parts.EtaDefinitions.OrderBy(s => s).Distinct());
            this.ThetaItemPartDefinitionComboBox.ItemsSource =
                CreateAssetList(balanceDefinition.Parts.ThetaDefinitions.OrderBy(s => s).Distinct());
            this.MaterialItemPartDefinitionComboBox.ItemsSource =
                CreateAssetList(balanceDefinition.Parts.MaterialDefinitions.OrderBy(s => s).Distinct());
        }

        private void OnTypeDefinitionChanged(object sender, TextChangedEventArgs e)
        {
            var typeDefinition = this.TypeDefinitionComboBox.Text;

            this.BalanceDefinitionComboBox.ItemsSource =
                CreateAssetList(InfoManager.ItemBalanceDefinitions.Items.Where(
                    wbd => wbd.Value.Types != null && wbd.Value.Types.Contains(typeDefinition) == true).Select(
                        wbd => wbd.Key).Distinct().OrderBy(s => s));

            if (this.BalanceDefinitionComboBox.Items.Contains(this.BalanceDefinitionComboBox.Text) == false)
            {
                this.ClearComboBoxPartItems();
            }
            else
            {
                this.UpdateComboBoxPartItems();
            }
        }

        private void OnBalanceDefinitionChanged(object sender, TextChangedEventArgs e)
        {
            if (InfoManager.ItemBalanceDefinitions.ContainsKey(this.BalanceDefinitionComboBox.Text) == false)
            {
                this.ClearComboBoxPartItems();
                return;
            }

            this.UpdateComboBoxPartItems();
        }
    }
}
