using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReadingClubWebApp.Data;
using ReadingClubWebApp.Interfaces;
using ReadingClubWebApp.Models;
using ReadingClubWebApp.Services;
using ReadingClubWebApp.ViewModels;

namespace ReadingClubWebApp.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IPhotoService _photoService; 
        public BookController(IBookRepository bookRepository, IPhotoService photoService)
        {
            _bookRepository = bookRepository;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Book> books = await _bookRepository.GetAll();
            return View(books);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Book book = await _bookRepository.GetByIdAsync(id);
            return View(book);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateBookViewModel bookVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(bookVM.Image);
                var book = new Book
                {
                    Title = bookVM.Title,
                    Author = bookVM.Author,
                    Description = bookVM.Description,
                    Image = result.Url.ToString()
                };
                _bookRepository.Add(book);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(bookVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return View("Error");
            var bookVM = new EditBookViewModel
            {
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                URL = book.Image,
                Genre = book.Genre
            };

            return View(bookVM);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id, EditBookViewModel bookVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit book");
                return View("Edit", bookVM);
            }

            var userBook = await _bookRepository.GetByIdAsyncNoTracking(id);

            if (userBook != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userBook.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(bookVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(bookVM.Image);

                var book = new Book
                {
                    Id = id,
                    Title = bookVM.Title,
                    Author = bookVM.Author,
                    Description = bookVM.Description,
                    Image = photoResult.Url.ToString()
                };

                _bookRepository.Update(book);

                return RedirectToAction("Index");
            }
            else
            {
                return View(bookVM); 
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var bookDetails = await _bookRepository.GetByIdAsync(id);
            if (bookDetails == null) return View("Error");
            return View(bookDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var bookDetails = await _bookRepository.GetByIdAsync(id);
            if (bookDetails == null) return View("Error");

            _bookRepository.Delete(bookDetails);
            return RedirectToAction("Index");
        }
    }
}
