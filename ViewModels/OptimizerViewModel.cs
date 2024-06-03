using ReactiveUI;

namespace OptiHeatPro.ViewModels
{
    public class OptimizerViewModel : ViewModelBase
    {
        private double _co2ReductionPercentage;
        public double co2ReductionPercentage
        {
            get => _co2ReductionPercentage;
            set => this.RaiseAndSetIfChanged(ref _co2ReductionPercentage, value);
        }

        private Result _wTotalResult;
        public Result WTotalResult
        {
            get => _wTotalResult;
            set => this.RaiseAndSetIfChanged(ref _wTotalResult, value);
        }

        private Result _sTotalResult;
        public Result STotalResult
        {
            get => _sTotalResult;
            set => this.RaiseAndSetIfChanged(ref _sTotalResult, value);
        }

        public OptimizerViewModel()
        {
            WTotalResult = new Result();
            STotalResult = new Result();
            // Check if co2 reduction slider value has changed and update data.
            this.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(co2ReductionPercentage))
                {
                    CalculateResults();
                }
            };

            CalculateResults();
        }

        private void CalculateResults()
        {
            // Optimizing data from heatingData
            HeatingData heatingData = new HeatingData();
            heatingData.Read();
            Optimizer optimizer = new Optimizer();
            List<Result> results = optimizer.Optimize(heatingData.WinterData, co2ReductionPercentage);

            Result newWTotalResult = new Result();
            // Summing up Winter results from all the given optimizer data.
            foreach (var result in results)
            {
                newWTotalResult.TotalElectricityProduction += result.TotalElectricityProduction;
                newWTotalResult.TotalProductionCost += result.TotalProductionCost;
                newWTotalResult.TotalGasConsumption += result.TotalGasConsumption;
                newWTotalResult.TotalOilConsumption += result.TotalOilConsumption;
                newWTotalResult.TotalElectricityConsumption += result.TotalElectricityConsumption;
                newWTotalResult.TotalCO2Emissions += result.TotalCO2Emissions;
            }
            WTotalResult = newWTotalResult;

            results = optimizer.Optimize(heatingData.SummerData, co2ReductionPercentage);

            Result newSTotalResult = new Result();
            // Summing up Summer results.
            foreach (var result in results)
            {
                newSTotalResult.TotalElectricityProduction += result.TotalElectricityProduction;
                newSTotalResult.TotalProductionCost += result.TotalProductionCost;
                newSTotalResult.TotalGasConsumption += result.TotalGasConsumption;
                newSTotalResult.TotalOilConsumption += result.TotalOilConsumption;
                newSTotalResult.TotalElectricityConsumption += result.TotalElectricityConsumption;
                newSTotalResult.TotalCO2Emissions += result.TotalCO2Emissions;
            }
            STotalResult = newSTotalResult;
        }
    }
}
