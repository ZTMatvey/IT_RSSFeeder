using IT_RSSFeeder.MVVM.Model;
using IT_RSSFeeder_BL;

namespace IT_RSSFeeder.Configuration
{
    public sealed class ConfigData
    {
        public Feed[] Feeds { get; set; }
        public int UpdateFrequency { get; set; }
        public ProxySettings ProxySettings { get; set; }
        public bool UseProxy { get; set; }
        public bool ShowDescriptionWithTagFormatting { get; set; }
    }
}
