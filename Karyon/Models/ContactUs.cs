using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class ContactUs: Menu
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
        public string? ProfilePic { get; set; }
        [DisplayName("Your Review")]
        public int Star { get; set; }
        public IFormFile? ProfileImage { get; set; }

        public DataSet SaveInfo()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Name",Name),
                new SqlParameter("@Email",Email),
                new SqlParameter("@Message",Message)
            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.SaveEnquiryInfo ,para );
            return ds;
        }
        public DataSet SaveFeedback()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Name",Name),
                new SqlParameter("@Email",Email),
                new SqlParameter("@Message",Message),
                new SqlParameter("@ProfilePic",ProfilePic),
                new SqlParameter("@Star",Star)
            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.SaveFeedback, para);
            return ds;
        }
        public DataSet GetFeedback()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@OpCode",OpCode),
                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetFeedback, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }


    }
    public class ContactUsResponse
    {
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
}
