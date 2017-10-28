using StaticWise.Common.Urls;
using StaticWise.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace StaticWise.Compiler.Components.Atom
{
    public class StandardAtom : IAtom
    {
        #region Constants

        private const string RFC3339_DATE_FORMAT = "yyyy-MM-dd'T'HH:mm:ssZ";

        #endregion

        #region Properties

        IUrlManager _urlManager;

        #endregion

        #region Constructors

        public StandardAtom(IUrlManager urlManager)
        {
            _urlManager = urlManager;
        }

        #endregion

        #region Methods

        public string Generate(List<Post> posts, string domainName, string title, string description)
        {
            if (posts == null || string.IsNullOrEmpty(domainName) ||
                string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description))
                return string.Empty;

            StringBuilder b = new StringBuilder();
            b.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            b.Append("<feed xmlns=\"http://www.w3.org/2005/Atom\">");
            b.Append($"<id>{domainName}</id><title>{WebUtility.HtmlEncode(title)}</title><subtitle>{WebUtility.HtmlEncode(description)}</subtitle><link rel=\"alternate\" type=\"text/html\" href=\"{domainName}\" /><updated>{DateTime.UtcNow.ToString(RFC3339_DATE_FORMAT)}</updated>");

            if (posts.Any())
            {
                foreach (Post post in posts)
                {
                    string link = _urlManager.CreateUrl(domainName, $"{post.FriendlyUrl}.html");
                    b.Append($"<entry><id>{link}</id><title>{WebUtility.HtmlEncode(post.Title)}</title><author><name>{WebUtility.HtmlEncode(title)}</name></author><summary>{WebUtility.HtmlEncode(post.Description)}</summary><link rel=\"alternate\" type=\"text/html\" href=\"{link}\" /><updated>{post.Datetime.ToUniversalTime().ToString(RFC3339_DATE_FORMAT)}</updated></entry>");
                }
            }

            b.Append("</feed>");

            return b.ToString();
        }

        #endregion
    }
}
