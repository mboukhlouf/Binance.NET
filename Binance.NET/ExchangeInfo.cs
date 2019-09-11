using Binance.Helpers;

namespace Binance
{
    public class ExchangeInfo
    {
        public string Timezone { get; set; }
        public long ServerTime { get; set; }
        public RateLimit[] RateLimits { get; set; }
        public ExchangeFilter[] ExchangeFilters { get; set; }
        public SymbolInfo[] Symbols { get; set; }

        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }
    }
}
