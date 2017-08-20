/* Copyright (c) 2017 Rick (rick 'at' gibbed 'dot' us)
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

namespace Gibbed.Borderlands2.SaveEdit
{
    public partial class BackpackWeaponView
    {
        public BackpackWeaponView()
        {
            this.InitializeComponent();
        }

        private void BackpackLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Windows.Media.Color DarkBack = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#141414");
            System.Windows.Media.Brush DarkBrush = new System.Windows.Media.SolidColorBrush(DarkBack);
            if (Properties.Settings.Default.isDarkEnabled == true)
            {
                #region Foreground
                SetText.Foreground = System.Windows.Media.Brushes.White;
                SetLabel.Foreground = System.Windows.Media.Brushes.White;
                TypeLabel.Foreground = System.Windows.Media.Brushes.White;
                BalanceLabel.Foreground = System.Windows.Media.Brushes.White;
                ManufacturerLabel.Foreground = System.Windows.Media.Brushes.White;
                ManufacturerGradeLabel.Foreground = System.Windows.Media.Brushes.White;
                BodyLabel.Foreground = System.Windows.Media.Brushes.White;
                GripLabel.Foreground = System.Windows.Media.Brushes.White;
                BarrelLabel.Foreground = System.Windows.Media.Brushes.White;
                SightLabel.Foreground = System.Windows.Media.Brushes.White;
                StockLabel.Foreground = System.Windows.Media.Brushes.White;
                ElementLabel.Foreground = System.Windows.Media.Brushes.White;
                Acc1Label.Foreground = System.Windows.Media.Brushes.White;
                Acc2Label.Foreground = System.Windows.Media.Brushes.White;
                MatLabel.Foreground = System.Windows.Media.Brushes.White;
                PrefixLabel.Foreground = System.Windows.Media.Brushes.White;
                TitleLabel.Foreground = System.Windows.Media.Brushes.White;
                GameStageLabel.Foreground = System.Windows.Media.Brushes.White;
                NoneButton.Foreground = System.Windows.Media.Brushes.White;
                UpButton.Foreground = System.Windows.Media.Brushes.White;
                DownButton.Foreground = System.Windows.Media.Brushes.White;
                LeftButton.Foreground = System.Windows.Media.Brushes.White;
                RightButton.Foreground = System.Windows.Media.Brushes.White;
                StandardButton.Foreground = System.Windows.Media.Brushes.White;
                FavButton.Foreground = System.Windows.Media.Brushes.White;
                TrashButton.Foreground = System.Windows.Media.Brushes.White;
                QuickSlotLabel.Foreground = System.Windows.Media.Brushes.White;
                MarkLabel.Foreground = System.Windows.Media.Brushes.White;
                #endregion
                #region Background
                SetText.Background = DarkBrush;
                SetLabel.Background = DarkBrush;
                TypeLabel.Background = DarkBrush;
                BalanceLabel.Background = DarkBrush;
                ManufacturerLabel.Background = DarkBrush;
                ManufacturerGradeLabel.Background = DarkBrush;
                BodyLabel.Background = DarkBrush;
                GripLabel.Background = DarkBrush;
                BarrelLabel.Background = DarkBrush;
                SightLabel.Background = DarkBrush;
                StockLabel.Background = DarkBrush;
                ElementLabel.Background = DarkBrush;
                Acc1Label.Background = DarkBrush;
                Acc2Label.Background = DarkBrush;
                MatLabel.Background = DarkBrush;
                PrefixLabel.Background = DarkBrush;
                TitleLabel.Background = DarkBrush;
                GameStageLabel.Background = DarkBrush;
                NoneButton.Background = DarkBrush;
                UpButton.Background = DarkBrush;
                DownButton.Background = DarkBrush;
                LeftButton.Background = DarkBrush;
                RightButton.Background = DarkBrush;
                StandardButton.Background = DarkBrush;
                FavButton.Background = DarkBrush;
                TrashButton.Background = DarkBrush;
#endregion
            }
            else
            {
                #region Foreground
                SetText.Foreground = System.Windows.Media.Brushes.Black;
                SetLabel.Foreground = System.Windows.Media.Brushes.Black;
                TypeLabel.Foreground = System.Windows.Media.Brushes.Black;
                BalanceLabel.Foreground = System.Windows.Media.Brushes.Black;
                ManufacturerLabel.Foreground = System.Windows.Media.Brushes.Black;
                ManufacturerGradeLabel.Foreground = System.Windows.Media.Brushes.Black;
                BodyLabel.Foreground = System.Windows.Media.Brushes.Black;
                GripLabel.Foreground = System.Windows.Media.Brushes.Black;
                BarrelLabel.Foreground = System.Windows.Media.Brushes.Black;
                SightLabel.Foreground = System.Windows.Media.Brushes.Black;
                StockLabel.Foreground = System.Windows.Media.Brushes.Black;
                ElementLabel.Foreground = System.Windows.Media.Brushes.Black;
                Acc1Label.Foreground = System.Windows.Media.Brushes.Black;
                Acc2Label.Foreground = System.Windows.Media.Brushes.Black;
                MatLabel.Foreground = System.Windows.Media.Brushes.Black;
                PrefixLabel.Foreground = System.Windows.Media.Brushes.Black;
                TitleLabel.Foreground = System.Windows.Media.Brushes.Black;
                GameStageLabel.Foreground = System.Windows.Media.Brushes.Black;
                TypeComboBox.Foreground = System.Windows.Media.Brushes.Black;
                BalanceComboBox.Foreground = System.Windows.Media.Brushes.Black;
                ManufacturerComboBox.Foreground = System.Windows.Media.Brushes.Black;
                GradeUpDown.Foreground = System.Windows.Media.Brushes.Black;
                BodyPartComboBox.Foreground = System.Windows.Media.Brushes.Black;
                GripPartComboBox.Foreground = System.Windows.Media.Brushes.Black;
                BarrelPartComboBox.Foreground = System.Windows.Media.Brushes.Black;
                SightPartComboBox.Foreground = System.Windows.Media.Brushes.Black;
                StockPartComboBox.Foreground = System.Windows.Media.Brushes.Black;
                ElementalPartComboBox.Foreground = System.Windows.Media.Brushes.Black;
                Accessory1PartComboBox.Foreground = System.Windows.Media.Brushes.Black;
                Accessory2PartComboBox.Foreground = System.Windows.Media.Brushes.Black;
                MaterialPartComboBox.Foreground = System.Windows.Media.Brushes.Black;
                PrefixPartComboBox.Foreground = System.Windows.Media.Brushes.Black;
                TitlePartComboBox.Foreground = System.Windows.Media.Brushes.Black;
                GameStageUpDown.Foreground = System.Windows.Media.Brushes.Black;
                NoneButton.Foreground = System.Windows.Media.Brushes.Black;
                UpButton.Foreground = System.Windows.Media.Brushes.Black;
                DownButton.Foreground = System.Windows.Media.Brushes.Black;
                LeftButton.Foreground = System.Windows.Media.Brushes.Black;
                RightButton.Foreground = System.Windows.Media.Brushes.Black;
                StandardButton.Foreground = System.Windows.Media.Brushes.Black;
                FavButton.Foreground = System.Windows.Media.Brushes.Black;
                TrashButton.Foreground = System.Windows.Media.Brushes.Black;
                QuickSlotLabel.Foreground = System.Windows.Media.Brushes.Black;
                MarkLabel.Foreground = System.Windows.Media.Brushes.Black;
                #endregion
                #region Background
                SetText.Background = System.Windows.Media.Brushes.White;
                SetLabel.Background = System.Windows.Media.Brushes.White;
                TypeLabel.Background = System.Windows.Media.Brushes.White;
                BalanceLabel.Background = System.Windows.Media.Brushes.White;
                ManufacturerLabel.Background = System.Windows.Media.Brushes.White;
                ManufacturerGradeLabel.Background = System.Windows.Media.Brushes.White;
                BodyLabel.Background = System.Windows.Media.Brushes.White;
                GripLabel.Background = System.Windows.Media.Brushes.White;
                BarrelLabel.Background = System.Windows.Media.Brushes.White;
                SightLabel.Background = System.Windows.Media.Brushes.White;
                StockLabel.Background = System.Windows.Media.Brushes.White;
                ElementLabel.Background = System.Windows.Media.Brushes.White;
                Acc1Label.Background = System.Windows.Media.Brushes.White;
                Acc2Label.Background = System.Windows.Media.Brushes.White;
                MatLabel.Background = System.Windows.Media.Brushes.White;
                PrefixLabel.Background = System.Windows.Media.Brushes.White;
                TitleLabel.Background = System.Windows.Media.Brushes.White;
                GameStageLabel.Background = System.Windows.Media.Brushes.White;
                TypeComboBox.Background = System.Windows.Media.Brushes.White;
                BalanceComboBox.Background = System.Windows.Media.Brushes.White;
                ManufacturerComboBox.Background = System.Windows.Media.Brushes.White;
                GradeUpDown.Background = System.Windows.Media.Brushes.White;
                BodyPartComboBox.Background = System.Windows.Media.Brushes.White;
                GripPartComboBox.Background = System.Windows.Media.Brushes.White;
                BarrelPartComboBox.Background = System.Windows.Media.Brushes.White;
                SightPartComboBox.Background = System.Windows.Media.Brushes.White;
                StockPartComboBox.Background = System.Windows.Media.Brushes.White;
                ElementalPartComboBox.Background = System.Windows.Media.Brushes.White;
                Accessory1PartComboBox.Background = System.Windows.Media.Brushes.White;
                Accessory2PartComboBox.Background = System.Windows.Media.Brushes.White;
                MaterialPartComboBox.Background = System.Windows.Media.Brushes.White;
                PrefixPartComboBox.Background = System.Windows.Media.Brushes.White;
                TitlePartComboBox.Background = System.Windows.Media.Brushes.White;
                GameStageUpDown.Background = System.Windows.Media.Brushes.White;
                NoneButton.Background = System.Windows.Media.Brushes.White;
                UpButton.Background = System.Windows.Media.Brushes.White;
                DownButton.Background = System.Windows.Media.Brushes.White;
                LeftButton.Background = System.Windows.Media.Brushes.White;
                RightButton.Background = System.Windows.Media.Brushes.White;
                StandardButton.Background = System.Windows.Media.Brushes.White;
                FavButton.Background = System.Windows.Media.Brushes.White;
                TrashButton.Background = System.Windows.Media.Brushes.White;
                #endregion
            }
        }
    }
}
