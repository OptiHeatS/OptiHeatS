using System.Collections.ObjectModel;
using ReactiveUI;

namespace OptiHeatPro.ViewModels
{
    public class OptimizerViewModel : ViewModelBase
    {
        private Result _WtotalResult;
        public Result WTotalResult
        {
            get => _WtotalResult;
            set => this.RaiseAndSetIfChanged(ref _WtotalResult, value);
        }
        private Result _StotalResult;
        public Result STotalResult
        {
            get => _StotalResult;
            set => this.RaiseAndSetIfChanged(ref _StotalResult, value);
        }

        
        public OptimizerViewModel()
        {
            HeatingData _heatingData = new HeatingData();
            _heatingData.Read();
            Optimizer optimizer = new Optimizer();
            List<Result> results = optimizer.Optimize(_heatingData.WinterData, 0);

            WTotalResult = new Result();
            foreach (var result in results)
            {
            WTotalResult.TotalElectricityProduction += result.TotalElectricityProduction;
            WTotalResult.TotalProductionCost += result.TotalProductionCost;
            WTotalResult.TotalGasConsumption += result.TotalGasConsumption;
            WTotalResult.TotalOilConsumption += result.TotalOilConsumption;
            WTotalResult.TotalElectricityConsumption += result.TotalElectricityConsumption;
            WTotalResult.TotalCO2Emissions += result.TotalCO2Emissions;
            }
            results = optimizer.Optimize(_heatingData.SummerData, 0);

            STotalResult = new Result();
            foreach (var result in results)
            {
            STotalResult.TotalElectricityProduction += result.TotalElectricityProduction;
            STotalResult.TotalProductionCost += result.TotalProductionCost;
            STotalResult.TotalGasConsumption += result.TotalGasConsumption;
            STotalResult.TotalOilConsumption += result.TotalOilConsumption;
            STotalResult.TotalElectricityConsumption += result.TotalElectricityConsumption;
            STotalResult.TotalCO2Emissions += result.TotalCO2Emissions;
            }
        }
    }
}
