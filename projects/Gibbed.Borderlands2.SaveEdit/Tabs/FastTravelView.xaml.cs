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
    public partial class FastTravelView
    {
        public FastTravelView()
        {
            this.InitializeComponent();
        }
        private void FT_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if(Properties.Settings.Default.isDarkEnabled == true)
            {
                System.Windows.Media.Color DarkBack = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#141414");
                System.Windows.Media.Brush DarkBrush = new System.Windows.Media.SolidColorBrush(DarkBack);
                LastTeleBox.Foreground = System.Windows.Media.Brushes.White;
                VisitedBox.Foreground = System.Windows.Media.Brushes.White;
                GroupedControl.Foreground = System.Windows.Media.Brushes.White;
                CheckAllLabel.Foreground = System.Windows.Media.Brushes.White;
                UncheckLabel.Foreground = System.Windows.Media.Brushes.White;
                ButtonBar.Background = DarkBrush;
            }
        }
    }
}
