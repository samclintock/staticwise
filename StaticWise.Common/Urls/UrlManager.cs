namespace StaticWise.Common.Urls
{
    public class UrlManager : IUrlManager
    {
        #region Methods

        string IUrlManager.CreateUrl(string domainname, string filePath)
        {
            if (string.IsNullOrEmpty(domainname) ||
                string.IsNullOrEmpty(filePath))
                return string.Empty;

            return $"{domainname.TrimEnd('/')}/{filePath.TrimStart('/')}";
        }

        #endregion
    }
}
