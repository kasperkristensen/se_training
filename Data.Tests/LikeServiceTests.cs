using Microsoft.Data.Sqlite;
using se_training.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;
using System;

namespace Data.Tests
{
    public class LikeServiceTests 
    {
        private readonly SeContext _context;

        private readonly LikeService _likeservice;
        private bool disposedValue;

        public LikeServiceTests()
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

            _likeservice = new LikeService(_context);
        }

         [Fact]
        public async void CreateLike_given_UserId_and_MaterialId_creates_Like()
        {
            
            var actual = await _likeservice.CreateLike("jeff", 1);

            var expected = Response.Created;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void RemoveLike_given_valid_UserId_and_MaterialId_returns_Deleted_response()
        {
            await _likeservice.CreateLike("jeff", 1);

            var actual = await _likeservice.RemoveLike("jeff", 1);

            var expected = Response.Deleted;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public async void RemoveLike_given_invalid_UserId_and_MaterialId_returns_NotFound_response()
        {
            await _likeservice.CreateLike("jeff", 1);

            var actual = await _likeservice.RemoveLike("joff", 2);

            var expected = Response.NotFound;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void GetLikeCountByMaterialId_given_material_id_returns_amount_of_likes()
        {
            await _likeservice.CreateLike("jeff", 1);
            await _likeservice.CreateLike("joff", 1);
            await _likeservice.CreateLike("juff", 1);

            var actual = _likeservice.GetLikeCountByMaterialId(1);

            var expected = 3;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void GetLikeCountByMaterialId_given_material_id_returns_amount_of_likes_after_remove()
        {
            await _likeservice.CreateLike("jeff", 1);
            await _likeservice.CreateLike("joff", 1);
            await _likeservice.CreateLike("juff", 1);

            await _likeservice.RemoveLike("joff", 1);

            var actual = _likeservice.GetLikeCountByMaterialId(1);

            var expected = 2;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void UserHasLikedMaterial_returns_false_when_user_has_not_liked_material()
        {
            var actual = _likeservice.UserHasLikedMaterial("jeff",1);

            Assert.False(actual);
        }

        [Fact]
        public async void UserHasLikedMaterial_returns_true_when_user_has_liked_material()
        {
            await _likeservice.CreateLike("jeff", 1);
            var actual = _likeservice.UserHasLikedMaterial("jeff",1);

            Assert.True(actual);
        }
    }
}