using Microsoft.AspNetCore.Mvc;
using ReadingClubWebApp.Data;
using ReadingClubWebApp.Models;

namespace ReadingClubWebApp.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Event> events = _context.Events.ToList();
            return View(events);
        }
    }
}
