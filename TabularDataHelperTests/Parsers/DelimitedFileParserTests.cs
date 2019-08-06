using System;
using System.IO;
using NUnit.Framework;
using TabularDataHelper.Parsers;

namespace TabularDataHelperTests.Parsers
{
    [TestFixture]
    public static class FileParserBaseTests
    {
        [TestCase("Tab-WithHeader.txt", true, "\t", 2)]
        [TestCase("Tab-WithoutHeader.txt", false, "\t", 2)]
        public static void Constructor_Test(string path, bool hasHeader, string delimiter, int expectedRows)
        {
            var parser = new DelimitedFileParser(hasHeader, delimiter);

            var content = File.ReadAllText(
                Path.Combine(
                    TestContext.CurrentContext.TestDirectory,
                    "TestFiles",
                    path));

            //System.IO.Path.GetDirectoryName(
            //    //Assembly.GetExecutingAssembly().Location
            //), 


            parser.Parse(content);

            Assert.That(parser.DataTable.Rows.Count, Is.EqualTo(expectedRows));

            Assert.That(parser.DataTable.Columns[0].ColumnName,
                hasHeader
                    ? Is.EqualTo("Name")
                    : Is.EqualTo("Column0001"));
        }
    }
}