using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class DashBoardRequest:Menu
    {
        public int CustomerId { get; set; }
        public int? TotalMemberR { get; set; }
        public int? TotalActiveMemberR { get; set; }
        public int? TotalInactiveMemberR { get; set; }
        public int? TotalMember { get; set; }
        public int? TotalActiveMember { get; set; }
        public int? TotalInactiveMember { get; set; }
        public decimal? LeftBVFresh { get; set; }
        public decimal? RightBVFresh { get; set; }
        public string? FestivalPopup { get; set; }
        public string? FranchiseTypeId { get; set; }
        public decimal? SponsorLeftBVFresh { get; set; }
        public decimal? SponsorRightBVFresh { get; set; }
        public string? Address { get; set; }
        public string? State { get; set; }
        public string? Pincode { get; set; }
        public string? City { get; set; }

        public DataSet GetShoppingDashBoard()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@Fk_MemId",CustomerId),

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetShoppingDashBoard, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetAssociateDashBoard()
        {
            try
            {
                SqlParameter[] para = {

                    new SqlParameter("@Fk_MemId",CustomerId),

                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetAssociateDashBoard, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetFranchiseeDashBoard()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@FK_FranchiseId",CustomerId),
                                      new SqlParameter("@FranchiseTypeId",FranchiseTypeId),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetFranchiseeDashBoard, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet Eway()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@FranchaiseId",CustomerId),
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.Eway, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class DashBoard
    {
        public string? IsBusinessId { get; set; } 
        public string? IskycApproved { get; set; } 
        public string? FestivalPopup { get; set; } 
        public List<Feature>? FeaturedList { get; set; }
        public List<CategoriesRequest>? CategoryList { get; set; }
        public List<Banner>? BannerList { get; set; }
        public List<OfferProduct>? OfferProducts { get; set; }

    }
    public class OfferProduct
    {
        public long? VarientId { get; set; }
        public string? Description { get; set; }
        public decimal? MRP { get; set; }
        public decimal? OfferPrice { get; set; }
        public DateTime? ToDate { get; set; }
        public string? ProductImage { get; set; }
        public string? ProductName { get; set; }
        
    }

    public class Feature
    {
        public string? FeatureName { get; set; }
        public List<Collection>? FeaturedProductList { get; set; }
    }
    public class Banner
    {
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public string? Message { get; set; }

    }
    public class Collection
    {
        public string? ProductName { get; set; }
        public string? PrimaryImage { get; set; }
        public string? UnitName { get; set; }
        public string? BoxQty { get; set; }
        public string? StarRating { get; set; }
        public float? MRP { get; set; }
        public float? SellPrice { get; set; }
        public float? PV { get; set; }
        public decimal? BoxPrice { get; set; }

        public long VarientId { get; set; }

        public List<VarientData>? VarientList { get; set; }
    }

    public class DashBoardResponse
    {
        public DashBoard? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class AssociateDashBoardResponse
    {
        public AssociateDashBoard? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class AssociateDashBoardResponseWeb : Menu
    {
        public AssociateDashBoard? Response { get; set; }
        //public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class AssociateDashBoard
    {
        public List<OfferMasterData>? lstOfferMaster { get; set; }
        public long? TotalMember { get; set; }
        public long? TotalActiveMember { get; set; }
        public long? TotalInactiveMember { get; set; }
        public decimal? TotalLeftBusiness { get; set; }
        public decimal? TotalRightBusiness { get; set; }
        public decimal? CaryForwardLeft { get; set; }
        public decimal? CaryForwardRight { get; set; }
        public decimal? PaidBusinessLeft { get; set; }
        public decimal? PaidBusinessRight { get; set; }
        public decimal? SelfIncome { get; set; }
        public decimal? BinaryIncome { get; set; }
        public decimal? TotalIncome { get; set; }
        public decimal? PayoutWallet { get; set; }
        public decimal? TodayIncome { get; set; }
        public decimal? TotalShopping { get; set; }
        public decimal? Advisor1 { get; set; }
        public decimal? Advisor2 { get; set; }
        public decimal? Advisor3 { get; set; }
        public decimal? Royal { get; set; }
        public string? RankImage { get; set; }
        public decimal? FamilynWallet { get; set; }
        public decimal? TotalLeftBusinessC { get; set; }
        public decimal? TotalRightBusinessC { get; set; }
        public decimal? CaryForwardLeftC { get; set; }
        public decimal? CaryForwardRightC { get; set; }
        public decimal? PaidBusinessLeftC { get; set; }
        public decimal? PaidBusinessRightC { get; set; }
        public decimal? CreatorHarmony { get; set; }
        public decimal? RightBVFresh { get; set; }
        public decimal? LeftBVFresh { get; set; }
        public decimal? CreatorHarmonyU { get; set; }
        public decimal? RoyalU { get; set; }
        public decimal? BinaryIncomeU { get; set; }
        public decimal? SelfIncomeU { get; set; }
        public List<ShoppingData>? lstShopping { get; set; }
        public string? Message { get; set; }
        public string? OfferImage { get; set; }
        public string? AchivedPoints { get; set; }
        public string? HarmonyPoints { get; set; }
        public string? OfferStatus { get; set; }
        public string? OfferName { get; set; }
        public int? TotalMemberR { get; set; }
        public int? TotalActiveMemberR { get; set; }
        public int? TotalInactiveMemberR { get; set; }
        public string? LeftBusiness { get; set; }
        public string? RightBusiness { get; set; }
        public bool? ShowEventMenu { get; set; }
    }
    public class ShoppingData
    {
        public string? OrderDate { get; set; }
        public string? OrderNo { get; set; }
        public decimal? OrderAmount { get; set; }
        public decimal? TotalPV { get; set; }
        public string? Status { get; set; }

    }
    public class FranchiseeDashBoardResponse
    {
        public FranchiseeDashBoard? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class FranchiseeDashBoard
    {
        public decimal? WalletBalance { get; set; }
        public decimal? ProductStockAmt { get; set; }
        public decimal? FranchisePayout { get; set; }
        public decimal? AssociateSaleAmt { get; set; }
    }
    public class OfferMasterData
    {
        public string? HarmonyCount { get; set; }
        public string? OfferImage { get; set; }
        public string? AchieveImage { get; set; }
        public string? OfferName { get; set; }
        public string? ToDate { get; set; }
        public string? FromDate { get; set; }
        public string? OfferStatus { get; set; }
        public string? AchivePoint { get; set; }
        public string? Pk_OfferId { get; set; }

    }
}
