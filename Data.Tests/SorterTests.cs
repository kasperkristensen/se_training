using System;
using Microsoft.Data.Sqlite;
using se_training.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;


namespace Data.Tests
{
    public class SorterTests : IDisposable
    {
        private readonly SeContext _context;

        private readonly MaterialRepository _repo;
        private readonly Searcher _search;

        public SorterTests()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<SeContext>();
            builder.UseSqlite(connection);
            var context = new SeContext(builder.Options);
            context.Database.EnsureCreated();
            context.Materials.Add(new Material { Title = "How to make your own Pokemon Rom hack", Note = "", Likes = new List<Like>(), UpdatedAt = System.DateTime.UtcNow, CreatedAt = DateTime.UtcNow});
            context.SaveChangesAsync(true);

            _context = context;
            _repo = new MaterialRepository(_context);
            _search = new Searcher(_context);
        }


        [Fact]
        public async void SortByUpdatedAtTest(){
            var dto = new MaterialCreateDTO
            {
                Title = "Java for monkeys 1",
                Note = "something",
                UserId = "69",
                TagValues = new List<string> { "tag4", "tag5" }
            };
             var id2 = await _repo.Create(dto);

            var d = new MaterialCreateDTO
            {
                Title = "Java for monkeys 2",
                Note = "something",
                UserId = "69",
                TagValues = new List<string> { "tag1", "tag2" }
            };
             var id3 = await _repo.Create(d);

             
            var d2 = new MaterialCreateDTO
            {
                Title = "Java for monkeys 2",
                Note = "something",
                UserId = "69",
                TagValues = new List<string> { "tag1", "tag2" }
            };
             var id4 = await _repo.Create(d2);

             var id1 = await _repo.Get(1);
             
             id1.UpdatedAt = System.DateTime.UtcNow;
             //id3.Item2.UpdatedAt = DateTime.UtcNow;
             id4.Item2.UpdatedAt = System.DateTime.UtcNow;
             id2.Item2.UpdatedAt = System.DateTime.UtcNow;



            var actual =from Material m in Sorter.SortByUpdatedAt(_context.Materials, true) select(m.Id);
            var actual2 =from Material m in Sorter.SortByUpdatedAt(_context.Materials, false) select(m.Id);
            //var actual =from Material m in _context.Materials.AsEnumerable() select(m.UpdatedAt);

            //Assert.Equal(new List<DateTime>{DateTime.Now} , actual);
            Assert.Equal(new List<int>{2,4,1,3} , actual);
            Assert.Equal(new List<int>{3,1,4,2} , actual2);
        }

        [Fact]
        public async void SortByCreatedAtTest(){
            var dto = new MaterialCreateDTO
            {
                Title = "Java for monkeys 1",
                Note = "something",
                UserId = "69",
                TagValues = new List<string> { "tag4", "tag5" }
            };
             var id2 = await _repo.Create(dto);

            var d = new MaterialCreateDTO
            {
                Title = "Java for monkeys 2",
                Note = "something",
                UserId = "69",
                TagValues = new List<string> { "tag1", "tag2" }
            };
             var id3 = await _repo.Create(d);

             
            var d2 = new MaterialCreateDTO
            {
                Title = "Java for monkeys 2",
                Note = "something",
                UserId = "69",
                TagValues = new List<string> { "tag1", "tag2" }
            };
             var id4 = await _repo.Create(d2);

             var id1 = await _repo.Get(1);
             
             id1.UpdatedAt = System.DateTime.UtcNow;
             //id3.Item2.UpdatedAt = DateTime.UtcNow;
             id4.Item2.UpdatedAt = System.DateTime.UtcNow;
             id2.Item2.UpdatedAt = System.DateTime.UtcNow;



            var actual =from Material m in Sorter.SortByCreatedAt(_context.Materials, true) select(m.Id);
            var actual2 =from Material m in Sorter.SortByCreatedAt(_context.Materials, false) select(m.Id);
            //var actual =from Material m in _context.Materials.AsEnumerable() select(m.UpdatedAt);

            //Assert.Equal(new List<DateTime>{DateTime.Now} , actual);
            Assert.Equal(new List<int>{4,3,2,1} , actual);
            Assert.Equal(new List<int>{1,2,3,4} , actual2);
        }


        [Fact]
        public async void SortbyLikes_returns_List_Sorted_by_Likes(){
            var dto = new MaterialCreateDTO
            {
                Title = "Java for monkeys 1",
                Note = "something",
                UserId = "69",
                TagValues = new List<string> { "tag4", "tag5" }
            };
             var mat = await _repo.Create(dto);

            var d = new MaterialCreateDTO
            {
                Title = "Java for monkeys 2",
                Note = "something",
                UserId = "69",
                TagValues = new List<string> { "tag1", "tag2" }
            };
             var mat2 = await _repo.Create(d);

             mat.Item2.Likes=new List<Like>();
             mat2.Item2.Likes=new List<Like>();
             
             for(int i = 0; i <10; i++){
                 if(i<3)
                 mat.Item2.Likes.Add(new Like{});
                 mat2.Item2.Likes.Add(new Like{});
             }



            var d2 = new MaterialSearchDTO{
                MinimumLikes = 5
            };
            var actual =from Material m in Sorter.SortbyLikes(_context.Materials.AsEnumerable()) select(m.Id);

            Assert.Equal(new List<int>{3,2,1} , actual);

        }



        [Fact]
        public async void SortbyStringRelevancy_returns_sorted_string(){
            var dto = new MaterialCreateDTO
            {
                Title = "Java for monkeys 1",
                Note = "something",
                UserId = "69",
                TagValues = new List<string> { "tag4", "tag5" }
            };
             var mat = await _repo.Create(dto);

            var d = new MaterialCreateDTO
            {
                Title = "something for monkeys 2",
                Note = "The sequel",
                UserId = "69",
                TagValues = new List<string> { "tag1", "tag2" }
            };
            var d2 = await _repo.Create(d);
            var dtour = new MaterialCreateDTO
            {
                Title = "Java for monkeys 3",
                Note = "Anything",
                UserId = "69",
                TagValues = new List<string> { "tag1", "tag2" }
            };
             var mat2 = await _repo.Create(dtour);

            var actual =from Material m in Sorter.SortbyStringRelevancy(_context.Materials, "Something Java Monkeys") select(m.Id);

            Assert.Equal(new List<int>{2,3, 4, 1} , actual);

        }

        /*
        [Fact]
        public void confirmation(){

        }
        */






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