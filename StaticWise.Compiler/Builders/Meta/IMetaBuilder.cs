namespace StaticWise.Compiler.Builders.Meta
{
    internal interface IMetaBuilder
    {
        /// <summary>
        /// Create a robots file ("robots.txt") and save it to the output directory
        /// </summary>
        void BuildRobots();
    }
}
