using FindTheDwarves.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDwarves.Infrastructure.Repository
{
    public class DwarfRepository : IDwarfRepository
    {
        private readonly Context _context;

        public DwarfRepository(Context context)
        {
            _context = context;
        }
    }
}
