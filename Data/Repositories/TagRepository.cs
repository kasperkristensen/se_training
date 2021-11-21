using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace se_training.Data
{
    public class TagRepository : ITagRepository
    {

        private readonly SeContext _context;

        public TagRepository(SeContext context)
        {
            _context = context;
        }

        public async Task<(Response, Tag)> Create(TagCreateDTO entity)
        {
            var material = await _context.Materials.FindAsync(entity.MaterialId);

            if (material == null)
            {
                return (Response.BadRequest, null);
            }

            var toCreate = new Tag
            {
                Value = entity.Value,
                Materials = new List<Material>() { await _context.Materials.FindAsync(entity.MaterialId) }
            };

            var tag = _context.Tags.Add(toCreate).Entity;
            await _context.SaveChangesAsync();

            return (Response.Created, tag);
        }

        public async Task<Response> Delete(TagUpdateDTO entity)
        {
            var tag = await _context.Tags.FindAsync(entity.Id);

            if (tag == null)
            {
                return Response.NotFound;
            }

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();

            return Response.Deleted;
        }

        public async Task<Response> Delete(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            await Delete(new TagUpdateDTO { Id = id });

            return Response.Deleted;
        }

        public async Task<IEnumerable<Tag>> GetAll()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag> GetById(int id)
        {
            return await _context.Tags.FindAsync(id);
        }

        public async Task<Tag> GetByValue(string value)
        {
            return await _context.Tags.FirstOrDefaultAsync(t => t.Value == value);
        }

        public async Task<Response> Update(TagUpdateDTO entity)
        {
            var tag = await _context.Tags.FindAsync(entity.Id);

            if (tag == null)
            {
                return Response.NotFound;
            }

            var materials = new List<Material>();

            foreach (var materialId in entity.MaterialIds)
            {
                materials.Add(await _context.Materials.FindAsync(materialId));
            }

            tag.Value = entity.Value;
            tag.Materials = materials;
            await _context.SaveChangesAsync();

            return Response.Updated;
        }
    }
}