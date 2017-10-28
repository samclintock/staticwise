namespace StaticWise.Compiler.Components.Pagination
{
    public class StandardPagination : IPagination
    {
        string IPagination.Generate(
            int currentPage, 
            int totalPosts, 
            string firstFileName, 
            string archiveFileName, 
            string archiveDirectoryName)
        {
            return "";
        }
    }
}
