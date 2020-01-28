using System;
using System.Collections.Generic;
using System.Text;

namespace PxTransform.Auto.Data.Base
{
    interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(object id);

        IEnumerable<TEntity> GetAll();

        void Insert(TEntity entity);

        void Insert(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Update(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);

        void Delete(IEnumerable<TEntity> entities);

    }
}
