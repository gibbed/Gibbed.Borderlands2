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
using System.Runtime.InteropServices;

namespace Gibbed.Borderlands2.FileFormats
{
    public class LZO
    {
        private static readonly bool _Is64Bit = DetectIs64Bit();

        private static bool DetectIs64Bit()
        {
            return Marshal.SizeOf(IntPtr.Zero) == 8;
        }

        private static class Native32
        {
            [DllImport("lzo_32.dll", EntryPoint = "#67", CallingConvention = CallingConvention.StdCall)]
            internal static extern int NativeCompress(byte[] inbuf,
                                                      uint inlen,
                                                      byte[] outbuf,
                                                      ref uint outlen,
                                                      byte[] workbuf);

            [DllImport("lzo_32.dll", EntryPoint = "#68", CallingConvention = CallingConvention.StdCall)]
            internal static extern int NativeDecompress(byte[] inbuf, uint inlen, byte[] outbuf, ref uint outlen);
        }

        private static class Native64
        {
            [DllImport("lzo_64.dll", EntryPoint = "#67", CallingConvention = CallingConvention.StdCall)]
            internal static extern int NativeCompress(byte[] inbuf,
                                                      uint inlen,
                                                      byte[] outbuf,
                                                      ref uint outlen,
                                                      byte[] workbuf);

            [DllImport("lzo_64.dll", EntryPoint = "#68", CallingConvention = CallingConvention.StdCall)]
            internal static extern int NativeDecompress(byte[] inbuf, uint inlen, byte[] outbuf, ref uint outlen);
        }

        private const int DictSize = 2;
        private const int WorkSize = (16384 * DictSize);

        private static byte[] CompressWork = new byte[WorkSize];

        public static int Compress(byte[] inbuf, uint inlen, byte[] outbuf, ref uint outlen)
        {
            lock (CompressWork)
            {
                if (_Is64Bit == true)
                {
                    return Native64.NativeCompress(inbuf, inlen, outbuf, ref outlen, CompressWork);
                }

                return Native32.NativeCompress(inbuf, inlen, outbuf, ref outlen, CompressWork);
            }
        }

        public static int Decompress(byte[] inbuf, uint inlen, byte[] outbuf, ref uint outlen)
        {
            if (_Is64Bit == true)
            {
                return Native64.NativeDecompress(inbuf, inlen, outbuf, ref outlen);
            }

            return Native32.NativeDecompress(inbuf, inlen, outbuf, ref outlen);
        }
    }
}
