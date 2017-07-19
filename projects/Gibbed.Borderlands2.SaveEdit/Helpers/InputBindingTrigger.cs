using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Diagnostics;

namespace Gibbed.Borderlands2.SaveEdit
{
    /// <summary>
    /// http://www.siimviikman.com/2012/06/28/caliburn-adding-keyboard-shortcuts/
    /// http://www.felicepollano.com/2011/05/02/InputBindingKeyBindingWithCaliburnMicro.aspx
    /// http://stackoverflow.com/questions/4181310/how-can-i-bind-key-gestures-in-caliburn-micro
    /// </summary>
    internal class InputBindingTrigger : TriggerBase<FrameworkElement>, ICommand
    {
        public static readonly DependencyProperty InputBindingProperty =
            DependencyProperty.Register("InputBinding",
                                        typeof(InputBinding),
                                        typeof(InputBindingTrigger),
                                        new UIPropertyMetadata(null));

        public InputBinding InputBinding
        {
            get { return (InputBinding)GetValue(InputBindingProperty); }
            set { SetValue(InputBindingProperty, value); }
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public bool CanExecute(object parameter)
        {
            // action is anyway blocked by Caliburn at the invoke level
            return true;
        }

        public void Execute(object parameter)
        {
            this.InvokeActions(parameter);
        }

        protected override void OnAttached()
        {
            if (this.InputBinding != null)
            {
                this.InputBinding.Command = this;

                if (this.AssociatedObject.Focusable == true)
                {
                    this.AssociatedObject.InputBindings.Add(this.InputBinding);
                }
                else
                {
                    Window window = null;

                    this.AssociatedObject.Loaded += delegate
                    {
                        window = this.GetWindow(AssociatedObject);
                        if (window.InputBindings.Contains(this.InputBinding) == false)
                        {
                            window.InputBindings.Add(this.InputBinding);
                        }
                    };

                    this.AssociatedObject.Unloaded += delegate
                    {
                        window.InputBindings.Remove(this.InputBinding);
                    };
                }
            }

            base.OnAttached();
        }

        private Window GetWindow(FrameworkElement frameworkElement)
        {
            if (frameworkElement is Window)
            {
                return frameworkElement as Window;
            }

            var parent = frameworkElement.Parent as FrameworkElement;
            Debug.Assert(parent != null);

            return this.GetWindow(parent);
        }
    }
}
