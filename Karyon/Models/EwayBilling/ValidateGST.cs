namespace Karyon.Models.EwayBilling
{
    public class ValidateGST
    {
        public string? user_gstin { get; set; }
        public string? gstin { get; set; }
        public string? FK_MemId { get; set; }
    }
    public class ValidateGSTCommonResponse
    {
        public ValidateGSTResultsCommon? results { get; set; }
        public ValidateGSTResults? resultsuccess { get; set; }
        public ValidateGSTResultsError? resultsError { get; set; }

    }
    public class ValidateGSTResponse
    {
        public ValidateGSTResults? results { get; set; }
    }
    public class ValidateGSTResults
    {
        public ValidateGStMessage? message { get; set; }
        public string? errorMessage { get; set; }
        public string? InfoDtls { get; set; }
        public string? status { get; set; }
       
    }
    public class ValidateGStMessage
    {
        public int code { get; set; }
        public string? Gstin { get; set; }
        public string? TradeName { get; set; }
        public string? LegalName { get; set; }
        public string? AddrBnm { get; set; }
        public string? AddrBno { get; set; }
        public string? AddrFlno { get; set; }
        public string? AddrSt { get; set; }
        public string? AddrLoc { get; set; }
        public int StateCode { get; set; }
        public int AddrPncd { get; set; }
        public string? TxpType { get; set; }
        public string? Status { get; set; }
        public string? BlkStatus { get; set; }
        public string? DtReg { get; set; }
        public string? DtDReg { get; set; }
    }
    public class ValidateGSTResponseError
    {
        public ValidateGSTResultsError? results { get; set; }
    }
   
    public class ValidateGSTResultsCommon
    {
        public string? status { get; set; }
        public int code { get; set; }
    }
    public class ValidateGSTResultsError
    {
        public string? message { get; set; }
        public string? status { get; set; }
        public string? code { get; set; }
        public string? requestId { get; set; }
    }
}
