namespace OptiHeatPro
{

  public class Boiler 
  {
    public string Name { get; set; }
    public double MaxHeat { get; set; }
    public double? MaxElectricity { get; set; } // Nullablell
    public decimal ProductionCosts { get; set; }
    public double CO2Emissions { get; set; }
    public double GasConsumption { get; set; }

    public Boiler(string name, double maxHeat, decimal productionCosts, double co2Emissions, double gasConsumption) 
    {
        Name = name;
        MaxHeat = maxHeat;
        ProductionCosts = productionCosts;
        CO2Emissions = co2Emissions;
        GasConsumption = gasConsumption;
    }

    public Boiler(string name, double maxHeat, double maxElectricity, decimal productionCosts, double co2Emissions, double gasConsumption) 
    {
        Name = name;
        MaxHeat = maxHeat;
        MaxElectricity = maxElectricity;
        ProductionCosts = productionCosts;
        CO2Emissions = co2Emissions;
        GasConsumption = gasConsumption;
    }

    public Boiler(string name, double maxHeat, double maxElectricity, decimal productionCosts) 
    {
        Name = name;
        MaxHeat = maxHeat;
        MaxElectricity = maxElectricity;
        ProductionCosts = productionCosts;
    }
  }
  public class BoilerManager
  {
    public List<Boiler> Boilers { get; }

    public BoilerManager()
    {
      Boilers = new List<Boiler>
      {
        new Boiler("GB", 5, 500, 215, 1.1),
        new Boiler("OB", 4, 700, 265, 1.2),
        new Boiler("GM", 3.6, 2.7, 1100, 640, 1.9),
        new Boiler("EK", 8, -8, 50)
      };
    }
  }
}