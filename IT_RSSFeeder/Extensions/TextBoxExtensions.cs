using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IT_RSSFeeder.Extensions
{
    internal static class TextBoxExtensions
    {
        public static bool IsDigitsOnly(this TextBox textBox)=>
            textBox.Text.Where(x => char.IsDigit(x)).Count() == 0;
        public static int RemoveAllNonDigits(this TextBox textBox)
        {
            if (!textBox.IsDigitsOnly())
            {
                var text = textBox.Text;
                textBox.Text = new string(text.Where(c => char.IsDigit(c)).ToArray());
                textBox.CaretIndex = text.Length;
            }
            var result = int.Parse(textBox.Text);
            return result;
        }
    }
}
