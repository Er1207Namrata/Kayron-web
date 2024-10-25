using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class UnpaidPayout : Menu
    {
        public List<UnpaidPayoutData>? unpaidpayoutList { get; set; }
    }
    public class UnpaidPayoutRequest : Menu
    {
        public string? FromLoginId { get; set; }
        public string? ToLoginId { get; set; }
        public string? IncomeType { get; set; }
        public DataSet GetUnpaidPayout()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@FromLoginId",FromLoginId==""?null:FromLoginId),
                    new SqlParameter("@ToLoginId",ToLoginId==""?null:ToLoginId),
                    new SqlParameter("@InComeType",IncomeType==""?null:IncomeType),
                    new SqlParameter("@Page",Page),
                    new SqlParameter("@Size",Size),
                    new SqlParameter("@ExportToExcel",ExportToExcel),
                    new SqlParameter("@FromDate",FromDate==""?null:FromDate),
                    new SqlParameter("@ToDate",ToDate==""?null:ToDate),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.getUnpaidPayout, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class UnpaidPayoutData
    {
        public string? FK_FromId { get; set; }
        public string? FK_ToId { get; set; }
        public string? FromLoginId { get; set; }
        public string? FromName { get; set; }
        public string? ToLoginId { get; set; }
        public string? ToName { get; set; }
        public decimal? BusinessAmount { get; set; }
        public decimal? BV { get; set; }
        public decimal? CommissionPercentage { get; set; }
        public decimal? Amount { get; set; }
        public string? IncomeType { get; set; }
        public string? LevelInfo { get; set; }
        public int? TotalRecords { get; set; }
    }
    public class UnpaidPayoutResponse
    {
        public UnpaidPayout? Response { get; set; }
        public string? Message { get; set; }
        public int? Status { get; set; }
    }
}
