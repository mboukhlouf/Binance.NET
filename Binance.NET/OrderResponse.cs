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
            public string Price { get; set; }
            public string Qty { get; set; }
            public string Commission { get; set; }
            public string CommissionAsset { get; set; }
        }

        public string Symbol { get; set; }
        public int OrderId { get; set; }
        public int OrderListId { get; set; }
        public string ClientOrderId { get; set; }
        public long TransactTime { get; set; }
        public string Price { get; set; }
        public string OrigQty { get; set; }
        public string ExecutedQty { get; set; }
        public string CummulativeQuoteQty { get; set; }
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
