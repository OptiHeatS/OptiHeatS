using Avalonia.Controls;
using OptiHeatPro.ViewModels;

namespace OptiHeatPro.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
    
}