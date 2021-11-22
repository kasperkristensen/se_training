using System.Collections.Generic;
using System.Threading.Tasks;

namespace se_training.Data
{
    public interface ICommentRepository
    {
        Task<Response> Create(CommentCreateDTO dto);
        Task<Response> Update(CommentDTO dto);
        Task<Response> Delete(int id);
        Task<Comment> Get(int id);
        Task<IEnumerable<Comment>> GetAllByMaterial(int materialId);
    }
}