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

        public async Task<Comment> Create(Comment entity)
        {
            var comment = _context.Comments.Add(entity);
            await _context.SaveChangesAsync();
            return comment.Entity;
        }

        public async Task Delete(Comment entity)
        {
            _context.Comments.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            await Delete(comment);
        }

        public async Task<Comment> GetById(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public Task<Comment> Update(Comment entity)
        {
            throw new System.NotImplementedException();
        }
    }
}