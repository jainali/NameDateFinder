using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace name_date_finder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string csvFilePath;
            string dateSyntax = "yyyy-MM-dd";

            if (Debugger.IsAttached)
            {
                string csvFileDirectory = new FileInfo(@"..\..\SupportFiles\Nimet.csv").DirectoryName;
                csvFilePath = csvFileDirectory + @"\Nimet.csv";
            }
            else
            {
                string currentFolder = System.AppContext.BaseDirectory;
                string csvFileName = "SupportFiles\\nimet.csv";
                csvFilePath = currentFolder + csvFileName;
            }

            csvFilePath = args.Length >= 2 ? args[1] : csvFilePath;
            dateSyntax = args.Length >= 3 ? args[2] : dateSyntax;

            DateTime date = GetDateTimeFromArgs(args, dateSyntax);

            List<NameDate> nameDates = LoadCSV(csvFilePath);

            string namesOfTheDate = FindNamesOfDate(nameDates, date);

            Console.WriteLine(namesOfTheDate);
            Console.Out.Write(namesOfTheDate);

        }

        public static DateTime GetDateTimeFromArgs(string[] args, string dateSyntax)
        {
            DateTime date = new DateTime();

            if (args.Length == 0)
            {
                throw new Exception("No input date argument added to commandline command");
            }

            try
            {
                date = DateTime.ParseExact(args[0], dateSyntax, CultureInfo.InvariantCulture);
            }
            catch
            {
                throw new Exception($"Inserted syntax for Datetime: {args[0]} is not correct. Correct format is : {dateSyntax}");

            }

            return date;
        }

        public static List<NameDate> LoadCSV(string filename)
        {
            string whole_file;
            try
            {
                // Get the file's text.
                whole_file = System.IO.File.ReadAllText(filename);
            }
            catch (System.IO.FileNotFoundException)
            {
                throw new System.IO.FileNotFoundException($"inserted file name from path: {filename} is not found");
            }

            // Split into lines.
            whole_file = whole_file.Replace('\n', '\r');

            string[] lines = whole_file.Split(new char[] { '\r' },
                StringSplitOptions.RemoveEmptyEntries);

            // See how many rows and columns there are.
            int num_rows = lines.Length;

            int num_cols = lines[0].Split(';').Length;

            if (num_cols != 2)
            {
                throw new Exception($"inserted file name from path: {filename} is not in correct format 2 columns separeted with semicolumn");
            }

            // Generate List for All name dates
            List<NameDate> nameDates = new List<NameDate>();
            int row = 0;

            // Load the array.
            try
            {
                for (row = 0; row < num_rows; row++)
                {
                    string[] columns = lines[row].Split(';');
                    NameDate nameDate = new NameDate(columns, row);
                    nameDates.Add(nameDate);
                }
            }
            catch
            {
                throw new Exception($"Data in row {row} is not correct. Please check the date is in correct format \"dd.MM\".");
            }

            return nameDates;
        }

        public static string FindNamesOfDate(List<NameDate> nameDates, DateTime date)
        {

            NameDate name = nameDates.FirstOrDefault(nd => nd.Day == date.Day && nd.Month == date.Month);

            string namesOfTheDate = name != null ? name.Names : $"No names found from file for the date {date}";

            return namesOfTheDate;
        }

    }
}
