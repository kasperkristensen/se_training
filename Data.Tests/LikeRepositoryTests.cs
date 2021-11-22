using Microsoft.Data.Sqlite;
using se_training.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;


namespace Data.Tests
{
    public class LikeRepositoryTests
    {
        private readonly SeContext _context;

        private readonly LikeRepository _repo;

        public LikeRepositoryTests()
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
            _repo = new LikeRepository(_context);

        }

        [Fact]
        public async void Create_given_LikeCreateDTO_returns_Created_Response()
        {

            var dto = new LikeCreateDTO
            {
                UserId = "jeff",
                MaterialId = 0
            };
            var response = await _repo.Create(dto);
            Like actual = response.Item2;
            Assert.Equal(dto.UserId, actual.UserId);
        }

        [Fact]
        public async void GetById_When_Given_Id_Returns_Corresponding_Comment()
        {
            var dto = new LikeCreateDTO
            {
                UserId = "jeff",
                MaterialId = 0
            };
            await _repo.Create(dto);

            Like actual = await _repo.Get(0);

            Assert.Equal(dto.UserId, actual.UserId);
        }
    }
}
