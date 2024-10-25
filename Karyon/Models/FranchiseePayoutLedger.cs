using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class FranchiseePayoutLedger : Menu
    {
        public List<FranchiseePayoutLedgerData>? payoutList { get; set; }
    }
    public class FranchiseePayoutLedgerRequest : Menu
    {
        //public string? LoginId { get; set; }
        public DataSet GetFranchisePayoutLedger()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@LoginId",LoginId),
                    new SqlParameter("@Page",Page),
                    new SqlParameter("@Size",Size),
                    new SqlParameter("@ExportToExcel",ExportToExcel),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetFranchisePayoutLedger, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class FranchiseePayoutLedgerData
    {
        public string? TransactionDate { get; set; }
        public string? Narration { get; set; }
        public decimal? Cramount { get; set; }
        public decimal? Dramount { get; set; }
        public decimal? Balance { get; set; }
        public int? TotalRecords { get; set; }
    }
    public class FranchiseePayoutLedgerResponse
    {
        public FranchiseePayoutLedger? Response { get; set; }
        public string? Message { get; set; }
        public int? Status { get; set; }
    }
}
