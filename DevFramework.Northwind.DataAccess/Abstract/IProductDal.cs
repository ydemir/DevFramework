using DevFramework.Core.DataAccess;
using DevFramework.Northwind.Entities.ComplexTypes;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Abstract
{
    //Direk IEntityRepo yu kullanmayıp bu şekilde bir işlem yapmamızın nedeni IProductDal a özgü Joinli vb sorgu oluşturabilir
   public interface IProductDal:IEntityRepository<Product>
    {
        //temel nesne product olduğu için

        List<ProductDetail> GetProductDetails();
    }
}
