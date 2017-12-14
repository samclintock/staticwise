using StaticWise.Entities;
using System.Collections.Generic;

namespace StaticWise.Compiler.Components.ArchivePage
{
    public interface IArchivePage
    {
        /// <summary>
        /// Generate HTML to view an archive page
        /// </summary>
        /// <param name="posts">A list of post objects</param>
        /// <param name="currentPage">The number of the current page where the navigation is being displayed</param>
        /// <param name="totalPosts">The total number of posts</param>
        /// <param name="paginationCount">The total number of posts to appear on the archive page</param>
        /// <param name="config">The config object</param>
        /// <returns>A string of HTML</returns>
        string Generate(
            List<Post> posts,
            int currentPage,
            int totalPosts,
            int paginationCount,
            Config config);
    }
}