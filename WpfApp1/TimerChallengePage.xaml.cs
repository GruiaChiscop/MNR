using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static WpfApp1.NotesController;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for TimerChallengePage.xaml
    /// </summary>
    public partial class TimerChallengePage : Page
    {
        private NotesController controller;
        private DispatcherTimer timer;
        private TimeSpan timeSpan;
        private TimeSpan elapsed = TimeSpan.Zero;
        private int note;
        private int guesses;
        private int failures;
        public TimerChallengePage(NotesController c, TimeSpan t) 
        {
            controller = c;
            timer = new DispatcherTimer();
            timeSpan = t;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            InitializeComponent();
            timer.Start();
        }
        private void pgLoad(object sender, RoutedEventArgs e)
        {
            var notesList = new List<Tuple<string, int>>();
            foreach (NotesController.Notes note in Enum.GetValues(typeof(NotesController.Notes)))
            {
                notesList.Add(Tuple.Create(note.ToString(), (int)note));
            }
            listBoxNotes.ItemsSource = notesList;
            listBoxNotes.DisplayMemberPath = "Item1";
            PlayNote();
        }

        private async void btnSubmit(object sender, RoutedEventArgs e)
        {
            var selectedNote = (Tuple<string, int>)listBoxNotes.SelectedItem;

            if (selectedNote.Item2 == note)
            {
                guesses++;
                if(Settings.Default.PlayMorseMessages) controller.MSController.Transmit("k");
                Announce("Guessed. Nice!");
            }
            else
            {
                failures++;
                if(Settings.Default.PlayMorseMessages) controller.MSController.Transmit("0");
                Announce("Wrong! The right answer was "+(NotesController.Notes)note+".");
            }
            await Task.Delay(1000);
            PlayNote();
        }

        private void PlayNote(bool regenerate=true)
        {
            listBoxNotes.SelectedIndex = 0;
            if(regenerate) note = new Random().Next(60, 72) + Settings.Default.Octave * 12;
            controller.PlayNote(note);
        }
private void replayBTN(object sender, RoutedEventArgs e)
        {
            PlayNote(false);
        }
        private void btnExit(object sender, RoutedEventArgs e)
        {
            string mesage = "you got " + guesses + " notes correct and " + failures + " wrong. ";
            if (guesses < Settings.Default.Guesses) mesage += "Worse than last time when you played";
            else mesage += "you're becoming better and better!";
            
            MessageBox.Show(mesage, "Your score", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            controller.MSController.Transmit("" + guesses + "/" + failures);
            Settings.Default.Guesses += guesses;
            Settings.Default.Failures += failures;
            Settings.Default.Save();
            NavigationService.GoBack();
        }
        private void Announce(string text)
        {
            if (Settings.Default.AnnounceLiveRegions)
            {
                var peer = UIElementAutomationPeer.FromElement(answerLBL);
                answerLBL.Text = text;
                if (peer != null) peer.RaiseAutomationEvent(AutomationEvents.LiveRegionChanged);
                else
                {
                    peer = UIElementAutomationPeer.CreatePeerForElement(answerLBL);
                    answerLBL.Text = text;
                    peer.RaiseAutomationEvent(AutomationEvents.LiveRegionChanged);
                }
            }
            else
            {
                MessageBox.Show(text);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            elapsed += TimeSpan.FromSeconds(1);
            if(elapsed>=timeSpan)
            {
                timer.Stop();
                MessageBox.Show("You have finished. The time is over", "Time is over", MessageBoxButton.OK, MessageBoxImage.Warning);
                string mesage = "you got " + guesses + " notes correct and " + failures + " wrong. ";
                if (guesses < Settings.Default.Guesses) mesage += "Worse than last time when you played";
                else mesage += "you're becoming better and better!";
                controller.MSController.Transmit("" + guesses + " " + failures);
                MessageBox.Show(mesage, "Your score", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                Settings.Default.Guesses += guesses;
                Settings.Default.Failures += failures;
                Settings.Default.Save();
                NavigationService.GoBack();
            }
        }
    }
}
