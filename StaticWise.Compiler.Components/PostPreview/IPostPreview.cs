using StaticWise.Entities;

namespace StaticWise.Compiler.Components.PreviewPost
{
    public interface IPostPreview
    {
        /// <summary>
        /// Generate HTML to view a preview of a post
        /// </summary>
        /// <param name="post">The post object</param>
        /// <param name="containerOpenHtml">HTML to open a preview post container</param>
        /// <param name="containerCloseHtml">HTML to close a preview post container</param>
        /// <param name="titleOpenHtml">HTML to open a H1, H2, etc. for a preview post</param>
        /// <param name="titleCloseHtml">HTML to close a H1, H2, etc. for a preview post</param>
        /// <param name="publishedOnText">Text to display in front of the published date</param>
        /// <param name="publishedDateFormat">The date format to use to dispay the published date</param>
        /// <param name="readMoreText">Text to use when linking to the individual post</param>
        /// <returns>A string of HTML</returns>
        string Generate(
            Post post,
            string containerOpenHtml,
            string containerCloseHtml,
            string titleOpenHtml,
            string titleCloseHtml,
            string publishedOnText,
            string publishedDateFormat,
            string readMoreText);
    }
}
