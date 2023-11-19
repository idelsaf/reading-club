using ReadingClubWebApp.Models;

namespace ReadingClubWebApp.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAll();
        Task<Event> GetByIdAsync(int id);
        Task<Event> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Event>> GetAllEventsByCity(string city);
        bool Add(Event reading_event);
        bool Update(Event reading_event);
        bool Delete(Event reading_event);
        bool Save();
    }
}
