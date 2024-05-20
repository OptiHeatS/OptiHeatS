using System;
using System.Collections.Generic;
using System.IO;

namespace OptiHeatPro
{
    public class Result
    {
        public double GasBoilerOutput { get; set; }
        public double OilBoilerOutput { get; set; }
        public double GasMotorOutput { get; set; }
        public double ElectricBoilerOutput { get; set; }
        public double TotalElectricityProduction { get; set; }
        public decimal TotalProductionCost { get; set; }
        public double TotalGasConsumption { get; set; }
        public double TotalOilConsumption { get; set; }
        public double TotalCO2Emissions { get; set; }
    }
    public class Optimizer
    {
        public List<Result> Optimize(List<DataEntry> heatingData)
        {
            List<Result> results = new List<Result>();
            
            for(int i = 0; i < heatingData.Count; i++)
            {
                BoilerManager boilerManager = new BoilerManager();
                List<Boiler> boilers = boilerManager.Boilers;
                
                foreach (var boiler in boilers)
                {
                    if (boiler.MaxElectricity.HasValue)
                    {
                        if (boiler.MaxElectricity < 0)
                        {
                            boiler.ProductionCosts += heatingData[i].ElectricityPrice;
                        }
                        if (boiler.MaxElectricity > 0)
                        {
                            boiler.ProductionCosts -= heatingData[i].ElectricityPrice;
                        }        
                    }
                }

                boilers = boilers.OrderBy(b => b.ProductionCosts).ThenBy(b => b.CO2Emissions).ToList();

                double remainingHeatDemand = heatingData[i].HeatDemand;

                double gasBoilerOutput = 0;
                double oilBoilerOutput = 0;
                double gasMotorOutput = 0;
                double electricBoilerOutput = 0;
                double totalElectricityProduction = 0;
                decimal totalProductionCost = 0;
                double totalGasConsumption = 0;
                double totalOilConsumption = 0;
                double totalCO2Emissions = 0;

                foreach (var boiler in boilers)
                {
                    if(remainingHeatDemand <= 0)  
                        break;

                    double allocatedHeat = Math.Min(remainingHeatDemand, boiler.MaxHeat);
                    remainingHeatDemand -= allocatedHeat;

                    decimal boilerCost = 0;
                    double electricityRatio;

                    if (boiler.Name == "GB")
                    {
                        boilerCost = (decimal)allocatedHeat * boiler.ProductionCosts;
                        gasBoilerOutput += allocatedHeat;
                        totalGasConsumption += boiler.GasConsumption * allocatedHeat;
                    }
                    else if (boiler.Name == "OB")
                    {
                        boilerCost = (decimal)allocatedHeat * boiler.ProductionCosts;
                        oilBoilerOutput += allocatedHeat;
                        totalOilConsumption += boiler.GasConsumption * allocatedHeat;
                    }
                    else if (boiler.Name == "GM")
                    {
                        electricityRatio = boiler.MaxElectricity.Value / boiler.MaxHeat;
                        boilerCost = (decimal)allocatedHeat * (decimal)electricityRatio * boiler.ProductionCosts;
                        totalElectricityProduction += electricityRatio * allocatedHeat;
                        gasMotorOutput += allocatedHeat;
                        totalGasConsumption += boiler.GasConsumption * allocatedHeat;
                    }
                    else if (boiler.Name == "EK")
                    {
                        boilerCost = (decimal)allocatedHeat * boiler.ProductionCosts;
                        electricBoilerOutput += allocatedHeat;

                    }

                    totalCO2Emissions += allocatedHeat * boiler.CO2Emissions;
                    totalProductionCost += boilerCost;
                }

                results.Add(new Result
                {
                    GasBoilerOutput = gasBoilerOutput,
                    OilBoilerOutput = oilBoilerOutput,
                    GasMotorOutput = gasMotorOutput,
                    ElectricBoilerOutput = electricBoilerOutput,
                    TotalElectricityProduction = totalElectricityProduction,
                    TotalProductionCost = totalProductionCost,
                    TotalGasConsumption = totalGasConsumption,
                    TotalOilConsumption = totalOilConsumption,
                    TotalCO2Emissions = totalCO2Emissions
                });
            }
        return results;
        }
    }
}