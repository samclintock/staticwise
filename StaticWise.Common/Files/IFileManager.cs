namespace StaticWise.Common.Files
{
    public interface IFileManager
    {
        /// <summary>
        /// Copy a source directory and it's contents to a destination directory
        /// </summary>
        /// <param name="sourceDirPath"></param>
        /// <param name="destinationDirPath"></param>
        /// <param name="destinationDirPath"></param>
        /// <returns>A boolean value representing if the copy was successful</returns>
        bool CopyDirectory(string sourceDirPath, string destinationDirPath, bool isRecursive = true);

        /// <summary>
        /// Check if a file exists at a specified location
        /// </summary>
        /// <param name="filePath">The file to check (e.g. "C:\README.md")</param>
        /// <returns>A boolean representing if the file exists or not</returns>
        bool IsExistingFile(string filePath);

        /// <summary>
        /// Check if a directory exists at a specified location
        /// </summary>
        /// <param name="directoryPath">The directory to check (e.g. "C:\Documents")</param>
        /// <returns>A boolean representing if the directory exists or not</returns>
        bool IsExistingDirectory(string directoryPath);

        /// <summary>
        /// Open a file and retrieve the text within
        /// </summary>
        /// <param name="filePath">The file to retrieve the text from (e.g. "C:\README.md")</param>
        /// <returns>A string that contains the text in the file</returns>
        string GetTextFromFile(string filePath);

        /// <summary>
        /// Save a string of text to a specified file location
        /// </summary>
        /// <param name="filePath">The file location of where to save the text</param>
        /// <param name="text">The text to save to the new or existing file</param>
        /// <returns>A boolean representing if the save was successful or not</returns>
        bool SaveTextToFile(string filePath, string text);

        /// <summary>
        /// Delete a file at a specified location
        /// </summary>
        /// <param name="filePath">The file to delete (e.g "C:\README.md")</param>
        /// <returns>A boolean representing if the file has been deleted or not</returns>
        bool DeleteFile(string filePath);

        /// <summary>
        /// Delete a directory at a specified location
        /// </summary>
        /// <param name="directoryPath">The directory to delete (e.g "C:\Documents")</param>
        /// <returns>A boolean representing if the directory has been deleted or not</returns>
        bool DeleteDirectory(string directoryPath);

        /// <summary>
        /// Create a new directory at a specified location
        /// </summary>
        /// <param name="directoryPath">The full path to the location of the new directory (including directory name)</param>
        /// <returns>A boolean representing if the directory has been created or not</returns>
        bool CreateDirectory(string directoryPath);

        /// <summary>
        /// Copy a file from a source file path to a destination file path
        /// </summary>
        /// <param name="sourceFilePath">The source file to copy ("C:\README.md")</param>
        /// <param name="destinationFilePath">The destination for the new/existing file ("C:\README.md")</param>
        /// <param name="isOverwriteEnabled">If a file already exists at the desired destination, should it be overwritten or not</param>
        /// <returns>A boolean representing if the file was successfully copied or not</returns>
        bool CopyFile(string sourceFilePath, string destinationFilePath, bool isOverwriteEnabled = true);

        /// <summary>
        /// Remove unnecessary elements from a string of HTML. This method will
        /// safely compress a HTML string to help compress the size of a HTML document
        /// </summary>
        /// <param name="html">The string of HTML to compress</param>
        /// <returns>A compressed string of HTML</returns>
        string CompressHtml(string html);
    }
}
