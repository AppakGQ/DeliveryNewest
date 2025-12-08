using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeliveryNew.Data;

namespace DeliveryNew.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var totalOrders = 0; // Placeholder until Orders table is real
            var totalItems = await _context.DeliveryItems.CountAsync();
            var totalUsers = await _context.Users.CountAsync();

            ViewData["TotalItems"] = totalItems;
            ViewData["TotalUsers"] = totalUsers;
            
            return View();
        }
    }
}
