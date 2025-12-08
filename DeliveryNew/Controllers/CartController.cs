using Microsoft.AspNetCore.Mvc;
using DeliveryNew.Data;
using DeliveryNew.Models;
using DeliveryNew.Extensions;

namespace DeliveryNew.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();
            return View(cart);
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> AddToCart(int id)
        {
            var item = await _context.DeliveryItems.FindAsync(id);
            if (item != null)
            {
                var cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();
                cart.AddItem(item, 1);
                HttpContext.Session.Set("Cart", cart);
            }
            return RedirectToAction("Index", "Orders");
        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var item = await _context.DeliveryItems.FindAsync(id);
            if (item != null)
            {
                var cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();
                cart.RemoveLine(item);
                HttpContext.Session.Set("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();
            if (cart.Items.Count == 0)
            {
                return RedirectToAction("Index");
            }
            return View(cart);
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult ProcessPayment()
        {
            // Simulate payment processing
            // In a real app, integrate Stripe/PayPal here.
            
            // Clear Cart
            HttpContext.Session.Remove("Cart");
            
            return View("CheckoutSuccess");
        }
    }
}
