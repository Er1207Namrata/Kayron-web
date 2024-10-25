using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Karyon.Models
{
    public class Tree: Menu
    {
        public string? AllBusinessLeft { get; set; }
        public string? AllBusinessRight { get; set; }
        public string? Fk_UserId { get; set; }
        public string? SessionPkId { get; set; }
        public string? SessionId { get; set; }
        public string? ParentId { get; set; }
        public string? prefixText { get; set; }

        public string? Leg { get; set; }
        //public string? Status { get; set; }
        public string? MemberName { get; set; }
        public string? ProductRightBusiness { get; set; }
        public string? ProductLeftBusiness { get; set; }
        //public string? LoginId { get; set; }
        public string? ParentLoginId { get; set; }
        public string? BlockStatus { get; set; }
        public string? CssClass { get; set; }
        public string? Href { get; set; }
        public string? SponsorId { get; set; }
        public string? SelfBusiness { get; set; }
        public string? TeamBusiness { get; set; }
        public string? JoiningDate { get; set; }
        public string? ActivationDate { get; set; }
        public string? ProductName { get; set; }
        public string? MainId { get; set; }
        public string? AllLeg1 { get; set; }
        public string? AllLeg2 { get; set; }
        public string? PermanentLeg1 { get; set; }
        public string? PermanentLeg2 { get; set; }
        public string? InactiveLeft { get; set; }
        public string? InactiveRight { get; set; }
        public string? Spillby { get; set; }
        public string? PCountLeg1 { get; set; }
        public string? PCountLeg2 { get; set; }
        public string? BalanceLeft { get; set; }
        public string? BalanceRight { get; set; }
        public string? PaidLeg1 { get; set; }
        public string? PaidLeg2 { get; set; }
        public string? ProfilePic { get; set; }
        public string? Gender { get; set; }
        public string? SelfBusines { get; set; }
        public DataTable? FirstUserDate { get; set; }
        public DataTable? TreeData { get; set; }
        //public DataTable dtDetails { get; set; }

        public DataTable GetTreeMembers()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@headID",Fk_UserId),
                                    new SqlParameter("@SessionPkId",SessionId)
                                };
            Connection db = new Connection();
            DataSet ds = Connection.ExecuteQuery(ProcedureName.GetTreeMembers, para);
            return ds.Tables[0];


        }
        public DataSet BinaryTreeForWeb()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Fk_UserId",Fk_UserId),
                                    new SqlParameter("@LoginId",LoginId),
                                    new SqlParameter("@AddedBy",AddedBy)
                                };
            Connection db = new Connection();
            DataSet ds = Connection.ExecuteQuery(ProcedureName.BinaryTreeForWeb, para);
            return ds;


        }
        public DataTable GetUserFirst()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Fk_UserId",Fk_UserId)
                                };
            Connection db = new Connection();
            DataSet ds = Connection.ExecuteQuery(ProcedureName.GetUserFirst, para);
            return ds.Tables[0];


        }
        public DataTable GetSearchData()
        {
            SqlParameter[] para ={
                                    
                                    new SqlParameter("@LoginId",LoginId)
                                };
            Connection db = new Connection();
            DataSet ds = Connection.ExecuteQuery(ProcedureName.GetSearchTreeData, para);
            return ds.Tables[0];


        }
        public DataTable CheckParentForTreeView(int Fk_MemID, int Session_Fk_MemID)
        {

            SqlParameter[] para ={

                                    new SqlParameter("@Fk_MemID",Fk_MemID),
                                    new SqlParameter("@Session_Fk_MemID",Session_Fk_MemID)
                                };
            Connection db = new Connection();
            DataSet ds = Connection.ExecuteQuery(ProcedureName.CheckParentForTreeView, para);
            return ds.Tables[0];

        }


        public DataTable GetTreeMembersAdmin()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@headID",Fk_UserId),
                                    new SqlParameter("@SessionPkId",SessionId)
                                };
            Connection db = new Connection();
            DataSet ds = Connection.ExecuteQuery(ProcedureName.GetTreeMembersAdmin, para);
            return ds.Tables[0];


        }
        public DataTable GetUserFirstAdmin()
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Fk_UserId",Fk_UserId)
                                };
            Connection db = new Connection();
            DataSet ds = Connection.ExecuteQuery(ProcedureName.GetUserFirstAdmin, para);
            return ds.Tables[0];


        }
        public DataTable GetSearchDataAdmin()
        {
            SqlParameter[] para ={

                                    new SqlParameter("@LoginId",LoginId)
                                };
            Connection db = new Connection();
            DataSet ds = Connection.ExecuteQuery(ProcedureName.GetSearchTreeDataAdmin, para);
            return ds.Tables[0];


        }
        public DataTable CheckParentForTreeViewAdmin(int Fk_MemID, int Session_Fk_MemID)
        {

            SqlParameter[] para ={

                                    new SqlParameter("@Fk_MemID",Fk_MemID),
                                    new SqlParameter("@Session_Fk_MemID",Session_Fk_MemID)
                                };
            Connection db = new Connection();
            DataSet ds = Connection.ExecuteQuery(ProcedureName.CheckParentForTreeViewAdmin, para);
            return ds.Tables[0];

        }
    }
}