using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
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
    public void MinimizeWindow(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    public void CloseWindow(object sender, RoutedEventArgs e)
    {
        Close();
    }
    public void TitleBar_OnPointerPressed(object sender, PointerPressedEventArgs e)
{
    this.BeginMoveDrag(e);
}
    private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
}