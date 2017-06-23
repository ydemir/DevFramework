using DevFramework.Northwind.Business.Abstract;
using System.Collections.Generic;
using DevFramework.Northwind.Entities.Concrete;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Business.ValidationRules.FluentValidation;
using DevFramework.Core.Aspects.Postsharp.ValidationAspects;
using DevFramework.Core.Aspects.Postsharp.TransactionAspects;

//DevFramework.Core referansı eklemem gerekiyor. aksi taktirde dal a ulaşamıyorum.

namespace DevFramework.Northwind.Business.Concrete.Managers
{
    
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        [FluentValidationAspect(typeof(ProductValidatior))]
        public Product Add(Product product)
        {
            return _productDal.Add(product);
        }

        public List<Product> GetAll()
        {
            return _productDal.GetList();
        }

        public Product GetById(int id)
        {
            return _productDal.Get(p => p.ProductId == id);
        }

        //public void TransactionOperation(Product product1,Product product2)
        //{
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        try
        //        {
        //            _productDal.Add(product1);
        //            //Business code
        //            _productDal.Update(product2);
        //            scope.Complete();
        //        }
        //        catch (Exception)
        //        {

        //            scope.Dispose();                }
        //    }

        //}

        [TransactionScopeAspect]
        public void TransactionOperation(Product product1, Product product2)
        {
            _productDal.Add(product1);

            _productDal.Update(product2);
        }

        [FluentValidationAspect(typeof(ProductValidatior))]
        public Product Update(Product product)
        {
            return _productDal.Update(product);
        }
    }
}
