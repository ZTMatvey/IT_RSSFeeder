using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using IT_RSSFeeder.Configuration;
using IT_RSSFeeder.Core;
using IT_RSSFeeder_BL;
using System.Windows;
using System.Threading;

namespace IT_RSSFeeder.MVVM.ViewModel
{
    internal sealed class HomeViewModel : ViewModel
    {
        private CancellationTokenSource _cts;
        private CancellationToken _updateCancellationToken;
        public ObservableCollection<SyndicationItem> Items { get; private set; }
        public ObservableCollection<TabButton> Tabs { get; private set; }
        private readonly ConfigData _config;
        private readonly FeedLoader _feedLoader;
        public HomeViewModel()
        {
            _config = Config.GetInstance();
            _feedLoader = new FeedLoader(_config.UseProxy, _config.ProxySettings);
            Tabs = new ObservableCollection<TabButton>();
            Items = new ObservableCollection<SyndicationItem>();
            {
                var command = new RelayCommand(o=> IteratevelyLoadAllFeeds());
                Tabs.Add(new TabButton("Все", true, command));
            }
            foreach (var feed in _config.Feeds)
            {
                var command = new RelayCommand(o => IteratevelyLoadFeed(feed.Link));
                Tabs.Add(new TabButton(feed.Name, false, command));
            }
            MainViewModel.ViewChanged += ViewChanged;
            IteratevelyLoadAllFeeds();
        }
        private void ViewChanged(ViewModel oldViewModel, ViewModel newViewModel)
        {
            var isOpened = newViewModel is HomeViewModel;
            if (isOpened)
                return;
            _cts?.Cancel();
            MainViewModel.ViewChanged -= ViewChanged;
        }
        private async void IterativelyUpdateItems(Action updateLogic)
        {
            if (updateLogic == null)
                throw new ArgumentNullException("Logic can not be null");
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            _updateCancellationToken = _cts.Token;
            try
            {
                await Task.Run(async () =>
                {
                    while (true)
                    {
                        await App.Current.Dispatcher.Invoke(() => {
                            Items.Clear();
                            updateLogic();
                            return Task.CompletedTask;
                        });
                        await Task.Delay(_config.UpdateFrequency, _updateCancellationToken);
                    }
                });
            }
            catch (TaskCanceledException)
            {}
        }
        private void LoadAllFeeds()
        {
            foreach (var feed in _config.Feeds)
                LoadFeed(feed.Link, false);
        }
        private void IteratevelyLoadAllFeeds()=> IterativelyUpdateItems(() => LoadAllFeeds());
        private async void LoadFeed(string link, bool showErrorMessage = true)
        {
            var items = await GetFeedItemsOrNullAsync(link);
            if (items == null)
            {
                if (showErrorMessage)
                    MessageBox.Show("Ошибка при загрузке данной ленты");
                return;
            }
            foreach (var item in items)
                Items.Add(item);
        }
        private void IteratevelyLoadFeed(string link, bool showErrorMessage = true) => 
            IterativelyUpdateItems(() => LoadFeed(link, showErrorMessage));
        private async Task<IEnumerable<SyndicationItem>> GetFeedItemsOrNullAsync(string link)
        {
            try
            {
                var items = await _feedLoader.LoadFeedItemsAsync(link);
                return items;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
