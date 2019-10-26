using System;
using Newtonsoft.Json;

namespace RefitDemo.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Coin
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }
}
