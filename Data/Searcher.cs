using System;
using se_training.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace Data
{
    public record MaterialSearchDTO
    {
        [Required]
        public string SearchString { get; init; }

        public int UpperBoundRating {get; init;}
        public int LowerBoundRating {get; init;}


        public IEnumerable<Tag> TagValues { get; init; }
    }
    
    public class Searcher
    {
        private readonly SeContext _context;
        public Searcher(SeContext context){
            _context = context;
        }
        public IEnumerable<int> Search(MaterialSearchDTO dto){
            var result = from mat in _context.Materials.AsEnumerable() where (dto.TagValues.Intersect(mat.Tags).Count())>=dto.TagValues.Count() select(mat.Id);
            return result;
        }

    }
}