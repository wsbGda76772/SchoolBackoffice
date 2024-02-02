using System.ComponentModel.DataAnnotations;

namespace SchoolBackoffice.Models
{
	public class Teacher
	{
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Surname { get; set; }

        public int? Age { get; set; }

        [Display(Name = "Licence number")]
        public string? LicenceNumber { get; set; }

        public ICollection<Lesson>? Lessons { get; set; }

        override public string ToString()
        {
            return Name + ' ' + Surname;
        }
    }
}

