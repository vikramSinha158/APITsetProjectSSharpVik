﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PxTransform.Auto.Data.Base;

namespace PxTransform.Auto.Data.Data
{
    public class AccretiveRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        AccretiveContext _context;
        private DbSet<TEntity> table;

        public AccretiveRepository(AccretiveContext context)
        {
            _context = context;
            table = _context.Set<TEntity>();
        }


        public TEntity GetById(object id)
        {
            return table.Find(id);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            //return table.Take(10).ToList();
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            TEntity exits = table.Find(id);
            table.Remove(exits);
        }

        public void Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}