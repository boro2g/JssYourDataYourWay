using Newtonsoft.Json.Linq;
using Sitecore.Data.Items;
using SitecoreApplication.JssYourDataYourWay.DynamicResolver.Data;
using SitecoreApplication.JssYourDataYourWay.DynamicResolver.Parsers;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SitecoreApplication.JssYourDataYourWay.DynamicResolver.Instructions.Implementations
{
	public abstract class BaseLinkedItemsInstruction : IDynamicResolverInstruction
	{
		private InstructionSpecification _specification;

		protected abstract Regex InstructionRegex { get; }

		public BaseLinkedItemsInstruction(InstructionSpecification specification)
		{
			_specification = specification;
		}

		public void Execute(DynamicResolverContext context)
		{
			Match match = InstructionRegex.Match(_specification.Instruction);
			BaseSitecoreData item = context.ResolveItemSpecification(_specification.Item);

			if (match.Success && item != null)
			{
				string instruction = match.Groups[1].Value;

				if (string.IsNullOrEmpty(instruction) && match.Groups.Count > 2)
				{
					instruction = match.Groups[2].Value;
				}

				IEnumerable<InstructionSpecification> linkedItemsInstructionSpecifications = InstructionSpecificationParser.Parse(instruction);
				IEnumerable<IDynamicResolverInstruction> linkedItemsInstructions = InstructionFactory.CreateInstuctions(linkedItemsInstructionSpecifications);

				List<FieldMapping> fieldMappings = FieldMappingParser.ParseFieldMappings(_specification.Field);

				foreach (FieldMapping mapping in fieldMappings)
				{
					AddToContext(context, item, mapping, linkedItemsInstructions);
				}
			}
		}

		protected abstract void AddToContext(
			DynamicResolverContext context,
			BaseSitecoreData item,
			FieldMapping mapping,
			IEnumerable<IDynamicResolverInstruction> instructions);

		protected JObject ExecuteInstructions(Item item, DynamicResolverContext context, IEnumerable<IDynamicResolverInstruction> instructions)
		{
			DynamicResolverContext listItemContext = CreateItemContext(item, context);

			foreach (IDynamicResolverInstruction listItemInstruction in instructions)
			{
				listItemInstruction.Execute(listItemContext);
			}

			return listItemContext.FieldsObject;
		}

		protected DynamicResolverContext CreateItemContext(Item listItem, DynamicResolverContext context)
		{
			return new DynamicResolverContext
			{
				DatasourceItem = context.DatasourceItem,
				ItemSerializer = context.ItemSerializer,
				PageItem = context.PageItem,
				CurrentItem = listItem,
				FieldsObject = new JObject()
			};
		}
	}
}
