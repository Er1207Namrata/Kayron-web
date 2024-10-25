using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class KaryonPoints 
    {
        public List<KaryonPointsData>? RequestList { get; set; }       
    }
    public class KaryonPointsData 
    {
        public decimal? Amount { get; set; }
        public string? TransactionNo { get; set; }
        public string? TransactionDate { get; set; }
        public string? PaymentMode { get; set; }
        public string? Status { get; set; }
        public string? RequestedDate { get; set; }
     
    }
    public class KaryonPointsRequest :Menu
    {
        public string? LoginId { get; set; }
        public string? Name { get; set; }
        public string? PaymentMode { get; set; }
        public decimal? Amount { get; set; }
        public string? TransactionNo { get; set; }
        public string? MerchantTranId { get; set; }
        public string? BankId { get; set; }
        public string? TransactionDate { get; set; }
        public string? PaymentSlip { get; set; }
        public DataTable? dtdetails { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? Status { get;  set; }
        public string? TXNID { get;  set; }
        public string? BANKTXNID { get;  set; }
        public string? CHECKSUMHASH { get;  set; }
        public string? RespMsg { get;  set; }
        public int? OpCode { get;  set; }
        public string? OrderNo { get;  set; }
        public int? IsLive { get;  set; }

        public DataSet RequestKaryonPoints()
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
                    new SqlParameter("@Status",Status),
                    new SqlParameter("@TXNID",TXNID),
                    new SqlParameter("@BANKTXNID",BANKTXNID),
                    new SqlParameter("@CHECKSUMHASH",CHECKSUMHASH),
                    new SqlParameter("@RespMsg",RespMsg),
                    new SqlParameter("@OpCode",OpCode),
                    new SqlParameter("@OrderNo",TransactionNo),
                    new SqlParameter("@Fk_BankId",BankId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.EwalletRequest, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetKaryonPointsList()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@LoginId",LoginId),
                    new SqlParameter("@MerchantTranId",MerchantTranId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.EwalletList, para);
                return ds;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
    public class KaryonPointsResponse
    {
        public KaryonPoints? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
        public string? OrderAmount { get;  set; }
        public string? OrderNo { get;  set; }
        public string? TxnToken { get;  set; }
    }


}
