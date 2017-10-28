using StaticWise.Entities;

namespace StaticWise.Compiler.Components.IndividualPost
{
    public interface IIndividualPost
    {
        /// <summary>
        /// Generate HTML to view an individual post
        /// </summary>
        /// <param name="post">The post object</param>
        /// <param name="config">The config object</param>
        /// <returns>A string of HTML</returns>
        string Generate(
            Post post,
            Config config);
    }
}
