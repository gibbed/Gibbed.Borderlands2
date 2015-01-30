/* Copyright (c) 2015 Rick (rick 'at' gibbed 'dot' us)
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
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using Caliburn.Micro.Contrib;
using Caliburn.Micro.Contrib.Results;
using Gibbed.Borderlands2.GameInfo;

namespace Gibbed.Borderlands2.SaveEdit
{
    [Export(typeof(ShellViewModel))]
    internal class ShellViewModel : PropertyChangedBase
    {
        #region Imports
        private GeneralViewModel _General;
        private CharacterViewModel _Character;
        private VehicleViewModel _Vehicle;
        private CurrencyOnHandViewModel _CurrencyOnHand;
        private BackpackViewModel _Backpack;
        private BankViewModel _Bank;
        private FastTravelViewModel _FastTravel;
        private AboutViewModel _About;

        [Import(typeof(GeneralViewModel))]
        public GeneralViewModel General
        {
            get { return this._General; }

            set
            {
                this._General = value;
                this.NotifyOfPropertyChange(() => this.General);
            }
        }

        [Import(typeof(CharacterViewModel))]
        public CharacterViewModel Character
        {
            get { return this._Character; }

            set
            {
                this._Character = value;
                this.NotifyOfPropertyChange(() => this.Character);
            }
        }

        [Import(typeof(VehicleViewModel))]
        public VehicleViewModel Vehicle
        {
            get { return this._Vehicle; }

            set
            {
                this._Vehicle = value;
                this.NotifyOfPropertyChange(() => this.Vehicle);
            }
        }

        [Import(typeof(CurrencyOnHandViewModel))]
        public CurrencyOnHandViewModel CurrencyOnHand
        {
            get { return this._CurrencyOnHand; }

            set
            {
                this._CurrencyOnHand = value;
                this.NotifyOfPropertyChange(() => this.CurrencyOnHand);
            }
        }

        [Import(typeof(BackpackViewModel))]
        public BackpackViewModel Backpack
        {
            get { return this._Backpack; }

            set
            {
                this._Backpack = value;
                this.NotifyOfPropertyChange(() => this.Backpack);
            }
        }

        [Import(typeof(BankViewModel))]
        public BankViewModel Bank
        {
            get { return this._Bank; }

            set
            {
                this._Bank = value;
                this.NotifyOfPropertyChange(() => this.Bank);
            }
        }

        [Import(typeof(FastTravelViewModel))]
        public FastTravelViewModel FastTravel
        {
            get { return this._FastTravel; }

            set
            {
                this._FastTravel = value;
                this.NotifyOfPropertyChange(() => this.FastTravel);
            }
        }

        [Import(typeof(AboutViewModel))]
        public AboutViewModel About
        {
            get { return this._About; }

            set
            {
                this._About = value;
                this.NotifyOfPropertyChange(() => this.About);
            }
        }
        #endregion

        #region Fields
        private SaveLoad _SaveLoad;
        private FileFormats.SaveFile _SaveFile;
        private readonly ICommand _NewSaveFromPlayerClass;
        #endregion

        #region Properties
        public IEnumerable<PlayerClassDefinition> PlayerClasses
        {
            get
            {
                return InfoManager.PlayerClasses.Items
                                  .Select(kv => kv.Value)
                                  .Distinct()
                                  .OrderBy(dp => dp.SortOrder);
            }
        }

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

        [Import(typeof(SaveLoad))]
        public SaveLoad SaveLoad
        {
            get { return this._SaveLoad; }

            set
            {
                this._SaveLoad = value;
                this.NotifyOfPropertyChange(() => this.SaveLoad);
            }
        }

        public ICommand NewSaveFromPlayerClass
        {
            get { return this._NewSaveFromPlayerClass; }
        }
        #endregion

        [ImportingConstructor]
        public ShellViewModel()
        {
            this._NewSaveFromPlayerClass = new DelegateCommand<PlayerClassDefinition>(this.DoNewSaveFromPlayerClass);
        }

        private void DoNewSaveFromPlayerClass(PlayerClassDefinition playerClass)
        {
            var saveFile = new FileFormats.SaveFile()
            {
                Platform = Platform.PC,
                SaveGame = new ProtoBufFormats.WillowTwoSave.WillowTwoPlayerSaveGame()
                {
                    SaveGameId = 1,
                    SaveGuid = (ProtoBufFormats.WillowTwoSave.Guid)Guid.NewGuid(),
                    PlayerClass = playerClass.ResourcePath,
                    UIPreferences = new ProtoBufFormats.WillowTwoSave.UIPreferencesData()
                    {
                        CharacterName = Encoding.UTF8.GetBytes(playerClass.Name),
                    },
                    AppliedCustomizations = new List<string>()
                    {
                        "None",
                        "",
                        "",
                        "",
                        "None",
                    },
                },
            };

            FileFormats.SaveExpansion.ExtractExpansionSavedataFromUnloadableItemData(saveFile.SaveGame);

            this.General.ImportData(saveFile.SaveGame, saveFile.Platform);
            this.Character.ImportData(saveFile.SaveGame);
            this.Vehicle.ImportData(saveFile.SaveGame);
            this.CurrencyOnHand.ImportData(saveFile.SaveGame);
            this.Backpack.ImportData(saveFile.SaveGame, saveFile.Platform);
            this.Bank.ImportData(saveFile.SaveGame, saveFile.Platform);
            this.FastTravel.ImportData(saveFile.SaveGame);
            this.SaveFile = saveFile;
        }

        public IEnumerable<IResult> NewSave()
        {
            yield return new DelegateResult(
                () =>
                {
                    var playerClasses =
                        InfoManager.PlayerClasses.Items.Where(pc => pc.Value.DLC == null)
                                   .Select(kv => kv.Value)
                                   .ToArray();
                    var playerClass = playerClasses[new Random().Next(playerClasses.Length)];
                    this.DoNewSaveFromPlayerClass(playerClass);
                })
                .Rescue<DllNotFoundException>().Execute(
                    x => new MyMessageBox("Failed to create save: " + x.Message, "Error")
                             .WithIcon(MessageBoxImage.Error).AsCoroutine())
                .Rescue<FileFormats.SaveFormatException>().Execute(
                    x => new MyMessageBox("Failed to create save: " + x.Message, "Error")
                             .WithIcon(MessageBoxImage.Error).AsCoroutine())
                .Rescue<FileFormats.SaveCorruptionException>().Execute(
                    x => new MyMessageBox("Failed to create save: " + x.Message, "Error")
                             .WithIcon(MessageBoxImage.Error).AsCoroutine())
                .Rescue().Execute(
                    x =>
                    new MyMessageBox("An exception was thrown (press Ctrl+C to copy):\n\n" + x.ToString(),
                                     "Error")
                        .WithIcon(MessageBoxImage.Error).AsCoroutine());
        }

        public IEnumerable<IResult> ReadSave()
        {
            string fileName = null;
            var platform = Platform.Invalid;

            foreach (var result in this.SaveLoad.OpenFile(s => fileName = s, p => platform = p))
            {
                yield return result;
            }

            if (fileName == null)
            {
                yield break;
            }

            FileFormats.SaveFile saveFile = null;

            yield return new DelegateResult(
                () =>
                {
                    using (var input = File.OpenRead(fileName))
                    {
                        saveFile = FileFormats.SaveFile.Deserialize(
                            input,
                            platform,
                            FileFormats.SaveFile.DeserializeSettings.None);
                    }

                    try
                    {
                        FileFormats.SaveExpansion.ExtractExpansionSavedataFromUnloadableItemData(
                            saveFile.SaveGame);

                        this.General.ImportData(saveFile.SaveGame, saveFile.Platform);
                        this.Character.ImportData(saveFile.SaveGame);
                        this.Vehicle.ImportData(saveFile.SaveGame);
                        this.CurrencyOnHand.ImportData(saveFile.SaveGame);
                        this.Backpack.ImportData(saveFile.SaveGame, saveFile.Platform);
                        this.Bank.ImportData(saveFile.SaveGame, saveFile.Platform);
                        this.FastTravel.ImportData(saveFile.SaveGame);
                        this.SaveFile = saveFile;
                    }
                    catch (Exception)
                    {
                        this.SaveFile = null;
                        throw;
                    }
                })
                .Rescue<DllNotFoundException>().Execute(
                    x => new MyMessageBox("Failed to load save: " + x.Message, "Error")
                             .WithIcon(MessageBoxImage.Error).AsCoroutine())
                .Rescue<FileFormats.SaveFormatException>().Execute(
                    x => new MyMessageBox("Failed to load save: " + x.Message, "Error")
                             .WithIcon(MessageBoxImage.Error).AsCoroutine())
                .Rescue<FileFormats.SaveCorruptionException>().Execute(
                    x => new MyMessageBox("Failed to load save: " + x.Message, "Error")
                             .WithIcon(MessageBoxImage.Error).AsCoroutine())
                .Rescue().Execute(
                    x =>
                    new MyMessageBox("An exception was thrown (press Ctrl+C to copy):\n\n" + x.ToString(),
                                     "Error")
                        .WithIcon(MessageBoxImage.Error).AsCoroutine());


            if (saveFile != null &&
                saveFile.SaveGame.IsBadassModeSaveGame == true)
            {
                saveFile.SaveGame.IsBadassModeSaveGame = false;
                yield return
                    new MyMessageBox("Your save file was set as 'Badass Mode', and this has now been cleared.\n\n" +
                                     "See http://bit.ly/graveyardsav for more details.",
                                     "Information")
                        .WithIcon(MessageBoxImage.Information);
            }


            if (this.SaveFile != null &&
                this.Backpack.BrokenWeapons.Count > 0)
            {
                var result = MessageBoxResult.No;
                do
                {
                    yield return
                        new MyMessageBox(
                            "There were weapons in the backpack that failed to load. Do you want to remove them?\n\n" +
                            "If you choose not to remove them, they will remain in your save but will not be editable." +
                            (result != MessageBoxResult.Cancel
                                 ? "\n\nChoose Cancel to copy error information to the clipboard."
                                 : ""),
                            "Warning")
                            .WithButton(result != MessageBoxResult.Cancel
                                            ? MessageBoxButton.YesNoCancel
                                            : MessageBoxButton.YesNo)
                            .WithDefaultResult(MessageBoxResult.No)
                            .WithResultDo(r => result = r)
                            .WithIcon(MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        this.Backpack.BrokenWeapons.Clear();
                    }
                    else if (result == MessageBoxResult.Cancel)
                    {
                        var sb = new StringBuilder();
                        this.Backpack.BrokenWeapons.ForEach(kv =>
                                                            {
                                                                sb.AppendLine(kv.Value.ToString());
                                                                sb.AppendLine();
                                                            });
                        if (MyClipboard.SetText(sb.ToString()) != MyClipboard.Result.Success)
                        {
                            MessageBox.Show("Clipboard failure.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                while (result == MessageBoxResult.Cancel);
            }

            if (this.SaveFile != null &&
                this.Backpack.BrokenItems.Count > 0)
            {
                var result = MessageBoxResult.No;
                do
                {
                    yield return
                        new MyMessageBox(
                            "There were items in the backpack that failed to load. Do you want to remove them?\n\n" +
                            "If you choose not to remove them, they will remain in your save but will not be editable." +
                            (result != MessageBoxResult.Cancel
                                 ? "\n\nChoose Cancel to copy error information to the clipboard."
                                 : ""),
                            "Warning")
                            .WithButton(result != MessageBoxResult.Cancel
                                            ? MessageBoxButton.YesNoCancel
                                            : MessageBoxButton.YesNo)
                            .WithDefaultResult(MessageBoxResult.No)
                            .WithResultDo(r => result = r)
                            .WithIcon(MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        this.Backpack.BrokenItems.Clear();
                    }
                    else if (result == MessageBoxResult.Cancel)
                    {
                        var sb = new StringBuilder();
                        this.Backpack.BrokenItems.ForEach(kv =>
                                                          {
                                                              sb.AppendLine(kv.Value.ToString());
                                                              sb.AppendLine();
                                                          });
                        if (MyClipboard.SetText(sb.ToString()) != MyClipboard.Result.Success)
                        {
                            MessageBox.Show("Clipboard failure.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                while (result == MessageBoxResult.Cancel);
            }

            if (this.SaveFile != null &&
                this.Bank.BrokenSlots.Count > 0)
            {
                var result = MessageBoxResult.No;
                do
                {
                    yield return
                        new MyMessageBox(
                            "There were weapons or items in the bank that failed to load. Do you want to remove them?\n\n" +
                            "If you choose not to remove them, they will remain in your save but will not be editable." +
                            (result != MessageBoxResult.Cancel
                                 ? "\n\nChoose Cancel to copy error information to the clipboard."
                                 : ""),
                            "Warning")
                            .WithButton(result != MessageBoxResult.Cancel
                                            ? MessageBoxButton.YesNoCancel
                                            : MessageBoxButton.YesNo)
                            .WithDefaultResult(MessageBoxResult.No)
                            .WithResultDo(r => result = r)
                            .WithIcon(MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        this.Bank.BrokenSlots.Clear();
                    }
                    else if (result == MessageBoxResult.Cancel)
                    {
                        var sb = new StringBuilder();
                        this.Bank.BrokenSlots.ForEach(kv =>
                                                      {
                                                          sb.AppendLine(kv.Value.ToString());
                                                          sb.AppendLine();
                                                      });
                        if (MyClipboard.SetText(sb.ToString()) != MyClipboard.Result.Success)
                        {
                            MessageBox.Show("Clipboard failure.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                while (result == MessageBoxResult.Cancel);
            }
        }

        public IEnumerable<IResult> WriteSave()
        {
            if (this.SaveFile == null)
            {
                yield break;
            }

            string fileName = null;

            foreach (var result in this.SaveLoad.SaveFile(s => fileName = s))
            {
                yield return result;
            }

            if (fileName == null)
            {
                yield break;
            }

            var saveFile = this.SaveFile;

            yield return new DelegateResult(
                () =>
                {
                    Platform platform;
                    this.General.ExportData(saveFile.SaveGame, out platform);
                    this.Character.ExportData(saveFile.SaveGame);
                    this.Vehicle.ExportData(saveFile.SaveGame);
                    this.CurrencyOnHand.ExportData(saveFile.SaveGame);
                    this.Backpack.ExportData(saveFile.SaveGame, platform);
                    this.Bank.ExportData(saveFile.SaveGame, platform);
                    this.FastTravel.ExportData(saveFile.SaveGame);

                    if (saveFile.SaveGame != null &&
                        saveFile.SaveGame.WeaponData != null)
                    {
                        saveFile.SaveGame.WeaponData.RemoveAll(
                            wd =>
                            Blacklisting.IsBlacklistedType(wd.Type) == true ||
                            Blacklisting.IsBlacklistedBalance(wd.Balance) == true);
                    }

                    if (saveFile.SaveGame != null &&
                        saveFile.SaveGame.ItemData != null)
                    {
                        saveFile.SaveGame.ItemData.RemoveAll(
                            wd =>
                            Blacklisting.IsBlacklistedType(wd.Type) == true ||
                            Blacklisting.IsBlacklistedBalance(wd.Balance) == true);
                    }

                    using (var output = File.Create(fileName))
                    {
                        FileFormats.SaveExpansion.AddExpansionSavedataToUnloadableItemData(
                            saveFile.SaveGame);
                        saveFile.Platform = platform;
                        saveFile.Serialize(output);
                        FileFormats.SaveExpansion
                                   .ExtractExpansionSavedataFromUnloadableItemData(
                                       saveFile.SaveGame);
                    }
                }).Rescue().Execute(
                    x =>
                    new MyMessageBox(
                        "An exception was thrown (press Ctrl+C to copy):\n\n" + x.ToString(),
                        "Error")
                        .WithIcon(MessageBoxImage.Error).AsCoroutine());
        }
    }
}
