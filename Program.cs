using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using System;
using System.IO;
using OfficeOpenXml;
using OptiHeatPro.ViewModels;

namespace OptiHeatPro
{
    // Main class to run the application
    public class Program
    {
        public static void Main(string[] args)
        {

            // Create instances of Boiler
            Boiler boiler1 = new Boiler("GB", 5.0, 500, 215, 1.1);
            Boiler boiler2 = new Boiler("OB", 4.0, 700, 265, 1.2);
            Boiler boiler3 = new Boiler("GM", 3.6, 2.7, 1100, 640, 1.9);
            Boiler boiler4 = new Boiler("EK", 8.0, -8.0, 50);

            // Initialize UI
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

            // Display information about each boiler
            //Console.WriteLine("Boiler Information:");
            //Console.WriteLine("-------------------");
           // Print.DisplayBoilerInfo(boiler1);
           // Print.DisplayBoilerInfo(boiler2);
           // Print.DisplayBoilerInfo(boiler3);
           // Print.DisplayBoilerInfo(boiler4);

            // Example calculation based on boiler specifications
            double totalCO2Emissions = boiler1.CO2Emissions + boiler2.CO2Emissions + boiler3.CO2Emissions + boiler4.CO2Emissions;
            //Console.WriteLine("\nTotal CO2 Emissions: " + totalCO2Emissions);

            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, "Data.xlsx");

            // Call the method to read data from Excel
            //ReadDataFromExcel(filePath);
        }

        // Method to read data from excel file
        public static void ReadDataFromExcel(string filePath)
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

        // Method to configure Avalonia application
        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace(); // Optionally remove .WithInterFont() if not needed
        }
    }
}
