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

        public async Task<(Response, Material)> Create(MaterialCreateDTO dto)
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
                        MaterialId = 1,
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
                Tags = tags,
                //time quick fix
                CreatedAt = System.DateTime.UtcNow,
                UpdatedAt = System.DateTime.UtcNow
            };



            var created = _context.Materials.Add(material).Entity;
            await _context.SaveChangesAsync();

            return (Response.Created, created);
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
