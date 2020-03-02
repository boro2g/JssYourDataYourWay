using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using SitecoreApplication.JssYourDataYourWay.DynamicResolver.Data;

namespace SitecoreApplication.JssYourDataYourWay.DynamicResolver.Instructions.Implementations
{
	public class LinkedItemInstruction : BaseLinkedItemsInstruction
	{
		protected override Regex InstructionRegex => new Regex(@"linkeditem\((.+)\)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		public LinkedItemInstruction(InstructionSpecification specification): base(specification)
		{
		}

		protected override void AddToContext(DynamicResolverContext context, BaseSitecoreData item, FieldMapping mapping, IEnumerable<IDynamicResolverInstruction> instructions)
		{
			Item linkedItem = item.GetItemFromField(mapping.SourceFieldName);

			if (linkedItem != null)
			{
				JObject fields = ExecuteInstructions(linkedItem, context, instructions);

				context.FieldsObject.AddOrReplace(mapping.DestinationFieldName, fields);
			}
		}
	}
}
