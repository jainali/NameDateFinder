using Microsoft.VisualStudio.TestTools.UnitTesting;
using name_date_finder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace name_date_finder.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void GetDateTimeFromArgsOkTest()
        {
            string[] args = { "2019-10-11", };
            string dateSyntax = "yyyy-MM-dd";

            DateTime expectedDate = new DateTime(2019, 10, 11);

            DateTime date = Program.GetDateTimeFromArgs(args, dateSyntax);

            Assert.AreEqual(expectedDate, date);
        }

        [TestMethod()]
        public void GetDateTimeFromArgsEmptyArgsTest()
        {
            string[] args = { };
            string dateSyntax = "yyyy-MM-dd";

            Exception expected = new Exception("No input date argument added to commandline command");
            try
            {
                DateTime date = Program.GetDateTimeFromArgs(args, dateSyntax);
                Assert.Fail("An exception should have been thrown");
            }
            catch (Exception e)
            {
                Assert.AreEqual("No input date argument added to commandline command", e.Message);
            }
        }

        [TestMethod()]
        public void GetDateTimeFromArgsWrongDateSyntaxTest()
        {
            string[] args = { "2019-10-11" };
            string dateSyntax = "yyyy-MM-dd HH";

            Exception expected = new Exception("No input date argument added to commandline command");
            try
            {
                DateTime date = Program.GetDateTimeFromArgs(args, dateSyntax);
                Assert.Fail("An exception should have been thrown");
            }
            catch (Exception e)
            {
                Assert.AreEqual($"Inserted syntax for Datetime: 2019-10-11 is not correct. Correct format is : yyyy-MM-dd HH", e.Message);
            }
        }

        [TestMethod()]
        public void LoadCSVOkTest()
        {
            string csvFileDirectory = new FileInfo(@"..\..\SupportFiles\OkNimet.csv").DirectoryName;
            string csvFilePath = csvFileDirectory + @"\OkNimet.csv";
            List<NameDate> expectedValues = new List<NameDate>();
            NameDate nameDate1 = new NameDate(new string[] { "2.9", "Sinikka, Sini, Justus" }, 0);
            NameDate nameDate2 = new NameDate(new string[] { "3.9", "Soile, Soili, Soila" }, 1);
            NameDate nameDate3 = new NameDate(new string[] { "30.11", "Antti" }, 2);
            NameDate nameDate4 = new NameDate(new string[] { "11.10", "Ohto" }, 3);

            expectedValues.Add(nameDate1);
            expectedValues.Add(nameDate2);
            expectedValues.Add(nameDate3);
            expectedValues.Add(nameDate4);

            List<NameDate> nameDates = Program.LoadCSV(csvFilePath);

            bool testOk = true;

            testOk = expectedValues.Capacity == nameDates.Capacity;

            for (int i = 0; i < expectedValues.Capacity; i++)
            {
                try
                {
                    if (expectedValues[i].Equals(nameDates[i]))
                    {
                        testOk = false;
                    }
                }
                catch
                {
                    testOk = false;

                }
            }

            Assert.IsTrue(testOk);
        }

        [TestMethod()]
        public void LoadCSVNotFoundTest()
        {
            string csvFileDirectory = new FileInfo(@"..\..\SupportFiles\OkNimet.csv").DirectoryName;
            string csvFilePath = csvFileDirectory + @"\NotFound.csv";

            try
            {
                List<NameDate> nameDates = Program.LoadCSV(csvFilePath);
                Assert.Fail("An exception should have been thrown");
            }
            catch (System.IO.FileNotFoundException e)
            {
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail("Wrong erro message is thrown:", e.Message);
            }
        }

        [TestMethod()]
        public void LoadCSVTooManyColumnsTest()
        {
            string csvFileDirectory = new FileInfo(@"..\..\SupportFiles\TooManyColumnsNimet.csv").DirectoryName;
            string csvFilePath = csvFileDirectory + @"\TooManyColumnsNimet.csv";

            try
            {
                List<NameDate> nameDates = Program.LoadCSV(csvFilePath);
                Assert.Fail("An exception should have been thrown");
            }
            catch (Exception e)
            {
                Assert.AreEqual($"inserted file name from path: {csvFilePath} is not in correct format 2 columns separeted with semicolumn", e.Message);
            }
        }

        [TestMethod()]
        public void LoadCSVDateColumnDataWrongTest()
        {
            string csvFileDirectory = new FileInfo(@"..\..\SupportFiles\DateDataWrongInColumnNimet.csv").DirectoryName;
            string csvFilePath = csvFileDirectory + @"\DateDataWrongInColumnNimet.csv";

            try
            {
                List<NameDate> nameDates = Program.LoadCSV(csvFilePath);
                Assert.Fail("An exception should have been thrown");
            }
            catch (Exception e)
            {
                Assert.AreEqual($"Data in row 1 is not correct. Please check the date is in correct format \"dd.MM\".", e.Message);
            }
        }

        [TestMethod()]
        public void FindNamesOfDateOkTest()
        {
            DateTime date = new DateTime(2019, 10, 11);
            List<NameDate> expectedValues = new List<NameDate>();
            NameDate nameDate1 = new NameDate(new string[] { "2.9", "Sinikka, Sini, Justus" }, 0);
            NameDate nameDate2 = new NameDate(new string[] { "3.9", "Soile, Soili, Soila" }, 1);
            NameDate nameDate3 = new NameDate(new string[] { "30.11", "Antti" }, 2);
            NameDate nameDate4 = new NameDate(new string[] { "11.10", "Ohto" }, 3);

            expectedValues.Add(nameDate1);
            expectedValues.Add(nameDate2);
            expectedValues.Add(nameDate3);
            expectedValues.Add(nameDate4);

            string names = Program.FindNamesOfDate(expectedValues, date);

            Assert.AreEqual(nameDate4.Names, names);
        }
    }
}