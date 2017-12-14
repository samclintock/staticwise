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
        /// <param name="offset">The offset to use for pagination</param>
        /// <param name="pageSize">The page size to use for pagination</param>
        /// <returns>A list of all deserialized page objects</returns>
        List<Page> SelectPages(string path, int offset, int pageSize);

        /// <summary>
        /// Get all posts by deserializing source files
        /// </summary>
        /// <param name="path">The path of the source files</param>
        /// <param name="offset">The offset to use for pagination</param>
        /// <param name="pageSize">The page size to use for pagination</param>
        /// <param name="incDraft">If posts marked as draft should be included in the result</param>
        /// <param name="sourceDateFormat">The date format to use when deserializing posts</param>
        /// <returns>A list of all deserialized post objects</returns>
        List<Post> SelectPosts(string path, int offset, int pageSize, bool incDraft, string sourceDateFormat);

        /// <summary>
        /// Get the total number of posts
        /// </summary>
        /// <param name="path">The path of the source files</param>
        /// <param name="incDraft">If posts marked as draft should be included in the result</param>
        /// <param name="sourceDateFormat">The date format to use when deserializing posts</param>
        /// <returns>An integer value to indicate the total number of posts</returns>
        int TotalPosts(string path, bool incDraft, string sourceDateFormat);
    }
}