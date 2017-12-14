using StaticWise.Entities;
using System.Collections.Generic;

namespace StaticWise.Compiler.Builders.Content
{
    internal interface IContentBuilder
    {
        /// <summary>
        /// Convert a list of pages into HTML and save them to the output directory
        /// </summary>
        /// <param name="pages">A list of publishable (non-draft) pages</param>
        /// <returns>An integer representing how many pages have been successfully built</returns>
        int BuildPages(List<Page> pages);

        /// <summary>
        /// Convert a list of posts into HTML and save them to the output directory
        /// </summary>
        /// <param name="posts">A list of publishable (non-draft) posts</param>
        /// <returns>An integer representing how many posts have been successfully built</returns>
        int BuildPosts(List<Post> posts);

        /// <summary>
        /// Convert a post object into HTML and save it to the output directory
        /// </summary>
        /// <param name="post">The post to be converted and saved as a HTML file</param>
        /// <returns>A boolean value representing if the build was successful or not</returns>
        bool BuildIndividualPost(Post post);

        /// <summary>
        /// Convert a page object into HTML and save it to the output directory
        /// </summary>
        /// <param name="page">The page to be converted and saved as a HTML file</param>
        /// <returns>A boolean value representing if the build was successful or not</returns>
        bool BuildIndividualPage(Page page);

        /// <summary>
        /// Build an individual home page as a series of posts and save it to the output directory
        /// </summary>
        /// <param name="posts">A list of posts, preferably in decending order by published date</param>
        /// <param name="friendlyUrl">The name of the page (e.g. "index", or "page2")</param>
        /// <param name="currentPage">The number of the current page where the navigation is being displayed</param>
        /// <param name="totalPosts">The total number of posts</param>
        /// <param name="paginationCount">The total number of posts to appear on the archive page</param>
        /// <param name="directoryName">The name of the directory to save the file (Optional)</param>
        void BuildArchivePage(
            List<Post> posts,
            string friendlyUrl,
            int currentPage,
            int totalPosts,
            int paginationCount,
            string directoryName = "");
    }
}
