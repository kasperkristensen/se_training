using System;
using Microsoft.Data.Sqlite;
using se_training.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            context.Materials.Add(new Material { Title = "How to make your own Pokemon Rom hack" });
            context.SaveChangesAsync(true);

            _context = context;
            _repo = new MaterialRepository(_context);

        }

        [Fact]
        public async void Create_given_Material_returns_Material_with_Id()
        {

            var dto = new MaterialCreateDTO
            {
                Title = "Java for monkeys",
                Note = "something",
                UserId = "69",
                TagValues = new List<string>{"tag1", "tag2"}
            };
            var response = await _repo.Create(dto);
            Material actual = response.Item2;
            var currentTime = DateTime.UtcNow;

            Assert.Equal(dto.Title, actual.Title);
            Assert.Equal(dto.Note, actual.Note);
            Assert.Equal(currentTime, actual.CreatedAt, precision: TimeSpan.FromSeconds(1));
            Assert.Equal(currentTime, actual.UpdatedAt, precision: TimeSpan.FromSeconds(1));
        }

        [Fact]
        public async void GetById_When_Given_Id_Returns_Corresponding_Material()
        {

            var dto = new MaterialCreateDTO
            {
                Title = "Java for monkeys",
                Note = "something",
                UserId = "69",
                TagValues = new List<string>{"tag1", "tag2"}
            };
            var d2 = new MaterialCreateDTO
            {
                Title = "Java for monkeys 2",
                Note = "something else",
                UserId = "69",
                TagValues = new List<string>{"tag1", "tag2"}
            };
            await _repo.Create(dto);
            await _repo.Create(d2);
            //Get(3) since we also have added a thing in the contructor
            Material actual = await _repo.Get(3);

            var currentTime = DateTime.UtcNow;

            Assert.Equal(d2.Title, actual.Title);
        }

        [Fact]
        public async void Update_When_Given_proper_UpdateDTO_Returns_Updated()
        {

            var dto = new MaterialCreateDTO
            {
                Title = "Java for monkeys",
                Note = "something",
                UserId = "69",
                TagValues = new List<string>{"tag1", "tag2"}
            };
            var d2 = new MaterialDTO
            {
                Id = 2,
                Title = "Java for monkeys 2",
                Note = "something else",
                UserId = "69",
                TagValues = new List<string>{"tag1", "tag2"}
            };
            _repo.Create(dto);
            Response actual = await _repo.Update(d2);
            Material updated = await _repo.Get(2);

            Assert.Equal(Response.Updated, actual);
            Assert.Equal(d2.Title, updated.Title);
        }

        [Fact]
        public async void Update_When_Given_UpdateDTO_With_Bad_Id_Returns_NotFound()
        {

            var dto = new MaterialCreateDTO
            {
                Title = "Java for monkeys",
                Note = "something",
                UserId = "69",
                TagValues = new List<string>{"tag1", "tag2"}
            };
            var d2 = new MaterialDTO
            {
                Id = 27,
                Title = "Java for monkeys 2",
                Note = "something else",
                UserId = "69"
            };
            _repo.Create(dto);
            Response actual = await _repo.Update(d2);
            Material updated = await _repo.Get(2);

            Assert.Equal(Response.NotFound, actual);
            Assert.Equal(dto.Title, updated.Title);
        }

    }
}
