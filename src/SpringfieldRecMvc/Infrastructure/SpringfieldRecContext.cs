using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SpringfieldRecMvc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SpringfieldRecMvc.Infrastructure
{
    public class SpringfieldRecContext : DbContext
    {
        private IDbContextTransaction _currentTransaction;

        public SpringfieldRecContext(DbContextOptions<SpringfieldRecContext> options) : base(options)
        { }

        public DbSet<RecMember> Members { get; set; }
        public DbSet<TelephoneNumber> TelephoneNumbers { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<NotificationRequest> NotificationRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

        public void BeginTransaction()
        {
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction = Database.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();

                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
