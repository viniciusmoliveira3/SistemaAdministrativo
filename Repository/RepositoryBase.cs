using Colex.Interfaces;
using Colex.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Colex.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly BaseContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public RepositoryBase(BaseContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }


        public void Add(TEntity entity)
        {
            Db.Add(entity);
            Db.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            Db.Remove(entity);
            Db.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = DbSet.Find(id)!;
            Db.Remove(entity);
            Db.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            Db.Update(entity);
            Db.SaveChanges();
        }

        public virtual TEntity? GetById(int id)
        {
            var entity = DbSet.Find(id);

            if (entity != null)
                DbSet.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public virtual List<TEntity> GetAll()
        {
            var list = DbSet.ToList();
            return list;
        }

        public void AddRange(List<TEntity> items)
        {
            DbSet.AddRange(items);
        }




    }

}  
    
    

