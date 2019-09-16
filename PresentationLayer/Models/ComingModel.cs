using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class ComingModelView: TableModel
    {
        public string Provider { get; set; }
        public int InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Comment { get; set; }
    }

    public class ComingModelEdit : ComingModelView
    {
        public List<ProductCountModel> ProductCounts { get; set; }
    }
}
