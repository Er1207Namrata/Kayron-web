using DocumentFormat.OpenXml.Wordprocessing;
using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class BinaryTree : Menu
    {
        public List<BinaryTreeData>? TreeList { get; set; }
    }
    public class BinaryTreeRequest
    {
        public string? Fk_UserId { get; set; }
        public string? LoginId { get; set; }
        public DataSet Tree()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_UserId",Fk_UserId),
                new SqlParameter("@LoginId",LoginId),
            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.BinaryTreeForMobile, para);
            return ds;
        } 
    }
    public class BinaryTreeData
    {
            public string? Fk_MemId  { get; set; }  
            public string? Fk_SponorId { get; set; }  
            public string? Fk_ParentId  { get; set; }  
            public string? LoginId  { get; set; }  
            public string? Name    { get; set; }  
            public string? Leg  { get; set; }  
            public string? Level   { get; set; }  
            public string? Status { get; set; }  
            public string? PermanentDate  { get; set; }  
            public string? JoiningDate  { get; set; }  
            public string? SponsorLoginId { get; set; }  
            public string? SponsorName { get; set; }  
            public string? AllLeg1 { get; set; }  
            public string? AllLeg2  { get; set; }  
            public string? PermanentLeg1 { get; set; }  
            public string? PermanentLeg2  { get; set; }  
            public string? InactiveLeft { get; set; }  
            public string?  InactiveRight { get; set; }
            public string? ImageURL { get; set; }
            public decimal? PcountLeg1 { get; set; }
            public decimal? PCountLeg2 { get; set; }
            public decimal? PaidLeg1 { get; set; }
            public decimal?  PaidLeg2 { get; set; }
    }
    public class BinaryTreeResponse
    {
        public BinaryTree? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }

    }
}
