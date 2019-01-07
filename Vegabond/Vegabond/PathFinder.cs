using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vegabond
{
    public static class PathFinder
    {
        public static string GetPathToSaveOrder(OrderViewModel order)
        {
            var directoryPath = CreateDirectory();

            string fileName = string.Format(
                "order_for_{0}_at_{1}.txt",
               string.IsNullOrWhiteSpace(order.CustomerName) ? "no_name" : order.CustomerName,
                order.Time.ToFileTimeString());
            return $"{directoryPath}\\{fileName}";
        }

        private static string CreateDirectory()
        {
            string currentPath = Directory.GetCurrentDirectory();
            string path = $"{currentPath}\\{DateTime.Now.ToFileDateString()}";
            Directory.CreateDirectory(path);

            return path;
        }

        public static string ToFileDateString(this DateTime source)
        {
            return source.ToString("yyyy-dd-M");
        }

        public static string ToFileTimeString(this DateTime source)
        {
            return source.ToString("HH-mm-ss");
        }
    }
}
