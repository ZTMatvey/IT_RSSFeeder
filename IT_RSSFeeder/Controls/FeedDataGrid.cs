using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace IT_RSSFeeder.Controls
{
    public sealed class FeedDataGrid:DataGrid
    {
        protected override DependencyObject GetContainerForItemOverride()=> new FeedDataGridRow();
    }
}
