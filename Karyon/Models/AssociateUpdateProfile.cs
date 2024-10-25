using System.Data;
using System.Data.SqlClient;
namespace Karyon.Models
{
    public class AssociateUpdateProfile : Menu
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Pincode { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public int? CustomerId { get; set; }
        public DataSet UpdateAssociateProfile()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@FK_MemId", CustomerId),
                                      new SqlParameter("@FirstName", FirstName),
                                      new SqlParameter("@LastName", LastName),
                                      new SqlParameter("@Email",Email),
                                      new SqlParameter("@Address",Address),
                                      new SqlParameter("@Pincode",Pincode),
                                      new SqlParameter("@State",State),
                                      new SqlParameter("@City",City),
                                      new SqlParameter("@Address",Address),
                                  };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.UpdateAssociateProfile, para);
                return ds;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
