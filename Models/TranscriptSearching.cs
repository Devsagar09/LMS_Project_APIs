namespace LMS_Project_APIs.Models
{
    public class TranscriptSearching
    {
        public string? training_name { get; set; } 
        public string? training_code { get; set; }
        public string? trainingtype_name { get; set; }
        public string? status_name { get; set; }
        public DateTime? enroll_date { get; set; }
        public DateTime? completion_date { get; set; }
    }
}
