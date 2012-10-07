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
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;

namespace Gibbed.Borderlands2.SaveEdit
{
    internal class DataResourceBindingExtension : MarkupExtension
    {
        private object _TargetObject;
        private object _TargetProperty;
        private DataResource _DataResouce;

        public DataResourceBindingExtension()
        {
        }

        /// <summary>
        /// Gets or sets the data resource.
        /// </summary>
        /// <value>The data resource.</value>
        public DataResource DataResource
        {
            get { return this._DataResouce; }
            set
            {
                if (this._DataResouce != value)
                {
                    if (this._DataResouce != null)
                    {
                        this._DataResouce.Changed -= OnDataResourceChanged;
                    }
                    this._DataResouce = value;

                    if (this._DataResouce != null)
                    {
                        this._DataResouce.Changed += OnDataResourceChanged;
                    }
                }
            }
        }

        /// <summary>
        /// When implemented in a derived class, returns an object that is set as the value of the target property for this markup extension.
        /// </summary>
        /// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
        /// <returns>
        /// The object value to set on the property where the extension is applied.
        /// </returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            IProvideValueTarget target = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));

            this._TargetObject = target.TargetObject;
            this._TargetProperty = target.TargetProperty;

            // mTargetProperty can be null when this is called in the Designer.
            Debug.Assert(this._TargetProperty != null || DesignerProperties.GetIsInDesignMode(new DependencyObject()));

            if (DataResource.BindingTarget == null && this._TargetProperty != null)
            {
                PropertyInfo propInfo = this._TargetProperty as PropertyInfo;
                if (propInfo != null)
                {
                    try
                    {
                        return Activator.CreateInstance(propInfo.PropertyType);
                    }
                    catch (MissingMethodException)
                    {
                        // there isn't a default constructor
                    }
                }

                DependencyProperty depProp = this._TargetProperty as DependencyProperty;
                if (depProp != null)
                {
                    DependencyObject depObj = (DependencyObject)this._TargetObject;
                    return depObj.GetValue(depProp);
                }
            }

            return DataResource.BindingTarget;
        }

        private void OnDataResourceChanged(object sender, EventArgs e)
        {
            // Ensure that the bound object is updated when DataResource changes.
            DataResource dataResource = (DataResource)sender;
            DependencyProperty depProp = this._TargetProperty as DependencyProperty;

            if (depProp != null)
            {
                DependencyObject depObj = (DependencyObject)this._TargetObject;
                object value = Convert(dataResource.BindingTarget, depProp.PropertyType);
                depObj.SetValue(depProp, value);
            }
            else
            {
                PropertyInfo propInfo = this._TargetProperty as PropertyInfo;
                if (propInfo != null)
                {
                    object value = Convert(dataResource.BindingTarget, propInfo.PropertyType);
                    propInfo.SetValue(this._TargetObject, value, new object[0]);
                }
            }
        }

        private object Convert(object obj, Type toType)
        {
            if (obj == null)
            {
                return null;
            }

            try
            {
                return System.Convert.ChangeType(obj, toType);
            }
            catch (InvalidCastException)
            {
                return obj;
            }
        }
    }
}
