using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SitecoreApplication.JssYourDataYourWay.DynamicResolver.Data;

namespace SitecoreApplication.JssYourDataYourWay.DynamicResolver.Parsers
{
	public class FieldMappingParser
	{
		private static readonly Regex _fieldMappingRegex = new Regex(@"(\*|(<([a-z]+)>)?([a-z\s]+))(\-\>([a-z\s]+))?(\z|\|)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		public static List<FieldMapping> ParseFieldMappings(string fieldParameters)
		{
			List<FieldMapping> fieldMappings = new List<FieldMapping>();
			MatchCollection matches = _fieldMappingRegex.Matches(fieldParameters);

			foreach (Match match in matches)
			{
				FieldMapping mapping = new FieldMapping();

				if (match.Groups[1].Value == "*")
				{
					mapping.IsWildCard = true;
				}
				else
				{
					mapping.SourceFieldName = match.Groups[4].Value;

					if (match.Groups.Count > 6 && match.Groups[6].Success)
					{
						mapping.DestinationFieldName = match.Groups[6].Value;
					}
					else
					{
						mapping.DestinationFieldName = mapping.SourceFieldName;
					}

					if (match.Groups[3].Success)
					{
						mapping.TypeForCasting = ParseType(match.Groups[3].Value);
					}
				}

				fieldMappings.Add(mapping);
			}

			return fieldMappings;
		}

		private static Type ParseType(string type)
		{
			if (type == "bool")
			{
				return typeof(bool);
			}
			return null;
		}
	}
}
