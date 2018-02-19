using Newtonsoft.Json;

namespace StaticWise.Entities
{
    public struct Page
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("friendlyUrl")]
        public string FriendlyUrl { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonIgnore]
        public string FileContent { get; set; }

        [JsonIgnore]
        public string FilePath { get; set; }
    }
}