using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeliveryNew.Data;
using DeliveryNew.Models;
using Microsoft.Extensions.Localization;

namespace DeliveryNew.Controllers
{
    [Authorize] // Auth required for all actions
    // This controller manages DeliveryItems (Products) - confusingly named OrdersController by legacy.
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<OrdersController> _localizer;

        public OrdersController(ApplicationDbContext context, IStringLocalizer<OrdersController> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        // GET: Orders
        // Lists all available products/items.
        [AllowAnonymous] // Allow viewing without login
        public async Task<IActionResult> Index()
        {
            // Initializes the list of delivery items from database
            return View(await _context.DeliveryItems.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.DeliveryItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Orders/Create
        // Shows the form to create a new product.
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // Handles the submission of the new product form.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,IsAvailable,CreatedAt,ImageUrl")] DeliveryItem item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = _localizer["ItemCreatedSuccess"].Value;
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.DeliveryItems.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,IsAvailable,CreatedAt,ImageUrl")] DeliveryItem item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryItemExists(item.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.DeliveryItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.DeliveryItems.FindAsync(id);
            if (item != null)
            {
                _context.DeliveryItems.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryItemExists(int id)
        {
            return _context.DeliveryItems.Any(e => e.Id == id);
        }
    }
}
