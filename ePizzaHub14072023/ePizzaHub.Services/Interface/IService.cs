using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Services.Interface
{
    public interface IService<TEntity> where TEntity : class
    {
        TEntity Get(int id);

        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);

        void Update(TEntity entity);
        void Remove(TEntity entity);

        void Delete(object id);

        
    }
}
