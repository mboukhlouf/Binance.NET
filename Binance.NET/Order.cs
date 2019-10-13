using System;
using System.Collections.Generic;
using System.Text;

using Binance.Serialization;

namespace Binance
{
    [FormData]
    public class Order
    {
        [FormField(Name = "symbol")]
        public String Symbol { get; set; }

        [FormField(Name = "side")]
        public OrderSide? Side { get; set; }

        [FormField(Name = "type")]
        public OrderType? Type { get; set; }

        [FormField(Name = "timeInForce")]
        public TimeInForce? TimeInForce { get; set; }

        [FormField (Name = "quantity")]
        public decimal? Quantity { get; set; }

        [FormField(Name = "price")]
        public decimal? Price { get; set; }

        [FormField(Name = "newClientOrderId")]
        public String NewClientOrderId { get; set; }

        [FormField(Name = "stopPrice")]
        public decimal? StopPrice { get; set; }

        [FormField(Name = "icebergQty")]
        public decimal? IcebergQuantity { get; set; }

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
