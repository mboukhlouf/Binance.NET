using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Binance
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SymbolFilterType
    {
        PRICE_FILTER,
        PERCENT_PRICE,
        LOT_SIZE,
        MIN_NOTIONAL,
        ICEBERG_PARTS,
        MARKET_LOT_SIZE,
        MAX_NUM_ORDERS,
        MAX_NUM_ALGO_ORDERS,
        MAX_NUM_ICEBERG_ORDERS
    }
}
