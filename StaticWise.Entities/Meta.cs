using Newtonsoft.Json;
using System.Collections.Generic;

namespace StaticWise.Entities
{
    public struct Meta
    {
        [JsonProperty("attribute")]
        public List<Attr> Attribute { get; set; }
    }
}