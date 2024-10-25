using static Karyon.Models.Common;

namespace Karyon.Models
{
    public class PAYTMStatus
    {
        public string? TransactionNo { get; set; }
    }
    public class PAYTMResponse
    {
        public int? Status { get; set; }
        public string? Message { get; set; }
        public string? OrderId { get; set; }
        public string? TxnId { get; set; }
    }
    public class PaytmStatusResponse
    {
        public body1? body { get; set; }
    }
    public class body1
    {
        public ResultInfo? resultInfo { get; set; }
        public string? orderId { get; set; }
        public string? txnId { get; set; }
    }
    public class ResultInfo
    {
        public string? resultMsg { get; set; }
        public string? resultCode { get; set; }
    }
}
