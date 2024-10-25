using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class LoginRequest : Menu
    {
        public string? Otp { get; set; }
        //public string? LoginId { get; set; }
        //public string? Password { get; set; }
        public string? LoginFrom { get; set; }
        public string? DeviceId { get; set; }
        public string? DeviceToken { get; set; }
        public string? LoginType { get; set; }
        //public string? Status { get; set; }

        public DataSet AdminLogin()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@LoginId", LoginId),
                    new SqlParameter("@Password",Password)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.AdminLogin, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet LoginAction()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@Password",Password),
                                      new SqlParameter("@DeviceId",DeviceId),
                                      new SqlParameter("@DeviceToken",DeviceToken),
                                      new SqlParameter("@LoginFrom",LoginFrom),
                                      new SqlParameter("@LoginType",LoginType),

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.Login, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet LoginByAdmin()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@MobileNo",MobileNo),
                                      new SqlParameter("@Password",Password),
                                      new SqlParameter("@LoginFrom",LoginFrom)
                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.LoginByAdmin, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet LoginFromOtp()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@LoginId", LoginId),
                    new SqlParameter("@Otp",Otp),
                    new SqlParameter("@DeviceId",DeviceId),
                    new SqlParameter("@DeviceToken",DeviceToken),
                    new SqlParameter("@LoginFrom",LoginFrom),
                    new SqlParameter("@LoginType",LoginType)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.LoginFromOtp, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet AutoLoginAction()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@MemberId", Fk_MemId),
                                      new SqlParameter("@DeviceId",DeviceId),
                                      new SqlParameter("@DeviceToken",DeviceToken),
                                      new SqlParameter("@LoginFrom",LoginFrom),
                                      new SqlParameter("@LoginType",LoginType),

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.AutoLogin, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class LoginResponse
    {
        public Login? Response { get; set; }        
        public string? Message { get;  set; }
        public int Status { get;  set; }
    }
    public class Login
    {
        public long CustomerId { get; set; }
        public long FK_FranchiseTypeId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? LoginId { get; set; }
        public string? UserType { get; set; }
        public int? TotalOrder { get; set; }
        public decimal? RegistrationAmount { get; set; }
        public string? IskycApproved { get; set; }
        public string? IsBusinessId { get; set; }
        public string? Message { get; set; }
        public string? Title { get; set; }
        public string? MobileNo { get; set; }
        //public string? AppStatus { get; set; }
    }
    public class LoginFromMobileResponse
    {
        public LoginData? Response { get; set; }
        public string? Message { get; set; }
        public int Status { get; set; }
    }
    public class LoginData
    {
        public List<Login>? MemberList { get; set; }
    }
}
