namespace Employee_Management_System.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber {get; set;}
        public string Department { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }


        public ICollection<LeaveRequest> LeaveRequests { get; set; }
        public ICollection<PerformanceReview> PerformanceReviews { get; set; }
        public ICollection<Document> Documents { get; set; }
        
    }
}
