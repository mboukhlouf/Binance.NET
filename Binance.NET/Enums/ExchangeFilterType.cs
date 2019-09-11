using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Binance
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ExchangeFilterType
    {
        EXCHANGE_MAX_NUM_ORDERS,
        EXCHANGE_MAX_NUM_ALGO_ORDERS
    }
}
