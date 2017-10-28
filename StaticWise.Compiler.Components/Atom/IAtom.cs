using StaticWise.Entities;
using System.Collections.Generic;

namespace StaticWise.Compiler.Components.Atom
{
    public interface IAtom
    {
        /// <summary>
        /// Generate XML for a valid atom feed
        /// </summary>
        /// <param name="posts">A list of latest posts for the atom feed</param>
        /// <param name="domainName">The domain name to use for absolute links</param>
        /// <param name="title">The title to use for thr atom feed</param>
        /// <param name="description">The description to use for atom feed</param>
        /// <returns>XML to create a valid atom feed</returns>
        string Generate(List<Post> posts, string domainName, string title, string description);
    }
}