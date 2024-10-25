using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class DownlineRequest: Menu
    {
        public int? CustomerId { get; set; }
        //public string? LoginId { get; set; }
        public string? PlaceUnderId { get; set; }
        public string? Zone { get; set; }
        public DataSet GetDownlineList()
        {
            try
            {
                SqlParameter[] para = {

                                       new SqlParameter("@FK_MemId",CustomerId),
                                      new SqlParameter("@PlaceUnderId",PlaceUnderId),
                                      new SqlParameter("@LoginId",LoginId),
                                      new SqlParameter("@Leg",Zone),
                                      new SqlParameter("@Page",Page),
                                      new SqlParameter("@Status",Status),
                                      new SqlParameter("@Size",Size)

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetDownlineList, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class Downline
    {
        public List<Data>? DownlineList { get; set; }
    }
    
    public class DownlineResponse
    {
        public Downline? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
}
