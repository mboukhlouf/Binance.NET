using System;

using Newtonsoft.Json.Linq;

namespace Binance.Exceptions
{
    public class MalformedRequestException : BinanceException
    {
        public MalformedRequestException()
        {
        }

        public MalformedRequestException(int code, String message) : base(code, message)
        {
            Code = code;
        }

        public static new MalformedRequestException CreateFromPayload(String payload)
        {
            JObject jPayload = JObject.Parse(payload);
            return new MalformedRequestException((int)jPayload["code"], (String)jPayload["msg"]);
        }
    }
}
