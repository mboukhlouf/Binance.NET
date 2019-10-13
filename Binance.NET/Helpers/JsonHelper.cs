using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace Binance.Helpers
{
    class JsonHelper
    {
        public static T ParseFromJson<T>(String jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static String ToJson(Object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
