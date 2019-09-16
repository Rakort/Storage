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
    public class ComingService
    {
        private DataManager _dataManager;
        
        public ComingService(DataManager dataManager)
        {
            this._dataManager = dataManager;
        }
        
        public List<ComingModelView> GetAll(DateTime? startDate = null, DateTime? endDate = null, int skip = 0, int count = 0 )
        {   
            return  _dataManager.Comings.GetAll(false, GetExpression(startDate, endDate), skip, count)
                .Select(ComingViewDbToModel).ToList();
        }

        public int GetCount(DateTime? startDate = null, DateTime? endDate = null)
        {
            return _dataManager.Comings.GetCount(GetExpression(startDate, endDate));
        }

        public ComingModelEdit GetComing(int id)
        {
            return ComingEditDbToModel(_dataManager.Comings.GetById(id, true));
        }

        public ComingModelView Save(ComingModelView coming)
        {
            Coming comingDb = ComingViewModelToDb(coming);

            _dataManager.Comings.Save(comingDb);

            return ComingViewDbToModel(comingDb);
        }

        public ComingModelEdit Save(ComingModelEdit coming)
        {
            Coming comingDb = ComingEditModelToDb(coming);

            _dataManager.Comings.Save(comingDb);

            foreach (var productCount in comingDb.ProductCounts)
            {
                _dataManager.ProductCounts.Save(productCount);
            }

            return ComingEditDbToModel(comingDb);
        }

        private Expression<Func<Coming, bool>> GetExpression(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || startDate == new DateTime()) startDate = DateTime.MinValue;
            if (endDate == null || endDate == new DateTime()) endDate = DateTime.MaxValue;

            return c => c.InvoiceDate >= startDate && c.InvoiceDate <= endDate;
        }

        #region ModelToDb

        private Coming ComingViewModelToDb(ComingModelView coming)
        {
            var comingDb = coming.Id != 0 ? _dataManager.Comings.GetById(coming.Id) : new Coming();
            comingDb.InvoiceDate = coming.InvoiceDate;
            comingDb.InvoiceNumber = coming.InvoiceNumber;
            comingDb.Comment = coming.Comment;
            comingDb.Provider = _dataManager.Providers.GetByExpression(c => c.Name == coming.Provider);

            return comingDb;
        }

        private Coming ComingEditModelToDb(ComingModelEdit coming)
        {
            var comingDb = coming.Id != 0 ? _dataManager.Comings.GetById(coming.Id) : new Coming();

            comingDb.InvoiceDate = coming.InvoiceDate;
            comingDb.InvoiceNumber = coming.InvoiceNumber;
            comingDb.Comment = coming.Comment;
            comingDb.Provider = _dataManager.Providers.GetByExpression(c => c.Name == coming.Provider);
            comingDb.ProductCounts = coming.ProductCounts.Select(ProductCountModelToDb).ToList();

            return comingDb;
        }

        private ProductCount ProductCountModelToDb(ProductCountModel productCount)
        {
            ProductService productService = new ProductService(_dataManager);
            return new ProductCount()
            {
                Count = productCount.Count,
                Product = productService.ProductModelToDb(productCount.Product)
            };
        }

        #endregion

        #region DbToModel

        private ComingModelView ComingViewDbToModel(Coming coming)
        {
            return new ComingModelView
            {
                Id = coming.Id,
                Provider = coming.Provider.Name,
                InvoiceDate = coming.InvoiceDate,
                InvoiceNumber = coming.InvoiceNumber,
                Comment = coming.Comment
            };
        }

        private ComingModelEdit ComingEditDbToModel(Coming coming)
        {
            return new ComingModelEdit
            {
                Id = coming.Id,
                Provider = coming.Provider.Name,
                InvoiceDate = coming.InvoiceDate,
                InvoiceNumber = coming.InvoiceNumber,
                Comment = coming.Comment,
                ProductCounts = coming.ProductCounts.Select(ProductCountDbToModel).ToList()
            };
        }

        private ProductCountModel ProductCountDbToModel(ProductCount productCount)
        {
            ProductService productService = new ProductService(_dataManager);
            return new ProductCountModel()
            {
                Count = productCount.Count,
                Product = productService.ProductDbToModel(productCount.Product)
            };
        }

        #endregion


    }
}
