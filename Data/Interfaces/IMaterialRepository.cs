using System.Collections.Generic;
using System.Threading.Tasks;

namespace se_training.Data
{
    public interface IMaterialRepository
    {
        Task<(Response, Material)> Create(MaterialCreateDTO dto);
        Task<Response> Update(MaterialDTO dto);
        Task<Response> Delete(int id);
        Task<Material> Get(int id);
        Task<IEnumerable<Material>> GetAll();
    }
}