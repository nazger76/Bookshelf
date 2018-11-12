using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bookshelf.Models.ViewModels
{
    public class BookViewModel
    {
        public int BookID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Display(Name = "Loaned To")]
        public string BorrowerName { get; set; }

        [Required]
        [Display(Name = "Borrower")]
        public int BorrowerID { get; set; }

        [Display(Name = "Borrowers")]
        public SelectList Borrowers { get; set; }
    }
}