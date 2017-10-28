using StaticWise.Entities;

namespace StaticWise.Compiler.Components.IndividualPage
{
    public interface IIndividualPage
    {
        /// <summary>
        /// Generate HTML to view an individual page
        /// </summary>
        /// <param name="page">The page object</param>
        /// <param name="config">The config object</param>
        /// <returns>A string of HTML</returns>
        string Generate(
            Page page,
            Config config);
    }
}
