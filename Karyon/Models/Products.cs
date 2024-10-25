using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class ProductsRequest : Menu
    {
        public int? ProductId { get; set; }
        public int? CategoryId { get; set; }
        public int? CustomerId { get; set; }
       
        public DataSet GetProducts()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@ProductId", ProductId),
                                      new SqlParameter("@CategoryId",CategoryId),
                                      new SqlParameter("@Fk_MemId",CustomerId)
                                     
                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetProducts,para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class ProductDetails
    {
        public string? HSNCode { get; set; }
        public long? ProductId { get; set; }
        public string? ProductEncryptId { get; set; }
        public string? ProductName { get; set; }
        public string? Model { get; set; }
        public string? BrandName { get; set; }
        public string? Image { get; set; }
        public string? StarRating { get; set; }
        public string? ProductsDetails { get; set; }
        public float? MRP { get; set; }
        public string? CartStatus { get; set; }
        public List<VarientData>? VarientList { get; set; }
    }
    
    public class VarientData
    {
        public string? Size { get; set; }
        public string? UnitName { get; set; }
        public decimal? PV { get; set; }
        public decimal? Weight { get; set; }
        public int? CustomerId { get; set; }
        public long VarientId { get; set; }
        public string? StarRating { get; set; }
        public string? EncryptId { get; set; }
        public long ProductId { get; set; }
        public string? CartStatus { get; set; }
        public string? CartQty { get; set; }
        public List<ImageData>? ImageList { get; set; }
        public decimal MRP { get;  set; }
        public decimal OfferedPrice { get;  set; }
    }
    public class ProductReposne
    {

        public Products? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class Products
    {
        public List<ProductDetails>? ProductList { get; set; }
        public List<Collection>? PopularItemsList { get; set; }
        public List<CategoriesRequest>? CategoryList { get; set; }
        public List<RatingData>? RatingList { get; set; }
    }
}
