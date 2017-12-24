using StaticWise.Entities;
using System.Text;

namespace StaticWise.Compiler.Components.PreviewPost
{
    public class StandardPostPreview : IPostPreview
    {
        #region Constants

        private const string TIME_DATE_FORMAT = "yyyy-MM-dd H:mmzzz";

        #endregion

        #region Methods

        string IPostPreview.Generate(
            Post post,
            string containerOpenHtml,
            string containerCloseHtml,
            string titleOpenHtml,
            string titleCloseHtml,
            string publishedOnText,
            string publishedDateFormat,
            string readMoreText)
        {
            StringBuilder builder = new StringBuilder();

            string path = string.Format($"{post.FriendlyUrl}.html");

            builder.Append(containerOpenHtml);
            builder.Append(string.Concat(
                titleOpenHtml,
                $"<a href=\"{path}\" title=\"{post.Title}\">{post.Title}</a>",
                titleCloseHtml));

            if (!string.IsNullOrEmpty(publishedDateFormat) &&
                !string.IsNullOrEmpty(publishedOnText))
            {
                string publishedTime = post.FileDate.ToString(TIME_DATE_FORMAT);
                string publishedText = string.Format("{0}{1}",
                    publishedOnText, post.FileDate.ToString(publishedDateFormat));

                builder.Append($"<time datetime=\"{publishedTime}\">{publishedText}</time>");
            }

            builder.Append(
                $"<p>{post.Description} <a href=\"{path}\" title=\"{post.Title}\">{readMoreText}</a></p>");

            builder.Append(containerCloseHtml);

            return builder.ToString();
        }

        #endregion
    }
}