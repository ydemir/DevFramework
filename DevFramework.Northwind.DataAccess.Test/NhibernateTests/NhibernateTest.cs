using Microsoft.VisualStudio.TestTools.UnitTesting;
using DevFramework.Northwind.DataAccess.Concrete.NHibernate;
using DevFramework.Northwind.DataAccess.Concrete.NHibernate.Helpers;

namespace DevFramework.Northwind.DataAccess.Test.NhibernateTest
{
    [TestClass]
    public class NhibernateTest
    {
        [TestMethod]
        public void Get_all_returns_all_products()
        {

            NhProductDal productDal = new NhProductDal(new SqlServerHelper());

            //productDal.GetList() methoduna ulaşmak için DevFramework.Core refenransını eklememiz gerekiyor.
            var result= productDal.GetList();

            Assert.AreEqual(77, result.Count);
        }

        [TestMethod]
        public void Get_all_with_paramether_returns_filtered_products()
        {

            NhProductDal productDal = new NhProductDal(new SqlServerHelper());

            //productDal.GetList() methoduna ulaşmak için DevFramework.Core refenransını eklememiz gerekiyor.
            var result = productDal.GetList(p=>p.ProductName.Contains("ab"));

            Assert.AreEqual(4, result.Count);
        }
    }
}
