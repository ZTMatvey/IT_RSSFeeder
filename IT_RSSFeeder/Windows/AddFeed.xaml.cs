using IT_RSSFeeder.MVVM.Model;
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
using System.Windows.Shapes;

namespace IT_RSSFeeder.Windows
{
    /// <summary>
    /// Interaction logic for AddFeed.xaml
    /// </summary>
    public partial class AddFeed : Window
    {
        public AddFeed()
        {
            InitializeComponent();
        }

        public static Feed Show()
        {
            var window = new AddFeed();
            window.ShowDialog();
            var name = window.NameTextBox.Text;
            var link = window.LinkTextBox.Text;
            var feed = new Feed { Name = name, Link = link};
            return feed;
        }

        private void Button_Click(object sender, RoutedEventArgs e) => Close();

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}
