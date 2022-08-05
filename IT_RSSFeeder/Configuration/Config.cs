using IT_RSSFeeder.MVVM.Model;
using IT_RSSFeeder_BL;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Xml.Serialization;

namespace IT_RSSFeeder.Configuration
{
    public sealed class Config
    {
        private readonly static string _configurationPath =
            $"{Directory.GetParent(@$"../../../")}/Configuration/config.xml";
        private static ConfigData _instance;
        public static ConfigData GetInstance()
        {
            if (_instance == null)
            {
                try
                {
                    _instance = new ConfigData();
                    var builder = new ConfigurationBuilder();
                    builder.AddXmlFile(_configurationPath);
                    var configuration = builder.Build();
                    configuration.Bind(_instance);
                }
                catch (Exception e)
                {
                    _instance = GetDefaultConfigData();
                }
            }
            return _instance;
        }
        private static ConfigData GetDefaultConfigData()
        {
            var result = new ConfigData
            {
                Feeds = new[]
                        {
                            new Feed { Link = "https://habr.com/rss/interesting/", Name = "Habr" },
                            new Feed { Link = "https://news.ru/rss/category/post/world/", Name = "News" },
                            new Feed { Link = "http://blog.cleancoder.com/atom.xml", Name = "Uncle Bob Martin" },
                        },
                ProxySettings = new ProxySettings
                {
                    IP = "127.0.0.1",
                    Port = 8080,
                    UseAuthentication = false,
                    UserName = string.Empty,
                    Password = string.Empty
                },
                UpdateFrequency = 10000,
                ShowDescriptionWithTagFormatting = true,
                UseProxy = false
            };
            return result;
        }
        public static void UpdateFeeds(Feed[] newValue)=>
            GetInstance().Feeds = newValue;
        public static void UpdateUpdateFrequency(int newValue) =>
            GetInstance().UpdateFrequency = newValue;
        public static void UpdateShowDescriptionValue(bool newValue) =>
            GetInstance().ShowDescriptionWithTagFormatting = newValue;
        public static void UpdateUseProxy(bool newValue) =>
            GetInstance().UseProxy = newValue;

        public static void UpdateProxyIp(string newValue) =>
            GetInstance().ProxySettings.IP = newValue;

        public static void UpdateProxyPort(int newValue) =>
            GetInstance().ProxySettings.Port = newValue;
        public static void UpdateProxyUseAuthentication(bool newValue) =>
            GetInstance().ProxySettings.UseAuthentication = newValue;
        public static void UpdateProxyUsername(string newValue) =>
            GetInstance().ProxySettings.UserName = newValue;
        public static void UpdateProxyPassword(string newValue) =>
            GetInstance().ProxySettings.Password = newValue;
    }
}
