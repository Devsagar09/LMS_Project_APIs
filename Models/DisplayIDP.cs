namespace LMS_Project_APIs.Models
{
    public class DisplayIDP
    {
        public int? student_id { get; set; }
        public int? training_id { get; set; }
        public string? thumbnail_image { get; set; }
        public string? training_name { get; set; }
        public string? training_code { get; set; }
        public string? summary { get; set; }
        public string? trainingtype_name { get; set; } 
        public string? status { get; set; }
    }
}
