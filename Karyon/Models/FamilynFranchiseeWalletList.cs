using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class FamilynFranchiseeWalletList
    {
        public List<FamilynFranchiseeWalletListData>? WalletList { get; set; }
        
    }
    public class FamilynFranchiseeWalletListData : Menu
    {
        public decimal? Amount { get; set; }
        public string? TransactionNo { get; set; }
        public string? TransactionDate { get; set; }
        public string? PaymentMode { get; set; }
        //public string? Status { get; set; }
        public string? RequestedDate { get; set; }
        public int? TotalRecords { get; set; }

    }
    public class FamilynFranchiseeWalletListRequest : Menu
    {
        //public string? LoginId { get; set; }
        
        public DataSet GetFamilynFranchiseeWalletList()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@LoginId",LoginId),
                    new SqlParameter("@Page",Page),
                    new SqlParameter("@Size",Size)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.FranchiseeFamilynWalletList, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class FamilynFranchiseeWalletListResponse
    {
        public FamilynFranchiseeWalletList? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    } 
   


}
