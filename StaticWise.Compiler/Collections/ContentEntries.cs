using StaticWise.Common.Queries;
using StaticWise.Entities;
using System.Collections.Generic;
using System.Linq;

namespace StaticWise.Compiler.Collections
{
    internal class ContentEntries : IContentEntries
    {
        #region Properties

        IQueryManager _queryManager;
        Config _config;

        #endregion

        #region Constructors

        public ContentEntries(IQueryManager queryManager, Config config)
        {
            _queryManager = queryManager;
            _config = config;
        }

        #endregion

        #region Methods

        List<Post> IContentEntries.GetPublishablePosts()
        {
            if (_queryManager == null)
                return null;

            return _queryManager.SelectPosts(
                _config.PostsDirIncRoot,
                _config.Code.MarkdownDateFormat)
                .Where(x => x.IsDraft == false)
                .OrderByDescending(x => x.Datetime)
                .ToList();
        }

        List<Page> IContentEntries.GetPublishablePages()
        {
            if (_queryManager == null)
                return null;

            return _queryManager.SelectPages(
                _config.PagesDirIncRoot)
                .Where(x => x.IsDraft == false)
                .ToList();
        }

        List<string> IContentEntries.CombineContentEntries(IEnumerable<Page> pages, IEnumerable<Post> posts)
        {
            if (pages == null || posts == null)
                return null;

            return pages.Select(x => $"{x.FriendlyUrl}.html")
                .Union(posts.Select(x => $"{x.FriendlyUrl}.html")).ToList();
        }

        #endregion
    }
}
