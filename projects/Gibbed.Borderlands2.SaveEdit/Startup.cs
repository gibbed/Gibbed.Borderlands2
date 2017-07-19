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
        private static Dictionary<string, Assembly> _LoadedAssemblies; 

        [STAThread]
        public static void Main()
        {
            _LoadedAssemblies = new Dictionary<string, Assembly>();
            AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;
            App.Main();
        }

        private static Assembly OnResolveAssembly(object sender, ResolveEventArgs e)
        {
            var assemblyName = new AssemblyName(e.Name);
            var dllName = assemblyName.Name + ".dll";

            Assembly assembly;
            if (_LoadedAssemblies.TryGetValue(dllName, out assembly) == true)
            {
                return assembly;
            }
            
            assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(dllName))
            {
                if (stream == null)
                {
                    return null;
                }

                try
                {
                    var block = new byte[stream.Length];
                    stream.Read(block, 0, block.Length);
                    
                    assembly = Assembly.Load(block);
                    if (assembly == null)
                    {
                        return null;
                    }

                    return _LoadedAssemblies[dllName] = assembly;
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
