using StaticWise.Entities;
using System.Collections.Generic;

namespace StaticWise.Compiler.Builders.Feeds
{
    internal interface IFeedBuilder
    {
        /// <summary>
        /// Create an Atom feed and save it to the output directory
        /// </summary>
        /// <param name="posts">A list of posts, preferably in decending order by published date</param>
        void BuildAtom(List<Post> posts);

        /// <summary>
        /// Create a sitemap file ("sitemap.xml") and save it to the output directory.
        /// The sitemap file is for search engines to discover and index all pages and posts
        /// </summary>
        /// <param name="entries">A list of user generated entries (e.g. "welcome.html", "about.html")</param>
        void BuildSitemap(List<string> entries);
    }
}
