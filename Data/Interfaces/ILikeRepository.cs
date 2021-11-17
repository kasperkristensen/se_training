using System.Collections.Generic;
using System.Threading.Tasks;

namespace se_training.Data
{
    public interface ILikeRepository
    {
        Task<Response> Create(LikeCreateDTO dto);
        Task<Response> Update(Like dto);
        Task<Response> Delete(int id);
        Like Get(int id);
        IEnumerable<Like> GetAll();
    }
}