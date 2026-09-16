namespace AutoReviewLab
{
    public class Complaint
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string Category { get; set; } = "General";

        public string Status { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; }

        public int PriorityLevel { get; set; }
    }
}
