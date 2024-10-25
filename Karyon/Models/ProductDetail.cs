using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class ProductDetail : Menu
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public string? Fk_FranchiseId { get; set; }
        public DataSet GetProductDeatils()
        {
            try
            {
                SqlParameter[] para = {                                     
                    new SqlParameter("@ProductId",ProductId),
                    new SqlParameter("@Fk_FranchiseId",Fk_FranchiseId),
                    new SqlParameter("@Fk_MemId",CustomerId)                                      
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetProductDetails, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }

    }
    public class ProductDetailsResponse
    {

        public ProductData? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class ProductData
    {
        public string? Category { get; set; }
        public long? ProductId { get; set; }
        public string? ProductEncryptId { get; set; }
        public string? ProductName { get; set; }
        public string? Tags { get; set; }
        public string? CartStatus { get; set; }
        public float? OfferPoint { get; set; }
        public string? Model { get; set; }
        public string? BrandName { get; set; }
        public string? Image { get; set; }
        public string? StarRating { get; set; }
        public string? ProductsDetails { get; set; }
        public float? MRP { get; set; }
        public float? SellPrice { get; set; }
        public List<RatingData>? RatingList { get; set; }
        public List<ImageData>? ImageList { get; set; }
        public List<Collection>? RelatedProductList { get; set; }
        public List<Collection>? SingleProduct { get; set; }
    }
  
    public class RatingData
    {
        public string? Name { get; set; }
        public string? LoginId { get; set; }
        public string? Rating { get; set; }
        public string? Star { get; set; }
        public string? RatingDate { get; set; }
    }
    public class ImageData
    {
        public string? ImageUrl { get; set; }
    }    
}
