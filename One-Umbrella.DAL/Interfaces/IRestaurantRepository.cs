﻿using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.DAL.Interfaces
{
    internal interface IRestaurantRepository : ICrudRepository<int, Restaurant>
    {
        Restaurant? getByIdentifier(string identifier);
        IEnumerable<Restaurant>? getByPageAndSorted(int page, int pageSize, string sortBy, bool isDescending, int? humanId, string? city);
        int getTotalRestaurants();
    }
}
