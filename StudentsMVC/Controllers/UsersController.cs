using Microsoft.AspNetCore.Mvc;
using ShopApplication.Interfaces.Services;

namespace StudentsMVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(
            IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index(
            CancellationToken cancellationToken)
        {
            var users =
                await _userService.GetAllUsersAsync(
                    cancellationToken);

            return View(users);
        }
    }
}