using Avalonia;
namespace OptiHeatPro
{
    // Main class to run the application
    public class Program
    {
        public static void Main(string[] args)
        {
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
