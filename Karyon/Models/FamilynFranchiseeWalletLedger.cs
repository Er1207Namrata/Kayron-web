using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class FamilynFranchiseeWalletLedger : Menu
    {
        public List<FamilynFranchiseeWalletLedgerData>? WalletLedger { get; set; }
        
    }
    public class FamilynFranchiseeWalletLedgerData : Menu
    {
        public string? TransactionDate { get; set; }
        public string? Narration { get; set; }
        public decimal? Cramount { get; set; }
        public decimal? Dramount { get; set; }
        public decimal? Balance { get; set; }
        public int? TotalRecords { get; set; }

    }
    public class FamilynFranchiseeWalletLedgerRequest : Menu
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
                DataSet ds = Connection.ExecuteQuery(ProcedureName.FranchiseeFamilynWalletLedger, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class FamilynFranchiseeWalletLedgerResponse
    {
        public FamilynFranchiseeWalletLedger? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    } 
   


}
