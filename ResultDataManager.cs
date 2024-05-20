using System;
using System.Collections.Generic;
using System.IO;

namespace OptiHeatPro
{
    public class ResultDataManager
    {
        public void WriteResultsToCSV(List<Result> results, string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write the CSV header
                    writer.WriteLine("Gas Boiler Output (MWh),Oil Boiler Output (MWh),Gas Motor Output (MWh),Electric Boiler Output (MWh),Total Electricity Production (MWh),Total Production Cost (DKK/MWh),Total Gas Consumption (MWh(gas)),Total Oil Consumption (MWh(oil)),Total CO2 Emissions (kg)");

                    // Write each result to the CSV file
                    foreach (var result in results)
                    {
                        writer.WriteLine($"{result.GasBoilerOutput},{result.OilBoilerOutput},{result.GasMotorOutput},{result.ElectricBoilerOutput},{result.TotalElectricityProduction},{Math.Round(result.TotalProductionCost, 2)},{result.TotalGasConsumption},{result.TotalOilConsumption},{result.TotalCO2Emissions}");
                    }
                }

                Console.WriteLine("Results written to CSV successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing results to CSV: {ex.Message}");
            }
        }
    }
}