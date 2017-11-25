using StaticWise.Entities;
using System.Collections.Generic;

namespace StaticWise.Common.Queries
{
    public interface IQueryManager
    {
        /// <summary>
        /// Get all pages by deserializing source files
        /// </summary>
        /// <param name="path">The path of the source files</param>
        /// <returns>A list of all deserialized page objects</returns>
        List<Page> SelectPages(string path);

        /// <summary>
        /// Get all posts by deserializing source files
        /// </summary>
        /// <param name="path">The path of the source files</param>
        /// <param name="sourceDateFormat">The date format to use when deserializing posts</param>
        /// <returns>A list of all deserialized post objects</returns>
        List<Post> SelectPosts(string path, string sourceDateFormat);
    }
}