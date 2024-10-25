using System.Data.SqlClient;
using System.Data;

namespace Karyon.Models
{
    public class CheckProductTypeRequest : Menu
    {
        public string? CustomerId { get; set; }
        public DataSet CheckProductType()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_MemId", CustomerId)
            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.CheckProduct, para);
            return ds;
        }

    }
    public class CheckProductTypeResponse
    {
        public CheckProductTypeData? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }

    }
    public class CheckProductTypeData
    {
        public List<CartDetails>? CartDetails { get; set; }

    }

}
