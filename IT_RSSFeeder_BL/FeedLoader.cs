using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Web;
using System.Net;

namespace IT_RSSFeeder_BL
{
    public sealed class FeedLoader
    {
        private readonly ProxySettings _proxySettings;
        private readonly bool _useProxy;
        public FeedLoader(bool useProxy = false, ProxySettings proxySettings = null)
        {
            if (useProxy && proxySettings == null)
                throw new ArgumentNullException("Proxy settings doen't set");
            _useProxy = useProxy;
            _proxySettings = proxySettings;
        }
        public async Task<IEnumerable<SyndicationItem>> LoadFeedItemsAsync(string source)
        {
            using var webClient = new WebClient();
            if(_useProxy)
                webClient.Proxy = GetProxy();
            webClient.Headers.Add("user-agent", "Only a test!");
            var uri = new Uri(source);
            var rss= await webClient.DownloadStringTaskAsync(uri);
            var stringReader = new StringReader(rss);
            var xmlReader = XmlReader.Create(stringReader);
            var feed = SyndicationFeed.Load(xmlReader);
            return feed.Items;
        }
        private IWebProxy GetProxy()
        {
            var proxy = new WebProxy(_proxySettings.IP, _proxySettings.Port);
            if(_proxySettings.IsAuthorize)
            {
                proxy.Credentials = new NetworkCredential(_proxySettings.UserName, _proxySettings.Password);
                proxy.UseDefaultCredentials = false;
            }
            return proxy;
        }
    }
}
