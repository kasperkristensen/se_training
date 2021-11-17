using System.Collections.Generic;
using System.Threading.Tasks;

namespace se_training.Data {
    public interface IRepository {
        Task GetById(int id);
        Task<Response> Create(T entity);
        Task<Response> Update(T entity);
        Task<Response> Delete(T entity);
        Task<Response> Delete(int id);
    }
}