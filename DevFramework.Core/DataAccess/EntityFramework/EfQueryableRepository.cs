using DevFramework.Core.Entities;
using System.Data.Entity;
using System.Linq;

namespace DevFramework.Core.DataAccess.EntityFramework
{
    //defancive ilerliyoruz. güvenli bir şekilde : dan sonra koşullarımızı belirtirouz.
    public class EfQueryableRepository<T> : IQueryableRepository<T> where T : class, IEntity, new()
    {
        private DbContext _context;
        private IDbSet<T> _entities;

        public EfQueryableRepository(DbContext context)
        {
            _context = context;
        }
        public IQueryable<T> Table => this.Entities;

        protected virtual IDbSet<T> Entities
        {
            get
            {
                if (_entities==null)
                {
                    _entities = _context.Set<T>();
                }
                return _entities;
            }
        }
    }
}
