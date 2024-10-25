using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class Registration : Menu
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? MobileNo { get; set; }
        public string? PanCard { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Pincode { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? DeviceId { get; set; }
        public string? DeviceToken { get; set; }
        public string? SponsorLoginId { get; set; }
        //public string? Password { get; set; }
        public string? RegistrationFrom { get; set; }
        public string? Leg { get; set; }
        public string? Pk_FranchiseId { get; set; }
        //public DataTable? dtDetails { get; set; }
        public string? PancardImg { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? Otp { get; set; }

        public DataSet CustomerRegistration()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@SponsorLoginId", SponsorLoginId),
                                      new SqlParameter("@Leg", Leg),
                                      new SqlParameter("@FirstName", FirstName),
                                      new SqlParameter("@MiddleName", MiddleName),
                                      new SqlParameter("@LastName", LastName),
                                      new SqlParameter("@MobileNo",MobileNo),
                                      new SqlParameter("@PanCard",PanCard),
                                      new SqlParameter("@Email",Email),
                                      new SqlParameter("@Address",Address),
                                      new SqlParameter("@Pincode",Pincode),
                                      new SqlParameter("@State",State),
                                      new SqlParameter("@City",City),
                                      new SqlParameter("@DeviceId",DeviceId),
                                      new SqlParameter("@DeviceToken",DeviceToken),
                                      new SqlParameter("@Password",Password),
                                      new SqlParameter("@RegistrationFrom",RegistrationFrom),
                                      new SqlParameter("@PancardImg",PancardImg),
                                      new SqlParameter("@Otp",Otp)

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.Registration, para);
                return ds;

               


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
