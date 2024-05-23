using Avalonia;

namespace OptiHeatPro
{
    // Main class to run the application
    public class Program
    {
        public static void Main(string[] args)
        {
            var heatingData = new HeatingData();
            var optimizer = new Optimizer();
            var resultDataManager = new ResultDataManager();

            // Read heating data
            heatingData.Read();

            // Optimize and write Winter results
            WriteResults(heatingData.WinterData, "Data/WinterResults.csv", optimizer, resultDataManager);

            // Optimize and write Summer results
            WriteResults(heatingData.SummerData, "Data/SummerResults.csv", optimizer, resultDataManager);

            // Initialize UI
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        // Method to configure Avalonia
        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();
        }

        private static void WriteResults(List<DataEntry> data, string filePath, Optimizer optimizer, ResultDataManager resultDataManager)
        {
            var results = optimizer.Optimize(data, 0);
            resultDataManager.WriteResultsToCSV(results, filePath);
        }
    }
}
