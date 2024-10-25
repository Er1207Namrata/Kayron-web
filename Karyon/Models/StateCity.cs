using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class StateCityRequest : Menu
    {
        public string? Pincode { get; set; }
        public DataSet GetStateCity()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@Value", Pincode),
                                      new SqlParameter("@OpCode","1")

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
    public class StateCity
    {
        public string? State { get; set; }
        public string? City { get; set; }
        
    }
    public class StateCityResponse
    {
        public string? Message { get; set; }
        public int Status { get; set; }
        public StateCity? Response { get; set; }
    }
}
