using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class BankTransaction : Menu
    {
        public string? encryptedKey { get; set; }
        public string? iv { get; set; }
        public string? encryptedData { get; set; }
        public DataSet DALICICITrasactionForAPI(MTransactionHistory _objDetails)
        {
            try
            {
                SqlParameter[] queryParameters ={
                                            new SqlParameter("@ClientCode", _objDetails.ClientCode),
                                            new SqlParameter("@VirtualACCode", _objDetails.VirtualAccountNumber),
                                            new SqlParameter("@Mode", _objDetails.MODE),
                                            new SqlParameter("@UTR", _objDetails.UTR),
                                            new SqlParameter("@SenderRemark", _objDetails.SenderRemark),
                                            new SqlParameter("@CustomerAccountNumber", _objDetails.ClientAccountNo),
                                            new SqlParameter("@Amount", _objDetails.Amount),
                                            new SqlParameter("@PayeeName", _objDetails.PayerName),
                                            new SqlParameter("@PayeeAccountNumber", _objDetails.PayerAccNumber),
                                            new SqlParameter("@PayeeBankIFSC", _objDetails.PayerBankIFSC),
                                            new SqlParameter("@PayeePaymentDate", _objDetails.PayerPaymentDate ?? string.Empty),
                                            new SqlParameter("@BankInternalTransactionNumber", _objDetails.BankInternalTransactionNumber ?? string.Empty),
                                              };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.InsTransactionHistory, queryParameters);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class ECollResponse
    {
        public string? ClientCode { get; set; }
        public string? ClientAccountNo { get; set; }
        public string? VirtualAccountNumber { get; set; }
        public string? Mode { get; set; }
        public string? CustomerAccountNo { get; set; }
        public string? Amount { get; set; }
        public string? PayerName { get; set; }
        public string? SenderRemark { get; set; }
        public string? PayerAccNumber { get; set; }
        public string? PayerBankIFSC { get; set; }
        public string? PayerPaymentDate { get; set; }
        public string? BankInternalTransactionNumber { get; set; }
        public string? UTR { get; set; }
    }
    public class MTransactionHistory
    {
        public string? ClientCode { get; set; }
        public string? VirtualAccountNumber { get; set; }
        public string? MODE { get; set; }
        public string? UTR { get; set; }
        public string? SenderRemark { get; set; }
        public string? ClientAccountNo { get; set; }
        public string? Amount { get; set; }
        public string? PayerName { get; set; }
        public string? PayerAccNumber { get; set; }
        public string? PayerBankIFSC { get; set; }
        public string? PayerPaymentDate { get; set; }
        public string? BankInternalTransactionNumber { get; set; }



    }
    public class MTransactionHistoryResposne
    {
        public string? CODE { get; set; }
        public string? Response { get; set; }
    }
}
