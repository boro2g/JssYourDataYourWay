using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.LayoutService.Configuration;
using Sitecore.LayoutService.ItemRendering.ContentsResolvers;
using Sitecore.Mvc.Presentation;

namespace SitecoreApplication.JssYourDataYourWay
{
    public class ChainedContentsResolver : RenderingContentsResolver
    {
        public override object ResolveContents(Rendering rendering, IRenderingConfiguration renderingConfig)
        {
            string relatedItemId = rendering?.Parameters?["Related Item"];

            bool isValid = Guid.TryParse(relatedItemId, out Guid relatedItemGuid);

            var baseObject = base.ResolveContents(rendering, renderingConfig);

            if (String.IsNullOrWhiteSpace(relatedItemId) || !isValid)
            {
                return baseObject;
            }

            return ObjectMerger.Merge(baseObject, LoadRelatedContent(relatedItemGuid));
        }

        private object LoadRelatedContent(Guid itemId)
        {
            var item = Sitecore.Context.Database.GetItem(new ID(itemId));

            var dictionary = new Dictionary<string, string>();

            foreach (Field field in item.Fields.Where(a => !a.Name.StartsWith("__")))
            {
                dictionary[field.Name] = field.Value;
            }

            return new
            {
                RelatedFields = dictionary
            };
        }
    }
}
