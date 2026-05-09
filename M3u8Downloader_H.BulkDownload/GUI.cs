using M3u8Downloader_H.Abstractions.Models;
using M3u8Downloader_H.Abstractions.Plugins.Window;
using M3u8Downloader_H.BulkDownload.ViewModels;

namespace M3u8Downloader_H.BulkDownload
{
    public class GUI : IWindowPlugin
    {
        private IWindowContext windowContext = default!;

        public Type ViewType => typeof(MainWindowView);

        public void InitializeWindow(IWindowContext windowContext)
        {
            this.windowContext = windowContext;
        }

        public object CreateMainView()
        {
            return new MainWindowViewModel(windowContext);
        }
    }
}
