﻿namespace ConsoleMio.Xml
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using Enumerations;

    public class TraverseUsingXDocument : DirectoryTraverse
    {
        private XElement currentParent;

        public TraverseUsingXDocument(XElement element)
        {
            this.currentParent = element;
        }

        protected override void CloseCurrentDirectory(string directoryName)
        {
            this.currentParent = this.currentParent.Parent;
        }

        protected override void LogPathToLoog(string fullName, PathTooLongException ex)
        {
            this.currentParent.Add(new XElement(
                "pathTooLong",
                new XAttribute("message", ex.Message)));
        }

        protected override void LogUnauthorizedAccess(string fullName, UnauthorizedAccessException ex)
        {
            this.currentParent.Add(new XElement(
                "accessDenied",
                new XAttribute("message", ex.Message)));
        }

        protected override void WriteDirectoryToDocument(string directoryName, string directoryPath)
        {
            var element = new XElement(
                "dir",
                new XAttribute("name", directoryName),
                new XAttribute("path", directoryPath));

            this.currentParent.Add(element);
            this.currentParent = element;
        }

        protected override void WriteFileInformation(
            Dictionary<string, List<FileInfo>> directoryFiles)
        {
            foreach (var key in directoryFiles.Keys)
            {
                this.currentParent.Add(new XElement(
                    "extension",
                    new XAttribute("type", $".{key}"),
                    from info in directoryFiles[key]
                    select new XElement(
                        "file",
                        new XAttribute("name", this.FileHelper.GetFileName(info.Name)),
                        new XAttribute(
                            "size",
                            string.Format(
                                "{0:F3} kb", this.FileHelper.ConvertFileLength(
                                  info.Length, FileLength.Kbyte))
              ))));
            }
        }
    }
}
