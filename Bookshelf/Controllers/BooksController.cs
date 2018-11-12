using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bookshelf.Models.ViewModels;
using Bookshelf.Data.Context;
using Bookshelf.Data.Repositories;

namespace Bookshelf.Controllers
{
    public class BooksController : Controller
    {
        private readonly BooksRepository Books;

        public BooksController(BookshelfContext context)
        {
            Books = new BooksRepository(context);
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(Books.Books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var book = Books.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookID,Title,Author,ISBN,BorrowerID")] BookViewModel book)
        {
            if (ModelState.IsValid)
            {
                await Books.Add(book);                
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var book = Books.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookID,Title,Author,ISBN,BorrowerID")] BookViewModel book)
        {
            if (id != book.BookID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                book = await Books.Update(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var book = Books.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await Books.Delete(id);
            if (!success)
            {
                var book = Books.Get(id);
                return View("Error", book);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Loan/5
        public async Task<IActionResult> Loan(int? id)
        {
            var book = Books.Get(id);
            if (book == null)
            {
                return NotFound();
            }

            return View("Loan", book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Loan(int id, [Bind("BookID,Title,Author,ISBN,BorrowerID")] BookViewModel book)
        {
            book = Books.UpdateBookViewModel(book);

            if (ModelState.IsValid)
            {
                var sucess = Books.Loan(book);
                if (!sucess)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }

            return View("Loan", book);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnLoan(int id)
        {
            var book = Books.Get(id);
            var success = Books.Return(book);
            if (!success)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}