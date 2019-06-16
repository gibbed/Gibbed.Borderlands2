using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;

namespace Gibbed.Borderlands2.SaveEdit
{
    /// <summary>
    /// http://www.11011.net/wpf-binding-properties
    /// </summary>
    internal class PropertyProxy : FrameworkElement
    {
        public static readonly DependencyProperty InProperty;
        public static readonly DependencyProperty OutProperty;

        public PropertyProxy()
        {
            Visibility = Visibility.Collapsed;
        }

        static PropertyProxy()
        {
            var inMetadata = new FrameworkPropertyMetadata(
                delegate (DependencyObject p, DependencyPropertyChangedEventArgs args)
                {
                    if (BindingOperations.GetBinding(p, OutProperty) != null &&
                        p is PropertyProxy proxy && proxy != null)
                    {
                        proxy.Out = args.NewValue;
                    }
                })
            {
                BindsTwoWayByDefault = false,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            };

            InProperty = DependencyProperty.Register(
                nameof(In),
                typeof(object),
                typeof(PropertyProxy),
                inMetadata);

            var outMetadata = new FrameworkPropertyMetadata(
                delegate (DependencyObject p, DependencyPropertyChangedEventArgs args)
                {
                    var source = DependencyPropertyHelper.GetValueSource(p, args.Property);
                    if (source.BaseValueSource != BaseValueSource.Local &&
                        p is PropertyProxy proxy && proxy != null)
                    {
                        object expected = proxy.In;
                        if (ReferenceEquals(args.NewValue, expected) == false)
                        {
                            Dispatcher.CurrentDispatcher.BeginInvoke(
                                DispatcherPriority.DataBind, new Action(delegate { proxy.Out = proxy.In; }));
                        }
                    }
                })
            {
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            };

            OutProperty = DependencyProperty.Register(nameof(Out), typeof(object), typeof(PropertyProxy), outMetadata);
        }

        public object In
        {
            get { return GetValue(InProperty); }
            set { SetValue(InProperty, value); }
        }

        public object Out
        {
            get { return GetValue(OutProperty); }
            set
            {
                SetValue(OutProperty, value);
                var expression = this.GetBindingExpression(OutProperty);
                if (expression != null)
                {
                    expression.UpdateSource();
                }
            }
        }
    }
}
