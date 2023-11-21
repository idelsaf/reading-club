using ReadingClubWebApp.Models;

namespace ReadingClubWebApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Book>> GetAllUserBooks();
    }
}
