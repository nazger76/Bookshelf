using System.Collections.Generic;
using Bookshelf.Models.ViewModels;

namespace Bookshelf.Data.Repositories
{
    public interface IBookshelf
    {
        IEnumerable<BookViewModel> Books { get; }
        bool Loan(BookViewModel book);
        bool Return(BookViewModel book);
    }
}
