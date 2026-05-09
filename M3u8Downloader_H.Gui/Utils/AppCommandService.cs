using M3u8Downloader_H.Abstractions.Common;
using M3u8Downloader_H.Abstractions.M3u8;
using System.Diagnostics;

namespace M3u8Downloader_H.Gui.Utils
{
    internal class AppCommandService : IAppCommandService
    {
        public void DownloadByM3uFileInfo(IDownloadParamBase downloadParamBase, IM3uFileInfo m3UFileInfo, string? pluginKey)
        {
            Debug.WriteLine(downloadParamBase);
        }

        public void DownloadByUrl(IM3u8DownloadParam m3U8DownloadParam, string? pluginKey)
        {
            Debug.WriteLine(m3U8DownloadParam, pluginKey);
        }

        public void DownloadMedia(IMediaDownloadParam mediaDownloadParam)
        {
            Debug.WriteLine(mediaDownloadParam);
        }
    }
}
