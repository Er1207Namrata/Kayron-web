using System.Data.SqlClient;
using System.Data;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Karyon.Models
{
    public class CreateCustomerOrder : Menu
    {
        public List<CreateCustomerOrderData>? CartList { get; set; }
    }
    public class CreateCustomerOrderRequest : Menu
    {
        public string? Pk_Id { get; set; }
        public string? Fk_VarientId { get; set; }
        public string? Fk_FranchiseId { get; set; }
        public string? PaymentMode { get; set; }
        public string? TransactionDate { get; set; }
        public string? TransactionNo { get; set; }
        public string? PaymentSlip { get; set; }
        //public string? LoginId { get; set; }
        public int? Quantity { get; set; }
        public DataSet CreateCustomerOrderTemp()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@Fk_ProductId",Fk_VarientId),
                    new SqlParameter("@Qty",Quantity),
                    new SqlParameter("@AddedBy",Fk_FranchiseId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.CreateCustomerOrderTemp, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetCustomerTempOrder()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@AddedBy",Fk_FranchiseId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetCustomerTempOrder, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GenerateCustomerOrder()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@AddedBy",Fk_FranchiseId),
                    new SqlParameter("@PaymentMode",PaymentMode),
                    new SqlParameter("@TransactionDate",TransactionDate),
                    new SqlParameter("@TransactionNo",TransactionNo),
                    new SqlParameter("@FileName",PaymentSlip),
                    new SqlParameter("@LoginId",LoginId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GenerateCustomerOrder, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet DeleteCustomerTempOrder()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@Pk_Id",Pk_Id)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.DeleteCustomerTempOrder, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class CreateCustomerOrderData
    {
        public string? Pk_Id { get; set; }
        public string? ProductName { get; set; }
        public decimal? BV { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? TotalBv { get; set; }
        public decimal? GSTPerc { get; set; }
        public decimal? Total { get; set; }
        public decimal? Price { get; set; }
        public decimal? GSTAmt { get; set; }
        public decimal? TotalPrice { get; set; }
    }
    public class CreateCustomerOrderResponse
    {
        public CreateCustomerOrder? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class CustomerOrderListResponse
    {
        public CreateCustomerOrder? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }

    }
    public class DeleteCustomerOrderResponse
    {
        public CreateCustomerOrder? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }

    }
    public class PlaceCustomerOrderResponse
    {
        public int? Status { get; set; }
        public string? Message { get; set; }

    }
}
