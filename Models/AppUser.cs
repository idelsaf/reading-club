using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadingClubWebApp.Models
{
    public class AppUser : IdentityUser
    {
        public string? PreferredGenre { get; set; }
        public string? FavoriteBook { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}
