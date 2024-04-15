using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Database;
using System;
using System.IO;

namespace OptiHeatPro
{
    public class ExcelData // Class for Source Dara Managing
    {
        public String? TFrom { get; set; } // time from
        public String? TTo { get; set; } // time to
        public Double HDemand { get; set; } // heat demand
        public Double EPrice { get; set; } // electricity price
        
        public ExcelData(String? tFrom, String? tTo, Double hDemand, Double ePrice)
        {
            TFrom = tFrom;
            TTo = tTo;
            HDemand = hDemand;
            EPrice = ePrice;
        }
        public static void Init(List<ExcelData> wPeriod, List<ExcelData> sPeriod) // Method to initialize excel data
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine(currentDirectory);
            string filePath = Path.Combine(currentDirectory, "Data.xlsx");

            ReadDataFromExcel(filePath, wPeriod, sPeriod);
        }

        public static void Print(List<ExcelData> wPeriod, List<ExcelData> sPeriod) // Method to write excel data to console
        {
            Console.WriteLine("\n Winter period:");
            for (int i = 0; i < wPeriod.Count; i++)
            {
                Console.WriteLine($"{wPeriod[i].TFrom}  {wPeriod[i].TTo}  {wPeriod[i].HDemand}  {wPeriod[i].EPrice}");
            }
            Console.WriteLine("\n Summer period:");
            for (int i = 0; i < wPeriod.Count; i++)
            {
                Console.WriteLine($"{sPeriod[i].TFrom}  {sPeriod[i].TTo}  {sPeriod[i].HDemand}  {sPeriod[i].EPrice}");
            }
        }

        public static void ReadDataFromExcel(string filePath, List<ExcelData> wPeriod, List<ExcelData> sPeriod) // Method to read data from excel file
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
}
