using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace se_training.Data
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly SeContext _context;

        public MaterialRepository(SeContext context)
        {
            _context = context;
        }

        public async Task<Response> Create(MaterialCreateDTO dto)
        {
            var tagRepo = new TagRepository(_context);

            var tags = new List<Tag>();

            foreach (var value in dto.TagValues)
            {
                var tag = await tagRepo.GetByValue(value);

                if (tag == null)
                {
                    var tagDTO = new TagCreateDTO
                    {
                        Value = value
                    };
                    var result = await tagRepo.Create(tagDTO);

                    if (result.Item2 != null)
                    {
                        tag = result.Item2;
                    }
                }

                tags.Add(tag);
            }

            var material = new Material
            {
                Title = dto.Title,
                Note = dto.Note,
                AuthorName = dto.AuthorName,
                VideoUrl = dto.VideoUrl,
                Tags = tags
            };

            _context.Materials.Add(material);
            await _context.SaveChangesAsync();

            return Response.Created;
        }

        public async Task<Response> Delete(int id)
        {
            var material = _context.Materials.Find(id);

            if (material == null)
            {
                return Response.NotFound;
            }

            _context.Materials.Remove(material);
            await _context.SaveChangesAsync();

            return Response.Deleted;
        }

        public async Task<Material> Get(int id)
        {
            return await _context.Materials.FindAsync(id);
        }

        public async Task<IEnumerable<Material>> GetAll()
        {
            return await _context.Materials.ToListAsync();
        }

        public async Task<Response> Update(MaterialDTO dto)
        {
            var material = await _context.Materials.FindAsync(dto.Id);

            if (material == null)
            {
                return Response.NotFound;
            }

            material.Title = dto.Title;
            material.Note = dto.Note;
            material.VideoUrl = dto.VideoUrl;

            _context.Update(material);
            await _context.SaveChangesAsync();

            return Response.Updated;
        }
    }
}
