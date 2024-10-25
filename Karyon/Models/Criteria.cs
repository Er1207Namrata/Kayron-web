using System.Data;
using System.Data.SqlClient;
namespace Karyon.Models
{
    public class Criteria : Menu
    {

        public List<CriteriaDetails>? ZoneA { get; set; }
        public List<CriteriaDetails>? ZoneB { get; set; }

        public DataSet getCriteriaDetails()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_MemId",Fk_MemId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.getCriteria, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet CriteriaDashboard()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_MemId",Fk_MemId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetCriteriaDetails, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetCriteriaDetails()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_MemId",Fk_MemId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetCriteriaDetails, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class CriteriaDetails
    {
        public string? LoginId { get; set; }
        public string? Name { get; set; }
        public string? PV { get; set; }
    }

    public class CriteriaRequest
    {
        public string? Fk_MemId { get; set; }
    }

    public class CriteriaResponse
    {
        public CriteriaResponseList? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }

    public class CriteriaResponseList
    {
        public CriteriaResponseDataList ResponseList { get; set; }
        public CriteriaResponseDataList1 ResponseList1 { get; set; }
    }

    public class CriteriaResponseDataList
    {
        public List<CriteriaResponseData>? CriteriaResponseData { get; set; }
    }

    public class CriteriaResponseDataList1
    {
        public List<CriteriaResponseData1>? CriteriaResponseData1 { get; set; }
    }

    public class CriteriaResponseData
    {
        public string? Biztype { get; set; }
        public string? AzoneTarget { get; set; }
        public string? BzoneTarget { get; set; }
        public string? azoneachieved { get; set; }
        public string? bzoneachieved { get; set; }
        public string? azonebal { get; set; }
        public string? bzonebal { get; set; }
        public string? Status { get; set; }
    }

    public class CriteriaResponseData1
    {
        public string? Biztype { get; set; }
        public string? AzoneTarget { get; set; }
        public string? BzoneTarget { get; set; }
        public string? azoneachieved { get; set; }
        public string? bzoneachieved { get; set; }
        public string? azonebal { get; set; }
        public string? bzonebal { get; set; }
        public string? Status { get; set; }
    }

    public class CriteriaDetailsResponse:Menu
    {
        
        public string? Message { get; set; }
        public CriteriaDetailsResponseList? Response { get; set; }
    }

    public class CriteriaDetailsResponseList:Menu
    {
        public List<CriteriaDetailsDataList>? CriteriaList { get; set; }
        public List<CriteriaDetailsDataList1>? CriteriaList1 { get; set; }
    }

    public class CriteriaDetailsDataList:Menu
    {
        public List<CriteriaDetailsData>? CriteriaDetailsList { get; set; }
    }

    public class CriteriaDetailsDataList1:Menu
    {
        public List<CriteriaDetailsData1>? CriteriaDetailsList1 { get; set; }
    }

    public class CriteriaDetailsData
    {
        public string? LoginId { get; set; }
        public string? Name { get; set; }
        public string? PV { get; set; }
    }

    public class CriteriaDetailsData1
    {
        public string? LoginId { get; set; }
        public string? Name { get; set; }
        public string? PV { get; set; }
    }

    public class Criterias : Menu
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? FromTarget { get; set; }
        public string? UnderLegCount { get; set; }
        public string? UnderLegId { get; set; }

        public string? Achived { get; set; }
        public string? Balance { get; set; }
        public string? SelfBusiness { get; set; }
        public string? UIDBusiness { get; set; }
        public string? Total { get; set; }
        public string? ToTarget { get; set; }


        public DataSet getCriteriaDetails()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_MemId",Fk_MemId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.getCriteria, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class CriteriaDataResp
    {
        
        public string? Message { get; set; }
        public string? Status { get; set; }
        public CriteriasList? responselist { get; set; }

    }
    public class CriteriasList
    {
        public List<Criterias>? Criteriaslist { get; set; }

        public List<Criterias>? Criteriaslist1 { get; set; }
    }
    

}
