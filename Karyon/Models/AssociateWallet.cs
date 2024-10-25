using System.Data.SqlClient;
using System.Data;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace Karyon.Models
{
    public class AssociateWalletRequest: Menu
    {
        //public string? LoginId { get; set; } 
        public string? CustomerId { get; set; } 
        public DataSet GetAssociateLedger()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@LoginId",LoginId),
                    new SqlParameter("@Size",Size),
                    new SqlParameter("@Page",Page)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetAssociateLedger, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetAssociateWalletBalance()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@CustomerId",CustomerId),
                    new SqlParameter("@LoginId",LoginId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetAssociateWalletBalance, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class AssociateWallet
    {
        public List<AssociateWalletData>? AssociateWalletList { get; set; }
        

    }
   
    public class AssociateWalletData
    {
        public string? OrderNo { get; set; }
        public string? TransactionDate { get; set; }
        public string? Narration { get; set; }
        public decimal? Cramount { get; set; }
        public decimal? Dramount { get; set; }
        public decimal? Balance { get; set; }
  

    }
    public class AssociateWalletResponse
    {
        public AssociateWallet? Response { get; set; }
        public string? Message { get; set; }
        public int? Status { get; set; }
    }
    public class WalletBalanceResponse
    {
        public WalletBalance? Response { get; set; }
        public string? Message { get; set; }
        public int? Status { get; set; }
    }
    public class WalletBalance
    {
        public WalletBalanceData? WalletBalanceData { get; set; }


    }
    public class WalletBalanceData
    {

        public decimal? Balance { get; set; }
        public decimal? FUPWallet { get; set; }
        public decimal? OFPWallet { get; set; }


    }

}