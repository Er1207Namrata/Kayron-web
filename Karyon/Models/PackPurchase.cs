using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class PackPurchase
    {
        public DataSet GetPackPurchase()
        {
            try
            {
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetPackPurchase);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    public class PackPurchaseResponse
    {
        public PackPurchaseList? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }

    public class PackPurchaseList
    {
        public List<PackPurchaseData>? PackList { get; set; }
        public string? OpCode { get; set; }
    }

    public class PackPurchaseData
    {
        public string? Pk_PackId { get; set; }
        public string? PackName { get; set; }
        public string? Amount { get; set; }
        public string? Offer { get; set; }
        public string? Note { get; set; }
    }
}
