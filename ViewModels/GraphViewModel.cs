using System.Collections.Generic;
using System.Windows.Markup;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;

namespace OptiHeatPro.ViewModels
{
    public partial class GraphViewModel : ObservableObject
    {
        public ISeries[] ElectricityPrices { get; set; } =
        {
            new StepLineSeries<double>
            {
                Values = new double[] { 2, 1, 3, 5, 3, 4, 6 },
                Fill = new SolidColorPaint(SKColors.LimeGreen)
            }
        };
        public ISeries[] HeatDemand { get; set; } =
        {
            new StepLineSeries<double>
            {
                Values = new List<double> { 2, 1, 3, 4, 3, 4, 6 },
                Fill = new SolidColorPaint(SKColors.OrangeRed)
            }
        };
        public ISeries[] ProductionCosts { get; set; } =
        {
            new StepLineSeries<double>
            {
                Values = new List<double> { 2, 1, 3, 4, 3, 4, 6 },
                Fill = new SolidColorPaint(SKColors.LimeGreen)
            }
        };
        public ISeries[] Emissions { get; set; } =
        {
            new StepLineSeries<double>
            {
                Values = new List<double> { 2, 1, 3, 4, 3, 4, 6 },
                Fill = new SolidColorPaint(SKColors.SlateGray)
            }
        };
        public ISeries[] Winter { get; set; } =
        {
            new StackedAreaSeries<double>
            {
                Name = "Boiler A",
                Values = new List<double> { 3, 2, 3, 5, 3, 4, 6 }
            },
            new StackedAreaSeries<double>
            {
                Name = "Boiler B",
                Values = new List<double> { 6.9, 5, 6, 3, 8, 5, 2 }
            },
            new StackedAreaSeries<double>
            {
                Name = "Boiler C",
                Values = new List<double> { 4, 8, 2, 8, 9, 5, 3 }
            }
        };
        public ISeries[] Summer { get; set; } =
        {
            new StackedAreaSeries<double>
            {
                Name = "Boiler A",
                Values = new List<double> { 3.2, 1, 3, 0, 3, 4, 2 }
            },
            new StackedAreaSeries<double>
            {
                Name = "Boiler A",
                Values = new List<double> { 5, 5, 3, 3, 4, 5, 2 }
            },
            new StackedAreaSeries<double>
            {
                Name = "Boiler C",
                Values = new List<double> { 4, 8, 7, 8, 9, 5, 8 }
            }
        };
        public DrawMarginFrame DrawMarginFrame => new DrawMarginFrame
        {
            //Fill = new SolidColorPaint(SKColors.AliceBlue),
            Stroke = new SolidColorPaint(SKColors.DimGray, 1)
        };
    }
}