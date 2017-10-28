using Newtonsoft.Json;
using System;

namespace StaticWise.Entities
{
    public struct Post
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("datetime")]
        public DateTime Datetime { get; set; }

        [JsonProperty("friendlyUrl")]
        public string FriendlyUrl { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("isDraft")]
        public bool IsDraft { get; set; }

        public string MarkdownContent { get; set; }

        /// <summary>
        /// The file path including the root (e.g. "C:\website\markdown.md")
        /// </summary>
        public string FilePathIncRoot { get; set; }
    }
}