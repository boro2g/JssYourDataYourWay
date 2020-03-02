using Newtonsoft.Json.Linq;
using Sitecore.Collections;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System.Collections.Generic;

namespace SitecoreApplication.JssYourDataYourWay.DynamicResolver.Data
{
	public class ItemSitecoreData : BaseSitecoreData
	{
		private readonly Item _item;
		private readonly SitecoreItemSerializer _serializer;

		private JObject _serializedFields;
		private JObject SerializedFields
		{
			get
			{
				if (_serializedFields == null)
				{
					_serializedFields = JObject.Parse(_serializer.Serialize(_item, null));
				}
				return _serializedFields;
			}
		}

		public ItemSitecoreData(Item item, SitecoreItemSerializer serializer)
		{
			_item = item;
			_serializer = serializer;
		}

		public override JProperty GetField(string fieldName)
		{
			return SerializedFields.Property(fieldName);
		}

		public override IEnumerable<JProperty> GetAllFields()
		{
			return SerializedFields.Properties();
		}

		public override IEnumerable<Item> GetItemsFromField(string fieldName)
		{
			MultilistField listField = _item.Fields[fieldName];
			return listField.GetItems();
		}

		public override Item GetItemFromField(string fieldName)
		{
			ReferenceField linkedItem = _item.Fields[fieldName];
			return linkedItem.TargetItem;
		}

		public override JToken GetFieldValue(JProperty field)
		{
			return field.Value["value"];
		}

		public override void SetFieldValue(JProperty field, JToken value)
		{
			field.Value["value"] = value;
		}

		public ChildList GetChildren()
		{
			return _item.Children;
		}
	}
}
