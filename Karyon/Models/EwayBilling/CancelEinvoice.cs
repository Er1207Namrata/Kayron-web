namespace Karyon.Models.EwayBilling
{
    public class CancelEinvoice
    {
        public string? access_token { get; set; }
        public string? FK_MemId { get; set; }
        public string? OrderNo { get; set; }
        public string? user_gstin { get; set; }
        public string? irn { get; set; }
        public string? cancel_reason { get; set; }
        public string? cancel_remarks { get; set; }
    }
    public class CancelEinvoiceCommonResponse
    {
        public CancelEinvoiceResultsCommon? results { get; set; }
        public CancelEinvoiceResults? resultsuccess { get; set; }
        public CancelEinvoiceResultsError? resultsError { get; set; }

    }
    public class CancelEinvoiceResults
    {
        public CancelEinvoiceMessage? message { get; set; }
        public string? errorMessage { get; set; }
        public string? InfoDtls { get; set; }
        public string? status { get; set; }

    }
    public class CancelEinvoiceResponse
    {
        public CancelEinvoiceResults? results { get; set; }
    }
    public class CancelEinvoiceResponseError
    {
        public CancelEinvoiceResultsError? results { get; set; }
    }
    public class CancelEinvoiceMessage
    {
        public string? Irn { get; set; }
        public string? CancelDate { get; set; }
    }
    public class CancelEinvoiceResultsCommon
    {
        public string? status { get; set; }
        public int code { get; set; }
    }
    public class CancelEinvoiceResultsError
    {
        public string? message { get; set; }
        public string? errorMessage { get; set; }
        public string? InfoDtls { get; set; }
        public string? status { get; set; }
        public int code { get; set; }
    }

}
