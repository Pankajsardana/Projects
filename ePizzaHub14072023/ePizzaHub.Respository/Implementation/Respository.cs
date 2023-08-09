using ePizzaHub.Core;
using ePizzaHub.Respository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Respository.Implementation
{
    public class Respository<TEntity> : IRespository<TEntity> where TEntity : class
    {
        public readonly AppDbContext _db;
        public Respository(AppDbContext db)
        {
            _db= db;
        }

        public void Add(TEntity entity)
        {
            _db.Set<TEntity>().Add(entity);
          
            //throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            TEntity entity=_db.Set<TEntity>().Find(id);
            if(entity != null) 
            {
                this.Remove(entity);
            }

        }

        public TEntity Find(object id)
        {
           return _db.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
           return _db.Set<TEntity>().ToList();
        }

        public void Remove(TEntity entity)
        {
            _db.Set<TEntity>().Remove(entity);

        }

        public int SaveChanges()
        {
           return _db.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _db.Set<TEntity>().Update(entity);
        }
    }
}
