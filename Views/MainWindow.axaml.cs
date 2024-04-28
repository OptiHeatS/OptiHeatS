using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using OptiHeatPro.ViewModels;

namespace OptiHeatPro.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
        
    }
    private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
}