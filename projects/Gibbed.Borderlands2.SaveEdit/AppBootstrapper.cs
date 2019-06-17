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
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using Caliburn.Micro;
using Caliburn.Micro.Contrib;
using Gibbed.Gearbox.WPF;

namespace Gibbed.Borderlands2.SaveEdit
{
    internal class AppBootstrapper : Bootstrapper<ShellViewModel>
    {
        private CompositionContainer _Container;

        protected override void Configure()
        {
            FrameworkExtensions.Message.Attach.AllowExtraSyntax(
                MessageSyntaxes.SpecialValueProperty | MessageSyntaxes.XamlBinding);
            FrameworkExtensions.ActionMessage.EnableFilters();
            FrameworkExtensions.ViewLocator.EnableContextFallback();

            var currentParser = Parser.CreateTrigger;
            Parser.CreateTrigger = (target, triggerText) =>
                ShortcutParser.CanParse(triggerText)
                    ? ShortcutParser.CreateTrigger(triggerText)
                    : currentParser(target, triggerText);

            this._Container = new CompositionContainer(
                new AggregateCatalog(AssemblySource.Instance.Select(x => new AssemblyCatalog(x))));

            var batch = new CompositionBatch();
            batch.AddExportedValue<IWindowManager>(new AppWindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(this._Container);

            this._Container.Compose(batch);
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return base.SelectAssemblies().Concat(new[]
            {
                typeof(ResultExtensions).Assembly
            });
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;

            var exports = this._Container.GetExportedValues<object>(contract).ToArray();
            if (exports.Length > 0)
            {
                return exports[0];
            }

            throw new InvalidOperationException(
                $"Could not locate any instances of contract {contract}.");
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return this._Container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        protected override void BuildUp(object instance)
        {
            this._Container.SatisfyImportsOnce(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            try
            {
                base.OnStartup(sender, e);
                GameInfo.InfoManager.Touch();
            }
            catch (CompositionException ex)
            {
                Execute.OnUIThread(() =>
                {
                    if (MessageBox.Show(
                    @"A compositon exception has occured during startup.

This generally indicates you have run the save editor from a
directory that has files from a previous version of the save editor
that are causing a conflict.

Choose Cancel to show the error information.",
                    "Error",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Error) == MessageBoxResult.Cancel)
                    {
                        MessageBox.Show(
                            $"An exception was thrown during startup (press Ctrl+C to copy):\n\n{ex}",
                            "Error",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                });
                Application.Shutdown(1);
            }
            catch (Exception ex)
            {
                Execute.OnUIThread(() =>
                {
                    MessageBox.Show(
                    $"An exception was thrown during startup (press Ctrl+C to copy):\n\n{ex}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                });
                Application.Shutdown(1);
            }
        }

        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Execute.OnUIThread(() =>
            {
                MessageBox.Show(
                    $"An unhandled exception was thrown (press Ctrl+C to copy):\n\n{e.Exception}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            });
            Application.Shutdown(1);
        }
    }
}
