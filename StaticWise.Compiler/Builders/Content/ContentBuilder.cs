using StaticWise.Common.Files;
using StaticWise.Compiler.Components.ArchivePage;
using StaticWise.Compiler.Components.IndividualPage;
using StaticWise.Compiler.Components.IndividualPost;
using StaticWise.Compiler.Utilities.Logger;
using StaticWise.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StaticWise.Compiler.Builders.Content
{
    internal class ContentBuilder : IContentBuilder
    {
        #region Properties

        IFileManager _fileManager;
        ILogger _log;
        Config _config;

        #endregion

        #region Constructors

        public ContentBuilder(
            ILogger log,
            IFileManager fileManager,
            Config config)
        {
            _log = log;
            _fileManager = fileManager;
            _config = config;
        }

        #endregion

        #region Methods

        int IContentBuilder.BuildPages(List<Page> pages)
        {
            int result = 0;

            if (pages != null)
                if (pages.Any())
                    for (int i = 0; i < pages.Count; i++)
                        if (((IContentBuilder)this).BuildIndividualPage(pages[i]))
                            result++;

            return result;
        }

        int IContentBuilder.BuildPosts(List<Post> posts)
        {
            int result = 0;

            if (posts != null)
            {
                if (posts.Any())
                {
                    for (int i = 0; i < posts.Count; i++)
                        if (((IContentBuilder) this).BuildIndividualPost(posts[i]))
                            result++;
                }
            }

            return result;
        }

        bool IContentBuilder.BuildIndividualPost(Post post)
        {
            bool result = false;

            try
            {
                IIndividualPost standardPost = new StandardPost(_fileManager);
                string postSource = standardPost.Generate(post, _config);

                string filePath = Path.Combine(_config.Directories.OutputDirIncRoot,
                    $"{post.FriendlyUrl}.html");

                if (_fileManager.SaveTextToFile(filePath,
                    _fileManager.CompressHtml(postSource)))
                {
                    _log.Info($"Created post file: \"{filePath}\"");
                    result = true;
                }
                else
                    _log.Error($"Unable to create post file: \"{filePath}\"");
            }
            catch (Exception ex)
            {
                _log.Error($"Unable to generate post \"{post.Title}\". Error: {ex.Message}");
            }

            return result;
        }

        bool IContentBuilder.BuildIndividualPage(Page page)
        {
            bool result = false;

            try
            {
                IIndividualPage standardPage = new StandardPage(_fileManager);
                string pageSource = standardPage.Generate(page, _config);

                string filePath = Path.Combine(_config.Directories.OutputDirIncRoot, 
                    $"{page.FriendlyUrl}.html");

                if (_fileManager.SaveTextToFile(filePath, 
                    _fileManager.CompressHtml(pageSource)))
                {
                    _log.Info($"Created page file: \"{filePath}\"");
                    result = true;
                }
                else
                    _log.Error($"Unable to create page file: \"{filePath}\"");
            }
            catch (Exception ex)
            {
                _log.Error($"Unable to generate page \"{page.Title}\". Error: {ex.Message}");
            }

            return result;
        }

        void IContentBuilder.BuildArchive(List<Post> posts)
        {
            if (posts != null)
            {
                try
                {
                    int currentArchivePage = 1, currentPost = 1;
                    List<Post> currentPosts = new List<Post>();
                    string archiveDirPath = Path.Combine(
                        _config.Directories.OutputDirIncRoot,
                        _config.ArchiveDirectoryName);

                    /*
                     * Delete the archive directory, 
                     * only if it already exists.
                     */
                    if (_fileManager.IsExistingDirectory(archiveDirPath))
                        _fileManager.DeleteDirectory(archiveDirPath);

                    // Create a new archive directory (if one is necessary)
                    if (posts.Count > _config.PaginationCount)
                        _fileManager.CreateDirectory(archiveDirPath);

                    foreach (Post post in posts)
                    {
                        currentPosts.Add(post);

                        /*
                         * Only create and save a new file when all 
                         * pages for a archive page are ready,
                         * or alternatively, when we've hit the last
                         * post in the collection.
                         */
                        if ((currentPosts.Count == _config.PaginationCount) 
                            || (currentPost == posts.Count))
                        {
                            /*
                             * Either save a new archive page, (e.g. 
                             * "archive/page2.html"), or save the file 
                             * as the blog, news or homepage (e.g. 
                             * "index.html").
                             */
                            if (currentPost > _config.PaginationCount)
                                ((IContentBuilder)this).BuildHomepage(
                                    currentPosts,
                                    $"{_config.ArchivePageName}{currentArchivePage}",
                                    posts.Count,
                                    currentPost,
                                    _config.ArchiveDirectoryName);
                            else
                                ((IContentBuilder)this).BuildHomepage(
                                    currentPosts,
                                    _config.IndexDestinationName,
                                    posts.Count,
                                    currentPost);

                            /* Once the current posts for the current archive 
                             * page have been saved to a new file, empty the 
                             * collection and start again.
                             */
                            currentPosts.Clear();

                            currentArchivePage++;
                        }

                        currentPost++;
                    }
                }
                catch (Exception ex)
                {
                    _log.Error($"Unable to generate an archive page. Error: {ex.Message}");
                }
            }
        }

        void IContentBuilder.BuildHomepage(
            List<Post> posts, 
            string fileNameWithoutExt,
            int currentPage,
            int totalPosts,
            string directoryName)
        {
            if (!(posts == null || string.IsNullOrEmpty(fileNameWithoutExt)))
            {
                IArchivePage standardArchive = new StandardArchivePage(_fileManager);
                string pageSource = standardArchive.Generate(
                    posts, currentPage, totalPosts, _config);

                string filePath = Path.Combine(
                    _config.Directories.OutputDirIncRoot, 
                    $"{fileNameWithoutExt}.html");

                // Alter the destination for the archive page file (if desired)
                if (!string.IsNullOrEmpty(directoryName))
                    filePath = Path.Combine(Path.Combine(
                        _config.Directories.OutputDirIncRoot, directoryName), 
                        $"{fileNameWithoutExt}.html");

                if (_fileManager.SaveTextToFile(filePath, 
                    _fileManager.CompressHtml(pageSource)))
                    _log.Info($"Created archive file: \"{filePath}\"");
                else
                    _log.Error($"Unable to create archive file: \"{filePath}\"");
            }
        }

        #endregion
    }
}
