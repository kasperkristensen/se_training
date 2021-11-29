using se_training.Data;
using System.Collections.Generic;
using System.Linq;
namespace Data
{
    public class Sorter
    {
        public static IEnumerable<Material> SortbyLikes(IEnumerable<Material> materials) =>from mat in materials orderby mat.Likes.Count() descending select(mat);

        public static IEnumerable<Material> SortByUpdatedAt(IEnumerable<Material> materials, bool descending) =>(descending) ? 
        from mat in materials orderby mat.UpdatedAt.ToUniversalTime() descending select(mat):
        from mat in materials orderby mat.UpdatedAt.ToUniversalTime() select(mat);

        public static IEnumerable<Material> SortByCreatedAt(IEnumerable<Material> materials, bool descending) =>(descending) ? 
        from mat in materials orderby mat.CreatedAt.ToUniversalTime() descending select(mat):
        from mat in materials orderby mat.CreatedAt.ToUniversalTime() select(mat);

        public static IEnumerable<Material> SortbyStringRelevancy(IEnumerable<Material> materials, string SearchString) => from mat in materials orderby CalcRelevancy(mat,SearchString) descending select(mat);
        public static int CalcRelevancy(Material material, string SS){
            int NoteOverlap =  SS.ToLower().Split(' ').Intersect(material.Note.ToLower().Split(' ')).Count();
            int TitleOverlap =  SS.ToLower().Split(' ').Intersect(material.Title.ToLower().Split(' ')).Count();
            return NoteOverlap+TitleOverlap;
        }

    }
}