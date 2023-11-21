using Microsoft.EntityFrameworkCore;
using ReadingClubWebApp.Data;
using ReadingClubWebApp.Data.Enum;
using ReadingClubWebApp.Interfaces;
using ReadingClubWebApp.Models;

namespace ReadingClubWebApp.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Book book)
        {
            _context.Add(book);
            return Save();
        }

        public bool Delete(Book book)
        {
            _context.Remove(book);
            return Save();
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _context.Books.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Book> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Books.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Book>> GetBookByGenre(Genre genre)
        {
            return await _context.Books.Where(c => c.Genre == genre).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Book book)
        {
            _context.Update(book);
            return Save();
        }
    }
}
