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
using Caliburn.Micro;
using Gibbed.Borderlands2.GameInfo;

namespace Gibbed.Borderlands2.SaveEdit
{
    [Export(typeof(SaveLoad))]
    internal class SaveLoad : PropertyChangedBase
    {
        private readonly string _SavePath;
        private int _FilterIndex = 1;

        public string SavePath
        {
            get { return this._SavePath; }
        }

        public SaveLoad()
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
        }

        public IEnumerable<IResult> OpenFile(Action<string> fileNameAction, Action<Platform> platformAction)
        {
            fileNameAction(null);
            platformAction(Platform.Invalid);

            string fileName = null;
            int filterIndex = -1;

            var ofr = new MyOpenFileResult()
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

            fileNameAction(fileName);
            platformAction(filterIndex < 1 || filterIndex > 3
                               ? Platform.PC
                               : platforms[filterIndex]);
        }

        public IEnumerable<IResult> SaveFile(Action<string> fileNameAction)
        {
            fileNameAction(null);

            string fileName = null;

            var ofr = new MySaveFileResult()
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

            fileNameAction(fileName);
        }
    }
}
