using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMSystem.Models;

namespace LMSystem.Controllers
{
    public class BorrowController : Controller
    {
        private readonly LibraryContext _context;

        public BorrowController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Borrow/Create?bookId=1
        public async Task<IActionResult> Create(int? bookId)
        {
            if (bookId == null || bookId == 0)
            {
                TempData["ErrorMessage"] = "Book ID was not provided for borrowing.";
                return View("NotFound");
            }

            var book = await _context.Books12.AsNoTracking().FirstOrDefaultAsync(b => b.BookId == bookId);
            if (book == null)
            {
                TempData["ErrorMessage"] = $"No book found with ID {bookId}.";
                return View("NotFound");
            }

            if (!book.IsAvailable)
            {
                TempData["ErrorMessage"] = "This book is already borrowed.";
                return RedirectToAction("Index", "Books");
            }

            var model = new BorrowRecord { BookId = book.BookId };
            ViewBag.BookTitle = book.Title;
            return View(model);
        }

        // POST: Borrow/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BorrowRecord record)
        {
            var book = await _context.Books12.FindAsync(record.BookId);
            if (book == null)
            {
                TempData["ErrorMessage"] = $"No book found with ID {record.BookId}.";
                return View("NotFound");
            }

            if (!book.IsAvailable)
            {
                TempData["ErrorMessage"] = "This book is already borrowed.";
                return RedirectToAction("Index", "Books");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.BorrowRecords12.Add(record);
                    book.IsAvailable = false; // Server-controlled, not user input
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = $"Successfully borrowed the book: {book.Title}.";
                    return RedirectToAction("Index", "Books");
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = "An error occurred while borrowing the book.";
                    return View(record);
                }
            }

            ViewBag.BookTitle = book.Title;
            return View(record);
        }

        // GET: Borrow/Return/5  (borrowRecordId)
        public async Task<IActionResult> Return(int? borrowRecordId)
        {
            if (borrowRecordId == null || borrowRecordId == 0)
            {
                TempData["ErrorMessage"] = "Borrow record ID was not provided.";
                return View("NotFound");
            }

            try
            {
                var record = await _context.BorrowRecords12.FindAsync(borrowRecordId);
                if (record == null)
                {
                    TempData["ErrorMessage"] = $"No borrow record found with ID {borrowRecordId}.";
                    return View("NotFound");
                }

                var book = await _context.Books12.FindAsync(record.BookId);
                if (book == null)
                {
                    TempData["ErrorMessage"] = "Associated book not found.";
                    return View("NotFound");
                }

                record.ReturnDate = DateTime.UtcNow;
                book.IsAvailable = true; // Server-controlled

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Successfully returned the book: {book.Title}.";
                return RedirectToAction("Index", "Books");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while returning the book.";
                return View("Error");
            }
        }
    }
}