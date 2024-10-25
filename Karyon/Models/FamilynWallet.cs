using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class FamilynWallet : Menu
    {
        public List<FamilynWalletData>? RequestList { get; set; }
        
    }
    public class FamilynWalletData : Menu
    {
        public decimal? Amount { get; set; }
        public string? TransactionNo { get; set; }
        public string? TransactionDate { get; set; }
        public string? PaymentMode { get; set; }
        //public string? Status { get; set; }
        public string? RequestedDate { get; set; }

    }
    public class FamilynWalletRequest : Menu
    {
        //public string? LoginId { get; set; }
        public string? Name { get; set; }
        public string? PaymentMode { get; set; }
        public decimal? Amount { get; set; }
        public string? TransactionNo { get; set; }
        public string? TransactionDate { get; set; }
        public string? PaymentSlip { get; set; }
        public DataTable? dtdetails { get; set; }
        public IFormFile? ImageFile { get; set; }
        public DataSet RequestFamilynWallet()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@LoginId",LoginId),
                    new SqlParameter("@Amount",Amount),
                    new SqlParameter("@PaymentMode",PaymentMode),
                    new SqlParameter("@TransactionNo",TransactionNo),
                    new SqlParameter("@TransactionDate",TransactionDate),
                    new SqlParameter("@PaymentSlip",PaymentSlip)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.FamilynWalletRequest, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
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
    public class FamilynWalletResponse
    {
        public FamilynWallet? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    } 
}
