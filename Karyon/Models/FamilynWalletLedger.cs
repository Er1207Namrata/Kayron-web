using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class FamilynWalletLedger : Menu
    {
        public List<FamilynWalletLedgerData>? WalletLedger { get; set; }
        
    }
    public class FamilynWalletLedgerData : Menu
    {
        public string? TransactionDate { get; set; }
        public string? Narration { get; set; }
        public decimal? Cramount { get; set; }
        public decimal? Dramount { get; set; }
        public decimal? Balance { get; set; }
        public int? TotalRecords { get; set; }

    }
    public class FamilynWalletLedgerRequest : Menu
    {
        //public string? LoginId { get; set; }
        
        public DataSet GetFamilynWalletLedger()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@LoginId",LoginId),
                    new SqlParameter("@Size",Size),
                    new SqlParameter("@Page",Page)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.FamilynWalletLedger, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class FamilynWalletLedgerResponse
    {
        public FamilynWalletLedger? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    } 
   


}
