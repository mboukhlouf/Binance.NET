using Binance.Helpers;

namespace Binance
{
    public class SymbolFilter
    {
        public SymbolFilterType FilterType { get; set; }
        public string MinPrice { get; set; }
        public string MaxPrice { get; set; }
        public string TickSize { get; set; }
        public string MultiplierUp { get; set; }
        public string MultiplierDown { get; set; }
        public int? AvgPriceMins { get; set; }
        public string MinQty { get; set; }
        public string MaxQty { get; set; }
        public string StepSize { get; set; }
        public string MinNotional { get; set; }
        public bool? ApplyToMarket { get; set; }
        public int? Limit { get; set; }
        public int? MaxNumAlgoOrders { get; set; }

        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }
    }
}
