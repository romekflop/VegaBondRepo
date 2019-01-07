using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Vegabond
{
    internal class TextPrinter
    {
        private readonly Font printFont;
        private StreamReader streamToPrint;
        private readonly string filePath;
        private readonly string printerName;

        public TextPrinter(string filePath, string printerName, Font printFont = null)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath));

            if (printFont == null)
                this.printFont = new Font("Arial", 10);

            this.filePath = filePath;

            this.printerName = printerName;

        }
        public void Print()
        {
            try
            {
                streamToPrint = new StreamReader(filePath);
                try
                {
                    PrintDocument pd = new PrintDocument();
                    pd.PrintPage += new PrintPageEventHandler(Pd_PrintPage);

                    pd.PrinterSettings.PrinterName = printerName;


                    //pd.PrinterSettings.PrintFileName =
                    //    filePath.TrimEnd(".txt".ToCharArray()) + ".pdf";
                    //pd.PrinterSettings.PrintToFile = true;

                    if (!pd.PrinterSettings.IsValid)
                        MessageBox.Show("print settings are not vaild");
                    else
                    {
                        pd.Print();
                    }
                }
                finally
                {
                    streamToPrint.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // The PrintPage event is raised for each page to be printed.
        private void Pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            String line = null;

            // Calculate the number of lines per page.
            linesPerPage = ev.MarginBounds.Height /
               printFont.GetHeight(ev.Graphics);

            // Iterate over the file, printing each line.
            while (count < linesPerPage &&
               ((line = streamToPrint.ReadLine()) != null))
            {
                yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, Brushes.Black,
                   leftMargin, yPos, new StringFormat());
                count++;
            }

            // If more lines exist, print another page.
            if (line != null)
                ev.HasMorePages = true;
            else
                ev.HasMorePages = false;
        }
    }
}
