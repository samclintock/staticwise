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

            if (currentPage == 2)
                b.Append($"{itemOpenHtml}<a href=\"/{firstFileName}.html\">Previous</a>{itemCloseHtml}");
            else if (currentPage > 2)
                b.Append($"{itemOpenHtml}<a href=\"/{archiveDirectoryName + archiveFileName + (currentPage - 1)}.html\">Previous</a>{itemCloseHtml}");

            int start = 0, end = totalPosts;

            if (totalPosts > PAGES_TO_DISPLAY)
            {
                start = currentPage - 5;
                end = currentPage + 5;

                if (start < 0)
                {
                    start = 0;
                    end = start + 10;
                }

                if (end > totalPosts)
                {
                    end = totalPosts;
                    start = totalPosts - 10;
                }
            }

            for (int i = start; i < end; i++)
            {
                if ((i + 1) == currentPage)
                    b.Append($"{itemOpenHtml}{(i+1)}{itemCloseHtml}");
                else
                    b.Append($"{itemOpenHtml}<a href=\"/{(((i+1) == 1) ? firstFileName : archiveDirectoryName + archiveFileName + (i+1))}.html\">{(i+1)}</a>{itemCloseHtml}");
            }

            if (totalPosts > currentPage)
                b.Append($"{itemOpenHtml}<a href=\"/{archiveDirectoryName + archiveFileName + (currentPage + 1)}.html\">Next</a>{itemCloseHtml}");

            b.Append(containerCloseHtml);
            return b.ToString();
        }

        #endregion
    }
}
