using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class ProfileRequest : Menu
    {
        public int? CustomerId { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Pincode { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        //public string? Status { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? MobileNo { get; set; }
        public string? AccNo { get; set; }
        public string? BankName { get; set; }
        public string? Branch { get; set; }
        public string? PanImage { get; set; }
        public string? IFSC { get; set; }
        public string? HolderName { get; set; }
        public string? PanCard { get; set; }
        public string? RankName { get; set; }
        public string? TotalOrders { get; set; }

        public IFormFile? ImageFile { get; set; }
        public IFormFile? ProfilePic { get; set; }
        public IFormFile? AddressProofFront { get; set; }
        public IFormFile? AddressProofBack { get; set; }
        public IFormFile? BankDoc { get; set; }
        public string? JoiningDate { get; set; }
        public string? SponsorId { get; set; }
        public string? SponsorName { get; set; }
        public string? PanStatus { get; set; }
        public string? AddressFrontStatus { get; set; }
        public string? AddressBackStatus { get; set; }
        public string? BankStatus { get; set; }
        public string? AddressProofImage { get; set; }
        public string? AddressProofBackImage { get; set; }
        public string? BankDocImage { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? Pk_FranchiseId { get; set; }
        public string? ContactPerson { get; set; }
        public string? CompanyName { get; set; }
        public string? GSTNo { get; set; }
        public string? FirmName { get; set; }
        public string? IsBid { get; set; }

        public DataSet GetAssociateProfile()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@FK_MemId",CustomerId)

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetAssociateProfile, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet ViewFranchiseeProfile()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@Pk_FranchiseId",Pk_FranchiseId)

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetFranchiseProfile, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet UpdateAssociateProfile()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@Email",Email),
                    new SqlParameter("@FK_MemId",CustomerId),
                    new SqlParameter("@Address",Address),
                    new SqlParameter("@Pincode",Pincode),
                    new SqlParameter("@State",State),
                    new SqlParameter("@City",City),
                    new SqlParameter("@Gender",Gender),
                    new SqlParameter("@AccNo",AccNo),
                    new SqlParameter("@BankName",BankName),
                    new SqlParameter("@Branch",Branch),
                    new SqlParameter("@PanImage",PanImage),
                    new SqlParameter("@IFSC",IFSC),
                    new SqlParameter("@PanCard",PanCard),
                    new SqlParameter("@HolderName",HolderName),
                    new SqlParameter("@AddressProofImage",AddressProofImage),
                    new SqlParameter("@AddressProofBackImage",AddressProofBackImage),
                    new SqlParameter("@BankDocImage",BankDocImage),
                    new SqlParameter("@GSTNo",GSTNo),
                    new SqlParameter("@FirmName",FirmName)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.UpdateAssociateProfile, para);
                return ds;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet UpdateFranchiseProfile()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@EmailId",Email),
                                      new SqlParameter("@Pk_FranchiseId",Pk_FranchiseId),
                                      new SqlParameter("@Address",Address),
                                      new SqlParameter("@Pincode",Pincode),
                                      new SqlParameter("@AccNo",AccNo),
                                      new SqlParameter("@BankName",BankName),
                                      new SqlParameter("@Branch",Branch),
                                      new SqlParameter("@IFSC",IFSC),
                                      new SqlParameter("@PanCard",PanCard),
                                      new SqlParameter("@HolderName",HolderName),
                                      new SqlParameter("@GstNo",GSTNo),
                                      new SqlParameter("@FirmName",FirmName),
                                      new SqlParameter("@Addedby",Pk_FranchiseId),

                                  };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.UpdateFranchiseProfile, para);
                return ds;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet UpdateKYC()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@PanImage",PanImage == "" ? null : PanImage),
                                      new SqlParameter("@Fk_MemId",CustomerId),
                                      new SqlParameter("@AddressProofImage",AddressProofImage == "" ? null : AddressProofImage),
                                      new SqlParameter("@AddressProofBackImage",AddressProofBackImage == "" ? null : AddressProofBackImage),
                                      new SqlParameter("@BankDocImage",BankDocImage == "" ? null : BankDocImage),
                                      new SqlParameter("@ProfilePic",ProfileImageUrl == "" ? null : ProfileImageUrl),
                                  };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.UpdateKYC, para);
                return ds;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet FranchiseUpdateKYC()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@Pk_FranchiseId",Pk_FranchiseId),
                                      new SqlParameter("@PanImage",PanImage==""?null:PanImage),
                                      new SqlParameter("@AddressProofImage",AddressProofImage==""?null:AddressProofImage),
                                      new SqlParameter("@AddressProofBackImage",AddressProofBackImage==""?null:AddressProofBackImage),
                                      new SqlParameter("@BankDocImage",BankDocImage == "" ? null : BankDocImage),
                                      new SqlParameter("@ProfilePic",ProfileImageUrl == "" ? null : ProfileImageUrl),
                                  };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.FranchiseUpdateKYC, para);
                return ds;

            }
            catch (Exception)
            {
                throw;
            }
        }


        


    }
    public class Profile : Menu
    {

        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? HolderName { get; set; }
        public string? PanCard { get; set; }
        public string? AccNo { get; set; }
        public string? BankName { get; set; }
        public string? Branch { get; set; }
        public string? PanImage { get; set; }
        public string? IFSC { get; set; }
        public string? JoiningDate { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Pincode { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? RankName { get; set; }
        public string? TotalOrders { get; set; }
        public string? SponsorId { get; set; }
        public string? SponsorName { get; set; }
        public string? PanStatus { get; set; }
        public string? AddressFrontStatus { get; set; }
        public string? AddressBackStatus { get; set; }
        public string? BankStatus { get; set; }
        public string? AddressProofFrontURL { get; set; }
        public string? AddressProofBackURL { get; set; }
        public string? BankDocURL { get; set; }
        public string? ProfilePicURL { get; set; }
        public string? Pk_FranchiseId { get; set; }
        public string? ContactPerson { get; set; }
        public string? CompanyName { get; set; }
        public string? GSTNo { get; set; }
        public string? FirmName { get; set; }
        public string? IsBid { get; set; }
        public string? PendingOrderCount { get; set; }

        public DataSet UpdateBID()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@LoginId",LoginId)

                                  };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.UpdateBID, para);
                return ds;

            }
            catch (Exception)
            {
                throw;
            }
        }


    }
    public class ProfileReposne : Menu
    {

        public Profile? Response { get; set; }
        //public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class KYCResponse
    {
        public int? Status { get; set; }
        public string? Message { get; set; }
    }

    public class ProfileListres:Menu
    {
        public List<Profile> profilelist { get; set; }
    }
    public class BIDReposne : Menu
    {

        public ProfileListres? Response { get; set; }
        //public int? Status { get; set; }
        public string? Message { get; set; }
    }
}
