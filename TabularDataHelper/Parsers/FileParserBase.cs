using System;
using System.Data;

namespace TabularDataHelper.Parsers
{
    public abstract class FileParserBase : IFileParser
    {
        protected FileParserBase()
        {
            FileType = FileType.None;
        }

        public FileType FileType { get; protected set; }

        public abstract void Parse(string input);

        public virtual DataTable DataTable { get; protected set; } = new DataTable();
    }
}