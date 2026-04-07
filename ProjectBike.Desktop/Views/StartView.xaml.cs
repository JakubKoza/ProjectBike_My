
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Globalization;

namespace ProjectBike.Desktop.Views
{
    public partial class StartView : UserControl
    {

        public StartView()
        {
            InitializeComponent();

            Loaded += StartView_Loaded;
        }

        private void StartView_Loaded(object sender, RoutedEventArgs e)
        {
            // Odśwież od razu
            UpdateTime(null, null);
        }

        private void UpdateTime(object? sender, EventArgs? e)
        {
            var culture = new CultureInfo("pl-PL");
            TimeTextBlock.Text = DateTime.Now.ToString("HH:mm");
            DateTextBlock.Text = DateTime.Now.ToString("dddd, d MMMM yyyy", culture);
        }
    }
}
