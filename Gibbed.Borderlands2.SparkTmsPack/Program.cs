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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Gibbed.Borderlands2.FileFormats;
using Gibbed.IO;
using NDesk.Options;

namespace Gibbed.Borderlands2.SparkTmsPack
{
    internal class Program
    {
        private static string GetExecutableName()
        {
            return Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
        }

        private static void Main(string[] args)
        {
            bool showHelp = false;
            bool verbose = false;

            var options = new OptionSet()
            {
                { "v|verbose", "be verbose", v => verbose = v != null },
                { "h|help", "show this message and exit", v => showHelp = v != null },
            };

            List<string> extras;

            try
            {
                extras = options.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("{0}: ", GetExecutableName());
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `{0} --help' for more information.", GetExecutableName());
                return;
            }

            if (extras.Count < 1 || extras.Count > 2 || showHelp == true)
            {
                Console.WriteLine("Usage: {0} [OPTIONS]+ input_dir [output_tms]", GetExecutableName());
                Console.WriteLine();
                Console.WriteLine("Options:");
                options.WriteOptionDescriptions(Console.Out);
                return;
            }

            string baseInputPath = extras[0];
            string outputPath = extras.Count > 1 ? extras[1] : Path.ChangeExtension(baseInputPath + "_packed", ".tms");

            var endian = Endian.Little;

            var inputPaths = Directory.GetFiles(baseInputPath);

            byte[] uncompressedBytes;
            using (var data = new MemoryStream())
            {
                foreach (var inputPath in inputPaths)
                {
                    {
                        var inputName = Path.GetFileName(inputPath) ?? "";
                        var inputNameBytes = Encoding.ASCII.GetBytes(inputName);
                        Array.Resize(ref inputNameBytes, inputNameBytes.Length + 1);
                        data.WriteValueS32(inputNameBytes.Length, endian);
                        data.WriteBytes(inputNameBytes);
                    }

                    {
                        var inputText = File.ReadAllText(inputPath);
                        var inputTextIsUnicode = inputText.Any(c => c > 127);

                        if (inputTextIsUnicode == false)
                        {
                            var inputTextBytes = Encoding.ASCII.GetBytes(inputText);
                            Array.Resize(ref inputTextBytes, inputTextBytes.Length + 1);
                            data.WriteValueS32(inputTextBytes.Length, endian);
                            data.WriteBytes(inputTextBytes);
                        }
                        else
                        {
                            var inputTextBytes = Encoding.Unicode.GetBytes(inputText);
                            Array.Resize(ref inputTextBytes, inputTextBytes.Length + 2);
                            data.WriteValueS32(-(inputTextBytes.Length / 2), endian);
                            data.WriteBytes(inputTextBytes);
                        }
                    }
                }

                data.Flush();

                uncompressedBytes = (byte[])data.GetBuffer().Clone();
                Array.Resize(ref uncompressedBytes, (int)data.Length);
            }

            var compressedBytes = new byte[uncompressedBytes.Length +
                                           (uncompressedBytes.Length / 16) + 64 + 3];
            var actualCompressedSize = compressedBytes.Length;

            var result = LZO.Compress(uncompressedBytes,
                                      0,
                                      uncompressedBytes.Length,
                                      compressedBytes,
                                      0,
                                      ref actualCompressedSize);
            if (result != LZO.ErrorCode.Success)
            {
                throw new SaveCorruptionException(string.Format("LZO compression failure ({0})", result));
            }

            Array.Resize(ref compressedBytes, actualCompressedSize);

            using (var output = File.Create(outputPath))
            {
                output.WriteValueS32(uncompressedBytes.Length, endian);
                output.WriteValueS32(inputPaths.Length, endian);
                output.WriteValueU32(0x9E2A83C1, endian);
                output.WriteValueU32(0x00020000, endian);
                output.WriteValueS32(compressedBytes.Length, endian);
                output.WriteValueS32(uncompressedBytes.Length, endian);
                output.WriteValueS32(compressedBytes.Length, endian);
                output.WriteValueS32(uncompressedBytes.Length, endian);
                output.WriteBytes(compressedBytes);
            }
        }
    }
}
