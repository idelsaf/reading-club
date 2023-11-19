using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReadingClubWebApp.Data;
using ReadingClubWebApp.Interfaces;
using ReadingClubWebApp.Models;
using ReadingClubWebApp.Repository;
using ReadingClubWebApp.ViewModels;

namespace ReadingClubWebApp.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly IPhotoService _photoService;

        public EventController(IEventRepository eventRepository, IPhotoService photoService)
        {
            _eventRepository = eventRepository;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Event> events = await _eventRepository.GetAll();
            return View(events);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Event reading_event = await _eventRepository.GetByIdAsync(id);
            return View(reading_event);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateEventViewModel eventVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(eventVM.Image);
                var reading_event = new Event
                {
                    Title = eventVM.Title,
                    Description = eventVM.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        City = eventVM.Address.City,
                        Country = eventVM.Address.Country,
                    }
                };
                _eventRepository.Add(reading_event);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(eventVM);
        }
    }
}
