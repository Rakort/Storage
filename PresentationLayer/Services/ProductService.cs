using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using BuissnesLayer;
using DataLayer;
using PresentationLayer.Models;

namespace PresentationLayer.Services
{
    public class ProductService
    {
        private DataManager _dataManager;
        
        public ProductService(DataManager dataManager)
        {
            this._dataManager = dataManager;
        }
        
        public List<ProductModel> GetAll(Availability availability = Availability.All, string productName = "", int skip = 0, int count = 0 )
        {
            return _dataManager.Products.GetAll(false,
                GetExpressionByAvailability(availability, productName), skip, count)
                .Select(ProductDbToModel).ToList();
        }

        public int GetCount(Availability availability = Availability.All, string productName = "")
        {
            return _dataManager.Products.GetCount(GetExpressionByAvailability(availability, productName));
        }

        private Expression<Func<Product, bool>> GetExpressionByAvailability(Availability availability, string productName)
        {
            switch (availability)
            {
                case Availability.Available:
                    return p => p.ProductName.Contains(productName) && p.Count > 0;
                case Availability.NotAvailable:
                    return p => p.ProductName.Contains(productName) && p.Count == 0;
                case Availability.BelowMinBalance:
                    return p => p.ProductName.Contains(productName) && p.Count < p.MinCount;
                default:
                    return p => p.ProductName.Contains(productName);
            }
        }

        private ProductModel ProductDbToModelById(int productId)
        {
            var product = _dataManager.Products.GetById(productId);

            return ProductDbToModel(product);
        }

        public ProductModel Save(ProductModel product)
        {
            Product productDb = ProductModelToDb(product);
            
            _dataManager.Products.Save(productDb);

            return ProductDbToModelById(productDb.Id);
        }

        internal ProductModel ProductDbToModel(Product product)
        {
            return new ProductModel()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Description = product.Description,
                Code = product.Code,
                Article = product.Article,
                Count = product.Count,
                MinCount = product.MinCount
            };
        }

        internal Product ProductModelToDb(ProductModel product)
        {
            var productDb = product.Id != 0 ? _dataManager.Products.GetById(product.Id) : new Product();

            productDb.ProductName = product.ProductName;
            productDb.Description = product.Description;
            productDb.Code = product.Code;
            productDb.Article = product.Article;
            productDb.Count = product.Count;
            productDb.MinCount = product.MinCount;

            return productDb;
        }


    }
}
