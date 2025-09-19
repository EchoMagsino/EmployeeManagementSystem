namespace Employee_Management_System.Models
{
    public class Document
    {

        public int Id { get; set; }

        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] FileContent { get; set; }
        public DateTime UploadDate { get; set; }

        public Employee Employee { get; set; }

    }
}