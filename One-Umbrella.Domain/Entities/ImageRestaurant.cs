﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.Domain.Entities
{
    public class ImageRestaurant
    {
        public int ImageId { get; set; }
        public int RestaurantId { get; set; }
        public byte[]? ImageData { get; set; }
        public bool IsFront { get; set; }
        public bool IsMenu { get; set; }
    }
}
