using Newtonsoft.Json;

namespace StaticWise.Entities
{
    public struct Scripts
    {
        [JsonProperty("externalCSS")]
        public string[] ExternalCSS { get; set; }

        [JsonProperty("internalCSS")]
        public string[] InternalCSS { get; set; }

        [JsonProperty("externalJS")]
        public string[] ExternalJS { get; set; }

        [JsonProperty("internalJS")]
        public string[] InternalJS { get; set; }
    }
}