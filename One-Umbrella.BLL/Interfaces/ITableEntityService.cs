using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.BLL.Interfaces
{
    public interface ITableEntityService
    {
        IEnumerable<TableEntity> getAllForOneGrid(int gridId);
        TableEntity getById(int tableId);
        bool create(TableEntity table);
        bool update(int tableId, TableEntity table);
        bool delete(int tableId);
    }
}
