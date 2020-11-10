using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlayTech.Shared.Database.Interfaces;

namespace PlayTech.Shared.Database
{
    public class EfDbContext : DbContext, IDbContext
    {
        public EfDbContext(DbContextOptions options) : base(options)
        {
        }

        #region ModelBuilder

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        #endregion ModelBuilder


        #region Methods

        int IDbContext.SaveChanges()
        {
            return SaveChanges();
        }

        async Task<int> IDbContext.SaveChangesAsync()
        {
            return await SaveChangesAsync();
        }

        /// <summary>
        /// Creates a DbSet that can be used to query and save instances of entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>A set for the given entity type</returns>
        public new virtual DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        
        #endregion Methods

    }
}
