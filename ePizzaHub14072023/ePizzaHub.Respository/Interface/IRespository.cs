using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Respository.Interface
{
    public interface IRespository<TEntity> where TEntity : class
    {
        TEntity Find(object id);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void Delete(object  id);
        int SaveChanges();
    }
}
