using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Data.Context;
using Bookshelf.Models;
using Bookshelf.Models.ViewModels;

namespace Bookshelf.Data.Repositories
{
    public class BorrowersRepository
    {
        private readonly BookshelfContext Context;

        public BorrowersRepository(BookshelfContext context)
        {
            Context = context;
        }

        public IEnumerable<BorrowerViewModel> Borrowers
        {
            get
            {
                var borrowers = Context.Borrowers.ToList();

                foreach (var borrower in borrowers)
                {
                    yield return GetBorrowerForDisplay(borrower);
                }
            }
        }

        public BorrowerViewModel Get(int? id)
        {
            return id != null
                ? Borrowers.FirstOrDefault(m => m.BorrowerID == id)
                : null;
        }

        public async Task Add(BorrowerViewModel borrower)
        {
            if (borrower == null)
            {
                return;
            }

            var borrowerModel = new Borrower(borrower.FirstName, borrower.LastName, borrower.BirthDate);
            Context.Add(borrowerModel);
            await Context.SaveChangesAsync();
        }

        public async Task<BorrowerViewModel> Update(BorrowerViewModel borrower)
        {
            if (borrower == null)
            {
                return null;
            }
            var borrowerModel = Context.Borrowers.FirstOrDefault(b => b.ID == borrower.BorrowerID);
            if (borrowerModel == null)
            {
                return null;
            }

            borrowerModel.Update(borrower.FirstName, borrower.LastName, borrower.BirthDate);
            try
            {
                Context.Update(borrowerModel);
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowerExists(borrowerModel.ID))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return Get(borrowerModel.ID);
        }

        public async Task<bool> Delete(int id)
        {
            var borrower = Context.Borrowers.FirstOrDefault(b => b.ID == id);
            if (borrower != null)
            {
                //This borrower still have a book to return.
                if (Context.Books.Any(b => b.LoanedTo == borrower.ID))
                {
                    return false;
                }

                Context.Borrowers.Remove(borrower);
                await Context.SaveChangesAsync();
            }
            return true;
        }

        private bool BorrowerExists(int id)
        {
            return Context.Borrowers.Any(e => e.ID == id);
        }

        private BorrowerViewModel GetBorrowerForDisplay(Borrower borrower)
        {
            return new BorrowerViewModel
            {
                BorrowerID = borrower.ID,
                FirstName = borrower.FirstName,
                LastName = borrower.LastName,
                BirthDate = borrower.BirthDate
            };
        }
    }
}