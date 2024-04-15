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

            // Creating instances for different periods
            List<ExcelData> wPeriod = new List<ExcelData>(); // winter period
            List<ExcelData> sPeriod = new List<ExcelData>(); // summer period

            // Reading data
            ExcelData.Init(wPeriod, sPeriod);

            // Writing data to console
            ExcelData.Print(wPeriod, sPeriod);
        }
    }
}
