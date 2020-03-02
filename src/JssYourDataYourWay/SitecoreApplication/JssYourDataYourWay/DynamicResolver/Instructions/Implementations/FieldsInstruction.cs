using Newtonsoft.Json.Linq;
using SitecoreApplication.JssYourDataYourWay.DynamicResolver.Data;
using SitecoreApplication.JssYourDataYourWay.DynamicResolver.Parsers;
using System.Collections.Generic;

namespace SitecoreApplication.JssYourDataYourWay.DynamicResolver.Instructions.Implementations
{
	public class FieldsInstruction : IDynamicResolverInstruction
	{
		private InstructionSpecification _specification;

		public FieldsInstruction(InstructionSpecification specification)
		{
			_specification = specification;
		}

		public void Execute(DynamicResolverContext context)
		{
			BaseSitecoreData item = context.ResolveItemSpecification(_specification.Item);

			if (item != null)
			{
				List<FieldMapping> fieldMappings = FieldMappingParser.ParseFieldMappings(_specification.Field);

				foreach (FieldMapping mapping in fieldMappings)
				{
					if (mapping.IsWildCard)
					{
						foreach (JProperty property in item.GetAllFields())
						{
							if (!property.Name.StartsWith("__"))
							{
								context.FieldsObject.Add(property.Name, property.Value);
							}
						}
					}
					else
					{
						JProperty sourceProperty = item.GetField(mapping.SourceFieldName);
						if (sourceProperty != null)
						{
							if (mapping.TypeForCasting != null)
							{
								JToken fieldValue = item.GetFieldValue(sourceProperty);

								JToken castedToken = fieldValue.CastValueTo(mapping.TypeForCasting);

								item.SetFieldValue(sourceProperty, castedToken);
							}

							context.FieldsObject.Add(mapping.DestinationFieldName, sourceProperty.Value);
						}
					}
				}
			}
		}
	}
}
