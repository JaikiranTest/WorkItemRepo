using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using WorkItem.Task.Models;
using WorkItem.Task.Services.Contracts;

namespace WorkItem.Task.Services
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class
    {
        internal WorkItemContext _context;
        internal DbSet<T> dbSet;

        public EntityRepository(WorkItemContext context)
        {
            _context = context;
            dbSet = context.Set<T>();
        }

        public async Task<bool> CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        [ExcludeFromCodeCoverage]
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(WorkItem.Task.Models.Task))
                return (IEnumerable<T>)await _context.Tasks.Include(t => t.Status).ToListAsync();
            return await dbSet.ToListAsync();
        }

        public async virtual Task<T?> GetByIDAsync(int? id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
