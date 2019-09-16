using System;
using System.Linq;
using System.Net;
using BuissnesLayer;
using DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresentationLayer;
using PresentationLayer.Models;

namespace Storage.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DataLayer()
        {
            using (EFDBContext db = new EFDBContext())
            {
                //// создаем два объекта User
                //Product user1 = new Product { ProductName = "Tom", Count = 33 };
                //Product user2 = new Product { ProductName = "Sam", Count = 26 };

                ////// добавляем их в бд
                //db.Product.Add(user1);
                //db.Product.Add(user2);
                //db.SaveChanges();

                var users = db.Product.ToList();
                Assert.AreEqual(users.Count(), 2);
            }
            Console.Read();
        }
        [TestMethod]
        public void DataLayerComing()
        {
            //using (EFDBContext db = new EFDBContext())
            //{
            //    Provider provider = new Provider()
            //    {
            //        Address = "Майкоп, Широтная 165/3",
            //        Email = "uporovo@krimm.ru",
            //        Name = "ООО АГРОФИРМА \"КРИММ\"",
            //        Note = "Генеральный директор: Рязанов Геннадий Александрович",
            //        Phone = "+7 (902) 622-66-93"
            //    };
            //    db.Provider.Add(provider);
            //    db.SaveChanges();
            //    provider = db.Provider.First();
                		
            //    Coming coming1 = new Coming { InvoiceDate = new DateTime(2017,08,12), Provider = provider};
            //    Coming coming2 = new Coming { InvoiceDate = new DateTime(2017, 08, 10), Provider = provider };

            //    db.Coming.Add(coming1);
            //    db.Coming.Add(coming2);
            //    db.SaveChanges();

            //    var comings = db.Coming.ToList();
            //    Assert.AreEqual(comings.Count(), 2);

            //    //db.Coming.Remove(coming1);
            //    //db.Coming.Remove(coming2);
            //}
            //Console.Read();
        }

        [TestMethod]
        public void ServiseManagerTest()
        {
            var s = new ServicesManager(new DataManager(new EFDBContext()));
            s.Products.Save(new ProductModel(){
                ProductName = "Яблоко",
                Description = "Фрукт",
                Code = "4818",
                Article = "1861",
                Count = 100,
                MinCount = 50
            });
            s.Products.Save(new ProductModel()
            {
                ProductName = "Картошка",
                Description = "Овощ",
                Code = "2036",
                Article = "9817",
                Count = 0,
                MinCount = 50
            });
            s.Products.Save(new ProductModel()
            {
                ProductName = "Морковка",
                Description = "Овощ",
                Code = "9734",
                Article = "1387",
                Count = 50,
                MinCount = 100
            });
        }
    }
}
