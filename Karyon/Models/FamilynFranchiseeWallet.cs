using System.Data.SqlClient;
using System.Data;
using Karyon.Models;

namespace Karyon.Models
{
    public class FamilynFranchiseeWalletRequest : Menu
    {
        //public string? LoginId { get; set; }
        public int? Fk_FranchiseId { get; set; }
        public string? Name { get; set; }
        public string? PaymentMode { get; set; }
        public decimal? Amount { get; set; }
        public string? TransactionNo { get; set; }
        public string? TransactionDate { get; set; }
        public string? PaymentSlip { get; set; }
        public IFormFile? ImageFile { get; set; }
        public DataSet FamilynWalletRequest()
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
                DataSet ds = Connection.ExecuteQuery(ProcedureName.FranchiseeFamilynWalletRequest, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class FamilynFranchiseeWalletResponse
    {
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
}

