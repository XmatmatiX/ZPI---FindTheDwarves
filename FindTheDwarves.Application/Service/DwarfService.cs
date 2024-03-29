using FindTheDwarves.Application.Interface;
using FindTheDwarves.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDwarves.Application.Service
{
    public class DwarfService : IDwarfService
    {
        private readonly IDwarfRepository _dwarfRepository;

        public DwarfService(IDwarfRepository dwarfRepository)
        {
            _dwarfRepository = dwarfRepository;
        }

    }
}
