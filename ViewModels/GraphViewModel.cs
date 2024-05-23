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
        private static List<double> STotalElectricityConsumption = new List<double> {};
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
        private static List<double> WTotalElectricityConsumption = new List<double> {};
        private static List<decimal> WTotalProductionCost = new List<decimal> {};
        private static List<double> WTotalGasConsumption = new List<double> {};
        private static List<double> WTotalOilConsumption = new List<double> {};
        private static List<double> WTotalCO2Emissions = new List<double> {};
        
        public GraphViewModel(){
            _heatingData = new HeatingData();
            _heatingData.Read();
            Optimizer optimizer = new Optimizer();
            List<Result> Results = optimizer.Optimize(_heatingData.SummerData, 0);
            
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
                STotalElectricityConsumption.Add(entry.TotalElectricityConsumption);
                STotalProductionCost.Add(entry.TotalProductionCost);
                STotalGasConsumption.Add(entry.TotalGasConsumption);
                STotalOilConsumption.Add(entry.TotalOilConsumption);
                STotalCO2Emissions.Add(entry.TotalCO2Emissions);
            }

            Results = optimizer.Optimize(_heatingData.WinterData, 0);

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
                WTotalElectricityConsumption.Add(entry.TotalElectricityConsumption);
                WTotalProductionCost.Add(entry.TotalProductionCost);
                WTotalGasConsumption.Add(entry.TotalGasConsumption);
                WTotalOilConsumption.Add(entry.TotalOilConsumption);
                WTotalCO2Emissions.Add(entry.TotalCO2Emissions);
            }
        }

        private static readonly SKColor GBC = new(204,166,51);
        private static readonly SKColor OBC = new(197,90,17);
        private static readonly SKColor GMC = new(0,112,192);
        private static readonly SKColor EBC = new(51,192,115);

        public ISeries[] Summer { get; set; } =
        {
            new StackedStepAreaSeries<double>
            {
                Name = "GM",
                Fill = new SolidColorPaint(GMC),
                Values = SGasMotorOutput,
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.DarkSlateGray) {StrokeThickness = 1},
                GeometryFill = new SolidColorPaint(GMC),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray),
                ScalesYAt = 0
            },
            new StackedStepAreaSeries<double>
            {
                Name = "EK",
                Fill = new SolidColorPaint(EBC),
                Values = SElectricBoilerOutput,
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.DarkSlateGray) {StrokeThickness = 1},
                GeometryFill = new SolidColorPaint(EBC),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray),
                ScalesYAt = 0

            },
            new StackedStepAreaSeries<double>
            {
                Name = "GB",
                Fill = new SolidColorPaint(GBC),
                Values = SGasBoilerOutput,
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.DarkSlateGray) {StrokeThickness = 1},
                GeometryFill = new SolidColorPaint(GBC),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray),
                ScalesYAt = 0
            },
            new StackedStepAreaSeries<double>
            {
                Name = "OB",
                Fill = new SolidColorPaint(OBC),
                Values = SOilBoilerOutput,
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.Brown) {StrokeThickness = 1},
                GeometryFill = new SolidColorPaint(OBC),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray),
                ScalesYAt = 0
            },
            new StepLineSeries<double>
            {
                Name = "Heat Demand",
                Values = SHeatDemand,
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.Black) {StrokeThickness = 3},
                GeometryFill = new SolidColorPaint(SKColors.Black),
                GeometryStroke = new SolidColorPaint(SKColors.Black),
                Fill = null,
                ScalesYAt = 0,
                ZIndex = 1999
            },
            new LineSeries<decimal>
            {
                Name= "Electricity Price",
                Stroke = new SolidColorPaint(SKColors.DarkGreen) {StrokeThickness = 2},
                GeometryFill = new SolidColorPaint(SKColors.DarkGreen),
                GeometryStroke = new SolidColorPaint(SKColors.DarkGreen),
                Fill = null,
                Values = SElectricityPrice,
                GeometrySize = 0,
                LineSmoothness = 1,
                ScalesYAt = 1,
                ZIndex = 2000
            }
        };
        public ISeries[] SummerElectricityPrices { get; set; } =
        {
            new StepLineSeries<decimal>
            {
                Name = "Electricity Price",
                Values = SElectricityPrice,
                Fill = new SolidColorPaint(EBC),
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.DarkSlateGray) {StrokeThickness = 1},
                GeometryFill = new SolidColorPaint(EBC),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray),
            }
        };
        public ISeries[] SummerElectricityProduction { get; set; } =
        {
            new StepLineSeries<double>
            {
                Name = "Produced Electricity",
                Values = STotalElectricityProduction,
                Fill = new SolidColorPaint(EBC),
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.DarkSlateGray) {StrokeThickness = 1},
                GeometryFill = new SolidColorPaint(EBC),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray),
                ScalesYAt = 0
            },
            new LineSeries<decimal>
            {
                Name= "Electricity Price",
                Stroke = new SolidColorPaint(SKColors.DarkGreen) {StrokeThickness = 2},
                GeometryFill = new SolidColorPaint(SKColors.DarkGreen),
                GeometryStroke = new SolidColorPaint(SKColors.DarkGreen),
                Fill = null,
                Values = SElectricityPrice,
                GeometrySize = 0,
                LineSmoothness = 1,
                ScalesYAt = 1,
                ZIndex = 2000
            }
        };
        public ISeries[] SummerElectricityConsumption { get; set; } =
        {
            new StepLineSeries<double>
            {
                Name = "Consumed Electricity",
                Values = STotalElectricityConsumption,
                Fill = new SolidColorPaint(EBC),
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.DarkSlateGray) {StrokeThickness = 1},
                GeometryFill = new SolidColorPaint(EBC),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray),
                ScalesYAt = 0
            },
            new LineSeries<decimal>
            {
                Name= "Electricity Price",
                Stroke = new SolidColorPaint(SKColors.DarkGreen) {StrokeThickness = 2},
                GeometryFill = new SolidColorPaint(SKColors.DarkGreen),
                GeometryStroke = new SolidColorPaint(SKColors.DarkGreen),
                Fill = null,
                Values = SElectricityPrice,
                GeometrySize = 0,
                LineSmoothness = 1,
                ScalesYAt = 1,
                ZIndex = 2000
            }
        };
        public ISeries[] SummerConsumption { get; set; } =
        {
            new StackedStepAreaSeries<double>
            {
                Name = "Gas",
                Fill = new SolidColorPaint(GBC),
                Values = STotalGasConsumption,
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.DarkSlateGray) {StrokeThickness = 1},
                GeometryFill = new SolidColorPaint(GBC),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray)
            },
            new StackedStepAreaSeries<double>
            {
                Name = "Oil",
                Fill = new SolidColorPaint(OBC),
                Values = STotalOilConsumption,
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.DarkSlateGray) {StrokeThickness = 1},
                GeometryFill = new SolidColorPaint(OBC),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray)
            }
        };
        public ISeries[] SummerHeatDemand { get; set; } =
        {
            new StepLineSeries<double>
            {
                Name = "Heat Demand",
                Values = SHeatDemand,
                Fill = new SolidColorPaint(SKColors.Peru),
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.DarkSlateGray) {StrokeThickness = 1},
                GeometryFill = new SolidColorPaint(SKColors.Peru),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray)
            }
        };
        public ISeries[] SummerProductionCosts { get; set; } =
        {
            new StepLineSeries<decimal>
            {
                Name = "Production Cost",
                Values = STotalProductionCost,
                Fill = new SolidColorPaint(EBC),
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.DarkSlateGray) {StrokeThickness = 1},
                GeometryFill = new SolidColorPaint(EBC),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray)
            }
        };
        public ISeries[] SummerEmissions { get; set; } =
        {
            new StepLineSeries<double>
            {
                Name = "CO2 Emissions",
                Values = STotalCO2Emissions,
                Fill = new SolidColorPaint(SKColors.SlateGray),
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.DarkSlateGray) {StrokeThickness = 1},
                GeometryFill = new SolidColorPaint(SKColors.SlateGray),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray)
            }
        };
        public ISeries[] Winter { get; set; } =
        {
            new StackedStepAreaSeries<double>
            {
                Name = "GM",
                Fill = new SolidColorPaint(GMC),
                Values = WGasMotorOutput,
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.DarkSlateGray) {StrokeThickness = 1},
                GeometryFill = new SolidColorPaint(GMC),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray),
                ScalesYAt = 0
            },
            new StackedStepAreaSeries<double>
            {
                Name = "EK",
                Fill = new SolidColorPaint(EBC),
                Values = WElectricBoilerOutput,
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.DarkSlateGray) {StrokeThickness = 1},
                GeometryFill = new SolidColorPaint(EBC),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray),
                ScalesYAt = 0
            },
            new StackedStepAreaSeries<double>
            {
                Name = "GB",
                Fill = new SolidColorPaint(GBC),
                Values = WGasBoilerOutput,
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.DarkSlateGray) {StrokeThickness = 1},
                GeometryFill = new SolidColorPaint(GBC),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray),
                ScalesYAt = 0
            },
            new StackedStepAreaSeries<double>
            {
                Name = "OB",
                Fill = new SolidColorPaint(OBC),
                Values = WOilBoilerOutput,
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.DarkSlateGray) {StrokeThickness = 1},
                GeometryFill = new SolidColorPaint(OBC),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray),
                ScalesYAt = 0
            },
            new StepLineSeries<double>
            {
                Name = "Heat Demand",
                Values = WHeatDemand,
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.Black) {StrokeThickness = 3},
                GeometryFill = new SolidColorPaint(SKColors.Black),
                GeometryStroke = new SolidColorPaint(SKColors.Black),
                Fill = null,
                ScalesYAt = 0,
                ZIndex = 1999
            },
            new LineSeries<decimal>
            {
                Name= "Electricity Price",
                Stroke = new SolidColorPaint(SKColors.DarkGreen) {StrokeThickness = 2},
                GeometryFill = new SolidColorPaint(SKColors.DarkGreen),
                GeometryStroke = new SolidColorPaint(SKColors.DarkGreen),
                Fill = null,
                Values = WElectricityPrice,
                GeometrySize = 0,
                LineSmoothness = 1,
                ScalesYAt = 1,
                ZIndex = 2000
            }
        };
        public ISeries[] WinterElectricityPrices { get; set; } =
        {
            new StepLineSeries<decimal>
            {
                Name = "Electricity Price",
                Values = WElectricityPrice,
                Fill = new SolidColorPaint(EBC),
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.DarkSlateGray) {StrokeThickness = 1},
                GeometryFill = new SolidColorPaint(EBC),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray)
            }
        };
         public ISeries[] WinterElectricityProduction { get; set; } =
        {
            new StepLineSeries<double>
            {
                Name = "Produced Electricity",
                Values = WTotalElectricityProduction,
                Fill = new SolidColorPaint(EBC),
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.DarkSlateGray) {StrokeThickness = 1},
                GeometryFill = new SolidColorPaint(EBC),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray)
            },
            new LineSeries<decimal>
            {
                Name= "Electricity Price",
                Stroke = new SolidColorPaint(SKColors.DarkGreen) {StrokeThickness = 2},
                GeometryFill = new SolidColorPaint(SKColors.DarkGreen),
                GeometryStroke = new SolidColorPaint(SKColors.DarkGreen),
                Fill = null,
                Values = WElectricityPrice,
                GeometrySize = 0,
                LineSmoothness = 1,
                ScalesYAt = 1,
                ZIndex = 2000
            }
        };
        public ISeries[] WinterElectricityConsumption { get; set; } =
        {
            new StepLineSeries<double>
            {
                Name = "Consumed Electricity",
                Values = WTotalElectricityConsumption,
                Fill = new SolidColorPaint(EBC),
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.DarkSlateGray) {StrokeThickness = 1},
                GeometryFill = new SolidColorPaint(EBC),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray),
                ScalesYAt = 0
            },
            new LineSeries<decimal>
            {
                Name= "Electricity Price",
                Stroke = new SolidColorPaint(SKColors.DarkGreen) {StrokeThickness = 2},
                GeometryFill = new SolidColorPaint(SKColors.DarkGreen),
                GeometryStroke = new SolidColorPaint(SKColors.DarkGreen),
                Fill = null,
                Values = WElectricityPrice,
                GeometrySize = 0,
                LineSmoothness = 1,
                ScalesYAt = 1,
                ZIndex = 2000
            }
        };
        public ISeries[] WinterConsumption { get; set; } =
        {
            new StackedStepAreaSeries<double>
            {
                Name = "Gas",
                Fill = new SolidColorPaint(GBC),
                Values = WTotalGasConsumption,
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.DarkSlateGray) {StrokeThickness = 1},
                GeometryFill = new SolidColorPaint(GBC),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray)
            },
            new StackedStepAreaSeries<double>
            {
                Name = "Oil",
                Fill = new SolidColorPaint(SKColors.Brown.WithAlpha(100)),
                Values = WTotalOilConsumption,
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.DarkSlateGray) {StrokeThickness = 1},
                GeometryFill = new SolidColorPaint(SKColors.Brown.WithAlpha(200)),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray)
            }
        };
        public ISeries[] WinterHeatDemand { get; set; } =
        {
            new StepLineSeries<double>
            {
                Name = "Heat Demand",
                Values = WHeatDemand,
                Fill = new SolidColorPaint(SKColors.Peru),
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.DarkSlateGray) {StrokeThickness = 1},
                GeometryFill = new SolidColorPaint(SKColors.Peru),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray)
            }
        };
        public ISeries[] WinterProductionCosts { get; set; } =
        {
            new StepLineSeries<decimal>
            {
                Name = "Production Cost",
                Values = WTotalProductionCost,
                Fill = new SolidColorPaint(EBC),
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.DarkSlateGray) {StrokeThickness = 1},
                GeometryFill = new SolidColorPaint(EBC),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray)
            }
        };
        public ISeries[] WinterEmissions { get; set; } =
        {
            new StepLineSeries<double>
            {
                Name = "CO2 Emissions",
                Values = WTotalCO2Emissions,
                Fill = new SolidColorPaint(SKColors.SlateGray),
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.DarkSlateGray) {StrokeThickness = 1},
                GeometryFill = new SolidColorPaint(SKColors.SlateGray),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray)
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
        public LabelVisual ECTitle { get; set; } =
        new LabelVisual
        {
            Text = "Electricity Consumption",
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