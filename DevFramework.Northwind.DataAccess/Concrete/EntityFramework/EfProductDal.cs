using DevFramework.Northwind.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using DevFramework.Northwind.Entities.Concrete;
using System.Linq.Expressions;
using DevFramework.Core.DataAccess.EntityFramework;
using DevFramework.Northwind.Entities.ComplexTypes;
using System.Linq;

namespace DevFramework.Northwind.DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        public List<ProductDetail> GetProductDetails()
        {
            //disposable olduğu için using ile yapıyoruz.
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from p in context.Products
                             join c in context.Categories on p.CategoryId equals c.CategoryId
                             select new ProductDetail
                             {
                                 ProductId = p.ProductId,
                                 ProductName = p.ProductName,
                                 CategoryName = c.CategoryName
                             };
                return result.ToList();
            }
                
        }
    }
}
