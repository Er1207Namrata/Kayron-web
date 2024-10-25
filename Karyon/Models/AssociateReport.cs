using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class AssociateReport : Menu
    {
        public int? CustomerId { get; set; }
        public string? PayoutNo { get; set; }
        public string? Type { get; set; }
        public string? Name { get; set; }
        public string? PreviousId { get; set; }
        public string? OrderNo { get; set; }
        public string? OrderDate { get;  set; }
        public string? OrderAmount { get;  set; }
        public string? TotalPV { get;  set; }
        public List<AssociateReport>? lstData { get; set; }
        public string? SelfBusiness { get; set; }
        public int SessionId { get;  set; }

        public DataSet GetAssociatePayoutReport()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@FK_MemId",CustomerId),
                                      new SqlParameter("@PayoutNo",PayoutNo),
                                      new SqlParameter("@Page",Page),
                                      new SqlParameter("@Size",Size),
                                      new SqlParameter("@ExportToExcel",ExportToExcel)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetAssociatePayoutReport, para);
                return ds;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetWeeklyAssociatePayoutReport()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@LoginId",LoginId),
                                      new SqlParameter("@FK_MemId",CustomerId),
                                      new SqlParameter("@OpCode",OpCode),
                                      new SqlParameter("@Page",Page),
                                      new SqlParameter("@Size",Size),
                                      new SqlParameter("@PayoutNo",PayoutNo),
                                      new SqlParameter("@ExportToExcel",ExportToExcel)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetWeeklyPayoutReport, para);
                return ds;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetAllIncomeReport()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@FK_MemId",CustomerId),
                                      new SqlParameter("@PayoutNo",PayoutNo),
                                      new SqlParameter("@Type",Type),
                                      new SqlParameter("@Page",Page),
                                      new SqlParameter("@Size",SessionManager.Size),
                                      new SqlParameter("@ExportToExcel",ExportToExcel)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetAllIncomePayoutReport, para);
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
                                      new SqlParameter("@Fk_Session_MemId",SessionId),
                                      new SqlParameter("@Type",Type),
                };
                DataSet ds = Connection.ExecuteQuery("GetBusiness", para);
                return ds;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetRepAssociatePayout()
        {
            try
            {
                SqlParameter[] para = {


                                      new SqlParameter("@LoginId",LoginId),
                                      new SqlParameter("@ToDate",ToDate==""?null:ToDate),
                                      new SqlParameter("@FromDate",FromDate==""?null:FromDate),
                                      new SqlParameter("@Page",Page),
                                      new SqlParameter("@Size",Size),
                                      new SqlParameter("@OpCode",OpCode),
                                      new SqlParameter("@FK_MemId",Fk_MemId==""?null:Fk_MemId),
                                      new SqlParameter("@PayoutNo",PayoutNo==""?null:PayoutNo),
                                      new SqlParameter("@ExportToExcel",ExportToExcel),

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.Rep_AssociatePayoutReport, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class Associate
    {
        public List<AssociateData>? AssociateList { get; set; }
    }
    public class AllIncomeReport
    {
        public List<AllIncomeReportData>? IncomeReport { get; set; }
    }
    public class AssociateData
    {
        public string? ClosingDate { get; set; }
        public string? PayoutNo { get; set; }
        public string? LoginId { get; set; }
        public string? Name { get; set; }
        public string? PayStatus { get; set; }
        public string? OrderNo { get; set; }
        public string? Creator { get; set; }
        public string? BusinessAmount { get; set; }
        public string? BusinessDate { get; set; }
        public string? Harmony { get; set; }
        public string? Advisor1 { get; set; }
        public string? Advisor2 { get; set; }
        public string? Advisor3 { get; set; }
        public string? Royal { get; set; }
        public string? CreatorHarmony { get; set; }
        public string? Smart { get; set; }
        public string? Leadership { get; set; }
        public string? HarmonyPoints { get; set; }
        public string? GrossAmount { get; set; }
        public string? TDSAmount { get; set; }
        public string? ProcessingFee { get; set; }
        public string? NetAmount { get; set; }
        public string? IsClosed { get; set; }
        public string? PrevLeft { get; set; }
        public string? PrevRight { get; set; }
        public string? CurrLeft { get; set; }
        public string? CurrRight { get; set; }
        public string? BalLeft { get; set; }
        public string? BalRight { get; set; }
        public string? DirectIncome { get; set; }
        
        public string? TeamIncome { get; set; }
        public string? SuperStartSale { get; set; }
        public string? SuperStartClub { get; set; }
        public string? PremiumClub { get; set; }
        public string? SuperPremiumClub { get; set; }
       
        public int? TotalRecords { get; set; }
    }
    public class AllIncomeReportData
    {
        public string? ClosingDate { get; set; }
        public string? PayoutNo { get; set; }
        public string? Income { get; set; }
    }
    public class AssociateReportResponse
    {
        public Associate? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
        public int? Pk_PayoutId { get; set; }
        public string? Type { get; set; }
    }
    public class AllIncomeReportResponse
    {
        public AllIncomeReport? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }

    }
    public class AssociateReportResp 
    {

        public string? Message { get; set; }
        public string? PreviousId { get; set; }
        public string? SelfBusiness { get; set; }
        public string? Status { get; set; }
        public AssociateReportlist? Response { get; set; }

    }
     public class AssociateReportlist
    {
        public List<AssociateReport>? associatelist { get; set; }
    }
}
