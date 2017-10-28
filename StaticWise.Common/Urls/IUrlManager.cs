namespace StaticWise.Common.Urls
{
    public interface IUrlManager
    {
        /// <summary>
        /// Create a URL safely, regardless of slashes in the domain name or file
        /// </summary>
        /// <param name="domainname">The domain name (e.g. "http://www.google.com/" or "http://www.google.com")</param>
        /// <param name="filePath">The filename and path(e.g. "/index.html", "index.html", "/pages/index.html" or "pages/index.html")</param>
        /// <returns>A correctly formatted URL (e.g. "http://www.google.com/index.html")</returns>
        string CreateUrl(string domainname, string file);
    }
}
