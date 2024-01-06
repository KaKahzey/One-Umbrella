using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.BLL.Interfaces
{
    public interface IStructuralElementService
    {
        IEnumerable<StructuralElement> getAllForOneGrid(int gridId);
        bool create(StructuralElement element);
        bool update(int elementId, StructuralElement element);
        bool delete(int elementId);
    }
}
