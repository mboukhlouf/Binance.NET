using Binance.Helpers;

namespace Binance
{
    public class SymbolInfo
    {
        public string Symbol { get; set; }

        public SymbolStatus Status { get; set; }

        public string BaseAsset { get; set; }

        public int BaseAssetPrecision { get; set; }

        public string QuoteAsset { get; set; }

        public int QuotePrecision { get; set; }

        public OrderType[] OrderTypes { get; set; }

        public bool IcebergAllowed { get; set; }

        public bool OCOAllowed { get; set; }

        public bool IsSpotTradingAllowed { get; set; }

        public bool IsMarginTradingAllowed { get; set; }

        public SymbolFilter[] Filters { get; set; }

        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }
    }
}
