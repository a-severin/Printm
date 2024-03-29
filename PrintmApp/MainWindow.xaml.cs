using System;
using System.Collections.Generic;
using System.IO;
using System.Printing;
using System.Windows;
using System.Drawing.Printing;
using System.Diagnostics;

namespace PrintmApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            PathTextBox.Text = @"C:\tmp\test.pdf";

            var list = new List<string>();

            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                var printer = PrinterSettings.InstalledPrinters[i];
                list.Add(printer);
            }
            PrintQueuesListBox.ItemsSource = list;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // https://gist.github.com/a-severin/c6443a0eace68aa1bf3b37884562a0b0

            // https://www.sumatrapdfreader.org/free-pdf-reader

            if (PrintQueuesListBox.SelectedItem == null)
            {
                MessageBox.Show("Select Printer");
                return;
            }

            var path = PathTextBox.Text ?? string.Empty;
            if (!File.Exists(path))
            {
                MessageBox.Show("Enter path to existing document");
                return;
            }

            var printer = (string)PrintQueuesListBox.SelectedItem;
            Process print = new Process();
            print.StartInfo.FileName = @"C:\sumatra\SumatraPDF.exe";
            print.StartInfo.UseShellExecute = true;
            print.StartInfo.CreateNoWindow = true;
            print.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            print.StartInfo.Arguments = "-print-to \"" + printer + "\" -exit-when-done \"" + path + "\"";
            print.Start();

            print.WaitForExit();
            print.Dispose();
        }
    }
}
