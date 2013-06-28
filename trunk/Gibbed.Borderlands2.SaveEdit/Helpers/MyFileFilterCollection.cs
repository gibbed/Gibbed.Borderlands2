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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Gibbed.Borderlands2.SaveEdit
{
    internal class MyFileFilterCollection
    {
        private readonly List<MyFilterBuilder> _Filters = new List<MyFilterBuilder>();
        private MyFilterBuilder _DefaultFilter;

        public string DefaultExtension { get; private set; }

        /// <summary>
        /// Adds a filter for the given <paramref name="extension"/>. By default, the first filter will be the default in the dialog
        /// </summary>
        /// <param name="extension">the file extension</param>
        /// <param name="isDefault">sets this filter as the default filter in the dialog</param>
        /// <example>AddFilter("xml")</example>
        /// <returns></returns>
        public MyFilterBuilder AddFilter(string extension, bool isDefault = false)
        {
            var filterBuilder = new MyFilterBuilder(this, extension);
            this._Filters.Add(filterBuilder);

            if (isDefault == true)
            {
                this._DefaultFilter = filterBuilder;
            }

            if (string.IsNullOrEmpty(this.DefaultExtension) == true || isDefault)
            {
                this.DefaultExtension = extension;
            }

            return filterBuilder;
        }

        /// <summary>
        /// Adds a filter for all files, i.e *.*
        /// </summary>
        /// <returns></returns>
        public MyFileFilterCollection AddAllFilesFilter(bool isDefault = false)
        {
            var filterBuilder = this.AddFilter("*", isDefault);
            filterBuilder.WithDescription("All Files");
            return this;
        }

        /// <summary>
        /// Adds a filter for the list of given <paramref name="extensions"/>
        /// </summary>
        /// <param name="extensions"></param>
        /// <returns></returns>
        public MyFilterBuilder AddFilter(params string[] extensions)
        {
            var filterBuilder = new MyFilterBuilder(this, extensions);
            this._Filters.Add(filterBuilder);
            return filterBuilder;
        }

        /// <summary>
        /// Creates the filter expression for the dialog. If no filter was added, a filter expression for alles files will be returned.
        /// </summary>
        /// <returns></returns>
        public string CreateFilterExpression()
        {
            if (this._Filters.Any() == false)
            {
                return "All Files (*.*)|*.*";
            }

            return string.Join("|",
                               this._Filters
                                   .Select(
                                       x =>
                                       string.Format(CultureInfo.InvariantCulture,
                                                     "{0}|{1}",
                                                     x.Description,
                                                     x.FilterExpression)));
        }

        public int GetFilterIndex()
        {
            if (this._Filters.Any() == false ||
                this._DefaultFilter == null)
            {
                return 0;
            }

            var index = this._Filters.IndexOf(this._DefaultFilter);
            if (index < 0)
            {
                return 0;
            }

            return 1 + index;
        }

        public class MyFilterBuilder
        {
            public MyFilterBuilder(MyFileFilterCollection fileFilterCollection, params string[] extensions)
            {
                if (fileFilterCollection == null)
                {
                    throw new ArgumentNullException("FileFilterCollection may not be null");
                }

                if (extensions.Any() == false || extensions.All(string.IsNullOrWhiteSpace))
                {
                    throw new ArgumentException("you must specify at least one extension");
                }

                this.FileFilterCollection = fileFilterCollection;
                this.Extensions = extensions;
            }

            internal MyFileFilterCollection FileFilterCollection { get; private set; }
            internal string[] Extensions { get; private set; }
            internal string Description { get; private set; }

            internal string FilterExpression
            {
                get
                {
                    return string.Join(";",
                                       Extensions
                                           .Select(x => string.Format(CultureInfo.InvariantCulture, "*.{0}", x)));
                }
            }

            /// <summary>
            /// Sets the descripion of the filter. File extensions are automatically added to the description unless <paramref name="appendExtensions"/> is false.
            /// </summary>
            /// <param name="description">the description</param>
            /// <param name="appendExtensions">append the extension(s) to the description?</param>
            /// <returns></returns>
            public MyFileFilterCollection WithDescription(string description, bool appendExtensions = true)
            {
                this.Description = description;

                if (appendExtensions == true)
                {
                    this.Description = AppendExtensions(Description);
                }

                return this.FileFilterCollection;
            }

            /// <summary>
            /// Creates a default description for the filter, i.e. 'Xml-File' for the extension 'xml'
            /// </summary>
            /// <returns></returns>
            public MyFileFilterCollection WithDefaultDescription()
            {
                var extensionString = string.Join(", ", this.Extensions.Select(Capitalize));
                this.Description = string.Format(CultureInfo.InvariantCulture, "{0}-Files", extensionString);
                return this.FileFilterCollection;
            }

            /// <summary>
            /// Capitalizes a string
            /// </summary>
            /// <param name="s"></param>
            /// <returns></returns>
            private static string Capitalize(string s)
            {
                if (String.IsNullOrWhiteSpace(s))
                {
                    return s;
                }
                if (s.Length == 1)
                {
                    return s.ToUpper();
                }

                return s.Substring(0, 1).ToUpper() + s.Substring(1);
            }

            /// <summary>
            /// Appends the extensions to a string
            /// </summary>
            /// <param name="s"></param>
            /// <returns></returns>
            private string AppendExtensions(string s)
            {
                return string.Format(CultureInfo.InvariantCulture, "{0} ({1})", s, FilterExpression);
            }
        }
    }
}
