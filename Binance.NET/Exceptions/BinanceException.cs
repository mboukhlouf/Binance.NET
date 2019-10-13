using System;

using Newtonsoft.Json.Linq;

namespace Binance.Exceptions
{
    public class BinanceException : Exception
    {
        public int Code { get; set; }

        public BinanceException() : base()
        {
        }

        public BinanceException(int code, String message) : base(message)
        {
            Code = code;
        }

        public static BinanceException CreateFromPayload(String payload)
        {
            JObject jPayload = JObject.Parse(payload);
            return new BinanceException((int)jPayload["code"], (String)jPayload["msg"]);
        }
    }
}
