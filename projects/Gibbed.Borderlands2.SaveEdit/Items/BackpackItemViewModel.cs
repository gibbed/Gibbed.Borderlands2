/* Copyright (c) 2019 Rick (rick 'at' gibbed 'dot' us)
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
using Gibbed.Borderlands2.FileFormats.Items;
using Gibbed.Borderlands2.ProtoBufFormats.WillowTwoSave;

namespace Gibbed.Borderlands2.SaveEdit
{
    internal class BackpackItemViewModel : BaseItemViewModel, IBackpackSlotViewModel
    {
        private readonly BackpackItem _BackpackItem;

        public BackpackItemViewModel(BackpackItem item)
            : base(item)
        {
            this._BackpackItem = item ?? throw new ArgumentNullException(nameof(item));
        }

        #region Properties
        public IBackpackSlot BackpackSlot
        {
            get { return this._BackpackItem; }
        }

        public BackpackItem BackpackItem
        {
            get { return this._BackpackItem; }
        }

        public int Quantity
        {
            get { return this._BackpackItem.Quantity; }
            set
            {
                this._BackpackItem.Quantity = value;
                this.NotifyOfPropertyChange(nameof(Quantity));
            }
        }

        public bool? Equipped
        {
            get { return this._BackpackItem.Equipped; }
            set
            {
                this._BackpackItem.Equipped = value.HasValue == false ? false : value.Value;
                this.NotifyOfPropertyChange(nameof(Equipped));
                this.NotifyOfPropertyChange(nameof(DisplayGroup));
            }
        }

        public PlayerMark Mark
        {
            get { return this._BackpackItem.Mark; }
            set
            {
                this._BackpackItem.Mark = value;
                this.NotifyOfPropertyChange(nameof(Mark));
            }
        }
        #endregion

        #region Display Properties
        public override string DisplayGroup
        {
            get
            {
                if (this.Equipped == true)
                {
                    return "Equipped";
                }

                return base.DisplayGroup;
            }
        }
        #endregion
    }
}
