using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Boilerplate.Domain.Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();

        // Task<TEntity> GetById(Guid id);

        // TEntity Create(TEntity entity);

        TEntity Update(TEntity entity);

        // Task Delete(Guid id);

        // Task<int> SaveChangesAsync();
    }
}