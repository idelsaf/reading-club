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

        public async Task<IActionResult> Edit(int id)
        {
            var reading_event = await _eventRepository.GetByIdAsync(id);
            if (reading_event == null) return View("Error");
            var eventVM = new EditEventViewModel
            {
                Title = reading_event.Title,
                Description = reading_event.Description,
                AddressId = reading_event.AddressId,
                Address = reading_event.Address,
                URL = reading_event.Image,
                EventCategory = reading_event.EventCategory
            };

            return View(eventVM);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id, EditEventViewModel eventVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit event");
                return View("Edit", eventVM);
            }

            var userEvent = await _eventRepository.GetByIdAsyncNoTracking(id);

            if (userEvent != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userEvent.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(eventVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(eventVM.Image);

                var reading_event = new Event
                {
                    Id = id,
                    Title = eventVM.Title,
                    Description = eventVM.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = eventVM.AddressId,
                    Address = eventVM.Address
                };

                _eventRepository.Update(reading_event);

                return RedirectToAction("Index");
            }
            else
            {
                return View(eventVM);
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            var eventDetails = await _eventRepository.GetByIdAsync(id);
            if (eventDetails == null) return View("Error");
            return View(eventDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var eventDetails = await _eventRepository.GetByIdAsync(id);
            if (eventDetails == null) return View("Error");

            _eventRepository.Delete(eventDetails);
            return RedirectToAction("Index");
        }
    }
}
