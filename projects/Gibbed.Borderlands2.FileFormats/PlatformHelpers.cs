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
using Gibbed.Borderlands2.GameInfo;
using Gibbed.IO;

namespace Gibbed.Borderlands2.FileFormats
{
    internal static class PlatformHelpers
    {
        public static Endian GetEndian(this Platform platform)
        {
            switch (platform)
            {
                case Platform.PC:
                case Platform.PSVita:
                case Platform.Shield:
                case Platform.Switch:
                {
                    return Endian.Little;
                }

                case Platform.X360:
                case Platform.PS3:
                {
                    return Endian.Big;
                }
            }

            throw new ArgumentException("unsupported platform", nameof(platform));
        }

        public static CompressionScheme GetCompressionScheme(this Platform platform)
        {
            switch (platform)
            {
                case Platform.Switch:
                {
                    return CompressionScheme.None;
                }

                case Platform.PC:
                case Platform.X360:
                {
                    return CompressionScheme.LZO;
                }

                case Platform.PS3:
                case Platform.PSVita:
                case Platform.Shield:
                {
                    return CompressionScheme.Zlib;
                }
            }

            throw new ArgumentException("unsupported platform", nameof(platform));
        }
    }
}
