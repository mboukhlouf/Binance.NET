using System;
using System.Collections.Generic;
using System.Text;

using Binance.Serialization;

namespace Binance
{
    [FormData]
    public class OcoOrder
    {
        [FormField(Name = "symbol")]
        public String Symbol { get; set; }

        [FormField(Name = "listClientOrderId")]
        public String ListClientOrderId { get; set; }

        [FormField(Name = "side")]
        public OrderSide? Side { get; set; }

        public decimal? Quantity { get; set; }

        [FormField(Name = "quantity")]
        public String QuantityStr
        {
            get
            {
                if (Quantity == null)
                    return null;
                else
                    return ((decimal)Quantity).ToString("G29");
            }
        }

        [FormField(Name = "limitClientOrderId")]
        public String LimitClientOrderId { get; set; }

        public decimal? Price { get; set; }
        [FormField(Name = "price")]
        public String PriceStr
        {
            get
            {
                if (Price == null)
                    return null;
                else
                    return ((decimal)Price).ToString("G29");
            }
        }
        

        [FormField(Name = "limitIcebergQty")]
        public String LimitIcebergQtyStr
        {
            get
            {
                if (LimitIcebergQty == null)
                    return null;
                else
                    return ((decimal)LimitIcebergQty).ToString("G29");
            }
        }
        public decimal? LimitIcebergQty { get; set; }

        [FormField(Name = "stopClientOrderId")]
        public String StopClientOrderId { get; set; }

        [FormField(Name = "stopPrice")]
        public String StopPriceStr
        {
            get
            {
                if (StopPrice == null)
                    return null;
                else
                    return ((decimal)StopPrice).ToString("G29");
            }
        }
        public decimal? StopPrice { get; set; }

        [FormField(Name = "stopLimitPrice")]
        public String StopLimitPriceStr
        {
            get
            {
                if (StopLimitPrice == null)
                    return null;
                else
                    return ((decimal)StopLimitPrice).ToString("G29");
            }
        }
        public decimal? StopLimitPrice { get; set; }


        [FormField(Name = "stopIcebergQty")]
        public String StopIcebergQtyStr
        {
            get
            {
                if (StopIcebergQty == null)
                    return null;
                else
                    return ((decimal)StopIcebergQty).ToString("G29");
            }
        }
        public decimal? StopIcebergQty { get; set; }


        [FormField(Name = "stopLimitTimeInForce")]
        public TimeInForce? StopLimitTimeInForce { get; set; }

        [FormField(Name = "newOrderRespType")]
        public OrderResponseType? OrderResponseType { get; set; }

        [FormField(Name = "recvWindow")]
        public long? RecvWindow { get; set; }

        [FormField(Name = "timestamp")]
        public long? Timestamp { get; set; }


        public override string ToString()
        {
            return FormDataSerializer.Serialize(this);
        }
    }
}
