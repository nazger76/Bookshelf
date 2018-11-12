using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Data.Context;
using Bookshelf.Models;
using Bookshelf.Models.ViewModels;

namespace Bookshelf.Data.Repositories
{
    public class BooksRepository : IBookshelf
    {
        private readonly BookshelfContext Context;
        private readonly BorrowersRepository BorrowersRepository;

        public BooksRepository(BookshelfContext context)
        {
            Context = context;
            BorrowersRepository = new BorrowersRepository(Context);
        }

        public IEnumerable<BookViewModel> Books
        {
            get
            {
                var books = Context.Books
                    .Include(b => b.Borrower)
                    .ToList();

                foreach (var book in books)
                {
                    yield return GetBookForDisplay(book);
                }
            }
        }

        public BookViewModel Get(int? id)
        {
            return id != null
                ? Books.FirstOrDefault(m => m.BookID == id)
                : null;
        }

        public async Task Add(BookViewModel book)
        {
            if (book == null)
            {
                return;
            }

            var bookModel = new Book(book.Title, book.Author, book.ISBN);
            Context.Add(bookModel);
            await Context.SaveChangesAsync();
        }

        public async Task<BookViewModel> Update(BookViewModel book)
        {
            if (book == null)
            {
                return null;
            }

            var bookModel = Context.Books.FirstOrDefault(b => b.ID == book.BookID);
            if (bookModel == null)
            {
                return null;
            }

            bookModel.Update(book.Title, book.Author, book.ISBN);
            try
            {
                Context.Update(bookModel);
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(bookModel.ID))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return Get(bookModel.ID);
        }

        public async Task<bool> Delete(int id)
        {
            var book = Context.Books.FirstOrDefault(b => b.ID == id);
            if (book != null)
            {
                //Someone has borrowed this book. Cannot delete it until returned.
                if (book.LoanedTo != null)
                {
                    return false;
                }

                Context.Books.Remove(book);
                await Context.SaveChangesAsync();
            }

            return true;
        }

        public bool Loan(BookViewModel book)
        {
            if (book == null || book.BookID <= 0)
            {
                return false;
            }

            var bookModel = Context.Books.FirstOrDefault(b => b.ID == book.BookID);
            if (bookModel == null)
            {
                return false;
            }

            bookModel.Loan(book.BorrowerID);
            Context.SaveChangesAsync();
            return true;
        }

        public bool Return(BookViewModel book)
        {
            if (book == null || book.BookID <= 0)
            {
                return false;
            }

            var bookModel = Context.Books.FirstOrDefault(b => b.ID == book.BookID);
            if (bookModel == null)
            {
                return false;
            }

            bookModel.Return();
            Context.SaveChangesAsync();
            return true;
        }

        private bool BookExists(int id)
        {
            return Context.Books.Any(e => e.ID == id);
        }

        private BookViewModel GetBookForDisplay(Book book)
        {
            var borrowerViewModel = book.Borrower != null
                ? BorrowersRepository.Get(book.Borrower.ID)
                : null;

            var bookViewModel = new BookViewModel
            {
                BookID = book.ID,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,

                BorrowerName = borrowerViewModel != null ? borrowerViewModel.DisplayName : null,
                Borrowers = new SelectList(GetSelectListItems(BorrowersRepository.Borrowers), "Value", "Text"),
            };

            return bookViewModel;
        }

        public BookViewModel UpdateBookViewModel(BookViewModel book)
        {
            book.Borrowers = new SelectList(GetSelectListItems(BorrowersRepository.Borrowers), "Value", "Text");
            return book;
        }

        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<BorrowerViewModel> borrowers)
        {
            foreach (var borrower in borrowers)
            {
                yield return new SelectListItem
                {
                    Value = borrower.BorrowerID.ToString(),
                    Text = borrower.DisplayName
                };
            }
        }
    }
}