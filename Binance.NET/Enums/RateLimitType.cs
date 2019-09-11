using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Binance
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RateLimitType
    {
        REQUEST_WEIGHT,
        ORDERS,
        RAW_REQUESTS
    }
}
