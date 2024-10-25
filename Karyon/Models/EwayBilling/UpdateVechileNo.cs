namespace Karyon.Models.EwayBilling
{
    public class UpdateVechileNo: Menu
    {
        public string? access_token { get; set; }
        public string? userGstin { get; set; }
        public string? Fk_MemId { get; set; }
        public long eway_bill_number { get; set; }
        public string? vehicle_number { get; set; }
        public string? vehicle_type { get; set; }
        public string? place_of_consignor { get; set; }
        public string? state_of_consignor { get; set; }
        public string? reason_code_for_vehicle_updation { get; set; }
        public string? reason_for_vehicle_updation { get; set; }
        public string? transporter_document_number { get; set; }
        public string? transporter_document_date { get; set; }
        public string? mode_of_transport { get; set; }
        public string? data_source { get; set; }
    }
    public class UpdateVechileCommonResponse
    {
        public UpdateVechileResultsCommon? results { get; set; }
        public UpdateVechileResults? resultsuccess { get; set; }
        public UpdateVechileResultsError? resultError { get; set; }

    }
    public class UpdateVechileResultsCommon
    {
        public string? status { get; set; }
        public int code { get; set; }
    }
    public class UpdateVechileResultsReponse
    {
        public UpdateVechileResults? results { get; set; }
    }
    public class UpdateVechileResultsReponseError
    {
        public UpdateVechileResultsError? results { get; set; }
    }
    public class UpdateVechileResults
    {
        public UpdateVechileMessage? message { get; set; }
        public string? status { get; set; }
        public int code { get; set; }
    }
    public class UpdateVechileMessage
    {
        public string? vehUpdDate { get; set; }
        public string? validUpto { get; set; }
        public bool error { get; set; }
        public string? url { get; set; }
    }
    public class UpdateVechileResultsError
    {
        public string? message { get; set; }
        public string? status { get; set; }
        public int code { get; set; }
    }

}
