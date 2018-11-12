using Microsoft.EntityFrameworkCore;
using Bookshelf.Models;

namespace Bookshelf.Data.Context
{
    public class BookshelfContext : DbContext
    {
        public BookshelfContext (DbContextOptions<BookshelfContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<Borrower>().ToTable("Borrower");
        }
    }
}
