using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PresentationLayer.Models
{
    public class WriteoffModelView: TableModel
    {
        public DateTime Date { get; set; }
        public string Comment { get; set; }
    }

    public class WriteoffModelEdit : WriteoffModelView
    {
        public List<ProductCountModel> ProductCounts { get; set; }

        public WriteoffModelEdit()
        {
            ProductCounts = new List<ProductCountModel>();
        }
    }
}
