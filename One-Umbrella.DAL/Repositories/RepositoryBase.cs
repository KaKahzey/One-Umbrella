using OneUmbrella.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace OneUmbrella.DAL.Repositories
{
    public abstract class RepositoryBase<TId, TEntity> : ICrudRepository<TId, TEntity> where TEntity : class
    {
        protected SqlConnection connection;
        
        public RepositoryBase(SqlConnection dbConnection)
        {
            connection = dbConnection;
        }


        public abstract IEnumerable<TEntity>? getAll();
        public abstract TEntity? getById(TId id);
        public abstract bool create(TEntity entity);
        public abstract bool update(TId id, TEntity entity);
        public abstract bool delete(TId id);
    }
}
