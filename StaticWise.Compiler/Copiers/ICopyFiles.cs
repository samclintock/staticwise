namespace StaticWise.Compiler.Copiers
{
    internal interface ICopyFiles
    {
        /// <summary>
        /// Copy the favicon to the output directory
        /// </summary>
        void CopyFaviconFile();

        /// <summary>
        /// Copy and merge the media directory to the output directory
        /// </summary>
        void CopyMediaDirectory();
    }
}