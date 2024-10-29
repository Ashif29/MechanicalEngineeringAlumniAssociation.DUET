using MEAlumniAssociationDUET.Core.Entities;
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
    public class AlumniUserRepository : Repository<AlumniUser>, IAlumniUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AlumniUserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
