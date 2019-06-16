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
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using Caliburn.Micro;

namespace Gibbed.Borderlands2.SaveEdit
{
    internal class AppWindowManager : WindowManager
    {
        private const string _WindowTitle = "Gibbed's Borderlands 2 Save Editor";
        private const double _WindowWidth = 800.0 + 95.0;
        private const double _WindowHeight = 560.0;
        private const string _WindowIconPath = "pack://application:,,,/Resources/Handsome Jack.png";

        protected override Window EnsureWindow(object model, object view, bool isDialog)
        {
            var window = base.EnsureWindow(model, view, isDialog);

            if (model is ShellViewModel)
            {
                var sb = new StringBuilder();
                sb.Append(_WindowTitle);

                // ReSharper disable ConditionIsAlwaysTrueOrFalse
                if (string.IsNullOrEmpty(VersionInfo.Configuration) == false)
                {
                    sb.Append(" [");
                    sb.Append(VersionInfo.Configuration.ToUpperInvariant());
                    sb.Append("]");
                }

                if (VersionInfo.Version != null)
                {
                    sb.Append(" ");
                    sb.Append(VersionInfo.Version.ToString(3));
                }

                if (string.IsNullOrEmpty(VersionInfo.Commit) == false)
                {
                    sb.Append(" (");
                    sb.Append(VersionInfo.Commit.Substring(0, 7));
                    if (string.IsNullOrEmpty(VersionInfo.Timestamp) == false)
                    {
                        sb.Append(" @ ");
                        sb.Append(MakeFriendlyTimestamp(VersionInfo.Timestamp));
                    }
                    sb.Append(")");
                }
                else if (string.IsNullOrEmpty(VersionInfo.Timestamp) == false)
                {
                    sb.Append(" (@ ");
                    sb.Append(MakeFriendlyTimestamp(VersionInfo.Timestamp));
                    sb.Append(")");
                }
                // ReSharper restore ConditionIsAlwaysTrueOrFalse

                window.Title = sb.ToString();
                window.SizeToContent = SizeToContent.Manual;
                window.Width = _WindowWidth;
                window.Height = _WindowHeight;

                window.Icon = BitmapFrame.Create(new Uri(_WindowIconPath, UriKind.RelativeOrAbsolute));
            }

            return window;
        }

        private static string MakeFriendlyTimestamp(string s)
        {
            var datetime = DateTime.Parse(s, null, DateTimeStyles.RoundtripKind).ToLocalTime();
            return datetime.ToString("g");
        }
    }
}
