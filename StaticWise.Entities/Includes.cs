using Newtonsoft.Json;

namespace StaticWise.Entities
{
    public struct Includes
    {
        [JsonProperty("siteHeader")]
        public string SiteHeader { get; set; }

        [JsonProperty("siteFooter")]
        public string SiteFooter { get; set; }

        [JsonProperty("postHeader")]
        public string PostHeader { get; set; }

        [JsonProperty("postFooter")]
        public string PostFooter { get; set; }
    }
}