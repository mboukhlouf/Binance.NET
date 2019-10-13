using System;

using Newtonsoft.Json.Linq;

namespace Binance.Exceptions
{
    public class RateLimitedException : BinanceException
    {
        public RateLimitedException()
        {
        }

        public RateLimitedException(int code, String message) : base(code, message)
        {
            Code = code;
        }

        public static new RateLimitedException CreateFromPayload(String payload)
        {
            JObject jPayload = JObject.Parse(payload);
            return new RateLimitedException((int)jPayload["code"], (String)jPayload["msg"]);
        }
    }
}
