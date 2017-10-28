using StaticWise.Common.Files;
using StaticWise.Common.Urls;
using StaticWise.Compiler.Components.Atom;
using StaticWise.Compiler.Components.Sitemap;
using StaticWise.Compiler.Utilities.Logger;
using StaticWise.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StaticWise.Compiler.Builders.Feeds
{
    internal class FeedBuilder : IFeedBuilder
    {
        #region Constants

        private const string SITEMAP_FILE = "sitemap.xml";

        #endregion

        #region Properties

        IFileManager _fileManager;
        IUrlManager _urlManager;
        ILogger _log;
        Config _config;

        #endregion

        #region Constructors

        public FeedBuilder(
            IFileManager fileManager,
            IUrlManager urlManager,
            ILogger log,
            Config config)
        {
            _fileManager = fileManager;
            _urlManager = urlManager;
            _log = log;
            _config = config;
        }

        #endregion

        #region Methods

        void IFeedBuilder.BuildAtom(List<Post> posts)
        {
            if (posts != null)
            {
                IAtom atom = new StandardAtom(_urlManager);
                string pageSource = atom.Generate(
                    posts, _config.DomainName, _config.Title, _config.Description);

                string filePath = Path.Combine(_config.Directories.OutputDirIncRoot,
                        $"{_config.FeedDestinationName}.xml");
                if (_fileManager.SaveTextToFile(filePath, pageSource))
                    _log.Info($"Created feed file \"{filePath}\" with a total of {posts.Count()} entries");
                else
                    _log.Error($"Unable to create feed file: \"{filePath}\"");
            }
        }

        void IFeedBuilder.BuildSitemap(List<string> entries)
        {
            if (entries != null)
            {
                ISitemap sitemap = new StandardSitemap(_urlManager);
                string pageSource = sitemap.Generate(entries, _config.DomainName);

                string filePath = Path.Combine(
                    _config.Directories.OutputDirIncRoot, SITEMAP_FILE);
                if (_fileManager.SaveTextToFile(filePath, pageSource))
                    _log.Info($"Created sitemap file \"{filePath}\" with a total of {entries.Count()} entries");
                else
                    _log.Error($"Unable to create sitemap file: \"{filePath}\"");
            }
        }

        #endregion
    }
}
