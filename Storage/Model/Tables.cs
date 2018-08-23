using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Storage.Model
{
    public class Table
    { }
    public class Product : Table 
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int IdProduct { get; set; }

        [MaxLength(255), NotNull]
        public string ProductName { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public string Article { get; set; }
        public int Count { get; set; }
        public int MinCount { get; set; }
    }

    public class Coming : Table
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int IdComing { get; set; }
        public int IdProvider { get; set; }
        public int InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Comment { get; set; }
    }
    public class ComingProduct : Table 
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }
        [NotNull]
        public int IdComing { get; set; }
        [NotNull]
        public int IdProduct { get; set; }
        [NotNull]
        public int Count { get; set; }
    }

    public class Writeoff : Table 
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int IdWriteoff { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
    }
    public class WriteoffProduct : Table 
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }
        [NotNull]
        public int IdWriteoff { get; set; }
        [NotNull]
        public int IdProduct { get; set; }
        [NotNull]
        public int Count { get; set; }
    }

    public class Provider : Table
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int IdProvider { get; set; }
        [NotNull]
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
    }
}
