using ReadingClubWebApp.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReadingClubWebApp.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public EventCategory EventCategory { get; set; }
        [ForeignKey("AppUser")]
        public string? UserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
