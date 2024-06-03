using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;
using System.Collections.ObjectModel;
using ReactiveUI;
using LiveChartsCore.Defaults;

namespace OptiHeatPro.ViewModels
{
    public partial class GraphViewModel : ViewModelBase
    {
        private static readonly SKColor GBC = new(204,166,51);
        private static readonly SKColor OBC = new(197,90,17);
        private static readonly SKColor GMC = new(0,112,192);
        private static readonly SKColor EBC = new(51,192,115);
        private HeatingData _heatingData = new HeatingData();

        // Each graph needs its own list
        private static List<decimal> SElectricityPrice = new List<decimal>{};
        private static List<double> SHeatDemand = new List<double>{};
        private static List<string> SDnT = new List<string> {};
        private static ObservableCollection<ObservableValue> SGasBoilerOutput = new ObservableCollection<ObservableValue>();
        private static ObservableCollection<ObservableValue> SOilBoilerOutput = new ObservableCollection<ObservableValue>();
        private static ObservableCollection<ObservableValue> SGasMotorOutput = new ObservableCollection<ObservableValue>();
        private static ObservableCollection<ObservableValue> SElectricBoilerOutput = new ObservableCollection<ObservableValue>();
        private static ObservableCollection<ObservableValue> STotalElectricityProduction = new ObservableCollection<ObservableValue>();
        private static ObservableCollection<ObservableValue> STotalElectricityConsumption = new ObservableCollection<ObservableValue>();
        private static ObservableCollection<ObservableValue> STotalProductionCost = new ObservableCollection<ObservableValue>();
        private static ObservableCollection<ObservableValue> STotalGasConsumption = new ObservableCollection<ObservableValue>();
        private static ObservableCollection<ObservableValue> STotalOilConsumption = new ObservableCollection<ObservableValue>();
        private static ObservableCollection<ObservableValue> STotalCO2Emissions = new ObservableCollection<ObservableValue>();
        private static List<decimal> WElectricityPrice = new List<decimal>{};
        private static List<double> WHeatDemand = new List<double>{};
        private static List<string> WDnT = new List<string> {};
        private static ObservableCollection<ObservableValue> WGasBoilerOutput = new ObservableCollection<ObservableValue>();
        private static ObservableCollection<ObservableValue> WOilBoilerOutput = new ObservableCollection<ObservableValue>();
        private static ObservableCollection<ObservableValue> WGasMotorOutput = new ObservableCollection<ObservableValue>();
        private static ObservableCollection<ObservableValue> WElectricBoilerOutput = new ObservableCollection<ObservableValue>();
        private static ObservableCollection<ObservableValue> WTotalElectricityProduction = new ObservableCollection<ObservableValue>();
        private static ObservableCollection<ObservableValue> WTotalElectricityConsumption = new ObservableCollection<ObservableValue>();
        private static ObservableCollection<ObservableValue> WTotalProductionCost = new ObservableCollection<ObservableValue>();
        private static ObservableCollection<ObservableValue> WTotalGasConsumption = new ObservableCollection<ObservableValue>();
        private static ObservableCollection<ObservableValue> WTotalOilConsumption = new ObservableCollection<ObservableValue>();
        private static ObservableCollection<ObservableValue> WTotalCO2Emissions = new ObservableCollection<ObservableValue>();
        
        private int _co2ReductionPercentage = 0;
        public int co2ReductionPercentage
        {
            get => _co2ReductionPercentage;
            set => this.RaiseAndSetIfChanged(ref _co2ReductionPercentage, value);
        }
        Optimizer optimizer = new Optimizer();
        public GraphViewModel()
        {
            _heatingData.Read();

            InitialGraphData();
            UpdateWinter(co2ReductionPercentage);
            UpdateSummer(co2ReductionPercentage);

            this.WhenAnyValue(x => x.co2ReductionPercentage)
            .Subscribe(newValue => { UpdateWinter(newValue); UpdateSummer(newValue); });
        }

        private void InitialGraphData()
        {
            SElectricityPrice.Clear(); //Have to do this because for some reason ViewModel is ran twice??
            SHeatDemand.Clear();
            SDnT.Clear();
            WElectricityPrice.Clear();
            WHeatDemand.Clear();
            WDnT.Clear();
            foreach(var entry in _heatingData.SummerData!)
            {
                
                SElectricityPrice.Add(entry.ElectricityPrice);
                SHeatDemand.Add(entry.HeatDemand);
                SDnT.Add(Convert.ToString(entry.TimeFrom));
            }
            foreach(var entry in _heatingData.WinterData!)
            {
                
                WElectricityPrice.Add(entry.ElectricityPrice);
                WHeatDemand.Add(entry.HeatDemand);
                WDnT.Add(Convert.ToString(entry.TimeFrom));
            }
        }
        private void UpdateWinter(int co2ReductionPercentage)
        {
            WGasBoilerOutput.Clear();
            WOilBoilerOutput.Clear();
            WGasMotorOutput.Clear();
            WElectricBoilerOutput.Clear();
            WTotalElectricityProduction.Clear();
            WTotalElectricityConsumption.Clear();
            WTotalProductionCost.Clear();
            WTotalGasConsumption.Clear();
            WTotalOilConsumption.Clear();
            WTotalCO2Emissions.Clear();
            List<Result> Results = optimizer.Optimize(_heatingData.WinterData!, (double)co2ReductionPercentage/100);

            foreach (var entry in Results)
            {
                WGasBoilerOutput.Add(new(entry.GasBoilerOutput));
                WOilBoilerOutput.Add(new(entry.OilBoilerOutput));
                WGasMotorOutput.Add(new(entry.GasMotorOutput));
                WElectricBoilerOutput.Add(new(entry.ElectricBoilerOutput));
                WTotalElectricityProduction.Add(new(entry.TotalElectricityProduction));
                WTotalElectricityConsumption.Add(new(entry.TotalElectricityConsumption));
                WTotalProductionCost.Add(new((double)entry.TotalProductionCost));
                WTotalGasConsumption.Add(new(entry.TotalGasConsumption));
                WTotalOilConsumption.Add(new(entry.TotalOilConsumption));
                WTotalCO2Emissions.Add(new(entry.TotalCO2Emissions));
            }
        }
        private void UpdateSummer(int co2ReductionPercentage)
        {
            SGasBoilerOutput.Clear();
            SOilBoilerOutput.Clear();
            SGasMotorOutput.Clear();
            SElectricBoilerOutput.Clear();
            STotalElectricityProduction.Clear();
            STotalElectricityConsumption.Clear();
            STotalProductionCost.Clear();
            STotalGasConsumption.Clear();
            STotalOilConsumption.Clear();
            STotalCO2Emissions.Clear();
            List<Result> Results = optimizer.Optimize(_heatingData.SummerData!, (double)co2ReductionPercentage/100);

            foreach (var entry in Results)
            {
                SGasBoilerOutput.Add(new(entry.GasBoilerOutput));
                SOilBoilerOutput.Add(new(entry.OilBoilerOutput));
                SGasMotorOutput.Add(new(entry.GasMotorOutput));
                SElectricBoilerOutput.Add(new(entry.ElectricBoilerOutput));
                STotalElectricityProduction.Add(new(entry.TotalElectricityProduction));
                STotalElectricityConsumption.Add(new(entry.TotalElectricityConsumption));
                STotalProductionCost.Add(new((double)entry.TotalProductionCost));
                STotalGasConsumption.Add(new(entry.TotalGasConsumption));
                STotalOilConsumption.Add(new(entry.TotalOilConsumption));
                STotalCO2Emissions.Add(new(entry.TotalCO2Emissions));
            }
        }
        public ISeries[] Summer { get; set; } =
        {
            new StackedStepAreaSeries<ObservableValue>
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
            new StackedStepAreaSeries<ObservableValue>
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
            new StackedStepAreaSeries<ObservableValue>
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
            new StackedStepAreaSeries<ObservableValue>
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
            new StepLineSeries<ObservableValue>
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
            new StepLineSeries<ObservableValue>
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
            new StackedStepAreaSeries<ObservableValue>
            {
                Name = "Gas",
                Fill = new SolidColorPaint(GBC),
                Values = STotalGasConsumption,
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.DarkSlateGray) {StrokeThickness = 1},
                GeometryFill = new SolidColorPaint(GBC),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray)
            },
            new StackedStepAreaSeries<ObservableValue>
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
            new StepLineSeries<ObservableValue>
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
            new StepLineSeries<ObservableValue>
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
            new StackedStepAreaSeries<ObservableValue>
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
            new StackedStepAreaSeries<ObservableValue>
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
            new StackedStepAreaSeries<ObservableValue>
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
            new StackedStepAreaSeries<ObservableValue>
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
            new StepLineSeries<ObservableValue>
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
            new StepLineSeries<ObservableValue>
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
            new StackedStepAreaSeries<ObservableValue>
            {
                Name = "Gas",
                Fill = new SolidColorPaint(GBC),
                Values = WTotalGasConsumption,
                GeometrySize = 0,
                Stroke = new SolidColorPaint(SKColors.DarkSlateGray) {StrokeThickness = 1},
                GeometryFill = new SolidColorPaint(GBC),
                GeometryStroke = new SolidColorPaint(SKColors.DarkSlateGray)
            },
            new StackedStepAreaSeries<ObservableValue>
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
            new StepLineSeries<ObservableValue>
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
            new StepLineSeries<ObservableValue>
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
                    Labels = WDnT
                }
            };
        public Axis[] SXAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    Labels = SDnT
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
                    Labeler = (value) => Math.Round(value,2) + " DKK/MWh",
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