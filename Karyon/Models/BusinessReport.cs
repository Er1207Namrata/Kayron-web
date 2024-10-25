using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class BusinessReport : Menu
    {
        public long? CustomerId { get; set; }
        public string? EntryDate { get; set; }

        public DataSet GetBusinessReport()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@Fk_MemId",CustomerId),
                                      new SqlParameter("@Page",Page),
                                      new SqlParameter("@Size",Size),
                                      new SqlParameter("@ExportToExcel",ExportToExcel)

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetBusinessReport, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetBusiness()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@Fk_MemId",CustomerId),
                                      new SqlParameter("@Page",Page),
                                      new SqlParameter("@Size",Size),
                                      new SqlParameter("@ExportToExcel",ExportToExcel)

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetBusiness, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetBusinessReportDetails()
        {

            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@Fk_MemId",CustomerId),
                                      new SqlParameter("@EntryDate",EntryDate),

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetBusinessReportDetails, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class Business
    {
        public List<BusinessData>? BusinessList { get; set; }

    }
    public class BusinessData
    {
        public decimal PaidZoneA { get; set; }
        public decimal Harmony { get; set; }
        public decimal HarmonyPoints { get; set; }
        public decimal AllHormony { get; set; }
        public decimal PrevLeft { get; set; }
        public decimal PrevRight { get; set; }
        public decimal CurrLeft { get; set; }
        public decimal CurrRight { get; set; }
        public decimal BalLeft { get; set; }
        public decimal BalRight { get; set; }
        public decimal PaidZoneB { get; set; }
        public decimal ZoneABusiness { get; set; }
        public decimal ZoneBBusiness { get; set; }
        public string? EntryDate { get; set; }
        public string? Name { get; set; }
        public string? ClosingDate { get; set; }
        public string? PayoutNo { get; set; }
        public string? LoginId { get; set; }
        public string? TotalRecords { get; set; }
    }
    public class BusinessReposne
    {

        public Business? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
}
