using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using System;
using System.IO;
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
            
            HeatingData.Read();
            // Initialize UI
            // BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

            // double totalCO2Emissions = boiler1.CO2Emissions + boiler2.CO2Emissions + boiler3.CO2Emissions + boiler4.CO2Emissions;
        }
        // Method to configure Avalonia application
        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();
        }
    }
}
