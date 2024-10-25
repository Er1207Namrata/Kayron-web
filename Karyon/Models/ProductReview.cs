using System.Data.SqlClient;
using System.Data;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.Reflection.Emit;

namespace Karyon.Models
{
    public class ProductReview
    {
        public List<ProductReviewData>? ReviewList { get; set; }
    }
    public class ProductReviewRequest:Menu
    {
        public long? CustomerId { get; set; }
        public long? Fk_ProductId { get; set; }
        public long? Fk_OrderId { get; set; }
        public decimal? Star { get; set; }   
        public string? Rating { get; set; }   
        public DataSet AddProductReview()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@CustomerId",CustomerId),
                new SqlParameter("@Fk_ProductId",Fk_ProductId),
                new SqlParameter("@Fk_OrderId",Fk_OrderId),
                new SqlParameter("@Star",Star),
                new SqlParameter("@Rating",Rating)
            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.ProductReview, para);
            return ds;
        }
        public DataSet GetProductReview()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_MemId",CustomerId),
                new SqlParameter("@Fk_OrderId",Fk_OrderId),
                new SqlParameter("@OpCode",OpCode),
            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.GetReview, para);
            return ds;
        }

    }
    public class ProductReviewData
    {
            public string? LoginId {get;set;}
            public string? ContactName {get;set;}
            public string? ProductName  {get;set;}
            public decimal? Star {get;set;}
            public long? Fk_OrderId { get; set; }
            public string? Rating { get; set; }
            public string? ProductImage { get; set; }
            public string? AddedDate { get; set; }
    }
    public class ProductReviewResponse
    {
        public ProductReview? Response { get; set; }
        public string? Message { get; set; }
        public int? Status { get; set; }    
    }
}

   