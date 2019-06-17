/* Copyright (c) 2016 Rick (rick 'at' gibbed 'dot' us)
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
using System.Reflection;

namespace Gibbed.Borderlands2.SaveEdit
{
    internal static class Startup
    {
        /* We keep a running list because a particular assembly might be resolved
         * more than once which would result in multiple copies of the
         * assembly loaded otherwise.
         */
        private static Dictionary<string, Assembly> _LoadedEmbeddedAssemblies;

        [STAThread]
        public static void Main()
        {
            _LoadedEmbeddedAssemblies = new Dictionary<string, Assembly>();
            AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;
            App.Main();
        }

        private static Assembly OnResolveAssembly(object sender, ResolveEventArgs e)
        {
            var assemblyName = new AssemblyName(e.Name);
            var resourceName = @"Assemblies\" + assemblyName.Name + @".dll";
            if (_LoadedEmbeddedAssemblies.TryGetValue(resourceName, out var assembly) == true)
            {
                return assembly;
            }

            using (var stream = typeof(Startup).Assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    return null;
                }

                try
                {
                    var bytes = new byte[stream.Length];
                    var read = stream.Read(bytes, 0, bytes.Length);
                    if (read != bytes.Length)
                    {
                        throw new EndOfStreamException();
                    }

                    assembly = Assembly.Load(bytes);
                    if (assembly == null)
                    {
                        return null;
                    }

                    return _LoadedEmbeddedAssemblies[resourceName] = assembly;
                }
                catch (IOException)
                {
                    return null;
                }
                catch (BadImageFormatException)
                {
                    return null;
                }
            }
        }
    }
}
