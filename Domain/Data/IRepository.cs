using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Data
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> FindOne(Guid id);
        Task AddAsync(T obj);
        Task Update(T obj);
        Task Remove(Guid id);
    }

}
