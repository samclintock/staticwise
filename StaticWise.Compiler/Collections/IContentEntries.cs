using StaticWise.Entities;
using System.Collections.Generic;

namespace StaticWise.Compiler.Collections
{
    internal interface IContentEntries
    {
        /// <summary>
        /// Get a list of publishable posts (that are not drafts) to be converted to HTML
        /// </summary>
        /// <returns>A list of (non-draft) posts, in decending order by published date</returns>
        List<Post> GetPublishablePosts();

        /// <summary>
        /// Get a list of publishable pages (that are not drafts) to be converted to HTML.
        /// </summary>
        /// <returns>A list of (non-draft) pages, in decending order by published date</returns>
        List<Page> GetPublishablePages();

        /// <summary>
        /// Get a list of all publishable pages and posts (non-draft) to be converted to HTML.
        /// The list will only contain filename entries (e.g. "about.html")
        /// </summary>
        /// <param name="pages">The list of pages to be converted into HTML</param>
        /// <param name="posts">The list of posts to be converted into HTML</param>
        /// <returns>A list of new user entries. The file "index.html" should be excluded</returns>
        List<string> CombineContentEntries(IEnumerable<Page> pages, IEnumerable<Post> posts);
    }
}
