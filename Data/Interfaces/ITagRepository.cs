using System.Collections.Generic;
using System.Threading.Tasks;

namespace se_training.Data
{
    public interface ITagRepository
    {
        Task<(Response, Tag)> Create(TagCreateDTO entity);
        Task<Response> Update(TagUpdateDTO entity);
        Task<Response> Delete(TagUpdateDTO entity);
        Task<Response> Delete(int id);
        Task<Tag> GetById(int id);
        Task<IEnumerable<Tag>> GetAll();
    }
}