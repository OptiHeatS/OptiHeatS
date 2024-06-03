using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OptiHeatPro.Views
{
    public partial class WinterGraphView : UserControl
    {
        public WinterGraphView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}