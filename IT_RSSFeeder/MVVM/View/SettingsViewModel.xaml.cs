using IT_RSSFeeder.Configuration;
using IT_RSSFeeder.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace IT_RSSFeeder.MVVM.View
{
    /// <summary>
    /// Interaction logic for SettingsViewModel.xaml
    /// </summary>
    public partial class SettingsViewModel : UserControl
    {
        public SettingsViewModel()
        {
            InitializeComponent();
            SetupControls();
        }
        private void SetupControls()
        {
            var config = Config.GetInstance();
            UpdateFrequencyTextBox.Text = config.UpdateFrequency.ToString();
            UpdateFrequencyTextBox.TextChanged += (s, e) =>
            {
                var updateFrequency = UpdateFrequencyTextBox.RemoveAllNonDigits();
                Config.UpdateUpdateFrequency(updateFrequency);
            };
            ShowFormattedDescriptionCheckBox.IsChecked = config.ShowDescriptionWithTagFormatting;
            ShowFormattedDescriptionCheckBox.Click += (s, e) =>
            {
                var value = ShowFormattedDescriptionCheckBox.IsChecked ?? true;
                Config.UpdateShowDescriptionValue(value);
            };
            UseProxyCheckBox.IsChecked = config.UseProxy;
            UseProxyCheckBox.Click += (s, e) =>
            {
                var value = UseProxyCheckBox.IsChecked ?? false;
                Config.UpdateUseProxy(value);
            };
            ProxyIpTextBox.Text = config.ProxySettings.IP;
            ProxyIpTextBox.LostFocus += (s, e) =>
            {
                var address = ProxyIpTextBox.Text;
                var isValid = IPAddress.TryParse(address, out var ip);
                if (isValid)
                    Config.UpdateProxyIp(ip.ToString());
                else
                    ProxyIpTextBox.Text = config.ProxySettings.IP;
            };
            ProxyPortTextBox.Text = config.ProxySettings.Port.ToString();
            ProxyPortTextBox.TextChanged += (s, e) =>
            {
                var updateFrequency = ProxyPortTextBox.RemoveAllNonDigits();
                Config.UpdateProxyPort(updateFrequency);
            };
            UseAuthenticationCheckBox.IsChecked = config.ProxySettings.UseAuthentication;
            UseAuthenticationCheckBox.Click += (s, e) =>
            {
                var value = UseAuthenticationCheckBox.IsChecked ?? false;
                Config.UpdateProxyUseAuthentication(value);
            };
            UserNameTextBox.Text = config.ProxySettings.UserName;
            UserNameTextBox.TextChanged += (s, e) =>
            {
                var text = UserNameTextBox.Text;
                Config.UpdateProxyUsername(text);
            };
            PasswordTextBox.Text = config.ProxySettings.Password;
            PasswordTextBox.TextChanged += (s, e) =>
            {
                var text = PasswordTextBox.Text;
                Config.UpdateProxyPassword(text);
            };
        }
    }
}
