using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadingClubWebApp.Models
{
    public class AppUser : IdentityUser
    {

        public int? BooksNumber { get; set; }
        public string? PreferredGenre { get; set; }
        public string? FavoriteBook { get; set; }
        [ForeignKey("Address")]
        public int AddressId {  get; set; }
        public Address? Address { get; set; }
        public ICollection<Club>? Clubs { get; set; }
        public ICollection<Event>? Events { get; set; }
    }
}
