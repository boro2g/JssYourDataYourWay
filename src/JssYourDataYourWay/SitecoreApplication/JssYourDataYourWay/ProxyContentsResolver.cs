using System;
using System.Collections.Specialized;
using System.Net.Http;
using Newtonsoft.Json;
using Sitecore.LayoutService.Configuration;
using Sitecore.LayoutService.ItemRendering.ContentsResolvers;
using Sitecore.Mvc.Presentation;

namespace SitecoreApplication.JssYourDataYourWay
{
    public class ProxyContentsResolver : IRenderingContentsResolver
    {
        public bool IncludeServerUrlInMediaUrls { get; set; }
        public bool UseContextItem { get; set; }
        public string ItemSelectorQuery { get; set; }
        public NameValueCollection Parameters { get; set; }

        public object ResolveContents(Rendering rendering, IRenderingConfiguration renderingConfig)
        {
            var timeoutMs = int.Parse(rendering.Parameters?["Timeout"] ?? "500");

            var url = rendering.Parameters?["Source Url"];

            if (String.IsNullOrWhiteSpace(url))
            {
                return new ProxyContentResult(false, "No url set on rendering parameters 'Source Url'");
            }

            var client = new HttpClient
            {
                Timeout = TimeSpan.FromMilliseconds(timeoutMs)
            };

            try
            {
                using (var message = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    using (var httpResponse = client.GetAsync(url).Result)
                    {
                        return new ProxyContentResult(true, JsonConvert.DeserializeObject<ContentModel>(httpResponse.Content.ReadAsStringAsync().Result));
                    }
                }
            }
            catch (Exception e)
            {
                return new ProxyContentResult(false, $"Timeout - {e.Message}.");
            }
        }

        public class ProxyContentResult
        {
            public ProxyContentResult(bool success, string content)
            {
                Success = success;
                Content = new ContentModel { Content = content };
            }

            public ProxyContentResult(bool success, ContentModel content)
            {
                Success = success;
                Content = content;
            }

            public bool Success { get; set; }
            public ContentModel Content { get; set; }
        }

        public class ContentModel
        {
            public string Content { get; set; }
            public string Source { get; set; }
            public int DelayMs { get; set; }
        }
    }
}
