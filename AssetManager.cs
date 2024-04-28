namespace OptiHeatPro
{

  public class Boiler 
  {
    public string Name { get; set; }
    public double MaxHeat { get; set; }
    public double? MaxElectricity { get; set; } // Nullablell
    public double ProductionCosts { get; set; }
    public double CO2Emissions { get; set; }
    public double GasConsumption { get; set; }

    public Boiler(string name, double maxHeat, double productionCosts, double co2Emissions, double gasConsumption) 
    {
        Name = name;
        MaxHeat = maxHeat;
        ProductionCosts = productionCosts;
        CO2Emissions = co2Emissions;
        GasConsumption = gasConsumption;
    }

    public Boiler(string name, double maxHeat, double maxElectricity, double productionCosts, double co2Emissions, double gasConsumption) 
    {
        Name = name;
        MaxHeat = maxHeat;
        MaxElectricity = maxElectricity;
        ProductionCosts = productionCosts;
        CO2Emissions = co2Emissions;
        GasConsumption = gasConsumption;
    }

    public Boiler(string name, double maxHeat, double maxElectricity, double productionCosts) 
    {
        Name = name;
        MaxHeat = maxHeat;
        MaxElectricity = maxElectricity;
        ProductionCosts = productionCosts;
    }
  }
}