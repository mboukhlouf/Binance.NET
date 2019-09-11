using Binance.Helpers;

namespace Binance
{
    public class RateLimit
    {
        public RateLimitType RateLimitType { get; set; }

        public RateLimitInterval Interval { get; set; }

        public int IntervalNum { get; set; }

        public int Limit { get; set; }

        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }
    }
}
