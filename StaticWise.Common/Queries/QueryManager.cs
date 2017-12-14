using StaticWise.Entities;
using System.Collections.Generic;
using System.IO;
using StaticWise.Common.Deserialize;
using System.Linq;
using StaticWise.Common.Files;

namespace StaticWise.Common.Queries
{
    public class QueryManager : IQueryManager
    {
        #region Constants

        private const string MARKDOWN_EXT = "md";

        #endregion

        #region Properties

        IDeserializeManager _deserializeManager;
        IFileManager _fileManager;

        #endregion

        #region Constructors

        public QueryManager(IDeserializeManager deserializeManager, IFileManager fileManager)
        {
            _deserializeManager = deserializeManager;
            _fileManager = fileManager;
        }

        #endregion

        #region Methods

        List<Page> IQueryManager.SelectPages(string path, int offset, int pageSize)
        {
            List<Page> result = new List<Page>();

            if (!string.IsNullOrEmpty(path) && _fileManager.IsExistingDirectory(path) && pageSize > 0)
            {
                try
                {
                    IEnumerable<string> files = Directory.GetFiles(path, $"*{MARKDOWN_EXT}", 
                        SearchOption.TopDirectoryOnly)
                        .OrderByDescending(d => new FileInfo(d).CreationTime)
                        .Skip(offset).Take(pageSize);

                    foreach (string file in files)
                        result.Add(_deserializeManager.DeserializePage(file));
                }
                catch
                {
                    return result;
                }
            }

            return result;
        }

        List<Post> IQueryManager.SelectPosts(string path, int offset, int pageSize, bool incDraft, string sourceDateFormat)
        {
            List<Post> result = new List<Post>();

            if (!string.IsNullOrEmpty(path) && _fileManager.IsExistingDirectory(path) && pageSize > 0)
            {
                try
                {
                    IEnumerable<string> files = Directory.GetFiles(path, $"*{MARKDOWN_EXT}",
                        SearchOption.TopDirectoryOnly)
                        .OrderByDescending(d => new FileInfo(d).CreationTime)
                        .Skip(offset).Take(pageSize);

                    if (incDraft)
                        foreach (string file in files)
                            result.Add(_deserializeManager.DeserializePost(file, sourceDateFormat));
                    else
                        foreach (string file in files)
                        {
                            Post post = _deserializeManager.DeserializePost(file, sourceDateFormat);
                            if (!post.IsDraft) result.Add(post);
                        }
                }
                catch
                {
                    return result;
                }
            }

            return result;
        }

        int IQueryManager.TotalPosts(string path, bool incDraft, string sourceDateFormat)
        {
            int result = 0;

            if (!string.IsNullOrEmpty(path) && _fileManager.IsExistingDirectory(path))
            {
                try
                {
                    IEnumerable<string> files = Directory.GetFiles(path, $"*{MARKDOWN_EXT}",
                        SearchOption.TopDirectoryOnly);

                    if (incDraft)
                        result = files.Count();
                    else
                        foreach (string file in files)
                        {
                            Post post = _deserializeManager.DeserializePost(file, sourceDateFormat);
                            if (!post.IsDraft) result++;
                        }
                }
                catch
                {
                    return result;
                }
            }

            return result;
        }

        #endregion
    }
}