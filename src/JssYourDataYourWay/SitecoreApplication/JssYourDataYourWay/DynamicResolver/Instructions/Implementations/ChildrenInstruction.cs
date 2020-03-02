using Newtonsoft.Json.Linq;
using Sitecore.Data.Items;
using SitecoreApplication.JssYourDataYourWay.DynamicResolver.Data;
using SitecoreApplication.JssYourDataYourWay.DynamicResolver.Parsers;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SitecoreApplication.JssYourDataYourWay.DynamicResolver.Instructions.Implementations
{
	public class ChildrenInstruction : IDynamicResolverInstruction
	{
		private static readonly Regex _childInstructionRegex = new Regex(@"children\((.+)\)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		private InstructionSpecification _specification;

		public ChildrenInstruction(InstructionSpecification specification)
		{
			_specification = specification;
		}

		public void Execute(DynamicResolverContext context)
		{
			Match match = _childInstructionRegex.Match(_specification.Instruction);
			BaseSitecoreData item = context.ResolveItemSpecification(_specification.Item);

			if (match.Success && item is ItemSitecoreData parentItem)
			{
				IEnumerable<InstructionSpecification> childInstructionSpecifications = InstructionSpecificationParser.Parse(match.Groups[1].Value);
				IEnumerable<IDynamicResolverInstruction> childInstructions = InstructionFactory.CreateInstuctions(childInstructionSpecifications);

				JArray childrenArray = new JArray();

				foreach(Item child in parentItem.GetChildren())
				{
					DynamicResolverContext childContext = CreateChildContext(child, context);

					foreach(IDynamicResolverInstruction childInstruction in childInstructions)
					{
						childInstruction.Execute(childContext);
					}

					childrenArray.Add(childContext.FieldsObject);
				}

				context.FieldsObject.Add(_specification.Field, childrenArray);
			}
		}

		private DynamicResolverContext CreateChildContext(Item child, DynamicResolverContext context)
		{
			return new DynamicResolverContext
			{
				DatasourceItem = context.DatasourceItem,
				ItemSerializer = context.ItemSerializer,
				PageItem = context.PageItem,
				CurrentItem = child,
				FieldsObject = new JObject()
			};
		}
	}
}
