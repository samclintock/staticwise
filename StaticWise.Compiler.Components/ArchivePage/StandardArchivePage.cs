using StaticWise.Common.Files;
using StaticWise.Compiler.Components.Pagination;
using StaticWise.Compiler.Components.PreviewPost;
using StaticWise.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StaticWise.Compiler.Components.ArchivePage
{
    public class StandardArchivePage : IArchivePage
    {
        #region Properties

        IFileManager _fileManager;

        #endregion

        #region Constructors

        public StandardArchivePage(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        #endregion

        #region Methods

        string IArchivePage.Generate(
            List<Post> posts,
            int currentPage,
            int totalPosts,
            Config config)
        {
            if (posts == null)
                return string.Empty;

            StringBuilder b = new StringBuilder();

            if (!string.IsNullOrEmpty(config.Code.DocumentType))
                b.Append(config.Code.DocumentType);

            if (!string.IsNullOrEmpty(config.Code.Html.ElementAtOrDefault(0)))
                b.Append(config.Code.Html.ElementAtOrDefault(0));

            if (!string.IsNullOrEmpty(config.Code.Head.ElementAtOrDefault(0)))
                b.Append(config.Code.Head.ElementAtOrDefault(0));

            if (!string.IsNullOrEmpty(config.Title))
                b.Append($"<title>{config.Title}</title>");

            if (!string.IsNullOrEmpty(config.Description))
                b.Append($"<meta name=\"description\" content=\"{config.Description}\" />");

            if (!string.IsNullOrEmpty(config.FeedDestinationName))
                b.Append($"<link rel=\"alternate\" type=\"application/atom+xml\" title=\"Archive\" href=\"{config.FeedDestinationName}.xml\" />");

            if (config.Meta != null && config.Meta.Any())
                foreach (Meta meta in config.Meta)
                    if (meta.Attribute != null && meta.Attribute.Any())
                    {
                        StringBuilder attributeBuilder =
                            new StringBuilder();

                        foreach (Attr attribute in meta.Attribute)
                            if (!string.IsNullOrEmpty(attribute.Key) && !string.IsNullOrEmpty(attribute.Value))
                                attributeBuilder.Append($"{attribute.Key}=\"{attribute.Value}\" ");

                        if (attributeBuilder.Length > 0)
                            b.Append($"<meta {attributeBuilder.ToString()}/>");
                    }

            if (!string.IsNullOrEmpty(config.Favicon))
                b.Append($"<link rel=\"icon\" href=\"{config.Favicon}\" />");

            if (config.Scripts.ExternalCSS != null && config.Scripts.ExternalCSS.Any())
                foreach (string css in config.Scripts.ExternalCSS)
                    if (!string.IsNullOrEmpty(css))
                        b.Append($"<link rel=\"stylesheet\" href=\"{css}\" />");

            if (!string.IsNullOrEmpty(config.Directories.RootPath) &&
                config.Scripts.InternalCSS != null && config.Scripts.InternalCSS.Any())
            {
                StringBuilder cssBuilder = new StringBuilder();

                foreach (string css in config.Scripts.InternalCSS)
                    if (!string.IsNullOrEmpty(css))
                        cssBuilder.Append(_fileManager.GetTextFromFile(
                            Path.Combine(config.Directories.ScriptsDirIncRoot, css)));

                b.Append($"<style type=\"text/css\">{cssBuilder.ToString()}</style>");
            }

            if (!string.IsNullOrEmpty(config.Code.Head.ElementAtOrDefault(1)))
                b.Append(config.Code.Head.ElementAtOrDefault(1));

            if (!string.IsNullOrEmpty(config.Code.Body.ElementAtOrDefault(0)))
                b.Append(config.Code.Body.ElementAtOrDefault(0));

            b.Append(_fileManager.GetTextFromFile(
                Path.Combine(config.Directories.IncludesDirIncRoot,
                config.Includes.SiteHeader)));

            if (!string.IsNullOrEmpty(config.Code.PageContainer.ElementAtOrDefault(0)))
                b.Append(config.Code.PageContainer.ElementAtOrDefault(0));

            IPostPreview archivePosts = new StandardPostPreview();
            for (int i = 0; i < posts.Count; i++)
            {
                b.Append(archivePosts.Generate(posts[i],
                    config.Code.ArchivePostContainer.ElementAtOrDefault(0),
                    config.Code.ArchivePostContainer.ElementAtOrDefault(1),
                    config.Code.ArchivePostTitle.ElementAtOrDefault(0),
                    config.Code.ArchivePostTitle.ElementAtOrDefault(1),
                    config.Code.PublishedOnText,
                    config.Code.PublishedDateFormat,
                    config.Code.ReadMoreText));
            }

            IPagination pagination = new StandardPagination();
            b.Append(pagination.Generate(
                currentPage,
                totalPosts,
                config.IndexDestinationName,
                config.ArchivePageName,
                config.ArchiveDirectoryName));

            if (!string.IsNullOrEmpty(config.Code.PageContainer.ElementAtOrDefault(1)))
                b.Append(config.Code.PageContainer.ElementAtOrDefault(1));

            b.Append(_fileManager.GetTextFromFile(
                Path.Combine(config.Directories.IncludesDirIncRoot,
                config.Includes.SiteFooter)));

            if (config.Scripts.ExternalJS != null && config.Scripts.ExternalJS.Any())
                foreach (string js in config.Scripts.ExternalJS)
                    if (!string.IsNullOrEmpty(js))
                        b.Append($"<script type=\"text/javascript\" src=\"{js}\" />");

            if (!string.IsNullOrEmpty(config.Directories.RootPath) &&
                config.Scripts.InternalJS != null && config.Scripts.InternalJS.Any())
            {
                StringBuilder jsBuilder = new StringBuilder();

                foreach (string js in config.Scripts.InternalJS)
                    if (!string.IsNullOrEmpty(js))
                        jsBuilder.Append(_fileManager.GetTextFromFile(
                            Path.Combine(config.Directories.ScriptsDirIncRoot, js)));

                b.Append($"<script type=\"text/javascript\">{jsBuilder.ToString()}</script>");
            }

            if (!string.IsNullOrEmpty(config.Code.Body.ElementAtOrDefault(1)))
                b.Append(config.Code.Body.ElementAtOrDefault(1));

            if (!string.IsNullOrEmpty(config.Code.Html.ElementAtOrDefault(1)))
                b.Append(config.Code.Html.ElementAtOrDefault(1));

            return b.ToString();
        }

        #endregion
    }
}
