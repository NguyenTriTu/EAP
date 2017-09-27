using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATNB.Models
{
    public class TblPublisher
    {
        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("PublisherName")]
        public string PublisherName { get; set; }

        [JsonProperty("$id")]
        public string Id { get; set; }

        [JsonProperty("PublisherId")]
        public long PublisherId { get; set; }

        [JsonProperty("tblBooks")]
        public TblBook[] TblBooks { get; set; }
    }
}