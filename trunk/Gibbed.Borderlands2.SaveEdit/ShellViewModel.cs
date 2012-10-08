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

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using Caliburn.Micro;
using Caliburn.Micro.Contrib;
using Caliburn.Micro.Contrib.Dialogs;
using Caliburn.Micro.Contrib.Results;
using Gibbed.IO;

namespace Gibbed.Borderlands2.SaveEdit
{
    [Export(typeof(ShellViewModel))]
    internal class ShellViewModel : PropertyChangedBase
    {
        #region Fields
        private readonly IEventAggregator _Events;
        private readonly string _SavePath;
        private FileFormats.SaveFile _SaveFile;
        private PlayerViewModel _Player;
        private CurrencyOnHandViewModel _CurrencyOnHand;
        private BackpackViewModel _Backpack;
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

        [Import(typeof(PlayerViewModel))]
        public PlayerViewModel Player
        {
            get { return this._Player; }

            set
            {
                this._Player = value;
                this.NotifyOfPropertyChange(() => this.Player);
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

        public IEnumerable<IResult> ReadSave()
        {
            string fileName = null;

            MyOpenFileResult ofd;

            ofd = new MyOpenFileResult()
                .FilterFiles(
                    ffc => ffc.AddFilter("sav", true)
                               .WithDescription("Borderlands 2 Save Files")
                               .AddAllFilesFilter())
                .WithFileDo(s => fileName = s);

            if (string.IsNullOrEmpty(this._SavePath) == false)
            {
                ofd = ofd.In(this._SavePath);
            }

            yield return ofd;
            if (fileName == null)
            {
                yield break;
            }

            using (var input = File.OpenRead(fileName))
            {
                var magic = input.ReadValueU32(Endian.Big);
                if (magic == 0x434F4E20)
                {
                    yield return new Error("Error",
                                           "You cannot directly open XBOX 360 CON files. Extract the save data using a tool like Modio first.",
                                           Answer.Ok)
                        .AsResult();
                    yield break;
                }

                input.Seek(0, SeekOrigin.Begin);
                try
                {
                    FileFormats.SaveFile saveFile;
                    saveFile = FileFormats.SaveFile.Deserialize(input, FileFormats.SaveFile.DeserializeSettings.None);
                    this.SaveFile = saveFile;
                    this._Events.Publish(new SaveUnpackMessage(saveFile));
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public IEnumerable<IResult> WriteSave()
        {
            if (this.SaveFile == null)
            {
                yield break;
            }

            string fileName = null;

            yield return new MySaveFileResult()
                .In(this._SavePath)
                .PromptForOverwrite()
                .FilterFiles(
                    ffc => ffc.AddFilter("sav", true).WithDescription("Borderlands 2 Save Files").AddAllFilesFilter())
                .WithFileDo(s => fileName = s);

            if (fileName == null)
            {
                yield break;
            }

            this._Events.Publish(new SavePackMessage(this.SaveFile));

            try
            {
                using (var output = File.Create(fileName))
                {
                    this.SaveFile.Serialize(output);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
