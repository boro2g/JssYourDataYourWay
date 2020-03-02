using System;

namespace SitecoreApplication.JssYourDataYourWay.DynamicResolver.Data
{
	public class FieldMapping
	{
		public bool IsWildCard { get; set; }
		public string SourceFieldName { get; set; }
		public string DestinationFieldName { get; set; }
		public Type TypeForCasting { get; set; }
	}
}
