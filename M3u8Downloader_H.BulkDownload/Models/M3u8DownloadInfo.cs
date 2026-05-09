using M3u8Downloader_H.BulkDownload.Extensions;

namespace M3u8Downloader_H.BulkDownload.Models
{
    public class M3u8DownloadInfo
    {
        public string Url { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Method {  get; set; } = default!;
        public string Key { get; set; } = default!;
        public string Iv { get; set; } = default!;

        public object? this[string key]
        {
            get => this.GetData(key);
            set => this.SetData(key, value);
        }
    }
}
