using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATNB.Models
{
    public class Order
    {
        [JsonProperty("Ammount")]
        public long Ammount { get; set; }

        [JsonProperty("OrderDetails")]
        public OrderDetail[] OrderDetails { get; set; }

        [JsonProperty("$id")]
        public string Id { get; set; }

        [JsonProperty("Date")]
        public string Date { get; set; }

        [JsonProperty("OrderId")]
        public long OrderId { get; set; }
    }
}