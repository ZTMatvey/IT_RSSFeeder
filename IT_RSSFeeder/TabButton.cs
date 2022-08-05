using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IT_RSSFeeder
{
    public sealed class TabButton
    {
        private string _title;
        public string Title => _title;
        private bool _isChecked;
        public bool IsChecked => _isChecked;
        private ICommand _select;
        public ICommand Select => _select;
        public TabButton(string title, bool isChecked, ICommand select)
        {
            _title = title;
            _isChecked = isChecked;
            _select = select;
        }
    }
}
