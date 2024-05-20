using Avalonia;
namespace OptiHeatPro
{
    // Main class to run the application
    public class Program
    {
        public static void Main(string[] args)
        {
            HeatingData heatingData = new HeatingData();
            Optimizer optimizer = new Optimizer();
            ResultDataManager resultDataManager = new ResultDataManager();

            heatingData.Read();

            // Choose file path for Winter results
            string filePath = "WinterResults.csv";

            // Winter results
            List<Result> results = optimizer.Optimize(heatingData.WinterData);

            // Write results to CSV file
            resultDataManager.WriteResultsToCSV(results, filePath);

            // Choose file path for summer results
            filePath = "SummerResults.csv";

            // Summer results
            results = optimizer.Optimize(heatingData.SummerData);

            // Write results to CSV file
            resultDataManager.WriteResultsToCSV(results, filePath);


             //Initialize UI
             BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
        //Method to configure Avalonia
        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();
        }
    }
}
