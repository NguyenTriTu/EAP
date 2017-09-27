using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATNB.Models
{
    public class OrderDetail
    {
        [JsonProperty("Order")]
        public Order Order { get; set; }

        [JsonProperty("Ammount")]
        public long Ammount { get; set; }

        [JsonProperty("$id")]
        public string Id { get; set; }

        [JsonProperty("BookId")]
        public long BookId { get; set; }

        [JsonProperty("Quantity")]
        public long Quantity { get; set; }

        [JsonProperty("OrderId")]
        public long OrderId { get; set; }

        [JsonProperty("tblBook")]
        public TblBook TblBook { get; set; }
    }
}