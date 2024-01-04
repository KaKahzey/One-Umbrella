using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.DAL.Interfaces
{
    internal interface IStructuralElementRepository
    {
        IEnumerable<StructuralElement> getAllForOneGrid(int gridId);
    }
}
