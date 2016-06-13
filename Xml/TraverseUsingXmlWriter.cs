namespace ConsoleMio.Xml
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;
    using Enumerations;

    public class TraverseUsingXmlWriter : DirectoryTraverse
    {
        private XmlWriter xmlWriter;

        public TraverseUsingXmlWriter(XmlWriter writer)
        {
            this.xmlWriter = writer;
        }

        protected override void CloseCurrentDirectory(string directoryName)
        {
            this.xmlWriter.WriteEndElement();
        }

        protected override void LogPathToLoog(string fullName, PathTooLongException ex)
        {
            this.xmlWriter.WriteStartElement("pathTooLong");
            this.xmlWriter.WriteAttributeString("path", fullName);
            this.xmlWriter.WriteAttributeString("message", ex.Message);
            this.xmlWriter.WriteEndElement();
        }

        protected override void LogUnauthorizedAccess(string fullName, UnauthorizedAccessException ex)
        {
            this.xmlWriter.WriteStartElement("accessDenied");
            this.xmlWriter.WriteAttributeString("path", fullName);
            this.xmlWriter.WriteAttributeString("message", ex.Message);
            this.xmlWriter.WriteEndElement();
        }

        protected override void WriteDirectoryToDocument(string directoryName, string directoryPath)
        {
            this.xmlWriter.WriteStartElement("dir");
            this.xmlWriter.WriteAttributeString("name", directoryName);
            this.xmlWriter.WriteAttributeString("path", directoryPath);
        }

        protected override void WriteFileInformation(Dictionary<string, List<FileInfo>> directoryFiles)
        {
            foreach (var key in directoryFiles.Keys)
            {
                this.xmlWriter.WriteStartElement("extension");
                this.xmlWriter.WriteAttributeString("type", $".{key}");

                foreach (var info in directoryFiles[key])
                {
                    this.xmlWriter.WriteStartElement("file");
                    this.xmlWriter.WriteAttributeString("name", this.FileHelper.GetFileName(info.Name));
                    this.xmlWriter.WriteAttributeString(
                        "size",
                        $"{this.FileHelper.ConvertFileLength(info.Length, FileLength.Kbyte):F3} kb");
                    this.xmlWriter.WriteEndElement();
                }

                this.xmlWriter.WriteEndElement();
            }
        }
    }
}
