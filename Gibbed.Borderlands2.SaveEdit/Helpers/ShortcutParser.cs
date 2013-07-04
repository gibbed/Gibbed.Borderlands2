using System;
using System.Linq;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Gibbed.Borderlands2.SaveEdit
{
    /// <summary>
    /// http://www.siimviikman.com/2012/06/28/caliburn-adding-keyboard-shortcuts/
    /// </summary>
    internal static class ShortcutParser
    {
        public static bool CanParse(string triggerText)
        {
            return string.IsNullOrWhiteSpace(triggerText) == false &&
                   triggerText.Contains("Shortcut") == true;
        }

        public static TriggerBase CreateTrigger(string triggerText)
        {
            var triggerDetail = triggerText.Replace("[", string.Empty)
                                           .Replace("]", string.Empty)
                                           .Replace("Shortcut", string.Empty)
                                           .Trim();

            var modifierKeys = ModifierKeys.None;
            var allKeys = triggerDetail.Split('+');
            var key = (Key)Enum.Parse(typeof(Key), allKeys.Last());

            foreach (var modifierKey in allKeys.Take(allKeys.Count() - 1))
            {
                modifierKeys |= (ModifierKeys)Enum.Parse(typeof(ModifierKeys), modifierKey);
            }

            var keyBinding = new KeyBinding(new InputBindingTrigger(), key, modifierKeys);
            var trigger = new InputBindingTrigger
            {
                InputBinding = keyBinding,
            };
            return trigger;
        }
    }
}
