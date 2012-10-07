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
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Caliburn.Micro;
using Gibbed.Borderlands2.SaveEdit.Packables;

namespace Gibbed.Borderlands2.SaveEdit
{
    [Export(typeof(BackpackViewModel))]
    internal class BackpackViewModel : PropertyChangedBase, IHandle<SaveLoadedMessage>
    {
        #region Fields
        private FileFormats.SaveFile _SaveFile;
        private readonly ObservableCollection<IBackpackSlot> _Slots;
        #endregion

        #region Properties
        public ObservableCollection<IBackpackSlot> Slots
        {
            get { return this._Slots; }
        }
        #endregion

        [ImportingConstructor]
        public BackpackViewModel(IEventAggregator events)
        {
            this._Slots = new ObservableCollection<IBackpackSlot>();
            events.Subscribe(this);
        }

        public void Handle(SaveLoadedMessage message)
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
                this.Slots.Add(weapon);
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

                this.Slots.Add(item);
            }
        }
    }
}
