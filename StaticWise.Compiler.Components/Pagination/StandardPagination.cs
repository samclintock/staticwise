using System;
using System.Text;

namespace StaticWise.Compiler.Components.Pagination
{
    public class StandardPagination : IPagination
    {
        #region Constants

        const int PAGES_TO_DISPLAY = 10;

        #endregion

        #region Methods

        string IPagination.Generate(
            int currentPage,
            int totalPosts,
            int paginationCount,
            string containerOpenHtml,
            string containerCloseHtml,
            string itemOpenHtml,
            string itemCloseHtml,
            string firstFileName,
            string archiveFileName,
            string archiveDirectoryName)
        {
            if (totalPosts <= 1 || totalPosts <= paginationCount)
                return string.Empty;

            StringBuilder b = new StringBuilder();
            b.Append(containerOpenHtml);

            if (!string.IsNullOrEmpty(archiveDirectoryName))
                archiveDirectoryName = archiveDirectoryName + "/";

            int totalPages = (int)Math.Ceiling(totalPosts / (decimal)paginationCount);
            int start = currentPage, end = (totalPages == 0) ? 1 : totalPages;

            if (currentPage == 2)
            {
                b.Append($"{itemOpenHtml}<a href=\"/{firstFileName}.html\">Previous</a>{itemCloseHtml}");
                start = start - 1;
            }
            else if (currentPage > 2)
            {
                b.Append($"{itemOpenHtml}<a href=\"/{archiveDirectoryName + archiveFileName + (currentPage - 1)}.html\">Previous</a>{itemCloseHtml}");
                start = start - 2;
            }

            int pageCount = 0;
            for (int i = start; i <= end; i++)
            {
                if (i == currentPage)
                    b.Append($"{itemOpenHtml}{(i)}{itemCloseHtml}");
                else
                    b.Append($"{itemOpenHtml}<a href=\"/{((i == 1) ? firstFileName : archiveDirectoryName + archiveFileName + i)}.html\">{i}</a>{itemCloseHtml}");

                if (pageCount == 9) break;
                pageCount++;
            }

            if (currentPage < totalPages)
                b.Append($"{itemOpenHtml}<a href=\"/{archiveDirectoryName + archiveFileName + (currentPage + 1)}.html\">Next</a>{itemCloseHtml}");

            b.Append(containerCloseHtml);
            return b.ToString();
        }

        #endregion
    }
}
