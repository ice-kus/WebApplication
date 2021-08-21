using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public class Region
    {
        public long Id { get; set; }       // Идентификатор
        [JsonProperty("region")]
        public string Name { get; set; }   // Название
    }
}
