using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class FranchiseRegistrationRequest : Menu
    {
        public string? SponsorLoginId { get; set; }
        public string? AssociateLoginId { get; set; }
        public string? CompanyName { get; set; }
        public string? ContactPerson { get; set; }
        public string? EmailId { get; set; }
        public string? MobileNo { get; set; }
        public string? Address { get; set; }
        public string? Pincode { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? GSTNo { get; set; }
        public string? PanCard { get; set; }
        //public string? Password { get; set; }
        public string? RegistrationFrom { get; set; }
        public string? DeviceId { get; set; }
        public string? DeviceToken { get; set; }
        public string? RegistrationAmount { get; set; }
        public string? FirmAddress { get; set; }
        public string? FK_FranchiseTypeId { get; set; }
        public string? Otp { get; set; }
        public string? FirstName { get; set; }
        //public string? LoginId { get; set; }
        public DataSet FranchiseRegistration()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@SponsorLoginId",SponsorLoginId),
                    new SqlParameter("@AssociateLoginId",AssociateLoginId),
                    new SqlParameter("@CompanyName",CompanyName),
                    new SqlParameter("@ContactPerson",ContactPerson),
                    new SqlParameter("@EmailId",EmailId),
                    new SqlParameter("@MobileNo",MobileNo),
                    new SqlParameter("@Password",Password),
                    new SqlParameter("@Address",Address),
                    new SqlParameter("@Pincode",Pincode),
                    new SqlParameter("@GSTNo",GSTNo),
                    new SqlParameter("@PanCard",PanCard),
                    new SqlParameter("@RegistrationFrom",RegistrationFrom),
                    new SqlParameter("@DeviceId",DeviceId),
                    new SqlParameter("@DeviceToken",DeviceToken),
                    new SqlParameter("@FirmAddress",FirmAddress),
                    new SqlParameter("@FK_FranchiseTypeId",FK_FranchiseTypeId),
                    new SqlParameter("@Otp",Otp),
                    new SqlParameter("@IsSpecial","0")
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.FranchiseRegistration, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet FranchiseeRequest()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@AssociateLoginId",AssociateLoginId),
                                      new SqlParameter("@CompanyName",CompanyName),
                                      new SqlParameter("@ContactPerson",ContactPerson),
                                      new SqlParameter("@EmailId",EmailId),
                                      new SqlParameter("@MobileNo",MobileNo),
                                      new SqlParameter("@Address",Address),
                                      new SqlParameter("@Pincode",Pincode),
                                      new SqlParameter("@GSTNo",GSTNo),
                                      new SqlParameter("@PanCard",PanCard),
                                      new SqlParameter("@FirmAddress",FirmAddress),
                                      new SqlParameter("@FK_FranchiseTypeId",FK_FranchiseTypeId),
                                      new SqlParameter("@RegistrationFrom",RegistrationFrom),
                                      new SqlParameter("@OpCode",1),


                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.FranchiseeRequest, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class FranchiseResponse
    {
        public Franchise? Response { get; set; }
        public string? Message { get; set; }
        public int Status { get; set; }
    }
    public class Franchise
    {
        public long FranchiseId { get; set; }
        public string? CompanyName { get; set; }
        public string? ContactPerson { get; set; }
        public string? LoginId { get; set; }
        public string? Password { get; set; }
    }
}
