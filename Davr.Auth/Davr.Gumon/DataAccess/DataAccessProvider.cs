using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Davr.Gumon.Entities.Abstracts;
using Davr.Gumon.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Davr.Gumon.DataAccess
{
    public class DataAccessProvider : IDataAccessProvider
    {
        public DataContext _context { get; set; }

        public DataAccessProvider(DataContext context)
        {
            _context = context;
        }

        #region Base DataAccess


        /// <summary>
        /// Add T type entities 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task AddEntities<T>(IEnumerable<T> entities) where T : class, IEntity<int>
        {
            _context.AttachRange(entities);
            _context.SaveChanges();
        }

        /// <summary>
        /// Update T type entities
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task UpdateEntities<T>(IEnumerable<T> entities) where T : class, IEntity<int>
        {
            _context.UpdateRange(entities);
            _context.SaveChanges();
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task DeleteEntities<T>(IEnumerable<T> entities) where T : class, IEntity<int>
        {
            foreach (var entity in entities)
            {
                if (entity.Id != 0)
                    _context.Remove(entity);
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Get entity by Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetEntity<T>(int id) where T : class, IEntity<int>
        {
            if (id == 0) return null;

            var entity = await _context.FindAsync<T>(id);
            return entity;
        }

        /// <summary>
        /// Get entities
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetEntities<T>() where T : class, IEntity<int>
        {
            var entities = await _context.Set<T>().ToListAsync();

            return entities;
        }

        /// <summary>
        /// Get total number of entity records with expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int GetEntitiesCount<T>(Expression<Func<T, bool>> exp) where T : class, IEntity<int>
        {
            var total = 0;
            if (exp == null)
            {
                total = _context.Set<T>().Count();
            }
            else
            {
                total = _context.Set<T>().Where(exp).Count();
            }

            return total;
        }

        /// <summary>
        /// Get entities with sql filter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetEntities<T>(Expression<Func<T, bool>> exp) where T : class, IEntity<int>
        {
            if (exp == null)
            {
                var entity = await GetEntities<T>();

                return entity;
            }

            var entities = await _context.Set<T>().Where(exp).ToListAsync();
            return entities;
        }

        /// <summary>
        /// Get Entities with expression, and skip quantity of records and take quantity of records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp">expression</param>
        /// <param name="skipQty">Skipping quantity</param>
        /// <param name="takeQty">Taking quantity</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetEntities<T>(Expression<Func<T, bool>> exp, int? skipQty, int? takeQty) where T : class, IEntity<int>
        {
            var entities = new List<T>();

            if (exp == null)
            {
                entities = await GetEntities<T>() as List<T>;
            }
            else
            {
                entities = await _context.Set<T>().Where(exp).Skip((int) skipQty).Take((int) takeQty).ToListAsync();
            }

            //if (skipQty != null && skipQty > 0) { entities = entities.Skip((int) skipQty) as List<T>; }

            //if (takeQty != null && takeQty > 0)
            //{
            //    entities = entities.Take((int) takeQty) as List<T>; 

            //}

            return entities;
        }


        #endregion

        public async Task AddOrUpdateEntity<T>(T entity) where T : class, IEntity<int>
        {
            if (entity.Id == 0)
            {
                await AddEntities(new T[] { entity });
            }
            else
            {
                await UpdateEntities(new T[] { entity });
            }
        }

        public async Task AddOrUpdateEntities<T>(IEnumerable<T> entities) where T : class, IEntity<int>
        {
            var newEntities = entities.Where(x => x.Id == 0).ToArray();
            var updateEntities = entities.Where(x => x.Id != 0).ToArray();

            if (newEntities.Any())
            {
                await AddEntities(newEntities);
            }

            if (updateEntities.Any())
            {
                await UpdateEntities(updateEntities);
            }
        }

        public virtual async Task DeleteEntity<T>(T entity) where T : class, IEntity<int>
        {
            await DeleteEntities(new T[] { entity });
        }

    }
}
