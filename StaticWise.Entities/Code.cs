using Newtonsoft.Json;

namespace StaticWise.Entities
{
    public struct Code
    {
        [JsonProperty("documentType")]
        public string DocumentType { get; set; }

        [JsonProperty("html")]
        public string[] Html { get; set; }

        [JsonProperty("head")]
        public string[] Head { get; set; }

        [JsonProperty("body")]
        public string[] Body { get; set; }

        [JsonProperty("archivePostContainer")]
        public string[] ArchivePostContainer { get; set; }

        [JsonProperty("archivePostTitle")]
        public string[] ArchivePostTitle { get; set; }

        [JsonProperty("individualPostContainer")]
        public string[] IndividualPostContainer { get; set; }

        [JsonProperty("individualPostTitle")]
        public string[] IndividualPostTitle { get; set; }

        [JsonProperty("pageContainer")]
        public string[] PageContainer { get; set; }

        [JsonProperty("pageTitle")]
        public string[] PageTitle { get; set; }

        [JsonProperty("publishedDateFormat")]
        public string PublishedDateFormat { get; set; }

        [JsonProperty("publishedOnText")]
        public string PublishedOnText { get; set; }

        [JsonProperty("readMoreText")]
        public string ReadMoreText { get; set; }

        [JsonProperty("markdownDateFormat")]
        public string MarkdownDateFormat { get; set; }
    }
}