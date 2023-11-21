using Microsoft.AspNetCore.Mvc;
using ReadingClubWebApp.Data;
using ReadingClubWebApp.Interfaces;
using ReadingClubWebApp.ViewModels;

namespace ReadingClubWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }
        public async Task<IActionResult> Index()
        {
            var userBooks = await _dashboardRepository.GetAllUserBooks();
            var dashboardViewModel = new DashboardViewModel()
            {
                Books = userBooks
            };
            return View(dashboardViewModel);
        }
    }
}
