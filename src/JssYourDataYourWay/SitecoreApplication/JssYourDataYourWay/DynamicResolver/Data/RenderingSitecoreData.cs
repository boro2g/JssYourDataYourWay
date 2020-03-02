using Newtonsoft.Json.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SitecoreApplication.JssYourDataYourWay.DynamicResolver.Data
{
	public class RenderingSitecoreData : BaseSitecoreData
	{
		private RenderingParameters _renderingParams;
		private Database _database;

		public RenderingSitecoreData(RenderingParameters renderingParams, Database database)
		{
			_renderingParams = renderingParams;
			_database = database;
		}

		public override JProperty GetField(string fieldName)
		{
			string fieldValue = _renderingParams[fieldName];
			return new JProperty(fieldName, fieldValue);
		}

		public override JToken GetFieldValue(JProperty field)
		{
			return field.Value;
		}

		public override void SetFieldValue(JProperty field, JToken value)
		{
			field.Value = value;
		}

		public override IEnumerable<JProperty> GetAllFields()
		{
			return _renderingParams.Select(property => new JProperty(property.Key, property.Value));
		}

		public override IEnumerable<Item> GetItemsFromField(string fieldName)
		{
			string fieldValue = _renderingParams[fieldName];

			string[] guids = fieldValue.Split('|');

			return guids
				.Where(g => ID.IsID(g))
				.Select(GetItemFromGuid);
		}

		public override Item GetItemFromField(string fieldName)
		{
			string fieldValue = _renderingParams[fieldName];

			return GetItemFromGuid(fieldValue);
		}

		private Item GetItemFromGuid(string guid)
		{
			ID id = ID.Parse(guid);
			return _database.GetItem(id);
		}
	}
}
