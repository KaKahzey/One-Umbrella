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
    public class TableEntityService : ITableEntityService
    {
        private readonly ITableEntityRepository _tableEntityRepository;

        public TableEntityService(ITableEntityRepository tableEntityRepository)
        {
            _tableEntityRepository = tableEntityRepository;
        }

        public IEnumerable<TableEntity> getAllForOneGrid(int gridId)
        {
            return _tableEntityRepository.getAllForOneGrid(gridId);
        }
        public TableEntity getById(int tableId)
        {
            return _tableEntityRepository.getById(tableId);
        }
        public bool create(TableEntity table)
        {
            return _tableEntityRepository.create(table);
        }
        public bool update(int tableId, TableEntity table)
        {
            return _tableEntityRepository.update(tableId, table);
        }
        public bool delete(int tableId)
        {
            return _tableEntityRepository.delete(tableId);
        }
    }
}
