using System.Collections.Generic;
using System.Windows.Markup;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;
using System.Collections.ObjectModel;
using ReactiveUI;
using System.Dynamic;
using OptiHeatPro.Views;
using System.Security.Cryptography.X509Certificates;

namespace OptiHeatPro.ViewModels
{
    public partial class GraphViewModel : ObservableObject
    {
        private HeatingData _heatingData;
        
        // Each graph needs its own list
        private static List<decimal> SElectricityPrice = new List<decimal>{};
        private static List<double> SHeatDemand = new List<double>{};
        private static List<string> SDnT = new List<string> {};
        private static List<double> SGasBoilerOutput = new List<double> {};
        private static List<double> SOilBoilerOutput = new List<double> {};

        private static List<double> SGasMotorOutput = new List<double> {};
        private static List<double> SElectricBoilerOutput = new List<double> {};
        private static List<double> STotalElectricityProduction = new List<double> {};
        private static List<decimal> STotalProductionCost = new List<decimal> {};
        private static List<double> STotalGasConsumption = new List<double> {};
        private static List<double> STotalOilConsumption = new List<double> {};
        private static List<double> STotalCO2Emissions = new List<double> {};

        private static List<decimal> WElectricityPrice = new List<decimal>{};
        private static List<double> WHeatDemand = new List<double>{};
        private static List<string> WDnT = new List<string> {};
        private static List<double> WGasBoilerOutput = new List<double> {};
        private static List<double> WOilBoilerOutput = new List<double> {};

        private static List<double> WGasMotorOutput = new List<double> {};
        private static List<double> WElectricBoilerOutput = new List<double> {};
        private static List<double> WTotalElectricityProduction = new List<double> {};
        private static List<decimal> WTotalProductionCost = new List<decimal> {};
        private static List<double> WTotalGasConsumption = new List<double> {};
        private static List<double> WTotalOilConsumption = new List<double> {};
        private static List<double> WTotalCO2Emissions = new List<double> {};
        
        public GraphViewModel(){
            _heatingData = new HeatingData();
            _heatingData.Read();
            Optimizer optimizer = new Optimizer();
            List<Result> Results = optimizer.Optimize(_heatingData.SummerData);
            
            foreach(var entry in _heatingData.SummerData)
            {
                
                SElectricityPrice.Add(entry.ElectricityPrice);
                SHeatDemand.Add(entry.HeatDemand);
                SDnT.Add(Convert.ToString(entry.TimeFrom));
            }
            foreach (var entry in Results)
            {
                SGasBoilerOutput.Add(entry.GasBoilerOutput);
                SOilBoilerOutput.Add(entry.OilBoilerOutput);
                SGasMotorOutput.Add(entry.GasMotorOutput);
                SElectricBoilerOutput.Add(entry.ElectricBoilerOutput);
                STotalElectricityProduction.Add(entry.TotalElectricityProduction);
                STotalProductionCost.Add(entry.TotalProductionCost);
                STotalGasConsumption.Add(entry.TotalGasConsumption);
                STotalOilConsumption.Add(entry.TotalOilConsumption);
                STotalCO2Emissions.Add(entry.TotalCO2Emissions);
            }

            Results = optimizer.Optimize(_heatingData.WinterData);

            foreach(var entry in _heatingData.WinterData)
            {
                
                WElectricityPrice.Add(entry.ElectricityPrice);
                WHeatDemand.Add(entry.HeatDemand);
                WDnT.Add(Convert.ToString(entry.TimeFrom));
            }
            foreach (var entry in Results)
            {
                WGasBoilerOutput.Add(entry.GasBoilerOutput);
                WOilBoilerOutput.Add(entry.OilBoilerOutput);
                WGasMotorOutput.Add(entry.GasMotorOutput);
                WElectricBoilerOutput.Add(entry.ElectricBoilerOutput);
                WTotalElectricityProduction.Add(entry.TotalElectricityProduction);
                WTotalProductionCost.Add(entry.TotalProductionCost);
                WTotalGasConsumption.Add(entry.TotalGasConsumption);
                WTotalOilConsumption.Add(entry.TotalOilConsumption);
                WTotalCO2Emissions.Add(entry.TotalCO2Emissions);
            }
        }

        public ISeries[] Summer { get; set; } =
        {
            new StackedStepAreaSeries<double>
            {
                Name = "Gas Boiler",
                Fill = new SolidColorPaint(SKColors.Red.WithAlpha(100)),
                Values = SGasBoilerOutput,
                ScalesYAt = 0
            },
            new StackedStepAreaSeries<double>
            {
                Name = "Oil Boiler",
                Fill = new SolidColorPaint(SKColors.SlateGray.WithAlpha(100)),
                Values = SOilBoilerOutput,
                ScalesYAt = 0
            },
            new StackedStepAreaSeries<double>
            {
                Name = "Gas Motor",
                Fill = new SolidColorPaint(SKColors.YellowGreen.WithAlpha(150)),
                Values = SGasMotorOutput,
                ScalesYAt = 0
            },
            new StackedStepAreaSeries<double>
            {
                Name = "Electric Boiler",
                Fill = new SolidColorPaint(SKColors.CadetBlue.WithAlpha(200)),
                Values = SElectricBoilerOutput,
                ScalesYAt = 0
            },
            new StepLineSeries<double>
            {
                Name = "Heat Demand",
                Values = SHeatDemand,
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.DarkSlateGray) {StrokeThickness = 5},
                GeometryFill = new SolidColorPaint(SKColors.DarkSlateGray),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray),
                Fill = null,
                ScalesYAt = 0
            },
            new LineSeries<decimal>
            {
                Name= "Electricity Price",
                Stroke = new SolidColorPaint(SKColors.Black) {StrokeThickness = 3},
                GeometryFill = new SolidColorPaint(SKColors.Black),
                GeometryStroke = new SolidColorPaint(SKColors.Black),
                Fill = null,
                Values = SElectricityPrice,
                GeometrySize = 0,
                LineSmoothness = 1,
                ScalesYAt = 1
            }
        };
        public ISeries[] SummerElectricityPrices { get; set; } =
        {
            new StepLineSeries<decimal>
            {
                Name = "Electricity Price",
                Values = SElectricityPrice,
                Fill = new SolidColorPaint(SKColors.Green.WithAlpha(150)),
                Stroke = new SolidColorPaint(SKColors.Green.WithAlpha(150)),
                GeometrySize = 0,
                GeometryFill = null,
                GeometryStroke = null
            }
        };
         public ISeries[] SummerElectricityProduction { get; set; } =
        {
            new StepLineSeries<double>
            {
                Name = "Produced Electricity",
                Values = STotalElectricityProduction,
                Fill = new SolidColorPaint(SKColors.Green.WithAlpha(150)),
                Stroke = new SolidColorPaint(SKColors.Green.WithAlpha(150)),
                GeometrySize = 0,
                GeometryFill = null,
                GeometryStroke = null
            }
        };
        public ISeries[] SummerConsumption { get; set; } =
        {
            new StackedStepAreaSeries<double>
            {
                Name = "Gas",
                Fill = new SolidColorPaint(SKColors.YellowGreen.WithAlpha(150)),
                Values = STotalGasConsumption
            },
            new StackedStepAreaSeries<double>
            {
                Name = "Oil",
                Fill = new SolidColorPaint(SKColors.SlateGray.WithAlpha(100)),
                Values = STotalOilConsumption
            }
        };
        public ISeries[] SummerHeatDemand { get; set; } =
        {
            new StepLineSeries<double>
            {
                Name = "Heat Demand",
                Values = SHeatDemand,
                Fill = new SolidColorPaint(SKColors.Red.WithAlpha(150)),
                Stroke = new SolidColorPaint(SKColors.Red.WithAlpha(150)),
                GeometrySize = 0,
                GeometryFill = null,
                GeometryStroke = null
            }
        };
        public ISeries[] SummerProductionCosts { get; set; } =
        {
            new StepLineSeries<decimal>
            {
                Name = "Production Cost",
                Values = STotalProductionCost,
                Fill = new SolidColorPaint(SKColors.Green.WithAlpha(150)),
                Stroke = new SolidColorPaint(SKColors.Green.WithAlpha(150)),
                GeometrySize = 0,
                GeometryFill = null,
                GeometryStroke = null
            }
        };
        public ISeries[] SummerEmissions { get; set; } =
        {
            new StepLineSeries<double>
            {
                Name = "CO2 Emissions",
                Values = STotalCO2Emissions,
                Fill = new SolidColorPaint(SKColors.SlateGray.WithAlpha(200)),
                Stroke = new SolidColorPaint(SKColors.SlateGray.WithAlpha(200)),
                GeometrySize = 0,
                GeometryFill = null,
                GeometryStroke = null
            }
        };
        public ISeries[] Winter { get; set; } =
        {
            new StackedStepAreaSeries<double>
            {
                Name = "Gas Boiler",
                Fill = new SolidColorPaint(SKColors.Red.WithAlpha(100)),
                Values = WGasBoilerOutput,
                ScalesYAt = 0
            },
            new StackedStepAreaSeries<double>
            {
                Name = "Oil Boiler",
                Fill = new SolidColorPaint(SKColors.SlateGray.WithAlpha(100)),
                Values = WOilBoilerOutput,
                ScalesYAt = 0
            },
            new StackedStepAreaSeries<double>
            {
                Name = "Gas Motor",
                Fill = new SolidColorPaint(SKColors.YellowGreen.WithAlpha(150)),
                Values = WGasMotorOutput,
                ScalesYAt = 0
            },
            new StackedStepAreaSeries<double>
            {
                Name = "Electric Boiler",
                Fill = new SolidColorPaint(SKColors.CadetBlue.WithAlpha(200)),
                Values = WElectricBoilerOutput,
                ScalesYAt = 0
            },
            new StepLineSeries<double>
            {
                Name = "Heat Demand",
                Values = WHeatDemand,
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.DarkSlateGray) {StrokeThickness = 5},
                GeometryFill = new SolidColorPaint(SKColors.DarkSlateGray),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray),
                Fill = null,
                ScalesYAt = 0
            },
            new LineSeries<decimal>
            {
                Name= "Electricity Price",
                Stroke = new SolidColorPaint(SKColors.Black) {StrokeThickness = 3},
                GeometryFill = new SolidColorPaint(SKColors.Black),
                GeometryStroke = new SolidColorPaint(SKColors.Black),
                Fill = null,
                Values = WElectricityPrice,
                GeometrySize = 0,
                LineSmoothness = 1,
                ScalesYAt = 1
            }
        };
        public ISeries[] WinterElectricityPrices { get; set; } =
        {
            new StepLineSeries<decimal>
            {
                Name = "Electricity Price",
                Values = WElectricityPrice,
                Fill = new SolidColorPaint(SKColors.Green.WithAlpha(150)),
                Stroke = new SolidColorPaint(SKColors.Green.WithAlpha(150)),
                GeometrySize = 0,
                GeometryFill = null,
                GeometryStroke = null
            }
        };
         public ISeries[] WinterElectricityProduction { get; set; } =
        {
            new StepLineSeries<double>
            {
                Name = "Produced Electricity",
                Values = WTotalElectricityProduction,
                Fill = new SolidColorPaint(SKColors.Green.WithAlpha(150)),
                Stroke = new SolidColorPaint(SKColors.Green.WithAlpha(150)),
                GeometrySize = 0,
                GeometryFill = null,
                GeometryStroke = null
            }
        };
        public ISeries[] WinterConsumption { get; set; } =
        {
            new StackedStepAreaSeries<double>
            {
                Name = "Gas",
                Fill = new SolidColorPaint(SKColors.YellowGreen.WithAlpha(150)),
                Values = WTotalGasConsumption
            },
            new StackedStepAreaSeries<double>
            {
                Name = "Oil",
                Fill = new SolidColorPaint(SKColors.SlateGray.WithAlpha(100)),
                Values = WTotalOilConsumption
            }
        };
        public ISeries[] WinterHeatDemand { get; set; } =
        {
            new StepLineSeries<double>
            {
                Name = "Heat Demand",
                Values = WHeatDemand,
                Fill = new SolidColorPaint(SKColors.Red.WithAlpha(150)),
                Stroke = new SolidColorPaint(SKColors.Red.WithAlpha(150)),
                GeometrySize = 0,
                GeometryFill = null,
                GeometryStroke = null
            }
        };
        public ISeries[] WinterProductionCosts { get; set; } =
        {
            new StepLineSeries<decimal>
            {
                Name = "Production Cost",
                Values = WTotalProductionCost,
                Fill = new SolidColorPaint(SKColors.Green.WithAlpha(150)),
                Stroke = new SolidColorPaint(SKColors.Green.WithAlpha(150)),
                GeometrySize = 0,
                GeometryFill = null,
                GeometryStroke = null
            }
        };
        public ISeries[] WinterEmissions { get; set; } =
        {
            new StepLineSeries<double>
            {
                Name = "CO2 Emissions",
                Values = WTotalCO2Emissions,
                Fill = new SolidColorPaint(SKColors.SlateGray.WithAlpha(200)),
                Stroke = new SolidColorPaint(SKColors.SlateGray.WithAlpha(200)),
                GeometrySize = 0,
                GeometryFill = null,
                GeometryStroke = null
            }
        };

        public Axis[] WXAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    Labels = WDnT,
                }
            };
        public Axis[] SXAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    Labels = SDnT,
                }
            };
        public Axis[] HEYAxes { get; set; }
            = new Axis[]
            {
                
                new Axis
                {
                    //Name = "MW",
                    Labeler = (value) => Math.Round(value,2) + " MW",
                    MinLimit = 0
                },
                new Axis
                {
                    //Name = "DKK",
                    Labeler = (value) => Math.Round(value,2) + " DKK",
                    Position = LiveChartsCore.Measure.AxisPosition.End,
                    MinLimit = 0
                }
            };
        public Axis[] ElYAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    //Name = "DKK / MWh",
                    Labeler = (value) => Math.Round(value,2) + " DKK/MWh"
                }
            };
        public Axis[] HYAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    //Name = "MW",
                    Labeler = (value) => Math.Round(value,2) + " MW",
                    MinLimit = 0
                }
            };
        public Axis[] CYAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    //Name = "DKK",
                    Labeler = (value) => Math.Round(value,2) + " DKK"
                }
            };
        public Axis[] EmYAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    //Name = "kg",
                    Labeler = (value) => Math.Round(value,2) + " kg",
                    MinLimit = 0
                }
            };
        public DrawMarginFrame DrawMarginFrame => new DrawMarginFrame
        {
            Stroke = new SolidColorPaint(SKColors.DimGray, 1)
        };
        public LabelVisual HPTitle { get; set; } =
        new LabelVisual
        {
            Text = "Heat Production",
            TextSize = 20,
            Padding = new LiveChartsCore.Drawing.Padding(5),
            Paint = new SolidColorPaint(SKColors.Black)
        };
        public LabelVisual HDTitle { get; set; } =
        new LabelVisual
        {
            Text = "Heat Demand",
            TextSize = 20,
            Padding = new LiveChartsCore.Drawing.Padding(5),
            Paint = new SolidColorPaint(SKColors.Black)
        };
        public LabelVisual PCTitle { get; set; } =
        new LabelVisual
        {
            Text = "Production Costs",
            TextSize = 20,
            Padding = new LiveChartsCore.Drawing.Padding(5),
            Paint = new SolidColorPaint(SKColors.Black)
        };
        public LabelVisual EPTitle { get; set; } =
        new LabelVisual
        {
            Text = "Electricity Prices",
            TextSize = 20,
            Padding = new LiveChartsCore.Drawing.Padding(5),
            Paint = new SolidColorPaint(SKColors.Black)
        };
        public LabelVisual EPDTitle { get; set; } =
        new LabelVisual
        {
            Text = "Electricity Production",
            TextSize = 20,
            Padding = new LiveChartsCore.Drawing.Padding(5),
            Paint = new SolidColorPaint(SKColors.Black)
        };
        public LabelVisual FCTitle { get; set; } =
        new LabelVisual
        {
            Text = "Fuel Consumption",
            TextSize = 20,
            Padding = new LiveChartsCore.Drawing.Padding(5),
            Paint = new SolidColorPaint(SKColors.Black)
        };
        public LabelVisual ETitle { get; set; } =
        new LabelVisual
        {
            Text = "CO2 Emissions",
            TextSize = 20,
            Padding = new LiveChartsCore.Drawing.Padding(5),
            Paint = new SolidColorPaint(SKColors.Black)
        };
    }
}