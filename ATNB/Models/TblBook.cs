using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATNB.Models
{
    public class TblBook
    {
        [JsonProperty("ModifieldDate")]
        public object ModifieldDate { get; set; }

        [JsonProperty("CategoryId")]
        public long CategoryId { get; set; }

        [JsonProperty("AuthorId")]
        public long AuthorId { get; set; }

        [JsonProperty("$id")]
        public string Id { get; set; }

        [JsonProperty("BookId")]
        public long BookId { get; set; }

        [JsonProperty("ImgUrl")]
        public object ImgUrl { get; set; }

        [JsonProperty("CreatedDate")]
        public string CreatedDate { get; set; }

        [JsonProperty("IsActive")]
        public object IsActive { get; set; }

        [JsonProperty("Summary")]
        public string Summary { get; set; }

        [JsonProperty("PublisherId")]
        public long PublisherId { get; set; }

        [JsonProperty("Price")]
        public long Price { get; set; }

        [JsonProperty("Quantity")]
        public long Quantity { get; set; }

        [JsonProperty("tblAuthor")]
        public TblAuthor TblAuthor { get; set; }

        [JsonProperty("tblComments")]
        public object[] TblComments { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("tblCategory")]
        public TblCategory TblCategory { get; set; }

        [JsonProperty("tblPublisher")]
        public TblPublisher TblPublisher { get; set; }
    }
}