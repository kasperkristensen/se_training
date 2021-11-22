using System;
using Microsoft.Data.Sqlite;
using se_training.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;


namespace Data.Tests
{
    public class CommentRepositoryTests
    {
        private readonly SeContext _context;

        private readonly CommentRepository _repo;

        public CommentRepositoryTests()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<SeContext>();
            builder.UseSqlite(connection);
            var context = new SeContext(builder.Options);
            context.Database.EnsureCreated();
            context.SaveChangesAsync(true);
            context.Materials.Add(new Material { Title = "How to make your own Pokemon Rom hack" });

            _context = context;
            _repo = new CommentRepository(_context);

        }

        [Fact]
        public async void Create_given_CommentCreateDTO_returns_Created_Response()
        {

            var dto = new CommentCreateDTO
            {
                Text = "True",
                UserId = "jeff",
                MaterialId = 0
            };
            Response actual = await _repo.Create(dto);
            Assert.Equal(Response.Created, actual);
        }

        [Fact]
        public async void GetById_When_Given_Id_Returns_Corresponding_Comment()
        {
            var dto = new CommentCreateDTO
            {
                Text = "True",
                UserId = "jeff",
                MaterialId = 0
            };
            await _repo.Create(dto);

            Comment actual = await _repo.Get(0);

            Assert.Equal(dto.Text, actual.Text);
        }

        [Fact]
        public async void Update_When_Given_proper_UpdateDTO_Returns_Updated()
        {

            var dto = new CommentCreateDTO
            {
                Text = "True",
                UserId = "jeff",
                MaterialId = 0
            };
            var d2 = new CommentDTO
            {
                Id = 0,
                Text = "False",
                UserId = "jeff",
                MaterialId = 0
            };
            _repo.Create(dto);
            Response actual = await _repo.Update(d2);
            Comment updated = await _repo.Get(0);

            Assert.Equal(Response.Updated, actual);
            Assert.Equal(d2.Text, updated.Text);
        }


        [Fact]
        public async void Update_When_Given_wrong_UpdateDTO_Returns_NotFound()
        {
            var dto = new CommentCreateDTO
            {
                Text = "True",
                UserId = "jeff",
                MaterialId = 0
            };
            var d2 = new CommentDTO
            {
                Id = 42,
                Text = "False",
                UserId = "jeff",
                MaterialId = 0
            };
            _repo.Create(dto);
            Response actual = await _repo.Update(d2);
            Comment updated = await _repo.Get(0);

            Assert.Equal(Response.Updated, actual);
            Assert.Equal(d2.Text, updated.Text);

        }
    }
}
