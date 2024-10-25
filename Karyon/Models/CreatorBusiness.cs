using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class CreatorBusiness : Menu
    {
        public int IsCHarmony { get; set; }
        public int CustomerId { get; set; }

        public DataSet GetCreatorBusiness()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@Fk_MemId",CustomerId),
                    new SqlParameter("@IsCHarmony",  IsCHarmony),
                    new SqlParameter("@Page",  Page),
                    new SqlParameter("@Size",  SessionManager.Size)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.CreatorBusiness, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class CreatorBusinessResponse
    {
        public CreatorBusinessList? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class CreatorBusinessList
    {

        public List<CreatorBusinessData>? CreatorBusinessLst { get; set; }



    }
    public class CreatorBusinessData
    {
        public string? LoginId { get; set; }
        public string? Name { get; set; }
        public string? Leg { get; set; }
        public string? OrderDate { get; set; }
        public decimal? OrderAmount { get; set; }
        public decimal? TotalPV { get; set; }
        public string? Status { get; set; }
        public bool? Iscalc { get; set; }
        public string? CalcDate { get; set; }
        public int? TotalRecords { get; set; }
    }
    public class CreatorHarmonyResponse
    {
        public CreatorHarmonyList? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class CreatorHarmonyList
    {
        public List<CreatorBusinessData>? CreatorHamonyL { get; set; }
        public List<CreatorBusinessData>? CreatorHamonyR { get; set; }

    }

}
