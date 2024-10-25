using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class FamilynWalletList : Menu
    {
        public List<FamilynWalletListData>? WalletList { get; set; }
        
    }
    public class FamilynWalletListData : Menu
    {
        public decimal? Amount { get; set; }
        public string? TransactionNo { get; set; }
        public string? TransactionDate { get; set; }
        public string? PaymentMode { get; set; }
        //public string? Status { get; set; }
        public string? RequestedDate { get; set; }

    }
    public class FamilynWalletListRequest : Menu
    {
        //public string? LoginId { get; set; }
        
        public DataSet GetFamilynWalletList()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@LoginId",LoginId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.FamilynWalletList, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class FamilynWalletListResponse
    {
        public FamilynWalletList? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    } 
   


}
