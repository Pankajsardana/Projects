

using ePizzaHub.Respository.Interface;
using ePizzaHub.Services.Interface;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Services.Implementations
{
    public class Service<TEntity>:IService<TEntity> where TEntity : class
    {
        IRespository<TEntity> _repos;
        private ICartRespository cartRepo;

        public Service(IRespository<TEntity> repos) 
        {
            _repos = repos;
        }

        //public Service(ICartRespository cartRepo)
        //{
        //    this.cartRepo = cartRepo;
        //}

        public void Add(TEntity entity)
        {
            _repos.Add(entity);
            _repos.SaveChanges();
        }

        public void Delete(object id)
        {
            _repos?.Delete(id);
            _repos?.SaveChanges();
        }

        public TEntity Get(int id)
        {
            return _repos.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _repos.GetAll();
        }

        public void Remove(TEntity entity)
        {
           _repos.Remove(entity);
            _repos.SaveChanges();   
        }

        public void Update(TEntity entity)
        {
            _repos.Update(entity);
            _repos.SaveChanges();
        }
    }
}
