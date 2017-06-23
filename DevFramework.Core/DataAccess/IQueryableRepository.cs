using DevFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevFramework.Core.DataAccess
{
    //T:Class gelecek değerin referans tip olması ve new lenebilir olması 
    //T:IEntity IEntity den implement edilmesi gerekiyor. 
  public  interface IQueryableRepository<T> where T:class,IEntity,new()
    {
        IQueryable<T> Table { get; }
    }
}
