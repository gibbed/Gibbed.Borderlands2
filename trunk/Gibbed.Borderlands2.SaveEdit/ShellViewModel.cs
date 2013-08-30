﻿/* Copyright (c) 2013 Rick (rick 'at' gibbed 'dot' us)
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
using System.Text;
using System.Windows;
using Caliburn.Micro;
using Caliburn.Micro.Contrib;
using Caliburn.Micro.Contrib.Results;
using Gibbed.Borderlands2.GameInfo;

namespace Gibbed.Borderlands2.SaveEdit
{
    [Export(typeof(ShellViewModel))]
    internal class ShellViewModel : PropertyChangedBase
    {
        #region Fields
        private readonly IEventAggregator _Events;
        private readonly string _SavePath;
        private FileFormats.SaveFile _SaveFile;
        private GeneralViewModel _General;
        private CharacterViewModel _Character;
        private VehicleViewModel _Vehicle;
        private CurrencyOnHandViewModel _CurrencyOnHand;
        private BackpackViewModel _Backpack;
        private BankViewModel _Bank;
        private FastTravelViewModel _FastTravel;
        private AboutViewModel _About;

        private int _FilterIndex = 1;
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

        [ImportingConstructor]
        public ShellViewModel(IEventAggregator events)
        {
            var savePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (string.IsNullOrEmpty(savePath) == false)
            {
                savePath = Path.Combine(savePath, "My Games");
                savePath = Path.Combine(savePath, "Borderlands 2", "WillowGame", "SaveData");

                if (Directory.Exists(savePath) == true)
                {
                    this._SavePath = savePath;
                }
            }

            this._Events = events;
            events.Subscribe(this);
        }

        public IEnumerable<IResult> NewSave()
        {
            yield return new DelegateResult(() =>
            {
                var saveFile = new FileFormats.SaveFile()
                {
                    Platform = Platform.PC,
                    SaveGame = new ProtoBufFormats.WillowTwoSave.WillowTwoPlayerSaveGame()
                    {
                        SaveGameId = 1,
                        SaveGuid = (ProtoBufFormats.WillowTwoSave.Guid)Guid.NewGuid(),
                        PlayerClass = "GD_Assassin.Character.CharClass_Assassin",
                        UIPreferences = new ProtoBufFormats.WillowTwoSave.UIPreferencesData()
                        {
                            CharacterName = Encoding.UTF8.GetBytes("Zero"),
                        },
                        AppliedCustomizations = new List<string>()
                        {
                            "GD_DefaultCustoms_MainGame.Assassin.Head_Default",
                            "",
                            "",
                            "",
                            "GD_DefaultCustoms_MainGame.Assassin.Skin_Default",
                        },
                        LastVisitedTeleporter = "None",
                    },
                };
                saveFile.SaveGame.Decompose();

                this.General.ImportData(saveFile.SaveGame, saveFile.Platform);
                this.Character.ImportData(saveFile.SaveGame);
                this.Vehicle.ImportData(saveFile.SaveGame);
                this.CurrencyOnHand.ImportData(saveFile.SaveGame);
                this.Backpack.ImportData(saveFile.SaveGame, saveFile.Platform);
                this.Bank.ImportData(saveFile.SaveGame, saveFile.Platform);
                this.FastTravel.ImportData(saveFile.SaveGame);
                this.SaveFile = saveFile;
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
            int filterIndex = 0;

            MyOpenFileResult ofr;

            ofr = new MyOpenFileResult()
                .FilterFiles(
                    ffc => ffc.AddFilter("sav", this._FilterIndex == 1)
                              .WithDescription("PC Save Files")
                              .AddFilter("sav", this._FilterIndex == 2)
                              .WithDescription("X360 Save Files")
                              .AddFilter("sav", this._FilterIndex == 3)
                              .WithDescription("PS3 Save Files"))
                .WithFileDo(s => fileName = s)
                .WithFilterIndexDo(i => filterIndex = i);

            if (string.IsNullOrEmpty(this._SavePath) == false &&
                Directory.Exists(this._SavePath) == true)
            {
                ofr = ofr.In(this._SavePath);
            }

            yield return ofr;

            if (fileName == null)
            {
                yield break;
            }

            this._FilterIndex = filterIndex;

            var platforms = new[]
            {
                Platform.Invalid,
                Platform.PC,
                Platform.X360,
                Platform.PS3
            };

            var platform = filterIndex < 1 || filterIndex > 3
                               ? Platform.PC
                               : platforms[filterIndex];

            FileFormats.SaveFile saveFile = null;

            yield return new DelegateResult(() =>
            {
                using (var input = File.OpenRead(fileName))
                {
                    saveFile = FileFormats.SaveFile.Deserialize(input,
                                                                platform,
                                                                FileFormats.SaveFile.DeserializeSettings.None);
                }

                try
                {
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

            MySaveFileResult ofr;

            ofr = new MySaveFileResult()
                .PromptForOverwrite()
                .FilterFiles(
                    ffc => ffc.AddFilter("sav", true)
                              .WithDescription("Save Files")
                              .AddAllFilesFilter())
                .WithFileDo(s => fileName = s);

            if (string.IsNullOrEmpty(this._SavePath) == false &&
                Directory.Exists(this._SavePath) == true)
            {
                ofr = ofr.In(this._SavePath);
            }

            yield return ofr;

            if (fileName == null)
            {
                yield break;
            }

            var saveFile = this.SaveFile;

            yield return new DelegateResult(() =>
            {
                Platform platform;
                this.General.ExportData(saveFile.SaveGame, out platform);
                this.Character.ExportData(saveFile.SaveGame);
                this.Vehicle.ExportData(saveFile.SaveGame);
                this.CurrencyOnHand.ExportData(saveFile.SaveGame);
                this.Backpack.ExportData(saveFile.SaveGame, platform);
                this.Bank.ExportData(saveFile.SaveGame, platform);
                this.FastTravel.ExportData(saveFile.SaveGame);

                using (var output = File.Create(fileName))
                {
                    saveFile.Platform = platform;
                    saveFile.Serialize(output);
                }
            }).Rescue().Execute(
                x =>
                new MyMessageBox("An exception was thrown (press Ctrl+C to copy):\n\n" + x.ToString(), "Error")
                    .WithIcon(MessageBoxImage.Error).AsCoroutine());
        }
    }
}
