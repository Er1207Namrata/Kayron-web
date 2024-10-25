using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
namespace Karyon.Models
{
    public class SetId : Menu
    {

        public string? MainId { get; set; }
        public string? Leg { get; set; }
        public string? UpperId { get; set; }
        public string? DownId { get; set; }
        public string? Name { get; set; }
        public string? MobileNo { get; set; }
        public DataTable? dt { get; set; }

        public DataSet RegistrationByAdmin()
        {
            try
            {
                SqlParameter[] para = {

                                  new SqlParameter("@ParentLoginId",MainId),
                                  new SqlParameter("@DownId",DownId),
                                  new SqlParameter("@Leg",Leg),
                                  new SqlParameter("@Name",Name),
                                  new SqlParameter("@MobileNo",MobileNo),
                                  new SqlParameter("@RegistrationFrom","Admin")


                              };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.RegistrationByAdmin, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }



        public DataSet InsertSetId()
        {
            try
            {
                SqlParameter[] para = {

                                  new SqlParameter("@MainLoginId",MainId),
                                  new SqlParameter("@UpLoginId",UpperId),
                                  new SqlParameter("@DownLoginId",DownId),
                                  

                              };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.InsertSetId, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    
    
}
