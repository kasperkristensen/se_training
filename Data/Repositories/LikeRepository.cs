using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace se_training.Data
{
    public class LikeRepository : IRepository<Like>
    {
        private readonly SeContext _context;

        public LikeRepository(SeContext context)
        {
            _context = context;
        }

        public async Task<Like> Create(LikeCreateDTO entity)
        {
            var L = DTO2Like(entity);
            var like = _context.Likes.Add(L);
            await _context.SaveChangesAsync();
            return like.Entity;
        }

        public async Task Delete(Like entity)
        {
            _context.Likes.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var like = await _context.Likes.FindAsync(id);
            await Delete(like);
        }

        public async Task<IEnumerable<Like>> GetAllByMaterialId(int materialId)
        {
            var likes = _context.Likes.Where(l => l.Material.Id == materialId);
            return await likes.ToListAsync();
        }

        public async Task<Like> GetById(int id)
        {
            return await _context.Likes.FindAsync(id);
        }

        public Task<Like> Update(Like entity)
        {
            throw new System.NotImplementedException();
        }
    }
}