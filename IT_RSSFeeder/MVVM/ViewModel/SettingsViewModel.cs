using IT_RSSFeeder.Configuration;
using IT_RSSFeeder.Core;
using IT_RSSFeeder.MVVM.Model;
using IT_RSSFeeder.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace IT_RSSFeeder.MVVM.ViewModel
{
    internal sealed class SettingsViewModel : ViewModel
    {
        public ObservableCollection<Feed> Feeds { get; private set; }
        public Feed SelectedFeed { get; private set; }
        public ICommand DeleteFeedCommand { get; private set; }
        public ICommand AddFeedCommand { get; private set; }
        private readonly ConfigData _config;
        public SettingsViewModel()
        {
            SelectedFeed = new Feed();
            var config = Config.GetInstance();
            DeleteFeedCommand = new RelayCommand(o => RemoveFeed(o as Feed));
            AddFeedCommand = new RelayCommand(o =>
            {
                var feed = Windows.AddFeed.Show();
                AddFeed(feed);
            });
            Feeds = new ObservableCollection<Feed>(config.Feeds);
        }
        private void AddFeed(Feed feed)
        {
            if (feed == null)
                return;
            Feeds.Add(feed);
            Config.UpdateFeeds(Feeds.ToArray());
        }
        private void RemoveFeed(Feed feed)
        {
            if (feed == null)
                return;
            Feeds.Remove(feed);
            Config.UpdateFeeds(Feeds.ToArray());
        }
    }
}
