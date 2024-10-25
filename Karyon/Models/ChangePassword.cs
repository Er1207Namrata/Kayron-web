using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class ChangePassword : Menu
    {
        public int CustomerId { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
        //public string? LoginId { get; set; }
        public string? OTP { get; set; }
        //public string? MobileNo { get; set; }
        public string? IdType { get; set; }

        public DataSet PasswordChange()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@Fk_MemId",CustomerId),
                                      new SqlParameter("@OldPassword",OldPassword),
                                      new SqlParameter("@NewPassword",NewPassword),
                                      new SqlParameter("@OpCode",OpCode)
                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.ChangePassword, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet OTPSend()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@LoginId",LoginId),
                                      new SqlParameter("@IdType",IdType),
                                      new SqlParameter("@MobileNo",MobileNo)
                                  };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.SendOtp, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet VerifyOTP()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@LoginId",LoginId),
                                      new SqlParameter("@OTP",OTP),
                                      new SqlParameter("@MobileNo",MobileNo),
                                      new SqlParameter("@IdType",IdType),


                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.VerifyOTP, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet ForgetPassword()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@LoginId",LoginId),
                                      new SqlParameter("@Password",NewPassword),
                                      new SqlParameter("@IdType",IdType),


                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.ForgotPassword, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class ChangePasswordResponse
    {
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class SendOtpResponse
    {
        public SendOtpforLogin? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
        public int? IsMultiple { get; set; }
    }
    public class VerifyOtpResponse
    {
        public int? Status { get; set; }
        public string? Message { get; set; }
        public string? LoginId { get; set; }
        public string? Old_LoginId { get; set; }
        public string? Name { get; set; }
    }
    public class ForgetPasswordResponse
    {
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class SendOtpforLogin
    {
        public List<SendOtpforLoginData>? IdList { get; set; }
    }
    public class SendOtpforLoginData
    {
        public string? Name { get; set; }
        public string? LoginId { get; set; }
    }

}
