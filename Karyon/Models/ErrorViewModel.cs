namespace Karyon.Models
{
    public class ErrorViewModel : Menu
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}