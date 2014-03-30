using System.Windows.Controls;
using SmartCalculator.ViewModel;
using SmartCalculator.Model;
namespace SmartCalculator.View
{
    public partial class ScreenView : UserControl
    {
        public ScreenView()
        {
            InitializeComponent();
        }

        private void listCal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.ViewModel.CurrentPos = listCal.SelectedIndex;
        }
    }
}