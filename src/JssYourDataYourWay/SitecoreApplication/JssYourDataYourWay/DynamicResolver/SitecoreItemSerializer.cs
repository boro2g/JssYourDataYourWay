using Newtonsoft.Json;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.LayoutService.Serialization.ItemSerializers;
using Sitecore.LayoutService.Serialization.Pipelines.GetFieldSerializer;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SitecoreApplication.JssYourDataYourWay.DynamicResolver
{
    /// <summary>
	/// Extension of the OOTB sitecore item serializer which allows a filtered set of
	/// field names
	/// </summary>
	public class SitecoreItemSerializer : DefaultItemSerializer
    {
        public SitecoreItemSerializer(IGetFieldSerializerPipeline getFieldSerializerPipeline) : base(getFieldSerializerPipeline)
        {
        }

        public string Serialize(Item item, string[] filterFields)
        {
            string result;

            using (StringWriter stringWriter = new StringWriter())
            {
                using (JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    jsonTextWriter.WriteStartObject();

                    IEnumerable<Field> itemFields = GetItemFields(item, filterFields);

                    foreach (Field current in itemFields)
                    {
                        SerializeField(current, jsonTextWriter, null);
                    }

                    jsonTextWriter.WriteEndObject();
                }

                result = stringWriter.ToString();
            }

            return result;
        }

        protected IEnumerable<Field> GetItemFields(Item item, string[] filterFields)
        {
            item.Fields.ReadAll();

            if (filterFields == null)
            {
                return item.Fields;
            }
            else
            {
                return item.Fields.Where(x => filterFields.Contains(x.Name));
            }
        }
    }
}
