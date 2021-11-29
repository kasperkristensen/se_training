using System;
using se_training.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace Data
{

    public class Searcher
    {
        private readonly SeContext _context;
        public Searcher(SeContext context){
            _context = context;
        }
        public IEnumerable<Material> Search(MaterialSearchDTO dto){
            var result = from mat in _context.Materials.AsEnumerable() where MaterialFitsDTO(dto,mat) select(mat);
            return result;
        }

        private bool MaterialFitsDTO(MaterialSearchDTO dto,Material mat){
            bool Tags_Matches = dto.TagValues == null || (dto.TagValues.Intersect(mat.Tags).Count())>=dto.TagValues.Count();
            bool Likes_Exceed_Minimum =dto.MinimumLikes == 0 || ((mat.Likes!= null) &&  (mat.Likes.Count>= dto.MinimumLikes));
            //bool Title_Contains_SearchString_Keyword = dto.SearchString == null || (dto.SearchString.ToLower().Split(' ').Intersect(mat.Title.ToLower().Split(' ')).Count())>=1;
            //bool Note_Contains_SearchString_Keyword = dto.SearchString == null || (dto.SearchString.ToLower().Split(' ').Intersect(mat.Note.ToLower().Split(' ')).Count())>=1;
            return Tags_Matches && Likes_Exceed_Minimum && TitleOrNoteFits(dto,mat);
        }

        private bool TitleOrNoteFits(MaterialSearchDTO dto,Material mat){
            if(dto.SearchString == null || dto.SearchString == ""){
                return true;
            }else{
                var TitleFits = (mat.Title == null) ? false : (dto.SearchString.ToLower().Split(' ').Intersect(mat.Title.ToLower().Split(' ')).Count())>=1;
                var NoteFits = (mat.Note == null) ? false : (dto.SearchString.ToLower().Split(' ').Intersect(mat.Note.ToLower().Split(' ')).Count())>=1;
                return (TitleFits || NoteFits);
            }

        }

    }
}