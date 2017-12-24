using Markdig;
using StaticWise.Common.Files;
using StaticWise.Entities;
using System.IO;
using System.Linq;
using System.Text;

namespace StaticWise.Compiler.Components.IndividualPost
{
    public class StandardPost : IIndividualPost
    {
        #region Constants

        private const string TIME_DATE_FORMAT = "yyyy-MM-dd H:mmzzz";

        #endregion

        #region Properties

        IFileManager _fileManager;

        #endregion

        #region Constructors

        public StandardPost(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        #endregion

        #region Methods

        string IIndividualPost.Generate(
            Post post,
            Config config)
        {
            StringBuilder b = new StringBuilder();

            if (!string.IsNullOrEmpty(config.Code.DocumentType))
                b.Append(config.Code.DocumentType);

            if (!string.IsNullOrEmpty(config.Code.Html.ElementAtOrDefault(0)))
                b.Append(config.Code.Html.ElementAtOrDefault(0));

            if (!string.IsNullOrEmpty(config.Code.Head.ElementAtOrDefault(0)))
                b.Append(config.Code.Head.ElementAtOrDefault(0));

            if (!string.IsNullOrEmpty(post.Title))
                b.Append($"<title>{post.Title}</title>");

            if (!string.IsNullOrEmpty(post.Description))
                b.Append($"<meta name=\"description\" content=\"{post.Description}\" />");

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

            if (!string.IsNullOrEmpty(config.RootPath) &&
                config.Scripts.InternalCSS != null && config.Scripts.InternalCSS.Any())
            {
                StringBuilder cssBuilder = new StringBuilder();

                foreach (string css in config.Scripts.InternalCSS)
                    if (!string.IsNullOrEmpty(css))
                        cssBuilder.Append(_fileManager.GetTextFromFile(
                            Path.Combine(config.ScriptsDirIncRoot, css)));

                b.Append($"<style type=\"text/css\">{cssBuilder.ToString()}</style>");
            }

            if (!string.IsNullOrEmpty(config.Code.Head.ElementAtOrDefault(1)))
                b.Append(config.Code.Head.ElementAtOrDefault(1));

            if (!string.IsNullOrEmpty(config.Code.Body.ElementAtOrDefault(0)))
                b.Append(config.Code.Body.ElementAtOrDefault(0));

            b.Append(_fileManager.GetTextFromFile(
                Path.Combine(config.IncludesDirIncRoot,
                config.Includes.SiteHeader)));

            if (!string.IsNullOrEmpty(config.Code.IndividualPostContainer.ElementAtOrDefault(0)))
                b.Append(config.Code.IndividualPostContainer.ElementAtOrDefault(0));

            b.Append(_fileManager.GetTextFromFile(
                Path.Combine(config.IncludesDirIncRoot,
                config.Includes.PostHeader)));

            if (!string.IsNullOrEmpty(config.Code.IndividualPostTitle.ElementAtOrDefault(0))
                && !string.IsNullOrEmpty(config.Code.IndividualPostTitle.ElementAtOrDefault(1)))
                b.Append(string.Concat(
                    config.Code.IndividualPostTitle.ElementAtOrDefault(0),
                    post.Title,
                    config.Code.IndividualPostTitle.ElementAtOrDefault(1)));

            if (!string.IsNullOrEmpty(config.Code.PublishedOnText) &&
                !string.IsNullOrEmpty(config.Code.PublishedDateFormat))
            {
                string publishedText = $"{config.Code.PublishedOnText}{post.FileDate.ToString(config.Code.PublishedDateFormat)}";
                b.Append($"<time datetime=\"{post.FileDate.ToString(TIME_DATE_FORMAT)}\">{publishedText}</time>");
            }

            if (!string.IsNullOrEmpty(post.FileContent))
                b.Append(Markdown.ToHtml(post.FileContent));

            b.Append(_fileManager.GetTextFromFile(
                Path.Combine(config.IncludesDirIncRoot,
                config.Includes.PostFooter)));

            if (!string.IsNullOrEmpty(config.Code.IndividualPostContainer.ElementAtOrDefault(1)))
                b.Append(config.Code.IndividualPostContainer.ElementAtOrDefault(1));

            b.Append(_fileManager.GetTextFromFile(
                Path.Combine(config.IncludesDirIncRoot,
                config.Includes.SiteFooter)));

            if (config.Scripts.ExternalJS != null && config.Scripts.ExternalJS.Any())
                foreach (string js in config.Scripts.ExternalJS)
                    if (!string.IsNullOrEmpty(js))
                        b.Append($"<script type=\"text/javascript\" src=\"{js}\" />");

            if (!string.IsNullOrEmpty(config.RootPath) &&
                config.Scripts.InternalJS != null && config.Scripts.InternalJS.Any())
            {
                StringBuilder jsBuilder = new StringBuilder();

                foreach (string js in config.Scripts.InternalJS)
                    if (!string.IsNullOrEmpty(js))
                        jsBuilder.Append(_fileManager.GetTextFromFile(
                            Path.Combine(config.ScriptsDirIncRoot, js)));

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
