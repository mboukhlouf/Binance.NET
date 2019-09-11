using Binance.Helpers;


namespace Binance
{
    public class ExchangeFilter
    {
        public ExchangeFilterType FilterType { get; set; }
        public int? MaxNumOrders { get; set; }
        public int? MaxNumAlgoOrders { get; set; }

        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }
    }
}
