using OneUmbrella.BLL.Interfaces;
using OneUmbrella.DAL.Interfaces;
using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.BLL.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;

        public RatingService(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public IEnumerable<Rating> getAll(int id, bool isHuman)
        {
            return _ratingRepository.getAll(id, isHuman);
        }
        public bool create(Rating rating)
        {
            return _ratingRepository.create(rating);
        }
        public bool update(Rating rating)
        {
            return _ratingRepository.update(rating);
        }
        public bool delete(int humanId, int restaurantId)
        {
            return _ratingRepository.delete(humanId, restaurantId);
        }
    }
}
