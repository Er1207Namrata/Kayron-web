using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
   
    public class CappingMaster : Menu
    {
        public decimal? CappingPoint { get; set; }
        public decimal? TargetPoint { get; set; }
        public string? UserId { get; set; }
        public string? Name { get; set; }
        public string? Date { get; set; }
        public string? Pk_Id { get; set; }
        public DataTable? dt { get; set; }
        //public int? OpCode { get; set; }     
        public DataSet InsertCappingMaster()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@CappingPoint",CappingPoint),
                                      new SqlParameter("@Pk_Id",Pk_Id),
                                      new SqlParameter("@TargetPoint",TargetPoint),
                                      new SqlParameter("@LoginId",UserId),
                                      new SqlParameter("@Date",Date),
                                      new SqlParameter("@OpCode",OpCode),

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.InsertSetCapping, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }

    }

}
