using DevFramework.Northwind.Business.Abstract;
using System.Collections.Generic;
using DevFramework.Northwind.Entities.Concrete;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Business.ValidationRules.FluentValidation;
using DevFramework.Core.Aspects.Postsharp.ValidationAspects;
using DevFramework.Core.Aspects.Postsharp.TransactionAspects;
using DevFramework.Core.CrossCuttingConcerns.Caching.Microsoft;
using DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using DevFramework.Core.Aspects.Postsharp.CacheAspects;
using DevFramework.Core.Aspects.Postsharp.LogAspects;
using DevFramework.Core.Aspects.Postsharp.PerformanceAspects;
using System.Threading;

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
       // [LogAspect(typeof(DatabaseLogger))]
      //  [LogAspect(typeof(FileLogger))]
        [FluentValidationAspect(typeof(ProductValidatior))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Product Add(Product product)
        {
            return _productDal.Add(product);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        //[LogAspect(typeof(DatabaseLogger))]
        //[LogAspect(typeof(FileLogger))]
        [PerformanceCounterAspect(2)]
        public List<Product> GetAll()
        {
            Thread.Sleep(3000);
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
        [FluentValidationAspect(typeof(ProductValidatior))]
        public void TransactionOperation(Product product1, Product product2)
        {
            _productDal.Add(product1);
            //Business code
            _productDal.Update(product2);
        }

        [FluentValidationAspect(typeof(ProductValidatior))]
        public Product Update(Product product)
        {
            return _productDal.Update(product);
        }
    }
}
