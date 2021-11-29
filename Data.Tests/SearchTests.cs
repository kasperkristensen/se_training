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
            context.Materials.Add(new Material { Title = "How to make your own Pokemon Rom hack" , Tags = new List<Tag>()});
            context.SaveChangesAsync(true);

            _context = context;
            _repo = new MaterialRepository(_context);
            _search = new Searcher(_context);
        }


        [Fact]
        public async void Search_When_Given_SearchDTO_With_Tags_Returns_Material_With_All_Tags_In_DTO(){
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

            var tags = mat2.Item2.Tags;

            var d2 = new MaterialSearchDTO{
                SearchString = "",
                MinimumLikes = 0,
                TagValues = tags
            };
            var actual =from Material m in _search.Search(d2) select(m.Id);

            Assert.Equal(new List<int>{1,3} , actual);

        }


        [Fact]
        public async void Search_When_Given_SearchDTO_With_Likes_Returns_Materials_With_Enough_Likes(){
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
                 mat2.Item2.Likes.Add(new Like{});
                 mat.Item2.Likes.Add(new Like{});
             }



            var d2 = new MaterialSearchDTO{
                MinimumLikes = 5
            };
            var actual =from Material m in _search.Search(d2) select(m.Id);

            Assert.Equal(new List<int>{2} , actual);

        }



        [Fact]
        public async void Search_When_Given_SearchDTO_With_SearchString_Returns_Materials_String_In_Title_Or_Note(){
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
            var dtour = new MaterialCreateDTO
            {
                Title = "Java for monkeys 3",
                Note = "Anything",
                UserId = "69",
                TagValues = new List<string> { "tag1", "tag2" }
            };
             var mat2 = await _repo.Create(d);
             var mat3 = await _repo.Create(dtour);


            var d2 = new MaterialSearchDTO{
                SearchString = "something"
            };
            var actual =from Material m in _search.Search(d2) select(m.Id);

            Assert.Equal(new List<int>{2,3} , actual);

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