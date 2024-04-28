namespace OptiHeatPro.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public BoilersViewModel BoilersViewModel { get; }
        public HeatingViewModel HeatingViewModel { get; }

        public MainWindowViewModel()
        {
            BoilersViewModel = new BoilersViewModel();
            HeatingViewModel = new HeatingViewModel();
        }
    }
}
