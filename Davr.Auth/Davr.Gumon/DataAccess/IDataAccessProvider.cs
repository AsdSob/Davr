using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Davr.Gumon.Entities.Abstracts;
using Davr.Gumon.Helpers;

namespace Davr.Gumon.DataAccess
{
    public interface IDataAccessProvider
    {
        public DataContext _context { get; set; }

        Task AddEntities<T>(IEnumerable<T> entity) where T : class, IEntity<int>;

        Task UpdateEntities<T>(IEnumerable<T> entity) where T : class, IEntity<int>;

        Task DeleteEntity<T>(T entity) where T : class, IEntity<int>;
        Task DeleteEntities<T>(IEnumerable<T> entity) where T : class, IEntity<int>;

        Task<T> GetEntity<T>(int id) where T : class, IEntity<int>;
        int GetEntitiesCount<T>(Expression<Func<T, bool>> exp) where T : class, IEntity<int>;

        Task<IEnumerable<T>> GetEntities<T>() where T : class, IEntity<int>;

        Task<IEnumerable<T>> GetEntities<T>(Expression<Func<T, bool>> exp) where T : class, IEntity<int>;

        Task<IEnumerable<T>> GetEntities<T>(Expression<Func<T, bool>> exp, int? skipQty, int? takeQty)
            where T : class, IEntity<int>;

        Task AddOrUpdateEntity<T>(T entity) where T : class, IEntity<int>;
        Task AddOrUpdateEntities<T>(IEnumerable<T> entity) where T : class, IEntity<int>;

    }
}
