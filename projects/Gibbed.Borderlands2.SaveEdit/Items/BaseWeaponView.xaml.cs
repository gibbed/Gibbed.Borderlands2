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
    public partial class BaseWeaponView
    {
        public BaseWeaponView()
        {
            this.InitializeComponent();
        }
        private void BaseWeaponView_Load(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Properties.Settings.Default.isDarkEnabled == true)
            {
                SetText.Foreground = System.Windows.Media.Brushes.White;
                SetLabel.Foreground = System.Windows.Media.Brushes.White;
                TypeLabel.Foreground = System.Windows.Media.Brushes.White;
                BalanceLabel.Foreground = System.Windows.Media.Brushes.White;
                ManufacturerLabel.Foreground = System.Windows.Media.Brushes.White;
                GradeLabel.Foreground = System.Windows.Media.Brushes.White;
                BodLabel.Foreground = System.Windows.Media.Brushes.White;
                GripLabel.Foreground = System.Windows.Media.Brushes.White;
                BarrelLabel.Foreground = System.Windows.Media.Brushes.White;
                SightLabel.Foreground = System.Windows.Media.Brushes.White;
                StockLabel.Foreground = System.Windows.Media.Brushes.White;
                ElementLabel.Foreground = System.Windows.Media.Brushes.White;
                AccessoryLabel.Foreground = System.Windows.Media.Brushes.White;
                Accessory2Label.Foreground = System.Windows.Media.Brushes.White;
                MatLabel.Foreground = System.Windows.Media.Brushes.White;
                PrefixLabel.Foreground = System.Windows.Media.Brushes.White;
                TitleLabel.Foreground = System.Windows.Media.Brushes.White;
                GameStageLabel.Foreground = System.Windows.Media.Brushes.White;
            }
            else
            {
                SetText.Foreground = System.Windows.Media.Brushes.Black;
                SetLabel.Foreground = System.Windows.Media.Brushes.Black;
                TypeLabel.Foreground = System.Windows.Media.Brushes.Black;
                BalanceLabel.Foreground = System.Windows.Media.Brushes.Black;
                ManufacturerLabel.Foreground = System.Windows.Media.Brushes.Black;
                GradeLabel.Foreground = System.Windows.Media.Brushes.Black;
                BodLabel.Foreground = System.Windows.Media.Brushes.Black;
                GripLabel.Foreground = System.Windows.Media.Brushes.Black;
                BarrelLabel.Foreground = System.Windows.Media.Brushes.Black;
                SightLabel.Foreground = System.Windows.Media.Brushes.Black;
                StockLabel.Foreground = System.Windows.Media.Brushes.Black;
                ElementLabel.Foreground = System.Windows.Media.Brushes.Black;
                AccessoryLabel.Foreground = System.Windows.Media.Brushes.Black;
                Accessory2Label.Foreground = System.Windows.Media.Brushes.Black;
                MatLabel.Foreground = System.Windows.Media.Brushes.Black;
                PrefixLabel.Foreground = System.Windows.Media.Brushes.Black;
                TitleLabel.Foreground = System.Windows.Media.Brushes.Black;
                GameStageLabel.Foreground = System.Windows.Media.Brushes.Black;
            }
        }
    }
}
