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

namespace Gibbed.Borderlands2.FileFormats.Items
{
    public struct PackedAssetReference
    {
        public static readonly PackedAssetReference None = new PackedAssetReference(-1, -1, false);

        public readonly int AssetIndex;
        public readonly int SublibraryIndex;
        public readonly bool UseSetId;

        public PackedAssetReference(int assetIndex, int sublibraryIndex, bool useSetId)
        {
            this.AssetIndex = assetIndex;
            this.SublibraryIndex = sublibraryIndex;
            this.UseSetId = useSetId;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }

            return (PackedAssetReference)obj == this;
        }

        public static bool operator ==(PackedAssetReference a, PackedAssetReference b)
        {
            return a.AssetIndex == b.AssetIndex &&
                   a.SublibraryIndex == b.SublibraryIndex &&
                   a.UseSetId == b.UseSetId;
        }

        public static bool operator !=(PackedAssetReference a, PackedAssetReference b)
        {
            return a.AssetIndex != b.AssetIndex ||
                   a.SublibraryIndex != b.SublibraryIndex ||
                   a.UseSetId != b.UseSetId;
        }

        public override int GetHashCode()
        {
            int hash = 37;
            hash = hash * 23 + this.AssetIndex.GetHashCode();
            hash = hash * 23 + this.SublibraryIndex.GetHashCode();
            hash = hash * 23 + this.UseSetId.GetHashCode();
            return hash;
        }
    }
}
