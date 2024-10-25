using ClosedXML.Excel;
using Karyon.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using static Karyon.Models.Common;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
//using paytm;
using Nancy.Json;
using QRCoder;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web.Http;
using DocumentFormat.OpenXml.Bibliography;
using Nancy.Responses;
using System.Text;
using DocumentFormat.OpenXml.Drawing.Charts;
using DataTable = System.Data.DataTable;
using System.Web.Http.Results;
using System.Reflection.Emit;
using Paytm;
using Microsoft.AspNetCore.Mvc.Filters;
using Nancy;
using Karyon.Models.EwayBilling;

namespace Karyon.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //CartSide Cart = null;
            //Cart = CartSide.GetCartData("0");
            //HttpContext.Session.SetObjectAsJson("_Cart", Cart);
            //HttpContext.Session.SetString("CustomerId", "0");
            //string LoginId = HttpContext.Request.Cookies["LoginId"];
            //string Password = HttpContext.Request.Cookies["Password"];
            //string UserType= HttpContext.Request.Cookies["UserType"];
            //if(UserType=="Associate")
            //{
            //    if (!string.IsNullOrEmpty(LoginId))
            //    {
            //        if (!string.IsNullOrEmpty(Password))
            //        {
            //            AutoLogin(LoginId, Password);
            //            return RedirectToAction("Shop", "Home");
            //        }
            //    }
            //}
            string CustomerId = HttpContext.Session.GetString("CustomerId") == null ? "0" : HttpContext.Session.GetString("CustomerId");
            HttpContext.Session.SetString("CustomerId", CustomerId == null ? "0" : CustomerId);
            string obj1 = "{ \"CustomerId\":\"" + CustomerId + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);

            string result = Common.HITAPI(APIURL.ShoppingDashBaord, Body);
            DashBoardResponse dashBoardResponse = new DashBoardResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            dashBoardResponse = JsonConvert.DeserializeObject<DashBoardResponse>(dcdata);
            CartSide Cart = null;
            Cart = CartSide.GetCartData(CustomerId);
            HttpContext.Session.SetObjectAsJson("_Cart", Cart);
            if (Cart.lstCartList == null)
            {
                HttpContext.Session.SetString("Count", "0");
            }
            else
            {
                HttpContext.Session.SetString("Count", Cart.lstCartList.Count.ToString());
            }
            return View(dashBoardResponse);

        }
        public ActionResult About()
        {
            CartSide Cart = null;
            Cart = CartSide.GetCartData(HttpContext.Session.GetString("CustomerId") == null ? "0" : HttpContext.Session.GetString("CustomerId"));
            HttpContext.Session.SetObjectAsJson("_Cart", Cart);
            if (Cart.lstCartList == null)
            {
                HttpContext.Session.SetString("Count", "0");
            }
            else
            {
                HttpContext.Session.SetString("Count", Cart.lstCartList.Count.ToString());
            }
            return View();
        }
        public ActionResult Festivalpopup(DashBoardRequest dashBoardRequest)
        {
            DataSet ds = dashBoardRequest.GetShoppingDashBoard();
            if (ds.Tables[5] != null)
            {
                if (ds.Tables[5].Rows.Count > 0)
                {
                    dashBoardRequest.CustomerId = int.Parse(HttpContext.Session.GetString("CustomerId"));
                    dashBoardRequest.FestivalPopup = ds.Tables[5].Rows[0]["Image"].ToString();
                }
            }
            return Json(dashBoardRequest);
        }
        public ActionResult Opportunity()
        {
            CartSide Cart = null;
            Cart = CartSide.GetCartData(HttpContext.Session.GetString("CustomerId") == null ? "0" : HttpContext.Session.GetString("CustomerId"));
            HttpContext.Session.SetObjectAsJson("_Cart", Cart);
            if (Cart.lstCartList == null)
            {
                HttpContext.Session.SetString("Count", "0");
            }
            else
            {
                HttpContext.Session.SetString("Count", Cart.lstCartList.Count.ToString());
            }
            return View();
        }
        public ActionResult Services()
        {
            CartSide Cart = null;
            Cart = CartSide.GetCartData(HttpContext.Session.GetString("CustomerId") == null ? "0" : HttpContext.Session.GetString("CustomerId"));
            HttpContext.Session.SetObjectAsJson("_Cart", Cart);
            if (Cart.lstCartList == null)
            {
                HttpContext.Session.SetString("Count", "0");
            }
            else
            {
                HttpContext.Session.SetString("Count", Cart.lstCartList.Count.ToString());
            }
            return View();
        }
        public ActionResult ContactUs(ContactUs contactUs)
        {
            string CustomerId = "0";
            CartSide Cart = null;
            Cart = CartSide.GetCartData(CustomerId);
            HttpContext.Session.SetObjectAsJson("_Cart", Cart);
            if (Cart.lstCartList == null)
            {
                HttpContext.Session.SetString("Count", "0");
            }
            else
            {
                HttpContext.Session.SetString("Count", Cart.lstCartList.Count.ToString());
            }
            if (!string.IsNullOrEmpty(contactUs.Email))
            {
                string sendmail = Common.SendMail(contactUs.Email, contactUs.Name);
                if (sendmail == "Mail Sent Successfully!")
                {
                    DataSet dataSet = contactUs.SaveInfo();
                }
            }
            return View(contactUs);
        }

        public ActionResult AutoLogin(string LoginId, string Password)
        {
            string Body = "";
            string obj1 = "{\"LoginId\":\"" + Crypto.Decrypt(LoginId) + "\",\"Password\":\"" + Crypto.Decrypt(Password) + "\",\"LoginFrom\":\"WEB\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.Login, Body);
            LoginResponse loginResponse = new LoginResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            loginResponse = JsonConvert.DeserializeObject<LoginResponse>(dcdata);
            if (loginResponse.Status == 0)
            {
                HttpContext.Session.SetString("CustomerId", "0");
                TempData["Login"] = loginResponse.Message;
            }
            else
            {
                HttpContext.Session.SetString("FirstName", loginResponse.Response.FirstName);
                HttpContext.Session.SetString("LastName", loginResponse.Response.LastName);
                HttpContext.Session.SetString("CustomerId", loginResponse.Response.CustomerId.ToString());
                HttpContext.Session.SetString("LoginId", Crypto.Decrypt(LoginId));
                Response.Cookies.Append("UserType", loginResponse.Response.UserType);
                Response.Cookies.Append("LoginId", LoginId);
                Response.Cookies.Append("Password", Password);

                CartSide Cart = null;
                Cart = CartSide.GetCartData(HttpContext.Session.GetString("CustomerId").ToString());
                if (Cart.lstCartList == null)
                {
                    HttpContext.Session.SetString("Count", "0");
                }
                else
                {
                    HttpContext.Session.SetString("Count", Cart.lstCartList.Count.ToString());
                }
                HttpContext.Session.SetObjectAsJson("_Cart", Cart);

            }
            return View();
        }
        public IActionResult Shop(string Id, string ProductId)
        {
            string CustomerId = HttpContext.Session.GetString("CustomerId") == null ? "0" : HttpContext.Session.GetString("CustomerId");
            string Body = "";
            if (!string.IsNullOrEmpty(Id))
            {
                string obj1 = "{ \"CategoryId\":\"" + Crypto.Decrypt(Id) + "\", \"ProductId\":\"" + 0 + "\",\"CustomerId\":\"" + CustomerId + "\"}";
                Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            }
            else if (!string.IsNullOrEmpty(ProductId))
            {
                string obj1 = "{ \"CategoryId\":\"" + 0 + "\", \"ProductId\":\"" + Crypto.Decrypt(ProductId) + "\",\"CustomerId\":\"" + CustomerId + "\"}";
                Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            }

            else
            {
                string obj1 = "{ \"CategoryId\":\"" + 0 + "\", \"ProductId\":\"" + 0 + "\",\"CustomerId\":\"" + CustomerId + "\"}";
                Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            }

            string result = Common.HITAPI(APIURL.GetAllProducts, Body);
            ProductReposne productReposne = new ProductReposne();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            productReposne = JsonConvert.DeserializeObject<ProductReposne>(dcdata);
            CartSide Cart = null;
            Cart = CartSide.GetCartData(HttpContext.Session.GetString("CustomerId") == null ? "0" : HttpContext.Session.GetString("CustomerId"));
            if (Cart.lstCartList == null)
            {
                HttpContext.Session.SetString("Count", "0");
            }
            else
            {
                HttpContext.Session.SetString("Count", Cart.lstCartList.Count.ToString());
            }
            HttpContext.Session.SetObjectAsJson("_Cart", Cart);
            // Console.WriteLine(dashBoardResponse.Response);
            return View(productReposne);

        }
        public ActionResult Products(string Id, string ProductId)
        {
            string Body = "";
            if (!string.IsNullOrEmpty(Id))
            {
                string obj1 = "{ \"CategoryId\":\"" + Crypto.Decrypt(Id) + "\", \"ProductId\":\"" + 0 + "\",\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\"}";
                Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            }
            else if (!string.IsNullOrEmpty(ProductId))
            {
                string obj1 = "{ \"CategoryId\":\"" + 0 + "\", \"ProductId\":\"" + Crypto.Decrypt(ProductId) + "\",\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\"}";
                Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            }

            else
            {
                string obj1 = "{ \"CategoryId\":\"" + 0 + "\", \"ProductId\":\"" + 0 + "\",\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\"}";
                Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            }

            string result = Common.HITAPI(APIURL.GetAllProducts, Body);
            ProductReposne productReposne = new ProductReposne();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            productReposne = JsonConvert.DeserializeObject<ProductReposne>(dcdata);

            // Console.WriteLine(dashBoardResponse.Response);
            return View(productReposne);
        }
        public ActionResult GetProductList()
        {
            string Body = "";

            Body = "pnyyA23t58aInr0wOEMFwFsKiKZt2ae9BIrhM447RZW+42xE2GVD3YAqyss5sZL6";
            string result = Common.HITAPI(APIURL.GetAllProducts, Body);
            ProductReposne productReposne = new ProductReposne();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            productReposne = JsonConvert.DeserializeObject<ProductReposne>(dcdata);
            return Json(productReposne.Response.ProductList);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public List<Tree> GetGeneology(string MemId, string SessionId)
        {
            Tree model = new Tree();
            model.Fk_UserId = MemId;
            model.SessionId = SessionId;

            DataTable dt = model.GetTreeMembers();
            List<Tree> tree = new List<Tree>();
            foreach (DataRow dr in dt.Rows)
            {
                tree.Add(new Tree
                {
                    Fk_UserId = dr["MemberID"].ToString(),
                    Leg = dr["Leg"].ToString(),
                    Status = dr["Status"].ToString(),
                    MemberName = dr["MemberName"].ToString(),
                    ParentId = dr["ParentId"].ToString(),
                    LoginId = dr["LoginId"].ToString(),
                    ParentLoginId = dr["ParentLoginId"].ToString(),
                    CssClass = dr["cssStatus"].ToString(),
                    SelfBusiness = dr["PackageName"].ToString(),
                    Href = dr["href"].ToString(),
                    JoiningDate = string.IsNullOrEmpty(dr["JoiningDate"].ToString()) ? "N/A" : Convert.ToDateTime(dr["JoiningDate"]).ToString("dd-MMM, yyyy"),
                    ActivationDate = string.IsNullOrEmpty(dr["ActivationDate"].ToString()) ? "N/A" : Convert.ToDateTime(dr["ActivationDate"]).ToString("dd-MMM, yyyy"),
                    Spillby = dr["Spillby"].ToString(),
                    AllLeg1 = dr["AllLeg1"].ToString(),
                    AllLeg2 = dr["AllLeg2"].ToString(),

                    PermanentLeg1 = dr["PermanentLeg1"].ToString(),
                    PermanentLeg2 = dr["PermanentLeg2"].ToString(),

                    InactiveLeft = dr["InactiveLeft"].ToString(),
                    InactiveRight = dr["InactiveRight"].ToString(),

                    PCountLeg1 = dr["PCountLeg1"].ToString(),
                    PCountLeg2 = dr["PCountLeg2"].ToString(),

                    PaidLeg1 = dr["PaidLeg1"].ToString(),
                    PaidLeg2 = dr["PaidLeg2"].ToString(),

                    BalanceLeft = dr["BalanceLeft"].ToString(),
                    BalanceRight = dr["BalanceRight"].ToString(),

                    ProductName = dr["PackageName"].ToString(),
                    Gender = dr["Gender"].ToString(),
                    ProfilePic = dr["ProfilePic"].ToString(),

                    AllBusinessLeft = dr["AllBusinessLeft"].ToString(),
                    AllBusinessRight = dr["AllBusinessRight"].ToString(),

                });




            }
            return tree;

        }

        public List<Tree> GetUserFirst(string MemId)
        {
            Tree model = new Tree();
            model.Fk_UserId = MemId;

            DataTable dt = model.GetUserFirst();
            List<Tree> List = new List<Tree>();
            foreach (DataRow dr in dt.Rows)
            {
                model.Leg = dr["Leg"].ToString();
                model.Status = dr["Status"].ToString();
                model.MemberName = dr["MemberName"].ToString();
                model.ParentId = dr["ParentId"].ToString();
                model.LoginId = dr["LoginId"].ToString();
                model.ParentLoginId = dr["ParentLoginId"].ToString();
                model.BlockStatus = dr["BlockStatus"].ToString();
                model.Fk_UserId = MemId;
                model.JoiningDate = Convert.ToDateTime(dr["JoiningDate"]).ToString("dd-MMM, yyyy");
                model.Spillby = dr["Spillby"].ToString();
                model.AllLeg1 = dr["AllLeg1"].ToString();
                model.AllLeg2 = dr["AllLeg2"].ToString();

                model.PermanentLeg1 = dr["PermanentLeg1"].ToString();
                model.PermanentLeg2 = dr["PermanentLeg2"].ToString();

                model.InactiveLeft = dr["InactiveLeft"].ToString();
                model.InactiveRight = dr["InactiveRight"].ToString();

                model.PCountLeg1 = dr["PCountLeg1"].ToString();
                model.PCountLeg2 = dr["PCountLeg2"].ToString();

                model.PaidLeg1 = dr["PaidLeg1"].ToString();
                model.PaidLeg2 = dr["PaidLeg2"].ToString();

                model.BalanceLeft = dr["BalanceLeft"].ToString();
                model.BalanceRight = dr["BalanceRight"].ToString();

                model.ProductName = dr["PackageName"].ToString();
                model.Gender = dr["Gender"].ToString();
                model.ProfilePic = dr["ProfilePic"].ToString();
                model.ActivationDate = dr["ActivationDate"].ToString();
                model.AllBusinessLeft = dr["AllBusinessLeft"].ToString();
                model.AllBusinessRight = dr["AllBusinessRight"].ToString();

                List.Add(model);
            }

            return List;
        }

        public List<string> SearchCustomersByLoginId2(string prefix)
        {
            List<string> list = new List<string>();
            if (HttpContext.Session.GetString("AdminId") != null)
            {
                int headId = 1;
                Tree tree = new Tree();
                tree.LoginId = prefix;
                DataTable dt = tree.GetSearchData();

                if (dt.Rows.Count > 0)
                {
                    string loginId = dt.Rows[0]["LoginId"].ToString();
                    DataTable dtCheck = tree.CheckParentForTreeView(Convert.ToInt32(dt.Rows[0]["MemberID"]), headId);
                    if (dtCheck.Rows.Count > 0)
                    {
                        if (dtCheck.Rows[0]["Msg"].ToString() == "Success")
                        {
                            int returnID = Convert.ToInt32(dtCheck.Rows[0]["Fk_MemId"]);
                        }
                        else
                        {
                            int returnID = Convert.ToInt32(dtCheck.Rows[0]["Fk_MemId"]);
                        }
                    }

                    foreach (DataRow dr in dtCheck.Rows)
                    {
                        list.Add(string.Format("{0}.{1}", loginId, dr["Fk_MemId"]));
                    }

                }

            }
            else if (HttpContext.Session.GetString("CustomerId") != null)
            {
                int headId = Convert.ToInt32(HttpContext.Session.GetString("CustomerId"));

                Tree tree = new Tree();
                tree.LoginId = prefix;
                DataTable dt = tree.GetSearchData();
                if (dt.Rows.Count > 0)
                {
                    string loginId = dt.Rows[0]["LoginId"].ToString();
                    DataTable dtCheck = tree.CheckParentForTreeView(Convert.ToInt32(dt.Rows[0]["MemberID"]), headId);
                    if (dtCheck.Rows.Count > 0)
                    {
                        if (dtCheck.Rows[0]["Msg"].ToString() == "Success")
                        {
                            int returnID = Convert.ToInt32(dtCheck.Rows[0]["Fk_MemId"]);
                        }
                        else
                        {
                            int returnID = Convert.ToInt32(dtCheck.Rows[0]["Fk_MemId"]);
                        }
                    }

                    foreach (DataRow dr in dtCheck.Rows)
                    {
                        list.Add(string.Format("{0}.{1}", loginId, dr["Fk_MemId"]));
                    }

                }
            }
            else
            {
                list.Add(string.Format("{0}.{1}", "Please Login First", "0"));
            }
            return list;
        }
        public List<string> GetRootData(string prefix)
        {
            List<string> list = new List<string>();


            int headId = Convert.ToInt32(HttpContext.Session.GetString("CustomerId"));

            Tree tree = new Tree();
            tree.LoginId = prefix;

            //string loginId = dt.Rows[0]["LoginId"].ToString();
            DataTable dtCheck = tree.CheckParentForTreeView(int.Parse(prefix), headId);
            if (dtCheck.Rows.Count > 0)
            {
                if (dtCheck.Rows[0]["Msg"].ToString() == "Success")
                {
                    int returnID = Convert.ToInt32(dtCheck.Rows[0]["Fk_MemId"]);
                }
                else
                {
                    int returnID = Convert.ToInt32(dtCheck.Rows[0]["Fk_MemId"]);
                }
            }

            foreach (DataRow dr in dtCheck.Rows)
            {
                list.Add(string.Format("{0}.{1}", "", dr["Fk_MemId"]));
            }



            return list;
        }

        public ActionResult ProductDetails(string Id)
        {
            string CustomerId = HttpContext.Session.GetString("CustomerId") == null ? "0" : HttpContext.Session.GetString("CustomerId");
            string Body = "";
            if (!string.IsNullOrEmpty(Id))
            {
                string obj1 = "{ \"CategoryId\":\"" + 0 + "\", \"ProductId\":\"" + Id + "\",\"CustomerId\":\"" + CustomerId + "\"}";
                Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            }

            else
            {
                string obj1 = "{ \"CategoryId\":\"" + 0 + "\", \"ProductId\":\"" + 0 + "\",\"CustomerId\":\"" + CustomerId + "\"}";
                Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            }

            string result = Common.HITAPI(APIURL.GetAllProducts, Body);
            ProductReposne productReposne = new ProductReposne();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            productReposne = JsonConvert.DeserializeObject<ProductReposne>(dcdata);

            // Console.WriteLine(dashBoardResponse.Response);
            return Json(productReposne.Response.ProductList);
        }

        public ActionResult VarientDetail(string Id)
        {
            string CustomerId = HttpContext.Session.GetString("CustomerId") == null ? "0" : HttpContext.Session.GetString("CustomerId");
            string Body = "";
            if (!string.IsNullOrEmpty(Id))
            {
                string obj1 = "{ \"CategoryId\":\"" + 0 + "\", \"ProductId\":\"" + Id + "\",\"CustomerId\":\"" + CustomerId + "\"}";
                Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            }

            else
            {
                string obj1 = "{ \"CategoryId\":\"" + 0 + "\", \"ProductId\":\"" + 0 + "\",\"CustomerId\":\"" + CustomerId + "\"}";
                Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            }

            string result = Common.HITAPI(APIURL.GetAllProducts, Body);
            ProductReposne productReposne = new ProductReposne();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            productReposne = JsonConvert.DeserializeObject<ProductReposne>(dcdata);

            // Console.WriteLine(dashBoardResponse.Response);
            return View(productReposne);
        }
        public int? AddToCart(string ProductId, string Quantity)
        {

            string Qty = string.IsNullOrEmpty(Quantity) ? "1" : Quantity;
            string Body = "";
            string obj1 = "{\"VarientId\":\"" + (ProductId) + "\",\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"Quantity\":\"" + Qty + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.AddToCart, Body);
            CartResponse cartResponse = new CartResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            cartResponse = JsonConvert.DeserializeObject<CartResponse>(dcdata);
            CartSide Cart = null;
            Cart = CartSide.GetCartData(HttpContext.Session.GetString("CustomerId").ToString());
            HttpContext.Session.SetObjectAsJson("_Cart", Cart);
            HttpContext.Session.SetString("Count", Cart.lstCartList.Count.ToString());
            return cartResponse.Status;
        }

        public ActionResult Login(ChangePassword loginRequest, string btnLogin, string Status, string ProductId, string Quantity, string Page, string btnSendOtp, string btnVerify)
        {
            loginRequest.Status = "0";
            HttpContext.Session.SetString("Page", "Shop");
            if (!string.IsNullOrEmpty(Status))
            {
                Status = Crypto.Decrypt(Status);
                if (Status == "AddToCart")
                {
                    string CustomerId = HttpContext.Session.GetString("CustomerId") == null ? "0" : HttpContext.Session.GetString("CustomerId");
                    HttpContext.Session.SetString("ProductId", ProductId);
                    if (CustomerId == "0")
                    {
                        HttpContext.Session.SetString("Quantity", Quantity);
                        HttpContext.Session.SetString("Status", "AddToCart");

                        return View(loginRequest);
                    }
                    else
                    {
                        int? cartstatus = AddToCart(ProductId, Quantity);
                        if (cartstatus == 0)
                        {

                            return RedirectToAction("Login");
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(Page))
                            {
                                return RedirectToAction("Shop");
                            }
                            return RedirectToAction("Index");

                        }

                    }
                }
                else
                {
                    HttpContext.Session.SetString("PreviousCLoginId", "");
                    HttpContext.Session.SetString("CustomerId", "0");
                    Response.Cookies.Append("MobileNo", "");
                    Response.Cookies.Append("Password", "");
                    Response.Cookies.Append("LoginId", "");
                    Response.Cookies.Append("UserType", "");
                    return RedirectToAction("Shop");
                }
            }
            if (!string.IsNullOrEmpty(btnSendOtp))
            {
                DataSet dataSet = loginRequest.OTPSend();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            loginRequest.Status = "1";

                            string Msg = "Dear Associate, OTP login request is " + dataSet.Tables[0].Rows[0]["OTP"].ToString() + ", OTP valid for 30 minute. http://karyon.organic/";

                            if (BaseUrl.LocalUrl == "https://karyon.organic" && dataSet.Tables[0].Rows[0]["MobileNo"].ToString()!="8052949381")
                            {
                                BLSMS.SendSMS(dataSet.Tables[0].Rows[0]["MobileNo"].ToString(), Msg, TemplateId.OTPTemplateId);
                            }
                        }
                        else
                        {
                            loginRequest.Status = "0";
                            TempData["Login"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                        }
                    }
                    else
                    {
                        loginRequest.Status = "0";
                        TempData["Login"] = "Invalid Mobile No.";
                    }
                }
            }
            if (!string.IsNullOrEmpty(btnVerify))
            {
                DataSet dataSet = loginRequest.VerifyOTP();
                loginRequest.dtDetails = dataSet.Tables[0];
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows.Count == 1 && dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            btnLogin = "Login";
                            loginRequest.LoginId = dataSet.Tables[0].Rows[0]["LoginId"].ToString();
                            loginRequest.ConfirmPassword = dataSet.Tables[0].Rows[0]["Password"].ToString();
                        }
                        else
                        {
                            if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                            {
                                loginRequest.Status = "2";
                            }
                            else
                            {
                                loginRequest.Status = "1";
                                TempData["Login"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            }
                        }
                    }
                    else
                    {
                        loginRequest.Status = "1";
                        TempData["Login"] = "Invalid Mobile No.";
                    }
                }
            }
            if (!string.IsNullOrEmpty(btnLogin))
            {
                string Body = "";
                string obj1 = "{\"LoginId\":\"" + loginRequest.LoginId + "\",\"Password\":\"" + loginRequest.ConfirmPassword + "\",\"LoginFrom\":\"WEB\"}";
                Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                string result = Common.HITAPI(APIURL.Login, Body);
                LoginResponse loginResponse = new LoginResponse();
                ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
                loginResponse = JsonConvert.DeserializeObject<LoginResponse>(dcdata);
                if (loginResponse.Status == 0)
                {
                    HttpContext.Session.SetString("CustomerId", "0");
                    TempData["Login"] = loginResponse.Message;
                }
                else
                {
                    HttpContext.Session.SetString("FirstName", loginResponse.Response.FirstName);
                    HttpContext.Session.SetString("LastName", loginResponse.Response.LastName);
                    HttpContext.Session.SetString("MobileNo", loginResponse.Response.MobileNo);
                    if (loginResponse.Response.FK_FranchiseTypeId > 0)
                    {
                        HttpContext.Session.SetString("FranchiseId", loginResponse.Response.CustomerId.ToString());
                    }
                    else
                    {
                        HttpContext.Session.SetString("CustomerId", loginResponse.Response.CustomerId.ToString());
                    }

                    HttpContext.Session.SetString("LoginId", loginResponse.Response.LoginId);
                    HttpContext.Session.SetString("RegistrationAmount", loginResponse.Response.RegistrationAmount.ToString());
                    HttpContext.Session.SetString("FK_FranchiseTypeId", loginResponse.Response.FK_FranchiseTypeId.ToString());
                    Response.Cookies.Append("LoginId", Crypto.Encrypt(loginResponse.Response.LoginId));
                    Response.Cookies.Append("Password", Crypto.Encrypt(loginRequest.ConfirmPassword));
                    Response.Cookies.Append("UserType", loginResponse.Response.UserType);
                    if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Status")))
                    {
                        AddToCart((HttpContext.Session.GetString("ProductId")), HttpContext.Session.GetString("Quantity"));
                        return RedirectToAction("CartList");
                    }
                    else
                    {
                        if (loginResponse.Response.UserType == "Associate")
                        {
                            AdminRenderMenu(2);
                            return RedirectToAction("NewDashBoard", "Home");
                        }
                        else
                        {
                            AdminRenderMenu(1);
                            return RedirectToAction("DashBoard", "Franchisee");
                        }

                    }

                }

            }
            return View(loginRequest);
        }

        public ActionResult CartList(CartResponse cartResponse, string checkout, string WalletType, string Pk_AddressId, string PromoCode, string PromoCodeDiscountPrice)
        {

            #region WalletBalance
            string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.AssociateWalletBalance, Body);
            WalletBalanceResponse walletBalance = new WalletBalanceResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            walletBalance = JsonConvert.DeserializeObject<WalletBalanceResponse>(dcdata);
            ViewBag.FUPWallet = walletBalance.Response.WalletBalanceData.FUPWallet;
            ViewBag.KaryonWallet = walletBalance.Response.WalletBalanceData.Balance;

            #endregion WalletBalance
            if (!string.IsNullOrEmpty(checkout))
            {
                HttpContext.Session.SetString("WalletType", WalletType);

                Common common = new Common();
                common.Fk_MemId = HttpContext.Session.GetString("CustomerId");
                common.WalletType = WalletType;
                DataSet dataSet = common.CommonCheckProductType();
                if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                {
                    CartData cartData = new CartData();
                    List<CartDetails> listResponses = new List<CartDetails>();
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        CartDetails cartDetails = new CartDetails();
                        cartDetails.CartId = long.Parse(dataSet.Tables[0].Rows[i]["CartId"].ToString());
                        cartDetails.ProductId = long.Parse(dataSet.Tables[0].Rows[i]["Id"].ToString());
                        cartDetails.ProductName = dataSet.Tables[0].Rows[i]["ProductName"].ToString();
                        cartDetails.MRP = float.Parse(dataSet.Tables[0].Rows[i]["MRP"].ToString());
                        cartDetails.TotalBv = float.Parse(dataSet.Tables[0].Rows[i]["TotalBv"].ToString());
                        cartDetails.Quantity = long.Parse(dataSet.Tables[0].Rows[i]["Quantity"].ToString());
                        cartDetails.FinalPrice = float.Parse(dataSet.Tables[0].Rows[i]["FinalPrice"].ToString());
                        cartDetails.Image = BaseUrl.ImageURL + dataSet.Tables[0].Rows[i]["IMAGE"].ToString();
                        cartDetails.ProductDetails = dataSet.Tables[0].Rows[i]["ProductDetails"].ToString();
                        cartDetails.Unit = dataSet.Tables[0].Rows[i]["Unit"].ToString();
                        cartDetails.BrandName = dataSet.Tables[0].Rows[i]["BrandName"].ToString();
                        cartDetails.CartStatus = dataSet.Tables[0].Rows[i]["CartStatus"].ToString();
                        cartDetails.VarientId = long.Parse(dataSet.Tables[0].Rows[i]["Fk_VarientId"].ToString());
                        cartDetails.BorderCss = dataSet.Tables[0].Rows[i]["BorderCss"].ToString();

                        listResponses.Add(cartDetails);
                    }
                    cartData.CartList = listResponses;
                    cartResponse.Response = cartData;
                    return View(cartResponse);
                }
                else
                {

                    return RedirectToAction("AddAddress", "Associate", new { PlaceOrders = "PlaceOrders", Pk_AddressId = Pk_AddressId, PromoCode = PromoCode, PromoCodeDiscountPrice = PromoCodeDiscountPrice });
                }
            }
            obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);


            result = Common.HITAPI(APIURL.CartList, Body);
            deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            cartResponse = JsonConvert.DeserializeObject<CartResponse>(dcdata);





            return View(cartResponse);
        }
        public ActionResult DeleteCartItem(string Id, string Status)
        {
            string Body = "";

            string obj1 = "{\"CartId\":\"" + Crypto.Decrypt(Id) + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.DeleteCart, Body);
            CartResponse cartResponse = new CartResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            cartResponse = JsonConvert.DeserializeObject<CartResponse>(dcdata);
            CartSide Cart = null;
            Cart = CartSide.GetCartData(HttpContext.Session.GetString("CustomerId").ToString());
            HttpContext.Session.SetObjectAsJson("_Cart", Cart);
            if (Cart.lstCartList == null)
            {
                HttpContext.Session.SetString("Count", "0");

            }
            else
            {
                HttpContext.Session.SetString("Count", Cart.lstCartList.Count.ToString());
            }
            if (!string.IsNullOrEmpty(Status))
            {
                return RedirectToAction("PlacePack", "Associate");
            }
            else
            {
                return RedirectToAction("CartList");
            }

        }

        public ActionResult GetSponsorName(string SponsorId)
        {
            string Body = "";
            string obj1 = "{\"SponsorLoginId\":\"" + SponsorId + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetSponsorName, Body);
            SponsorResponse sponsorResponse = new SponsorResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            sponsorResponse = JsonConvert.DeserializeObject<SponsorResponse>(dcdata);

            return Json(sponsorResponse);
        }
        public ActionResult GetSponsorNameByNewId(string SponsorId)
        {
            string Body = "";
            string obj1 = "{\"SponsorLoginId\":\"" + SponsorId + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetSponsorNameByNewId, Body);
            SponsorResponse sponsorResponse = new SponsorResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            sponsorResponse = JsonConvert.DeserializeObject<SponsorResponse>(dcdata);

            return Json(sponsorResponse);
        }
        public ActionResult GetStateCity(string PinCode)
        {
            string Body = "";
            string obj1 = "{\"Pincode\":\"" + PinCode + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetStateCity, Body);
            StateCityResponse stateCityResponse = new StateCityResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            stateCityResponse = JsonConvert.DeserializeObject<StateCityResponse>(dcdata);

            return Json(stateCityResponse);
        }
        public async Task<ActionResult> Registration(Registration registration, string btnRegister, string pid, string Zone)
        {
            if (!string.IsNullOrEmpty(btnRegister))
            {
                if (registration.ImageFile != null)
                {
                    string fileName = await FileManagement.WriteFiles(registration.ImageFile, "AssociatePanCard");
                    registration.PancardImg = fileName;
                }
                string Body = "";
                string obj1 = "{\"SponsorLoginId\":\"" + registration.SponsorLoginId + "\",\"Leg\":\"" + "L" + "\",\"FirstName\":\"" + registration.FirstName + "\",\"LastName\":\"" + registration.LastName + "\",\"MobileNo\":\"" + registration.MobileNo + "\",\"PanCard\":\"" + registration.PanCard + "\",\"Email\":\"" + registration.Email + "\",\"Address\":\"\",\"Pincode\":\"" + registration.Pincode + "\",\"State\":\"" + registration.State + "\",\"City\":\"" + registration.City + "\",\"DeviceId\":\"\",\"DeviceToken\":\"\",\"Password\":\"" + registration.Password + "\",\"MiddleName\":\"" + registration.MiddleName + "\",\"PanCard\":\"" + registration.PanCard + "\",\"PancardImg\":\"" + registration.PancardImg + "\",\"RegistrationFrom\":\"Web\"}";
                Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                string result = Common.HITAPI(APIURL.Registration, Body);
                LoginResponse loginResponse = new LoginResponse();
                ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
                loginResponse = JsonConvert.DeserializeObject<LoginResponse>(dcdata);
                if (loginResponse.Status == 1)
                {
                    HttpContext.Session.SetString("FirstName", loginResponse.Response.FirstName);
                    HttpContext.Session.SetString("MiddleName", loginResponse.Response.MiddleName);
                    HttpContext.Session.SetString("LastName", loginResponse.Response.LastName);
                    HttpContext.Session.SetString("Password", registration.Password);
                    HttpContext.Session.SetString("LoginId", loginResponse.Response.LoginId);
                    try
                    {
                        string Msg = "CONGRATULATIONS!! " + loginResponse.Response.FirstName + " " + loginResponse.Response.LastName + "ON BECOMING PART OF KARYON. YOUR ID NO: " + loginResponse.Response.LoginId + "AND PASSWORD: " + registration.Password + " VISIT OUR WEBISTE: www.karyon.organic";
                        //string Msg = "Dear Associate OTP for login request is "+ dataSet.Tables[0].Rows[0]["OTP"].ToString() + ", OTP is valid for 30 minute. "+CompanyDetails.Website;

                        BLSMS.SendSMS(registration.MobileNo, Msg, TemplateId.Registartion);
                    }
                    catch { }
                    return RedirectToAction("Congratulations");
                }
                else
                {
                    TempData["Registraion"] = loginResponse.Message;
                }
            }
            else if (!string.IsNullOrEmpty(pid))
            {
                registration.Leg = Zone;
                registration.SponsorLoginId = pid;
                return View(registration);

            }
            return View();
        }
        public ActionResult Congratulations()
        {
            return View();
        }

        public ActionResult AddToCarts(string ProductId, string Quantity)
        {
            string CustomerId = HttpContext.Session.GetString("CustomerId");
            string Body = "";
            if (string.IsNullOrEmpty(Quantity))
            {
                Quantity = "1";
            }
            string obj1 = "{\"VarientId\":\"" + (ProductId) + "\",\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"Quantity\":\"" + Quantity + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.AddToCart, Body);
            CartResponse cartResponse = new CartResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            cartResponse = JsonConvert.DeserializeObject<CartResponse>(dcdata);
            CartSide Cart = null;
            Cart = CartSide.GetCartData(HttpContext.Session.GetString("CustomerId").ToString());
            HttpContext.Session.SetObjectAsJson("_Cart", Cart);
            HttpContext.Session.SetString("Count", Cart.lstCartList.Count.ToString());
            return Json(cartResponse);
        }
        public ActionResult DashBoard()
        {
            string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetAssociateDashBoard, Body);
            AssociateDashBoardResponseWeb associateDashBoardResponse = new AssociateDashBoardResponseWeb();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            associateDashBoardResponse = JsonConvert.DeserializeObject<AssociateDashBoardResponseWeb>(dcdata);
            if (associateDashBoardResponse.Status == "1")
            {
                string a = associateDashBoardResponse.Response.ShowEventMenu.ToString();
                HttpContext.Session.SetString("ShowEventMenu", associateDashBoardResponse.Response.ShowEventMenu.ToString());

            }
            return View(associateDashBoardResponse);
        }

        public ActionResult VarientDetails(string ProductId)
        {
            string Body = "";
            if (!string.IsNullOrEmpty(ProductId))
            {
                string obj1 = "{ \"CategoryId\":\"" + 0 + "\", \"ProductId\":\"" + (ProductId) + "\",\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\"}";
                Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            }


            string result = Common.HITAPI(APIURL.GetAllProducts, Body);
            ProductReposne productReposne = new ProductReposne();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            productReposne = JsonConvert.DeserializeObject<ProductReposne>(dcdata);

            // Console.WriteLine(dashBoardResponse.Response);
            return Json(productReposne.Response.ProductList);
        }
        public ActionResult CheckLogin(string ProductId, string Quantity)
        {
            string Status = Crypto.Encrypt("AddToCart");

            string URl = HttpContext.Session.GetString("Redirect");

            return Json(Status);
            //return RedirectToAction("Login", new { Status = Status, ProductId = Crypto.Encrypt(ProductId), Quantity = Quantity });
        }

        //public ActionResult FranchiseRegistration(FranchiseRegistrationRequest registration, string btnRegister)
        //{
        //    if (!string.IsNullOrEmpty(btnRegister))
        //    {
        //        string Body = "";
        //        string obj1 = "{\"AssociateLoginId\":\"" + registration.AssociateLoginId + "\",\"CompanyName\":\"" + registration.CompanyName + "\",\"ContactPerson\":\"" + registration.ContactPerson + "\",\"EmailId\":\"" + registration.EmailId + "\",\"MobileNo\":\"" + registration.MobileNo + "\",\"Address\":\"" + registration.Address + "\",\"Pincode\":\"" + registration.Pincode + "\",\"GSTNo\":\"" + registration.GSTNo + "\",\"PanCard\":\"" + registration.PanCard + "\",\"FK_FranchiseTypeId\":\"" + registration.FK_FranchiseTypeId + "\",\"FirmAddress\":\"" + registration.FirmAddress + "\"}";
        //        Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
        //        string result = Common.HITAPI(APIURL.FranchiseRequest, Body);
        //        FranchiseResponse franchiseResponse = new FranchiseResponse();
        //        ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
        //        string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
        //        franchiseResponse = JsonConvert.DeserializeObject<FranchiseResponse>(dcdata);
        //        if (franchiseResponse.Status == 1)
        //        {
        //            TempData["Registraion"] = franchiseResponse.Message;
        //        }
        //        else
        //        {
        //            TempData["Registraion"] = franchiseResponse.Message;
        //        }
        //    }

        //    return View();
        //}
        public ActionResult FranchiseRegistration(FranchiseRegistrationRequest registration, string btnRegister)
        {
            if (!string.IsNullOrEmpty(btnRegister))
            {
                string Body = "";
                string obj1 = "{\"IsSpecial\":\"0\",\"SponsorLoginId\":\"" + registration.SponsorLoginId + "\",\"AssociateLoginId\":\"" + registration.AssociateLoginId + "\",\"CompanyName\":\"" + registration.CompanyName + "\",\"ContactPerson\":\"" + registration.ContactPerson + "\",\"EmailId\":\"" + registration.EmailId + "\",\"MobileNo\":\"" + registration.MobileNo + "\",\"Address\":\"" + registration.Address + "\",\"Pincode\":\"" + registration.Pincode + "\",\"Password\":\"" + registration.Password + "\",\"State\":\"" + registration.State + "\",\"City\":\"" + registration.City + "\",\"DeviceId\":\"\",\"DeviceToken\":\"\",\"RegistrationAmount\":\"" + registration.RegistrationAmount + "\",\"RegistrationFrom\":\"Web\"}";
                Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                string result = Common.HITAPI(APIURL.FranchiseRegistration, Body);
                FranchiseResponse franchiseResponse = new FranchiseResponse();
                ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
                franchiseResponse = JsonConvert.DeserializeObject<FranchiseResponse>(dcdata);
                if (franchiseResponse.Status == 1)
                {
                    HttpContext.Session.SetString("ContactPerson", franchiseResponse.Response.ContactPerson);
                    HttpContext.Session.SetString("FirstName", franchiseResponse.Response.ContactPerson);
                    HttpContext.Session.SetString("LastName", "");
                    HttpContext.Session.SetString("Password", registration.Password);
                    HttpContext.Session.SetString("LoginId", franchiseResponse.Response.LoginId);
                    try
                    {
                        string Msg = "CONGRATULATIONS!! " + franchiseResponse.Response.ContactPerson + "YOUR KARYON CENTER ID: " + franchiseResponse.Response.LoginId + " , PASSWORD: " + registration.Password + ", VISIT : https://karyon.organic/Home/Login HEALTHY MORNING";
                        //string Msg = "Dear Associate OTP for login request is "+ dataSet.Tables[0].Rows[0]["OTP"].ToString() + ", OTP is valid for 30 minute. "+CompanyDetails.Website;

                        BLSMS.SendSMS(registration.MobileNo, Msg, TemplateId.FranchiseRegistartion);
                    }
                    catch { }
                    obj1 = "{\"LoginId\":\"" + franchiseResponse.Response.LoginId + "\",\"Password\":\"" + registration.Password + "\",\"MobileNo\":\"" + registration.MobileNo + "\",\"LoginFrom\":\"WEB\"}";
                    Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                    result = Common.HITAPI(APIURL.LoginByAdmin, Body);
                    LoginResponse loginResponse = new LoginResponse();
                    deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                    dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
                    loginResponse = JsonConvert.DeserializeObject<LoginResponse>(dcdata);
                    HttpContext.Session.SetString("FirstName", loginResponse.Response.FirstName);
                    HttpContext.Session.SetString("LastName", loginResponse.Response.LastName);
                    if (loginResponse.Response.FK_FranchiseTypeId > 0)
                    {
                        HttpContext.Session.SetString("FranchiseId", loginResponse.Response.CustomerId.ToString());
                    }
                    else
                    {
                        HttpContext.Session.SetString("CustomerId", loginResponse.Response.CustomerId.ToString());
                    }

                    HttpContext.Session.SetString("LoginId", loginResponse.Response.LoginId);
                    HttpContext.Session.SetString("RegistrationAmount", loginResponse.Response.RegistrationAmount.ToString());
                    HttpContext.Session.SetString("FK_FranchiseTypeId", loginResponse.Response.FK_FranchiseTypeId.ToString());
                    Response.Cookies.Append("LoginId", Crypto.Encrypt(loginResponse.Response.LoginId));
                    Response.Cookies.Append("Password", Crypto.Encrypt(registration.Password));
                    Response.Cookies.Append("UserType", loginResponse.Response.UserType);
                    if (loginResponse.Response.UserType == "Associate")
                    {
                        return RedirectToAction("Congratulations");
                    }
                    else
                    {
                        AdminRenderMenu(1);
                        if (loginResponse.Response.TotalOrder > 0)
                        {
                            return RedirectToAction("Dashboard", "Franchisee");
                        }
                        else
                        {
                            return RedirectToAction("CreateOrder", "Franchisee");
                        }

                    }

                    //return RedirectToAction("Congratulations");
                }
                else
                {
                    TempData["Registraion"] = franchiseResponse.Message;
                }
            }
            return View();
        }
        public ActionResult GetFranchiseSponsorName(string SponsorId)
        {
            string Body = "";
            string obj1 = "{\"SponsorLoginId\":\"" + SponsorId + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetFranchiseSponsorName, Body);
            SponsorResponse sponsorResponse = new SponsorResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            sponsorResponse = JsonConvert.DeserializeObject<SponsorResponse>(dcdata);

            return Json(sponsorResponse);
        }

        public ActionResult AdminLogin(LoginRequest loginRequest, string btnLogin)
        {

            if (!string.IsNullOrEmpty(btnLogin))
            {
                string Body = "";
                string obj1 = "{\"LoginId\":\"" + loginRequest.LoginId + "\",\"Password\":\"" + loginRequest.Password + "\",\"LoginFrom\":\"WEB\"}";
                Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                string result = Common.HITAPI(APIURL.AdminLogin, Body);
                LoginResponse loginResponse = new LoginResponse();
                ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
                loginResponse = JsonConvert.DeserializeObject<LoginResponse>(dcdata);
                if (loginResponse.Status == 0)
                {
                    HttpContext.Session.SetString("CustomerId", "0");
                    TempData["Login"] = loginResponse.Message;
                }
                else
                {
                    HttpContext.Session.SetString("Name", loginResponse.Response.FirstName);
                    HttpContext.Session.SetString("AdminId", loginResponse.Response.CustomerId.ToString());
                    HttpContext.Session.SetString("LoginId", loginRequest.LoginId);
                    Response.Cookies.Append("LoginId", Crypto.Encrypt(loginRequest.LoginId));
                    Response.Cookies.Append("Password", Crypto.Encrypt(loginRequest.Password));
                    return RedirectToAction("DashBoard", "Admin");

                }
            }

            return View();
        }

        public ActionResult GetFranchiseOrderDetails(string OrderNo, string Type, string DispatchCount)
        {
            CreateOrder createOrder = new CreateOrder();
            createOrder.OrderNo = OrderNo;
            createOrder.OpCode = 2;
            createOrder.Type = Type;
            createOrder.DispatchCount = string.IsNullOrEmpty(DispatchCount) ? 0 : int.Parse(DispatchCount);
            createOrder.Fk_ParentFranchiseId = HttpContext.Session.GetString("FranchiseId");
            List<CreateOrder> lst = new List<CreateOrder>();
            DataSet dataSet = createOrder.GetFranchiseOrders();
            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
            {
                CreateOrder create = new CreateOrder();
                create.OrderNo = dataSet.Tables[0].Rows[i]["OrderNo"].ToString();
                create.ProductName = dataSet.Tables[0].Rows[i]["ProductName"].ToString();
                create.Amount = decimal.Parse(dataSet.Tables[0].Rows[i]["MRP"].ToString());
                create.PV = decimal.Parse(dataSet.Tables[0].Rows[i]["PV"].ToString());
                create.Qty = int.Parse(dataSet.Tables[0].Rows[i]["Qty"].ToString());
                create.SubTotal = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalPrice"].ToString());
                create.Fk_ProductId = int.Parse(dataSet.Tables[0].Rows[i]["Fk_VarientId"].ToString());
                create.Fk_OrderId = int.Parse(dataSet.Tables[0].Rows[i]["Fk_OrderId"].ToString());
                create.IsDispatched = dataSet.Tables[0].Rows[i]["IsDispatched"].ToString();
                create.PendingQty = dataSet.Tables[0].Rows[i]["PendingQty"].ToString();
                create.CompanyName = dataSet.Tables[0].Rows[i]["CompanyName"].ToString();
                create.DispatchQty = int.Parse(dataSet.Tables[0].Rows[i]["DispatchQty"].ToString());
                create.Stock = int.Parse(dataSet.Tables[0].Rows[i]["Stock"].ToString());
                lst.Add(create);
            }
            return Json(lst);

        }
        public ActionResult GetCustomerOrderDetails(string OrderNo, string Type)
        {
            CreateOrder createOrder = new CreateOrder();
            createOrder.OrderNo = OrderNo;
            createOrder.OpCode = 2;
            createOrder.Type = Type;
            createOrder.Fk_DispatchId = HttpContext.Session.GetString("FranchiseId");
            createOrder.CustomerId = HttpContext.Session.GetString("CustomerId").ToString() == "0" ? int.Parse(HttpContext.Session.GetString("FranchiseId")) : int.Parse(HttpContext.Session.GetString("CustomerId"));
            List<CreateOrder> lst = new List<CreateOrder>();
            DataSet dataSet = createOrder.GetCustomerOrders();

            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
            {
                CreateOrder create = new CreateOrder();
                create.OrderNo = dataSet.Tables[0].Rows[i]["OrderNo"].ToString();
                create.ProductName = dataSet.Tables[0].Rows[i]["ProductName"].ToString();
                create.Amount = decimal.Parse(dataSet.Tables[0].Rows[i]["MRP"].ToString());
                create.PV = decimal.Parse(dataSet.Tables[0].Rows[i]["PV"].ToString());
                create.Qty = int.Parse(dataSet.Tables[0].Rows[i]["Qty"].ToString());
                create.SubTotal = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalPrice"].ToString());
                create.Fk_ProductId = int.Parse(dataSet.Tables[0].Rows[i]["Fk_VarientId"].ToString());
                create.Fk_OrderId = int.Parse(dataSet.Tables[0].Rows[i]["Fk_OrderId"].ToString());
                create.IsDispatched = dataSet.Tables[0].Rows[i]["IsDispatched"].ToString();
                create.PendingQty = dataSet.Tables[0].Rows[i]["PendingQty"].ToString();
                create.CompanyName = dataSet.Tables[0].Rows[i]["CompanyName"].ToString();
                create.CustomerName = dataSet.Tables[0].Rows[i]["CustomerName"].ToString();
                create.Address = dataSet.Tables[0].Rows[i]["Address"].ToString();
                create.MobileNo = dataSet.Tables[0].Rows[i]["MobileNo"].ToString();
                create.PurchaseBy = dataSet.Tables[0].Rows[i]["PurchseBy"].ToString();
                create.PaymentMode = dataSet.Tables[0].Rows[i]["PaymentMode"].ToString();
                create.DispatchQty = int.Parse(dataSet.Tables[0].Rows[i]["DispatchQty"].ToString());
                create.Stock = int.Parse(dataSet.Tables[0].Rows[i]["Stock"].ToString());
                createOrder.Fk_ProductId = int.Parse(dataSet.Tables[0].Rows[0]["ProductId"].ToString());

                lst.Add(create);
            }
            List<CreateOrder> orderList = new List<CreateOrder>();
            CreateOrder order = new CreateOrder();
            if (dataSet.Tables[1] != null)
            {
                if (dataSet.Tables[1].Rows.Count > 0)
                {
                    List<ProductReviewData> review = new List<ProductReviewData>();
                    for (int i = 0; i <= dataSet.Tables[1].Rows.Count - 1; i++)
                    {

                        ProductReviewData reviewData = new ProductReviewData();
                        reviewData.ProductImage = dataSet.Tables[1].Rows[i]["Image"].ToString();
                        reviewData.Rating = dataSet.Tables[1].Rows[i]["Rating"].ToString();
                        reviewData.Star = decimal.Parse(dataSet.Tables[1].Rows[i]["Star"].ToString());
                        reviewData.AddedDate = dataSet.Tables[1].Rows[i]["AddedDate"].ToString();
                        reviewData.ProductName = dataSet.Tables[1].Rows[i]["ProductName"].ToString();
                        review.Add(reviewData);
                    }

                    order.ReviewList = review;
                }

            }

            order.Orderdetails = lst;
            orderList.Add(order);

            return Json(orderList);

        }
        public ActionResult GetCustomerOrderDetailsForFranchisee(string OrderNo, string Type, string DispatchCount)
        {
            CreateOrder createOrder = new CreateOrder();
            createOrder.OrderNo = OrderNo;
            createOrder.OpCode = 2;
            createOrder.Type = Type;
            createOrder.Fk_DispatchId = HttpContext.Session.GetString("FranchiseId");
           // createOrder.DispatchCount = int.Parse(DispatchCount);
            if (DispatchCount != null)
            {
                createOrder.DispatchCount = int.Parse(DispatchCount);
            }
            List<CreateOrder> lst = new List<CreateOrder>();
            DataSet dataSet = createOrder.GetCustomerOrders();

            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
            {
                CreateOrder create = new CreateOrder();
                create.OrderNo = dataSet.Tables[0].Rows[i]["OrderNo"].ToString();
                create.ProductName = dataSet.Tables[0].Rows[i]["ProductName"].ToString();
                create.Amount = decimal.Parse(dataSet.Tables[0].Rows[i]["MRP"].ToString());
                create.PV = decimal.Parse(dataSet.Tables[0].Rows[i]["PV"].ToString());
                create.Qty = int.Parse(dataSet.Tables[0].Rows[i]["Qty"].ToString());
                create.SubTotal = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalPrice"].ToString());
                create.Fk_ProductId = int.Parse(dataSet.Tables[0].Rows[i]["Fk_VarientId"].ToString());
                create.Fk_OrderId = int.Parse(dataSet.Tables[0].Rows[i]["Fk_OrderId"].ToString());
                create.IsDispatched = dataSet.Tables[0].Rows[i]["IsDispatched"].ToString();
                create.PendingQty = dataSet.Tables[0].Rows[i]["PendingQty"].ToString();
                create.CompanyName = dataSet.Tables[0].Rows[i]["CompanyName"].ToString();
                create.CustomerName = dataSet.Tables[0].Rows[i]["CustomerName"].ToString();
                create.Address = dataSet.Tables[0].Rows[i]["Address"].ToString();
                create.MobileNo = dataSet.Tables[0].Rows[i]["MobileNo"].ToString();
                create.PurchaseBy = dataSet.Tables[0].Rows[i]["PurchseBy"].ToString();
                create.PaymentMode = dataSet.Tables[0].Rows[i]["PaymentMode"].ToString();
                create.DispatchQty = int.Parse(dataSet.Tables[0].Rows[i]["DispatchQty"].ToString());
                create.Stock = int.Parse(dataSet.Tables[0].Rows[i]["Stock"].ToString());
                createOrder.Fk_ProductId = int.Parse(dataSet.Tables[0].Rows[0]["ProductId"].ToString());

                lst.Add(create);
            }



            return Json(lst);

        }
        //public ActionResult GetReview(ProductReviewRequest model, int fk_OrderId)
        //{
        //    model.CustomerId = int.Parse(HttpContext.Session.GetString("CustomerId"));
        //    model.Fk_OrderId = fk_OrderId;
        //    model.OpCode = 3;
        //    DataSet dataSet = model.GetProductReview();
        //    model.dtDetailsR = dataSet.Tables[0];
        //    List<ProductReviewData> lst = new List<ProductReviewData>();

        //    for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
        //    {
        //        ProductReviewData reviewData = new ProductReviewData();
        //        reviewData.ProductImage = dataSet.Tables[0].Rows[i]["Image"].ToString();
        //        reviewData.Rating = dataSet.Tables[0].Rows[i]["Rating"].ToString();
        //        reviewData.Star = int.Parse(dataSet.Tables[0].Rows[i]["Star"].ToString());
        //        reviewData.AddedDate = dataSet.Tables[0].Rows[i]["AddedDate"].ToString();
        //        reviewData.AddedDate = dataSet.Tables[0].Rows[i]["AddedDate"].ToString();


        //        lst.Add(reviewData);
        //    }
        //    //model.Star = int.Parse(ds.Tables[0].Rows[0]["Star"].ToString());
        //    //model.Rating = ds.Tables[0].Rows[0]["Rating"].ToString();
        //    return Json(lst);
        //}

        public ActionResult ForgotPassword(ChangePassword changePassword, string btnSendOtp, string btnVerify, string btnChange)
        {
            if (!string.IsNullOrEmpty(btnSendOtp))
            {
                //changePassword.IdType = "Associate";
                DataSet dataSet = changePassword.OTPSend();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            changePassword.Status = "1";
                            string Msg = "Dear Associate, OTP login request is " + dataSet.Tables[0].Rows[0]["OTP"].ToString() + ", OTP valid for 30 minute. http://karyon.organic/";
                            //string Msg = "Dear Associate OTP for login request is "+ dataSet.Tables[0].Rows[0]["OTP"].ToString() + ", OTP is valid for 30 minute. "+CompanyDetails.Website;

                            BLSMS.SendSMS(dataSet.Tables[0].Rows[0]["MobileNo"].ToString(), Msg, TemplateId.OTPTemplateId);
                            return View(changePassword);
                        }
                        else
                        {
                            changePassword.Status = "0";
                            TempData["ForgotPassword"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            return View(changePassword);
                        }
                    }
                    else
                    {
                        changePassword.Status = "0";
                        TempData["ForgotPassword"] = "Invalid LoginId";
                        return View(changePassword);
                    }
                }

            }
            if (!string.IsNullOrEmpty(btnVerify))
            {
                DataSet dataSet = changePassword.VerifyOTP();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            changePassword.Status = "2";
                            return View(changePassword);
                        }
                        else
                        {
                            changePassword.Status = "1";
                            TempData["ForgotPassword"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            return View(changePassword);
                        }
                    }
                    else
                    {
                        changePassword.Status = "1";
                        TempData["ForgotPassword"] = "Invalid LoginId";
                        return View(changePassword);
                    }
                }
            }
            if (!string.IsNullOrEmpty(btnChange))
            {
                DataSet dataSet = changePassword.ForgetPassword();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            changePassword.Status = "0";
                            TempData["ForgotPassword"] = "Password Changed Successfully";
                            return View(changePassword);
                        }
                        else
                        {
                            changePassword.Status = "1";
                            TempData["ForgotPassword"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            return View(changePassword);
                        }
                    }
                    else
                    {
                        changePassword.Status = "1";
                        TempData["ForgotPassword"] = "Invalid LoginId";
                        return View(changePassword);
                    }
                }
            }
            else
            {
                changePassword.Status = "0";
            }
            return View(changePassword);
        }

        public ActionResult ForgotId(ChangePassword changePassword, string btnSendOtp, string btnVerify)
        {
            if (!string.IsNullOrEmpty(btnSendOtp))
            {
                DataSet dataSet = changePassword.OTPSend();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            changePassword.Status = "1";

                            string Msg = "Dear Associate, OTP login request is " + dataSet.Tables[0].Rows[0]["OTP"].ToString() + ", OTP valid for 30 minute. http://karyon.organic/";
                            //string Msg = "Dear Associate OTP for login request is "+ dataSet.Tables[0].Rows[0]["OTP"].ToString() + ", OTP is valid for 30 minute. "+CompanyDetails.Website;

                            BLSMS.SendSMS(dataSet.Tables[0].Rows[0]["MobileNo"].ToString(), Msg, TemplateId.OTPTemplateId);

                            return View(changePassword);
                        }
                        else
                        {
                            changePassword.Status = "0";
                            TempData["ForgotId"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            return View(changePassword);
                        }
                    }
                    else
                    {
                        changePassword.Status = "0";
                        TempData["ForgotId"] = "Invalid Mobile No.";
                        return View(changePassword);
                    }
                }

            }
            if (!string.IsNullOrEmpty(btnVerify))
            {
                DataSet dataSet = changePassword.VerifyOTP();
                changePassword.dtDetails = dataSet.Tables[0];
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            changePassword.Status = "2";
                            return View(changePassword);
                        }
                        else
                        {
                            changePassword.Status = "1";
                            TempData["ForgotId"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            return View(changePassword);
                        }
                    }
                    else
                    {
                        changePassword.Status = "1";
                        TempData["ForgotId"] = "Invalid Mobile No.";
                        return View(changePassword);
                    }
                }
            }
            else
            {
                changePassword.Status = "0";

            }
            return View(changePassword);

        }
        public List<Tree> GetGeneologyAdmin(string MemId, string SessionId)
        {
            Tree model = new Tree();
            model.Fk_UserId = MemId;
            model.SessionId = SessionId;

            DataTable dt = model.GetTreeMembersAdmin();
            List<Tree> tree = new List<Tree>();
            foreach (DataRow dr in dt.Rows)
            {
                tree.Add(new Tree
                {
                    Fk_UserId = dr["MemberID"].ToString(),
                    Leg = dr["Leg"].ToString(),
                    Status = dr["Status"].ToString(),
                    MemberName = dr["MemberName"].ToString(),
                    ParentId = dr["ParentId"].ToString(),
                    LoginId = dr["LoginId"].ToString(),
                    ParentLoginId = dr["ParentLoginId"].ToString(),
                    CssClass = dr["cssStatus"].ToString(),
                    SelfBusiness = dr["PackageName"].ToString(),
                    Href = dr["href"].ToString(),
                    JoiningDate = string.IsNullOrEmpty(dr["JoiningDate"].ToString()) ? "N/A" : Convert.ToDateTime(dr["JoiningDate"]).ToString("dd-MMM, yyyy"),
                    ActivationDate = string.IsNullOrEmpty(dr["ActivationDate"].ToString()) ? "N/A" : Convert.ToDateTime(dr["ActivationDate"]).ToString("dd-MMM, yyyy"),
                    Spillby = dr["Spillby"].ToString(),
                    AllLeg1 = dr["AllLeg1"].ToString(),
                    AllLeg2 = dr["AllLeg2"].ToString(),

                    PermanentLeg1 = dr["PermanentLeg1"].ToString(),
                    PermanentLeg2 = dr["PermanentLeg2"].ToString(),

                    InactiveLeft = dr["InactiveLeft"].ToString(),
                    InactiveRight = dr["InactiveRight"].ToString(),

                    PCountLeg1 = dr["PCountLeg1"].ToString(),
                    PCountLeg2 = dr["PCountLeg2"].ToString(),

                    PaidLeg1 = dr["PaidLeg1"].ToString(),
                    PaidLeg2 = dr["PaidLeg2"].ToString(),

                    BalanceLeft = dr["BalanceLeft"].ToString(),
                    BalanceRight = dr["BalanceRight"].ToString(),

                    ProductName = dr["PackageName"].ToString(),
                    Gender = dr["Gender"].ToString(),
                    ProfilePic = dr["ProfilePic"].ToString(),

                    AllBusinessLeft = dr["AllBusinessLeft"].ToString(),
                    AllBusinessRight = dr["AllBusinessRight"].ToString(),

                });




            }
            return tree;

        }
        public List<Tree> GetUserFirstAdmin(string MemId)
        {
            Tree model = new Tree();
            model.Fk_UserId = MemId;

            DataTable dt = model.GetUserFirstAdmin();
            List<Tree> List = new List<Tree>();
            foreach (DataRow dr in dt.Rows)
            {
                model.Leg = dr["Leg"].ToString();
                model.Status = dr["Status"].ToString();
                model.MemberName = dr["MemberName"].ToString();
                model.ParentId = dr["ParentId"].ToString();
                model.LoginId = dr["LoginId"].ToString();
                model.ParentLoginId = dr["ParentLoginId"].ToString();
                model.BlockStatus = dr["BlockStatus"].ToString();
                model.Fk_UserId = MemId;
                model.JoiningDate = Convert.ToDateTime(dr["JoiningDate"]).ToString("dd-MMM, yyyy");
                model.Spillby = dr["Spillby"].ToString();
                model.AllLeg1 = dr["AllLeg1"].ToString();
                model.AllLeg2 = dr["AllLeg2"].ToString();

                model.PermanentLeg1 = dr["PermanentLeg1"].ToString();
                model.PermanentLeg2 = dr["PermanentLeg2"].ToString();

                model.InactiveLeft = dr["InactiveLeft"].ToString();
                model.InactiveRight = dr["InactiveRight"].ToString();

                model.PCountLeg1 = dr["PCountLeg1"].ToString();
                model.PCountLeg2 = dr["PCountLeg2"].ToString();

                model.PaidLeg1 = dr["PaidLeg1"].ToString();
                model.PaidLeg2 = dr["PaidLeg2"].ToString();

                model.BalanceLeft = dr["BalanceLeft"].ToString();
                model.BalanceRight = dr["BalanceRight"].ToString();

                model.ProductName = dr["PackageName"].ToString();
                model.Gender = dr["Gender"].ToString();
                model.ProfilePic = dr["ProfilePic"].ToString();
                model.ActivationDate = dr["ActivationDate"].ToString();
                model.AllBusinessLeft = dr["AllBusinessLeft"].ToString();
                model.AllBusinessRight = dr["AllBusinessRight"].ToString();

                List.Add(model);
            }

            return List;
        }
        public List<string> SearchCustomersByLoginId2Admin(string prefix)
        {
            List<string> list = new List<string>();
            if (HttpContext.Session.GetString("AdminId") != null)
            {
                int headId = 1;
                Tree tree = new Tree();
                tree.LoginId = prefix;
                DataTable dt = tree.GetSearchDataAdmin();

                if (dt.Rows.Count > 0)
                {
                    string loginId = dt.Rows[0]["LoginId"].ToString();
                    DataTable dtCheck = tree.CheckParentForTreeView(Convert.ToInt32(dt.Rows[0]["MemberID"]), headId);
                    if (dtCheck.Rows.Count > 0)
                    {
                        if (dtCheck.Rows[0]["Msg"].ToString() == "Success")
                        {
                            int returnID = Convert.ToInt32(dtCheck.Rows[0]["Fk_MemId"]);
                        }
                        else
                        {
                            int returnID = Convert.ToInt32(dtCheck.Rows[0]["Fk_MemId"]);
                        }
                    }

                    foreach (DataRow dr in dtCheck.Rows)
                    {
                        list.Add(string.Format("{0}.{1}", loginId, dr["Fk_MemId"]));
                    }

                }

            }
            else if (HttpContext.Session.GetString("CustomerId") != null)
            {
                int headId = Convert.ToInt32(HttpContext.Session.GetString("CustomerId"));

                Tree tree = new Tree();
                tree.LoginId = prefix;
                DataTable dt = tree.GetSearchData();
                if (dt.Rows.Count > 0)
                {
                    string loginId = dt.Rows[0]["LoginId"].ToString();
                    DataTable dtCheck = tree.CheckParentForTreeView(Convert.ToInt32(dt.Rows[0]["MemberID"]), headId);
                    if (dtCheck.Rows.Count > 0)
                    {
                        if (dtCheck.Rows[0]["Msg"].ToString() == "Success")
                        {
                            int returnID = Convert.ToInt32(dtCheck.Rows[0]["Fk_MemId"]);
                        }
                        else
                        {
                            int returnID = Convert.ToInt32(dtCheck.Rows[0]["Fk_MemId"]);
                        }
                    }

                    foreach (DataRow dr in dtCheck.Rows)
                    {
                        list.Add(string.Format("{0}.{1}", loginId, dr["Fk_MemId"]));
                    }

                }
            }
            else
            {
                list.Add(string.Format("{0}.{1}", "Please Login First", "0"));
            }
            return list;
        }
        public ActionResult DownloadExcelForBank()
        {
            try
            {
                Transactions transactions = new Transactions();
                DataSet dataSet = transactions.GetAssociateBankDetails();
                transactions.dtDetails = dataSet.Tables[0];
                DataTable dt = transactions.dtDetails;
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BankDetailsForPayout.xlsx");
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public ActionResult GetStockDetails(string Fk_VarientId)
        {
            CreateOrder createOrder = new CreateOrder();
            createOrder.Fk_ProductId = int.Parse(Fk_VarientId);

            createOrder.FranchiseId = HttpContext.Session.GetString("FranchiseId");
            List<CreateOrder> lst = new List<CreateOrder>();
            DataSet dataSet = createOrder.GetStockDetails();
            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
            {
                CreateOrder create = new CreateOrder();
                create.OrderNo = dataSet.Tables[0].Rows[i]["OrderNo"].ToString();
                create.LoginId = dataSet.Tables[0].Rows[i]["LoginId"].ToString();
                create.CompanyName = dataSet.Tables[0].Rows[i]["ContactPerson"].ToString();
                create.OrderDate = dataSet.Tables[0].Rows[i]["OrderDate"].ToString();
                create.Credit = decimal.Parse(dataSet.Tables[0].Rows[i]["Credit"].ToString());
                create.Debit = decimal.Parse(dataSet.Tables[0].Rows[i]["Debit"].ToString());
                lst.Add(create);
            }

            return Json(lst);
        }
        public ActionResult TermsCondition()
        {
            string CustomerId = "0";
            CartSide Cart = null;
            Cart = CartSide.GetCartData(CustomerId);
            HttpContext.Session.SetObjectAsJson("_Cart", Cart);
            if (Cart.lstCartList == null)
            {
                HttpContext.Session.SetString("Count", "0");
            }
            else
            {
                HttpContext.Session.SetString("Count", Cart.lstCartList.Count.ToString());
            }
            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            string CustomerId = "0";
            CartSide Cart = null;
            Cart = CartSide.GetCartData(CustomerId);
            HttpContext.Session.SetObjectAsJson("_Cart", Cart);
            if (Cart.lstCartList == null)
            {
                HttpContext.Session.SetString("Count", "0");
            }
            else
            {
                HttpContext.Session.SetString("Count", Cart.lstCartList.Count.ToString());
            }
            return View();
        }
        public ActionResult PaytmResponse(PaytmResponse data, string Id)
        {
            PlaceOrder placeOrder = new PlaceOrder();
            if (Id == "CustomerOrder")
            {
                string Body = "";
                string obj1 = "{\"Status\":\"" + data.STATUS + "\",\"OrderNo\":\"" + data.ORDERID + "\",\"TxnId\":\"" + data.TXNID + "\",\"BankTxnId\":\"" + data.BANKTXNID + "\",\"CheckSumHash\":\"" + data.CHECKSUMHASH + "\",\"RespMsg\":\"" + data.RESPMSG + "\",\"OpCode\":\"1\"}";
                Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                string result = Common.HITAPI(APIURL.PlaceOrder, Body);
                PlaceOrderResponse placeOrderResponse = new PlaceOrderResponse();
                ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
                placeOrderResponse = JsonConvert.DeserializeObject<PlaceOrderResponse>(dcdata);
                if (placeOrderResponse.Status == 1)
                {
                    return RedirectToAction("OrderList", "Associate");
                }
                else
                {
                    TempData["Placeorder"] = placeOrderResponse.Message;
                }
                placeOrder.LoginId = HttpContext.Session.GetString("LoginId");
                CartSide Cart = null;
                Cart = CartSide.GetCartData(HttpContext.Session.GetString("CustomerId") == null ? "0" : HttpContext.Session.GetString("CustomerId"));
                if (Cart.lstCartList == null)
                {
                    HttpContext.Session.SetString("Count", "0");
                }
                else
                {
                    HttpContext.Session.SetString("Count", Cart.lstCartList.Count.ToString());
                }
                HttpContext.Session.SetObjectAsJson("_Cart", Cart);
                return View(placeOrder);
            }
            else if (Id == "FranchiseOrder")
            {

                CreateOrder createOrder = new CreateOrder();
                createOrder.OpCode = 1;
                createOrder.Status = data.STATUS;
                createOrder.OrderNo = data.ORDERID;
                createOrder.TxnId = data.TXNID;
                createOrder.BankTxnId = data.BANKTXNID;
                createOrder.CheckSumHash = data.CHECKSUMHASH;
                createOrder.RespMsg = data.RESPMSG;
                DataSet dataSet = createOrder.GenerateFranchiseOrder();
                return RedirectToAction("ViewOrders", "Franchisee");

            }
            else if (Id == "FwalletRequest")
            {
                FranchiseWallet karyonPointsRequest = new FranchiseWallet();
                karyonPointsRequest.Status = data.STATUS;
                karyonPointsRequest.TransactionNo = data.ORDERID;
                karyonPointsRequest.TXNID = data.TXNID;
                karyonPointsRequest.BANKTXNID = data.BANKTXNID;
                karyonPointsRequest.CHECKSUMHASH = data.CHECKSUMHASH;
                karyonPointsRequest.RespMsg = data.RESPMSG;
                karyonPointsRequest.OpCode = 2;
                DataSet dataSet = karyonPointsRequest.FranchisewalletRequest();
                return RedirectToAction("WalletRequest", "Franchisee");
            }
            else
            {
                KaryonPointsRequest karyonPointsRequest = new KaryonPointsRequest();
                karyonPointsRequest.Status = data.STATUS;
                karyonPointsRequest.TransactionNo = data.ORDERID;
                karyonPointsRequest.TXNID = data.TXNID;
                karyonPointsRequest.BANKTXNID = data.BANKTXNID;
                karyonPointsRequest.CHECKSUMHASH = data.CHECKSUMHASH;
                karyonPointsRequest.RespMsg = data.RESPMSG;
                karyonPointsRequest.OpCode = 2;
                DataSet dataSet = karyonPointsRequest.RequestKaryonPoints();
                return RedirectToAction("KaryonPointsList", "Associate");
            }


        }
        public ActionResult CancellationPolicy()
        {
            return View();
        }
        public ActionResult LoginFromAdminPanel(string LoginId, string MobileNo, string Password)
        {
            //Password = Password.Replace("_", "#");
            string obj1 = "{\"LoginId\":\"" + LoginId + "\",\"MobileNo\":\"" + MobileNo + "\",\"Password\":\"" + Password + "\",\"LoginFrom\":\"WEB\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.LoginByAdmin, Body);
            LoginResponse loginResponse = new LoginResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            loginResponse = JsonConvert.DeserializeObject<LoginResponse>(dcdata);
            if (loginResponse.Status == 0)
            {
                HttpContext.Session.SetString("CustomerId", "0");
                HttpContext.Session.SetString("FranchiseId", "0");
                TempData["AutoLogin"] = loginResponse.Message;
            }
            else
            {
                HttpContext.Session.SetString("FirstName", loginResponse.Response.FirstName);
                HttpContext.Session.SetString("LastName", loginResponse.Response.LastName);
                if (loginResponse.Response.FK_FranchiseTypeId > 0)
                {
                    HttpContext.Session.SetString("FranchiseId", loginResponse.Response.CustomerId.ToString());
                }
                else
                {
                    HttpContext.Session.SetString("CustomerId", loginResponse.Response.CustomerId.ToString());
                }

                HttpContext.Session.SetString("LoginId", loginResponse.Response.LoginId);
                HttpContext.Session.SetString("RegistrationAmount", loginResponse.Response.RegistrationAmount.ToString());
                HttpContext.Session.SetString("FK_FranchiseTypeId", loginResponse.Response.FK_FranchiseTypeId.ToString());
                Response.Cookies.Append("LoginId", Crypto.Encrypt(loginResponse.Response.LoginId));
                //Response.Cookies.Append("Password", Crypto.Encrypt(Password));
                Response.Cookies.Append("UserType", loginResponse.Response.UserType);
                if (loginResponse.Response.UserType == "Associate")
                {
                    AdminRenderMenu(2);
                    return RedirectToAction("NewDashBoard", "Home");
                }
                else
                {
                    AdminRenderMenu(1);
                    return RedirectToAction("DashBoard", "Franchisee");
                }
            }
            return View();
        }
        public ActionResult AddressList(AddressRequest addressRequest,string Value)
        {
            string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetAddress, Body);
            AddressResponse addressResponse = new AddressResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            addressResponse = JsonConvert.DeserializeObject<AddressResponse>(dcdata);
            if (addressResponse.Status == 1)
            {
                addressRequest.AddressList = addressResponse.Response.AddressList;
            }
            obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            result = Common.HITAPI(APIURL.AssociateWalletBalance, Body);
            WalletBalanceResponse walletBalance = new WalletBalanceResponse();
            deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            walletBalance = JsonConvert.DeserializeObject<WalletBalanceResponse>(dcdata);
            ViewBag.FUPWallet = walletBalance.Response.WalletBalanceData.FUPWallet;
            ViewBag.KaryonBalance = walletBalance.Response.WalletBalanceData.Balance;
            if (Value != null)
            {
                HttpContext.Session.SetString("Amount", Value);
                addressRequest.Amount = Value;
            }
            addressRequest.LoginId = HttpContext.Session.GetString("LoginId");
            return View(addressRequest);
        }
        public ActionResult GenerateQRCODE(string Amount,string Type, string LoginId, string merchantTranId, string Pk_AddressId)
        {
            List<CreateOrder> lst = new List<CreateOrder>();
            CreateOrder createOrder = new CreateOrder();
           //string Amount=HttpContext.Session.GetString("Amount");
            if (string.IsNullOrEmpty(Amount))
            {
                createOrder.QRCODEIMG = "";
            }
            else
            {
                merchantTranId = DateTime.Now.ToString("ddMMyyyyhhMMss");
                var result = "";
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://bankapi.karyon.organic/api/banktrans/GenerateQRCode");
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";
                    using (var streamWriter = new

                    StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = new JavaScriptSerializer().Serialize(new
                        {
                            Amount = Amount,
                            Type = Type,
                            LoginId = LoginId,
                            merchantTranId = merchantTranId,
                            Pk_AddressId = Pk_AddressId

                        });

                        streamWriter.Write(json);
                    }
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        result = streamReader.ReadToEnd();
                    }

                    string Qrcode = result.ToString().Remove(0, 1);
                    int length = Qrcode.Length - 1;
                    Qrcode = Qrcode.Remove(length, 1);

                    createOrder.QRCODEIMG = Qrcode;
                    createOrder.merchantTranId = merchantTranId;
                    HttpContext.Session.SetString("Type", Type);
                    HttpContext.Session.SetString("PaymentType", "QrCode");

                }
                catch (Exception ex)
                {

                    createOrder.QRCODEIMG = ex.Message;
                }
            }

            lst.Add(createOrder);
            return Json(lst);
        }
        public class TransactionResponse
        {
            public string response { get; set; }
            public string merchantId { get; set; }
            public string subMerchantId { get; set; }
            public string terminalId { get; set; }
            public string success { get; set; }
            public string message { get; set; }
            public string merchantTranId { get; set; }
            public string BankRRN { get; set; }
        }
        public ActionResult PayUPIID(string PayerVPA,string Amount, string Type, string LoginId, string merchantTranId, string Pk_AddressId)
        {
            List<TransactionResponse> lst = new List<TransactionResponse>();
            CreateOrder createOrder = new CreateOrder();
            TransactionResponse transaction=new TransactionResponse();
            //string Amount=HttpContext.Session.GetString("Amount");
            if (string.IsNullOrEmpty(Amount))
            {
                createOrder.QRCODEIMG = "";
            }
            else
            {
                long transactionNumber = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                string collectByDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm tt");

                merchantTranId = DateTime.Now.Ticks.ToString();
                var result = "";
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://bankapi.karyon.organic/api/banktrans/CollectPay2");
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";
                    using (var streamWriter = new

                    StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = new JavaScriptSerializer().Serialize(new
                        {
                            PayerVPA = PayerVPA,
                            Amount = Amount,
                            collectByDate = collectByDate,
                            merchantTranId = merchantTranId,
                            Pk_AddressId = Pk_AddressId,
                            Type= Type,
                            LoginId=LoginId,


                        });

                        streamWriter.Write(json);
                    }
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        result = streamReader.ReadToEnd();
                    }

                  

                    transaction = JsonConvert.DeserializeObject<TransactionResponse>(result);
                    HttpContext.Session.SetString("VPA", PayerVPA);
                    HttpContext.Session.SetString("Type", Type);
                    HttpContext.Session.SetString("PaymentType", "UPIId");



                }
                catch (Exception ex)
                {

                    createOrder.QRCODEIMG = ex.Message;
                }
            }

            return Json(transaction);
        }
        public ActionResult Getmessage()
        {
            string CustomerId = "0";
            CartSide Cart = null;
            Cart = CartSide.GetCartData(CustomerId);
            HttpContext.Session.SetObjectAsJson("_Cart", Cart);
            if (Cart.lstCartList == null)
            {
                HttpContext.Session.SetString("Count", "0");
            }
            else
            {
                HttpContext.Session.SetString("Count", Cart.lstCartList.Count.ToString());
            }
            return View();
        }
        public ActionResult GetTeamGraph()
        {
            List<DashBoardRequest> dataList = new List<DashBoardRequest>();
            DashBoardRequest dashBoardRequest = new DashBoardRequest();
            dashBoardRequest.CustomerId = int.Parse(HttpContext.Session.GetString("CustomerId").ToString());
            DataSet dataSet = dashBoardRequest.GetAssociateDashBoard();
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dataSet.Tables[0].Rows)
                {
                    DashBoardRequest details = new DashBoardRequest();
                    details.TotalMemberR = int.Parse(dr["TotalMemberR"].ToString());
                    details.TotalActiveMemberR = int.Parse(dr["TotalActiveMemberR"].ToString());
                    details.TotalInactiveMemberR = int.Parse(dr["TotalInactiveMemberR"].ToString());
                    details.TotalMember = int.Parse(dr["TotalMember"].ToString());
                    details.TotalActiveMember = int.Parse(dr["TotalActiveMember"].ToString());
                    details.TotalInactiveMember = int.Parse(dr["TotalInactiveMember"].ToString());
                    details.LeftBVFresh = decimal.Parse(dr["LeftBVFresh"].ToString());
                    details.RightBVFresh = decimal.Parse(dr["RightBVFresh"].ToString());
                    details.SponsorLeftBVFresh = decimal.Parse(dr["SponsorLeftBVFresh"].ToString());
                    details.SponsorRightBVFresh = decimal.Parse(dr["SponsorRightBVFresh"].ToString());
                    dataList.Add(details);
                }
            }
            return Json(dataList);
        }
        public async Task<ActionResult> ReviewAboutus(ContactUs contactUs, string Save)
        {
            string CustomerId = "0";
            CartSide Cart = null;
            Cart = CartSide.GetCartData(CustomerId);
            HttpContext.Session.SetObjectAsJson("_Cart", Cart);
            if (Cart.lstCartList == null)
            {
                HttpContext.Session.SetString("Count", "0");
            }
            else
            {
                HttpContext.Session.SetString("Count", Cart.lstCartList.Count.ToString());
            }
            if (!string.IsNullOrEmpty(contactUs.Email))
            {
                //string sendmail = Common.SendMail(contactUs.Email, contactUs.Name);
                //if (sendmail == "Mail Sent Successfully!")
                //{
                string fileName = await FileManagement.WriteFiles(contactUs.ProfileImage, "FeedbackUserImage");
                contactUs.ProfilePic = fileName;
                string obj1 = "{\"Name\":\"" + contactUs.Name + "\",\"Star\":\"" + contactUs.Star + "\",\"Message\":\"" + contactUs.Message + "\",\"Email\":\"" + contactUs.Email + "\",\"ProfilePic\":\"" + contactUs.ProfilePic + "\"}";
                string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                string result = Common.HITAPI(APIURL.FeedBack, Body);
                ContactUsResponse contactUsResponse = new ContactUsResponse();
                ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
                contactUsResponse = JsonConvert.DeserializeObject<ContactUsResponse>(dcdata);
                TempData["Feedback"] = contactUsResponse.Message;
                //}

            }

            return View(contactUs);
        }
        public ActionResult GetSiteFeedback(ContactUs contactUs)
        {


            contactUs.OpCode = 3;
            DataSet dataSet = contactUs.GetFeedback();
            List<ContactUs> lst = new List<ContactUs>();
            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
            {
                ContactUs contact = new ContactUs();
                contact.Name = dataSet.Tables[0].Rows[i]["Name"].ToString();
                contact.Email = dataSet.Tables[0].Rows[i]["Email"].ToString();
                contact.ProfilePic = dataSet.Tables[0].Rows[i]["ProfilePic"].ToString();
                contact.Message = dataSet.Tables[0].Rows[i]["Message"].ToString();
                contact.Star = int.Parse(dataSet.Tables[0].Rows[i]["Star"].ToString());

                lst.Add(contact);
            }
            return Json(lst);
        }
       
        public ActionResult GetMemberdetails(string LoginId)
        {
            string Body = "";
            string obj1 = "{\"SponsorLoginId\":\"" + LoginId + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetSponsorName, Body);
            SponsorResponse sponsorResponse = new SponsorResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            sponsorResponse = JsonConvert.DeserializeObject<SponsorResponse>(dcdata);

            return Json(sponsorResponse);
        }
        public ActionResult GetWalletBalance(string LoginId)
        {
            #region WalletBalance
            string obj1 = "{\"LoginId\":\"" + LoginId + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.AssociateWalletBalance, Body);
            WalletBalanceResponse walletBalance = new WalletBalanceResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            walletBalance = JsonConvert.DeserializeObject<WalletBalanceResponse>(dcdata);
            //ViewBag.FUPWallet = walletBalance.Response.WalletBalanceData.FUPWallet;
            //ViewBag.KaryonWallet = walletBalance.Response.WalletBalanceData.Balance;

            #endregion WalletBalance
            return Json(walletBalance);
        }
        public ActionResult FranchiseeRequest(FranchiseRegistrationRequest registrationRequest)
        {
            //#region BindPaymentMode
            //string obj1 = "{\"OpCode\":\"" + 13 + "\"}";
            //string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            //string result = Common.HITAPI(APIURL.GetFranchiseType, Body);

            //MasterDataResponse masterDataResponse = new MasterDataResponse();
            //ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            //string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            //masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            //if (masterDataResponse.Status == 1)
            //{
            //    DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);
            //    List<SelectListItem> ddlFranchiseType = new List<SelectListItem>();
            //    ddlFranchiseType.Add(new SelectListItem { Text = "Payment Mode", Value = "0" });
            //    if (dataTable != null && dataTable.Rows.Count > 0)
            //    {
            //        foreach (DataRow r in dataTable.Rows)
            //        {
            //            ddlFranchiseType.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_Id"].ToString() });
            //        }
            //    }
            //    ViewBag.ddlFranchiseType = ddlFranchiseType;
            //}
            //#endregion
            return View(registrationRequest);
        }
        public ActionResult GetAddressDetails(string LoginId)
        {
            OpenOrderGetAddressResponse addressResponse = new OpenOrderGetAddressResponse();
            if (!string.IsNullOrEmpty(LoginId))
            {
                string obj1 = "{\"LoginId\":\"" + LoginId + "\"}";
                string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                string result = Common.HITAPI(APIURL.GetAssociateAddressDetails, Body);
                ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
                addressResponse = JsonConvert.DeserializeObject<OpenOrderGetAddressResponse>(dcdata);
            }
            return Json(addressResponse);
        }
        public ActionResult GetOrderAmount()
        {
            CreateOrder createOrder = new CreateOrder();
            createOrder.Fk_MemId = HttpContext.Session.GetString("CustomerId").ToString();
            DataSet dataSet = createOrder.GetCartAmount();
            createOrder.Amount = decimal.Parse(dataSet.Tables[0].Rows[0]["CartAmount"].ToString());
            return Json(createOrder.Amount);
        }

        public ActionResult ApplyPromoCode(string PromoCode, string Amount)
        {
            PromoCodeResponse promoCode = new PromoCodeResponse();
            if (!string.IsNullOrEmpty(PromoCode))
            {
                string obj1 = "{\"Fk_MemId\":\"" + HttpContext.Session.GetString("CustomerId").ToString() + "\",\"Amount\":\"" + Amount + "\",\"PromoCode\":\"" + PromoCode + "\"}";
                string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                string result = Common.HITAPI(APIURL.ApplyPromoCode, Body);
                ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
                promoCode = JsonConvert.DeserializeObject<PromoCodeResponse>(dcdata);
            }
            return Json(promoCode);
        }

        
        public ActionResult AdminRenderMenu(int OpCode)
        {
            Menu model = new Menu();
            MenuDataResp objs = new MenuDataResp();
            List<Menu> list = new List<Menu>();
            List<Menu> list1 = new List<Menu>();
            DataSet dataset = new DataSet();
            if(OpCode == 1)
            {
                model.Fk_MemId = HttpContext.Session.GetString("FK_FranchiseTypeId");
                model.OpCode = 1;
            }
            else
            {
                model.Fk_MemId = HttpContext.Session.GetString("CustomerId");
                model.OpCode = 2;
            }
            MenuDataResp objresponse = new MenuDataResp();
            string obj = "{\"Fk_MemId\":\"" + model.Fk_MemId + "\",\"OpCode\":\"" + model.OpCode + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj);
            string result = Common.HITAPI(APIURL.AdminRenderMenu, Body);
            ShoppingWebResponse deserialized = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserialized.body);
            objresponse = JsonConvert.DeserializeObject<MenuDataResp>(dcdata);
            if (objresponse != null)
            {
                if (objresponse.Status == "1")
                {
                    DataTable dataTable = Common.ToDataTable(objresponse.responselist.menuList);
                    DataTable dataTable1 = Common.ToDataTable(objresponse.responselist.subMenuList);
                    objs.dtDetails = dataTable;
                    objs.dtDetails1 = dataTable1;
                    for (var i = 0; i < objs.dtDetails.Rows.Count; i++)
                    {
                        Menu ListData = new Menu();
                        ListData.MenuName = objs.dtDetails.Rows[i]["MenuName"].ToString();
                        ListData.MenuId = objs.dtDetails.Rows[i]["MenuId"].ToString();
                        ListData.Url = objs.dtDetails.Rows[i]["Url"].ToString();
                        ListData.Class = objs.dtDetails.Rows[i]["Class"].ToString();
                        ListData.IsDropdown = objs.dtDetails.Rows[i]["Isdropdown"].ToString();
                        ListData.Icon = objs.dtDetails.Rows[i]["Icon"].ToString();
                        list.Add(ListData);
                    }
                    for (int j = 0; j <= objs.dtDetails1.Rows.Count - 1; j++)
                    {
                        Menu SubList = new Menu();
                        SubList.SubMenuName = objs.dtDetails1.Rows[j]["SubMenuName"].ToString();
                        //SubList.MenuName = dataset.Tables[1].Rows[j]["MenuName"].ToString();
                        SubList.Fk_MenuId = objs.dtDetails1.Rows[j]["Fk_MenuId"].ToString();
                        SubList.SubMenuId = int.Parse(objs.dtDetails1.Rows[j]["SubMenuId"].ToString());
                        SubList.Url = objs.dtDetails1.Rows[j]["Url"].ToString();
                        SubList.Icon = objs.dtDetails1.Rows[j]["SubMenuIcon"].ToString();
                        list1.Add(SubList);
                    }
                }
                //else
                //{
                //    model.Fk_MemId = HttpContext.Session.GetString("CustomerId");
                //}
                //model.Fk_FranchiseId = HttpContext.Session.GetString("FranchiseId");
                //model.OpCode = OpCode;
                //dataset = model.GetMenuDetails();
                //if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
                //{
               
                   

              //  }
                //model.menuList = list;
                //model.subMenuList = list1;
                //HttpContext.Session.SetObjectAsJson("_menu", model);
                //HttpContext.Session.SetObjectAsJson("_submenu", model);
            }
            model.menuList = list;
            model.subMenuList = list1;
            HttpContext.Session.SetObjectAsJson("_menu", model);
            HttpContext.Session.SetObjectAsJson("_submenu", model);
            return PartialView("_FranchaiseePartialLayout");
        }

        public ActionResult CheckTermsCondition(string MobileNo, string LoginType)
        {
            Common common = new Common();
            common.MobileNo = MobileNo;
            common.LoginType = LoginType;
            DataSet dataSet = common.GetTermsLog();
            common.Flag = dataSet.Tables[0].Rows[0]["Flag"].ToString();
            return Json(common);
        }
        public ActionResult GetOrderDetails(string OrderNo)
        {
            CreateOrder createOrder = new CreateOrder();
            List<CreateOrder> lst = new List<CreateOrder>();
            createOrder.OrderNo = OrderNo;
            DataSet dataSet = createOrder.GetOrderDetails();
            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
            {
                CreateOrder create = new CreateOrder();
                create.OrderNo = dataSet.Tables[0].Rows[i]["OrderNo"].ToString();
                create.ProductName = dataSet.Tables[0].Rows[i]["ProductName"].ToString();
                create.OfferedPrice = dataSet.Tables[0].Rows[i]["OfferedPrice"].ToString();
                create.SGST = dataSet.Tables[0].Rows[i]["SGST"].ToString();
                create.CGST = dataSet.Tables[0].Rows[i]["CGST"].ToString();
                create.IGST = dataSet.Tables[0].Rows[i]["IGST"].ToString();
                create.Amount = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalAmount"].ToString());
                lst.Add(create);
            }

            return Json(lst);
        }
        public ActionResult GetCustomerAddress(string LoginId)
        {
            try
            {
                OpenOrderGetAddressResponse addressResponse = new OpenOrderGetAddressResponse();
                if (!string.IsNullOrEmpty(LoginId))
                {
                    string obj1 = "{\"LoginId\":\"" + LoginId + "\"}";
                    string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                    string result = Common.HITAPI(APIURL.GetAssociateAddress, Body);
                    ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                    string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
                    addressResponse = JsonConvert.DeserializeObject<OpenOrderGetAddressResponse>(dcdata);
                }
                return Json(addressResponse);
            }
            catch (Exception ex) { throw ex; }
        }
        public ActionResult GetMemberAddressDetails(string LoginId)
        {
            OpenOrderGetAddressResponse addressResponse = new OpenOrderGetAddressResponse();
            if (!string.IsNullOrEmpty(LoginId))
            {
                string obj1 = "{\"LoginId\":\"" + LoginId + "\"}";
                string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                string result = Common.HITAPI(APIURL.GetAssociateAddress, Body);
                ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
                addressResponse = JsonConvert.DeserializeObject<OpenOrderGetAddressResponse>(dcdata);
            }
            return Json(addressResponse);
        }
        public ActionResult NewDashBoard()
        {
            HttpContext.Session.SetString("SearchLoginId", HttpContext.Session.GetString("CustomerId"));
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("CustomerId")))
            {

                return RedirectToAction("Login");

            }
            if (DateTime.Now.Hour < 12)
			{
				ViewBag.WelcomeMessage = "Good Morning";
				
			}
			else if (DateTime.Now.Hour < 17)
			{
				ViewBag.WelcomeMessage = "Good Afternoon";
				
			}
			else
			{
				ViewBag.WelcomeMessage = "Good Evening";
			}
			string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetAssociateDashBoard2, Body);
            AssociateDashBoard2ResponseWeb associateDashBoardResponse = new AssociateDashBoard2ResponseWeb();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            associateDashBoardResponse = JsonConvert.DeserializeObject<AssociateDashBoard2ResponseWeb>(dcdata);
            return View(associateDashBoardResponse);
        }


        public ActionResult GenerateEinvice(string OrderNo)
        {
            CreateOrder createOrder=new CreateOrder();
            createOrder.OrderNo = OrderNo;
            DataSet dataSet12 = createOrder.GetEinvoiceData();
            #region HITEinvoiceAPI
            GenerateEinvoice generateEinvoice = new GenerateEinvoice();
            generateEinvoice.FK_MemId = dataSet12.Tables[0].Rows[0]["Fk_MemId"].ToString();
            generateEinvoice.user_gstin = CommonEwayBIll.userGstin;
            generateEinvoice.data_source = "erp";
            #region TransactionDetails
            TransactionDetails transactionDetails = new TransactionDetails();
            transactionDetails.supply_type = "B2B";
            transactionDetails.charge_type = "N";
            transactionDetails.igst_on_intra = "N";
            transactionDetails.ecommerce_gstin = "";
            generateEinvoice.transaction_details = transactionDetails;
            #endregion TransactionDetails
            #region document_details
            DocumentDetails documentDetails = new DocumentDetails();
            documentDetails.document_type = "INV";
            documentDetails.document_number = dataSet12.Tables[0].Rows[0]["OrderNo"].ToString();
            documentDetails.document_date = dataSet12.Tables[0].Rows[0]["OrderDate"].ToString();
            generateEinvoice.document_details = documentDetails;
            #endregion document_details
            #region seller_details
            SellerDetails sellerDetails = new SellerDetails();
            sellerDetails.gstin = CommonEwayBIll.userGstin;
            sellerDetails.legal_name = CommonEwayBIll.legal_name_of_consignor;
            sellerDetails.trade_name = CommonEwayBIll.legal_name_of_consignor;
            sellerDetails.address1 = CommonEwayBIll.address1_of_consignor;
            sellerDetails.address2 = CommonEwayBIll.address2_of_consignor;
            sellerDetails.location = CommonEwayBIll.place_of_consignor;
            sellerDetails.pincode = CommonEwayBIll.pincode_of_consignor;
            sellerDetails.state_code = CommonEwayBIll.state_of_consignor;
            sellerDetails.phone_number = CommonEwayBIll.phone_number;
            sellerDetails.email = CommonEwayBIll.email;
            generateEinvoice.seller_details = sellerDetails;
            #endregion seller_details
            #region buyer_details
            BuyerDetails buyerDetails = new BuyerDetails();
            buyerDetails.gstin = dataSet12.Tables[0].Rows[0]["GstNo"].ToString();
            buyerDetails.phone_number = long.Parse(dataSet12.Tables[0].Rows[0]["MobileNo"].ToString());
            buyerDetails.email = "";
            buyerDetails.place_of_supply = dataSet12.Tables[0].Rows[0]["PlaceOfSupply"].ToString();
            buyerDetails.address1 = dataSet12.Tables[0].Rows[0]["Address"].ToString();
            buyerDetails.address2 = dataSet12.Tables[0].Rows[0]["Address1"].ToString();
            buyerDetails.location = dataSet12.Tables[0].Rows[0]["Location"].ToString();
            buyerDetails.pincode = int.Parse(dataSet12.Tables[0].Rows[0]["Pincode"].ToString());
            buyerDetails.state_code = dataSet12.Tables[0].Rows[0]["PlaceOfSupply"].ToString();
            generateEinvoice.buyer_details = buyerDetails;
            #endregion buyer_details
            #region dispatch_details
            DispatchDetails dispatchDetails = new DispatchDetails();
            dispatchDetails.company_name = CommonEwayBIll.legal_name_of_consignor;
            dispatchDetails.address1 = dataSet12.Tables[0].Rows[0]["FAddress"].ToString();
            dispatchDetails.address2 = "";
            dispatchDetails.location = dataSet12.Tables[0].Rows[0]["FLocation"].ToString();
            dispatchDetails.pincode = int.Parse(dataSet12.Tables[0].Rows[0]["FPincode"].ToString());
            dispatchDetails.state_code = dataSet12.Tables[0].Rows[0]["FState"].ToString();
            generateEinvoice.dispatch_details = dispatchDetails;
            #endregion dispatch_details
            #region ship_details
            ShipDetails shipDetails = new ShipDetails();
            shipDetails.gstin = dataSet12.Tables[0].Rows[0]["GstNo"].ToString();
            shipDetails.address1 = dataSet12.Tables[0].Rows[0]["Address"].ToString();
            shipDetails.address2 = dataSet12.Tables[0].Rows[0]["Address1"].ToString();
            shipDetails.location = dataSet12.Tables[0].Rows[0]["Location"].ToString();
            shipDetails.pincode = int.Parse(dataSet12.Tables[0].Rows[0]["Pincode"].ToString());
            shipDetails.state_code = dataSet12.Tables[0].Rows[0]["PlaceOfSupply"].ToString();
            generateEinvoice.ship_details = shipDetails;
            #endregion ship_details

            decimal TotalGross = 0, TotalCGST = 0, TotalSGST = 0, AmountAfterTax = 0, TotalIGST = 0;
            for (int i = 0; i <= dataSet12.Tables[1].Rows.Count - 1; i++)
            {

                TotalGross = TotalGross + decimal.Parse(dataSet12.Tables[1].Rows[i]["Gross"].ToString());
                if (dataSet12.Tables[0].Rows[0]["PlaceOfSupply"].ToString() == "MAHARASHTRA")
                {
                    TotalCGST = TotalCGST + decimal.Parse(dataSet12.Tables[1].Rows[i]["CGSTAmt"].ToString());
                    TotalSGST = TotalSGST + decimal.Parse(dataSet12.Tables[1].Rows[i]["SGSTAmt"].ToString());
                    TotalIGST = 0;
                    AmountAfterTax = TotalGross + TotalCGST + TotalSGST;
                }
                else
                {
                    TotalCGST = 0;
                    TotalSGST = 0;
                    TotalIGST = TotalIGST + decimal.Parse(dataSet12.Tables[1].Rows[i]["IGSTAmt"].ToString());
                    AmountAfterTax = TotalGross + TotalIGST;
                }
            }

            #region value_details
            ValueDetails valueDetails = new ValueDetails();
            valueDetails.total_assessable_value = TotalGross;
            valueDetails.total_cgst_value = TotalCGST;
            valueDetails.total_sgst_value = TotalSGST;
            valueDetails.total_igst_value = TotalIGST;
            valueDetails.total_cess_value = 0;
            valueDetails.total_cess_value_of_state = 0;
            valueDetails.total_discount = 0;
            valueDetails.total_other_charge = 0;
            valueDetails.round_off_amount = 0;
            valueDetails.total_invoice_value_additional_currency = 0;
            valueDetails.total_invoice_value = AmountAfterTax;
            generateEinvoice.value_details = valueDetails;
            #endregion value_details

            List<EItemList> lstItems = new List<EItemList>();
            int k = 1;
            #region item_list
            for (int j = 0; j <= dataSet12.Tables[1].Rows.Count - 1; j++)
            {
                TotalCGST = 0; TotalSGST = 0; AmountAfterTax = 0; TotalIGST = 0;
                EItemList eItemList = new EItemList();
                eItemList.item_serial_number = k.ToString();
                eItemList.product_description = dataSet12.Tables[1].Rows[j]["ProductName"].ToString();
                eItemList.is_service = "N";
                eItemList.hsn_code = dataSet12.Tables[1].Rows[j]["HSNCode"].ToString();
                eItemList.bar_code = "";
                eItemList.quantity = int.Parse(dataSet12.Tables[1].Rows[j]["DispatchQty"].ToString());
                eItemList.unit = dataSet12.Tables[1].Rows[j]["UnitName"].ToString();
                eItemList.unit_price = decimal.Parse(dataSet12.Tables[1].Rows[j]["Rate"].ToString());
                eItemList.total_amount = eItemList.unit_price * eItemList.quantity;

                eItemList.pre_tax_value = decimal.Parse(dataSet12.Tables[1].Rows[j]["Rate"].ToString());
                eItemList.assessable_value = eItemList.total_amount;
                TotalGross = decimal.Parse(dataSet12.Tables[1].Rows[j]["Gross"].ToString());
                if (dataSet12.Tables[0].Rows[0]["PlaceOfSupply"].ToString() == "MAHARASHTRA")
                {
                    TotalCGST = decimal.Parse(dataSet12.Tables[1].Rows[j]["CGSTAmt"].ToString());
                    TotalSGST = decimal.Parse(dataSet12.Tables[1].Rows[j]["SGSTAmt"].ToString());
                    TotalIGST = 0;
                    AmountAfterTax = TotalGross + TotalCGST + TotalSGST;
                    eItemList.gst_rate = decimal.Parse(dataSet12.Tables[1].Rows[j]["IGSTPerc"].ToString());
                    eItemList.cgst_amount = decimal.Parse(dataSet12.Tables[1].Rows[j]["CGSTAmt"].ToString());
                    eItemList.sgst_amount = decimal.Parse(dataSet12.Tables[1].Rows[j]["SGSTAmt"].ToString());
                    eItemList.igst_amount = 0;
                    eItemList.total_item_value = AmountAfterTax;
                }
                else
                {
                    TotalCGST = 0;
                    TotalSGST = 0;
                    TotalIGST = decimal.Parse(dataSet12.Tables[1].Rows[j]["IGSTAmt"].ToString());
                    AmountAfterTax = TotalGross + TotalIGST;
                    eItemList.gst_rate = decimal.Parse(dataSet12.Tables[1].Rows[j]["IGSTPerc"].ToString());
                    eItemList.cgst_amount = 0;
                    eItemList.sgst_amount = 0;
                    eItemList.igst_amount = decimal.Parse(dataSet12.Tables[1].Rows[j]["IGSTAmt"].ToString());
                    eItemList.total_item_value = AmountAfterTax;
                }



                eItemList.free_quantity = 0;
                eItemList.discount = 0;
                eItemList.other_charge = 0;
                eItemList.cess_rate = 0;
                eItemList.cess_amount = 0;
                eItemList.cess_nonadvol_amount = 0;
                eItemList.state_cess_rate = 0;
                eItemList.state_cess_amount = 0;
                eItemList.state_cess_nonadvol_amount = 0;
                eItemList.country_origin = "";
                eItemList.order_line_reference = "";
                eItemList.product_serial_number = "";
                k++;
                lstItems.Add(eItemList);
            }
            #endregion item_list

            generateEinvoice.item_list = lstItems;
            string url = "https://karyon.organic/api/karyon/GenerateEinvoice";
            string result1 = Common.HITEWAYAPI(url, JsonConvert.SerializeObject(generateEinvoice));


            #endregion HITEinvoiceAPI

            return null;
        }

        public ActionResult RedundAmount()
        {
            //string url = "https://securegw.paytm.in/refund/apply";
			Dictionary<string, string> body = new Dictionary<string, string>();
			Dictionary<string, string> head = new Dictionary<string, string>();
			Dictionary<string, Dictionary<string, string>> requestBody = new Dictionary<string, Dictionary<string, string>>();

			body.Add("mid", Gateway.MID);
			body.Add("txnType", "REFUND");
			body.Add("orderId", "24052024110513");
			body.Add("txnId", "20240524210320000000723085735401228");
			body.Add("refId", DateTime.Now.ToString("ddMMyyyyhhmmss"));
			body.Add("refundAmount", "43320.00");

			/*
			* Generate checksum by parameters we have in body
			* Find your Merchant Key in your Paytm Dashboard at https://dashboard.paytm.com/next/apikeys 
			*/
			string paytmChecksum = Checksum.generateSignature(JsonConvert.SerializeObject(body), Gateway.MerchantId);

			head.Add("signature", paytmChecksum);

			requestBody.Add("body", body);
			requestBody.Add("head", head);

			string post_data = JsonConvert.SerializeObject(requestBody);

			//For  Staging
			//string url = "https://securegw.paytm.in/refund/apply";

			//For  Production 
			string  url  =  "https://securegw.paytm.in/refund/apply";

			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

			webRequest.Method = "POST";
			webRequest.ContentType = "application/json";
			webRequest.ContentLength = post_data.Length;

			using (StreamWriter requestWriter = new StreamWriter(webRequest.GetRequestStream()))
			{
				requestWriter.Write(post_data);
			}

			string responseData = string.Empty;

			using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
			{
				responseData = responseReader.ReadToEnd();
				Console.WriteLine(responseData);
			}

            return View();
		}

        public ActionResult LevelTree()
        {

            return View();
        }
        public class SeriesList
        {
            public string? id { get; set; }
            public string? name { get; set; }

        }
        public class IndicatorList
        {
            public string? IndicatorName { get; set; }
        }
        public ActionResult GetLevelTree()
        {
            DirectRequest directRequest = new DirectRequest();
            directRequest.Fk_MemId = "332";
            DataSet dataSet = directRequest.GetLevelTree();


            directRequest.ConvertDataTableToList(dataSet.Tables[0]);
            List<List<string>> resultArray = directRequest.MyArray;




            return Json(directRequest);
        }
        public ActionResult GetClosingTime(string OrderId)
        {
            Common common=new Common();

            DataSet dataSet = common.GetClosingTime(OrderId, HttpContext.Session.GetString("Type"));
            common.closingdate = dataSet.Tables[0].Rows[0]["ClosingDate"].ToString();
            return Json(common);
        }
        public ActionResult ProcessingPayment(string OrderId)
        {
            AddressRequest addressRequest = new AddressRequest();
            addressRequest.MerchantTranId = OrderId;
            return View(addressRequest);
        }
    }
}


