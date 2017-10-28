using System.Collections.Generic;

namespace StaticWise.Compiler.Components.Sitemap
{
    public interface ISitemap
    {
        /// <summary>
        /// Generate XML for a valid sitemap
        /// </summary>
        /// <param name="entries">A list of page or post entries</param>
        /// <param name="domainName">The domain name to use to create an absolute link</param>
        /// <returns>XML to create a valid sitemap</returns>
        string Generate(List<string> entries, string domainName);
    }
}