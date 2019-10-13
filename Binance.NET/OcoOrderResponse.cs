using System;
using System.Collections.Generic;
using System.Text;

using Binance.Helpers;

namespace Binance
{
    public class OcoOrderResponse
    {
        public class Order
        {
            public string Symbol { get; set; }
            public int OrderId { get; set; }
            public string ClientOrderId { get; set; }
        }

        public class OrderReport
        {
            public string Symbol { get; set; }
            public int OrderId { get; set; }
            public int OrderListId { get; set; }
            public string ClientOrderId { get; set; }
            public long TransactTime { get; set; }
            public string Price { get; set; }
            public string OrigQty { get; set; }
            public string ExecutedQty { get; set; }
            public string CummulativeQuoteQty { get; set; }
            public string Status { get; set; }
            public string TimeInForce { get; set; }
            public string Type { get; set; }
            public string Side { get; set; }
            public string StopPrice { get; set; }
        }

        public int OrderListId { get; set; }
        public string ContingencyType { get; set; }
        public string ListStatusType { get; set; }
        public string ListOrderStatus { get; set; }
        public string ListClientOrderId { get; set; }
        public long TransactionTime { get; set; }
        public string Symbol { get; set; }
        public Order[] Orders { get; set; }
        public OrderReport[] OrderReports { get; set; }


        public override string ToString()
        {
            return JsonHelper.ToJson(this);
        }
    }
}
