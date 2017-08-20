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

using System.Windows.Input;

namespace Gibbed.Borderlands2.SaveEdit
{
    public partial class BackpackView
    {
        public BackpackView()
        {
            this.InitializeComponent();
        }

        public static class Commands
        {
            public static readonly RoutedUICommand Duplicate;
            public static readonly RoutedUICommand Bank;
            public static readonly RoutedUICommand CopyCode;
            public static readonly RoutedUICommand Delete;

            static Commands()
            {
                Duplicate = new RoutedUICommand("Duplicate", "Duplicate", typeof(BackpackView));
                Bank = new RoutedUICommand("Bank", "Bank", typeof(BackpackView));
                CopyCode = new RoutedUICommand("Copy Code", "CopyCode", typeof(BackpackView));
                Delete = new RoutedUICommand("Delete", "Delete", typeof(BackpackView));
            }
        }
        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Windows.Media.Color DarkBack = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#141414");
            System.Windows.Media.Brush DarkBrush = new System.Windows.Media.SolidColorBrush(DarkBack);
            if (Properties.Settings.Default.isDarkEnabled == true)
            {
                BackpackControl.Background = DarkBrush;
                BackgroundGrid.Background = DarkBrush;
                BackpackListView.Background = DarkBrush;
                BackpackGridSplitter.Background = DarkBrush;
                ButtonBar.Background = DarkBrush;
                NewWepLabel.Foreground = System.Windows.Media.Brushes.White;
                NewItemLabel.Foreground = System.Windows.Media.Brushes.White;
                PasteCodeLabel.Foreground = System.Windows.Media.Brushes.White;
                SyncEquipLabel.Foreground = System.Windows.Media.Brushes.White;
                SyncAllLabel.Foreground = System.Windows.Media.Brushes.White;
                ButtonBar.Foreground = System.Windows.Media.Brushes.White;
                BackpackControl.Foreground = System.Windows.Media.Brushes.White;
                GearCalcView.Foreground = System.Windows.Media.Brushes.White;
                
            }
            else
            {
                BackpackControl.Background = System.Windows.Media.Brushes.White;
                BackgroundGrid.Background = System.Windows.Media.Brushes.White;
                BackpackListView.Background = System.Windows.Media.Brushes.White;
                BackpackGridSplitter.Background = System.Windows.Media.Brushes.White;
                ButtonBar.Background = System.Windows.Media.Brushes.White;
                BackpackControl.Foreground = System.Windows.Media.Brushes.Black;
                NewWepLabel.Foreground = System.Windows.Media.Brushes.Black;
                NewItemLabel.Foreground = System.Windows.Media.Brushes.Black;
                PasteCodeLabel.Foreground = System.Windows.Media.Brushes.Black;
                SyncEquipLabel.Foreground = System.Windows.Media.Brushes.Black;
                SyncAllLabel.Foreground = System.Windows.Media.Brushes.Black;
                ButtonBar.Foreground = System.Windows.Media.Brushes.Black;
                BackpackControl.Foreground = System.Windows.Media.Brushes.Black;
                GearCalcView.Foreground = System.Windows.Media.Brushes.Black;
            }
        }
    }
}
