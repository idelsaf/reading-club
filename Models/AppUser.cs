using System.ComponentModel.DataAnnotations;

namespace ReadingClubWebApp.Models
{
    public class AppUser
    {
        [Key]
        public string? Id { get; set; }
        public int? BooksNumber { get; set; }
        public string? PreferredGenre { get; set; }
        public string? FavoriteBook { get; set; }
        public Address? Address { get; set; }
        public ICollection<Club>? Clubs { get; set; }
        public ICollection<Event>? Events { get; set; }
    }
}
