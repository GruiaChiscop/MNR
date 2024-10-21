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
    public partial class UsingKeyBoardPage : Window
    {
        private NotesController controller;
        int instrument = 0, octave=Settings.Default.Octave;
        public UsingKeyBoardPage(NotesController c)
        {
            controller = c;
            InitializeComponent();
        }

private void FRMLoaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Remember that this feature is still in early beta stages, so expect bugs. Feel to report them by sending a feedback using the send feedback button from the main menu. Happy playing!");
        }

        private void pgKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.A:
                    controller.PlayNote(NotesController.Notes.C, octave);
                    break;
                case Key.S:
                    controller.PlayNote(NotesController.Notes.D, octave);
                    break;
                case Key.D:
                    controller.PlayNote(NotesController.Notes.E, octave);
                    break;
                case Key.F:
                    controller.PlayNote(NotesController.Notes.F, octave);
                    break;
                case Key.G:
                    controller.PlayNote(NotesController.Notes.G, octave);
                    break;
                case Key.H:
                    controller.PlayNote(NotesController.Notes.A, octave);
                    break;
                case Key.J:
                    controller.PlayNote(NotesController.Notes.B, octave);
                    break;
                case Key.K:
                    controller.PlayNote(NotesController.Notes.C, octave + 1);
                    break;
                case Key.L:
                    controller.PlayNote(NotesController.Notes.D, octave + 1);
                    break;
                case Key.OemSemicolon:
                    controller.PlayNote(NotesController.Notes.E, octave + 1);
                    break;
                case Key.OemQuotes:
                    controller.PlayNote(NotesController.Notes.F, octave + 1);
                    break;
                case Key.W:
                    controller.PlayNote(NotesController.Notes.CSharp, octave);
                    break;
                case Key.E:
                    controller.PlayNote(NotesController.Notes.DSharp, octave);
                    break;
                case Key.T:
                    controller.PlayNote(NotesController.Notes.FSharp, octave);
                    break;
                case Key.Y:
                    controller.PlayNote(NotesController.Notes.GSharp, octave);
                    break;
                case Key.U:
                    controller.PlayNote(NotesController.Notes.ASharp, octave);
                    break;
                case Key.I:
                    controller.PlayNote(NotesController.Notes.CSharp, octave + 1);
                    break;
                case Key.O:
                    controller.PlayNote(NotesController.Notes.DSharp, octave + 1);
                    break;
                case Key.P:
                    controller.PlayNote(NotesController.Notes.FSharp, octave + 1);
                    break;
                case Key.OemOpenBrackets:
                    controller.PlayNote(NotesController.Notes.GSharp, octave + 1);
                    break;
                case Key.OemCloseBrackets:
                    controller.PlayNote(NotesController.Notes.ASharp, octave + 1);
                    break;
                case Key.Escape:
                    //controller.Dispose();
                    //NavigationService.GoBack();
                    this.Close();
                    break;
                case Key.F2:
                    if(instrument<=127)
                        {
                        instrument++;
                        controller.changeInstrument(instrument);
                    }
                    break;
                    case Key.F1:
                    if(instrument>=0)
                    {
                        instrument--;
                        controller.changeInstrument(instrument);
                    }
                    break;
                case Key.X:
                    if (octave < 5) octave++;
                    break;
                case Key.Z:
                    if(octave>0) octave--;
                    break;
            }
        }
    }
}
