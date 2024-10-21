using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : Page
    {
        private TextToMorse msController;
        public WelcomePage()
        {
            InitializeComponent();
            DataContext = Settings.Default;
            cbxOct.Items.Add(0);
            cbxOct.Items.Add(1);
            cbxOct.Items.Add(2);
            cbxOct.Items.Add(3);
            txtBoxName.Focus();
            //c = new NotesController();
            msController = new TextToMorse();
        }

        private async void btnFinish(object sender, RoutedEventArgs e)
        {
            if(Settings.Default.Name==string.Empty || Settings.Default.Name.Length > 20 || Settings.Default.Name.Length < 3)
            {
                MessageBox.Show("Sorry, but you must provide a valid name to continue", "Pardon", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
                        Settings.Default.IsConfigured = true;
            Settings.Default.Save();
            //new TextToMorse().Transmit(Settings.Default.Name);
            MessageBox.Show($"Hello, {Settings.Default.Name}! Welcome!");
            msController.Transmit(Settings.Default.Name);
            await Task.Delay(500);
            NavigationService.Navigate(new MainPage());
            
        }
    }
}
