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

            var allKeys = triggerDetail.Split('+');
            var key = (Key)Enum.Parse(typeof(Key), allKeys.Last());

            var modifierKeys = allKeys.Take(allKeys.Count() - 1)
                                      .Aggregate(ModifierKeys.None,
                                                 (current, modifierKey) =>
                                                 current | (ModifierKeys)Enum.Parse(typeof(ModifierKeys), modifierKey));

            var keyBinding = new KeyBinding(new InputBindingTrigger(), key, modifierKeys);
            var trigger = new InputBindingTrigger
            {
                InputBinding = keyBinding,
            };
            return trigger;
        }
    }
}
