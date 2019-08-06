using System;
using System.Data;

namespace TabularDataHelper.Parsers
{
    public interface IFileParser
    {
        DataTable DataTable { get; }
        void Parse(string input);
    }
}