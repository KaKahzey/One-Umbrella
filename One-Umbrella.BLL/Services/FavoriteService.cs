using OneUmbrella.BLL.Interfaces;
using OneUmbrella.DAL.Interfaces;
using OneUmbrella.DAL.Repositories;
using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.BLL.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoriteService(IFavoriteRepository favorieRepository)
        {
            _favoriteRepository = favorieRepository;
        }

        public IEnumerable<Restaurant> getAllForHuman(int id)
        {
            return _favoriteRepository.getAllForHuman(id); 
        }
        public bool create(Favorite favorite)
        {
            return _favoriteRepository.create(favorite);
        }
        public bool delete(int humanId, int restaurantId)
        {
            return _favoriteRepository.delete(humanId, restaurantId);
        }
    }
}
