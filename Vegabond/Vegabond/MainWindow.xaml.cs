using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Vegabond
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        OrderViewModel order;

        public MainWindow()
        {
            InitializeComponent();
            order = new OrderViewModel();

            DataContext = order;
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            //foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            //{
            //    MessageBox.Show(printer);
            //}

            // save text to file
            string path = PathFinder.GetPathToSaveOrder(order);
            new OrderSaver(order, path).Save();
            // call printer with the filename

            //get printer name
            var printerName = Application.Current.FindResource("printerName").ToString();
            new TextPrinter(path, printerName).Print();

        }
        private void AddText_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var content = button.Content.ToString();
            order.AddContent(content);
        }

        private void AddText_SameLine_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var content = button.Content.ToString();
            order.AddContent_SameLine(content);
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            order = new OrderViewModel();
            DataContext = order;
        }

        private void DeleteLine_Click(object sender, RoutedEventArgs e)
        {
            order.DeleteLastLine();
        }

        private void AddComment_Click(object sender, RoutedEventArgs e)
        {
            order.AddContent_SameLine(order.Comments);
            order.Comments = String.Empty;
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                AddComment_Click(this, new RoutedEventArgs());
            }
        }
    }
}
