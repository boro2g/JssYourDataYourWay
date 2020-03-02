using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SitecoreApplication.JssYourDataYourWay
{
    internal class ObjectMerger
    {
        internal static object Merge(object baseObject, params object[] additionalObjects)
        {
            JsonSerializer serializer = new JsonSerializer();

            var resultObject = JObject.FromObject(baseObject, serializer);

            if (additionalObjects != null)
            {
                foreach (object additionalContentItem in additionalObjects)
                {
                    var additionalContentJObject = JObject.FromObject(additionalContentItem, serializer);

                    resultObject.Merge(additionalContentJObject, new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Concat });
                }
            }

            return resultObject;
        }
    }
}
