using StaticWise.Common.Urls;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StaticWise.Compiler.Components.Sitemap
{
    public class StandardSitemap : ISitemap
    {
        #region Properties

        IUrlManager _urlManager;

        #endregion

        #region Constructors

        public StandardSitemap(IUrlManager urlManager)
        {
            _urlManager = urlManager;
        }

        #endregion

        #region Methods

        public string Generate(List<string> entries, string domainName)
        {
            if (entries == null || string.IsNullOrEmpty(domainName))
                return string.Empty;

            StringBuilder b = new StringBuilder();
            b.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            b.Append("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");

            if (entries.Any())
            {
                for (int i = 0; i < entries.Count(); i++)
                {
                    b.Append($"<url><loc>{_urlManager.CreateUrl(domainName, entries[i])}</loc></url>");
                }
            }

            b.Append("</urlset>");

            return b.ToString();
        }

        #endregion
    }
}
