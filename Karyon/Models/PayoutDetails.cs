using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class PayoutDetail:Menu
    {
        
        public string PayoutNo { get; set; }
        public string ClosingDate { get; set; }
        public string PK_PayoutMaster { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public decimal BusinessAmount { get; set; }
        public decimal CommissionPercentage { get; set; }
        public string PlanId { get; set; }
        public string IncomeType { get; set; }
        public string DirectIncome { get; set; }
        public string TeamIncome { get; set; }
        public string Smart { get; set; }
        public string Leadership { get; set; }
        public string SuperStartSale { get; set; }
        public string SuperStartClub { get; set; }
        public string PremiumClub { get; set; }
        public string SuperPremiumClub { get; set; }
        public string SeminarBonus { get; set; }
        public string CarClubBonus { get; set; }
        public string TravelClubBonus { get; set; }
        public string ProvidentClubBonus { get; set; }
        public string HouseClubBonus { get; set; }
        public string BlueDiamondClubBonus { get; set; }
        public string CrownAmbassadorClubBonus { get; set; }
        public string PresidentClubBonus { get; set; }
        public string GrossAmount { get; set; }
        public string TDSAmount { get; set; }
        public string ProcessingFee { get; set; }
        public string NetAmount { get; set; }
        public string TotalRecords { get; set; }
        public string PerformanceLevel { get; set; }
        public string LevelPercentage { get; set; }
        public string SelfPV { get; set; }
        public string TeamPV { get; set; }
        public string TotalOfferpoint { get; set; }
        public string? CrPoint { get; set; }
        public string? DrPoint { get; set; }
        public string? Narration { get; set; }
        public string? Transactiondate { get; set; }
        public decimal OfferPoint { get; set; }
        public string AchiveDate { get; set; }
        public string OrderNo { get; set; }
        public decimal? Balance { get; set; }

        public DataSet GetPayoutMaster()
        {
            try
            {
                SqlParameter[] para = {

                                    new SqlParameter("@Fk_MemId",Fk_MemId)  
                };
                DataSet ds = Connection.ExecuteQuery("PayoutMaster", para);
                return ds;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetPayoutClosingDetails()
        {
            try
            {
                SqlParameter[] para = {

                    new SqlParameter("@PayoutNo",PayoutNo),
                    new SqlParameter("@FK_MemId",Fk_MemId),
                    new SqlParameter("@LoginId",LoginId),
                    new SqlParameter("@Page",1),
                    new SqlParameter("@Size",30)


                                      
                };
                DataSet ds = Connection.ExecuteQuery("Rep_AssociatePayoutReport", para);
                return ds;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetPayoutSummaryDetails()
        {
            try
            {
                SqlParameter[] para = {

                    new SqlParameter("@Pk_PlanId",PlanId),
                    new SqlParameter("@LoginId",LoginId),
                    new SqlParameter("@payoutNo",PayoutNo)


                                      
                };
                DataSet ds = Connection.ExecuteQuery("rep_GetPayoutDetails", para);
                return ds;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetSmartPointDetails()
        {
            try
            {
                SqlParameter[] para = {

                    new SqlParameter("@Fk_memId",Fk_MemId),
                    new SqlParameter("@PayoutNo",PayoutNo),


                                      
                };
                DataSet ds = Connection.ExecuteQuery("getSmartPointDetails", para);
                return ds;

            }
            catch (Exception)
            {
                throw;
            }
        }
        

        public DataSet GetofferPointLedger()
        {
            try
            {
                SqlParameter[] para = {


                                      new SqlParameter("@LoginId",LoginId==""?null:LoginId),
                                      new SqlParameter("@ToDate",ToDate==""?null:ToDate),
                                      new SqlParameter("@FromDate",FromDate==""?null:FromDate),
                                      new SqlParameter("@Page",Page),
                                      new SqlParameter("@Size",Size),
                                      new SqlParameter("@ExportToExcel",ExportToExcel),

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetOfferPointLedger, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
         public DataSet GetofferPointDetails()
        {
            try
            {
                SqlParameter[] para = {


                                      new SqlParameter("@FK_MemId",Fk_MemId)

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.Rep_OfferPointsDetails, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }


    }
    public class PayoutDetailLit
    {
        public List<PayoutDetail>? list { get; set; }
    }
    public class PayoutDetailResponse 
    {
        public PayoutDetailLit? Response { get; set; }
        public string? message { get; set; }
        public string? Status { get; set; }
    }
}
