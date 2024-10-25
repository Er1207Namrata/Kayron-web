using System.Data.SqlClient;
using System.Data;
using System.Reflection.Emit;

namespace Karyon.Models
{
    public class ValidateUserInfoRequest : Menu
    {
        public string? UserType { get; set; }
        public string? Type { get; set; }
        public string? Value { get; set; }
        public DataSet ValidateDetails()
        {
            SqlParameter[] para = {
            new SqlParameter("@UserType", UserType),
            new SqlParameter("@Type", Type),
            new SqlParameter("@Value", Value)
        };
        DataSet ds = Connection.ExecuteQuery(ProcedureName.ValidateInfo, para);
        return ds;
        }
    }
    public class ValidateUserInfoResponse
    {
        public string? Message { get; set; }
        public int? Status { get; set; }
    }


}
