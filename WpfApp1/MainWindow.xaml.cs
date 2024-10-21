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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TextToMorse msController;
        public MainWindow()
        {
            msController = new TextToMorse();
            InitializeComponent();
        }

        private void frmLoad(object sender, RoutedEventArgs e)
        {
            msController.Transmit("yo3gru");
            if(!Settings.Default.IsConfigured)
            {
                MessageBox.Show("Îți mulțumesc că ai descărcat și instalat această aplicație! Mi-ar plăcea foarte mult dacă mi-ai trimite și un feedback la adresa chiscopgruia@gmail.com, sau din meniul principal, give feedback. REține că dezvoltarea acestor aplicații ia timp așa că o donație m-ar ajuta să continui dezvoltarea acestui proiect. Le poți trimite pe paypal la aceeași adresă sau din meniul principal, donate. Încă o dată, îți mulțumesc pentru că ai ales să-mi folosești aplicația!", "Message from developer", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                MessageBox.Show("Da, iată-ne ajunși și la a doua iterație a aplicației, și ultima a acesteia. Aceasta introduce suportul pentru regiuni live, cât și corectează anumite greșeli de ortografie. Acum butoanele settings și donate funcționează. S-au adăugat anumite mesaje redate în morse, ca pentru a face o introducere a proiectului principal morse hub. Descurajez utilizarea opțiunii de a cânta cu tastatura, dar nu am eliminat-o din meniu. Bug-uri: Singurul bug pe care nu l-am putut rezolva este cel cu regiunile live, ele fiind instabile. Dacă întâmpinați probleme, le puteți opri din setările aplicației, iar rezultatele vor fi afișate într-un dialog box tradițional. Mă gândisem să adopt utilizarea tolk, dar nu sunt încă pe deplin convins, pentru că el nu suportă cititorul ZDSr plus și alte mici incoveniente.");
                frm.Navigate(new WelcomePage());
                
            }
            else
            {
                frm.Navigate(new MainPage());

            }
        }

        private async void WindowClose(object sender, EventArgs e)
        {
            msController.Transmit("73");
                System.Threading.Thread.Sleep(2000);
        }
    }
}
