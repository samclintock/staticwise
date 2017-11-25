namespace StaticWise.Compiler.Components.Pagination
{
    public interface IPagination
    {
        /// <summary>
        /// Create HTML to navigate between archived pages
        /// </summary>
        /// <param name="currentPage">The number of the current page where the navigation is being displayed</param>
        /// <param name="totalPosts">The total number of posts</param>
        /// <param name="paginationCount">The number of posts per page</param>
        /// <param name="containerOpenHtml">HTML to open a pagination container ("<ul>")</param>
        /// <param name="containerCloseHtml">HTML to close a pagination container ("</ul>")</param>
        /// <param name="itemOpenHtml">HTML to open a pagination item ("<li>")</param>
        /// <param name="itemCloseHtml">HTML to close a pagination item ("</li>")</param>
        /// <param name="firstFileName">The name of the first archived file (e.g. "index.html, "blog.html", "archive.html") (Optional)</param>
        /// <param name="archiveFileName">The name to use for the additional pages (e.g. "pageX", "archiveX") (Optional)</param>
        /// <param name="archiveDirectoryName">The name of where the archived pages are stored (Optional)</param>
        /// <returns>A string of HTML</returns>
        string Generate(
            int currentPage,
            int totalPosts,
            int paginationCount,
            string containerOpenHtml,
            string containerCloseHtml,
            string itemOpenHtml,
            string itemCloseHtml,
            string firstFileName= "",
            string archiveFileName = "",
            string archiveDirectoryName = "");
    }
}