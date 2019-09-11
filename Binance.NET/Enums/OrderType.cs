using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Binance
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderType
    {
        LIMIT,
        MARKET,
        STOP_LOSS,
        STOP_LOSS_LIMIT,
        TAKE_PROFIT,
        TAKE_PROFIT_LIMIT,
        LIMIT_MAKER
    }
}
