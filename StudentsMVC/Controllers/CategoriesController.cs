using Microsoft.AspNetCore.Mvc;
using ShopApplication.Interfaces.Services;

namespace StudentsMVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(
            ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(
            CancellationToken cancellationToken)
        {
            var categories =
                await _categoryService.GetAllCategoriesAsync(
                    cancellationToken);

            return View(categories);
        }
    }
}