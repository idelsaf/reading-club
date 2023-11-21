using ReadingClubWebApp.Data.Enum;
using ReadingClubWebApp.Models;

namespace ReadingClubWebApp.ViewModels
{
    public class CreateBookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string Genre { get; set; }
    }
}
