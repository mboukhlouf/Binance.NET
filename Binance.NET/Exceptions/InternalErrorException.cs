using System;

using Newtonsoft.Json.Linq;

namespace Binance.Exceptions
{
    public class InternalErrorException : BinanceException
    {
        public InternalErrorException() : base()
        {
        }

        public InternalErrorException(int code, String message) : base(code, message)
        {
            Code = code;
        }

        public static new InternalErrorException CreateFromPayload(String payload)
        {
            JObject jPayload = JObject.Parse(payload);
            return new InternalErrorException((int)jPayload["code"], (String)jPayload["msg"]);
        }
    }
}
