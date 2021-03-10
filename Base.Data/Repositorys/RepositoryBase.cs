using Base.Domain.Interfaces.Repositorys;
using Base.Helpers.Utilitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Base.Data.Repositorys
{
    public sealed class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        public DbContext DbContextEntity { get; set; }
        public DbSet<TEntity> DbSetEntity { get; set; }

        public RepositoryBase(AppDbContext dbContext)
        {
            DbContextEntity = dbContext;
            DbContextEntity.ChangeTracker.AutoDetectChangesEnabled = false;
            DbSetEntity = dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where)
        {
            return DbSetEntity.AsNoTracking().Where(where);
        }

        public IQueryable<TEntity> List()
        {
            return DbSetEntity.AsNoTracking();
        }
        public void Save(TEntity obj)
        {
            Assert.NotNull(obj, nameof(obj));

            if ((Guid)obj.GetType().GetProperty("Id").GetValue(obj, null) != Guid.Empty)
            {
                foreach (var entity in DbContextEntity.ChangeTracker.Entries())
                {
                    entity.State = EntityState.Detached;
                }
                DbSetEntity.Update(obj);
            }
            else
            {
                DbSetEntity.Add(obj);
            }

        }

        public void Delete(Guid id)
        {
            var data = DbSetEntity.Find(id);
            Assert.NotNull(data, nameof(data));
            if (data != null)
            {
                DbSetEntity.Remove(data);
            }
        }

        public int SaveChanges()
        {
            var written = 0;
            while (written == 0)
            {
                try
                {
                    written = DbContextEntity.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    foreach (var entry in ex.Entries)
                    {
                        throw new NotSupportedException("Concurrency error in " + entry.Metadata.Name);
                    }
                }
            }
            return written;
        }

        public DbContext Context()
        {
            return DbContextEntity;
        }
    }
}
