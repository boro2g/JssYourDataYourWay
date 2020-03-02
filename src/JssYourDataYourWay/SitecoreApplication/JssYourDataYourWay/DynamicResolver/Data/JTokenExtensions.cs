using System;
using Newtonsoft.Json.Linq;

namespace SitecoreApplication.JssYourDataYourWay.DynamicResolver.Data
{
	public static class JTokenExtensions
	{
		public static JToken CastValueTo(this JToken token, Type type)
		{
			if (type == typeof(bool))
			{
				return CastValueToBoolean(token);
			}

			return token;
		}

		private static JToken CastValueToBoolean(JToken token)
		{
			switch (token.Type)
			{
				case JTokenType.String:
					string stringValue = token.ToString();
					token = stringValue == "1" || stringValue == "true";
					break;
				case JTokenType.Boolean:
					break;
				default:
					token = false;
					break;
			}

			return token;
		}
	}
}
