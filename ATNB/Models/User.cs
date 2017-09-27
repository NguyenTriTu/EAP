using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATNB.Models
{
    public class User
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        [JsonProperty("IsActive")]
        public bool IsActive { get; set; }
    }
}