using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OptiHeatPro.Views
{
    public partial class SummerGraphView : UserControl
    {
        public SummerGraphView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}