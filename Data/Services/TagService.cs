using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace se_training.Data
{
    public class TagService
    {
        private TagRepository _tagRepository;

        public TagService(SeContext context)
        {
            _tagRepository = new TagRepository(context);
        }

        public async Task<Tag> GetTag(int id)
        {
            var tag = await _tagRepository.GetById(id);

            if (tag == null)
            {
                throw new Exception("Tag not found");
            }

            return tag;
        }

        public async Task<Tag> GetTagByName(string value)
        {
            var tag = await _tagRepository.GetByValue(value);

            if (tag == null)
            {
                throw new Exception("Tag not found");
            }

            return tag;
        }

        public async Task<IEnumerable<Tag>> GetAllTags()
        {
            return await _tagRepository.GetAll();
        }

        public async Task<Tag> CreateTag(TagCreateDTO dto)
        {
            var (response, tag) = await _tagRepository.Create(dto);

            if (response != Response.Created)
            {
                throw new Exception(String.Format("Error: {0}", response));
            }

            return tag;
        }

        public async Task<Response> UpdateTag(TagUpdateDTO dto)
        {
            var response = await _tagRepository.Update(dto);

            if (response != Response.Updated)
            {
                throw new Exception(String.Format("Error: {0}", response));
            }

            return response;
        }

        public async Task<Response> DeleteTag(int id)
        {
            var response = await _tagRepository.Delete(id);

            if (response != Response.Deleted)
            {
                throw new Exception(String.Format("Error: {0}", response));
            }

            return response;
        }
    }
}