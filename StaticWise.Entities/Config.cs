using Newtonsoft.Json;
using System.Collections.Generic;

namespace StaticWise.Entities
{
    public struct Config
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("domainName")]
        public string DomainName { get; set; }

        [JsonProperty("favicon")]
        public string Favicon { get; set; }

        [JsonProperty("paginationCount")]
        public int PaginationCount { get; set; }

        [JsonProperty("feedEntryCount")]
        public int FeedEntryCount { get; set; }

        [JsonProperty("indexDestinationName")]
        public string IndexDestinationName { get; set; }

        [JsonProperty("feedDestinationName")]
        public string FeedDestinationName { get; set; }

        [JsonProperty("mediaDestinationName")]
        public string MediaDestinationName { get; set; }

        [JsonProperty("archiveDirectoryName")]
        public string ArchiveDirectoryName { get; set; }

        [JsonProperty("archivePageName")]
        public string ArchivePageName { get; set; }

        [JsonProperty("meta")]
        public List<Meta> Meta { get; set; }

        [JsonProperty("scripts")]
        public Scripts Scripts { get; set; }

        [JsonProperty("includes")]
        public Includes Includes { get; set; }

        [JsonProperty("directories")]
        public Directories Directories { get; set; }

        [JsonProperty("code")]
        public Code Code { get; set; }
    }
}