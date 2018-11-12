using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshelf.Models
{
    public class Book
    {
        public Book()
        {
        }

        public Book(string title, string author, string isbn)
        {
            Update(title, author, isbn);
        }

        [Key]
        public int ID { get; private set; }

        [Required]
        public string Title { get; private set; }

        [Required]
        public string Author { get; private set; }

        [Required]
        public string ISBN { get; private set; }

        public int? LoanedTo { get; private set; }

        [ForeignKey("LoanedTo")]
        public virtual Borrower Borrower { get; private set; }        

        public void Update(string title, string author, string isbn)
        {
            this.Title = title;
            this.Author = author;
            this.ISBN = isbn;
        }

        public void Loan(string borrowerID)
        {
            int loanedTo;
            if (int.TryParse(borrowerID, out loanedTo))
            {
                this.LoanedTo = loanedTo;
            }
        }

        public void Loan(int borrowerID)
        {
            this.LoanedTo = borrowerID;
        }

        public void Return()
        {
            this.LoanedTo = null;
        }
    }
}