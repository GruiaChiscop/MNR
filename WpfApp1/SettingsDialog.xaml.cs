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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for SettingsDialog.xaml
    /// </summary>
    public partial class SettingsDialog : Window
    {
        private int octave = Settings.Default.Octave;
        private string name = Settings.Default.Name;
        private bool useLiveRegions = Settings.Default.AnnounceLiveRegions;

        public SettingsDialog()
        {
            InitializeComponent();
            DataContext = Settings.Default;
        }

        private void windowLoaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i <= 3; i++) octaveCBX.Items.Add(i);
        }

        private void scoreClearBTN(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete your score", "Score deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                Settings.Default.Guesses = 0;
                Settings.Default.Failures = 0;
        }
    }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to reset your settings to the default?", "App data deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                Settings.Default.Guesses = 0;
                Settings.Default.Failures = 0;
                Settings.Default.AnnounceLiveRegions = true;
                Settings.Default.IsConfigured = false;
                Settings.Default.Octave = 0;
                Settings.Default.Name = string.Empty;
                MessageBox.Show("All dat have been successfully deleted.", "Finished", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                Settings.Default.Save();
                //App.Current.MainWindow.Close();
                
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Settings.Default.Name = name;
            Settings.Default.Octave = octave;
            Settings.Default.AnnounceLiveRegions = useLiveRegions;
            Settings.Default.Save();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Settings.Default.Save();
            DialogResult = true;
        }

        private void windowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //App.Current.MainWindow.Show();
        }
        }
    }
