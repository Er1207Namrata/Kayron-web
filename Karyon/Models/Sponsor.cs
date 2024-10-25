using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class SponsorRequest : Menu
    {
        public string? SponsorLoginId { get; set; }
        public DataSet GetSponsorName()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@Value", SponsorLoginId),
                                      new SqlParameter("@OpCode","2")

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetMasterData, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetSponsorNameByNewId()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@Value", SponsorLoginId),
                                      new SqlParameter("@OpCode","20")

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetMasterData, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetFranchiseSponsorName()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@Value", SponsorLoginId),
                                      new SqlParameter("@OpCode","5")

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetMasterData, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class Sponsor
    {
        public string? Name { get; set; }
        public string? MobileNo { get; set; }
        public string? SponsorLoginId { get; set; }
        public string? SponsorName { get; set; }
        public string? PerformanceLevel { get; set; }
        public string? TempPermanent { get; set; }
        public string? IsBID { get; set; }
    }
    public class SponsorResponse
    {
        public Sponsor? Response { get; set; }
        public string? Message { get; set; }
        public int Status { get; set; }
    }
}
