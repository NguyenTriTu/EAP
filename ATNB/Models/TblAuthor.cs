using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATNB.Models
{
    public class TblAuthor
    {
        [JsonProperty("AuthorId")]
        public long AuthorId { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("$id")]
        public string Id { get; set; }

        [JsonProperty("AuthorName")]
        public string AuthorName { get; set; }

        [JsonProperty("tblBooks")]
        public TblBook[] TblBooks { get; set; }
    }
}