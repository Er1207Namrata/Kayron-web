using System.Data.SqlClient;
using System.Data;

namespace Karyon.Models
{
    public class OpenOrder
    {
        public List<OpenOrderTempData>? TempOpenOrderList { get; set; }
    }
    public class OpenOrderRequest : Menu
    {
        public string? Pk_Id { get; set; }
        public string? Fk_ProductId { get; set; }
        public string? Qty { get; set; }
        public string? Amount { get; set; }
        public string? OrderType { get; set; }
        public string? BillAddress { get; set; }
        public string? BillTo { get; set; }
        public string? BillContactNo { get; set; }
        public string? BillGSTNo { get; set; }
        //public string? LoginId { get; set; }
        public string? CommissionPer { get; set; }
        public string? Pincode { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public DataSet GetCustomerTempOpenOrder()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@AddedBy",AddedBy)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetCustomerTempOpenOrder, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet DeleteCustomerTempOpenOrder()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@Pk_Id",Pk_Id)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.DeleteCustomerTempOpenOrder, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet CreateTempOpenOrder()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@Fk_ProductId",Fk_ProductId),
                    new SqlParameter("@Qty",Qty),
                    new SqlParameter("@Amount",Amount),
                    new SqlParameter("@OrderType",OrderType),
                    new SqlParameter("@AddedBy",AddedBy)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.CreateTempOpenOrder, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GenerateCustomerOpenOrder()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@AddedBy",AddedBy),
                    new SqlParameter("@BillAddress",BillAddress),
                    new SqlParameter("@BillTo",BillTo),
                    new SqlParameter("@BillContactNo",BillContactNo),
                    new SqlParameter("@BillGSTNo",BillGSTNo),
                    new SqlParameter("@LoginId",LoginId),
                    new SqlParameter("@OrderType",OrderType),
                    new SqlParameter("@BillToPincode",Pincode),
                    new SqlParameter("@BillToState",State),
                    new SqlParameter("@BillToCity",City),
                    new SqlParameter("@CommissionPer",CommissionPer)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GenerateCustomerOpenOrder, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetAddressDetails()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@OpCode",48),
                    new SqlParameter("@Value",LoginId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetMasterData, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetAssociateAddress()
            {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@OpCode",48),
                    new SqlParameter("@Value",LoginId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetMasterData, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetCustomerAddress()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@OpCode",48),
                    new SqlParameter("@Value",LoginId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetMasterData, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class OpenOrderTempData
    {
        public string? Pk_Id { get; set; }
        public string? ProductName { get; set; }
        public int? Qty { get; set; }
        public decimal? MRP { get; set; }
        public decimal? TotalBv { get; set; }
        public decimal? GSTPerc { get; set; }
        public decimal? Total { get; set; }
        public decimal? Price { get; set; }
        public decimal? GSTAmt { get; set; }
        public decimal? TotalPrice { get; set; }
    }
    public class OpenOrderResponse
    {
        public string? Message { get; set; }
        public int? Status { get; set; }
    }
    public class OpenOrderTempResponse
    {
        public OpenOrder? Response { get; set; }
        public string? Message { get; set; }
        public int? Status { get; set; }
    }
    public class OpenOrderGetAddressResponse
    {
        public AddressDetailsData? Response { get; set; }
        public string? Message { get; set; }
        public int? Status { get; set; }
    }
    public class AddressDetailsData
    {
        public string? ContactNo { get; set; }
        public string? Address { get; set; }
        public string? Locality { get; set; }
        public string? Pincode { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Landmark { get; set; }
        public string? GSTNo { get; set; }
    }
}
