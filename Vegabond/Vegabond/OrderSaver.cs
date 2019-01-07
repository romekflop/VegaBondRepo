using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vegabond
{
    internal class OrderSaver
    {
        readonly OrderViewModel order;
        private readonly string path;

        public OrderSaver(OrderViewModel order, string path)
        {
            //add validations
            this.order = order;
            this.path = path;
        }

        /// <summary>
        /// saves 
        /// </summary>
        /// <returns></returns>
        public void Save()
        {
            System.IO.File.WriteAllText(path, order.ToString());
        }
    }
}
