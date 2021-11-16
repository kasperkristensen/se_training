using System.Collections.Generic;
using System.Threading.Tasks;

namespace se_training.Data {
    public interface IRepository<T> where T : class {
        Task<T> GetById(int id);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);
        Task Delete(int id);
    }
}