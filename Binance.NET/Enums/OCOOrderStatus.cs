using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Binance
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OcoOrderStatus
    {
        EXECUTING,
        ALL_DONE,
        REJECT
    }
}
