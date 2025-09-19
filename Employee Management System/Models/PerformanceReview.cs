using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System.Models
{
    public class PerformanceReview
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int? ReviewerId { get; set; }
        public DateTime ReviewDate { get; set; }
        public string Comments { get; set; }

        [Range(0, 100, ErrorMessage = "Rating must be between 0 and 100.")]
        public int Rating { get; set; }

        //navigation 
        public Employee? Employee { get; set; }
        public Employee? Reviewer { get; set; }

        
       
    }
}