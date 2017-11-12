namespace StaticWise.Compiler.Components.Pagination
{
    public interface IPagination
    {
        /// <summary>
        /// Create HTML to navigate between archived pages
        /// </summary>
        /// <param name="current">The number of the current page where the navigation is being displayed</param>
        /// <param name="total">The total number of posts</param>
        /// <param name="containerOpenHtml">HTML to open a pagination container ("<ul>")</param>
        /// <param name="containerCloseHtml">HTML to close a pagination container ("</ul>")</param>
        /// <param name="itemOpenHtml">HTML to open a pagination item ("<li>")</param>
        /// <param name="itemCloseHtml">HTML to close a pagination item ("</li>")</param>
        /// <param name="firstFileName">The name of the first archived file (e.g. "index.html, "blog.html", "archive.html") (Optional)</param>
        /// <param name="archiveFileName">The name to use for the additional pages (e.g. "pageX", "archiveX") (Optional)</param>
        /// <param name="archiveDirectoryName">The name of where the archived pages are stored (Optional)</param>
        /// <returns>A string of HTML</returns>
        string Generate(
            int current,
            int total,
            string containerOpenHtml,
            string containerCloseHtml,
            string itemOpenHtml,
            string itemCloseHtml,
            string firstFileName= "",
            string archiveFileName = "",
            string archiveDirectoryName = "");
    }
}