using name_date_finder;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace name_date_finder.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        [TestMethod()]
        public void GetDateTimeFromArgsOkTest()
        {
            string[] args = { "2019-10-11", };
            string dateSyntax = "yyyy-MM-dd";

            DateTime date = GetDateTimeFromArgs(args, dateSyntax);
            Assert.Fail();
        }

        [TestMethod()]
        public void GetDateTimeFromArgsWrongSyntaxTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetDateTimeFromArgsEmptryArgsTest()
        {
            Assert.Fail();
        }
    }
}

namespace name_date_finder
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetDateTimeFromArgsOkTest()
        {
            string[] args = { "2019-10-11",  };
            string dateSyntax = "yyyy-MM-dd";

            DateTime date = GetDateTimeFromArgs(args, dateSyntax);

        }
    }
}
