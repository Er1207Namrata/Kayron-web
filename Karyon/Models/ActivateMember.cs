using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class ActivationMember : Menu
    {
        //public int? FK_MemId { get; set; }       
        //public string? LoginId { get; set; }
        public int? Page { get; set; }
        public int? Size { get; set; }
        public DataSet GetActivationMember()
        {
            try
            {
                SqlParameter[] para = {

                                      //new SqlParameter("@FK_MemId",FK_MemId),                                   
                                      new SqlParameter("@LoginId",LoginId),                                    
                                      new SqlParameter("@Page",Page),
                                      new SqlParameter("@Size",Size)

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.ActiveMemberDetails, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetUpdationLog()
        {
            try
            {
                SqlParameter[] para = {

                                      //new SqlParameter("@FK_MemId",FK_MemId),                                   
                                      new SqlParameter("@LoginId",LoginId),                                    
                                      new SqlParameter("@Page",Page),
                                      new SqlParameter("@Size",Size)

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.RankUpdateDetails, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
 
}
