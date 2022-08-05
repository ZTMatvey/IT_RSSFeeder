using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace IT_RSSFeeder.Controls
{
    public sealed class FeedDataGridRow: DataGridRow
    {
        public string Description
        {
            get
            {
                return (string)GetValue(DescriptionProperty);
            }
            set { SetValue(DescriptionProperty, value); }
        }
        public static readonly DependencyProperty
        DescriptionProperty = DependencyProperty.Register("Description",
            typeof(string), typeof(FeedDataGridRow), new PropertyMetadata(""));
        public string Link
        {
            get
            {
                return (string)GetValue(LinkProperty);
            }
            set { SetValue(LinkProperty, value); }
        }
        public static readonly DependencyProperty
        LinkProperty = DependencyProperty.Register("Link",
            typeof(string), typeof(FeedDataGridRow), new PropertyMetadata(""));
    }
}
