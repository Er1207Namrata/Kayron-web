namespace Karyon.Models.EwayBilling
{
    public class CancelEway: Menu
    {
        public string? access_token { get; set; }
        public string? userGstin { get; set; }
        public string? eway_bill_number { get; set; }
        public string? reason_of_cancel { get; set; }
        public string? cancel_remark { get; set; }
        public string? data_source { get; set; }
    }
}
