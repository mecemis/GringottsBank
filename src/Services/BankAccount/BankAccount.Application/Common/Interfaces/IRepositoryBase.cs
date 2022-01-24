using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BankAccount.Domain.Common;

namespace BankAccount.Application.Common.Interfaces
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeString = null,
            bool disableTracking = true);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate,
            string includeString = null,
            bool disableTracking = true);
    }
}