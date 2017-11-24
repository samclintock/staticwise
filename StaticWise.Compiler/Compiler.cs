using StaticWise.Common.Files;
using StaticWise.Common.Queries;
using StaticWise.Compiler.Utilities.Logger;
using StaticWise.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using StaticWise.Compiler.Copiers;
using StaticWise.Compiler.Builders.Content;
using StaticWise.Compiler.Collections;
using StaticWise.Compiler.Builders.Feeds;
using StaticWise.Compiler.Builders.Meta;
using StaticWise.Common.Urls;

namespace StaticWise.Compiler
{
    public class Compile
    {
        #region Properties

        Config _config;
        ILogger _log;
        IFileManager _fileManager;
        IQueryManager _queryManager;
        IUrlManager _urlManager;

        #endregion

        #region Constructors

        public Compile(
            Config config, 
            ILogger log,
            IFileManager fileManager,
            IQueryManager queryManager,
            IUrlManager urlManager)
        {
            if (!string.IsNullOrEmpty(config.RootPath) &&
                log != null &&
                fileManager != null &&
                queryManager != null &&
                urlManager != null)
            {
                _config = config;
                _log = log;
                _fileManager = fileManager;
                _queryManager = queryManager;
                _urlManager = urlManager;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Build the static website by using the configuration object
        /// </summary>
        public void Build()
        {
            if (!string.IsNullOrEmpty(_config.RootPath))
            {
                IContentEntries contentEntries = new ContentEntries(
                    _queryManager, _config);
                List<Page> pages = contentEntries.GetPublishablePages();
                List<Post> posts = contentEntries.GetPublishablePosts();
                List<string> newEntries = contentEntries.CombineContentEntries(
                    pages, posts);
                
                CleanOutputDirectory(newEntries);

                IContentBuilder contentBuilder = new ContentBuilder(
                    _log, _fileManager, _config);
                int builtPosts = contentBuilder.BuildPosts(posts);
                int builtPages = contentBuilder.BuildPages(pages);
                contentBuilder.BuildArchive(posts.ToList());

                IFeedBuilder feedBuilder = new FeedBuilder(
                    _fileManager, _urlManager, _log, _config);
                feedBuilder.BuildAtom(posts.Take(_config.FeedEntryCount).ToList());
                feedBuilder.BuildSitemap(newEntries);

                IMetaBuilder metaBuilder = new MetaBuilder(
                    _urlManager, _fileManager, _log, _config);
                metaBuilder.BuildRobots();

                ICopyFiles copyFiles = new CopyFiles(_fileManager, _log, _config);
                copyFiles.CopyMediaDirectory();
                copyFiles.CopyFaviconFile();

                if (_log != null)
                    _log.Info($"Created a total of {builtPosts} posts and {builtPages} pages");
            }
            else
                throw new Exception("Please check the directory structure is setup correctly in the configuration file");
        }

        /// <summary>
        /// To save compile time, only delete files that the user will not need.
        /// E.g. If a markdown file is changed to a draft, delete the HTML 
        /// file from the output directory and only re-add it when it is changed 
        /// to published
        /// </summary>
        /// <param name="entries">A list of user generated entries (e.g. "welcome.html", "about.html")</param>
        private void CleanOutputDirectory(List<string> entries)
        {
            if (entries != null)
            {
                // Append application generated files
                entries.Add($"{_config.IndexDestinationName}.html");

                try
                {
                    IEnumerable<string> oldEntries =
                        Directory.EnumerateFiles(_config.OutputDirIncRoot, "*html")
                        .Select(Path.GetFileName);

                    IEnumerable<string> deleteEntries = oldEntries.Except(
                        entries, StringComparer.OrdinalIgnoreCase);

                    if (deleteEntries.Any())
                    {
                        foreach (var entry in deleteEntries)
                        {
                            _fileManager.DeleteFile(Path.Combine(_config.OutputDirIncRoot, entry));
                        }
                    }

                    _log.Info($"Cleaned output directory and deleted a total of {deleteEntries.Count()} unwanted entries");
                }
                catch (Exception)
                {
                    _log.Error("Unable to clean output directory. Please delete unwanted files manually");
                }
            }
        }

        #endregion
    }
}