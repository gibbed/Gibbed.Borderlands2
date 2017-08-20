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
    public partial class CurrencyOnHandView
    {
        public CurrencyOnHandView()
        {
            this.InitializeComponent();
        }
        private void Currency_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if(Properties.Settings.Default.isDarkEnabled == true)
            {
                CurrencyExpander.Foreground = System.Windows.Media.Brushes.White;
                CreditsLabel.Foreground = System.Windows.Media.Brushes.White;
                EridLabel.Foreground = System.Windows.Media.Brushes.White;
                SeraphLab.Foreground = System.Windows.Media.Brushes.White;
                TorgueLab.Foreground = System.Windows.Media.Brushes.White;
                ReservedExpander.Foreground = System.Windows.Media.Brushes.White;
                ALabel.Foreground = System.Windows.Media.Brushes.White;
                CLabel.Foreground = System.Windows.Media.Brushes.White;
                DLabel.Foreground = System.Windows.Media.Brushes.White;
                ELabel.Foreground = System.Windows.Media.Brushes.White;
                FLabel.Foreground = System.Windows.Media.Brushes.White;
                GLabel.Foreground = System.Windows.Media.Brushes.White;
                HLabel.Foreground = System.Windows.Media.Brushes.White;
                ILabel.Foreground = System.Windows.Media.Brushes.White;
                JLabel.Foreground = System.Windows.Media.Brushes.White;
            }
        }
    }
}
