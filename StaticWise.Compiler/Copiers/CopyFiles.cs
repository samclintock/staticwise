using StaticWise.Common.Files;
using StaticWise.Compiler.Utilities.Logger;
using StaticWise.Entities;
using System.IO;

namespace StaticWise.Compiler.Copiers
{
    internal class CopyFiles : ICopyFiles
    {
        #region Properties

        IFileManager _fileManager;
        ILogger _log;
        Config _config;

        #endregion

        #region Constructors

        public CopyFiles(IFileManager fileManager, ILogger log, Config config)
        {
            _fileManager = fileManager;
            _log = log;
            _config = config;
        }

        #endregion

        #region Methods

        void ICopyFiles.CopyMediaDirectory()
        {
            if (_fileManager.CopyDirectory(
                _config.MediaDirIncRoot,
                Path.Combine(_config.OutputDirIncRoot, _config.MediaDestinationName)))
                _log.Info("Transferred the media directory to the output directory");
            else
                _log.Error("Unable to transfer the media directory. Please check you have sufficient permission");
        }

        void ICopyFiles.CopyFaviconFile()
        {
            if (_fileManager.CopyFile(Path.Combine(_config.RootPath, _config.Favicon),
                Path.Combine(_config.OutputDirIncRoot, _config.Favicon)))
                _log.Info("Transferred the favicon file to the output directory");
            else
                _log.Error("Unable to transfer the favicon file. Please check you have sufficient permission");
        }

        #endregion
    }
}
