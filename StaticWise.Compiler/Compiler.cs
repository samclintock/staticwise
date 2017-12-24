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
using StaticWise.Compiler.Builders.Feeds;
using StaticWise.Compiler.Builders.Meta;
using StaticWise.Common.Urls;

namespace StaticWise.Compiler
{
    public class Compile
    {
        #region Constants

        int PAGE_SIZE = 5;

        #endregion

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
                IContentBuilder contentBuilder = new ContentBuilder(_log, _fileManager, _config);
                List<string> newUserEntries = new List<string>();
                int totalPageResult = 0, pageOffset = 0, lastPageCount = 0;

                do
                {
                    List<Page> pages = _queryManager.SelectPages(
                        _config.PagesDirIncRoot, pageOffset, PAGE_SIZE);
                    newUserEntries.AddRange(pages.Select(x => $"{x.FriendlyUrl}.html").ToArray());
                    totalPageResult = totalPageResult + contentBuilder.BuildPages(pages);
                    lastPageCount = pages.Count();
                    pageOffset = pageOffset + lastPageCount;
                } while (lastPageCount >= PAGE_SIZE);

                int totalPostResult = 0, postOffset = 0, lastPostCount = 0, archiveCount = 1;
                string archiveDirPath = Path.Combine(_config.OutputDirIncRoot,_config.ArchiveDirectoryName);

                // Delete the archive directory, only if it already exists
                if (_fileManager.IsExistingDirectory(archiveDirPath))
                    _fileManager.DeleteDirectory(archiveDirPath);

                // Get the total number of posts (exclude draft posts)
                int totalPosts = _queryManager.TotalPosts(
                    _config.PostsDirIncRoot, false);

                // Create a new archive directory (if one is necessary)
                if (totalPosts > PAGE_SIZE)
                    _fileManager.CreateDirectory(archiveDirPath);

                do
                {
                    List<Post> posts = _queryManager.SelectPosts(
                        _config.PostsDirIncRoot, postOffset, PAGE_SIZE, false);
                    newUserEntries.AddRange(posts.Select(x => $"{x.FriendlyUrl}.html").ToArray());
                    totalPostResult = totalPostResult + contentBuilder.BuildPosts(posts);
                    lastPostCount = posts.Count();
                    postOffset = postOffset + lastPostCount;
                    
                    /*
                     * Either save a new archive page, (e.g. 
                     * "archive/page2.html"), or save the file 
                     * as the blog, news or homepage (e.g. 
                     * "index.html").
                     */
                    if (archiveCount > 1)
                        contentBuilder.BuildArchivePage(
                            posts,
                            $"{_config.ArchivePageName}{archiveCount}",
                            archiveCount,
                            totalPosts,
                            PAGE_SIZE,
                            _config.ArchiveDirectoryName);
                    else
                        contentBuilder.BuildArchivePage(
                            posts,
                            _config.IndexDestinationName,
                            archiveCount,
                            totalPosts,
                            PAGE_SIZE);

                    archiveCount++;
                } while (lastPostCount > PAGE_SIZE);

                CleanOutputDirectory(newUserEntries);
                
                List<Post> feedEntries = _queryManager.SelectPosts(
                    _config.PostsDirIncRoot, 0, _config.FeedEntryCount, false);

                IFeedBuilder feedBuilder = new FeedBuilder(_fileManager, _urlManager, _log, _config);
                feedBuilder.BuildAtom(feedEntries);
                feedBuilder.BuildSitemap(newUserEntries);

                IMetaBuilder metaBuilder = new MetaBuilder(_urlManager, _fileManager, _log, _config);
                metaBuilder.BuildRobots();

                ICopyFiles copyFiles = new CopyFiles(_fileManager, _log, _config);
                copyFiles.CopyMediaDirectory();
                copyFiles.CopyFaviconFile();

                if (_log != null)
                    _log.Info($"Created a total of {totalPostResult} posts and {totalPageResult} pages");
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
        /// <param name="newUserEntries">A list of user generated entries (e.g. "welcome.html", "about.html")</param>
        private void CleanOutputDirectory(List<string> newUserEntries)
        {
            if (newUserEntries != null)
            {
                // Append application generated files
                newUserEntries.Add($"{_config.IndexDestinationName}.html");

                try
                {
                    Directory.EnumerateFiles(_config.OutputDirIncRoot, "*html")
                        .Select(Path.GetFileName).Except(newUserEntries, StringComparer.OrdinalIgnoreCase)
                        .Select(x => _fileManager.DeleteFile(Path.Combine(_config.OutputDirIncRoot, x)));

                    _log.Info($"Successfully cleaned the output directory: \"{_config.OutputDirIncRoot}\"");
                }
                catch (Exception)
                {
                    _log.Error($"Unable to clean the output directory: \"{_config.OutputDirIncRoot}\"");
                }
            }
        }

        #endregion
    }
}