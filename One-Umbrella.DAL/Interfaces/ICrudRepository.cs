﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.DAL.Interfaces
{
    internal interface ICrudRepository<TId, TEntity> where TEntity : class
    {
        IEnumerable<TEntity>? getAll();
        TEntity? getById(TId id);
        bool create(TEntity entity);
        bool update(TId id, TEntity entity);
        bool delete(TId id);


    }
}
