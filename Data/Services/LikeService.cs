using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace se_training.Data
{
    public class LikeService
    {
        private LikeRepository _likeRepository;

        public LikeService(SeContext context)
        {
            _likeRepository = new LikeRepository(context);
        }

        public async Task<IEnumerable<Like>> GetLikesByMaterialId(int id)
        {
            var likes = await _likeRepository.GetAllByMaterial(id);
            
            return likes.Where(l => l.DeletedAt == null); //deleted??
        }

        public int GetLikeCountByMaterialId(int id) //TODO: make this async?
        {
             return GetLikesByMaterialId(id).Result.Count();
        }

        public async Task<Response> CreateLike(string UserId, int MaterialId) //Kan den her være void?
        {
            var (response, like) = await _likeRepository.Create(new LikeCreateDTO{
                UserId = UserId,
                MaterialId = MaterialId
            });

            if (response != Response.Created)
            {
                throw new Exception(String.Format("Error: {0}", response));
            }
            
            return response;
        }

        public async Task<Response> RemoveLike(string UserId, int MaterialId)
        {
            var response = await _likeRepository
            .Delete(_likeRepository.GetIdByUserIdAndMaterialId(UserId, MaterialId));
            
            if (response != Response.Deleted)
            {
                return Response.NotFound;
            }
            
            return response;
        }

        public bool UserHasLikedMaterial(string UserId, int MaterialId) //async??
        {
            return _likeRepository.GetIdByUserIdAndMaterialId(UserId, MaterialId) != 0;
        }




    }
}