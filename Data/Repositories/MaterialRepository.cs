using System.Collections.Generic;
using System.Threading.Tasks;

namespace  se_training.Data
{
    public class MaterialRepository : IRepository<Material>
    {
        private readonly SeContext _context;

        public MaterialRepository(SeContext context)
        {
            _context = context;
        }

        public async Task<Material> Create(MaterialCreateDTO entity)
        {
            var Mat = DTO2Material(entity);
            var material = _context.Materials.Add(Mat);
            await _context.SaveChangesAsync();
            return material.Entity;
        }

        private Material DTO2Material(MaterialCreateDTO entity){
            return new Material();
        }

        public async Task Delete(Material entity)
        {
            _context.Materials.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var material = await _context.Materials.FindAsync(id);
            await Delete(material);
        }

        public async Task<Material> GetById(int id)
        {
            return await _context.Materials.FindAsync(id);
        }

        public Task<Response> Update(MaterialUpdateDTO entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
        