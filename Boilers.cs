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

  public class Print 
  {
    public static void DisplayBoilerInfo(Boiler boiler) 
    {
        Console.WriteLine("Name: " + boiler.Name);
        Console.WriteLine("Max Heat: " + boiler.MaxHeat);
        if (boiler.MaxElectricity != null) 
        {
            Console.WriteLine("Max Electricity: " + boiler.MaxElectricity);
        }
        Console.WriteLine("Production Costs: " + boiler.ProductionCosts);
        if (boiler.CO2Emissions != 0) 
        {
            Console.WriteLine("CO2 Emissions: " + boiler.CO2Emissions);
        }
        if (boiler.GasConsumption != 0) 
        {
            Console.WriteLine("Gas Consumption: " + boiler.GasConsumption);
        }
        Console.WriteLine();
    }
  }
}