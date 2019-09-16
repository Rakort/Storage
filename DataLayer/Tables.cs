using System;
using System.Collections.Generic;

namespace DataLayer
{
    public class Table
    {
        public int Id { get; set; }
    }
    public class Product : Table 
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Article { get; set; }
        public int Count { get; set; }
        public int MinCount { get; set; }
    }

    public class Coming : Table
    {
        public Provider Provider { get; set; }
        public int InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Comment { get; set; }
        public List<ProductCount> ProductCounts { get; set; }
    }
    public class ProductCount : Table 
    {
        public Product Product { get; set; }
        public int Count { get; set; }
    }

    public class Writeoff : Table 
    {
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public List<ProductCount> ProductCounts { get; set; }
    }

    public class Provider : Table
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
    }
}
