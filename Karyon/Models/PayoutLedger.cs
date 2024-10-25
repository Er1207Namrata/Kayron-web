using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class PayoutLedger : Menu
    {
        public List<PayoutLedgerData>? payoutList { get; set; }
    }
    public class PayoutLedgerRequest : Menu
    {
        //public string? LoginId { get; set; }
        public DataSet GetPayoutLedger()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@LoginId",LoginId),
                    new SqlParameter("@Page",Page),
                    new SqlParameter("@Size",Size),
                    new SqlParameter("@ExportToExcel",ExportToExcel),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetPayoutLedger, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class PayoutLedgerData
    {
        public string? TransactionDate { get; set; }
        public string? Narration { get; set; }
        public decimal? Cramount { get; set; }
        public decimal? Dramount { get; set; }
        public decimal? Balance { get; set; }
        public int? TotalRecords { get; set; }
    }
    public class PayoutLedgerResponse
    {
        public PayoutLedger? Response { get; set; }
        public string? Message { get; set; }
        public int? Status { get; set; }
    }
}
