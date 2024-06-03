namespace OptiHeatPro
{
    public class ResultDataManager
    {
        public void WriteResultsToCSV(List<Result> results, string filePath)
        {
            ValidateParameters(results, filePath);

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write the CSV header
                    writer.WriteLine("Gas Boiler Output (MWh),Oil Boiler Output (MWh),Gas Motor Output (MWh),Electric Boiler Output (MWh),Total Electricity Production (MWh),Total Electricity Consumption (MWh),Total Production Cost (DKK/MWh),Total Gas Consumption (MWh(gas)),Total Oil Consumption (MWh(oil)),Total CO2 Emissions (kg)");

                    // Write each result to the CSV file
                    foreach (Result result in results)
                    {
                        writer.WriteLine($"{result.GasBoilerOutput},{result.OilBoilerOutput},{result.GasMotorOutput},{result.ElectricBoilerOutput},{result.TotalElectricityProduction},{result.TotalElectricityConsumption},{result.TotalProductionCost:F2},{result.TotalGasConsumption},{result.TotalOilConsumption},{result.TotalCO2Emissions}");
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error writing results to CSV: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing results to CSV: {ex.Message}");
            }
        }
        private void ValidateParameters(List<Result> results, string filePath)
        {
            if (results == null)
            {
                throw new ArgumentNullException(nameof(results));
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            }
        }

        private void HandleError(string message, Exception ex)
        {
            Console.WriteLine($"{message}: {ex.Message}");
        }
    }
}