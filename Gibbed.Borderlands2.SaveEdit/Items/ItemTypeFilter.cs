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

namespace Gibbed.Borderlands2.SaveEdit
{
    public class ItemTypeFilter
    {
        private readonly string _DisplayName;
        private readonly GameInfo.ItemType _Value;
        private readonly GameInfo.ItemType _SecondValue;

        public ItemTypeFilter(string displayName, GameInfo.ItemType value)
            : this(displayName, value, GameInfo.ItemType.Unknown)
        {
        }

        public ItemTypeFilter(string displayName, GameInfo.ItemType value, GameInfo.ItemType secondValue)
        {
            this._DisplayName = displayName;
            this._Value = value;
            this._SecondValue = secondValue;
        }

        public string DisplayName
        {
            get { return this._DisplayName; }
        }

        public GameInfo.ItemType Value
        {
            get { return this._Value; }
        }


        public GameInfo.ItemType SecondValue
        {
            get { return this._SecondValue; }
        }
    }
}
