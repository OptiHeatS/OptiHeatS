using System.Collections.Generic;
using System.Windows.Markup;
//using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;
using System.Collections.ObjectModel;
using ReactiveUI;

namespace OptiHeatPro.ViewModels
{
    public partial class GraphViewModel : ObservableObject
    {
        private static readonly SKColor BrightRed = new(226,0,15);
        //public Axis[] XAxes = new Axis[];
        private HeatingData _heatingData;
        private static List<decimal> Elps = new List<decimal> {};
        private static List<decimal> Elpw = new List<decimal> {};
        private static List<double> Hds = new List<double> {};
        private static List<double> Hdw = new List<double> {};
        private static List<string> DnTs = new List<string> {};
        private static List<string> DnTw = new List<string> {};
        private static List<double> FakeData = new List<double> {}; //temp
        public GraphViewModel(){
            _heatingData = new HeatingData();
            _heatingData.Read();
            foreach(var entry in _heatingData.SummerData) // the only way I could manage to extracy the values bbd..
            {
                Elps.Add(entry.ElectricityPrice);
                Hds.Add(entry.HeatDemand);
                DnTs.Add(Convert.ToString(entry.TimeFrom));
                FakeData.Add(1);
            }
            foreach(var entry in _heatingData.WinterData)
            {
                Elpw.Add(entry.ElectricityPrice);
                Hdw.Add(entry.HeatDemand);
                DnTw.Add(Convert.ToString(entry.TimeFrom));
            }
        }
        
        public ISeries[] Winter { get; set; } =
        {
            new StackedAreaSeries<double>
            {
                Name = "Boiler A",
                Fill = new SolidColorPaint(SKColors.Red),
                Values = FakeData
            },
            new StackedAreaSeries<double>
            {
                Name = "Boiler B",
                Fill = new SolidColorPaint(SKColors.Green),
                Values = FakeData
            },
            new StackedAreaSeries<double>
            {
                Name = "Boiler C",
                Fill = new SolidColorPaint(SKColors.Yellow),
                Values = FakeData
            }
        };
        public ISeries[] WElectricityPrices { get; set; } =
        {
            new StepLineSeries<decimal>
            {
                Values = Elpw,
                Fill = new SolidColorPaint(SKColors.LimeGreen),
                GeometrySize = 0,
                GeometryStroke = null
            }
        };
        public ISeries[] WHeatDemand { get; set; } =
        {
            new StepLineSeries<double>
            {
                Values = Hdw,
                Fill = new SolidColorPaint(SKColors.OrangeRed),
                GeometrySize = 0,
                GeometryStroke = null
            }
        };
        public ISeries[] WProductionCosts { get; set; } =
        {
            new StepLineSeries<double>
            {
                Values = FakeData,
                Fill = new SolidColorPaint(SKColors.LimeGreen),
                GeometrySize = 0,
                GeometryStroke = null
            }
        };
        public ISeries[] WEmissions { get; set; } =
        {
            new StepLineSeries<double>
            {
                Values = FakeData,
                Fill = new SolidColorPaint(SKColors.SlateGray),
                GeometrySize = 0,
                GeometryStroke = null
            }
        };


        public ISeries[] Summer { get; set; } =
        {
            new StackedAreaSeries<double>
            {
                Name = "Boiler A",
                Fill = new SolidColorPaint(SKColors.Red),
                Values = FakeData
            },
            new StackedAreaSeries<double>
            {
                Name = "Boiler A",
                Fill = new SolidColorPaint(SKColors.Green),
                Values = FakeData
            },
            new StackedAreaSeries<double>
            {
                Name = "Boiler C",
                Fill = new SolidColorPaint(SKColors.Yellow),
                Values = FakeData
            }
        };
        public ISeries[] SElectricityPrices { get; set; } =
        {
            new StepLineSeries<decimal>
            {
                Values = Elps,
                Fill = new SolidColorPaint(SKColors.LimeGreen),
                GeometrySize = 0,
                GeometryStroke = null
            }
        };
        public ISeries[] SHeatDemand { get; set; } =
        {
            new StepLineSeries<double>
            {
                Values = Hds,
                Fill = new SolidColorPaint(SKColors.OrangeRed),
                GeometrySize = 0,
                GeometryStroke = null
            }
        };
        public ISeries[] SProductionCosts { get; set; } =
        {
            new StepLineSeries<double>
            {
                Values = FakeData,
                Fill = new SolidColorPaint(SKColors.LimeGreen),
                GeometrySize = 0,
                GeometryStroke = null
            }
        };
        public ISeries[] SEmissions { get; set; } =
        {
            new StepLineSeries<double>
            {
                Values = FakeData,
                Fill = new SolidColorPaint(SKColors.SlateGray),
                GeometrySize = 0,
                GeometryStroke = null
            }
        };
        public Axis[] WXAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    Labels = DnTw
                }
            };
        public Axis[] SXAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    Labels = DnTs
                }
            };
        public Axis[] ElYAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    Name = "DKK / MWh(el)",
                    Labeler = (value) => Math.Round(value,2) + " DKK/MWh"
                }
            };
        public Axis[] HYAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    Name = "MWh",
                    Labeler = (value) => Math.Round(value,2) + " MWh"
                }
            };
        public Axis[] CYAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    Name = "DKK / MWh(th)",
                    Labeler = (value) => Math.Round(value,2) + " DKK/MWh"
                }
            };
        public Axis[] EmYAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    Name = "kg / MWh(th)",
                    Labeler = (value) => Math.Round(value,2) + " kg/MWh"
                }
            };
        public DrawMarginFrame DrawMarginFrame => new DrawMarginFrame
        {
            Stroke = new SolidColorPaint(SKColors.DimGray, 1)
        };
    }
}