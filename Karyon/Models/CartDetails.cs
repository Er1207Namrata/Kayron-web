using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class CartRequest : Menu
    {
        public int CustomerId { get; set; }
        public int VarientId { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }
        //public int OpCode { get; set; }
        public string? OrderType { get; set; }



        public DataSet MembersCart()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@OpCode", OpCode),
                                      new SqlParameter("@Fk_MemId",CustomerId),
                                      new SqlParameter("@Fk_VarientId",VarientId),
                                      new SqlParameter("@Quantity",Quantity),
                                      new SqlParameter("@CartId",CartId),
                                      new SqlParameter("@OrderType",OrderType)
                                  };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.MembersCart, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }

    }
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
    public class CartDetails
    {
        public string? ProductName { get; set; }
        public float? MRP { get; set; }
        public long? Quantity { get; set; }
        public float? FinalPrice { get; set; }
        public string? ProductDetails { get; set; }
        public string? Image { get; set; }
        public string? Unit { get; set; }
        public string? BrandName { get; set; }
        public string? CartStatus { get; set; }
        public long CartId { get; set; }
        public long VarientId { get; set; }
        public long ProductId { get; set; }
        public float TotalBv { get;  set; }
        public float BeforeTax { get;  set; }
        public decimal? KaryonWallet { get; set; }
        public decimal? FUPWallet { get; set; }
        public string? BorderCss { get; set; }
    }
    public class CartResponse
    {

        public CartData? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }

        public string? WalletType { get; set; }

        //public string? qty_amount { get; set; }
    }
    public class CartData
    {
        public float? ShippingCharge { get; set; }

        public float? TaxAmount { get; set; }
        public List<CartDetails>? CartList { get; set; }
    }
    public class CartSide
    {
        public int CustomerId { get; set; }
        public string? VarientId { get; set; }
        public int OpCode { get; set; }
        public string? ProductName { get; set; }
        public string? MRP { get; set; }
        public string? Quantity { get; set; }
        public string? FinalPrice { get; set; }
        public string? TotalBV { get; set; }
        public int? CartId { get; set; }
        public string? IMAGE { get; set; }
        public List<CartSide>? lstCartList { get; set; }
        public DataSet MembersCart()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@OpCode", OpCode),
                                      new SqlParameter("@Fk_MemId",CustomerId)


                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.MembersCart, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public static CartSide GetCartData(string CustomerId)
        {
            CartSide model = new CartSide();
            List<CartSide> CartList = new List<CartSide>();

            model.CustomerId = int.Parse(CustomerId);
            model.OpCode = 3;
            DataSet dsHeader = model.MembersCart();
            if (dsHeader != null && dsHeader.Tables.Count > 0)
            {
                if (dsHeader.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsHeader.Tables[0].Rows)
                    {
                        CartSide obj = new CartSide();

                        obj.ProductName = r["ProductName"].ToString();
                        obj.VarientId = r["Fk_VarientId"].ToString();
                        obj.MRP = r["MRP"].ToString();
                        obj.Quantity = r["Quantity"].ToString();
                        obj.TotalBV = r["TotalBV"].ToString();
                        obj.FinalPrice = r["FinalPrice"].ToString();
                        obj.CartId = int.Parse(r["CartId"].ToString());
                        obj.IMAGE = BaseUrl.ImageURL + "" + r["IMAGE"].ToString();
                        CartList.Add(obj);
                    }

                    model.lstCartList = CartList;


                }

            }
            return model;

        }



    }

    
}
