using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class SendOtpRequest : Menu
    {
        //public string? LoginId { get; set; }
        public string? IdType { get; set; }
        public DataSet OTPSend()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@LoginId",LoginId),
                                      new SqlParameter("@IdType",IdType)
                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.SendOtp, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class SendOtpR12esponse
    {
        public SendOtp? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class SendOtpData
    {
        public string? OTP { get; set; }
        public string? MobileNo { get; set; }
        public string? Flag { get; set; }
        public string? Name { get; set; }
        public string? LoginId { get; set; }
        public string? OldLoginId { get; set; }
    }
    public class SendOtp
    {
        public SendOtpData? OtpData { get; set; }
    }
}
