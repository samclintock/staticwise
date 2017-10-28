using Newtonsoft.Json;
using System.IO;

namespace StaticWise.Entities
{
    public struct Directories
    {
        [JsonProperty("rootPath")]
        public string RootPath;

        [JsonProperty("includesDir")]
        public string IncludesDirExcRoot;

        [JsonProperty("scriptsDir")]
        public string ScriptsDirExcRoot;

        [JsonProperty("postsDir")]
        public string PostsDirExcRoot;

        [JsonProperty("pagesDir")]
        public string PagesDirExcRoot;

        [JsonProperty("mediaDir")]
        public string MediaDirExcRoot;

        [JsonProperty("outputDir")]
        public string OutputDirExcRoot;

        public string IncludesDirIncRoot
        {
            get { return Path.Combine(RootPath, IncludesDirExcRoot); }
        }

        public string ScriptsDirIncRoot
        {
            get { return Path.Combine(RootPath, ScriptsDirExcRoot); }
        }

        public string PostsDirIncRoot
        {
            get { return Path.Combine(RootPath, PostsDirExcRoot); }
        }

        public string PagesDirIncRoot
        {
            get { return Path.Combine(RootPath, PagesDirExcRoot); }
        }

        public string MediaDirIncRoot
        {
            get { return Path.Combine(RootPath, MediaDirExcRoot); }
        }
        
        public string OutputDirIncRoot
        {
            get { return Path.Combine(RootPath, OutputDirExcRoot); }
        }
    }
}