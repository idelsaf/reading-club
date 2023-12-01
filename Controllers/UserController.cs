using Microsoft.AspNetCore.Mvc;
using ReadingClubWebApp.Interfaces;
using ReadingClubWebApp.ViewModels;

namespace ReadingClubWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>();

            foreach (var user in users)
            {
                var userViewModel = new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    PreferredGenre = user.PreferredGenre,
                    FavoriteBook = user.FavoriteBook
                };

                result.Add(userViewModel);
            }
            return View(result);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);
            var userDeatilViewModel = new UserDetailViewModel()
            {
                Id = user.Id,
                UserName =user.UserName,
                PreferredGenre = user.PreferredGenre,
                FavoriteBook = user.FavoriteBook
            };

            return View(userDeatilViewModel);
        }
    }
}
