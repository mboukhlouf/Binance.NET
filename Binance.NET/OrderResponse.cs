using System;
using System.Collections.Generic;
using System.Text;

using Binance.Helpers;

namespace Binance
{
    public class OrderResponse
    {
        public class Fill
        {
            public decimal Price { get; set; }
            public decimal Qty { get; set; }
            public decimal Commission { get; set; }
            public string CommissionAsset { get; set; }
        }

        public string Symbol { get; set; }
        public int OrderId { get; set; }
        public int OrderListId { get; set; }
        public string ClientOrderId { get; set; }
        public long TransactTime { get; set; }
        public decimal Price { get; set; }
        public decimal OrigQty { get; set; }
        public decimal ExecutedQty { get; set; }
        public decimal CummulativeQuoteQty { get; set; }
        public OrderStatus Status { get; set; }
        public TimeInForce TimeInForce { get; set; }
        public OrderType Type { get; set; }
        public OrderSide Side { get; set; }
        public Fill[] Fills { get; set; }

        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }
    }
}
