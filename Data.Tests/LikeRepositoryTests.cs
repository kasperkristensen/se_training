using Microsoft.Data.Sqlite;
using se_training.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;
using System;

namespace Data.Tests
{
    public class LikeRepositoryTests 
    {
        private readonly SeContext _context;

        private readonly LikeRepository _repo;
        private bool disposedValue;

        public LikeRepositoryTests()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<SeContext>();
            builder.UseSqlite(connection);
            var context = new SeContext(builder.Options);
            context.Database.EnsureCreated();
            context.Materials.Add(new Material { Title = "How to make your own Pokemon Rom hack" });
            context.SaveChangesAsync(true);

            _context = context;

            _repo = new LikeRepository(_context);
        }


        [Fact]
        public async void Context_Material_Has_Material(){

            var a = _context.Materials.Count();

            Assert.Equal(1, a);
        }

        [Fact]
        public async void Create_given_LikeCreateDTO_returns_Created_Response()
        {
            var dto = new LikeCreateDTO
            {
                UserId = "jeff",
                MaterialId = 1
            };
            var response = await _repo.Create(dto);
            Like actual = response.Item2;
            var count = _context.Materials.Count();

            Assert.Equal(dto.UserId, actual.UserId);
        }

        [Fact]
        public async void GetById_When_Given_Id_Returns_Corresponding_Comment()
        {
            var dto = new LikeCreateDTO
            {
                UserId = "jeff",
                MaterialId = 1
            };
            await _repo.Create(dto);

            Like actual = await _repo.Get(1);

            Assert.Equal(dto.UserId, actual.UserId);
        }

        [Fact]
        public async void GetIdByUserIdAndMaterialId_Returns_id_given_UserId_and_liked_MaterialId()
        {
            var dto = new LikeCreateDTO
            {
                UserId = "jeff",
                MaterialId = 1
            };
            var created = await _repo.Create(dto);
            int expected = created.Item2.Id;
            int actual =  _repo.GetIdByUserIdAndMaterialId("jeff",1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void GetIdByUserIdAndMaterialId_Returns_0_given_invalid_UserId_and_liked_MaterialId()
        {
             var dto = new LikeCreateDTO
            {
                UserId = "jeff",
                MaterialId = 1
            };
            var created = await _repo.Create(dto);
            int expected = 0;
            int actual =  _repo.GetIdByUserIdAndMaterialId("joff",2);
            Console.WriteLine(actual);
            Assert.Equal(expected, actual);
        }
    }
}
