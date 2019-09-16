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
    public class WriteoffService
    {
        private DataManager _dataManager;
        
        public WriteoffService(DataManager dataManager)
        {
            this._dataManager = dataManager;
        }
        
        public List<WriteoffModelView> GetAll(DateTime? startDate = null, DateTime? endDate = null, int skip = 0, int count = 0 )
        {   
            return  _dataManager.Writeoffs.GetAll(false, GetExpression(startDate, endDate), skip, count)
                .Select(WriteoffViewDbToModel).ToList();
        }

        public int GetCount(DateTime? startDate = null, DateTime? endDate = null)
        {
            return _dataManager.Writeoffs.GetCount(GetExpression(startDate, endDate));
        }

        public WriteoffModelEdit GetWriteoff(int id)
        {
            return WriteoffEditDbToModel(_dataManager.Writeoffs.GetById(id, true));
        }

        public WriteoffModelView Save(WriteoffModelView Writeoff)
        {
            Writeoff WriteoffDb = WriteoffViewModelToDb(Writeoff);

            _dataManager.Writeoffs.Save(WriteoffDb);

            return WriteoffViewDbToModel(WriteoffDb);
        }

        public WriteoffModelEdit Save(WriteoffModelEdit Writeoff)
        {
            Writeoff WriteoffDb = WriteoffEditModelToDb(Writeoff);

            _dataManager.Writeoffs.Save(WriteoffDb);

            foreach (var productCount in WriteoffDb.ProductCounts)
            {
                _dataManager.ProductCounts.Save(productCount);
            }

            return WriteoffEditDbToModel(WriteoffDb);
        }

        private Expression<Func<Writeoff, bool>> GetExpression(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || startDate == new DateTime()) startDate = DateTime.MinValue;
            if (endDate == null || endDate == new DateTime()) endDate = DateTime.MaxValue;

            return c => c.Date >= startDate && c.Date <= endDate;
        }

        #region ModelToDb

        private Writeoff WriteoffViewModelToDb(WriteoffModelView Writeoff)
        {
            var WriteoffDb = Writeoff.Id != 0 ? _dataManager.Writeoffs.GetById(Writeoff.Id) : new Writeoff();
            WriteoffDb.Date = Writeoff.Date;
            WriteoffDb.Comment = Writeoff.Comment;

            return WriteoffDb;
        }

        private Writeoff WriteoffEditModelToDb(WriteoffModelEdit Writeoff)
        {
            var WriteoffDb = Writeoff.Id != 0 ? _dataManager.Writeoffs.GetById(Writeoff.Id) : new Writeoff();

            WriteoffDb.Date = Writeoff.Date;
            WriteoffDb.Comment = Writeoff.Comment;
            WriteoffDb.ProductCounts = Writeoff.ProductCounts.Select(ProductCountModelToDb).ToList();

            return WriteoffDb;
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

        private WriteoffModelView WriteoffViewDbToModel(Writeoff writeoff)
        {
            return new WriteoffModelView
            {
                Id = writeoff.Id,
                Date = writeoff.Date,
                Comment = writeoff.Comment
            };
        }

        private WriteoffModelEdit WriteoffEditDbToModel(Writeoff writeoff)
        {
            return new WriteoffModelEdit
            {
                Id = writeoff.Id,
                Date = writeoff.Date,
                Comment = writeoff.Comment,
                ProductCounts = writeoff.ProductCounts.Select(ProductCountDbToModel).ToList()
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
