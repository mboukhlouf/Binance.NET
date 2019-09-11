﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Binance
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OCOOrderStatus
    {
        EXECUTING,
        ALL_DONE,
        REJECT
    }
}
