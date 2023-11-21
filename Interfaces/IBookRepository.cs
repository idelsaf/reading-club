using ReadingClubWebApp.Data.Enum;
using ReadingClubWebApp.Models;

namespace ReadingClubWebApp.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book> GetByIdAsync(int id);
        Task<Book> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Book>> GetBookByGenre(Genre genre);
        bool Add(Book book);
        bool Update(Book book);
        bool Delete(Book book);
        bool Save();
    }
}
