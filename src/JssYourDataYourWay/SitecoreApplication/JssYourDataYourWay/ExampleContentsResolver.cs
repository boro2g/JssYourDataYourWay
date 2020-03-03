using System;
using Sitecore.LayoutService.Configuration;
using Sitecore.LayoutService.ItemRendering.ContentsResolvers;
using Sitecore.Mvc.Presentation;

namespace SitecoreApplication.JssYourDataYourWay
{
    public class ExampleContentsResolver : RenderingContentsResolver
    {
        public override object ResolveContents(Rendering rendering, 
            IRenderingConfiguration renderingConfig)
        {
            return new
            {
                name = "SiteCore",
                date = DateTime.Now,
                casing = "needs moar C"
            };
        }
    }
}
