using MEAlumniAssociationDUET.Core.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAlumniAssociationDUET.Service.Core
{
    public class Service<TEntity> : IService<TEntity> where TEntity : Entity
    {
        //private readonly IRepository<TEntity> repository;
        //private readonly IUnitOfWork unitOfWork;

        //public Service(IRepository<TEntity> repository, IUnitOfWork unitOfWork)
        //{
        //    this.repository = repository;
        //    this.unitOfWork = unitOfWork;
        //}

        //public virtual async Task<Guid> AddAsync(TEntity entity)
        //{
        //    await this.repository.AddAsync(entity);
        //    await this.unitOfWork.CompleteAsync();
        //    return entity.Id;
        //}

        //public virtual async Task<Guid> UpdateAsync(TEntity entity)
        //{
        //    await this.repository.UpdateAsync(entity);
        //    await this.unitOfWork.CompleteAsync();
        //    return entity.Id;
        //}

        //public virtual async Task<bool> ActiveInactiveAsync(Guid id)
        //{
        //    await this.repository.ActiveInactiveAsync(id);
        //    return await this.unitOfWork.CompleteAsync();
        //}

        //public virtual async Task<bool> DeleteAsync(Guid id)
        //{
        //    await this.repository.DeleteAsync(id);
        //    return await this.unitOfWork.CompleteAsync();
        //}

        //public virtual async Task<QueryResult<TEntity>> GetAllAsync()
        //{
        //    return await this.repository.GetAllAsync();
        //}

        //public virtual async Task<TEntity> GetByIdAsync(Guid id)
        //{
        //    return await this.repository.GetByIdAsync(id,true);
        //}

        //public virtual async Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> predicate)
        //{
        //    return await this.repository.IsExistsAsync(predicate);
        //}
    }
}
