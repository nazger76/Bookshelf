using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bookshelf.Data.Context;
using Bookshelf.Data.Repositories;
using Bookshelf.Models.ViewModels;

namespace Bookshelf.Controllers
{
    public class BorrowersController : Controller
    {
        private readonly BorrowersRepository Borrowers;
        
        public BorrowersController(BookshelfContext context)
        {
            Borrowers = new BorrowersRepository(context);
        }

        // GET: Borrowers
        public async Task<IActionResult> Index()
        {
            return View(Borrowers.Borrowers);
        }

        // GET: Borrowers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var borrower = Borrowers.Get(id);
            if (borrower == null)
            {
                return NotFound();
            }
            return View(borrower);          
        }

        // GET: Borrowers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Borrowers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BorrowerID,FirstName,LastName,BirthDate")] BorrowerViewModel borrower)
        {
            if (ModelState.IsValid)
            {
                await Borrowers.Add(borrower);
                return RedirectToAction(nameof(Index));
            }
            return View(borrower);
        }

        // GET: Borrowers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var borrower = Borrowers.Get(id);
            if (borrower == null)
            {
                return NotFound();
            }
            return View(borrower);          
        }

        // POST: Borrowers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BorrowerID,FirstName,LastName,BirthDate")] BorrowerViewModel borrower)
        {
            if (id != borrower.BorrowerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                borrower = await Borrowers.Update(borrower);
                return RedirectToAction(nameof(Index));              
            }
            return View(borrower);
        }

        // GET: Borrowers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var borrower = Borrowers.Get(id);
            if (borrower == null)
            {
                return NotFound();
            }
            return View(borrower);
        }

        // POST: Borrowers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await Borrowers.Delete(id);
            if (!success)
            {
                var borrower = Borrowers.Get(id);
                return View("Error", borrower);
            }
            return RedirectToAction(nameof(Index));            
        }
    }
}