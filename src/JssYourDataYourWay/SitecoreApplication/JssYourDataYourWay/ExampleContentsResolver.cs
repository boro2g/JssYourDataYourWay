using System;
using System.Collections.Specialized;
using Sitecore.LayoutService.Configuration;
using Sitecore.LayoutService.ItemRendering.ContentsResolvers;
using Sitecore.Mvc.Presentation;

namespace SitecoreApplication.JssYourDataYourWay
{
    public class ExampleContentsResolver : IRenderingContentsResolver
    {
        public bool IncludeServerUrlInMediaUrls { get; set; }
        public bool UseContextItem { get; set; }
        public string ItemSelectorQuery { get; set; }
        public NameValueCollection Parameters { get; set; }

        public object ResolveContents(Rendering rendering, IRenderingConfiguration renderingConfig)
        {
            //if you want to access the datasource item
            //var datasource = !string.IsNullOrEmpty(rendering.DataSource)
            //    ? rendering.RenderingItem?.Database.GetItem(rendering.DataSource)
            //    : null;

            return new
            {
                name = "test",
                date = DateTime.Now,
                hello = "world"
            };
        }
    }
}
