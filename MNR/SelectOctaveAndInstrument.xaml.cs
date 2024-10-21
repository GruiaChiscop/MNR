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
using NAudio.Midi;
namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for SelectOctaveAndInstrument.xaml
    /// </summary>
    public partial class SelectOctaveAndInstrument : Window
    {
        private NotesController controller;
        public SelectOctaveAndInstrument(NotesController c)
        {
            controller = c;
            InitializeComponent();
            DataContext = Settings.Default;
        }

        private void windowLoad(object sender, EventArgs e)
        {
            cbxOct.Items.Add(0);
            cbxOct.Items.Add(1);
            cbxOct.Items.Add(2);
            cbxOct.Items.Add(3);
            cbxOct.SelectedValue = Settings.Default.Octave;
            foreach(string instrument in NotesController.instrumentNames.Values)
            {
                listInstrument.Items.Add(instrument);
            }
            listInstrument.SelectedIndex = 0;
        }
        private void btnStart(object sender, RoutedEventArgs e)
        {
            controller.changeInstrument(listInstrument.SelectedIndex);
            DialogResult = true;
        }
    }
}
