using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace se_training.Data
{
    public class CommentRepository : ICommentRepository
    {
        private readonly string[] AllowedRelations = { "Material" };
        private readonly SeContext _context;

        public CommentRepository(SeContext context)
        {
            _context = context;
        }

        public async Task<Response> Create(CommentCreateDTO dto)
        {
            var material = await _context.Materials.FindAsync(dto.MaterialId);

            if (material == null)
            {
                return Response.BadRequest;
            }

            var parent = await _context.Comments.FindAsync(dto.ParentId);

            var comment = new Comment
            {
                Material = material,
                Text = dto.Text,
                UserId = dto.UserId,
                UserName = dto.UserName,
                Parent = parent
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return Response.Created;
        }

        public async Task<Response> Delete(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return Response.NotFound;
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return Response.Deleted;
        }

        public async Task<Comment> Get(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<IEnumerable<Comment>> GetAllByMaterial(int materialId)
        {
            return await _context.Comments
                .Include(c => c.Material)
                .Where(c => c.Material.Id == materialId)
                .ToListAsync();
        }

        public async Task<Response> Update(CommentDTO dto)
        {
            var comment = await _context.Comments.FindAsync(dto.Id);

            if (comment == null)
            {
                return Response.NotFound;
            }

            comment.Text = dto.Text;

            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();

            return Response.Updated;
        }
    }
}