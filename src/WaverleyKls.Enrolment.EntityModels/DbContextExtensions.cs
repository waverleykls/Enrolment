using System;

using Microsoft.EntityFrameworkCore;

namespace WaverleyKls.Enrolment.EntityModels
{
    /// <summary>
    /// This represents the extension entity for the <see cref="DbContext"/> class.
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// Adds or updates the database context.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to add or update.</typeparam>
        /// <param name="ctx"><see cref="IWklsDbContext"/> instance.</param>
        /// <param name="entity">Entity to add or update.</param>
        public static void AddOrUpdate<TEntity>(this IWklsDbContext ctx, TEntity entity) where TEntity : class
        {
            var entry = ctx.Entry(entity);
            switch (entry.State)
            {
                case EntityState.Detached:
                    ctx.Add(entity);
                    break;
                case EntityState.Modified:
                    ctx.Update(entity);
                    break;
                case EntityState.Added:
                    ctx.Add(entity);
                    break;
                case EntityState.Unchanged:
                    //item already in db no need to do anything  
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}