using Karyon.Controllers;
using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class DirectRequest : Menu
    {
        public int? CustomerId { get; set; }
        public string PrevoiusLoginId { get; set; }
        //public string? LoginId { get; set; }
        public string? PlaceUnderId { get; set; }
        public string? Zone { get; set; }
        public List<List<string>> MyArray { get; set; } = new List<List<string>>();
        public List<HomeController.SeriesList>? SeriesList { get; set; }

        public void ConvertDataTableToList(DataTable table)
        {
            // Loop through each row in the DataTable
            foreach (DataRow row in table.Rows)
            {
                List<string> rowList = new List<string>();

                // Loop through each column in the DataRow
                foreach (var item in row.ItemArray)
                {
                    rowList.Add(item.ToString()); // Add the item to the list
                }

                // Add the rowList to MyArray
                MyArray.Add(rowList);
            }
        }
        public DataSet GetDirectList()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@FK_MemId",CustomerId),
                                      new SqlParameter("@PlaceUnderId",PlaceUnderId),
                                      new SqlParameter("@LoginId",LoginId),
                                      new SqlParameter("@Leg",Zone),
                                      new SqlParameter("@Page",Page),
                                      new SqlParameter("@Size",Size)

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetDirectList, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetDirectTeam()
        {
            try
            {
                SqlParameter[] para = {


                                      new SqlParameter("@LoginId",LoginId),
                                      new SqlParameter("@OpCode",OpCode),
                                      new SqlParameter("@Zone",Zone)
                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetDirectTeam, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetLevelTree()
        {
            try
            {
                SqlParameter[] para = {


                                      new SqlParameter("@FK_MemId",Fk_MemId),
                                     
                                  };

                DataSet ds = Connection.ExecuteQuery("GetLevelTree", para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetDirectListTree()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@FK_MemId",CustomerId),
                                      new SqlParameter("@LoginId",LoginId),
                                     

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetDirectListTree, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class Direct
    {
       public string PrevoiusLoginId { get; set; }
       public string PrevoiusFK_MemId { get; set; }
        public List<Data>? DirectList { get; set; }
        public List<Data>? SelfData { get; set; }
        public List<Data>? SecoundLevelData { get; set; }
    }
    public class Data
    {
        public string? LoginId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PlaceUnderName { get; set; }
        public string? PlaceUnderLoginId { get; set; }
        public string? Status { get; set; }
        public string? JoiningDate { get; set; }
        public string? PermanentDate { get; set; }
        public string? FK_MemId { get; set; }
        public string? Leg { get; set; }
        public int? TotalRecords { get; set; }
        public string? SponsorName { get;  set; }
        public string? SponsorLoginId { get;  set; }
        public string? ImageURL { get;  set; }
        public string? PerformanceLevel { get;  set; }
        public string? TotalPv { get;  set; }
        public string? SelfPV { get;  set; }
        public string? UIDPV { get;  set; }
        public string? TeamPV { get;  set; }
        public string? CurrentSelfPV { get;  set; }
        public string? CurrentTeamPV { get;  set; }
        public string? CurrentTotalPV { get;  set; }
        public string? CurrentUIDPV { get;  set; }
    }
    public class DirectResponse
    {
        public Direct? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
        public string? PreviousId { get; set; }
    }
}
