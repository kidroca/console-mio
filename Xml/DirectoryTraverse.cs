namespace HomeworkHelpers.Xml
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Implemented Template Method Pattern :)
    /// </summary>
    public abstract class DirectoryTraverse
    {
        /// <summary>
        /// Creates an instance with the default implementation
        /// </summary>
        protected DirectoryTraverse()
            : this(new StreamHomeworkHelper())
        {
        }

        /// <summary>
        /// Creates an instance with the given fileHelper
        /// </summary>
        /// <param name="fileHelper">A streams helper for operations with files and directories</param>
        protected DirectoryTraverse(StreamHomeworkHelper fileHelper)
        {
            if (fileHelper == null)
            {
                throw new ArgumentNullException(nameof(fileHelper));
            }

            this.FileHelper = fileHelper;
        }

        /// <summary>
        /// Readonly reference to the file helper
        /// </summary>
        protected StreamHomeworkHelper FileHelper { get; }

        public void TraverseFolder(DirectoryInfo dirInfo)
        {
            string directoryName = dirInfo.Name,
                directoryPath = dirInfo.FullName;

            Dictionary<string, List<FileInfo>> directoryFiles = this.FileHelper.GetDirectoryFiles(dirInfo);

            this.WriteDirectoryToDocument(directoryName, directoryPath);
            this.WriteFileInformation(directoryFiles);

            foreach (var dir in dirInfo.GetDirectories())
            {
                try
                {
                    this.TraverseFolder(dir);
                }
                catch (UnauthorizedAccessException ex)
                {
                    this.LogUnauthorizedAccess(dir.FullName, ex);
                }
                catch (PathTooLongException ex)
                {
                    this.LogPathToLoog(dir.FullName, ex);
                }
            }

            this.CloseCurrentDirectory(directoryName);
        }

        protected abstract void WriteDirectoryToDocument(string directoryName, string directoryPath);

        protected abstract void WriteFileInformation(Dictionary<string, List<FileInfo>> directoryFiles);

        protected abstract void CloseCurrentDirectory(string directoryName);

        protected abstract void LogPathToLoog(string fullName, PathTooLongException ex);

        protected abstract void LogUnauthorizedAccess(string fullName, UnauthorizedAccessException ex);
    }
}
