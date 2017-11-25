using StaticWise.Entities;
using System.Collections.Generic;
using System.IO;
using StaticWise.Common.Deserialize;

namespace StaticWise.Common.Queries
{
    public class QueryManager : IQueryManager
    {
        #region Constants

        private const string MARKDOWN_EXT = "md";

        #endregion

        #region Properties

        IDeserializeManager _deserializeManager;

        #endregion

        #region Constructors

        public QueryManager(IDeserializeManager deserializeManager)
        {
            _deserializeManager = deserializeManager;
        }

        #endregion

        #region Methods

        List<Post> IQueryManager.SelectPosts(string path, string sourceDateFormat)
        {
            List<Post> result = new List<Post>();
            
            if (!string.IsNullOrEmpty(path))
            {
                if (Directory.Exists(path))
                {
                    string[] posts = Directory.GetFiles(path, $"*{MARKDOWN_EXT}");
                    foreach (string p in posts)
                    {
                        result.Add(_deserializeManager.DeserializePost(
                            p, sourceDateFormat));
                    };
                }
            }

            return result;
        }

        List<Page> IQueryManager.SelectPages(string path)
        {
            List<Page> result = new List<Page>();

            if (!string.IsNullOrEmpty(path))
            {
                if (Directory.Exists(path))
                {
                    string[] pages = Directory.GetFiles(path, $"*{MARKDOWN_EXT}");
                    foreach (string p in pages)
                    {
                        result.Add(_deserializeManager.DeserializePage(p));
                    };
                }
            }

            return result;
        }

        #endregion
    }
}