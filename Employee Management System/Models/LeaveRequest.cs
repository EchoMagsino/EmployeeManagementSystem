namespace Employee_Management_System.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime StartDate {get; set;}
        public DateTime EndDate {get; set;}
        public string LeaveType { get; set; }
        public string Status { get; set; }
        public string ApproverComments { get; set; }


        public Employee Employee { get; set; }
    }
}