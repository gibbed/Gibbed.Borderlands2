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

using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using Gibbed.Borderlands2.GameInfo;

namespace Gibbed.Borderlands2.SaveEdit.Validators
{
    internal abstract class AssetValidationRule : ValidationRule
    {
        private int _AssetLibrarySetId = -1;
        private string[] _Assets = new string[0];

        protected abstract string[] GetAssets();

        protected AssetLibrarySet AssetLibrarySet
        {
            get { return InfoManager.AssetLibraryManager.GetSet(this.AssetLibrarySetId); }
        }

        public int AssetLibrarySetId
        {
            get { return this._AssetLibrarySetId; }
            set
            {
                if (value != this._AssetLibrarySetId)
                {
                    this._AssetLibrarySetId = value;
                    this._Assets = this.GetAssets();
                }
            }
        }

        protected static string[] GetAvailableAssets(AssetLibrary assetLibrary)
        {
            return new[]
            {
                "None"
            }.Concat(
                assetLibrary.Sublibraries.SelectMany(sl => sl.Assets.Select(a => sl.Package + "." + a)).Distinct().
                    OrderBy(a => a)).ToArray();
        }

        public override ValidationResult Validate(object value, CultureInfo ultureInfo)
        {
            if (value == null ||
                this._Assets.Contains(value.ToString()) == false)
            {
                return new ValidationResult(false, "The value is not a valid asset");
            }

            return new ValidationResult(true, null);
        }
    }
}
