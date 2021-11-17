using System.Collections.Generic;
using System.Threading.Tasks;

namespace se_training.Data
{
    public interface ICommentRepository
    {
        Task<Response> Create(CommentCreateDTO dto);
        Task<Response> Update(CommentDTO dto);
        Task<Response> Delete(int id);
        Task<Response> Get(int id);
        IEnumerable<Comment> GetAll();
    }
}