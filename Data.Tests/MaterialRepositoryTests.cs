using System;
using Microsoft.Data.Sqlite;
using se_training.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;


namespace Data.Tests
{
    public class MaterialRepositoryTests
    {
        private readonly SeContext _context;

        private readonly MaterialRepository _repo;

        public MaterialRepositoryTests()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<SeContext>();
            builder.UseSqlite(connection);
            var context = new SeContext(builder.Options);
            context.Database.EnsureCreated();
            context.Materials.Add(new Material {Title = "How to make your own Pokemon Rom hack"});
            context.SaveChangesAsync(true);

            _context = context;
            _repo = new MaterialRepository(_context);

        }

        [Fact]
        public void Create_given_Material_returns_Material_with_Id()
        {
            
        }
    }
}
