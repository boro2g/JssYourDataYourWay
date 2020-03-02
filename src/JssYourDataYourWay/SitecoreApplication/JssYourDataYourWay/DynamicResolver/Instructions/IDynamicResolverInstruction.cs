using SitecoreApplication.JssYourDataYourWay.DynamicResolver.Data;

namespace SitecoreApplication.JssYourDataYourWay.DynamicResolver.Instructions
{
	public interface IDynamicResolverInstruction
	{
		void Execute(DynamicResolverContext context);
	}
}
