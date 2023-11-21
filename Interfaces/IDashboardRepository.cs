using ReadingClubWebApp.Models;

namespace ReadingClubWebApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Event>> GetAllUserEvents();
        Task<List<Club>> GetAllUserClubs();
    }
}
