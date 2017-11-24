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
            int current,
            int total,
            int paginationCount,
            string containerOpenHtml,
            string containerCloseHtml,
            string itemOpenHtml,
            string itemCloseHtml,
            string firstFileName,
            string archiveFileName,
            string archiveDirectoryName)
        {
            if (total <= 1 || total <= paginationCount)
                return string.Empty;

            StringBuilder b = new StringBuilder();
            b.Append(containerOpenHtml);

            if (!string.IsNullOrEmpty(archiveDirectoryName))
                archiveDirectoryName = archiveDirectoryName + "/";

            if (current == 2)
                b.Append($"{itemOpenHtml}<a href=\"/{firstFileName}.html\">Previous</a>{itemCloseHtml}");
            else if (current > 2)
                b.Append($"{itemOpenHtml}<a href=\"/{archiveDirectoryName + archiveFileName + (current - 1)}.html\">Previous</a>{itemCloseHtml}");

            int start = 0, end = total;

            if (total > PAGES_TO_DISPLAY)
            {
                start = current - 5;
                end = current + 5;

                if (start < 0)
                {
                    start = 0;
                    end = start + 10;
                }

                if (end > total)
                {
                    end = total;
                    start = total - 10;
                }
            }

            for (int i = start; i < end; i++)
            {
                if ((i + 1) == current)
                    b.Append($"{itemOpenHtml}{(i+1)}{itemCloseHtml}");
                else
                    b.Append($"{itemOpenHtml}<a href=\"/{(((i+1) == 1) ? firstFileName : archiveDirectoryName + archiveFileName + (i+1))}.html\">{(i+1)}</a>{itemCloseHtml}");
            }

            if (total > current)
                b.Append($"{itemOpenHtml}<a href=\"/{archiveDirectoryName + archiveFileName + (current + 1)}.html\">Next</a>{itemCloseHtml}");

            b.Append(containerCloseHtml);
            return b.ToString();
        }

        #endregion
    }
}
