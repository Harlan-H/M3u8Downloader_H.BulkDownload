using Avalonia.Threading;
using M3u8Downloader_H.Abstractions.Models;
using Material.Styles.Controls;
using Material.Styles.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace M3u8Downloader_H.Gui.Utils
{
#pragma warning disable CS9113 // 参数未读。
    public class SnackbarManager(string hostname, TimeSpan duration) : ISnackbarMaranger
#pragma warning restore CS9113 // 参数未读。
    {
        public void Notify(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
