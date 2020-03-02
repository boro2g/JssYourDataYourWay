using Newtonsoft.Json.Linq;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;

namespace SitecoreApplication.JssYourDataYourWay.DynamicResolver.Data
{
	public abstract class BaseSitecoreData
	{
		public abstract JProperty GetField(string fieldName);
		public abstract IEnumerable<JProperty> GetAllFields();
		public abstract IEnumerable<Item> GetItemsFromField(string fieldName);
		public abstract Item GetItemFromField(string fieldName);
		public abstract JToken GetFieldValue(JProperty field);
		public abstract void SetFieldValue(JProperty field, JToken value);
	}
}
