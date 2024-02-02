using System.ComponentModel.DataAnnotations;

namespace SchoolBackoffice.Models
{
    public class Lesson
    {
        public int Id { get; set; }

        public required string Type { get; set; }

        [Display(Name = "Day of week")]
        public required string WeekDay { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Start time")]
        public required DateTime DateStart { get; set; }

        public int Duration { get; set; }

        public required int TeacherId { get; set; }

        public Teacher? Teacher { get; set; }

        public string DateStop()
        {
            return DateStart.AddMinutes(Duration).ToString("hh:mm");
        }
    }
}

