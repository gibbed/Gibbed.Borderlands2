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
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
using Caliburn.Micro;
using Caliburn.Micro.Contrib;
using Caliburn.Micro.Contrib.Results;
using Gibbed.Borderlands2.GameInfo;
using Gibbed.Borderlands2.ProtoBufFormats.WillowTwoSave;
using Guid = Gibbed.Borderlands2.ProtoBufFormats.WillowTwoSave.Guid;

namespace Gibbed.Borderlands2.SaveEdit
{
    [Export(typeof(GeneralViewModel))]
    internal class GeneralViewModel : PropertyChangedBase
    {
        #region Imports
        private ShellViewModel _Shell;
        private SaveLoad _SaveLoad;

        [Import(typeof(ShellViewModel))]
        public ShellViewModel Shell
        {
            get { return this._Shell; }
            set
            {
                this._Shell = value;
                this.NotifyOfPropertyChange(() => this.Shell);
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
        #endregion

        #region Fields
        private Platform _Platform;
        private System.Guid _SaveGuid;
        private int _SaveGameId;
        #endregion

        #region Properties
        public Platform Platform
        {
            get { return this._Platform; }
            set
            {
                if (this._Platform != value)
                {
                    this._Platform = value;
                    this.NotifyOfPropertyChange(() => this.Platform);
                }
            }
        }

        public System.Guid SaveGuid
        {
            get { return this._SaveGuid; }
            set
            {
                this._SaveGuid = value;
                this.NotifyOfPropertyChange(() => this.SaveGuid);
            }
        }

        public int SaveGameId
        {
            get { return this._SaveGameId; }
            set
            {
                this._SaveGameId = value;
                this.NotifyOfPropertyChange(() => this.SaveGameId);
            }
        }

        public ObservableCollection<PlatformDisplay> Platforms { get; private set; }
        #endregion

        [ImportingConstructor]
        public GeneralViewModel()
        {
            this.Platforms = new ObservableCollection<PlatformDisplay>
            {
                new PlatformDisplay("PC", Platform.PC),
                new PlatformDisplay("360", Platform.X360),
                new PlatformDisplay("PS3", Platform.PS3),
            };
        }

        public void RandomizeSaveGuid()
        {
            this.SaveGuid = System.Guid.NewGuid();
        }

        public IEnumerable<IResult> DoImportSkills()
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

            yield return new DelegateResult(() =>
            {
                using (var input = File.OpenRead(fileName))
                {
                    saveFile = FileFormats.SaveFile.Deserialize(input,
                                                                platform,
                                                                FileFormats.SaveFile.DeserializeSettings.None);
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

            if (saveFile != null)
            {
                // TODO: deep copy?
                this.Shell.SaveFile.SaveGame.SkillData = saveFile.SaveGame.SkillData;
                yield return
                    new MyMessageBox("Import successful.")
                        .WithButton(MessageBoxButton.OK)
                        .WithIcon(MessageBoxImage.Information);
            }
        }

        public IEnumerable<IResult> DoImportMissions()
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

            yield return new DelegateResult(() =>
            {
                using (var input = File.OpenRead(fileName))
                {
                    saveFile = FileFormats.SaveFile.Deserialize(input,
                                                                platform,
                                                                FileFormats.SaveFile.DeserializeSettings.None);
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

            if (saveFile != null)
            {
                // TODO: deep copy?
                this.Shell.SaveFile.SaveGame.MissionPlaythroughs = saveFile.SaveGame.MissionPlaythroughs;
                yield return
                    new MyMessageBox("Import successful.")
                        .WithButton(MessageBoxButton.OK)
                        .WithIcon(MessageBoxImage.Information);
            }
        }

        public IEnumerable<IResult> DoImportWorld()
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

            yield return new DelegateResult(() =>
            {
                using (var input = File.OpenRead(fileName))
                {
                    saveFile = FileFormats.SaveFile.Deserialize(input,
                                                                platform,
                                                                FileFormats.SaveFile.DeserializeSettings.None);
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

            if (saveFile != null)
            {
                // TODO: deep copy?
                this.Shell.SaveFile.SaveGame.RegionGameStages = saveFile.SaveGame.RegionGameStages;
                this.Shell.SaveFile.SaveGame.WorldDiscoveryList = saveFile.SaveGame.WorldDiscoveryList;
                this.Shell.SaveFile.SaveGame.FullyExploredAreas = saveFile.SaveGame.FullyExploredAreas;
                yield return
                    new MyMessageBox("Import successful.")
                        .WithButton(MessageBoxButton.OK)
                        .WithIcon(MessageBoxImage.Information);
            }
        }

        public IEnumerable<IResult> DoImportStats()
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

            yield return new DelegateResult(() =>
            {
                using (var input = File.OpenRead(fileName))
                {
                    saveFile = FileFormats.SaveFile.Deserialize(input,
                                                                platform,
                                                                FileFormats.SaveFile.DeserializeSettings.None);
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

            if (saveFile != null)
            {
                // TODO: deep copy?
                this.Shell.SaveFile.SaveGame.StatsData = saveFile.SaveGame.StatsData;
                yield return
                    new MyMessageBox("Import successful.")
                        .WithButton(MessageBoxButton.OK)
                        .WithIcon(MessageBoxImage.Information);
            }
        }

        public void ImportData(WillowTwoPlayerSaveGame saveGame, Platform platform)
        {
            this.Platform = platform;
            this.SaveGuid = (System.Guid)saveGame.SaveGuid;
            this.SaveGameId = saveGame.SaveGameId;
        }

        public void ExportData(WillowTwoPlayerSaveGame saveGame, out Platform platform)
        {
            platform = this.Platform;
            saveGame.SaveGuid = (Guid)this.SaveGuid;
            saveGame.SaveGameId = this.SaveGameId;
        }

        internal class PlatformDisplay
        {
            public string Name { get; private set; }
            public Platform Value { get; private set; }

            public PlatformDisplay(string name, Platform value)
            {
                this.Name = name;
                this.Value = value;
            }
        }
    }
}
