using System;

using Microsoft.EntityFrameworkCore;

namespace WaverleyKls.Enrolment.EntityModels
{
    public static class DbContextExtensions
    {
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