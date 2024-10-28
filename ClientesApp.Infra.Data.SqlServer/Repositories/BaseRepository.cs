using ClientesApp.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Infra.Data.SqlServer.Repositories
{
    public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : class
    {
        public virtual Task AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<List<TEntity>> GetManyAsync(Func<TEntity, bool> where)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity?> GetOneAsync(Func<TEntity, bool> where)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity?> GetByIdAsync(TKey id)
        {
            throw new NotImplementedException();
        }

        public virtual void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
