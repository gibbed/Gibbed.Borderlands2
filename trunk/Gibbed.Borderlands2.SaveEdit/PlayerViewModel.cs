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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using Caliburn.Micro;
using Gibbed.Borderlands2.GameInfo;

namespace Gibbed.Borderlands2.SaveEdit
{
    [Export(typeof(PlayerViewModel))]
    internal class PlayerViewModel : PropertyChangedBase, IHandle<SaveUnpackMessage>, IHandle<SavePackMessage>
    {
        #region Fields
        private FileFormats.SaveFile _SaveFile;
        private string _SelectedHead;
        private string _SelectedSkin;
        #endregion

        #region Properties
        public FileFormats.SaveFile SaveFile
        {
            get { return this._SaveFile; }
            private set
            {
                if (this._SaveFile != value)
                {
                    this._SaveFile = value;
                    this.NotifyOfPropertyChange(() => this.SaveFile);
                }
            }
        }

        public string SelectedHead
        {
            get { return this._SelectedHead; }
            set
            {
                this._SelectedHead = value;
                this.NotifyOfPropertyChange(() => this.SelectedHead);
            }
        }

        public string SelectedSkin
        {
            get { return this._SelectedSkin; }
            set
            {
                this._SelectedSkin = value;
                this.NotifyOfPropertyChange(() => this.SelectedSkin);
            }
        }

        public ObservableCollection<EndianDisplay> Endians { get; private set; }
        public ObservableCollection<AssetDisplay> ClassDefinitions { get; private set; }
        public ObservableCollection<AssetDisplay> HeadAssets { get; private set; }
        public ObservableCollection<AssetDisplay> SkinAssets { get; private set; }
        #endregion

        internal class EndianDisplay
        {
            public string Name { get; private set; }
            public IO.Endian Value { get; private set; }

            public EndianDisplay(string name, IO.Endian value)
            {
                this.Name = name;
                this.Value = value;
            }
        }

        [ImportingConstructor]
        public PlayerViewModel(IEventAggregator events)
        {
            this.Endians = new ObservableCollection<EndianDisplay>();
            this.Endians.Add(new EndianDisplay("Little (PC)", IO.Endian.Little));
            this.Endians.Add(new EndianDisplay("Big (360, PS3)", IO.Endian.Big));

            this.ClassDefinitions = new ObservableCollection<AssetDisplay>();
            this.ClassDefinitions.Add(new AssetDisplay("Axton (Commando)",
                                                       "GD_Soldier.Character.CharClass_Soldier"));
            this.ClassDefinitions.Add(new AssetDisplay("Zer0 (Assassin)",
                                                       "GD_Assassin.Character.CharClass_Assassin"));
            this.ClassDefinitions.Add(new AssetDisplay("Maya (Siren)", "GD_Siren.Character.CharClass_Siren"));
            this.ClassDefinitions.Add(new AssetDisplay("Salvador (Gunzerker)",
                                                       "GD_Mercenary.Character.CharClass_Mercenary"));
            this.ClassDefinitions.Add(new AssetDisplay("Gaige (Mechromancer)", "GD_Tulip_Mechromancer.Character.CharClass_Mechromancer"));

            this.HeadAssets = new ObservableCollection<AssetDisplay>();
            this.SkinAssets = new ObservableCollection<AssetDisplay>();

            events.Subscribe(this);

            this.BuildCustomizationAssets();
        }

        private CustomizationUsage GetCustomizationUsage()
        {
            if (this.SaveFile != null &&
                this.SaveFile.SaveGame != null)
            {
                switch (this.SaveFile.SaveGame.PlayerClassDefinition)
                {
                    case "GD_Soldier.Character.CharClass_Soldier":
                    {
                        return CustomizationUsage.Soldier;
                    }

                    case "GD_Assassin.Character.CharClass_Assassin":
                    {
                        return CustomizationUsage.Assassin;
                    }

                    case "GD_Siren.Character.CharClass_Siren":
                    {
                        return CustomizationUsage.Siren;
                    }

                    case "GD_Mercenary.Character.CharClass_Mercenary":
                    {
                        return CustomizationUsage.Mercenary;
                    }

                    case "GD_Tulip_Mechromancer.Character.CharClass_Mechromancer":
                    {
                        return CustomizationUsage.Mechromancer;
                    }
                }
            }

            return CustomizationUsage.Unknown;
        }

        private void BuildCustomizationAssets()
        {
            var usage = this.GetCustomizationUsage();

            var headAssets = new List<AssetDisplay>();
            foreach (
                var kv in
                    InfoManager.Customizations.Items.Where(
                        kv => kv.Value.Type == CustomizationType.Head && kv.Value.Usage.Contains(usage) == true).OrderBy
                        (cd => cd.Value.Name))
            {
                headAssets.Add(new AssetDisplay(kv.Value.Name, kv.Key));
            }

            var skinAssets = new List<AssetDisplay>();
            foreach (
                var kv in
                    InfoManager.Customizations.Items.Where(
                        kv => kv.Value.Type == CustomizationType.Skin && kv.Value.Usage.Contains(usage) == true).OrderBy
                        (cd => cd.Value.Name))
            {
                skinAssets.Add(new AssetDisplay(kv.Value.Name, kv.Key));
            }

            var selectedHead = this.SelectedHead;
            this.HeadAssets.Clear();
            headAssets.ForEach(a => this.HeadAssets.Add(a));
            this.SelectedHead = selectedHead;

            var selectedSkin = this.SelectedSkin;
            this.SkinAssets.Clear();
            skinAssets.ForEach(a => this.SkinAssets.Add(a));
            this.SelectedSkin = selectedSkin;
        }

        public void Handle(SaveUnpackMessage message)
        {
            if (this.SaveFile != null)
            {
                this.SaveFile.SaveGame.PropertyChanged -= this.SaveGameOnPropertyChanged;
            }

            this.SaveFile = message.SaveFile;
            this.SaveFile.SaveGame.PropertyChanged += this.SaveGameOnPropertyChanged;

            this.SelectedHead = this.SaveFile.SaveGame.AppliedCustomizations[0];
            this.SelectedSkin = this.SaveFile.SaveGame.AppliedCustomizations[4];

            this.BuildCustomizationAssets();
        }

        private void SaveGameOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "PlayerClassDefinition")
            {
                this.BuildCustomizationAssets();
            }
        }

        public void Handle(SavePackMessage message)
        {
            if (this.SaveFile != null)
            {
                this.SaveFile.SaveGame.PropertyChanged -= this.SaveGameOnPropertyChanged;
            }

            this.SaveFile.SaveGame.AppliedCustomizations[0] = this.SelectedHead;
            this.SaveFile.SaveGame.AppliedCustomizations[4] = this.SelectedSkin;
        }
    }
}
