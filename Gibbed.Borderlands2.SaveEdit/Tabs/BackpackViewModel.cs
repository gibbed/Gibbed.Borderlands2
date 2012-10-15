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
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using Caliburn.Micro;
using Caliburn.Micro.Contrib;
using Caliburn.Micro.Contrib.Dialogs;
using Caliburn.Micro.Contrib.Results;
using Gibbed.Borderlands2.FileFormats.Items;
using PlayerMark = Gibbed.Borderlands2.ProtoBufFormats.WillowTwoSave.PlayerMark;
using QuickWeaponSlot = Gibbed.Borderlands2.ProtoBufFormats.WillowTwoSave.QuickWeaponSlot;

namespace Gibbed.Borderlands2.SaveEdit
{
    [Export(typeof(BackpackViewModel))]
    internal class BackpackViewModel : PropertyChangedBase, IHandle<SaveUnpackMessage>, IHandle<SavePackMessage>
    {
        #region Fields
        private FileFormats.SaveFile _SaveFile;
        private readonly ObservableCollection<IBackpackSlotViewModel> _Slots;

        private IBackpackSlotViewModel _SelectedSlot;
        #endregion

        #region Properties
        public ObservableCollection<IBackpackSlotViewModel> Slots
        {
            get { return this._Slots; }
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
        #endregion

        [ImportingConstructor]
        public BackpackViewModel(IEventAggregator events)
        {
            this._Slots = new ObservableCollection<IBackpackSlotViewModel>();
            events.Subscribe(this);
        }

        public void NewWeapon()
        {
            var weapon = new BackpackWeapon()
            {
                UniqueId = new Random().Next(int.MinValue, int.MaxValue),
                // TODO: check other item unique IDs to prevent rare collisions
                AssetLibrarySetId = 0,
            };
            var viewModel = new BackpackWeaponViewModel(weapon);
            this.Slots.Add(viewModel);
            this.SelectedSlot = viewModel;
        }

        public void NewItem()
        {
            var item = new BackpackItem()
            {
                UniqueId = new Random().Next(int.MinValue, int.MaxValue),
                // TODO: check other item unique IDs to prevent rare collisions
                AssetLibrarySetId = 0,
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
            if (Clipboard.ContainsText(TextDataFormat.Text) != true &&
                Clipboard.ContainsText(TextDataFormat.UnicodeText) != true)
            {
                yield break;
            }

            var errors = 0;
            var viewModels = new List<IBackpackSlotViewModel>();
            yield return new DelegateResult(() =>
            {
                var codes = Clipboard.GetText();

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
                        packable = BackpackDataHelper.Decode(data);
                    }
                    catch (Exception)
                    {
                        errors++;
                        continue;
                    }

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
                    new Warning("Warning",
                                "Failed to load " + errors.ToString(CultureInfo.InvariantCulture) + " codes.",
                                Answer.Ok).AsResult();
            }
        }

        public void CopySelectedSlotCode()
        {
            if (this.SelectedSlot == null ||
                !(this.SelectedSlot.Slot is IPackable))
            {
                Clipboard.SetText("", TextDataFormat.Text);
                return;
            }

            var data = BackpackDataHelper.Encode((IPackable)this.SelectedSlot.Slot);
            var sb = new StringBuilder();
            sb.Append("BL2(");
            sb.Append(Convert.ToBase64String(data, Base64FormattingOptions.None));
            sb.Append(")");
            Clipboard.SetText(sb.ToString(), TextDataFormat.Text);
        }

        public void DuplicateSelectedSlot()
        {
            if (this.SelectedSlot == null)
            {
                return;
            }

            var copy = (IBackpackSlot)this.SelectedSlot.Slot.Clone();
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

        public void Handle(SaveUnpackMessage message)
        {
            this._SaveFile = message.SaveFile;

            this.Slots.Clear();

            foreach (var packedWeapon in this._SaveFile.SaveGame.PackedWeaponData)
            {
                var weapon = (BackpackWeapon)BackpackDataHelper.Decode(packedWeapon.Data);
                var test = BackpackDataHelper.Encode(weapon);
                if (Enumerable.SequenceEqual(packedWeapon.Data, test) == false)
                {
                    throw new FormatException();
                }

                weapon.QuickSlot = packedWeapon.QuickSlot;
                weapon.Mark = packedWeapon.Mark;

                var viewModel = new BackpackWeaponViewModel(weapon);
                this.Slots.Add(viewModel);
            }

            foreach (var packedItem in this._SaveFile.SaveGame.PackedItemData)
            {
                var item = (BackpackItem)BackpackDataHelper.Decode(packedItem.Data);
                var test = BackpackDataHelper.Encode(item);
                if (Enumerable.SequenceEqual(packedItem.Data, test) == false)
                {
                    throw new FormatException();
                }

                item.Quantity = packedItem.Quantity;
                item.Equipped = packedItem.Equipped;
                item.Mark = packedItem.Mark;

                var viewModel = new BackpackItemViewModel(item);
                this.Slots.Add(viewModel);
            }
        }

        public void Handle(SavePackMessage message)
        {
            var saveFile = message.SaveFile;

            saveFile.SaveGame.PackedWeaponData.Clear();
            saveFile.SaveGame.PackedItemData.Clear();

            foreach (var viewModel in this.Slots)
            {
                var slot = viewModel.Slot;

                if (slot is BackpackWeapon)
                {
                    var weapon = (BackpackWeapon)slot;
                    var data = BackpackDataHelper.Encode(weapon);

                    saveFile.SaveGame.PackedWeaponData.Add(new ProtoBufFormats.WillowTwoSave.PackedWeaponData()
                    {
                        Data = data,
                        QuickSlot = weapon.QuickSlot,
                        Mark = weapon.Mark,
                    });
                }
                else if (slot is BackpackItem)
                {
                    var item = (BackpackItem)slot;
                    var data = BackpackDataHelper.Encode(item);

                    saveFile.SaveGame.PackedItemData.Add(new ProtoBufFormats.WillowTwoSave.PackedItemData()
                    {
                        Data = data,
                        Quantity = item.Quantity,
                        Equipped = item.Equipped,
                        Mark = item.Mark,
                    });
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }
    }
}
