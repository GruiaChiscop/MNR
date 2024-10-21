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
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;

namespace WpfApp1
{
    public partial class MainPage : Page
    {
        private NotesController controller;
        private TimeSpan timeSpan;
        public MainPage()
        {
            controller = new NotesController();
            timeSpan = new TimeSpan();
            InitializeComponent();
            DataContext = Settings.Default;
            BtnGuessNote.Focus();
        }

        private void btnSettings(object sender, RoutedEventArgs e)
        {
            var f = new SettingsDialog();
            App.Current.MainWindow.Hide();
            f.ShowDialog();
            App.Current.MainWindow.Show();
        }

        private void btnTimeChallenge(object sender, RoutedEventArgs e)
        {
            var f = new SelectTimerDialog(controller, timeSpan);
            if (Settings.Default.Name == string.Empty) MessageBox.Show("You haven't set a name yet. Go to the settings to set one first, and try again", "Your name not found", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
            App.Current.MainWindow.Hide();
            var result = f.ShowDialog();
            if (result.HasValue && result.Value)
            {
                timeSpan = f.timeSpan;
                App.Current.MainWindow.Show();
                NavigationService.Navigate(new TimerChallengePage(controller, timeSpan));
            }
            App.Current.MainWindow.Show();
        }
        }

        private void btnGiveFeedback(object sender, RoutedEventArgs e)
        {
            var f = new MailToDialog();
        if (Settings.Default.Name == string.Empty) MessageBox.Show("You haven't set a name yet. Go to the settings to set one first, and try again", "Your name not found", MessageBoxButton.OK, MessageBoxImage.Warning);
        else
        {
            App.Current.MainWindow.Hide();
            f.ShowDialog();
            App.Current.MainWindow.Show();
        }
        }

        private void BtnViewChangeLog(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Da, iată-ne ajunși și la a doua iterație a aplicației, și ultima a acesteia. Aceasta introduce suportul pentru regiuni live, cât și corectează anumite greșeli de ortografie. Acum butoanele settings și donate funcționează. S-au adăugat anumite mesaje redate în morse, ca pentru a face o introducere a proiectului principal morse hub. Descurajez utilizarea opțiunii de a cânta cu tastatura, dar nu am eliminat-o din meniu. Bug-uri: Singurul bug pe care nu l-am putut rezolva este cel cu regiunile live, ele fiind instabile. Dacă întâmpinați probleme, le puteți opri din setările aplicației, iar rezultatele vor fi afișate într-un dialog box tradițional. Mă gândisem să adopt utilizarea tolk, dar nu sunt încă pe deplin convins, pentru că el nu suportă cititorul ZDSr plus și alte mici incoveniente.");
        }

        private void btnSingWithKB(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new UsingKeyBoardPage(controller));
            var f = new UsingKeyBoardPage(controller);
            App.Current.MainWindow.Hide();
            f.ShowDialog();
            App.Current.MainWindow.Show();
        }

        private void btnGuessNote(object sender, RoutedEventArgs e)
        {
            var f = new SelectOctaveAndInstrument(controller);
        if (Settings.Default.Name == string.Empty) MessageBox.Show("You haven't set a name yet. Go to the settings to set one first, and try again", "Your name not found", MessageBoxButton.OK, MessageBoxImage.Warning);
        else
        {
            App.Current.MainWindow.Hide();
            var result = f.ShowDialog();
            if (result.HasValue && result.Value)
            {
                App.Current.MainWindow.Show();
                NavigationService.Navigate(new NoteGuesser(controller));
            }
            App.Current.MainWindow.Show();
        }
        }

        private void btnDonate(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Îți mulțumesc pentru că i-ai dat aplicației mele o șansă și că te interesează și acest buton de donații. Mă poți susține donându-mi pe paypal la adresa chiscopgruia@gmail.com!");
        }
    }
}
