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
        public async Task<List<Book>> GetAllUserBooks()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userBooks = _context.Books.Where(r => r.AppUser.Id == curUser);
            return userBooks.ToList();
        }
    }
}
