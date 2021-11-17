using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace se_training.Data
{
    public class CommentRepository : IRepository<Comment>
    {
        private readonly string[] AllowedRelations = { "Material" };
        private readonly SeContext _context;

        public CommentRepository(SeContext context)
        {
            _context = context;
        }

        public async Task<Response> Create(Comment entity)
        {
            var comment = _context.Comments.Add(entity);
            await _context.SaveChangesAsync();
            
            return Response.Created;
        }

        public async Task<Response> Delete(Comment entity)
        {
            _context.Comments.Remove(entity);
            await _context.SaveChangesAsync();
            return Response.Deleted;
        }

        public async Task<Response> Delete(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            await Delete(comment);
            return Response.Deleted;
        }

        public async Task<Comment> GetById(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<Response> Update(CommentDTO entity)
        {
            var comment = _context.Comments.Find(entity.Id);

            if (comment == null)
            {
                return Response.NotFound;
            }

            comment.Text = entity.Text;
            await _context.SaveChangesAsync();

            return Response.Updated;
        }
    }
}