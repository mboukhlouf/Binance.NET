using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Binance
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderResponseType
    {
        ACK,
        RESULT,
        FULL
    }
}
