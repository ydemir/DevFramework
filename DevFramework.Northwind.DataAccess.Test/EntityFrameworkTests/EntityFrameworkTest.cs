using Microsoft.VisualStudio.TestTools.UnitTesting;
using DevFramework.Northwind.DataAccess.Concrete.EntityFramework;


namespace DevFramework.Northwind.DataAccess.Test.EntityFrameworkTests
{
    [TestClass]
    public class EntityFrameworkTest
    {
        [TestMethod]
        public void Get_all_returns_all_products()
        {
            
            EfProductDal productDal = new EfProductDal();

            //productDal.GetList() methoduna ulaşmak için DevFramework.Core refenransını eklememiz gerekiyor.
            var result= productDal.GetList();

            Assert.AreEqual(77, result.Count);
        }

        [TestMethod]
        public void Get_all_with_paramether_returns_filtered_products()
        {

            EfProductDal productDal = new EfProductDal();

            //productDal.GetList() methoduna ulaşmak için DevFramework.Core refenransını eklememiz gerekiyor.
            var result = productDal.GetList(p=>p.ProductName.Contains("ab"));

            Assert.AreEqual(4, result.Count);
        }
    }
}
