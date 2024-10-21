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
using System.Diagnostics;
using Microsoft.Win32;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Principal;
using System.Net;
using System.Globalization;
namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MailToDialog.xaml
    /// </summary>
    public partial class MailToDialog : Window
    {
        public string MailTitle { get; set; }
        public string Message { get; set; }
        public MailToDialog()
        {
            InitializeComponent();
            txtBoxTitle.Focus();
            MailTitle = String.Empty;
            Message = string.Empty;
            DataContext = this;
        }

        private void windowLoad(object sender, EventArgs e)
        {
            string[] feedTypes = new string[] { "Report an issue", "Suggest new features" };
            cbxType.ItemsSource= feedTypes;
            cbxType.SelectedIndex = 0;
        }

        private void btnSend(object sender, RoutedEventArgs e)
        {
            string title;
            string message;
            string windVersion= Environment.OSVersion.ToString();
            bool is64 = Environment.Is64BitOperatingSystem;
            bool isThisAppRunAsAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
            string compName = Environment.MachineName;
            string acountName = WindowsIdentity.GetCurrent().Name;
            string hostName = Dns.GetHostName();
            var entry = Dns.GetHostEntry(hostName);
            string IPs="";
            foreach (var ip in entry.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    IPs += ip.ToString() + ", ";
                }
            }
                string scriptPath = Environment.CurrentDirectory;
                int numProcessesOpen = Process.GetProcesses().Length;
            bool useScreenReader = Process.GetProcessesByName("nvda.exe").Length!=0 || Process.GetProcessesByName("jfw.exe").Length!=0;
            string language = CultureInfo.CurrentUICulture.Name;
            bool thisProcess64 = Environment.Is64BitProcess;
            string info = $"Computer name: {compName}, windows name: {windVersion}, host Name: {hostName}, IP address: {IPs}, Use screen reader: {useScreenReader.ToString()}, is this app run as admin: {isThisAppRunAsAdmin.ToString()}, is this process in 64 bit: {is64.ToString()}, language: {language}, number of processes: {numProcessesOpen}, script path: {scriptPath}, Account name: {acountName}, this process as 64 bit: {thisProcess64.ToString()};";
            if(cbxType.SelectedIndex==0 && (checkboxSendingData.IsChecked.HasValue && !checkboxSendingData.IsChecked.Value))
            {
                MessageBox.Show("Sorry, but for reporting an issue you must agree with sending diagnostics to me", "Checkbox unchecked", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            title = cbxType.SelectedIndex == 0 ? "Issue: " : "Suggestion: " + Title;
            if(checkboxSendingData.IsChecked.HasValue && checkboxSendingData.IsChecked.Value)
            {
                message = string.Concat(Message, "\n\n\n", "aditional information", "\n\n\n", info);
            }
            else
            {
                message = Message;
            }

            string mailtourl = $"mailto:chiscopgruia@gmail.com?subject={Uri.EscapeUriString(title)}&body={Uri.EscapeUriString(message)}";
            Process.Start(mailtourl);
            DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
