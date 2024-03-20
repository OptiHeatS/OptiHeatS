using System;
using System.IO;
using OfficeOpenXml;

namespace OptiHeatPro
{
    // Class to represent a single data entry in the imported file
    /*public class DataEntry
    {
        public DateTime Date { get; set; }
        public double MarketPrice { get; set; }
        public double Temperature { get; set; }
        // Add other relevant properties
    }
    */
    // Class to handle importing and processing data from the file...

    // Class to represent the heat production schedule
    /*public class HeatProductionSchedule
    {

    }
    */
    // Class to generate and manage heat production schedules
    /*public class Scheduler
    {
        public List<HeatProductionSchedule> GenerateSchedule(List<DataEntry> importedData)
        {

        }
    }*/

    // Main class to run the application
    class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, "Data.xlsx");

            // Call the method to read data from Excel
            ReadDataFromExcel(filePath);
        }
    
     // Method to read data from excel file
    static void ReadDataFromExcel(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    for (int row = 1; row <= rowCount; row++)
                    {
                        for (int col = 1; col <= colCount; col++)
                        {
                            Console.Write(worksheet.Cells[row, col].Value + "\t");
                        }
                        Console.WriteLine();
                    }
                }
                Console.WriteLine("Data read successfully from Excel.");
            }
            else
            {
                Console.WriteLine("Excel file not found.");
            }
        }
    }
}

