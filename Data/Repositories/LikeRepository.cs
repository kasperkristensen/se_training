using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace se_training.Data
{
    public class LikeRepository : ILikeRepository
    {
        private readonly SeContext _context;

        public LikeRepository(SeContext context)
        {
            _context = context;
        }

        public async Task<(Response, Like)> Create(LikeCreateDTO dto)
        {
            var materialRepo = new MaterialRepository(_context);

            var material = await materialRepo.Get(dto.MaterialId);

            if (material == null)
            {
                return (Response.BadRequest, null);
            }

            var toCreate = new Like
            {
                UserId = dto.UserId,
                Material = material
            };

            var like = _context.Likes.Add(toCreate).Entity;
            await _context.SaveChangesAsync();

            return (Response.Created, like);
        }

        public async Task<Response> Delete(int id)
        {
            var like = await _context.Likes.FindAsync(id);

            if (like == null)
            {
                return Response.NotFound;
            }

            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();

            return Response.Deleted;
        }

        public async Task<Like> Get(int id)
        {
            return await _context.Likes.FindAsync(id);
        }

        public async Task<IEnumerable<Like>> GetAllByMaterial(int materialId) //deleted?
        {
            return await _context.Likes
                .Include(l => l.Material)
                .Where(l => l.Material.Id == materialId)
                .ToListAsync();
        }

        public int GetIdByUserIdAndMaterialId(string UserId, int MaterialId) //Async??
        {
            var like = _context.Likes
            .Where(l => l.Material.Id == MaterialId &&
                        l.UserId == UserId)
            .FirstOrDefault();

            if( like != null)
            {
                return like.Id;
            }
            else return 0;
        }
    }
}