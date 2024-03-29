using FindTheDwarves.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDwarves.Infrastructure.Repository
{
    public class AchievementRepository : IAchievementRepository
    {
        private readonly Context _context;

        public AchievementRepository(Context context)
        {
            _context = context;
        }

    }
}
