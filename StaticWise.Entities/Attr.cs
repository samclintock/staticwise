using Newtonsoft.Json;

namespace StaticWise.Entities
{
    public struct Attr
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}