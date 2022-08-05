using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using IT_RSSFeeder.Controls;
using System.Text.RegularExpressions;
using IT_RSSFeeder.Configuration;

namespace IT_RSSFeeder.MVVM.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        private Action<object> ShowFeedDescription;
        private const string BROWSER_CONTROL_NAME = "CurrentFeedDescriptionBrowser";
        private const string TEXTBLOCK_CONTROL_NAME = "CurrentFeedDescriptionBrowser";
        public HomeView()
        {
            InitializeComponent();
            AddFeedDescriptionViewerToGridBasedOnConfig();
        }
        private void AddFeedDescriptionViewerToGridBasedOnConfig()
        {
            var config = Config.GetInstance();
            var withFormatting = config.ShowDescriptionWithTagFormatting;
            if (withFormatting)
            {
                CreateBrowser();
                ShowFeedDescription = ShowFeedDescriptionInBrowser;
            }
            else
            {
                CreateTextBlock();
                ShowFeedDescription = ShowFeedDescriptionInTextBlock;
            }
        }
        private void CreateTextBlock()
        {
            var textBlock = new TextBlock();
            textBlock.SetValue(NameProperty, TEXTBLOCK_CONTROL_NAME);
            textBlock.TextWrapping = TextWrapping.Wrap;
            Grid.SetRow(textBlock, 3);
            RegisterName(textBlock.Name, textBlock);
            MainGrid.Children.Add(textBlock);
        }
        private void ShowFeedDescriptionInTextBlock(object sender)
        {
            var textBlock = FindName(TEXTBLOCK_CONTROL_NAME) as TextBlock;
            if (textBlock == null)
                return;
            var row = sender as FeedDataGridRow;
            var description = row.Description;
            var descriptionWithoutTags = Regex.Replace(description, "<.*?>|&.*?;", string.Empty); 
            textBlock.Text = descriptionWithoutTags;
        }
        private void CreateBrowser()
        {
            var browser = new WebBrowser();
            browser.SetValue(NameProperty, BROWSER_CONTROL_NAME);
            Grid.SetRow(browser, 3);
            RegisterName(browser.Name, browser);
            MainGrid.Children.Add(browser);
        }
        private void ShowFeedDescriptionInBrowser(object sender)
        {
            var browser = FindName(BROWSER_CONTROL_NAME) as WebBrowser;
            if (browser == null)
                return;
            var row = sender as FeedDataGridRow;
            var description = row.Description;
            var descriptionWithoutImages = Regex.Replace(description, @"<img(.*?)>", "");
            var html = $"<html><head><meta http-equiv='Content-Type' content='text/html;charset=UTF-8'></head><body>{descriptionWithoutImages}<body></html>";
            browser.NavigateToString(html);
        }
        private void FeedDataGridRow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowFeedDescription(sender);
        }

        private void FeedDataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = sender as FeedDataGridRow;
            var link = row.Link;
            var processStartInfo = new ProcessStartInfo { FileName = link, UseShellExecute = true };
            Process.Start(processStartInfo);
        }
    }
}
