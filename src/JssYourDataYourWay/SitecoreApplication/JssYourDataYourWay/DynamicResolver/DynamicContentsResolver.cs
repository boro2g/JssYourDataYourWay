using Newtonsoft.Json.Linq;
using Sitecore.LayoutService.Configuration;
using Sitecore.LayoutService.ItemRendering.ContentsResolvers;
using Sitecore.LayoutService.Serialization.Pipelines.GetFieldSerializer;
using SitecoreApplication.JssYourDataYourWay.DynamicResolver.Data;
using SitecoreApplication.JssYourDataYourWay.DynamicResolver.Instructions;
using SitecoreApplication.JssYourDataYourWay.DynamicResolver.Parsers;
using System.Collections.Generic;
using SitecoreRendering = Sitecore.Mvc.Presentation.Rendering;

namespace SitecoreApplication.JssYourDataYourWay.DynamicResolver
{
	public class DynamicContentsResolver : RenderingContentsResolver
	{
		private SitecoreItemSerializer _itemSerializer;

		public DynamicContentsResolver(IGetFieldSerializerPipeline getFieldSerializerPipeline)
		{
			_itemSerializer = new SitecoreItemSerializer(getFieldSerializerPipeline);
		}
		
		public override object ResolveContents(SitecoreRendering rendering, IRenderingConfiguration renderingConfig)
		{
			string specificationString = rendering.RenderingItem.Parameters;
			IEnumerable<InstructionSpecification> specifications = InstructionSpecificationParser.Parse(specificationString);
			IEnumerable<IDynamicResolverInstruction> instructions = InstructionFactory.CreateInstuctions(specifications);

			DynamicResolverContext context = CreateStartContext(rendering);

			foreach (IDynamicResolverInstruction instruction in instructions)
			{
				instruction.Execute(context);
			}

			return context.FieldsObject;
		}

		private DynamicResolverContext CreateStartContext(SitecoreRendering rendering)
		{
			return new DynamicResolverContext
			{
				Rendering = rendering,
				DatasourceItem = rendering.Item,
				CurrentItem = null,
				PageItem = Sitecore.Context.Item,
				FieldsObject = new JObject(),
				ItemSerializer = _itemSerializer
			};
		}
	}
}
