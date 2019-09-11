using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Binance
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SymbolStatus
    {
        PRE_TRADING,
        TRADING,
        POST_TRADING,
        END_OF_DAY,
        HALT,
        AUCTION_MATCH,
        BREAK
    }
}
