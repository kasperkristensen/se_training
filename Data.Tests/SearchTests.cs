using System;
using Microsoft.Data.Sqlite;
using se_training.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;


namespace Data.Tests
{
    public class SearchTests : IDisposable
    {
        private readonly SeContext _context;

        private readonly MaterialRepository _repo;
        private readonly Searcher _search;

        public SearchTests()
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
            _search = new Searcher(_context);
        }

        //[Fact]
        public async void Context_Has_Materials(){
            var dto = new MaterialCreateDTO
            {
                Title = "Java for monkeys",
                Note = "something",
                UserId = "69",
                TagValues = new List<string> { "tag1", "tag2" }
            };
             await _repo.Create(dto);
            var a = 2;
            Assert.Equal(2,a);
        }
        [Fact]
        public async void Search_When_Given_SearchDTO_Returns_Material_With_All_Tags_In_DTO(){
            var dto = new MaterialCreateDTO
            {
                Title = "Java for monkeys",
                Note = "something",
                UserId = "69",
                TagValues = new List<string> { "tag4", "tag5" }
            };
             await _repo.Create(dto);

            var d = new MaterialCreateDTO
            {
                Title = "Java for monkeys",
                Note = "something",
                UserId = "69",
                TagValues = new List<string> { "tag1", "tag2" }
            };
             await _repo.Create(d);

            await _repo.Create(dto);
            var mat = await _repo.Get(2);
            var tags = mat.Tags;

            var d2 = new MaterialSearchDTO{
                SearchString = "",
                LowerBoundRating = 0,
                UpperBoundRating = 5,
                TagValues = tags
            };
            var actual =_search.Search(d2);
            var expected = new List<int>{2};
            var mat1 = await _repo.Get(2);
            var mat2 = await _repo.Get(3);

            var t1 = new List<string>{"a","b"};
            var t2 = new List<string>{"b","c"};
            var a = t1.Intersect(t2);
            var pc1 = from tag in mat1.Tags select (tag.Value);
            var pc2 = from tag in mat2.Tags select (tag.Value);
            var intersect = from t in mat1.Tags where pc1.Intersect(pc2).Contains(t.Value) select t.Value;

            Assert.Equal(new List<int>{1,3} , actual);

        }

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        
    }
}