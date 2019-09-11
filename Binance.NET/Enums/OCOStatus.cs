using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Binance
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OCOStatus
    {
        RESPONSE,
        EXEC_STARTED,
        ALL_DONE
    }
}
