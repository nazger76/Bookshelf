using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bookshelf.Models
{
    public class Borrower
    {
        public Borrower()
        {
        }

        public Borrower(string firstName, string lastName, DateTime birthDate)
        {
            Update(firstName, lastName, birthDate);
        }

        [Key]
        public int ID { get; private set; }

        [Required]
        public string FirstName { get; private set; }

        [Required]
        public string LastName { get; private set; }

        [Required]
        public DateTime BirthDate { get; private set; }

        public void Update(string firstName, string lastName, DateTime birthDate)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
        }
    }
}