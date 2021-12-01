using System;
using Microsoft.Data.Sqlite;
using se_training.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Xunit;


namespace Data.Tests
{
    public class TagRepositoryTests
    {
        private readonly SeContext _context;

        private readonly TagRepository _repo;

        public TagRepositoryTests()
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
            _repo = new TagRepository(_context);

        }

        [Fact]
        public async void Create_given_TagCreateDTO_returns_Created_Response()
        {

            var dto = new TagCreateDTO
            {
                Value = "Yep",
                MaterialId = 1
            };
            var response = await _repo.Create(dto);
            Response actual = response.Item1;
            Assert.Equal(Response.Created, actual);
        }

        [Fact]
        public async void GetById_When_Given_Id_Returns_Corresponding_Tag()
        {
            var dto = new TagCreateDTO
            {
                Value = "Yep",
                MaterialId = 1
            };
            await _repo.Create(dto);

            Tag actual = await _repo.GetById(1);

            Assert.Equal(dto.Value, actual.Value);
        }

        [Fact]
        public async void Update_When_Given_proper_UpdateDTO_Returns_Updated()
        {

            var dto = new TagCreateDTO
            {
                Value = "ye",
                MaterialId = 1
            };
            var d2 = new TagUpdateDTO
            {
                Id = 1,
                Value = "Yep",
                MaterialIds = new List<int>{1}
            };
            _repo.Create(dto);
            Response actual = await _repo.Update(d2);
            Tag updated = await _repo.GetById(1);

            Assert.Equal(Response.Updated, actual);
            Assert.Equal(d2.Value, updated.Value);
        }


        [Fact]
        public async void Update_When_Given_wrong_UpdateDTO_Returns_NotFound()
        {

            var dto = new TagCreateDTO
            {
                Value = "ye",
                MaterialId = 1
            };
            var d2 = new TagUpdateDTO
            {
                Id = 3460,
                Value = "Yep",
            };
            _repo.Create(dto);
            Response actual = await _repo.Update(d2);
            Tag updated = await _repo.GetById(1);

            Assert.Equal(Response.NotFound, actual);
            Assert.Equal("ye", updated.Value);

        }
    }
}
