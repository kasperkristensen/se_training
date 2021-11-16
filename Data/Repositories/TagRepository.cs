using System.Collections.Generic;
using System.Threading.Tasks;

namespace se_training.Data
{
    public class TagRepository : IRepository<Tag>
    {
        
        private readonly SeContext _context;

        public TagRepository(SeContext context)
        {
            _context = context;
        }

        public async Task<Tag> Create(Tag entity)
        {
            var tag = _context.Tags.Add(entity);
            await _context.SaveChangesAsync();
            return tag.Entity;
        }

        public async Task Delete(Tag entity)
        {
            _context.Tags.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            await Delete(tag);
        }

        public async Task<Tag> GetById(int id)
        {
            return await _context.Tags.FindAsync(id);
        }

        public Task<Tag> Update(Tag entity)
        {
            throw new System.NotImplementedException();
        }
    }
}