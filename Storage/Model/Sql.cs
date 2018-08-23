using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using SQLite;

namespace Storage.Model
{
    public static class Sql
    {
        private const string DbName = "StorageDB.db";

        public static void CreateTable()
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadWrite, true))
            {
                db.CreateTable<Product>();
                db.CreateTable<Provider>();
                db.CreateTable<Coming>();
                db.CreateTable<Writeoff>();
                db.CreateTable<ComingProduct>();
                db.CreateTable<WriteoffProduct>();
            }
        }
        
        public static int Add(Table table)
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadWrite, true))
            {
                return db.Insert(table);
            }
        }
        public static int Update(Table table)
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadWrite, true))
            {
                return db.Update(table);
            }
        }
        public static int Delete(Table table)
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadWrite, true))
            {
                return db.Delete(table);
            }
        }

        public static List<T> GetTable<T>() where T : Table
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                Type type = typeof(T);

                if (type == typeof(Coming))
                    return db.Table<Coming>().ToList() as List<T>;
                if (type == typeof(ComingProduct))
                    return db.Table<ComingProduct>().ToList() as List<T>;
                if (type == typeof(Writeoff))
                    return db.Table<Writeoff>().ToList() as List<T>;
                if (type == typeof(WriteoffProduct))
                    return db.Table<WriteoffProduct>().ToList() as List<T>;
                if (type == typeof(Product))
                    return db.Table<Product>().ToList() as List<T>;
                if (type == typeof(Provider))
                    return db.Table<Provider>().ToList() as List<T>;

                return new List<T>();
            }
        }

        public static T GetValue<T>(int id) where T : Table
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                Type type = typeof(T);

                if (type == typeof(Coming))
                    return db.Table<Coming>().First(f => f.IdComing == id) as T;
                if (type == typeof(Writeoff))
                    return db.Table<Writeoff>().First(f => f.IdWriteoff == id) as T;
                if (type == typeof(Product))
                    return db.Table<Product>().First(f => f.IdProduct == id) as T;
                if (type == typeof(Provider))
                    return db.Table<Provider>().First(f => f.IdProvider == id) as T;

                return null;
            }
        }
        public static T GetValue<T>(string name) where T : Table
        {
            using (var db = new SQLiteConnection(DbName, SQLiteOpenFlags.ReadOnly, true))
            {
                Type type = typeof(T);

                if (type == typeof(Product))
                    return db.Table<Product>().First(f => f.ProductName == name) as T;
                if (type == typeof(Provider))
                    return db.Table<Provider>().First(f => f.Name == name) as T;

                return null;
            }
        }
    }
}
