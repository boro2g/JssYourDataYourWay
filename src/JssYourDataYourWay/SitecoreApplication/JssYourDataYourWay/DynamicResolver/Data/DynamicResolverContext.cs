using Newtonsoft.Json.Linq;
using Sitecore.Data.Items;
using Sitecore.Data.Query;
using System.Linq;
using SitecoreRendering = Sitecore.Mvc.Presentation.Rendering;

namespace SitecoreApplication.JssYourDataYourWay.DynamicResolver.Data
{
	public class DynamicResolverContext
	{
		private const string _itemSpecDataSource = "__datasource";
		private const string _itemSpecPage = "__page";
		private const string _itemSpecCurrent = "__item";
		private const string _itemSpecRendering = "__rendering";
		private const char _queryDelimiter = '$';

		public SitecoreRendering Rendering { get; set; }

		public Item DatasourceItem { get; set; }
		public Item CurrentItem { get; set; }
		public Item PageItem { get; set; }
		public JObject FieldsObject { get; set; }
		public SitecoreItemSerializer ItemSerializer { get; set; }

		private Item ResolveQuery(Item baseItem, string queryString)
		{
			Item[] queryResult = Query.SelectItems(queryString, baseItem);

			return queryResult?.FirstOrDefault();
		}

		private Item ResolveItem(string itemSpecification)
		{
			if (itemSpecification == _itemSpecDataSource)
			{
				return DatasourceItem;
			}
			else if(itemSpecification == _itemSpecPage)
			{
				return PageItem;
			}
			else if(itemSpecification == _itemSpecCurrent)
			{
				return CurrentItem;
			}
			else if (itemSpecification.Contains(_queryDelimiter.ToString()))
			{
				string[] specificationParts = itemSpecification.Split(_queryDelimiter);
				Item item = ResolveItem(specificationParts[0]);
				return ResolveQuery(item, specificationParts[1]);
			}
			else
			{
				return (DatasourceItem ?? PageItem).Database.GetItem(itemSpecification);
			}
		}

		public BaseSitecoreData ResolveItemSpecification(string itemSpecification)
		{
			if (itemSpecification == _itemSpecRendering)
			{
				return new RenderingSitecoreData(Rendering.Parameters, PageItem.Database);
			}

			return new ItemSitecoreData(ResolveItem(itemSpecification), ItemSerializer);
		}
	}
}
