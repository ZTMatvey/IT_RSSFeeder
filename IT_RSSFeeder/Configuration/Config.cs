using IT_RSSFeeder.MVVM.Model;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace IT_RSSFeeder.Configuration
{
    public sealed class Config
    {
        private readonly static string _configerationPath =
            $"{Directory.GetParent(@$"../../../")}/Configuration/config.xml";
        private static ConfigData _instance;
        public static ConfigData GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ConfigData();
                var builder = new ConfigurationBuilder();
                builder.AddXmlFile(_configerationPath);
                var configuration = builder.Build();
                configuration.Bind(_instance);
            }
            return _instance;
        }
        public static void UpdateFeeds(Feed[] newFeeds)=>
            _instance.Feeds = newFeeds;
        public static void UpdateUpdateFrequency(int newUpdateFrequency)
            => _instance.UpdateFrequency = newUpdateFrequency;
    }
}
