using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using M3u8Downloader_H.Abstractions.Models;
using M3u8Downloader_H.BulkDownload.Extensions;
using M3u8Downloader_H.BulkDownload.Framework;
using M3u8Downloader_H.BulkDownload.Models;
using System.Collections.ObjectModel;

namespace M3u8Downloader_H.BulkDownload.ViewModels
{
    public partial class MainWindowViewModel : IPluginViewModelBase
    {
        private string[] _templateKeys = [];
        private readonly IWindowContext windowContext;

        public ObservableCollection<M3u8DownloadInfo> DownloadInfo { get; } = [];
        public ObservableCollection<M3u8DownloadInfo> SelectedDownloads { get; } = [];

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(StartParseCommand))]
        public partial string TxtFilePath { get; set; } = default!;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveTemplateCommand))]
        public partial string Separator { get; set; } = "----";

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveTemplateCommand))]
        public partial string Template { get; set; } = "$url----$title----$method----$key----$iv";

        public MainWindowViewModel(IWindowContext windowContext)
        {
            this.windowContext = windowContext;
            SelectedDownloads.CollectionChanged += (_, _) =>
            {
                ConfirmCommand.NotifyCanExecuteChanged();
                CancelCommand.NotifyCanExecuteChanged();
            };
            _templateKeys = [..Template
                    .Split(Separator)
                    .Select(x => x.Trim().TrimStart('$'))];
        }

        private bool CanSaveTemplate => !string.IsNullOrEmpty(Separator) && !string.IsNullOrEmpty(Template);

        [RelayCommand(CanExecute = nameof(CanSaveTemplate))]
        private void SaveTemplate()
        {
            try
            {
                _templateKeys = [.. Template
                .Split(Separator)
                .Select(x => x.Trim().TrimStart('$'))];
                windowContext.SnackbarMaranger.Notify("保存生成,可以添加txt文件了");
            } catch (Exception ex) {
                windowContext.SnackbarMaranger.Notify($"保存失败,{ex.Message}");
            }
        }


        private bool CanStartParse => !string.IsNullOrWhiteSpace(TxtFilePath);

        [RelayCommand(CanExecute = nameof(CanStartParse))]
        private async Task StartParse()
        {
            DownloadInfo.Clear();

            try
            {
                using var reader = File.OpenText(TxtFilePath);
                while (true)
                {
                    var line = await reader.ReadLineAsync();
                    if (string.IsNullOrWhiteSpace(line))
                        break;

                    var values = line.Split(Separator);
                    M3u8DownloadInfo m3U8DownloadInfo = new();
                    for (int i = 0; i < _templateKeys.Length && i < values.Length; i++)
                    {
                        m3U8DownloadInfo[_templateKeys[i]] = values[i];
                    }
                    DownloadInfo.Add(m3U8DownloadInfo);
                }
            }
            catch(Exception ex)
            {
                windowContext.SnackbarMaranger.Notify($"文件解析失败,{ex.Message}");
            }
        }


        private bool CanConfirm => SelectedDownloads.Any();

        [RelayCommand(CanExecute = nameof(CanConfirm))]
        private async Task Confirm()
        {
            if (!SelectedDownloads.Any())
                return;

            try
            {

                foreach (var item in SelectedDownloads.ToList())
                {
                    var downloadParam = item.ToM3u8DwonloadParam();
                    windowContext.AppCommandService.DownloadByUrl(downloadParam,null);
                    await Task.Delay(1);
                    SelectedDownloads.Remove(item);
                }
                windowContext.SnackbarMaranger.Notify($"已经开始下载,请点击左边基础查看");
            }
            catch (Exception ex)
            {
                windowContext.SnackbarMaranger.Notify(ex.Message);
            }

        }

        [RelayCommand(CanExecute = nameof(CanConfirm))]
        private void Cancel()
        {
            SelectedDownloads.Clear();
        }
    }
}
