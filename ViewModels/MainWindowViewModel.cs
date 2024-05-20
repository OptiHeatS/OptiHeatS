namespace OptiHeatPro.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public BoilersViewModel BoilersViewModel { get; }
        public HeatingViewModel HeatingViewModel { get; }
        public GraphViewModel GraphViewModel{ get; }
        public OptimizerViewModel OptimizerViewModel{ get; }

        public MainWindowViewModel()
        {
            BoilersViewModel = new BoilersViewModel();
            HeatingViewModel = new HeatingViewModel();
            GraphViewModel = new GraphViewModel();
            OptimizerViewModel = new OptimizerViewModel();
        }
    }
}
