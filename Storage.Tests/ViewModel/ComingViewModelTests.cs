using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Storage.ViewModel;

namespace Storage.Tests.ViewModel
{
    [TestClass]
    public class ComingViewModelTests
    {
        //[TestMethod]
        //public void ComingFilter()
        //{
        //    //DB db = Mock.Of<DB>(
        //    //    d => d.GetAllComing() == new List<Coming>()
        //    //    {
        //    //        new Coming() {InvoiceDate = new DateTime(2019,08,10)},
        //    //        new Coming() {InvoiceDate = new DateTime(2019,08,12)},
        //    //        new Coming() {InvoiceDate = new DateTime(2019,08,05)}
        //    //    });
        //    Mock<DB> db = new Mock<DB>();
        //    db.Setup(ld => ld.GetAllComing(It.IsAny<Func<Coming, bool>>(),0,10))
        //        .Returns<List<Coming>>(predicate => new List<Coming>()
        //        {
        //            new Coming() {InvoiceDate = new DateTime(2019,08,10)},
        //            new Coming() {InvoiceDate = new DateTime(2019,08,12)},
        //            new Coming() {InvoiceDate = new DateTime(2019,08,05)}
        //        }.Where(predicate));

        //    var cvm = new ComingViewModel(db);

        //    cvm.StartDate = new DateTime(2019, 08, 10);
        //    cvm.ApplyFilter.Execute(null);

        //    Assert.AreEqual(2, cvm.ComingEntries.Count);
        //}

        //public void ComingFilter()
        //{
        //    DB db = new DB_SQLite(ShowWin.TestDbName);

        //    db.Add(new Coming() { InvoiceDate = new DateTime(2019, 08, 10) });
        //    db.Add(new Coming() { InvoiceDate = new DateTime(2019, 08, 12) });
        //    db.Add(new Coming() { InvoiceDate = new DateTime(2019, 08, 05) });

        //    var cvm = new ComingViewModel(db);

        //    cvm.StartDate = new DateTime(2019, 08, 10);
        //    cvm.ApplyFilter.Execute(null);

        //    Assert.AreEqual(2, cvm.ComingEntries.Count);
        //}



    }
}
