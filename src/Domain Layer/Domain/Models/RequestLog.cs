namespace Domain.Models
{
    public class RequestLog
    {
        public Guid Id { get; set; }
        public string? RequestPath { get; set; }
        public string? RequestBody { get; set; }
        public string? ResponseStatusCode { get; set; }
        public string? ResponseBody { get; set; }
        public DateTime TimeOfRequest { get; set; }
        public decimal? TimeForRequest { get; set; }

    }
}