using M3u8Downloader_H.Abstractions.Common;
using M3u8Downloader_H.Abstractions.Models;
using M3u8Downloader_H.Gui.Utils;
using System;

namespace M3u8Downloader_H.Gui.Models
{
    public class MyWindowContext: IWindowContext
    {
        public IApiFactory ApiFactory => null!;

        public ISnackbarMaranger SnackbarMaranger => new SnackbarManager(string.Empty,TimeSpan.Zero);
        public IAppCommandService AppCommandService => new AppCommandService();


    }
}
