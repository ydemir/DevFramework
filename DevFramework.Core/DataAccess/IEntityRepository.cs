using DevFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DevFramework.Core.DataAccess
{
    //Bu repository i kullanacak kişilere kısıt getiriyoruz. 
    //T : class ile class burda referans tip , interface lerde referans tip olduğu için ve Interface gönderemesin diğe new() işlemi 
    //Yapılabilir bir class genderememesi için
    //IEntity nedir ? IEntity bizim için bir imza veritabanı nesnelerini IEntity ile süslüyor olacağız. Nesnelerin belli bir standarda sahip 
    //olması için bu şekilde kullanıyoruz. Tüm Entity leri IEntity den implement edeceğiz.
   public interface IEntityRepository<T> where T:class, IEntity,new()
    {
        List<T> GetList(Expression<Func<T,bool>> filter=null);
        T Get(Expression<Func<T, bool>> filter = null);
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
