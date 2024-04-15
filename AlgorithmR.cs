namespace Algorithm2
{
    public class Boiler
    {
        public string name;
        public int type;
    }
    public class BoilerGas
    {
        //Cost during these Hours
        public int offHours;
        public int fullHours;
        public int peakHours;
    }
    public class BoilerEl
    {
        public string cost;
    }
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hi");

            Console.WriteLine("What is your boiler");
            Boiler boiler1 = new Boiler();
            boiler1.name = Console.ReadLine();
            Console.WriteLine($"Your boiler is: {boiler1.name}");
            Console.WriteLine("Please choose your type of Boiler: Type 1 for Gas or Type 2 for Electric");
            boiler1.type = int.Parse(Console.ReadLine());
            if(boiler1.type == 1){
                //BoilerGas boilerGas1 = new BoilerGas();
                //Console.WriteLine("test1");
                GasBoiler();
            }
            if(boiler1.type == 2){
                BoilerEl boilerEl1 = new BoilerEl();
                //Console.WriteLine("test2");
            }
        }
        public static void GasBoiler(){
            BoilerGas boilerGas1 = new BoilerGas();
            Console.WriteLine("What is the cost for gas during off hours");
            boilerGas1.offHours = int.Parse(Console.ReadLine());
            Console.WriteLine("What is the cost for gas during full hours");
            boilerGas1.fullHours = int.Parse(Console.ReadLine());
            Console.WriteLine("What is the cost for gas during peak hours");
            boilerGas1.peakHours = int.Parse(Console.ReadLine());

            Console.WriteLine($"{boilerGas1.offHours}, {boilerGas1.fullHours}, {boilerGas1.peakHours}");
        }
    }
}