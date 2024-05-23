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
        public double TotalElectricityConsumption { get; set; }
        public decimal TotalProductionCost { get; set; }
        public double TotalGasConsumption { get; set; }
        public double TotalOilConsumption { get; set; }
        public double TotalCO2Emissions { get; set; }
    }
    public class Optimizer
    {
        public List<Result> Optimize(List<DataEntry> heatingData, double co2ReductionPercentage)
        {
            List<Result> results = new List<Result>();
            
            for(int i = 0; i < heatingData.Count; i++)
            {
                BoilerManager boilerManager = new BoilerManager();
                List<Boiler> boilers = boilerManager.Boilers;

                double gasBoilerOutput = 0;
                double oilBoilerOutput = 0;
                double gasMotorOutput = 0;
                double electricBoilerOutput = 0;
                double totalElectricityProduction = 0;
                double totalElectricityConsumption = 0;
                decimal totalProductionCost = 0;
                double totalGasConsumption = 0;
                double totalOilConsumption = 0;
                double totalCO2Emissions = 0;

                UpdateProductionCosts(boilers, heatingData[i]);

                boilers = boilers.OrderBy(b => b.ProductionCosts).ThenBy(b => b.CO2Emissions).ToList(); // Boilers are sorted by production cost and followed by CO2 emissions 
                
                double maxCO2Allowed = CalculateTotalEmissions(boilers, heatingData[i].HeatDemand) * (1 - co2ReductionPercentage); // Calculates how much CO2 is allowed based on the user selected CO2 reduction percentage
                
                double remainingHeatDemand = heatingData[i].HeatDemand;

                foreach (var boiler in boilers)
                {
                    if (remainingHeatDemand <= 0)
                        break;

                    double availableHeat = Math.Min(remainingHeatDemand, boiler.MaxHeat);
                    double allocatedHeat = AllocateHeat(boiler, availableHeat, totalCO2Emissions, maxCO2Allowed);

                    double electricityRatio = 0;
                    decimal boilerCost = 0;

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
                        boilerCost = (decimal)allocatedHeat * boiler.ProductionCosts;
                        totalElectricityProduction += electricityRatio * allocatedHeat;
                        gasMotorOutput += allocatedHeat;
                        totalGasConsumption += boiler.GasConsumption * allocatedHeat;
                    }
                    else if (boiler.Name == "EK")
                    {
                        boilerCost = (decimal)allocatedHeat * boiler.ProductionCosts;
                        electricBoilerOutput += allocatedHeat;
                        totalElectricityConsumption = electricBoilerOutput;

                    }

                    totalCO2Emissions += allocatedHeat * boiler.CO2Emissions;
                    totalProductionCost += boilerCost;
                    remainingHeatDemand -= allocatedHeat;
                }

                results.Add(new Result
                {
                    GasBoilerOutput = gasBoilerOutput,
                    OilBoilerOutput = oilBoilerOutput,
                    GasMotorOutput = gasMotorOutput,
                    ElectricBoilerOutput = electricBoilerOutput,
                    TotalElectricityProduction = totalElectricityProduction,
                    TotalElectricityConsumption = totalElectricityConsumption,
                    TotalProductionCost = totalProductionCost,
                    TotalGasConsumption = totalGasConsumption,
                    TotalOilConsumption = totalOilConsumption,
                    TotalCO2Emissions = totalCO2Emissions
                });
            }
        return results;
        }
        private void UpdateProductionCosts(List<Boiler> boilers, DataEntry heatingData) // Method which updates the production cost of boilers that produce/consume electricity based on the current electricity price
        {
            foreach (var boiler in boilers)
            {
                if (boiler.MaxElectricity.HasValue)
                {
                    if (boiler.MaxElectricity < 0)
                    {
                        boiler.ProductionCosts += heatingData.ElectricityPrice;
                    }
                    if (boiler.MaxElectricity > 0)
                    {
                        boiler.ProductionCosts -= heatingData.ElectricityPrice * (decimal)boiler.MaxElectricity / (decimal)boiler.MaxHeat;
                    }
                }
            }
        }
        private double CalculateTotalEmissions(List<Boiler> boilers, double heatDemand) // Method which find the total amount of CO2 emissions generated by the cheapest grid
        {
            double totalEmissions = 0;
            foreach (var boiler in boilers)
                {
                    if(heatDemand <= 0)  
                        break;

                    double allocatedHeat = Math.Min(heatDemand, boiler.MaxHeat);
                    heatDemand -= allocatedHeat;
                    totalEmissions += allocatedHeat * boiler.CO2Emissions;
                }
            return totalEmissions;
        }
        private double AllocateHeat(Boiler boiler, double availableHeat, double totalCO2Emissions, double maxCO2Allowed) // Method which finds the maximum possible heat that a boiler can produce and stay in the allowed CO2 emission range
        {
            double allocatedHeat = 0;

            // Check if the boiler emits CO2
            if (boiler.CO2Emissions > 0)
            {
                // Check if allocating the available heat from this boiler exceeds the maximum allowed CO2 emissions
                if ((totalCO2Emissions + availableHeat * boiler.CO2Emissions) > maxCO2Allowed)
                {
                    // If allocating the available heat exceeds the CO2 limit, calculate the heat that can be allocated
                    // to stay within the CO2 limit
                    allocatedHeat = (maxCO2Allowed - totalCO2Emissions) / boiler.CO2Emissions;
                }
                else
                {
                    // Allocate all available heat from this boiler
                    allocatedHeat = availableHeat;
                }
            }
            else
            {
                // If the boiler emits no CO2, allocate all available heat regardless of the CO2 limit
                allocatedHeat = availableHeat;
            }

            return allocatedHeat;
        }

    }
}