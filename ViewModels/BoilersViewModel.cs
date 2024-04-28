namespace OptiHeatPro.ViewModels
{
    public class BoilersViewModel : ViewModelBase
    {
        public Boiler Boiler1 { get; } = new Boiler("GB", 5.0, 500, 215, 1.1);
        public Boiler Boiler2 { get; } = new Boiler("OB", 4.0, 700, 265, 1.2);
        public Boiler Boiler3 { get; } = new Boiler("GM", 3.6, 2.7, 1100, 640, 1.9);
        public Boiler Boiler4 { get; } = new Boiler("EK", 8.0, -8.0, 50);
    }
}