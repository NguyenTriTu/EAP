using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATNB.Models
{
    public class TblCategory
    {
        [JsonProperty("CategoryId")]
        public long CategoryId { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("$id")]
        public string Id { get; set; }

        [JsonProperty("CategoryName")]
        public string CategoryName { get; set; }

        [JsonProperty("tblBooks")]
        public TblBook[] TblBooks { get; set; }
    }
}