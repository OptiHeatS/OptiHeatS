using System;
using System.IO;
using OfficeOpenXml;

namespace OptiHeatPro
{
    // Main class to run the application
    public class Program
    {
        public static void Main(string[] args)
        {
            // Creating instances of Boiler
            Boiler boiler1 = new Boiler("GB", 5.0, 500, 215, 1.1);
            Boiler boiler2 = new Boiler("OB", 4.0, 700, 265, 1.2);
            Boiler boiler3 = new Boiler("GM", 3.6, 2.7, 1100, 640, 1.9);
            Boiler boiler4 = new Boiler("EK", 8.0, -8.0, 50);

            // Displaying information about each boiler
            Console.WriteLine("Boiler Information:");
            Console.WriteLine("-------------------");
            Print.DisplayBoilerInfo(boiler1);
            Print.DisplayBoilerInfo(boiler2);
            Print.DisplayBoilerInfo(boiler3);
            Print.DisplayBoilerInfo(boiler4);

            // Example calculation based on boiler specifications
            double totalCO2Emissions = boiler1.CO2Emissions + boiler2.CO2Emissions + boiler3.CO2Emissions + boiler4.CO2Emissions;
            Console.WriteLine("\nTotal CO2 Emissions: " + totalCO2Emissions);
            
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, "Data.xlsx");

            // Call the method to read data from Excel
            List<ExcelData> wPeriod = new List<ExcelData>(); // winter period
            List<ExcelData> sPeriod = new List<ExcelData>(); // summer period

            ReadDataFromExcel(filePath, wPeriod, sPeriod);

            Console.WriteLine("\n Winter period:");
            for(int i=0; i<wPeriod.Count; i++)
            {
                Console.WriteLine($"{wPeriod[i].TFrom}  {wPeriod[i].TTo}  {wPeriod[i].HDemand}  {wPeriod[i].EPrice}");
            }
            Console.WriteLine("\n Summer period:");
            for(int i=0; i<wPeriod.Count; i++)
            {
                Console.WriteLine($"{sPeriod[i].TFrom}  {sPeriod[i].TTo}  {sPeriod[i].HDemand}  {sPeriod[i].EPrice}");
            }
        }
    
     // Method to read data from excel file

    public static void ReadDataFromExcel(string filePath,List<ExcelData> wPeriod, List<ExcelData> sPeriod)
        {
            if (File.Exists(filePath))
            {
                using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    for (int row = 4; row <= rowCount; row++)
                    {
                        wPeriod.Add(new ExcelData(Convert.ToString(worksheet.Cells[row, 2].Value), Convert.ToString(worksheet.Cells[row, 3].Value), Convert.ToDouble(worksheet.Cells[row, 4].Value), Convert.ToDouble(worksheet.Cells[row, 5].Value)));
                        sPeriod.Add(new ExcelData(Convert.ToString(worksheet.Cells[row, 7].Value), Convert.ToString(worksheet.Cells[row, 8].Value), Convert.ToDouble(worksheet.Cells[row, 9].Value), Convert.ToDouble(worksheet.Cells[row, 10].Value)));
                        /*
                        for (int col = 1; col <= colCount; col++)
                        {
                            Console.Write(worksheet.Cells[row, col].Value + "\t");
                        }
                        Console.WriteLine();
                        */
                    }
                }
                Console.WriteLine("Data read successfully from Excel.");
            }
            else
            {
                Console.WriteLine("Excel file not found. " + filePath);
            }
        }
    }

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
}
