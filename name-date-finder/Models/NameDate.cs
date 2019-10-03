using System;

namespace name_date_finder
{
    public class NameDate
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public string Names { get; set; }


        public NameDate(string[] values, int row)
        {
            try
            {
                string[] dateAndMonth = values[0].Split('.');

                this.Day = Int32.Parse(dateAndMonth[0]);
                this.Month = Int32.Parse(dateAndMonth[1]);
                this.Names = values[1];
            }
            catch (FormatException)
            {
                throw new System.FormatException($"First column in row number {row} is not in in correct format. Correct format is \"dd.MM\".");
            }
        }
    }
}