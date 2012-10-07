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

using Gibbed.Borderlands2.ProtoBufFormats.WillowTwoSave;

namespace Gibbed.Borderlands2.SaveEdit.Packables
{
    internal class BackpackItem : PackableItem, IBackpackSlot
    {
        #region Fields
        private PackableItem _Item;
        private int _Quantity;
        private bool _Equipped;
        private PlayerMark _Mark;
        #endregion

        #region Properties
        public PackableItem Item
        {
            get { return this._Item; }
            set
            {
                if (value != this._Item)
                {
                    this._Item = Item;
                    this.NotifyOfPropertyChange(() => this.Item);
                }
            }
        }

        public int Quantity
        {
            get { return this._Quantity; }
            set
            {
                if (value != this._Quantity)
                {
                    this._Quantity = value;
                    this.NotifyOfPropertyChange(() => this.Quantity);
                }
            }
        }

        public bool Equipped
        {
            get { return this._Equipped; }
            set
            {
                if (value != this._Equipped)
                {
                    this._Equipped = value;
                    this.NotifyOfPropertyChange(() => this.Equipped);
                    this.NotifyOfPropertyChange(() => this.DisplayGroup);
                }
            }
        }

        public PlayerMark Mark
        {
            get { return this._Mark; }
            set
            {
                if (value != this._Mark)
                {
                    this._Mark = value;
                    this.NotifyOfPropertyChange(() => this.Mark);
                }
            }
        }
        #endregion

        #region ICloneable Members
        public override object Clone()
        {
            return new BackpackItem()
            {
                Item = (PackableItem)this.Item.Clone(),
                Quantity = this.Quantity,
                Equipped = this.Equipped,
                Mark = this.Mark,
            };
        }
        #endregion

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
    }
}
