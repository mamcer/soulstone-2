using CrossCutting.Core.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Soulstone.Data
{
    public class BaseRepository<TEntity> : IDisposable, IRepository<TEntity, int> where TEntity : class
    {
        private SoulstoneEntities _soulstoneEntities;
        private DbSet<TEntity> _dbSet;
        private bool disposed = false;

        public BaseRepository(SoulstoneEntities soulstoneEntities)
        {
            _soulstoneEntities = soulstoneEntities;
            _dbSet = _soulstoneEntities.Set<TEntity>();
        }

        public PagedResult<TEntity> SearchPaged(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, int pageNumber, int pageSize, Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
        {
            var query = CreateQuery(filter, orderBy, includeProperties);

            var items = query.Skip(pageNumber * pageSize).Take(pageSize).ToList();
            var total = _dbSet.Count();

            return new PagedResult<TEntity>(items, total);
        }

        private IQueryable<TEntity> CreateQuery(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }

        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            return CreateQuery(filter, orderBy, includeProperties).ToList();
        }

        public virtual void Delete(int id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entity)
        {
            if (_soulstoneEntities.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual TEntity Get(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual TEntity Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _soulstoneEntities.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public virtual TEntity Save(TEntity entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public virtual int Count()
        {
            return _dbSet.Count();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbSet = null;
                    _soulstoneEntities = null;
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}