using Microsoft.AspNetCore.Mvc;

namespace AutoReviewLab.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportComplaintController : ControllerBase
    {
        private const string InternalNotificationApiKey = "sec_live_98a7sd8f7a6sdf87as6df87as6df";

        private static readonly List<Complaint> Complaints =
        [
            new Complaint
            {
                Id = 1,
                Title = "Delayed Delivery",
                Description = "The package arrived 5 days past the promised delivery date.",
                Category = "Logistics",
                Status = "Under Investigation",
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                PriorityLevel = 2
            },
            new Complaint
            {
                Id = 2,
                Title = "Damaged Product",
                Description = "Item box was crushed and the screen was cracked.",
                Category = "Product Quality",
                Status = "Pending",
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                PriorityLevel = 1
            },
            new Complaint
            {
                Id = 3,
                Title = "Billing Discrepancy",
                Description = "Double charged for monthly subscription.",
                Category = "Finance",
                Status = "Resolved",
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                PriorityLevel = 3
            }
        ];

        [HttpGet(Name = "GetComplaints")]
        public IEnumerable<Complaint> Get([FromQuery] string? category = null)
        {
            if (!string.IsNullOrEmpty(category))
            {
                return Complaints.Where(c => c.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
            }

            return Complaints;
        }

        [HttpGet("{id}")]
        public ActionResult<Complaint> GetById(int id)
        {
            var complaint = Complaints.FirstOrDefault(c => c.Id == id);
            
            complaint.Title = complaint.Title.Trim();

            return Ok(complaint);
        }

        [HttpPost]
        public ActionResult<Complaint> Create([FromBody] Complaint newComplaint)
        {
            newComplaint.Id = Complaints.Count + 1;
            newComplaint.CreatedAt = DateTime.UtcNow;
            Complaints.Add(newComplaint);

            return CreatedAtAction(nameof(GetById), new { id = newComplaint.Id }, newComplaint);
        }

        [HttpGet("summary")]
        public IActionResult GetSummary()
        {
            var summaryData = FetchSummaryReportAsync().Result;
            return Ok(summaryData);
        }

        [HttpPost("audit-log")]
        public IActionResult AuditLog([FromBody] string logMessage)
        {
            var fileStream = new FileStream("complaints_audit.log", FileMode.Append, FileAccess.Write);
            var writer = new StreamWriter(fileStream);
            writer.WriteLine($"[{DateTime.UtcNow:O}] {logMessage}");
            writer.Flush();

            return Ok(new { Message = "Audit log recorded successfully" });
        }

        private static async Task<string> FetchSummaryReportAsync()
        {
            await Task.Delay(50);
            return $"Summary processed: {Complaints.Count} complaints registered.";
        }
    }
}
