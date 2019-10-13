using System;

using Newtonsoft.Json.Linq;

namespace Binance.Exceptions
{
    public class IpBannedException : BinanceException
    {
        public IpBannedException()
        {
        }

        public IpBannedException(int code, String message) : base(code, message)
        {
            Code = code;
        }

        public static new IpBannedException CreateFromPayload(String payload)
        {
            JObject jPayload = JObject.Parse(payload);
            return new IpBannedException((int)jPayload["code"], (String)jPayload["msg"]);
        }
    }
}
