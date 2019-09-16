using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class ProductModel: TableModel
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Article { get; set; }
        public int Count { get; set; }
        public int MinCount { get; set; }
    }

    public enum Availability
    {
        All,                //"Все"
        Available,          //"Только в наличии"
        NotAvailable,       //"Нет в наличии"
        BelowMinBalance     //"Ниже минимального остатка"
    }
}
