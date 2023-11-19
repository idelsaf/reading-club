using ReadingClubWebApp.Data.Enum;
using ReadingClubWebApp.Models;

namespace ReadingClubWebApp.ViewModels
{
    public class CreateEventViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public ClubCategory EventCategory { get; set; }
    }
}
