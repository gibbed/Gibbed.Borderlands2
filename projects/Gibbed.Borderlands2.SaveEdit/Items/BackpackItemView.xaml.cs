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
    public partial class BackpackItemView
    {
        public BackpackItemView()
        {
            this.InitializeComponent();
        }
        private void BackpackItem_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if(Properties.Settings.Default.isDarkEnabled == true)
            {
                SetLab.Foreground = System.Windows.Media.Brushes.White;
                SetLabel.Foreground = System.Windows.Media.Brushes.White;
                TypeLab.Foreground = System.Windows.Media.Brushes.White;
                TypeFilter.Foreground = System.Windows.Media.Brushes.White;
                BalanceLab.Foreground = System.Windows.Media.Brushes.White;
                ManufacturerLab.Foreground = System.Windows.Media.Brushes.White;
                ManuGradeLab.Foreground = System.Windows.Media.Brushes.White;
                AlphaLab.Foreground = System.Windows.Media.Brushes.White;
                BetaLab.Foreground = System.Windows.Media.Brushes.White;
                GammaLab.Foreground = System.Windows.Media.Brushes.White;
                DeltaLab.Foreground = System.Windows.Media.Brushes.White;
                EpsilonLab.Foreground = System.Windows.Media.Brushes.White;
                ZetaLab.Foreground = System.Windows.Media.Brushes.White;
                EtaLab.Foreground = System.Windows.Media.Brushes.White;
                ThetaLab.Foreground = System.Windows.Media.Brushes.White;
                MatLab.Foreground = System.Windows.Media.Brushes.White;
                PrefixLab.Foreground = System.Windows.Media.Brushes.White;
                TitleLab.Foreground = System.Windows.Media.Brushes.White;
                GameStageLab.Foreground = System.Windows.Media.Brushes.White;
                QuantityLab.Foreground = System.Windows.Media.Brushes.White;
                EquippedLab.Foreground = System.Windows.Media.Brushes.White;
                MarkLab.Foreground = System.Windows.Media.Brushes.White;
                StandardButton.Foreground = System.Windows.Media.Brushes.White;
                FavButton.Foreground = System.Windows.Media.Brushes.White;
                TrashButton.Foreground = System.Windows.Media.Brushes.White;
            }
        }
    }
}
