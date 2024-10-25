using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class FranchiseWallet : Menu
    {
        //public string? LoginId { get; set; }
        //public int? Fk_FranchiseId { get; set; }
        public int? BankId { get; set; }
        public string? Name { get; set; }
        public string? PaymentMode { get; set; }
        public decimal? Amount { get; set; }
        public string? TransactionNo { get; set; }
        public string? TransactionDate { get; set; }
        public string? PaymentSlip { get; set; }
        public string? MerchantTranId { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? TXNID { get; set; }
        public string? OrderNo { get; set; }
        public string? BANKTXNID { get; set; }
        public string? CHECKSUMHASH { get; set; }
        public string? RespMsg { get; set; }
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
                                      new SqlParameter("@TXNID",TXNID),
                                      new SqlParameter("@BANKTXNID",BANKTXNID),
                                      new SqlParameter("@CHECKSUMHASH",CHECKSUMHASH),
                                      new SqlParameter("@OrderNo",OrderNo),
                                      new SqlParameter("@Fk_BankId",BankId),

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
        public DataSet GetFranchiseeBalance()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_FranchiseId",Fk_FranchiseId)

                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetFranchiseeBalance, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetFranchiseLedger()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@LoginId",LoginId),
                    new SqlParameter("@Page",Page)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetFranchiseLedger, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetAssociateLedger()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@LoginId",LoginId),
                    new SqlParameter("@Page",Page),
                    new SqlParameter("@Size",Size)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetAssociateLedger, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class FranchiseeWalletBalance
    {
        public FranchiseeWalletData? WalletBalance { get; set; }
    }
    public class FranchiseeWalletData
    {
        public decimal? KaryonWallet { get; set; }
        public decimal? FUPWallet { get; set; }
        public decimal? OFPWallet { get; set; }
    }
    public class FranchiseeWalletResponse
    {
        public FranchiseeWalletBalance? Response { get; set; }
        public string? Message { get; set; }
        public int? Status { get; set; }
    }

    public class FranchiseeLedgerData
    {
        public string? TransactionDate { get; set; }
        public string? Narration { get; set; }
        public decimal? Cramount { get; set; }
        public decimal? Dramount { get; set; }
        public decimal? Balance { get; set; }
    }
    public class FranchiseeLedgerResponse
    {
        public FranchiseeLedger? Response { get; set; }
        public string? Message { get; set; }
        public int? Status { get; set; }
    
    }
    public class FranchiseeLedger
    {
        public List<FranchiseeLedgerData>? LedgerList { get; set; }
    }


}
