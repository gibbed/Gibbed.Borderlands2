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
    public partial class PackableWeaponView
    {
        public PackableWeaponView()
        {
            this.InitializeComponent();

            this.TypeDefinitionComboBox.ItemsSource =
                CreateAssetList(InfoManager.WeaponBalanceDefinitions.Items.Where(wbd => wbd.Value.Types != null).
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
            this.BodyPartDefinitionComboBox.ItemsSource = CreateAssetList(null);
            this.GripPartDefinitionComboBox.ItemsSource = CreateAssetList(null);
            this.BarrelPartDefinitionComboBox.ItemsSource = CreateAssetList(null);
            this.SightPartDefinitionComboBox.ItemsSource = CreateAssetList(null);
            this.StockPartDefinitionComboBox.ItemsSource = CreateAssetList(null);
            this.ElementalPartDefinitionComboBox.ItemsSource = CreateAssetList(null);
            this.Accessory1PartDefinitionComboBox.ItemsSource = CreateAssetList(null);
            this.Accessory2PartDefinitionComboBox.ItemsSource = CreateAssetList(null);
            this.MaterialPartDefinitionComboBox.ItemsSource = CreateAssetList(null);
        }

        private void UpdateComboBoxPartItems()
        {
            var balanceDefinition = InfoManager.WeaponBalanceDefinitions[this.BalanceDefinitionComboBox.Text];
            this.BodyPartDefinitionComboBox.ItemsSource =
                CreateAssetList(balanceDefinition.Parts.BodyDefinitions.OrderBy(s => s).Distinct());
            this.GripPartDefinitionComboBox.ItemsSource =
                CreateAssetList(balanceDefinition.Parts.GripDefinitions.OrderBy(s => s).Distinct());
            this.BarrelPartDefinitionComboBox.ItemsSource =
                CreateAssetList(balanceDefinition.Parts.BarrelDefinitions.OrderBy(s => s).Distinct());
            this.SightPartDefinitionComboBox.ItemsSource =
                CreateAssetList(balanceDefinition.Parts.SightDefinitions.OrderBy(s => s).Distinct());
            this.StockPartDefinitionComboBox.ItemsSource =
                CreateAssetList(balanceDefinition.Parts.StockDefinitions.OrderBy(s => s).Distinct());
            this.ElementalPartDefinitionComboBox.ItemsSource =
                CreateAssetList(balanceDefinition.Parts.ElementalDefinitions.OrderBy(s => s).Distinct());
            this.Accessory1PartDefinitionComboBox.ItemsSource =
                CreateAssetList(balanceDefinition.Parts.Accessory1Definitions.OrderBy(s => s).Distinct());
            this.Accessory2PartDefinitionComboBox.ItemsSource =
                CreateAssetList(balanceDefinition.Parts.Accessory2Definitions.OrderBy(s => s).Distinct());
            this.MaterialPartDefinitionComboBox.ItemsSource =
                CreateAssetList(balanceDefinition.Parts.MaterialDefinitions.OrderBy(s => s).Distinct());
        }

        private void OnTypeDefinitionChanged(object sender, TextChangedEventArgs e)
        {
            var typeDefinition = this.TypeDefinitionComboBox.Text;

            this.BalanceDefinitionComboBox.ItemsSource =
                CreateAssetList(InfoManager.WeaponBalanceDefinitions.Items.Where(
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
            if (InfoManager.WeaponBalanceDefinitions.ContainsKey(this.BalanceDefinitionComboBox.Text) == false)
            {
                this.ClearComboBoxPartItems();
                return;
            }

            this.UpdateComboBoxPartItems();
        }
    }
}
