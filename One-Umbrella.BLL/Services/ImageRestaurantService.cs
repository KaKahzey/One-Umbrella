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
    public class ImageRestaurantService : IImageRestaurantService
    {
        private readonly IImageRestaurantRepository _imageRepository;

        public ImageRestaurantService(IImageRestaurantRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public IEnumerable<ImageRestaurant> getAllForOneRestaurant(int restaurantId)
        {
            return _imageRepository.getAllForOneRestaurant(restaurantId);
        }
        public bool create(ImageRestaurant image)
        {
            return _imageRepository.create(image);
        }
        public bool delete(int imageId)
        {
            return _imageRepository.delete(imageId);
        }
    }
}
