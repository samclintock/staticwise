using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

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

        [JsonProperty("code")]
        public Code Code { get; set; }

        [JsonProperty("includesDirectory")]
        public string IncludesDirExcRoot;

        [JsonProperty("scriptsDirectory")]
        public string ScriptsDirExcRoot;

        [JsonProperty("postsDirectory")]
        public string PostsDirExcRoot;

        [JsonProperty("pagesDirectory")]
        public string PagesDirExcRoot;

        [JsonProperty("mediaDirectory")]
        public string MediaDirExcRoot;

        [JsonProperty("outputDirectory")]
        public string OutputDirExcRoot;

        [JsonIgnore]
        public string FilePath;

        [JsonIgnore]
        public string RootPath;

        [JsonIgnore]
        public string IncludesDirIncRoot
        {
            get { return Path.Combine(RootPath, IncludesDirExcRoot); }
        }

        [JsonIgnore]
        public string ScriptsDirIncRoot
        {
            get { return Path.Combine(RootPath, ScriptsDirExcRoot); }
        }

        [JsonIgnore]
        public string PostsDirIncRoot
        {
            get { return Path.Combine(RootPath, PostsDirExcRoot); }
        }

        [JsonIgnore]
        public string PagesDirIncRoot
        {
            get { return Path.Combine(RootPath, PagesDirExcRoot); }
        }

        [JsonIgnore]
        public string MediaDirIncRoot
        {
            get { return Path.Combine(RootPath, MediaDirExcRoot); }
        }

        [JsonIgnore]
        public string OutputDirIncRoot
        {
            get { return Path.Combine(RootPath, OutputDirExcRoot); }
        }
    }
}