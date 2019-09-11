using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Binance
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TimeInForce
    {
        GTC,
        IOC,
        FOK
    }
}
