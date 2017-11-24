using StaticWise.Common.Files;
using StaticWise.Common.Urls;
using StaticWise.Compiler.Utilities.Logger;
using StaticWise.Entities;
using System.IO;
using System.Text;

namespace StaticWise.Compiler.Builders.Meta
{
    internal class MetaBuilder : IMetaBuilder
    {
        #region Constants

        private const string ROBOTS_FILE = "robots.txt";
        private const string SITEMAP_FILE = "sitemap.xml";

        #endregion

        #region Properties

        IUrlManager _urlManager;
        IFileManager _fileManager;
        ILogger _log;
        Config _config;

        #endregion

        #region Constructors

        public MetaBuilder(
            IUrlManager urlManager,
            IFileManager fileManager,
            ILogger log,
            Config config)
        {
            _urlManager = urlManager;
            _fileManager = fileManager;
            _log = log;
            _config = config;
        }

        #endregion

        #region Methods

        void IMetaBuilder.BuildRobots()
        {
            StringBuilder b = new StringBuilder();
            b.Append($"Sitemap: {_urlManager.CreateUrl(_config.DomainName, SITEMAP_FILE)}");

            string filePath = Path.Combine(
                _config.OutputDirIncRoot, ROBOTS_FILE);
            if (_fileManager.SaveTextToFile(filePath, b.ToString()))
                _log.Info($"Created robots file: \"{filePath}\"");
            else
                _log.Error($"Unable to create robots file: \"{filePath}\"");
        }

        #endregion
    }
}
