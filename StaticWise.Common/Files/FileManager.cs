using System;
using System.IO;
using System.Linq;
using System.Text;

namespace StaticWise.Common.Files
{
    public class FileManager : IFileManager
    {
        #region Methods

        bool IFileManager.CopyDirectory(string sourceDirPath, string destinationDirPath, bool isRecursive)
        {
            if (string.IsNullOrEmpty(sourceDirPath) || string.IsNullOrEmpty(destinationDirPath))
                return false;

            try
            {
                // Only attempt to copy the source directory if it exists
                if (!Directory.Exists(sourceDirPath))
                    return false;

                // Create the destination directory if it doesn't exist
                if (!Directory.Exists(destinationDirPath))
                    Directory.CreateDirectory(destinationDirPath);

                // Get the source directory and subdirectories
                DirectoryInfo sourceDir = new DirectoryInfo(sourceDirPath);

                // Get the files in the source directory and copy them to the new location
                FileInfo[] files = sourceDir.GetFiles();
                if (files.Any())
                {
                    foreach (FileInfo file in files)
                    {
                        FileInfo destFile = new FileInfo(Path.Combine(
                            destinationDirPath, file.Name));

                        // Only overwrite files if they are newer (to improve performance)
                        if (file.LastWriteTime > destFile.LastWriteTime || !destFile.Exists)
                            file.CopyTo(destFile.FullName, true);
                    }
                }

                // If copying subdirectories, copy them and their contents to new location
                if (isRecursive)
                {
                    DirectoryInfo[] sourceSubDir = sourceDir.GetDirectories();
                    if (sourceSubDir.Any())
                    {
                        foreach (DirectoryInfo subdir in sourceSubDir)
                        {
                            string temppath = Path.Combine(destinationDirPath, subdir.Name);
                            ((IFileManager) this).CopyDirectory(subdir.FullName, temppath, isRecursive);
                        }
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        bool IFileManager.IsExistingFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return false;

            try
            {
                return File.Exists(filePath);
            }
            catch
            {
                return false;
            }
        }

        bool IFileManager.IsExistingDirectory(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath))
                return false;

            try
            {
                return Directory.Exists(directoryPath);
            }
            catch
            {
                return false;
            }
        }

        string IFileManager.GetTextFromFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return string.Empty;

            if (!((IFileManager) this).IsExistingFile(filePath))
                return string.Empty;

            try
            {
                return File.ReadAllText(filePath, Encoding.UTF8);
            }
            catch
            {
                return string.Empty;
            }
        }

        bool IFileManager.SaveTextToFile(string filePath, string text)
        {
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(text))
                return false;

            try
            {
                File.WriteAllText(filePath, text, Encoding.UTF8);

                return true;
            }
            catch
            {
                return false;
            }
        }

        bool IFileManager.DeleteFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return false;

            try
            {
                File.Delete(filePath);

                return true;
            }
            catch
            {
                return false;
            }
        }

        bool IFileManager.DeleteDirectory(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath))
                return false;

            try
            {
                string[] files = Directory.GetFiles(directoryPath);
                string[] directories = Directory.GetDirectories(directoryPath);

                if (files.Any())
                    foreach (string file in files)
                    {
                        File.SetAttributes(file, FileAttributes.Normal);
                        File.Delete(file);
                    }

                if (directories.Any())
                    foreach (string directory in directories)
                    {
                        ((IFileManager) this).DeleteDirectory(directory);
                    }

                Directory.Delete(directoryPath, false);

                return true;
            }
            catch
            {
                return false;
            }
        }

        bool IFileManager.CreateDirectory(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath))
                return false;

            try
            {
                Directory.CreateDirectory(directoryPath);

                return true;
            }
            catch
            {
                return false;
            }
        }

        bool IFileManager.CopyFile(string sourceFilePath, string destinationFilePath, bool isOverwriteEnabled)
        {
            if (string.IsNullOrEmpty(sourceFilePath) || string.IsNullOrEmpty(destinationFilePath))
                return false;

            try
            {
                File.Copy(sourceFilePath, destinationFilePath, isOverwriteEnabled);

                return true;
            }
            catch
            {
                return false;
            }
        }

        string IFileManager.CompressHtml(string html)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;

            html = html.Replace(Environment.NewLine, string.Empty);

            // Commented out due to bug in CSS presentation
            //text = text.Replace("\t", string.Empty);

            return html;
        }

        #endregion
    }
}
