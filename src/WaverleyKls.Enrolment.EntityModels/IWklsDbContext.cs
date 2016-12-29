using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WaverleyKls.Enrolment.EntityModels
{
    /// <summary>
    /// This provides interfaces to the <see cref="WklsDbContext"/> class.
    /// </summary>
    public interface IWklsDbContext : IDisposable // TODO: This needs to be refactored to IDbContext
    {
        /// <summary>
        /// Gets or sets the set of <see cref="EnrolmentForm"/> entities.
        /// </summary>
        DbSet<EnrolmentForm> EnrolmentForms { get; set; }

        /// <summary>
        /// Gets or sets the set of <see cref="Payment"/> entities.
        /// </summary>
        DbSet<Payment> Payments { get; set; }
        
        /// <summary>
        /// Gets the <see cref="DatabaseFacade"/> instance.
        /// </summary>
        DatabaseFacade Database { get; }

        /// <summary>
        /// Gets the database entry.
        /// </summary>
        /// <typeparam name="TEntity">Type of database entity.</typeparam>
        /// <param name="entity">Database entity.</param>
        /// <returns>Returns the <see cref="EntityEntry{TEntity}"/> instance.</returns>
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Adds the database entity.
        /// </summary>
        /// <typeparam name="TEntity">Type of database entity.</typeparam>
        /// <param name="entity">Database entity.</param>
        /// <returns>Returns the <see cref="EntityEntry{TEntity}"/> instance.</returns>
        EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Updates the database entity.
        /// </summary>
        /// <typeparam name="TEntity">Type of database entity.</typeparam>
        /// <param name="entity">Database entity.</param>
        /// <returns>Returns the <see cref="EntityEntry{TEntity}"/> instance.</returns>
        EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Saves all changes.
        /// </summary>
        /// <returns>Returns the result code.</returns>
        int SaveChanges();

        /// <summary>
        /// Saves all changes.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> instance.</param>
        /// <returns>Returns the result code.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
