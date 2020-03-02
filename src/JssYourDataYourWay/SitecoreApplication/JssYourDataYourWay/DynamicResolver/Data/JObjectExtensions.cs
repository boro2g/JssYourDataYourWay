using Newtonsoft.Json.Linq;

namespace SitecoreApplication.JssYourDataYourWay.DynamicResolver.Data
{
	public static class JObjectExtensions
	{
		public static void AddOrReplace(this JObject input, string key, JToken value)
		{
			JToken token;

			if (input.TryGetValue(key, out token))
			{
				token.Replace(value);
			}
			else
			{
				input.Add(key, value);
			}
		}
	}
}
