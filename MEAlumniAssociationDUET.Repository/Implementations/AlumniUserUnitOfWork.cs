using MEAlumniAssociationDUET.Repository.Contracts;
using MEAlumniAssociationDUET.Repository.Core;
using MEAlumniAssociationDUET.Repository.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAlumniAssociationDUET.Repository.Implementations
{
    public class AlumniUserUnitOfWork:UnitOfWork,IAlumniUserUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public AlumniUserUnitOfWork(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public IAlumniUserRepository AlumniUserRepository
        {
            get
            {
                return new AlumniUserRepository(_context);
            }
        }

        public Task<bool> CompleteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
