using ReadingClubWebApp.Data;
using ReadingClubWebApp.Interfaces;
using ReadingClubWebApp.Models;

namespace ReadingClubWebApp.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Club>> GetAllUserClubs()
        {
            var curUser = _httpContextAccessor.HttpContext?.User;
            var userClubs = _context.Clubs.Where(r => r.AppUser.Id == curUser.ToString());
            return userClubs.ToList();
        }

        public async Task<List<Event>> GetAllUserEvents()
        {
            var curUser = _httpContextAccessor.HttpContext?.User;
            var userEvents = _context.Events.Where(r => r.AppUser.Id == curUser.ToString());
            return userEvents.ToList();
        }
    }
}
