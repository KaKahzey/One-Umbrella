﻿using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.BLL.Interfaces
{
    public interface IReservedTableService
    {
        IEnumerable<ReservedTable> getAllForOneRestaurantForOneDay(int restaurantId, DateTime date);
        bool create(int reservationId, int tableId);
    }
}
