﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Data.Repository.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(Guid id);

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void Update(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
