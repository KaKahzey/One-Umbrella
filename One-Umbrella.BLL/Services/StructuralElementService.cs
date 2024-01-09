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
    public class StructuralElementService : IStructuralElementService
    {
        private readonly IStructuralElementRepository _structuralElementRepository;

        public StructuralElementService(IStructuralElementRepository structuralElementRepository)
        {
            _structuralElementRepository = structuralElementRepository;
        }

        public IEnumerable<StructuralElement> getAllForOneGrid(int gridId)
        {
            return _structuralElementRepository.getAllForOneGrid(gridId);
        }
        public bool create(StructuralElement element)
        {
            return _structuralElementRepository.create(element);
        }
        public bool update(int elementId, StructuralElement element)
        {
            return _structuralElementRepository.update(elementId, element);
        }
        public bool delete(int elementId)
        {
            return _structuralElementRepository.delete(elementId);
        }
    }
}
