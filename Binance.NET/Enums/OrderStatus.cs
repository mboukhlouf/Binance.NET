using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Binance
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderStatus
    {
        NEW,
        PARTIALLY_FILLED,
        FILLED,
        CANCELED,
        PENDING_CANCEL,
        REJECTED,
        EXPIRED
    }
}
