/* Copyright (c) 2013 Rick (rick 'at' gibbed 'dot' us)
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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using Caliburn.Micro.Contrib.Results;
using Gibbed.Borderlands2.FileFormats.Items;
using Gibbed.Borderlands2.GameInfo;
using Gibbed.Borderlands2.ProtoBufFormats.WillowTwoSave;

namespace Gibbed.Borderlands2.SaveEdit
{
    [Export(typeof(BackpackViewModel))]
    internal class BackpackViewModel : PropertyChangedBase
    {
        #region Imports
        private CharacterViewModel _Character;
        private BankViewModel _Bank;

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
        #endregion

        #region Fields
        private readonly ObservableCollection<IBackpackSlotViewModel> _Slots;

        // TODO: STUPID STUPID STUPID STUPID WHY GEARBOX WHY?????????????????????
        private readonly List<PackedItemData> _ExpansionItems;

        private readonly List<KeyValuePair<PackedWeaponData, Exception>> _BrokenWeapons;
        private readonly List<KeyValuePair<PackedItemData, Exception>> _BrokenItems;

        private IBackpackSlotViewModel _SelectedSlot;

        private ICommand _NewWeapon;
        private ICommand _NewItem;
        #endregion

        #region Properties
        public IEnumerable<DownloadablePackageDefinition> DownloadablePackages
        {
            get
            {
                return
                    InfoManager.DownloadableContents.Items
                               .Where(dc => dc.Value.Type == DownloadableContentType.ItemSet &&
                                            dc.Value.Package != null)
                               .Select(dc => dc.Value.Package)
                               .Where(dp => InfoManager.AssetLibraryManager.Sets.Any(s => s.Id == dp.Id) == true)
                               .Distinct()
                               .OrderBy(dp => dp.Id);
            }
        }

        public ObservableCollection<IBackpackSlotViewModel> Slots
        {
            get { return this._Slots; }
        }

        public List<KeyValuePair<PackedWeaponData, Exception>> BrokenWeapons
        {
            get { return this._BrokenWeapons; }
        }

        public List<KeyValuePair<PackedItemData, Exception>> BrokenItems
        {
            get { return this._BrokenItems; }
        }

        public IBackpackSlotViewModel SelectedSlot
        {
            get { return this._SelectedSlot; }
            set
            {
                this._SelectedSlot = value;
                this.NotifyOfPropertyChange(() => this.SelectedSlot);
            }
        }

        public ICommand NewWeapon
        {
            get { return this._NewWeapon; }
        }

        public ICommand NewItem
        {
            get { return this._NewItem; }
        }
        #endregion

        [ImportingConstructor]
        public BackpackViewModel()
        {
            this._Slots = new ObservableCollection<IBackpackSlotViewModel>();
            this._ExpansionItems = new List<PackedItemData>();
            this._BrokenWeapons = new List<KeyValuePair<PackedWeaponData, Exception>>();
            this._BrokenItems = new List<KeyValuePair<PackedItemData, Exception>>();
            this._NewWeapon = new DelegateCommand<int>(this.DoNewWeapon);
            this._NewItem = new DelegateCommand<int>(this.DoNewItem);
        }

        public void DoNewWeapon(int assetLibrarySetId)
        {
            var weapon = new BackpackWeapon()
            {
                UniqueId = new Random().Next(int.MinValue, int.MaxValue),
                // TODO: check other item unique IDs to prevent rare collisions
                AssetLibrarySetId = assetLibrarySetId,
            };
            var viewModel = new BackpackWeaponViewModel(weapon);
            this.Slots.Add(viewModel);
            this.SelectedSlot = viewModel;
        }

        public void DoNewItem(int assetLibrarySetId)
        {
            var item = new BackpackItem()
            {
                UniqueId = new Random().Next(int.MinValue, int.MaxValue),
                // TODO: check other item unique IDs to prevent rare collisions
                AssetLibrarySetId = assetLibrarySetId,
            };
            var viewModel = new BackpackItemViewModel(item);
            this.Slots.Add(viewModel);
            this.SelectedSlot = viewModel;
        }

        private static readonly Regex _CodeSignature =
            new Regex(@"BL2\((?<data>(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?)\)",
                      RegexOptions.CultureInvariant | RegexOptions.Multiline);

        public IEnumerable<IResult> PasteCode()
        {
            bool containsText;
            bool containsUnicodeText = false;
            if (MyClipboard.ContainsText(TextDataFormat.Text, out containsText) != MyClipboard.Result.Success ||
                MyClipboard.ContainsText(TextDataFormat.UnicodeText, out containsUnicodeText) !=
                MyClipboard.Result.Success)
            {
                yield return new MyMessageBox("Clipboard failure.", "Error")
                    .WithIcon(MessageBoxImage.Error);
            }

            if (containsText == false &&
                containsUnicodeText == false)
            {
                yield break;
            }

            var errors = 0;
            var viewModels = new List<IBackpackSlotViewModel>();
            yield return new DelegateResult(() =>
            {
                string codes;
                if (MyClipboard.GetText(out codes) != MyClipboard.Result.Success)
                {
                    MessageBox.Show("Clipboard failure.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // strip whitespace
                codes = Regex.Replace(codes, @"\s+", "");

                foreach (var match in _CodeSignature.Matches(codes).Cast<Match>()
                                                    .Where(m => m.Success == true))
                {
                    var code = match.Groups["data"].Value;

                    IPackable packable;

                    try
                    {
                        var data = Convert.FromBase64String(code);
                        packable = BackpackDataHelper.Decode(data, Platform.PC);
                    }
                    catch (Exception)
                    {
                        errors++;
                        continue;
                    }

                    // TODO: check other item unique IDs to prevent rare collisions
                    packable.UniqueId = new Random().Next(int.MinValue, int.MaxValue);

                    if (packable is BackpackWeapon)
                    {
                        var weapon = (BackpackWeapon)packable;
                        weapon.QuickSlot = QuickWeaponSlot.None;
                        weapon.Mark = PlayerMark.Standard;
                        var viewModel = new BackpackWeaponViewModel(weapon);
                        viewModels.Add(viewModel);
                    }
                    else if (packable is BackpackItem)
                    {
                        var item = (BackpackItem)packable;
                        item.Quantity = 1;
                        item.Equipped = false;
                        item.Mark = PlayerMark.Standard;
                        var viewModel = new BackpackItemViewModel(item);
                        viewModels.Add(viewModel);
                    }
                }
            });

            if (viewModels.Count > 0)
            {
                viewModels.ForEach(vm => this.Slots.Add(vm));
                this.SelectedSlot = viewModels.First();
            }

            if (errors > 0)
            {
                yield return
                    new MyMessageBox("Failed to load " + errors.ToString(CultureInfo.InvariantCulture) + " codes.",
                                     "Warning")
                        .WithIcon(MessageBoxImage.Warning);
            }
            else if (viewModels.Count == 0)
            {
                yield return
                    new MyMessageBox("Did not find any codes in clipboard.",
                                     "Warning")
                        .WithIcon(MessageBoxImage.Warning);
            }
        }

        public IEnumerable<IResult> CopySelectedSlotCode()
        {
            yield return new DelegateResult(() =>
            {
                if (this.SelectedSlot == null ||
                    (this.SelectedSlot.BackpackSlot is IPackable) == false)
                {
                    if (MyClipboard.SetText("") != MyClipboard.Result.Success)
                    {
                        MessageBox.Show("Clipboard failure.",
                                        "Error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                    }
                    return;
                }

                // just a hack until I add a way to override the unique ID in Encode()
                var copy = (IPackable)this.SelectedSlot.BackpackSlot.Clone();
                copy.UniqueId = 0;

                var data = BackpackDataHelper.Encode(copy, Platform.PC);
                var sb = new StringBuilder();
                sb.Append("BL2(");
                sb.Append(Convert.ToBase64String(data, Base64FormattingOptions.None));
                sb.Append(")");

                /*
                if (MyClipboard.SetText(sb.ToString()) != MyClipboard.Result.Success)
                {
                    MessageBox.Show("Clipboard failure.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                */

                var dobj = new DataObject();
                dobj.SetText(sb.ToString());
                if (MyClipboard.SetDataObject(dobj, false) != MyClipboard.Result.Success)
                {
                    MessageBox.Show("Clipboard failure.",
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                }
            });
        }

        public void DuplicateSelectedSlot()
        {
            if (this.SelectedSlot == null)
            {
                return;
            }

            var copy = (IBackpackSlot)this.SelectedSlot.BackpackSlot.Clone();
            copy.UniqueId = new Random().Next(int.MinValue, int.MaxValue);
            // TODO: check other item unique IDs to prevent rare collisions

            if (copy is BackpackWeapon)
            {
                var weapon = (BackpackWeapon)copy;
                weapon.QuickSlot = QuickWeaponSlot.None;
                weapon.Mark = PlayerMark.Standard;

                var viewModel = new BackpackWeaponViewModel(weapon);
                this.Slots.Add(viewModel);
                this.SelectedSlot = viewModel;
            }
            else if (copy is BackpackItem)
            {
                var item = (BackpackItem)copy;
                item.Equipped = false;
                item.Mark = PlayerMark.Standard;

                var viewModel = new BackpackItemViewModel(item);
                this.Slots.Add(viewModel);
                this.SelectedSlot = viewModel;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public void BankSelectedSlot()
        {
            // TODO: implement me
        }

        public void DeleteSelectedSlot()
        {
            if (this.SelectedSlot == null)
            {
                return;
            }

            this.Slots.Remove(this.SelectedSlot);
            this.SelectedSlot = this.Slots.FirstOrDefault();
        }

        public void SyncEquippedLevels()
        {
            foreach (var viewModel in this.Slots)
            {
                if (viewModel is BackpackWeaponViewModel)
                {
                    var weapon = (BackpackWeaponViewModel)viewModel;
                    if (weapon.QuickSlot != QuickWeaponSlot.None &&
                        (weapon.ManufacturerGradeIndex + weapon.GameStage) >= 2)
                    {
                        weapon.ManufacturerGradeIndex = this.Character.SyncLevel;
                        weapon.GameStage = this.Character.SyncLevel;
                    }
                }
                else if (viewModel is BackpackItemViewModel)
                {
                    var item = (BackpackItemViewModel)viewModel;
                    if (item.Equipped == true &&
                        (item.ManufacturerGradeIndex + item.GameStage) >= 2)
                    {
                        item.ManufacturerGradeIndex = this.Character.SyncLevel;
                        item.GameStage = this.Character.SyncLevel;
                    }
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }

        public void SyncAllLevels()
        {
            foreach (var viewModel in this.Slots)
            {
                if (viewModel is BackpackWeaponViewModel)
                {
                    var weapon = (BackpackWeaponViewModel)viewModel;
                    if ((weapon.ManufacturerGradeIndex + weapon.GameStage) >= 2)
                    {
                        weapon.ManufacturerGradeIndex = this.Character.SyncLevel;
                        weapon.GameStage = this.Character.SyncLevel;
                    }
                }
                else if (viewModel is BackpackItemViewModel)
                {
                    var item = (BackpackItemViewModel)viewModel;
                    if ((item.ManufacturerGradeIndex + item.GameStage) >= 2)
                    {
                        item.ManufacturerGradeIndex = this.Character.SyncLevel;
                        item.GameStage = this.Character.SyncLevel;
                    }
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }

        public void ImportData(WillowTwoPlayerSaveGame saveGame, Platform platform)
        {
            this.Slots.Clear();

            this._BrokenWeapons.Clear();
            foreach (var packedWeapon in saveGame.PackedWeaponData)
            {
                BackpackWeapon weapon;
                try
                {
                    weapon = (BackpackWeapon)BackpackDataHelper.Decode(packedWeapon.InventorySerialNumber, platform);
                }
                catch (Exception e)
                {
                    this._BrokenWeapons.Add(new KeyValuePair<PackedWeaponData, Exception>(packedWeapon, e));
                    continue;
                }

                var test = BackpackDataHelper.Encode(weapon, platform);
                if (packedWeapon.InventorySerialNumber.SequenceEqual(test) == false)
                {
                    throw new FormatException("backpack weapon reencode mismatch");
                }

                weapon.QuickSlot = packedWeapon.QuickSlot;
                weapon.Mark = packedWeapon.Mark;

                var viewModel = new BackpackWeaponViewModel(weapon);
                this.Slots.Add(viewModel);
            }

            this._ExpansionItems.Clear();
            this._BrokenItems.Clear();
            foreach (var packedItem in saveGame.PackedItemData)
            {
                // TODO: STUPID STUPID STUPID STUPID WHY GEARBOX WHY?????????????????????
                if (packedItem.Quantity < 0)
                {
                    this._ExpansionItems.Add(packedItem);
                    continue;
                }

                BackpackItem item;
                try
                {
                    item = (BackpackItem)BackpackDataHelper.Decode(packedItem.InventorySerialNumber, platform);
                }
                catch (Exception e)
                {
                    this._BrokenItems.Add(new KeyValuePair<PackedItemData, Exception>(packedItem, e));
                    continue;
                }

                var test = BackpackDataHelper.Encode(item, platform);
                if (packedItem.InventorySerialNumber.SequenceEqual(test) == false)
                {
                    throw new FormatException("backpack item reencode mismatch");
                }

                item.Quantity = packedItem.Quantity;
                item.Equipped = packedItem.Equipped;
                item.Mark = (PlayerMark)packedItem.Mark;

                // required since protobuf is no longer doing the validation for us
                if (item.Mark != PlayerMark.Trash &&
                    item.Mark != PlayerMark.Standard &&
                    item.Mark != PlayerMark.Favorite)
                {
                    throw new FormatException("invalid PlayerMark value");
                }

                var viewModel = new BackpackItemViewModel(item);
                this.Slots.Add(viewModel);
            }
        }

        public void ExportData(WillowTwoPlayerSaveGame saveGame, Platform platform)
        {
            saveGame.PackedWeaponData.Clear();
            saveGame.PackedItemData.Clear();

            foreach (var viewModel in this.Slots)
            {
                var slot = viewModel.BackpackSlot;

                if (slot is BackpackWeapon)
                {
                    var weapon = (BackpackWeapon)slot;
                    var data = BackpackDataHelper.Encode(weapon, platform);

                    saveGame.PackedWeaponData.Add(new PackedWeaponData()
                    {
                        InventorySerialNumber = data,
                        QuickSlot = weapon.QuickSlot,
                        Mark = weapon.Mark,
                    });
                }
                else if (slot is BackpackItem)
                {
                    var item = (BackpackItem)slot;
                    var data = BackpackDataHelper.Encode(item, platform);

                    saveGame.PackedItemData.Add(new PackedItemData()
                    {
                        InventorySerialNumber = data,
                        Quantity = item.Quantity,
                        Equipped = item.Equipped,
                        Mark = (int)item.Mark,
                    });
                }
                else
                {
                    throw new NotSupportedException();
                }
            }

            this._BrokenWeapons.ForEach(kv => saveGame.PackedWeaponData.Add(kv.Key));
            this._BrokenItems.ForEach(kv => saveGame.PackedItemData.Add(kv.Key));

            // TODO: STUPID STUPID STUPID STUPID WHY GEARBOX WHY?????????????????????
            foreach (var packedItem in this._ExpansionItems)
            {
                saveGame.PackedItemData.Add(packedItem);
            }
        }
    }
}
