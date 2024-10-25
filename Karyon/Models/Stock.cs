using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class Stock : Menu
    {
        public int Fk_VarientId { get; set; }
        public string? FranchiseLoginId { get; set; }
        public string? MRP { get; set; }
        public string? PV { get; set; }
        public string? OfferedPrice { get; set; }
        public string? AvailableQty { get; set; }
        public string? FranchiseId { get; set; }

        public DataSet GetFranchiseStock()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@FranchiseLoginId",FranchiseLoginId),
                    new SqlParameter("@Fk_Product",Fk_VarientId),
                    new SqlParameter("@Page",Page),
                    new SqlParameter("@Size",Size)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetFranchiseStock, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetStockDetails()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@Fk_FranchiseId",FranchiseId),
                                      new SqlParameter("@Fk_VarientId",Fk_VarientId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetStockDetails, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public DataSet GetProductList()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@Fk_FranchiseId",FranchiseId),
                                      new SqlParameter("@Fk_VarientId",Fk_VarientId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.ProductsListForFranchisee, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class FranchiseeStock
    {
        public List<StockData>? StockList { get; set; }
    }
    public class StockData
    {
        public string? Fk_VarientId { get; set; }
        public string? ProductName { get; set; }
        public string? CreditQty { get; set; }
        public string? DebitQty { get; set; }
    }
    public class StockDetails
    {
        public List<StockDetailsData>? StockList { get; set; }
    }
    public class StockDetailsData
    {
        public string? OrderNo { get; set; }
        public string? LoginId { get; set; }
        public string? ContactPerson { get; set; }
        public string? Credit { get; set; }
        public string? Debit { get; set; }
    }
    public class StockResponse
    {
        public FranchiseeStock? Response { get; set; }
        public string? Message { get; set; }
        public int? Status { get; set; }
    }
    public class StockDetailsResponse
    {
        public StockDetails? Response { get; set; }
        public string? Message { get; set; }
        public int? Status { get; set; }
    }
    public class FranchiseeStockResponse
    {
        public FranchiseeStockDetails? Response { get; set; }
        public string? Message { get; set; }
        public int? Status { get; set; }
    }
    public class FranchiseeStockDetails
    {
        public List<FranchiseeProductData>? ProductList { get; set; }

    }
    public class FranchiseeProductData
    {
        public string? ProductName { get; set; }
        public string? VarientId { get; set; }
        public decimal? OfferedPrice { get; set; }
        public decimal? MRP { get; set; }
        public decimal? PV { get; set; }

    }
    public class ProductsListResponse
    {
        public ProductsLists? Response { get; set; }
        public string? Message { get; set; }
        public int? Status { get; set; }
    }
    public class ProductsLists
    {
        public List<ProductsListData>? ProductList { get; set; }

    }
    public class ProductsListData
    {
        public string? ProductName { get; set; }
        public string? VarientId { get; set; }
        public decimal? OfferedPrice { get; set; }
        public decimal? MRP { get; set; }
        public decimal? PV { get; set; }
        public decimal? BoxPrice { get; set; }
        public int? BoxOty { get; set; }

    }

}
