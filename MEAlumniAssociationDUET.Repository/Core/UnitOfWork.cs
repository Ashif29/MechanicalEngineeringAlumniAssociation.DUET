using MEAlumniAssociationDUET.Repository.DataAccess;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAlumniAssociationDUET.Repository.Core
{
    public class UnitOfWork:IUnitOfWork
    {
        protected readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
        //public virtual async Task<IDbContextTransaction> BeginTransaction()
        //{
        //    return await _dbContext.Database.BeginTransactionAsync();
        //}
    }
}
