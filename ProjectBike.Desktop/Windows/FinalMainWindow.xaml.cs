using ProjectBike.Desktop.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace ProjectBike.Desktop.Windows
{
    public partial class FinalMainWindow : Window
    {
        public FinalMainWindow()
        {
            InitializeComponent();
        }

        private void LogoButton_Click(object sender, RoutedEventArgs e)
        {
            // Indeks 0 odpowiada za StartView
            MenuListBox.SelectedIndex = 0;
        }
        private void ColorZone_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove(); // Wbudowana funkcja WPF do przenoszenia okna
            }
        }
    }
}