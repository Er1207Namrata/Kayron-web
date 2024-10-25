using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class ForgotId : Menu
    {
        public List<ForgotIdData>? IdList { get; set; } 
    }
    public class ForgotIdRequest : Menu
    {
        public string? MobileNo { get; set; }
        public string? IdType { get; set; }
        public int? OTP { get; set; }
        public DataSet ForgotId()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@MobileNo",MobileNo),
                new SqlParameter("@IdType",IdType),
                new SqlParameter("OTP",OTP)
            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.VerifyOTPForForgotId, para);
            return ds;

        }
    }
    public class ForgotIdData : Menu
    {
        public string? Name { get; set; }
        //public string? LoginId { get; set; }
        public string? OldLoginId { get; set; }
    }
    public class ForgotIdResponse
    {
        public ForgotId? Response { get; set; } 
        public string? Message { get; set; } 
        public int? Status { get; set; } 
    }

}
