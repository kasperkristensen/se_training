using System.Collections.Generic;
using System.Threading.Tasks;

namespace se_training.Data
{
    public interface ILikeRepository
    {
        Task<(Response, Like)> Create(LikeCreateDTO dto);
        Task<Response> Delete(int id);
        Task<Like> Get(int id);
        Task<IEnumerable<Like>> GetAllByMaterial(int materialId);
    }
}