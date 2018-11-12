using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bookshelf.Models.ViewModels
{
    public class BorrowerViewModel
    {
        public int BorrowerID { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Name")]
        public string DisplayName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        [Required]
        [DisplayName("Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

    }
}
