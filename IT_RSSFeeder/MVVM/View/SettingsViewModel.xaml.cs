using IT_RSSFeeder.Configuration;
using IT_RSSFeeder.Extensions;
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
            var config = Config.GetInstance();
            UpdateFrequencyTextBox.Text = config.UpdateFrequency.ToString();
            UpdateFrequencyTextBox.TextChanged += (s, e) =>
            {
                var updateFrequency = UpdateFrequencyTextBox.RemoveAllNonDigits();
                Config.UpdateUpdateFrequency(updateFrequency);
            };
        }
    }
}
