using System;
using System.Collections.Generic;
using System.Text;
using PxTransform.Auto.Data.Base;
using Microsoft.EntityFrameworkCore;
namespace PxTransform.Auto.Data.Data

{


    public class TranRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        public TranContext _context;
        public DbSet<TEntity> table;

        public TranRepository(TranContext context)
        {
            _context = context;
            table = _context.Set<TEntity>();
        }
        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public TEntity GetById(object id)
        {
            throw new NotImplementedException();
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
    }
}
