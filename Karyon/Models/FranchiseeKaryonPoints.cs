using System.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace Karyon.Models
{
    public class FranchiseeKaryonPoints
    {
        public List<FranchiseeKaryonPointsData>? WalletList { get; set; }
    }
    public class FranchiseeKaryonPointsRequest : Menu
    {
        //public string? LoginId { get; set; }
        public string? PaymentMode { get; set; }
        public decimal? Amount { get; set; }
        public string? TransactionNo { get; set; }
        public string? TransactionDate { get; set; }
        public string? PaymentSlip { get; set; }
        public string? OrderNo { get; set; }
        public int? IsLive { get; set; }
        public DataSet FranchisewalletRequest()
        {
            try
            {
                SqlParameter[] para = {
                        new SqlParameter("@LoginId",LoginId),
                        new SqlParameter("@Amount",Amount),
                        new SqlParameter("@PaymentMode",PaymentMode),
                        new SqlParameter("@TransactionNo",TransactionNo),
                        new SqlParameter("@TransactionDate",TransactionDate),
                        new SqlParameter("@PaymentSlip",PaymentSlip),
                        new SqlParameter("@OrderNo",OrderNo),
                 };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.FranchisewalletRequest, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet FranchiseWalletList()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@LoginId",LoginId),
                    new SqlParameter("@Status",Status==""?null:Status),
                    new SqlParameter("@Page",Page),
                    new SqlParameter("@Size",Size)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.FranchiseWalletList, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class FranchiseeKaryonPointsData
    {
        public decimal? Amount { get; set; }
        public string? RequestedDate { get; set; }
        public string? Status { get; set; }
        public string? TransactionDate { get; set; }
        public string? PaymentMode { get; set; }
        public string? TransactionNo { get; set; }
    }
    public class FranchiseeKaryonPointsResponse
    {
        public FranchiseeKaryonPoints? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
        public string? OrderAmount { get;  set; }
        public string? OrderNo { get;  set; }
        public string? TxnToken { get;  set; }
    }
    public class FranchiseeWalletListResponse
    {
        public FranchiseeKaryonPoints? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
}
