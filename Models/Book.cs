using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ReadingClubWebApp.Data.Enum;

namespace ReadingClubWebApp.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public Genre Genre { get; set; }

        [ForeignKey("AppUser")]
        public string? UserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
