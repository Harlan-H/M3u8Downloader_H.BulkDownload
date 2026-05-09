using M3u8Downloader_H.BulkDownload.Models;
using M3u8Downloader_H.Common.DownloadPrams;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace M3u8Downloader_H.BulkDownload.Extensions
{
    public static class M3u8DownloadInfoExtensnion
    {
        private static readonly Dictionary<string, PropertyInfo> _cache;
        static M3u8DownloadInfoExtensnion()
        {
            _cache = typeof(M3u8DownloadInfo)
                .GetProperties()
                .ToDictionary(p => p.Name.ToLower(), p => p);
        }

        extension(M3u8DownloadInfo m3U8DownloadInfo)
        {
            public void SetData(string key, object? value)
            {
                var ret = _cache.TryGetValue(key, out var propValue);
                if (ret)
                {
                    propValue?.SetValue(m3U8DownloadInfo, Convert.ChangeType(value, propValue.PropertyType));
                }
            }

            public object? GetData(string key)
            {
                var prop = _cache[key];
                return prop?.GetValue(m3U8DownloadInfo);
            }


            public M3u8DownloadParams ToM3u8DwonloadParam()
            {
                Uri requestUrl = new Uri(m3U8DownloadInfo.Url);
                return new M3u8DownloadParams(requestUrl, m3U8DownloadInfo.Title, string.Empty, "mp4", null, m3U8DownloadInfo.Method, m3U8DownloadInfo.Key, m3U8DownloadInfo.Iv);
            }
        }
    }
}
