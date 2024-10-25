using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class ChangeSponsor : Menu
    {
        //public string? LoginId { get; set; }
        public string? SponsorLoginId { get; set; }
        public string? TempPermanent { get; set; }
        public string? BIDUID { get; set; }
        public string? Leg { get; set; }
        //public int? AddedBy { get; set; }
        //public DataTable? dtDetails { get; set; }

        public DataSet ChangeSponsorLeg()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@LoginId",LoginId),
                                      new SqlParameter("@SponsorLoginId",SponsorLoginId),
                                      new SqlParameter("@Leg",Leg),
                                      new SqlParameter("@AddedBy",AddedBy),
                                      new SqlParameter("@BIDUID",BIDUID),
                                    

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.ChangeSponsorLeg, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet ChangeSponsorList()
        {
            try
            {
                

                DataSet ds = Connection.ExecuteQuery(ProcedureName.ChangeSponsorList);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet ActivateMember()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@LoginId",LoginId),
                                      new SqlParameter("@AddedBy",AddedBy),
                                      new SqlParameter("@TempPermanent",TempPermanent),
                                      new SqlParameter("@BIDUID",BIDUID)


                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.ActivateMember, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
