using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class PlaceOrder : Menu
    {
        public int CustomerId { get; set; }
        public int Pk_AddressId { get; set; }
        public string? PaymentMode { get; set; }
        //public string? LoginId { get; set; }
        //public string? Status { get; set; }
        public string? OrderNo { get; set; }
        public string? TxnId { get; set; }
        public string? BankTxnId { get; set; }
        public string? CheckSumHash { get; set; }
        public string? RespMsg { get; set; }
        public string? OrderType { get; set; }
        public string? PackValue { get; set; }
        //public string? WalletType { get; set; }
        //public string? OpCode { get; set; }
        public string? OrderFrom { get; set; }
        public string? OrderAmount { get; set; }
        public int? IsLive { get; set; }
        public int? PromoCode { get; set; }
        public int? PromoCodeDiscountPrice { get; set; }

        public DataSet PlaceShoppingOrder()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@PaymentMode", PaymentMode),
                                      new SqlParameter("@Fk_MemId",CustomerId),
                                      new SqlParameter("@Fk_AddressId",Pk_AddressId),
                                      new SqlParameter("@OrderType",OrderType),
                                      new SqlParameter("@PackValue",PackValue),
                                      new SqlParameter("@WalletType",WalletType),
                                      new SqlParameter("@Status",Status),
                                      new SqlParameter("@OrderNo",OrderNo),
                                      new SqlParameter("@TxnId",TxnId),
                                      new SqlParameter("@BankTxnId",BankTxnId),
                                      new SqlParameter("@CheckSumHash",CheckSumHash),
                                      new SqlParameter("@RespMsg",RespMsg),
                                      new SqlParameter("@OpCode",OpCode),
                                      new SqlParameter("@OrderFrom",OrderFrom),
                                      new SqlParameter("@PromoCode",PromoCode),
                                      new SqlParameter("@PromoCodeDiscountPrice",PromoCodeDiscountPrice)
                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.PlaceOrder, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet PlaceOrderForMobile()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@PaymentMode", PaymentMode),
                                      new SqlParameter("@Fk_MemId",CustomerId),
                                      new SqlParameter("@Fk_AddressId",Pk_AddressId),
                                      new SqlParameter("@OrderType",OrderType),
                                      new SqlParameter("@PackValue",PackValue),
                                      new SqlParameter("@WalletType",WalletType),
                                      new SqlParameter("@Status",Status),
                                      new SqlParameter("@OrderNo",OrderNo),
                                      new SqlParameter("@TxnId",TxnId),
                                      new SqlParameter("@BankTxnId",BankTxnId),
                                      new SqlParameter("@CheckSumHash",CheckSumHash),
                                      new SqlParameter("@RespMsg",RespMsg),
                                      new SqlParameter("@OpCode",OpCode),
                                      new SqlParameter("@OrderFrom",OrderFrom)
                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.PlaceOrderForMobile, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class PlaceOrderResponse
    {
        public int? Status { get; set; }
        public string? Message { get; set; }
        public string? OrderAmount { get; set; }
        public string? OrderNo { get; set; }
        public string? TxnToken { get; set; }
        public string? TransactionType { get;  set; }
    }
    
}
