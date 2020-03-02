using Newtonsoft.Json.Linq;
using Sitecore.Data.Items;
using SitecoreApplication.JssYourDataYourWay.DynamicResolver.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SitecoreApplication.JssYourDataYourWay.DynamicResolver.Instructions.Implementations
{
	class LinkedMultiItemInstruction : BaseLinkedItemsInstruction
	{
		protected override Regex InstructionRegex => new Regex(@"linkedmultiitem\((.+)\)|linkeditems\((.+)\)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		public LinkedMultiItemInstruction(InstructionSpecification specification): base(specification)
		{
		}

		protected override void AddToContext(
			DynamicResolverContext context,
			BaseSitecoreData item,
			FieldMapping mapping,
			IEnumerable<IDynamicResolverInstruction> instructions)
		{
			IEnumerable<Item> linkedItems = item.GetItemsFromField(mapping.SourceFieldName);

			JArray fields = GetLinkedItemsFields(linkedItems, context, instructions);

			if (fields.HasValues)
			{
				context.FieldsObject.AddOrReplace(mapping.DestinationFieldName, fields);
			}
		}

		private JArray GetLinkedItemsFields(IEnumerable<Item> items, DynamicResolverContext context, IEnumerable<IDynamicResolverInstruction> instructions)
		{
			JArray linkedItemsArray = new JArray();

			foreach (Item listItem in items)
			{
				JObject fields = ExecuteInstructions(listItem, context, instructions);

				linkedItemsArray.Add(fields);
			}

			return linkedItemsArray;
		}
	}
}
