using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public class EfRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
       where TContext : DbContext, new()
    {

        IHttpContextAccessor _httpContext;
        public EfRepositoryBase(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                using var context = new TContext();
                entity.CreatorUserId = Guid.Parse(_httpContext.HttpContext.User.Identity.Name);
                entity.CreatedDate = DateTime.Now;
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                await context.SaveChangesAsync();
                return addedEntity.Entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteAsync(TEntity entity)
        {            
            using var context = new TContext();
            entity.IsDeleted = true;
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public IQueryable<TEntity> FindByAsync(Expression<Func<TEntity, bool>> filter)
        {
            using var context = new TContext();
            var query = context.Set<TEntity>().Where(filter).Where(x => x.IsDeleted == false).AsNoTracking();
            return query;
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>>? filter = null, Expression<Func<TEntity, object>>[]? includes = null)
        {
            using var context = new TContext();
            IQueryable<TEntity> query = context.Set<TEntity>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            var result = filter == null ? await query.SingleOrDefaultAsync(x => x.IsDeleted == false) : await query.SingleOrDefaultAsync(filter);
            if(result != null && result.IsDeleted == false)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? filter = null, Expression<Func<TEntity, object>>[]? includes = null)
        {
            try
            {
                using var context = new TContext();
                IQueryable<TEntity> query = context.Set<TEntity>();
                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }
                var res = filter == null ? await query.OrderBy(c => c.CreatedDate).ToListAsync() : await query.Where(filter).OrderBy(c => c.CreatedDate).ToListAsync();
                return res.Where(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<TEntity>> GetListAsyncThenInclude(Expression<Func<TEntity, bool>>? filter = null,Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null)
        {
            using var context = new TContext();
            IQueryable<TEntity> query = context.Set<TEntity>().AsQueryable();

            // Eğer filter varsa, sorguya ekliyoruz
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Include ve ThenInclude işlemi yapılır
            if (includes != null)
            {
                query = includes(query);
            }
            var res = await query.ToListAsync();
            return res.Where(x => x.IsDeleted == false).ToList();
        }



        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using var context = new TContext();
            entity.CreatedDate = DateTime.Now;
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return updatedEntity.Entity;
        }
    }
}
