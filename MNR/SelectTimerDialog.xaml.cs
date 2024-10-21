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
    /// Interaction logic for SelectTimerDialog.xaml
    /// </summary>
    public partial class SelectTimerDialog : Window
    {
        public TimeSpan timeSpan;
        private NotesController controller;
        public SelectTimerDialog(NotesController c, TimeSpan t)
        {
            controller = c;
            timeSpan = t;
            InitializeComponent();
        }

        private void windowLoad(object sender, EventArgs e)
        {
            cbxOct.Items.Add(0);
            cbxOct.Items.Add(1);
            cbxOct.Items.Add(2);
            cbxOct.Items.Add(3);
            cbxOct.SelectedValue = Settings.Default.Octave;
            foreach (string instrument in NotesController.instrumentNames.Values)
            {
                listInstrument.Items.Add(instrument);
            }
            listInstrument.SelectedIndex = 0;
        }

        private void btnStart(object sender, RoutedEventArgs e)
        {
            controller.changeInstrument(listInstrument.SelectedIndex);
            int timeMinutes;
            if(int.TryParse(txtBoxTimerSelector.Text, out timeMinutes))
            {
                timeSpan = TimeSpan.FromMinutes(timeMinutes);
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Try to type a valid number", "Error in parsing", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult= false;
            }
        }
    }
}
