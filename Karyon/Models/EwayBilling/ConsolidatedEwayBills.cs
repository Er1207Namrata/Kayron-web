namespace Karyon.Models.EwayBilling
{
    public class ConsolidatedEwayBills
    {
        public string? Fk_MemId { get; set; }
        public string? access_token { get; set; }
        public string? userGstin { get; set; }
        public string? place_of_consignor { get; set; }
        public string? state_of_consignor { get; set; }
        public string? vehicle_number { get; set; }
        public string? mode_of_transport { get; set; }
        public string? transporter_document_number { get; set; }
        public string? transporter_document_date { get; set; }
        public string? data_source { get; set; }
        public List<ListOfEwayBill>? list_of_eway_bills { get; set; }
    }
    public class ListOfEwayBill
    {
        public object? eway_bill_number { get; set; }
    }
    public class ConsolidatedEwaybillingCommonResponse
    {
        public ConsolidatedEwayBillingResultsCommon? results { get; set; }
        public ConsolidatedResults? resultsuccess { get; set; }
        public ConsolidatedResultsError? resultError { get; set; }

    }
    public class ConsolidatedEwayBillingResultsCommon
    {
        public string? status { get; set; }
        public int code { get; set; }
        public string? requestId { get; set; }
    }
    public class ConsolidatedEwayBilsReponse
    {
        public ConsolidatedResults? results { get; set; }
    }
    public class ConsolidatedEwayBilsReponseError
    {
        public ConsolidatedResultsError? results { get; set; }
    }
    public class ConsolidatedResultsError
    {
        public string? message { get; set; }
        public string? status { get; set; }
        public int code { get; set; }
    }
    public class ConsolidatedResults
    {
        public ConsolidatedMessage? message { get; set; }
        public string? status { get; set; }
        public int code { get; set; }
    }
    public class ConsolidatedMessage
    {
        public string? cEwbNo { get; set; }
        public string? cEWBDate { get; set; }
        public bool error { get; set; }
    }

}
