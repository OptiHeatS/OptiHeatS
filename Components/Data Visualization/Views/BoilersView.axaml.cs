using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OptiHeatPro.Views
{
    public partial class BoilersView : UserControl
    {
        public BoilersView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}