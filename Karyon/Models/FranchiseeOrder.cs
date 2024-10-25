using System.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace Karyon.Models
{
    public class FranchiseeCartList
    {
        public List<FranchiseeCartListData>? CartList { get; set; }
    }
    public class FranchiseeOrderRequest : Menu
    {
        public string? Fk_FranchiseId { get; set; }
        public string? Fk_ProductId { get; set; }
        public int? Quantity { get; set; }
        public string? Pk_Id { get; set; }
        public string? PaymentMode { get; set; }
        public string? OrderFrom { get; set; }
        public DataSet GetTempOrder()
        {
            try
            {
                SqlParameter[] para = {
                          new SqlParameter("@AddedBy",Fk_FranchiseId),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetTempOrder, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet CreateOrderTemp()
        {
            try
            {
                SqlParameter[] para = {
                        new SqlParameter("@Fk_ProductId",Fk_ProductId),
                        new SqlParameter("@Qty",Quantity),
                        new SqlParameter("@AddedBy",Fk_FranchiseId),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.CreateOrderTemp, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet DeleteTempOrder()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@Pk_Id",Pk_Id)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.DeleteOrderTemp, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GenerateFranchiseOrder()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@AddedBy",AddedBy),
                    new SqlParameter("@PaymentMode",  PaymentMode),
                    new SqlParameter("@WalletType",  WalletType),
                    new SqlParameter("@OrderFrom",  OrderFrom)
                                  };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GenerateFranchiseOrder, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class FranchiseeCartListData : Menu
    {
        public string? Pk_Id { get; set; }
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public decimal? MRP { get; set; }
        public decimal? BV { get; set; }
        public int? Quantity { get; set; }
        public decimal? TotalBv { get; set; }
        public decimal? Bottles { get; set; }
        public decimal? GSTPerc { get; set; }
        public decimal? GSTAmt { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? Total { get; set; }

    }
    public class FranchiseeAddtoCartResponse
    {
        public FranchiseeCartList? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class FranchiseeCartListResponse
    {

        public FranchiseeCartList? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class FranchiseeDeleteCartResponse
    {

        public FranchiseeCartList? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }

    }
    public class PlaceFranchiseeOrderResponse
    {
        public PlaceFranchiseeOrder? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class PlaceFranchiseeOrder
    {
        public string? OrderNo { get; set; }
        public decimal? OrderAmount { get; set; }
    }
   
}
