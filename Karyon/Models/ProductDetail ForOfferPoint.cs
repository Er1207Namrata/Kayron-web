using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class ProductDetailForOfferPoint : Menu
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        
        public DataSet GetProductDeatilsForOfferPoint()
        {
            try
            {
                SqlParameter[] para = {                                     
                    new SqlParameter("@ProductId",ProductId)                                                   
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetProductDetailsForOfferPoint, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }       
    }
    public class ProductDetailsForOfferPointResponse
    {

        public ProductForOfferPointData? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class ProductForOfferPointData
    {       
        public long? ProductId { get; set; }
        public string? ProductEncryptId { get; set; }
        public string? ProductName { get; set; }       
        public float? OfferPoint { get; set; }      
            
    }         
}
