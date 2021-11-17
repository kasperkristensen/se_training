using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace se_training.Data
{
    public class TagRepository : ITagRepository
    {
        
        private readonly SeContext _context;

        public TagRepository(SeContext context)
        {
            _context = context;
        }

        public async Task<Response> Create(TagCreateDTO entity)
        {
            var material = await _context.Materials.FindAsync(entity.MaterialId);

            if (material == null)
            {
                return Response.BadRequest;
            }

            var tag = new Tag
            {
                Value = entity.Value,
                Materials = new List<Material>() { await _context.Materials.FindAsync(entity.MaterialId) }
            };

            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
            
            return Response.Created;
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

        public async Task<Tag> GetById(int id)
        {
            return await _context.Tags.FindAsync(id);
        }

        public async Task<Response> Update(TagUpdateDTO entity)
        {
            var tag = await _context.Tags.FindAsync(entity.Id);

            if (tag == null)
            {
                return Response.NotFound;
            }

            tag.Value = entity.Value;
            tag.Materials = await entity.MaterialIds.Select(async id => await _context.Materials.FindAsync(id)).ToList();
            await _context.SaveChangesAsync();

            return Response.Updated;
        }
    }
}