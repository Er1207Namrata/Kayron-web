using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.EMMA;
using Karyon.Models;
using Karyon.Models.EwayBilling;
using Karyon.Models.RazorPay;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Paytm;
using QRCoder;
using Razorpay.Api;
using System.Data;
using System.Drawing;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using static Karyon.Models.Common;
using Data = Karyon.Models.Data;
using DataTable = System.Data.DataTable;


namespace Karyon.Controllers
{
    [ApiController]
    [Route("api/Karyon/")]
    public class KaryonController : ControllerBase
    {
        [HttpPost("GetStateCity")]
        [Produces("application/json")]
        public ResponseModel GetStateCity(RequestModel requestModel)
        {
            string EncryptedText = "";
            StateCityResponse objres = new StateCityResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {

                StateCityRequest stateCityRequest = new StateCityRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                stateCityRequest = JsonConvert.DeserializeObject<StateCityRequest>(dcdata);
                DataSet dataSet = stateCityRequest.GetStateCity();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            StateCity stateCity = new StateCity();
                            stateCity.State = dataSet.Tables[0].Rows[0]["State"].ToString();
                            stateCity.City = dataSet.Tables[0].Rows[0]["City"].ToString();
                            objres.Response = stateCity;
                            objres.Message = "";
                            objres.Status = 1;

                        }
                        else
                        {
                            objres.Message = Messages.NoRecordFound;
                            objres.Status = 0;

                        }


                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(StateCityResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("GetSponsorName")]
        [Produces("application/json")]
        public ResponseModel GetSponsorName(RequestModel requestModel)
        {
            string EncryptedText = "";
            SponsorResponse objres = new SponsorResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {

                SponsorRequest sponsorRequest = new SponsorRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                sponsorRequest = JsonConvert.DeserializeObject<SponsorRequest>(dcdata);
                DataSet dataSet = sponsorRequest.GetSponsorName();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            Sponsor sponsor = new Sponsor();
                            sponsor.Name = dataSet.Tables[0].Rows[0]["Name"].ToString();
                            sponsor.MobileNo = dataSet.Tables[0].Rows[0]["MobileNo"].ToString();
                            sponsor.TempPermanent = dataSet.Tables[0].Rows[0]["TempPermanent"].ToString();
                            sponsor.IsBID = dataSet.Tables[0].Rows[0]["IsBID"].ToString();
                            objres.Response = sponsor;
                            objres.Message = "";
                            objres.Status = 1;

                        }
                        else
                        {
                            objres.Message = Messages.InvalidSponsorId;
                            objres.Status = 0;

                        }


                    }
                    else
                    {
                        objres.Message = Messages.InvalidSponsorId;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.InvalidSponsorId;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(SponsorResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("GetSponsorNameByNewId")]
        [Produces("application/json")]
        public ResponseModel GetSponsorNameByNewId(RequestModel requestModel)
        {
            string EncryptedText = "";
            SponsorResponse objres = new SponsorResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {

                SponsorRequest sponsorRequest = new SponsorRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                sponsorRequest = JsonConvert.DeserializeObject<SponsorRequest>(dcdata);
                DataSet dataSet = sponsorRequest.GetSponsorNameByNewId();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            Sponsor sponsor = new Sponsor();
                            sponsor.Name = dataSet.Tables[0].Rows[0]["Name"].ToString();
                            sponsor.MobileNo = dataSet.Tables[0].Rows[0]["MobileNo"].ToString();
                            sponsor.SponsorLoginId = dataSet.Tables[0].Rows[0]["sponsorLoginId"].ToString();
                            sponsor.SponsorName = dataSet.Tables[0].Rows[0]["SponsorName"].ToString();
                            sponsor.PerformanceLevel = dataSet.Tables[0].Rows[0]["PerformanceLevel"].ToString();
                            objres.Response = sponsor;
                            objres.Message = "";
                            objres.Status = 1;

                        }
                        else
                        {
                            objres.Message = Messages.InvalidSponsorId;
                            objres.Status = 0;

                        }


                    }
                    else
                    {
                        objres.Message = Messages.InvalidSponsorId;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.InvalidSponsorId;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(SponsorResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("Registration")]
        [Produces("application/json")]
        public ResponseModel Registration(RequestModel requestModel)
        {
            string EncryptedText = "";
            LoginResponse objres = new LoginResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                Registration registration = new Registration();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                registration = JsonConvert.DeserializeObject<Registration>(dcdata);
                DataSet dataSet = registration.CustomerRegistration();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            Login login = new Login();
                            login.CustomerId = long.Parse(dataSet.Tables[0].Rows[0]["CustomerId"].ToString());
                            login.FirstName = dataSet.Tables[0].Rows[0]["FirstName"].ToString();
                            login.MiddleName = dataSet.Tables[0].Rows[0]["MiddleName"].ToString();
                            login.LastName = dataSet.Tables[0].Rows[0]["LastName"].ToString();
                            login.LoginId = dataSet.Tables[0].Rows[0]["LoginId"].ToString();
                            login.IskycApproved = dataSet.Tables[0].Rows[0]["IskycApproved"].ToString();
                            login.IsBusinessId = dataSet.Tables[0].Rows[0]["IsBusinessId"].ToString();
                            objres.Response = login;

                            objres.Message = "Registration Completed.Your LoginId is " + dataSet.Tables[0].Rows[0]["LoginId"].ToString() + " and password is " + registration.Password;
                            objres.Status = 1;

                            //Pushnotification pushnotification = new Pushnotification();
                            //if (dataSet.Tables[0].Rows.Count > 0)
                            //{
                            //    pushnotification.Notification(dataSet.Tables[1].Rows[0]["DeviceToken"].ToString(), "Registration Completed.Your LoginId is " + dataSet.Tables[0].Rows[0]["LoginId"].ToString() + " and password is " + registration.Password, "Associate Registration", dataSet.Tables[1].Rows[0]["LoginFrom"].ToString(), "OrderList");
                            //}

                        }
                        else
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 0;
                        }


                    }
                    else
                    {
                        objres.Message = Messages.ProblemInConnection;
                        objres.Status = 0;

                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(LoginResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("Login")]
        [Produces("application/json")]
        public ResponseModel Login(RequestModel requestModel)
        {
            string EncryptedText = "";
            LoginResponse objres = new LoginResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                LoginRequest loginRequest = new LoginRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                loginRequest = JsonConvert.DeserializeObject<LoginRequest>(dcdata);
                DataSet dataSet = loginRequest.LoginAction();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {

                            Login login = new Login();
                            login.CustomerId = long.Parse(dataSet.Tables[0].Rows[0]["CustomerId"].ToString());
                            login.FirstName = dataSet.Tables[0].Rows[0]["FirstName"].ToString();
                            login.LastName = dataSet.Tables[0].Rows[0]["LastName"].ToString();
                            login.LoginId = dataSet.Tables[0].Rows[0]["LoginId"].ToString();
                            login.UserType = dataSet.Tables[0].Rows[0]["UserType"].ToString();
                            login.RegistrationAmount = decimal.Parse(dataSet.Tables[0].Rows[0]["RegistrationAmount"].ToString());
                            login.FK_FranchiseTypeId = long.Parse(dataSet.Tables[0].Rows[0]["FK_FranchiseTypeId"].ToString());
                            login.IskycApproved = dataSet.Tables[0].Rows[0]["IskycApproved"].ToString();
                            login.IsBusinessId = dataSet.Tables[0].Rows[0]["IsBusinessId"].ToString();
                            login.TotalOrder = int.Parse(dataSet.Tables[0].Rows[0]["TotalOrder"].ToString());
                            login.MobileNo = dataSet.Tables[0].Rows[0]["MobileNo"].ToString();
                            objres.Response = login;
                            objres.Message = "";
                            objres.Status = 1;

                        }
                        else
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 0;
                        }


                    }
                    else
                    {
                        objres.Message = Messages.InvalidLoginId;
                        objres.Status = 0;

                    }
                }
                else
                {
                    objres.Message = Messages.InvalidLoginId;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(LoginResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("AdminLogin")]
        [Produces("application/json")]
        public ResponseModel AdminLogin(RequestModel requestModel)
        {
            string EncryptedText = "";
            LoginResponse objres = new LoginResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                LoginRequest loginRequest = new LoginRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                loginRequest = JsonConvert.DeserializeObject<LoginRequest>(dcdata);
                DataSet dataSet = loginRequest.AdminLogin();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {

                            Login login = new Login();
                            login.CustomerId = long.Parse(dataSet.Tables[0].Rows[0]["AdminId"].ToString());
                            login.FirstName = dataSet.Tables[0].Rows[0]["DisplayName"].ToString();

                            login.LoginId = dataSet.Tables[0].Rows[0]["LoginId"].ToString();
                            objres.Response = login;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.InvalidLoginId;
                            objres.Status = 0;

                        }


                    }
                    else
                    {
                        objres.Message = Messages.InvalidLoginId;
                        objres.Status = 0;

                    }
                }
                else
                {
                    objres.Message = Messages.InvalidLoginId;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = ex.Message;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(LoginResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("GetAllCategories")]
        [Produces("application/json")]
        public ResponseModel GetAllCategories()
        {
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            CategoryReposne objres = new CategoryReposne();
            try
            {
                Categories categories = new Categories();
                CategoriesRequest categoriesRequest = new CategoriesRequest();
                Categories categories1 = new Categories();
                DataSet dataSet = categoriesRequest.GetAllCategories();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<CategoriesRequest> listResponses = new List<CategoriesRequest>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            CategoriesRequest cat = new CategoriesRequest();
                            CategoryReposne objres1 = new CategoryReposne();
                            cat.CategoryId = long.Parse(dataSet.Tables[0].Rows[i]["CategoryId"].ToString());
                            cat.CategoryName = dataSet.Tables[0].Rows[i]["CategoryName"].ToString();
                            cat.ImageUrl = dataSet.Tables[0].Rows[i]["ImageUrl"].ToString();

                            listResponses.Add(cat);





                        }
                        categories1.CategoryList = listResponses;

                        objres.Message = "";
                        objres.Status = 1;
                        objres.Response = categories1;


                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;

                    }
                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;

            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CategoryReposne));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("GetAllProducts")]
        [Produces("application/json")]
        public ResponseModel GetAllProducts(RequestModel requestModel)
        {
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            ProductReposne objres = new ProductReposne();
            try
            {
                List<ProductDetails> listResponses = new List<ProductDetails>();
                List<CategoriesRequest> lstcategories = new List<CategoriesRequest>();
                List<Collection> listFeaturedProducts = new List<Collection>();
                List<RatingData> listratingData = new List<RatingData>();
                ProductsRequest productsRequest = new ProductsRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                productsRequest = JsonConvert.DeserializeObject<ProductsRequest>(dcdata);
                DataSet dataSet = productsRequest.GetProducts();
                if (dataSet != null)
                {
                    Products products = new Products();
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            List<VarientData> lstVarient = new List<VarientData>();
                            for (int j = 0; j <= dataSet.Tables[3].Rows.Count - 1; j++)
                            {

                                if (dataSet.Tables[0].Rows[i]["ProductId"].ToString() == dataSet.Tables[3].Rows[j]["ProductId"].ToString())
                                {
                                    VarientData varientData = new VarientData();
                                    Common common = new Common();
                                    varientData.Size = dataSet.Tables[3].Rows[j]["Size"].ToString();
                                    varientData.VarientId = long.Parse(dataSet.Tables[3].Rows[j]["VarientId"].ToString());
                                    varientData.EncryptId = Crypto.Encrypt(dataSet.Tables[3].Rows[j]["VarientId"].ToString());
                                    varientData.UnitName = dataSet.Tables[3].Rows[j]["UnitName"].ToString();
                                    varientData.PV = decimal.Parse(dataSet.Tables[3].Rows[j]["PV"].ToString());
                                    varientData.Weight = decimal.Parse(dataSet.Tables[3].Rows[j]["Weight"].ToString());
                                    varientData.CustomerId = int.Parse(dataSet.Tables[3].Rows[j]["CustomerId"].ToString());
                                    varientData.CartStatus = dataSet.Tables[3].Rows[j]["CartStatus"].ToString();
                                    varientData.CartQty = dataSet.Tables[3].Rows[j]["CartQty"].ToString();
                                    varientData.MRP = decimal.Parse(dataSet.Tables[3].Rows[j]["MRP"].ToString());
                                    varientData.OfferedPrice = decimal.Parse(dataSet.Tables[3].Rows[j]["OfferedPrice"].ToString());
                                    DataTable dataTable = common.JsonStringToDataTable(dataSet.Tables[3].Rows[j]["ProductImage"].ToString());
                                    ImageData imageData = new ImageData();
                                    List<ImageData> lstImageData = new List<ImageData>();
                                    for (int k = 0; k <= dataTable.Rows.Count - 1; k++)
                                    {
                                        imageData.ImageUrl = BaseUrl.ImageURL + "" + dataTable.Rows[k]["FileName"].ToString();
                                        lstImageData.Add(imageData);
                                    }
                                    varientData.ImageList = lstImageData;
                                    lstVarient.Add(varientData);

                                }
                            }
                            ProductDetails productDetails = new ProductDetails();
                            productDetails.ProductId = long.Parse(dataSet.Tables[0].Rows[i]["ProductId"].ToString());
                            productDetails.MRP = float.Parse(dataSet.Tables[0].Rows[i]["MRP"].ToString());
                            productDetails.ProductEncryptId = Crypto.Encrypt(dataSet.Tables[0].Rows[i]["ProductId"].ToString());
                            productDetails.HSNCode = dataSet.Tables[0].Rows[i]["HSNCode"].ToString();
                            productDetails.ProductName = dataSet.Tables[0].Rows[i]["ProductName"].ToString();
                            productDetails.Model = dataSet.Tables[0].Rows[i]["Model"].ToString();
                            productDetails.BrandName = dataSet.Tables[0].Rows[i]["BrandName"].ToString();
                            productDetails.StarRating = dataSet.Tables[0].Rows[i]["StarRating"].ToString();
                            productDetails.Image = BaseUrl.ImageURL + "" + dataSet.Tables[0].Rows[i]["IMAGE"].ToString();

                            productDetails.ProductsDetails = dataSet.Tables[0].Rows[i]["ProductDetails"].ToString();

                            productDetails.VarientList = lstVarient;
                            listResponses.Add(productDetails);
                        }

                        products.ProductList = listResponses;


                    }
                    if (dataSet.Tables[1].Rows.Count > 0)
                    {
                        for (int j = 0; j <= dataSet.Tables[1].Rows.Count - 1; j++)
                        {
                            CategoriesRequest categories = new CategoriesRequest();
                            categories.CategoryId = long.Parse(dataSet.Tables[1].Rows[j]["Id"].ToString());
                            categories.CategoryName = dataSet.Tables[1].Rows[j]["Name"].ToString();

                            lstcategories.Add(categories);
                        }
                        products.CategoryList = lstcategories;

                    }
                    if (dataSet.Tables[4].Rows.Count > 0)
                    {
                        for (int j = 0; j <= dataSet.Tables[4].Rows.Count - 1; j++)
                        {
                            RatingData ratingData = new RatingData();
                            ratingData.Name = dataSet.Tables[4].Rows[j]["Name"].ToString();
                            ratingData.LoginId = dataSet.Tables[4].Rows[j]["LoginId"].ToString();
                            ratingData.Rating = dataSet.Tables[4].Rows[j]["Rating"].ToString();
                            ratingData.Star = dataSet.Tables[4].Rows[j]["Star"].ToString();
                            ratingData.RatingDate = dataSet.Tables[4].Rows[j]["RatingDate"].ToString();
                            listratingData.Add(ratingData);
                        }
                        products.RatingList = listratingData;

                    }
                    objres.Message = "";
                    objres.Status = 1;
                    objres.Response = products;

                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ProductReposne));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("GetProductDetails")]
        [Produces("application/json")]
        public ResponseModel GetProductDetails(RequestModel requestModel)
        {
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            ProductDetailsResponse objres = new ProductDetailsResponse();
            try
            {
                List<ProductData> lstProduct = new List<ProductData>();
                List<RatingData> lstRating = new List<RatingData>();
                List<ImageData> lstImage = new List<ImageData>();
                List<Collection> lstRelatedProd = new List<Collection>();
                List<Collection> lstSingleProduct = new List<Collection>();
                ProductDetail productDetail = new ProductDetail();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                productDetail = JsonConvert.DeserializeObject<ProductDetail>(dcdata);
                DataSet dataSet = productDetail.GetProductDeatils();
                if (dataSet != null)
                {

                    ProductData productData = new ProductData();
                    if (dataSet.Tables[0] != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            productData.ProductId = long.Parse(dataSet.Tables[0].Rows[0]["ProductId"].ToString());
                            productData.MRP = float.Parse(dataSet.Tables[0].Rows[0]["MRP"].ToString());
                            productData.SellPrice = float.Parse(dataSet.Tables[0].Rows[0]["SellPrice"].ToString());
                            productData.ProductEncryptId = Crypto.Encrypt(dataSet.Tables[0].Rows[0]["ProductId"].ToString());
                            productData.Category = dataSet.Tables[0].Rows[0]["Category"].ToString();
                            productData.ProductName = dataSet.Tables[0].Rows[0]["ProductName"].ToString();
                            productData.Model = dataSet.Tables[0].Rows[0]["Model"].ToString();
                            productData.BrandName = dataSet.Tables[0].Rows[0]["BrandName"].ToString();
                            productData.StarRating = dataSet.Tables[0].Rows[0]["StarRating"].ToString();
                            productData.ProductsDetails = dataSet.Tables[0].Rows[0]["ProductDetails"].ToString();
                            productData.Tags = dataSet.Tables[0].Rows[0]["Tags"].ToString();
                            productData.CartStatus = dataSet.Tables[0].Rows[0]["CartStatus"].ToString();
                            productData.OfferPoint = float.Parse(dataSet.Tables[0].Rows[0]["OfferPoint"].ToString());
                            lstProduct.Add(productData);
                        }
                    }
                    if (dataSet.Tables[1] != null)
                    {
                        if (dataSet.Tables[1].Rows.Count > 0)
                        {
                            for (int j = 0; j <= dataSet.Tables[1].Rows.Count - 1; j++)
                            {
                                RatingData ratingData = new RatingData();
                                ratingData.RatingDate = dataSet.Tables[1].Rows[j]["RatingDate"].ToString();
                                ratingData.Name = dataSet.Tables[1].Rows[j]["Name"].ToString();
                                ratingData.LoginId = dataSet.Tables[1].Rows[j]["LoginId"].ToString();
                                ratingData.Rating = dataSet.Tables[1].Rows[j]["Rating"].ToString();
                                ratingData.Star = dataSet.Tables[1].Rows[j]["Star"].ToString();

                                lstRating.Add(ratingData);
                            }
                            productData.RatingList = lstRating;

                        }
                    }
                    if (dataSet.Tables[2] != null)
                    {
                        if (dataSet.Tables[2].Rows.Count > 0)
                        {
                            for (int j = 0; j <= dataSet.Tables[2].Rows.Count - 1; j++)
                            {
                                ImageData imageData = new ImageData();
                                imageData.ImageUrl = BaseUrl.ImageURL + "" + dataSet.Tables[2].Rows[j]["ImageUrl"].ToString();
                                lstImage.Add(imageData);
                            }
                            productData.ImageList = lstImage;
                        }
                    }

                    if (dataSet.Tables[3] != null)
                    {
                        if (dataSet.Tables[3].Rows.Count > 0)
                        {
                            for (int j = 0; j <= dataSet.Tables[3].Rows.Count - 1; j++)
                            {
                                Collection collection = new Collection();

                                collection.VarientId = long.Parse(dataSet.Tables[3].Rows[j]["VarientId"].ToString());

                                collection.MRP = float.Parse(dataSet.Tables[3].Rows[j]["MRP"].ToString());
                                collection.PV = float.Parse(dataSet.Tables[3].Rows[j]["PV"].ToString());
                                collection.BoxPrice = decimal.Parse(dataSet.Tables[3].Rows[j]["BoxPrice"].ToString());
                                collection.SellPrice = float.Parse(dataSet.Tables[3].Rows[j]["OfferedPrice"].ToString());
                                collection.UnitName = dataSet.Tables[3].Rows[j]["UnitName"].ToString();
                                collection.BoxQty = dataSet.Tables[3].Rows[j]["BoxQty"].ToString();
                                lstSingleProduct.Add(collection);
                            }
                            productData.SingleProduct = lstSingleProduct;
                        }
                    }

                    objres.Message = "";
                    objres.Status = 1;
                    objres.Response = productData;

                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ProductDetailsResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("CartList")]
        [Produces("application/json")]
        public ResponseModel CartList(RequestModel requestModel)
        {
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            CartResponse objres = new CartResponse();
            try
            {
                List<CartDetails> listResponses = new List<CartDetails>();
                CartRequest cartRequest = new CartRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                cartRequest = JsonConvert.DeserializeObject<CartRequest>(dcdata);
                cartRequest.OpCode = 3;
                DataSet dataSet = cartRequest.MembersCart();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        CartData cartData = new CartData();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            CartDetails cartDetails = new CartDetails();
                            cartDetails.CartId = long.Parse(dataSet.Tables[0].Rows[i]["CartId"].ToString());
                            cartDetails.ProductId = long.Parse(dataSet.Tables[0].Rows[i]["Id"].ToString());
                            cartDetails.ProductName = dataSet.Tables[0].Rows[i]["ProductName"].ToString();
                            cartDetails.MRP = float.Parse(dataSet.Tables[0].Rows[i]["MRP"].ToString());
                            cartDetails.TotalBv = float.Parse(dataSet.Tables[0].Rows[i]["TotalBv"].ToString());
                            cartDetails.Quantity = long.Parse(dataSet.Tables[0].Rows[i]["Quantity"].ToString());
                            cartDetails.FinalPrice = float.Parse(dataSet.Tables[0].Rows[i]["FinalPrice"].ToString());
                            cartDetails.Image = BaseUrl.ImageURL + "" + dataSet.Tables[0].Rows[i]["IMAGE"].ToString();
                            cartDetails.ProductDetails = dataSet.Tables[0].Rows[i]["ProductDetails"].ToString();
                            cartDetails.Unit = dataSet.Tables[0].Rows[i]["Unit"].ToString();
                            cartDetails.BrandName = dataSet.Tables[0].Rows[i]["BrandName"].ToString();
                            cartDetails.CartStatus = dataSet.Tables[0].Rows[i]["CartStatus"].ToString();
                            cartDetails.VarientId = long.Parse(dataSet.Tables[0].Rows[i]["Fk_VarientId"].ToString());
                            cartDetails.BeforeTax = float.Parse(dataSet.Tables[0].Rows[i]["BeforeTax"].ToString());
                            listResponses.Add(cartDetails);
                        }
                        cartData.CartList = listResponses;
                        cartData.ShippingCharge = float.Parse(dataSet.Tables[1].Rows[0]["ShippingCharge"].ToString());
                        cartData.TaxAmount = float.Parse(dataSet.Tables[1].Rows[0]["TaxAmount"].ToString());
                        objres.Message = "";
                        objres.Status = 1;
                        objres.Response = cartData;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;

                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CartResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("AddToCart")]
        [Produces("application/json")]
        public ResponseModel AddToCart(RequestModel requestModel)
        {
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            CartResponse objres = new CartResponse();
            try
            {
                List<CartDetails> listResponses = new List<CartDetails>();
                CartRequest cartRequest = new CartRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                cartRequest = JsonConvert.DeserializeObject<CartRequest>(dcdata);
                cartRequest.OpCode = 1;
                DataSet dataSet = cartRequest.MembersCart();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            CartData cartData = new CartData();
                            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                CartDetails cartDetails = new CartDetails();
                                cartDetails.CartId = long.Parse(dataSet.Tables[0].Rows[i]["CartId"].ToString());
                                cartDetails.ProductId = long.Parse(dataSet.Tables[0].Rows[i]["Id"].ToString());
                                cartDetails.ProductName = dataSet.Tables[0].Rows[i]["ProductName"].ToString();
                                cartDetails.MRP = float.Parse(dataSet.Tables[0].Rows[i]["MRP"].ToString());
                                cartDetails.TotalBv = float.Parse(dataSet.Tables[0].Rows[i]["TotalBv"].ToString());
                                cartDetails.Quantity = long.Parse(dataSet.Tables[0].Rows[i]["Quantity"].ToString());
                                cartDetails.FinalPrice = float.Parse(dataSet.Tables[0].Rows[i]["FinalPrice"].ToString());
                                cartDetails.Image = BaseUrl.ImageURL + "" + dataSet.Tables[0].Rows[i]["IMAGE"].ToString();
                                cartDetails.ProductDetails = dataSet.Tables[0].Rows[i]["ProductDetails"].ToString();
                                cartDetails.Unit = dataSet.Tables[0].Rows[i]["Unit"].ToString();
                                cartDetails.BrandName = dataSet.Tables[0].Rows[i]["BrandName"].ToString();
                                cartDetails.CartStatus = dataSet.Tables[0].Rows[i]["CartStatus"].ToString();
                                cartDetails.VarientId = long.Parse(dataSet.Tables[0].Rows[i]["Fk_VarientId"].ToString());
                                listResponses.Add(cartDetails);
                            }

                            cartData.CartList = listResponses;
                            cartData.ShippingCharge = float.Parse(dataSet.Tables[1].Rows[0]["ShippingCharge"].ToString());
                            cartData.TaxAmount = float.Parse(dataSet.Tables[1].Rows[0]["TaxAmount"].ToString());
                            objres.Message = "";
                            objres.Status = 1;
                            objres.Response = cartData;
                        }
                        else
                        {
                            CartData cartData = new CartData();
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 0;
                            cartData.CartList = listResponses;
                            objres.Response = cartData;
                        }

                        //objres.CartList = listResponses;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;

                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CartResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("DeleteCart")]
        [Produces("application/json")]
        public ResponseModel DeleteCart(RequestModel requestModel)
        {
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            CartResponse objres = new CartResponse();
            try
            {
                CartData cartData = new CartData();
                List<CartDetails> listResponses = new List<CartDetails>();
                CartRequest cartRequest = new CartRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                cartRequest = JsonConvert.DeserializeObject<CartRequest>(dcdata);
                cartRequest.OpCode = 2;
                DataSet dataSet = cartRequest.MembersCart();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {

                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            CartDetails cartDetails = new CartDetails();
                            cartDetails.CartId = long.Parse(dataSet.Tables[0].Rows[i]["CartId"].ToString());
                            cartDetails.ProductId = long.Parse(dataSet.Tables[0].Rows[i]["Id"].ToString());
                            cartDetails.ProductName = dataSet.Tables[0].Rows[i]["ProductName"].ToString();
                            cartDetails.MRP = float.Parse(dataSet.Tables[0].Rows[i]["MRP"].ToString());
                            cartDetails.Quantity = long.Parse(dataSet.Tables[0].Rows[i]["Quantity"].ToString());
                            cartDetails.TotalBv = float.Parse(dataSet.Tables[0].Rows[i]["TotalBv"].ToString());
                            cartDetails.FinalPrice = float.Parse(dataSet.Tables[0].Rows[i]["FinalPrice"].ToString());
                            cartDetails.Image = BaseUrl.ImageURL + "" + dataSet.Tables[0].Rows[i]["IMAGE"].ToString();
                            cartDetails.ProductDetails = dataSet.Tables[0].Rows[i]["ProductDetails"].ToString();
                            cartDetails.Unit = dataSet.Tables[0].Rows[i]["Unit"].ToString();
                            cartDetails.BrandName = dataSet.Tables[0].Rows[i]["BrandName"].ToString();
                            cartDetails.CartStatus = dataSet.Tables[0].Rows[i]["CartStatus"].ToString();
                            cartDetails.VarientId = long.Parse(dataSet.Tables[0].Rows[i]["Fk_VarientId"].ToString());
                            listResponses.Add(cartDetails);
                        }
                        cartData.ShippingCharge = float.Parse(dataSet.Tables[1].Rows[0]["ShippingCharge"].ToString());
                        cartData.TaxAmount = float.Parse(dataSet.Tables[1].Rows[0]["TaxAmount"].ToString());
                        cartData.CartList = listResponses;
                        objres.Message = "";
                        objres.Status = 1;
                        objres.Response = cartData;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 1;
                        cartData.CartList = listResponses;
                        objres.Response = cartData;

                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CartResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("GetShoppingDashBoard")]
        [Produces("application/json")]
        public ResponseModel GetShoppingDashBoard(RequestModel requestModel)
        {
            string EncryptedText = "";
            DashBoardResponse objres = new DashBoardResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {

                DashBoardRequest dashBoardRequest = new DashBoardRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                dashBoardRequest = JsonConvert.DeserializeObject<DashBoardRequest>(dcdata);

                DashBoard dashBoard = new DashBoard();
                Common common = new Common();
                DataSet dataSet = dashBoardRequest.GetShoppingDashBoard();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<Feature> listResponses = new List<Feature>();
                        List<CategoriesRequest> lstCategory = new List<CategoriesRequest>();
                        List<Banner> lstBanner = new List<Banner>();


                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            List<Collection> lstCollection = new List<Collection>();


                            for (int j = 0; j <= dataSet.Tables[1].Rows.Count - 1; j++)
                            {

                                if (dataSet.Tables[0].Rows[i]["Id"].ToString() == dataSet.Tables[1].Rows[j]["FeatureId"].ToString())
                                {
                                    List<VarientData> lstVarient = new List<VarientData>();
                                    for (int k = 0; k <= dataSet.Tables[2].Rows.Count - 1; k++)
                                    {
                                        if ((dataSet.Tables[1].Rows[j]["ProductId"].ToString() == dataSet.Tables[2].Rows[k]["ProductId"].ToString()))
                                        {
                                            VarientData varientData = new VarientData();
                                            varientData.Size = dataSet.Tables[2].Rows[k]["Size"].ToString();
                                            varientData.VarientId = long.Parse(dataSet.Tables[2].Rows[k]["VarientId"].ToString());
                                            varientData.ProductId = long.Parse(dataSet.Tables[2].Rows[k]["ProductId"].ToString());
                                            varientData.UnitName = dataSet.Tables[2].Rows[k]["UnitName"].ToString();
                                            varientData.PV = decimal.Parse(dataSet.Tables[2].Rows[k]["PV"].ToString());
                                            varientData.CartStatus = dataSet.Tables[2].Rows[k]["CartStatus"].ToString();
                                            varientData.Weight = decimal.Parse(dataSet.Tables[2].Rows[k]["Weight"].ToString());
                                            varientData.MRP = decimal.Parse(dataSet.Tables[2].Rows[k]["MRP"].ToString());
                                            varientData.StarRating = dataSet.Tables[2].Rows[k]["Star"].ToString();
                                            varientData.OfferedPrice = decimal.Parse(dataSet.Tables[2].Rows[k]["OfferedPrice"].ToString());
                                            DataTable dataTable = common.JsonStringToDataTable(dataSet.Tables[2].Rows[k]["ProductImage"].ToString());
                                            ImageData imageData = new ImageData();
                                            List<ImageData> lstImageData = new List<ImageData>();
                                            for (int m = 0; m <= dataTable.Rows.Count - 1; m++)
                                            {
                                                imageData.ImageUrl = BaseUrl.ImageURL + "" + dataTable.Rows[m]["FileName"].ToString();
                                                lstImageData.Add(imageData);
                                            }

                                            varientData.ImageList = lstImageData;
                                            lstVarient.Add(varientData);
                                        }
                                    }

                                    Collection collection = new Collection();
                                    //collection.VarientId = long.Parse(dataSet.Tables[1].Rows[j]["VarientId"].ToString());
                                    collection.ProductName = dataSet.Tables[1].Rows[j]["ProductName"].ToString();
                                    collection.PrimaryImage = BaseUrl.ImageURL + "" + dataSet.Tables[1].Rows[j]["PrimaryImage"].ToString();
                                    collection.StarRating = dataSet.Tables[1].Rows[j]["Star"].ToString();

                                    collection.VarientList = lstVarient;
                                    lstCollection.Add(collection);


                                }

                            }
                            Feature feature = new Feature();
                            feature.FeatureName = dataSet.Tables[0].Rows[i]["FeatureName"].ToString();
                            feature.FeaturedProductList = lstCollection;
                            listResponses.Add(feature);



                        }
                        dashBoard.FeaturedList = listResponses;


                        for (int j = 0; j <= dataSet.Tables[3].Rows.Count - 1; j++)
                        {
                            CategoriesRequest categoriesRequest = new CategoriesRequest();
                            categoriesRequest.CategoryId = long.Parse(dataSet.Tables[3].Rows[j]["Id"].ToString());
                            categoriesRequest.CategoryCount = int.Parse(dataSet.Tables[3].Rows[j]["CategoryCount"].ToString());
                            categoriesRequest.CategoryName = dataSet.Tables[3].Rows[j]["Name"].ToString();
                            categoriesRequest.ImageUrl = BaseUrl.ImageURL + "" + dataSet.Tables[3].Rows[j]["ImageUrl"].ToString();
                            //categoriesRequest.AchieveImageUrl = BaseUrl.ImageURL + "" + dataSet.Tables[3].Rows[j]["AchieverImage"].ToString();

                            lstCategory.Add(categoriesRequest);
                        }
                        dashBoard.CategoryList = lstCategory;

                        if (dataSet.Tables[4].Rows.Count > 0)
                        {
                            for (int j = 0; j <= dataSet.Tables[4].Rows.Count - 1; j++)
                            {
                                Banner banner = new Banner();
                                banner.ImageUrl = BaseUrl.ImageURL + dataSet.Tables[4].Rows[j]["Image"].ToString();
                                banner.Description = dataSet.Tables[4].Rows[j]["Description"].ToString();
                                banner.Message = dataSet.Tables[4].Rows[j]["Message"].ToString();

                                lstBanner.Add(banner);
                            }
                        }
                        if (dataSet.Tables[5].Rows.Count > 0)
                        {
                            dashBoard.FestivalPopup = BaseUrl.ImageURL + dataSet.Tables[5].Rows[0]["Image"].ToString();
                        }
                        dashBoard.BannerList = lstBanner;
                        if (dataSet.Tables[6].Rows.Count > 0)
                        {
                            dashBoard.IsBusinessId = dataSet.Tables[6].Rows[0]["IsBusinessId"].ToString();
                            dashBoard.IskycApproved = dataSet.Tables[6].Rows[0]["IskycApproved"].ToString();
                        }
                        // List<OfferProduct> offerProduct = new List<OfferProduct>();
                        //if (dataSet.Tables[7].Rows.Count > 0)
                        //{
                        //    for (int j = 0; j <= dataSet.Tables[7].Rows.Count - 1; j++)
                        //    {
                        //        OfferProduct Products = new OfferProduct();
                        //        Products.Description= dataSet.Tables[7].Rows[j]["Description"].ToString();
                        //        Products.ProductName= dataSet.Tables[7].Rows[j]["ProductName"].ToString();
                        //        Products.VarientId = long.Parse(dataSet.Tables[7].Rows[j]["VarientId"].ToString());
                        //        Products.MRP = decimal.Parse(dataSet.Tables[7].Rows[j]["MRP"].ToString());
                        //        Products.OfferPrice = decimal.Parse(dataSet.Tables[7].Rows[j]["OfferPrice"].ToString());
                        //        Products.ProductImage= BaseUrl.ImageURL + "" + dataSet.Tables[7].Rows[j]["ProductImage"].ToString();
                        //        //Products.ToDate= dataSet.Tables[7].Rows[j]["ToDate"].ToString('DD/MM/YYYY HH:mm:sss');
                        //        DataTable dataTable = common.JsonStringToDataTable(dataSet.Tables[7].Rows[j]["ProductImage"].ToString());
                        //        ImageData imageData = new ImageData();
                        //        List<ImageData> lstImageData = new List<ImageData>();
                        //        for (int m = 0; m <= dataTable.Rows.Count - 1; m++)
                        //        {
                        //            Products.ProductImage = BaseUrl.ImageURL + "" + dataTable.Rows[m]["FileName"].ToString();

                        //        }
                        //        offerProduct.Add(Products);
                        //    }
                        //    dashBoard.OfferProducts = offerProduct;
                        //}
                        objres.Message = "";
                        objres.Status = 1;
                        objres.Response = dashBoard;

                    }

                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(DashBoardResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("GetAssociateDashBoard")]
        [Produces("application/json")]
        public ResponseModel GetAssociateDashBoard(RequestModel requestModel)
        {
            string EncryptedText = "";
            AssociateDashBoardResponse objres = new AssociateDashBoardResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                DashBoardRequest dashBoardRequest = new DashBoardRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                dashBoardRequest = JsonConvert.DeserializeObject<DashBoardRequest>(dcdata);
                DashBoard dashBoard = new DashBoard();
                DataSet dataSet = dashBoardRequest.GetAssociateDashBoard();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        AssociateDashBoard associateDashBoard = new AssociateDashBoard();
                        associateDashBoard.TotalMember = long.Parse(dataSet.Tables[0].Rows[0]["TotalMember"].ToString());
                        associateDashBoard.TotalActiveMember = long.Parse(dataSet.Tables[0].Rows[0]["TotalActiveMember"].ToString());
                        associateDashBoard.TotalInactiveMember = long.Parse(dataSet.Tables[0].Rows[0]["TotalInactiveMember"].ToString());
                        associateDashBoard.TotalLeftBusiness = decimal.Parse(dataSet.Tables[0].Rows[0]["TotalLeftBusiness"].ToString());
                        associateDashBoard.TotalRightBusiness = decimal.Parse(dataSet.Tables[0].Rows[0]["TotalRightBusiness"].ToString());
                        associateDashBoard.CaryForwardLeft = decimal.Parse(dataSet.Tables[0].Rows[0]["CaryForwardLeft"].ToString());
                        associateDashBoard.CaryForwardRight = decimal.Parse(dataSet.Tables[0].Rows[0]["CaryForwardRight"].ToString());
                        associateDashBoard.PaidBusinessLeft = decimal.Parse(dataSet.Tables[0].Rows[0]["PaidBusinessLeft"].ToString());
                        associateDashBoard.PaidBusinessRight = decimal.Parse(dataSet.Tables[0].Rows[0]["PaidBusinessRight"].ToString());
                        associateDashBoard.TotalRightBusiness = decimal.Parse(dataSet.Tables[0].Rows[0]["TotalRightBusiness"].ToString());
                        associateDashBoard.SelfIncome = decimal.Parse(dataSet.Tables[0].Rows[0]["SelfIncome"].ToString());
                        associateDashBoard.BinaryIncome = decimal.Parse(dataSet.Tables[0].Rows[0]["BinaryIncome"].ToString());
                        associateDashBoard.TotalIncome = decimal.Parse(dataSet.Tables[0].Rows[0]["TotalIncome"].ToString());
                        associateDashBoard.PayoutWallet = decimal.Parse(dataSet.Tables[0].Rows[0]["TotalWallet"].ToString());
                        associateDashBoard.TodayIncome = decimal.Parse(dataSet.Tables[0].Rows[0]["Todayincome"].ToString());
                        associateDashBoard.TotalShopping = decimal.Parse(dataSet.Tables[0].Rows[0]["TotalShopping"].ToString());
                        associateDashBoard.Advisor1 = decimal.Parse(dataSet.Tables[0].Rows[0]["Advisor1"].ToString());
                        associateDashBoard.Advisor2 = decimal.Parse(dataSet.Tables[0].Rows[0]["Advisor2"].ToString());
                        associateDashBoard.Advisor3 = decimal.Parse(dataSet.Tables[0].Rows[0]["Advisor3"].ToString());
                        associateDashBoard.Royal = decimal.Parse(dataSet.Tables[0].Rows[0]["Royal"].ToString());
                        associateDashBoard.RankImage = dataSet.Tables[0].Rows[0]["RankImage"].ToString();
                        associateDashBoard.FamilynWallet = decimal.Parse(dataSet.Tables[0].Rows[0]["FamilynWallet"].ToString());
                        associateDashBoard.TotalLeftBusinessC = decimal.Parse(dataSet.Tables[0].Rows[0]["TotalLeftBusinessC"].ToString());
                        associateDashBoard.TotalRightBusinessC = decimal.Parse(dataSet.Tables[0].Rows[0]["TotalRightBusinessC"].ToString());
                        associateDashBoard.CaryForwardLeftC = decimal.Parse(dataSet.Tables[0].Rows[0]["CaryForwardLeftC"].ToString());
                        associateDashBoard.CaryForwardRightC = decimal.Parse(dataSet.Tables[0].Rows[0]["CaryForwardRightC"].ToString());
                        associateDashBoard.PaidBusinessLeftC = decimal.Parse(dataSet.Tables[0].Rows[0]["PaidBusinessLeftC"].ToString());
                        associateDashBoard.PaidBusinessRightC = decimal.Parse(dataSet.Tables[0].Rows[0]["PaidBusinessRightC"].ToString());
                        associateDashBoard.TotalRightBusinessC = decimal.Parse(dataSet.Tables[0].Rows[0]["TotalRightBusinessC"].ToString());
                        associateDashBoard.CreatorHarmony = decimal.Parse(dataSet.Tables[0].Rows[0]["CreatorHarmony"].ToString());
                        associateDashBoard.LeftBVFresh = decimal.Parse(dataSet.Tables[0].Rows[0]["LeftBVFresh"].ToString());
                        associateDashBoard.RightBVFresh = decimal.Parse(dataSet.Tables[0].Rows[0]["RightBVFresh"].ToString());
                        associateDashBoard.SelfIncomeU = decimal.Parse(dataSet.Tables[0].Rows[0]["SelfIncomeU"].ToString());
                        associateDashBoard.BinaryIncomeU = decimal.Parse(dataSet.Tables[0].Rows[0]["BinaryIncomeU"].ToString());
                        associateDashBoard.RoyalU = decimal.Parse(dataSet.Tables[0].Rows[0]["RoyalU"].ToString());
                        associateDashBoard.CreatorHarmonyU = decimal.Parse(dataSet.Tables[0].Rows[0]["CreatorHarmonyU"].ToString());
                        associateDashBoard.OfferImage = BaseUrl.ImageURL + "" + dataSet.Tables[0].Rows[0]["OfferImage"].ToString();
                        associateDashBoard.AchivedPoints = dataSet.Tables[0].Rows[0]["AchivedPoints"].ToString();
                        associateDashBoard.HarmonyPoints = dataSet.Tables[0].Rows[0]["HarmonyPoints"].ToString();
                        associateDashBoard.OfferStatus = dataSet.Tables[0].Rows[0]["OfferStatus"].ToString();
                        associateDashBoard.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                        associateDashBoard.OfferName = dataSet.Tables[0].Rows[0]["OfferName"].ToString();
                        associateDashBoard.TotalMemberR = int.Parse(dataSet.Tables[0].Rows[0]["TotalMemberR"].ToString());
                        associateDashBoard.TotalActiveMemberR = int.Parse(dataSet.Tables[0].Rows[0]["TotalActiveMemberR"].ToString());
                        associateDashBoard.TotalInactiveMemberR = int.Parse(dataSet.Tables[0].Rows[0]["TotalInactiveMemberR"].ToString());
                        associateDashBoard.LeftBusiness = dataSet.Tables[0].Rows[0]["LeftBusiness"].ToString();
                        associateDashBoard.RightBusiness = dataSet.Tables[0].Rows[0]["RightBusiness"].ToString();
                        associateDashBoard.ShowEventMenu = bool.Parse(dataSet.Tables[0].Rows[0]["ShowEventMenu"].ToString());

                        List<ShoppingData> listResponses = new List<ShoppingData>();
                        if (dataSet.Tables[1] != null)
                        {
                            if (dataSet.Tables[1].Rows.Count > 0)
                            {
                                for (int i = 0; i <= dataSet.Tables[1].Rows.Count - 1; i++)
                                {
                                    ShoppingData shoppingData = new ShoppingData();
                                    shoppingData.OrderDate = dataSet.Tables[1].Rows[i]["OrderDate"].ToString();
                                    shoppingData.OrderNo = dataSet.Tables[1].Rows[i]["OrderNo"].ToString();
                                    shoppingData.OrderAmount = decimal.Parse(dataSet.Tables[1].Rows[i]["OrderAmount"].ToString());
                                    shoppingData.TotalPV = decimal.Parse(dataSet.Tables[1].Rows[i]["TotalPV"].ToString());
                                    shoppingData.Status = dataSet.Tables[1].Rows[i]["Status"].ToString();
                                    listResponses.Add(shoppingData);
                                }
                            }
                            associateDashBoard.lstShopping = listResponses;
                        }

                        objres.Message = "";
                        objres.Status = 1;
                        objres.Response = associateDashBoard;
                        List<OfferMasterData> listOfferMasterResponses = new List<OfferMasterData>();
                        if (dataSet.Tables[2] != null)
                        {
                            if (dataSet.Tables[2].Rows.Count > 0)
                            {
                                for (int i = 0; i <= dataSet.Tables[2].Rows.Count - 1; i++)
                                {
                                    OfferMasterData offerMasterData = new OfferMasterData();
                                    offerMasterData.OfferName = dataSet.Tables[2].Rows[i]["OfferName"].ToString();
                                    offerMasterData.OfferImage = BaseUrl.LeftRightURL + dataSet.Tables[2].Rows[i]["OfferImage"].ToString();
                                    offerMasterData.ToDate = dataSet.Tables[2].Rows[i]["ToDate"].ToString();
                                    offerMasterData.FromDate = dataSet.Tables[2].Rows[i]["FromDate"].ToString();
                                    offerMasterData.HarmonyCount = dataSet.Tables[2].Rows[i]["HarmonyCount"].ToString();
                                    offerMasterData.OfferStatus = dataSet.Tables[2].Rows[i]["OfferStatus"].ToString();
                                    offerMasterData.AchivePoint = dataSet.Tables[2].Rows[i]["AchivePoint"].ToString();
                                    offerMasterData.Pk_OfferId = dataSet.Tables[2].Rows[i]["Pk_OfferId"].ToString();
                                    offerMasterData.AchieveImage = BaseUrl.LeftRightURL + dataSet.Tables[2].Rows[i]["AchieverImage"].ToString();
                                    listOfferMasterResponses.Add(offerMasterData);
                                }
                            }
                            associateDashBoard.lstOfferMaster = listOfferMasterResponses;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AssociateDashBoardResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("GetAssociateProfile")]
        [Produces("application/json")]
        public ResponseModel GetAssociateProfile(RequestModel requestModel)
        {
            string EncryptedText = "";
            ProfileReposne objres = new ProfileReposne();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                ProfileRequest profileRequest = new ProfileRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                profileRequest = JsonConvert.DeserializeObject<ProfileRequest>(dcdata);
                DataSet dataSet = profileRequest.GetAssociateProfile();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        Profile profile = new Profile();
                        profile.LoginId = dataSet.Tables[0].Rows[0]["LoginId"].ToString();
                        profile.FirstName = dataSet.Tables[0].Rows[0]["FirstName"].ToString();
                        profile.LastName = dataSet.Tables[0].Rows[0]["LastName"].ToString();
                        profile.MobileNo = dataSet.Tables[0].Rows[0]["MobileNo"].ToString();
                        profile.Gender = dataSet.Tables[0].Rows[0]["Gender"].ToString();
                        profile.Status = dataSet.Tables[0].Rows[0]["Status"].ToString();
                        profile.PanCard = dataSet.Tables[0].Rows[0]["PanCard"].ToString();
                        profile.JoiningDate = dataSet.Tables[0].Rows[0]["JoiningDate"].ToString();
                        profile.Address = dataSet.Tables[0].Rows[0]["Address"].ToString();
                        profile.Email = dataSet.Tables[0].Rows[0]["Email"].ToString();
                        objres.Message = "";
                        objres.Status = "1";
                        objres.Response = profile;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = "0";
                    }

                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = "0";
                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = "0";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ProfileReposne));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("DirectList")]
        [Produces("application/json")]
        public ResponseModel DirectList(RequestModel requestModel)
        {
            string EncryptedText = "";
            DirectResponse objres = new DirectResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {

                DirectRequest directRequest = new DirectRequest();
                Direct direct = new Direct();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                directRequest = JsonConvert.DeserializeObject<DirectRequest>(dcdata);

                directRequest.Zone = string.IsNullOrEmpty(directRequest.Zone) ? null : directRequest.Zone;
                directRequest.LoginId = string.IsNullOrEmpty(directRequest.LoginId) ? null : directRequest.LoginId;
                directRequest.PlaceUnderId = string.IsNullOrEmpty(directRequest.PlaceUnderId) ? null : directRequest.PlaceUnderId;
                DataSet dataSet = directRequest.GetDirectList();
                if (dataSet != null)
                {
                    objres.PreviousId = dataSet.Tables[1].Rows[0]["FK_MemId"].ToString();
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {

                        List<Data> listResponses = new List<Data>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            Data data = new Data();

                            data.LoginId = dataSet.Tables[0].Rows[i]["LoginId"].ToString();
                            data.FirstName = dataSet.Tables[0].Rows[i]["FirstName"].ToString();
                            data.LastName = dataSet.Tables[0].Rows[i]["LastName"].ToString();
                            data.Status = dataSet.Tables[0].Rows[i]["Status"].ToString();
                            data.JoiningDate = dataSet.Tables[0].Rows[i]["JoiningDate"].ToString();
                            data.PermanentDate = dataSet.Tables[0].Rows[i]["PermanentDate"].ToString();
                            data.FK_MemId = dataSet.Tables[0].Rows[i]["FK_MemId"].ToString();
                            data.SponsorName = dataSet.Tables[0].Rows[i]["SponsorName"].ToString();
                            data.SponsorLoginId = dataSet.Tables[0].Rows[i]["SponsorLoginId"].ToString();
                            data.ImageURL = dataSet.Tables[0].Rows[i]["ImageURL"].ToString();
                            data.TotalRecords = int.Parse(dataSet.Tables[0].Rows[i]["ToatlRecord"].ToString());

                            listResponses.Add(data);





                        }
                        List<Data> listResponses1 = new List<Data>();
                        for (int i = 0; i <= dataSet.Tables[2].Rows.Count - 1; i++)
                        {
                            Data data = new Data();

                            data.LoginId = dataSet.Tables[2].Rows[i]["LoginId"].ToString();
                            data.FirstName = dataSet.Tables[2].Rows[i]["FirstName"].ToString();
                            data.LastName = dataSet.Tables[2].Rows[i]["LastName"].ToString();
                            data.Status = dataSet.Tables[2].Rows[i]["Status"].ToString();
                            data.JoiningDate = dataSet.Tables[2].Rows[i]["JoiningDate"].ToString();
                            data.PermanentDate = dataSet.Tables[2].Rows[i]["PermanentDate"].ToString();
                            data.FK_MemId = dataSet.Tables[2].Rows[i]["FK_MemId"].ToString();
                            data.SponsorName = dataSet.Tables[2].Rows[i]["SponsorName"].ToString();
                            data.SponsorLoginId = dataSet.Tables[2].Rows[i]["SponsorLoginId"].ToString();
                            data.ImageURL = dataSet.Tables[2].Rows[i]["ImageURL"].ToString();

                            listResponses1.Add(data);





                        }
                        direct.DirectList = listResponses;
                        direct.SelfData = listResponses1;

                        objres.Message = "";
                        objres.Status = 1;
                        objres.Response = direct;

                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(DirectResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("DownlineList")]
        [Produces("application/json")]
        public ResponseModel DownlineList(RequestModel requestModel)
        {
            string EncryptedText = "";
            DownlineResponse objres = new DownlineResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {

                DownlineRequest downlineRequest = new DownlineRequest();
                Downline downline = new Downline();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                downlineRequest = JsonConvert.DeserializeObject<DownlineRequest>(dcdata);
                downlineRequest.Zone = string.IsNullOrEmpty(downlineRequest.Zone) ? null : downlineRequest.Zone;
                downlineRequest.LoginId = string.IsNullOrEmpty(downlineRequest.LoginId) ? null : downlineRequest.LoginId;
                downlineRequest.PlaceUnderId = string.IsNullOrEmpty(downlineRequest.PlaceUnderId) ? null : downlineRequest.PlaceUnderId;
                downlineRequest.Status = string.IsNullOrEmpty(downlineRequest.Status) ? null : downlineRequest.Status;

                DataSet dataSet = downlineRequest.GetDownlineList();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {

                        List<Data> listResponses = new List<Data>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            Data data = new Data();

                            data.LoginId = dataSet.Tables[0].Rows[i]["LoginId"].ToString();
                            data.FirstName = dataSet.Tables[0].Rows[i]["FirstName"].ToString();
                            data.LastName = dataSet.Tables[0].Rows[i]["LastName"].ToString();
                            data.Status = dataSet.Tables[0].Rows[i]["Status"].ToString();
                            data.JoiningDate = dataSet.Tables[0].Rows[i]["JoiningDate"].ToString();
                            data.PermanentDate = dataSet.Tables[0].Rows[i]["PermanentDate"].ToString();
                            data.Leg = dataSet.Tables[0].Rows[i]["Leg"].ToString();
                            data.TotalRecords = int.Parse(dataSet.Tables[0].Rows[i]["ToatlRecord"].ToString());
                            data.PlaceUnderLoginId = dataSet.Tables[0].Rows[i]["PlaceUnderLoginId"].ToString();
                            data.PlaceUnderName = dataSet.Tables[0].Rows[i]["PlaceUnderName"].ToString();
                            listResponses.Add(data);





                        }
                        downline.DownlineList = listResponses;

                        objres.Message = "";
                        objres.Status = 1;
                        objres.Response = downline;

                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(DownlineResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("ChangePassword")]
        [Produces("application/json")]
        public ResponseModel ChangePassword(RequestModel requestModel)
        {
            string EncryptedText = "";
            ChangePasswordResponse objres = new ChangePasswordResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {

                ChangePassword changePassword = new ChangePassword();

                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                changePassword = JsonConvert.DeserializeObject<ChangePassword>(dcdata);


                DataSet dataSet = changePassword.PasswordChange();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {

                            objres.Message = "Password changed successfully";
                            objres.Status = 1;

                        }
                        else
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 0;

                        }


                    }
                    else
                    {
                        objres.Message = Messages.ProblemInConnection;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ChangePasswordResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("KaryonPointsRequest")]

        public async Task<ResponseModel> KaryonPointsRequest(IFormFile? files, [FromForm] RequestModel requestModel)
        {

            string EncryptedText = "";
            KaryonPointsResponse objres = new KaryonPointsResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                string filePath = "";
                if (files != null)
                {
                    filePath = await files.WriteFiles("KaryonPointsRequest");
                }

                KaryonPointsRequest karyonPointsRequest = new KaryonPointsRequest();
                KaryonPoints karyonPoints = new KaryonPoints();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                karyonPointsRequest = JsonConvert.DeserializeObject<KaryonPointsRequest>(dcdata);
                karyonPointsRequest.PaymentSlip = filePath;
                karyonPointsRequest.OrderNo = DateTime.Now.ToString("ddMMyyyyhhMMss");
                karyonPointsRequest.TransactionDate = string.IsNullOrEmpty(karyonPointsRequest.TransactionDate) ? null : Common.ConvertToSystemDate(karyonPointsRequest.TransactionDate, "dd/MM/yyyy");
                DataSet dataSet = karyonPointsRequest.RequestKaryonPoints();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            List<KaryonPointsData> listResponses = new List<KaryonPointsData>();
                            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                KaryonPointsData karyonPointsData = new KaryonPointsData();

                                karyonPointsData.Amount = decimal.Parse(dataSet.Tables[0].Rows[i]["Amount"].ToString());
                                karyonPointsData.RequestedDate = dataSet.Tables[0].Rows[i]["RequestedDate"].ToString();
                                karyonPointsData.Status = dataSet.Tables[0].Rows[i]["Status"].ToString();
                                karyonPointsData.Status = dataSet.Tables[0].Rows[i]["Status"].ToString();
                                karyonPointsData.TransactionDate = dataSet.Tables[0].Rows[i]["TransactionDate"].ToString();
                                karyonPointsData.PaymentMode = dataSet.Tables[0].Rows[i]["PaymentMode"].ToString();
                                karyonPointsData.TransactionNo = dataSet.Tables[0].Rows[i]["TransactionNo"].ToString();

                                listResponses.Add(karyonPointsData);
                            }
                            karyonPoints.RequestList = listResponses;
                            objres.Message = "Karyon Points Request Completed";
                            objres.Status = 1;
                            objres.Response = karyonPoints;

                        }
                        else
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 0;

                        }


                    }
                    else
                    {
                        objres.Message = Messages.ProblemInConnection;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(KaryonPointsResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;

        }


        [HttpPost("KaryonPointsRequestMobile")]

        public async Task<ResponseModel> KaryonPointsRequestMobile(IFormFile? files, [FromForm] RequestModel requestModel)
        {

            string EncryptedText = "";
            KaryonPointsResponse objres = new KaryonPointsResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                string filePath = "";
                if (files != null)
                {
                    filePath = await files.WriteFiles("KaryonPointsRequest");
                }
                String merchantKey = "";
                string MID = "";
                KaryonPointsRequest karyonPointsRequest = new KaryonPointsRequest();
                KaryonPoints karyonPoints = new KaryonPoints();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                karyonPointsRequest = JsonConvert.DeserializeObject<KaryonPointsRequest>(dcdata);
                karyonPointsRequest.PaymentSlip = filePath;
                karyonPointsRequest.TransactionDate = string.IsNullOrEmpty(karyonPointsRequest.TransactionDate) ? null : Common.ConvertToSystemDate(karyonPointsRequest.TransactionDate, "dd/MM/yyyy");
                if (karyonPointsRequest.PaymentMode == "UPI/NET Banking/Debit Card/Credit Card")
                {
                    string orderId = karyonPointsRequest.TransactionNo;
                    Dictionary<string, object> body = new Dictionary<string, object>();
                    Dictionary<string, string> head = new Dictionary<string, string>();
                    Dictionary<string, object> requestBody = new Dictionary<string, object>();

                    Dictionary<string, string> txnAmount = new Dictionary<string, string>();
                    txnAmount.Add("value", karyonPointsRequest.Amount.ToString());
                    txnAmount.Add("currency", "INR");
                    Dictionary<string, string> userInfo = new Dictionary<string, string>();
                    userInfo.Add("custId", karyonPointsRequest.LoginId);
                    body.Add("requestType", "Payment");
                    if (karyonPointsRequest.IsLive == 1)
                    {
                        body.Add("mid", Gateway.MID);
                        body.Add("websiteName", Gateway.WebSite);
                    }
                    else
                    {
                        body.Add("mid", Gateway.MIDLocal);
                        body.Add("websiteName", Gateway.WebSiteLocal);
                    }


                    body.Add("orderId", orderId);
                    body.Add("txnAmount", txnAmount);
                    body.Add("userInfo", userInfo);
                    if (karyonPointsRequest.IsLive == 1)
                    {
                        body.Add("callbackUrl", "https://securegw.paytm.in");
                    }
                    else
                    {
                        body.Add("callbackUrl", "https://securegw-stage.paytm.in");
                    }


                    if (karyonPointsRequest.IsLive == 1)
                    {
                        merchantKey = Gateway.MerchantId;
                        MID = Gateway.MID;
                    }
                    else
                    {
                        merchantKey = Gateway.MerchantIdLocal;
                        MID = Gateway.MIDLocal;
                    }
                    string paytmChecksum = Checksum.generateSignature(JsonConvert.SerializeObject(body), merchantKey);

                    //  string paytmChecksum = CheckSum.generateCheckSum(merchantKey, JsonConvert.SerializeObject(body));

                    head.Add("signature", paytmChecksum);

                    requestBody.Add("body", body);
                    requestBody.Add("head", head);

                    string post_data = JsonConvert.SerializeObject(requestBody);
                    string url = "";
                    if (karyonPointsRequest.IsLive == 1)
                    {
                        url = "https://securegw.paytm.in/theia/api/v1/initiateTransaction?mid=" + MID + "&orderId=" + orderId + "";
                    }
                    else
                    {
                        url = "https://securegw-stage.paytm.in/theia/api/v1/initiateTransaction?mid=" + MID + "&orderId=" + orderId + "";
                    }

                    //For  Production 
                    //string  url  =  "https://securegw.paytm.in/theia/api/v1/initiateTransaction?mid=YOUR_MID_HERE&orderId=ORDERID_98765";

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
                        PaytmInitiateResponse deserializedProduct = JsonConvert.DeserializeObject<PaytmInitiateResponse>(responseData);
                        if (deserializedProduct.body.resultInfo.resultCode == "0000")
                        {
                            karyonPointsRequest.OrderNo = orderId;

                            DataSet dataSet = karyonPointsRequest.RequestKaryonPoints();
                            if (dataSet != null)
                            {
                                if (dataSet.Tables[0].Rows.Count > 0)
                                {
                                    if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                                    {
                                        objres.Message = "";
                                        objres.Status = 1;
                                        objres.OrderAmount = dataSet.Tables[0].Rows[0]["Amount"].ToString();
                                        objres.OrderNo = dataSet.Tables[0].Rows[0]["OrderNo"].ToString();
                                        objres.TxnToken = deserializedProduct.body.txnToken;
                                    }
                                    else
                                    {
                                        objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                                        objres.Status = 0;
                                    }
                                }
                                else
                                {
                                    objres.Message = Messages.ProblemInConnection;
                                    objres.Status = 0;

                                }

                            }
                            else
                            {
                                objres.Message = Messages.ProblemInConnection;
                                objres.Status = 0;

                            }
                        }
                        else
                        {
                            objres.Message = deserializedProduct.body.resultInfo.resultMsg;
                            objres.Status = 0;
                        }
                        Console.WriteLine(responseData);
                    }
                }
                else
                {

                    if (files != null)
                    {
                        filePath = await files.WriteFiles("KaryonPointsRequest");
                    }
                    DataSet dataSet = karyonPointsRequest.RequestKaryonPoints();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                            {
                                List<KaryonPointsData> listResponses = new List<KaryonPointsData>();
                                for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                                {
                                    KaryonPointsData karyonPointsData = new KaryonPointsData();

                                    karyonPointsData.Amount = decimal.Parse(dataSet.Tables[0].Rows[i]["Amount"].ToString());
                                    karyonPointsData.RequestedDate = dataSet.Tables[0].Rows[i]["RequestedDate"].ToString();
                                    karyonPointsData.Status = dataSet.Tables[0].Rows[i]["Status"].ToString();
                                    karyonPointsData.Status = dataSet.Tables[0].Rows[i]["Status"].ToString();
                                    karyonPointsData.TransactionDate = dataSet.Tables[0].Rows[i]["TransactionDate"].ToString();
                                    karyonPointsData.PaymentMode = dataSet.Tables[0].Rows[i]["PaymentMode"].ToString();
                                    karyonPointsData.TransactionNo = dataSet.Tables[0].Rows[i]["TransactionNo"].ToString();

                                    listResponses.Add(karyonPointsData);





                                }
                                karyonPoints.RequestList = listResponses;
                                objres.Message = "Karyon Points Request Completed";
                                objres.Status = 1;
                                objres.Response = karyonPoints;

                            }
                            else
                            {
                                objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                                objres.Status = 0;

                            }


                        }
                        else
                        {
                            objres.Message = Messages.ProblemInConnection;
                            objres.Status = 0;

                        }

                    }
                    else
                    {
                        objres.Message = Messages.ProblemInConnection;
                        objres.Status = 0;

                    }
                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(KaryonPointsResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;

        }

        [HttpPost("FranchiseeKaryonWalletRequestMobile")]
        [Produces("application/json")]
        public ResponseModel FranchiseeKaryonWalletRequestMobile(RequestModel requestModel)
        {
            string EncryptedText = "";
            FranchiseeKaryonPointsResponse objres = new FranchiseeKaryonPointsResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                String merchantKey = "";
                string MID = "";
                FranchiseeKaryonPointsRequest karyonPointsRequest = new FranchiseeKaryonPointsRequest();
                FranchiseeKaryonPoints karyonPoints = new FranchiseeKaryonPoints();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                karyonPointsRequest = JsonConvert.DeserializeObject<FranchiseeKaryonPointsRequest>(dcdata);
                karyonPointsRequest.TransactionDate = string.IsNullOrEmpty(karyonPointsRequest.TransactionDate) ? null : Common.ConvertToSystemDate(karyonPointsRequest.TransactionDate, "dd/MM/yyyy");
                if (karyonPointsRequest.PaymentMode == "UPI/NET Banking/Debit Card/Credit Card")
                {
                    string orderId = "KPF" + DateTime.Now.ToString("ddMMyyyyhhMMss");
                    Dictionary<string, object> body = new Dictionary<string, object>();
                    Dictionary<string, string> head = new Dictionary<string, string>();
                    Dictionary<string, object> requestBody = new Dictionary<string, object>();

                    Dictionary<string, string> txnAmount = new Dictionary<string, string>();
                    txnAmount.Add("value", karyonPointsRequest.Amount.ToString());
                    txnAmount.Add("currency", "INR");
                    Dictionary<string, string> userInfo = new Dictionary<string, string>();
                    userInfo.Add("custId", karyonPointsRequest.LoginId);
                    body.Add("requestType", "Payment");
                    if (karyonPointsRequest.IsLive == 1)
                    {
                        body.Add("mid", Gateway.MID);
                        body.Add("websiteName", Gateway.WebSite);
                    }
                    else
                    {
                        body.Add("mid", Gateway.MIDLocal);
                        body.Add("websiteName", Gateway.WebSiteLocal);
                    }


                    body.Add("orderId", orderId);
                    body.Add("txnAmount", txnAmount);
                    body.Add("userInfo", userInfo);
                    if (karyonPointsRequest.IsLive == 1)
                    {
                        body.Add("callbackUrl", "https://securegw.paytm.in");
                    }
                    else
                    {
                        body.Add("callbackUrl", "https://securegw-stage.paytm.in");
                    }


                    if (karyonPointsRequest.IsLive == 1)
                    {
                        merchantKey = Gateway.MerchantId;
                        MID = Gateway.MID;
                    }
                    else
                    {
                        merchantKey = Gateway.MerchantIdLocal;
                        MID = Gateway.MIDLocal;
                    }
                    string paytmChecksum = Checksum.generateSignature(JsonConvert.SerializeObject(body), merchantKey);

                    //  string paytmChecksum = CheckSum.generateCheckSum(merchantKey, JsonConvert.SerializeObject(body));

                    head.Add("signature", paytmChecksum);

                    requestBody.Add("body", body);
                    requestBody.Add("head", head);

                    string post_data = JsonConvert.SerializeObject(requestBody);
                    string url = "";
                    if (karyonPointsRequest.IsLive == 1)
                    {
                        url = "https://securegw.paytm.in/theia/api/v1/initiateTransaction?mid=" + MID + "&orderId=" + orderId + "";
                    }
                    else
                    {
                        url = "https://securegw-stage.paytm.in/theia/api/v1/initiateTransaction?mid=" + MID + "&orderId=" + orderId + "";
                    }

                    //For  Production 
                    //string  url  =  "https://securegw.paytm.in/theia/api/v1/initiateTransaction?mid=YOUR_MID_HERE&orderId=ORDERID_98765";

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
                        PaytmInitiateResponse deserializedProduct = JsonConvert.DeserializeObject<PaytmInitiateResponse>(responseData);
                        if (deserializedProduct.body.resultInfo.resultCode == "0000")
                        {
                            karyonPointsRequest.OrderNo = orderId;

                            DataSet dataSet = karyonPointsRequest.FranchisewalletRequest();
                            if (dataSet != null)
                            {
                                if (dataSet.Tables[0].Rows.Count > 0)
                                {
                                    if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                                    {


                                        objres.Message = "";
                                        objres.Status = 1;
                                        objres.OrderAmount = dataSet.Tables[0].Rows[0]["Amount"].ToString();
                                        objres.OrderNo = dataSet.Tables[0].Rows[0]["OrderNo"].ToString();
                                        objres.TxnToken = deserializedProduct.body.txnToken;
                                    }
                                    else
                                    {
                                        objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                                        objres.Status = 0;
                                    }
                                }
                                else
                                {
                                    objres.Message = Messages.ProblemInConnection;
                                    objres.Status = 0;

                                }

                            }
                            else
                            {
                                objres.Message = Messages.ProblemInConnection;
                                objres.Status = 0;

                            }
                        }
                        else
                        {
                            objres.Message = deserializedProduct.body.resultInfo.resultMsg;
                            objres.Status = 0;
                        }
                        Console.WriteLine(responseData);
                    }
                }
                else
                {

                    dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                    karyonPointsRequest = JsonConvert.DeserializeObject<FranchiseeKaryonPointsRequest>(dcdata);
                    karyonPointsRequest.TransactionDate = string.IsNullOrEmpty(karyonPointsRequest.TransactionDate) ? null : Common.ConvertToSystemDate(karyonPointsRequest.TransactionDate, "dd/MM/yyyy");
                    DataSet dataSet = karyonPointsRequest.FranchisewalletRequest();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                            {
                                List<FranchiseeKaryonPointsData> listResponses = new List<FranchiseeKaryonPointsData>();
                                for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                                {
                                    FranchiseeKaryonPointsData karyonPointsData = new FranchiseeKaryonPointsData();
                                    karyonPointsData.Amount = decimal.Parse(dataSet.Tables[0].Rows[i]["Amount"].ToString());
                                    karyonPointsData.RequestedDate = dataSet.Tables[0].Rows[i]["RequestedDate"].ToString();
                                    karyonPointsData.Status = dataSet.Tables[0].Rows[i]["Status"].ToString();
                                    karyonPointsData.TransactionDate = dataSet.Tables[0].Rows[i]["TransactionDate"].ToString();
                                    karyonPointsData.PaymentMode = dataSet.Tables[0].Rows[i]["PaymentMode"].ToString();
                                    karyonPointsData.TransactionNo = dataSet.Tables[0].Rows[i]["TransactionNo"].ToString();

                                    listResponses.Add(karyonPointsData);
                                }
                                karyonPoints.WalletList = listResponses;
                                objres.Message = "Karyon Points Request Completed";
                                objres.Status = 1;
                                objres.Response = karyonPoints;
                            }
                            else
                            {
                                objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                                objres.Status = 0;
                            }
                        }
                        else
                        {
                            objres.Message = Messages.NoRecordFound;
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.ProblemInConnection;
                        objres.Status = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FranchiseeKaryonPointsResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("ViewProfile")]
        [Produces("application/json")]
        public ResponseModel ViewProfile(RequestModel requestModel)
        {
            string EncryptedText = "";
            ProfileReposne objres = new ProfileReposne();
            ResponseModel returnResponse = new ResponseModel();
            try
            {

                ProfileRequest profileRequest = new ProfileRequest();
                Profile profile = new Profile();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                profileRequest = JsonConvert.DeserializeObject<ProfileRequest>(dcdata);


                DataSet dataSet = profileRequest.GetAssociateProfile();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        profile.LoginId = dataSet.Tables[0].Rows[0]["LoginId"].ToString();
                        profile.FirstName = dataSet.Tables[0].Rows[0]["FirstName"].ToString();
                        profile.LastName = dataSet.Tables[0].Rows[0]["LastName"].ToString();
                        profile.MiddleName = dataSet.Tables[0].Rows[0]["MiddleName"].ToString();
                        profile.MobileNo = dataSet.Tables[0].Rows[0]["MobileNo"].ToString();
                        profile.Gender = dataSet.Tables[0].Rows[0]["Gender"].ToString();
                        profile.State = dataSet.Tables[0].Rows[0]["StateName"].ToString();
                        profile.City = dataSet.Tables[0].Rows[0]["CitiesName"].ToString();
                        profile.Pincode = dataSet.Tables[0].Rows[0]["Pincode"].ToString();
                        profile.PanCard = dataSet.Tables[0].Rows[0]["PanCard"].ToString();
                        profile.Address = dataSet.Tables[0].Rows[0]["Address"].ToString();
                        profile.Email = dataSet.Tables[0].Rows[0]["Email"].ToString();
                        profile.BankName = dataSet.Tables[0].Rows[0]["BankName"].ToString();
                        profile.Branch = dataSet.Tables[0].Rows[0]["Branch"].ToString();
                        profile.PanImage = dataSet.Tables[0].Rows[0]["PanImage"].ToString();
                        profile.IFSC = dataSet.Tables[0].Rows[0]["IFSC"].ToString();
                        profile.AccNo = dataSet.Tables[0].Rows[0]["AccNo"].ToString();
                        profile.HolderName = dataSet.Tables[0].Rows[0]["HolderName"].ToString();
                        profile.RankName = dataSet.Tables[0].Rows[0]["RankName"].ToString();
                        profile.TotalOrders = dataSet.Tables[0].Rows[0]["TotalOrders"].ToString();
                        profile.JoiningDate = dataSet.Tables[0].Rows[0]["JoiningDate"].ToString();
                        profile.Status = dataSet.Tables[0].Rows[0]["Status"].ToString();
                        profile.SponsorId = dataSet.Tables[0].Rows[0]["SponsorId"].ToString();
                        profile.SponsorName = dataSet.Tables[0].Rows[0]["SponsorName"].ToString();
                        profile.PanStatus = dataSet.Tables[0].Rows[0]["PanStatus"].ToString();
                        profile.AddressFrontStatus = dataSet.Tables[0].Rows[0]["AddressFrontStatus"].ToString();
                        profile.AddressBackStatus = dataSet.Tables[0].Rows[0]["AddressBackStatus"].ToString();
                        profile.BankStatus = dataSet.Tables[0].Rows[0]["BankStatus"].ToString();
                        profile.AddressProofFrontURL = dataSet.Tables[0].Rows[0]["AddressProofFront"].ToString();
                        profile.AddressProofBackURL = dataSet.Tables[0].Rows[0]["AddressProofBack"].ToString();
                        profile.BankDocURL = dataSet.Tables[0].Rows[0]["BankDoc"].ToString();
                        profile.ProfilePicURL = dataSet.Tables[0].Rows[0]["ProfilePic"].ToString();
                        profile.GSTNo = dataSet.Tables[0].Rows[0]["GSTNo"].ToString();
                        profile.FirmName = dataSet.Tables[0].Rows[0]["FirmName"].ToString();
                        profile.IsBid = dataSet.Tables[0].Rows[0]["IsBid"].ToString();

                        objres.Message = "";
                        objres.Status = "1";
                        objres.Response = profile;

                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = "0";

                    }

                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = "0";

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = "0";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ProfileReposne));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("UpdateAssociateProfile")]
        [Produces("application/json")]
        public ResponseModel UpdateAssociateProfile(RequestModel requestModel)
        {
            string EncryptedText = "";
            ProfileReposne objres = new ProfileReposne();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                Profile profile = new Profile();
                ProfileRequest profileRequest = new ProfileRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                profileRequest = JsonConvert.DeserializeObject<ProfileRequest>(dcdata);
                DataSet dataSet = profileRequest.UpdateAssociateProfile();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            /* objres.Message = "Registration Completed.Your LoginId is " + dataSet.Tables[0].Rows[0]["LoginId"].ToString() + " and password is " + registration.Password*/

                            objres.Status = "1";
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();

                        }
                        else
                        {

                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = "0";
                        }


                    }
                    else
                    {
                        objres.Message = Messages.ProblemInConnection;
                        objres.Status = "0";

                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = "0";

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = "0";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ProfileReposne));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("KaryonPointsList")]
        [Produces("application/json")]
        public ResponseModel KaryonPointsList(RequestModel requestModel)
        {
            string EncryptedText = "";
            KaryonPointsResponse objres = new KaryonPointsResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                KaryonPointsRequest karyonPointsRequest = new KaryonPointsRequest();
                KaryonPoints karyonPoints = new KaryonPoints();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                karyonPointsRequest = JsonConvert.DeserializeObject<KaryonPointsRequest>(dcdata);


                DataSet dataSet = karyonPointsRequest.GetKaryonPointsList();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {

                        List<KaryonPointsData> listResponses = new List<KaryonPointsData>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            KaryonPointsData karyonPointsData = new KaryonPointsData();

                            karyonPointsData.Amount = decimal.Parse(dataSet.Tables[0].Rows[i]["Amount"].ToString());
                            karyonPointsData.PaymentMode = dataSet.Tables[0].Rows[i]["PaymentMode"].ToString();
                            karyonPointsData.TransactionNo = dataSet.Tables[0].Rows[i]["TransactionNo"].ToString();
                            karyonPointsData.TransactionDate = dataSet.Tables[0].Rows[i]["TransactionDate"].ToString();
                            karyonPointsData.RequestedDate = dataSet.Tables[0].Rows[i]["RequestedDate"].ToString();
                            karyonPointsData.Status = dataSet.Tables[0].Rows[i]["Status"].ToString();
                            listResponses.Add(karyonPointsData);
                        }
                        karyonPoints.RequestList = listResponses;
                        objres.Response = karyonPoints;
                        objres.Message = "";
                        objres.Status = 1;

                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }



            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(KaryonPointsResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("FranchiseRegistration")]
        [Produces("application/json")]
        public ResponseModel FranchiseRegistration(RequestModel requestModel)
        {
            string EncryptedText = "";
            FranchiseResponse objres = new FranchiseResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {

                FranchiseRegistrationRequest franchiseRegistrationRequest = new FranchiseRegistrationRequest();
                KaryonPoints karyonPoints = new KaryonPoints();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                franchiseRegistrationRequest = JsonConvert.DeserializeObject<FranchiseRegistrationRequest>(dcdata);


                DataSet dataSet = franchiseRegistrationRequest.FranchiseRegistration();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            Franchise franchise = new Franchise();
                            franchise.FranchiseId = long.Parse(dataSet.Tables[0].Rows[0]["FranchiseId"].ToString());
                            franchise.LoginId = dataSet.Tables[0].Rows[0]["LoginId"].ToString();
                            franchise.CompanyName = dataSet.Tables[0].Rows[0]["CompanyName"].ToString();
                            franchise.ContactPerson = dataSet.Tables[0].Rows[0]["ContactPerson"].ToString();
                            franchise.Password = dataSet.Tables[0].Rows[0]["Password"].ToString();
                            objres.Response = franchise;
                            objres.Message = "";
                            objres.Status = 1;
                            //Pushnotification pushnotification = new Pushnotification();
                            //if (dataSet.Tables[0].Rows.Count > 0)
                            //{
                            //    pushnotification.Notification(dataSet.Tables[1].Rows[0]["DeviceToken"].ToString(), "Franchisee Registration Completed.Your LoginId is " + dataSet.Tables[0].Rows[0]["LoginId"].ToString() + " and password is " + registration.Password, "Franchisee Registration", dataSet.Tables[1].Rows[0]["LoginFrom"].ToString(), "OrderList");
                            //}
                        }
                        else
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 0;

                        }
                    }
                    else
                    {
                        objres.Message = Messages.ProblemInConnection;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FranchiseResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("FranchiseRequest")]
        [Produces("application/json")]
        public ResponseModel FranchiseRequest(RequestModel requestModel)
        {
            string EncryptedText = "";
            FranchiseResponse objres = new FranchiseResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {

                FranchiseRegistrationRequest franchiseRegistrationRequest = new FranchiseRegistrationRequest();
                KaryonPoints karyonPoints = new KaryonPoints();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                franchiseRegistrationRequest = JsonConvert.DeserializeObject<FranchiseRegistrationRequest>(dcdata);


                DataSet dataSet = franchiseRegistrationRequest.FranchiseeRequest();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {

                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 0;

                        }


                    }
                    else
                    {
                        objres.Message = Messages.ProblemInConnection;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FranchiseResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("GetFranchiseSponsorName")]
        [Produces("application/json")]
        public ResponseModel GetFranchiseSponsorName(RequestModel requestModel)
        {
            string EncryptedText = "";
            SponsorResponse objres = new SponsorResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {

                SponsorRequest sponsorRequest = new SponsorRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                sponsorRequest = JsonConvert.DeserializeObject<SponsorRequest>(dcdata);
                DataSet dataSet = sponsorRequest.GetFranchiseSponsorName();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            Sponsor sponsor = new Sponsor();
                            sponsor.Name = dataSet.Tables[0].Rows[0]["Name"].ToString();
                            objres.Response = sponsor;
                            objres.Message = "";
                            objres.Status = 1;

                        }
                        else
                        {
                            objres.Message = Messages.InvalidSponsorId;
                            objres.Status = 0;

                        }


                    }
                    else
                    {
                        objres.Message = Messages.InvalidSponsorId;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.InvalidSponsorId;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(SponsorResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("GetMasterData")]
        [Produces("application/json")]
        public ResponseModel GetMasterData(RequestModel requestModel)
        {
            string EncryptedText = "";
            MasterDataResponse objres = new MasterDataResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                MasterDataRequest masterDataRequest = new MasterDataRequest();
                MasterData masterData = new MasterData();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                masterDataRequest = JsonConvert.DeserializeObject<MasterDataRequest>(dcdata);

                DataSet dataSet = masterDataRequest.RequestMasterData();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<GetMasterData> listResponses = new List<GetMasterData>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            GetMasterData getMasterData = new GetMasterData();
                            getMasterData.Pk_Id = dataSet.Tables[0].Rows[i]["Pk_Id"].ToString();
                            getMasterData.Name = dataSet.Tables[0].Rows[i]["Name"].ToString();
                            listResponses.Add(getMasterData);
                        }
                        masterData.RequestList = listResponses;
                        objres.Response = masterData;
                        objres.Message = "";
                        objres.Status = 1;
                    }
                    else
                    {
                        objres.Message = Messages.ProblemInConnection;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(MasterDataResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("PlaceOrder")]
        [Produces("application/json")]
        public ResponseModel PlaceOrder(RequestModel requestModel)
        {
            string EncryptedText = "";
            PlaceOrderResponse objres = new PlaceOrderResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                PlaceOrder placeOrder = new PlaceOrder();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                placeOrder = JsonConvert.DeserializeObject<PlaceOrder>(dcdata);
                DataSet dataSet = placeOrder.PlaceShoppingOrder();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            objres.Message = "";
                            objres.Status = 1;
                            objres.OrderAmount = dataSet.Tables[0].Rows[0]["OrderAmount"].ToString();
                            objres.OrderNo = dataSet.Tables[0].Rows[0]["OrderNo"].ToString();

                           

                            Pushnotification pushnotification = new Pushnotification();
                            //if (dataSet.Tables[0].Rows.Count > 0)
                            //{
                            //    pushnotification.Notification(dataSet.Tables[1].Rows[0]["DeviceToken"].ToString(), "Your Order No " + dataSet.Tables[0].Rows[0]["OrderNo"].ToString() + " Placed Seccussfully !", "Place Order", dataSet.Tables[1].Rows[0]["LoginFrom"].ToString(), "OrderList");
                            //}
                        }
                        else
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.ProblemInConnection;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(PlaceOrderResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("PlaceOrderMobile")]
        [Produces("application/json")]
        public ResponseModel PlaceOrderMobile(RequestModel requestModel)
        {
            string EncryptedText = "";
            PlaceOrderResponse objres = new PlaceOrderResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                PlaceOrder placeOrder = new PlaceOrder();
                String merchantKey = "";
                string MID = "";
                string orderId = DateTime.Now.ToString("ddMMyyyyhhMMss");
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                placeOrder = JsonConvert.DeserializeObject<PlaceOrder>(dcdata);

                if (placeOrder.WalletType == "Gateway")
                {
                    Dictionary<string, object> body = new Dictionary<string, object>();
                    Dictionary<string, string> head = new Dictionary<string, string>();
                    Dictionary<string, object> requestBody = new Dictionary<string, object>();

                    Dictionary<string, string> txnAmount = new Dictionary<string, string>();
                    txnAmount.Add("value", placeOrder.OrderAmount);
                    txnAmount.Add("currency", "INR");
                    Dictionary<string, string> userInfo = new Dictionary<string, string>();
                    userInfo.Add("custId", placeOrder.CustomerId.ToString());
                    body.Add("requestType", "Payment");
                    if (placeOrder.IsLive == 1)
                    {
                        body.Add("mid", Gateway.MID);
                        body.Add("websiteName", Gateway.WebSite);
                    }
                    else
                    {
                        body.Add("mid", Gateway.MIDLocal);
                        body.Add("websiteName", Gateway.WebSiteLocal);
                    }


                    body.Add("orderId", orderId);
                    body.Add("txnAmount", txnAmount);
                    body.Add("userInfo", userInfo);
                    if (placeOrder.IsLive == 1)
                    {
                        body.Add("callbackUrl", "https://securegw.paytm.in");
                    }
                    else
                    {
                        body.Add("callbackUrl", "https://securegw-stage.paytm.in");
                    }


                    if (placeOrder.IsLive == 1)
                    {
                        merchantKey = Gateway.MerchantId;
                        MID = Gateway.MID;
                    }
                    else
                    {
                        merchantKey = Gateway.MerchantIdLocal;
                        MID = Gateway.MIDLocal;
                    }
                    /*
                    * Generate checksum by parameters we have in body
                    * Find your Merchant Key in your Paytm Dashboard at https://dashboard.paytm.com/next/apikeys 
                    */
                    string paytmChecksum = Checksum.generateSignature(JsonConvert.SerializeObject(body), merchantKey);

                    //  string paytmChecksum = CheckSum.generateCheckSum(merchantKey, JsonConvert.SerializeObject(body));

                    head.Add("signature", paytmChecksum);

                    requestBody.Add("body", body);
                    requestBody.Add("head", head);

                    string post_data = JsonConvert.SerializeObject(requestBody);
                    string url = "";
                    if (placeOrder.IsLive == 1)
                    {
                        url = "https://securegw.paytm.in/theia/api/v1/initiateTransaction?mid=" + MID + "&orderId=" + orderId + "";
                    }
                    else
                    {
                        url = "https://securegw-stage.paytm.in/theia/api/v1/initiateTransaction?mid=" + MID + "&orderId=" + orderId + "";
                    }

                    //For  Production 
                    //string  url  =  "https://securegw.paytm.in/theia/api/v1/initiateTransaction?mid=YOUR_MID_HERE&orderId=ORDERID_98765";

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
                        PaytmInitiateResponse deserializedProduct = JsonConvert.DeserializeObject<PaytmInitiateResponse>(responseData);
                        if (deserializedProduct.body.resultInfo.resultCode == "0000")
                        {
                            placeOrder.OrderNo = orderId;

                            DataSet dataSet = placeOrder.PlaceOrderForMobile();
                            if (dataSet != null)
                            {
                                if (dataSet.Tables[0].Rows.Count > 0)
                                {
                                    if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                                    {


                                        objres.Message = "";
                                        objres.Status = 1;
                                        objres.OrderAmount = dataSet.Tables[0].Rows[0]["OrderAmount"].ToString();
                                        objres.OrderNo = dataSet.Tables[0].Rows[0]["OrderNo"].ToString();
                                        objres.TxnToken = deserializedProduct.body.txnToken;
                                    }
                                    else
                                    {
                                        objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                                        objres.Status = 0;
                                    }
                                }
                                else
                                {
                                    objres.Message = Messages.ProblemInConnection;
                                    objres.Status = 0;

                                }

                            }
                            else
                            {
                                objres.Message = Messages.ProblemInConnection;
                                objres.Status = 0;

                            }
                        }
                        else
                        {
                            objres.Message = deserializedProduct.body.resultInfo.resultMsg;
                            objres.Status = 0;
                        }
                        Console.WriteLine(responseData);
                    }
                }
                else
                {
                    placeOrder.OrderNo = orderId;

                    DataSet dataSet = placeOrder.PlaceOrderForMobile();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                            {


                                objres.Message = "";
                                objres.Status = 1;
                                objres.OrderAmount = dataSet.Tables[0].Rows[0]["OrderAmount"].ToString();
                                objres.OrderNo = dataSet.Tables[0].Rows[0]["OrderNo"].ToString();
                                objres.TxnToken = "";
                            }
                            else
                            {
                                objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                                objres.Status = 0;
                            }
                        }
                        else
                        {
                            objres.Message = Messages.ProblemInConnection;
                            objres.Status = 0;

                        }

                    }
                    else
                    {
                        objres.Message = Messages.ProblemInConnection;
                        objres.Status = 0;

                    }
                }





            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(PlaceOrderResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("GetPincodeFranchise")]
        [Produces("application/json")]
        public ResponseModel GetPincodeFranchise(RequestModel requestModel)
        {
            string EncryptedText = "";
            MasterDataResponse objres = new MasterDataResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {

                MasterDataRequest masterDataRequest = new MasterDataRequest();
                MasterData masterData = new MasterData();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                masterDataRequest = JsonConvert.DeserializeObject<MasterDataRequest>(dcdata);


                DataSet dataSet = masterDataRequest.RequestMasterData();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<GetMasterData> listResponses = new List<GetMasterData>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            GetMasterData getMasterData = new GetMasterData();

                            getMasterData.Pk_Id = dataSet.Tables[0].Rows[i]["Pk_Id"].ToString();
                            getMasterData.Name = dataSet.Tables[0].Rows[i]["Name"].ToString();
                            getMasterData.Mobile = dataSet.Tables[0].Rows[i]["Mobile"].ToString();

                            listResponses.Add(getMasterData);

                        }
                        masterData.RequestList = listResponses;
                        objres.Response = masterData;
                        objres.Message = "";
                        objres.Status = 1;
                    }
                    else
                    {
                        objres.Message = Messages.ProblemInConnection;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(MasterDataResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("UpdatePickupFranchisee")]
        [Produces("application/json")]
        public ResponseModel UpdatePickupFranchisee(RequestModel requestModel)
        {
            string EncryptedText = "";
            UpdatePickupFranchiseResponse objres = new UpdatePickupFranchiseResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {

                CreateOrder createOrder = new CreateOrder();

                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                createOrder = JsonConvert.DeserializeObject<CreateOrder>(dcdata);


                DataSet dataSet = createOrder.UpdatePickupFranchise();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                            {

                                objres.Message = "";
                                objres.Status = 1;
                            }
                            else
                            {
                                objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                                objres.Status = 0;

                            }
                        }
                        else
                        {
                            objres.Message = Messages.ProblemInConnection;
                            objres.Status = 0;

                        }


                    }
                    else
                    {
                        objres.Message = Messages.ProblemInConnection;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(UpdatePickupFranchiseResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("UpdatePickupFranchiseeForFranchiseeOrder")]
        [Produces("application/json")]
        public ResponseModel UpdatePickupFranchiseeForFranchiseeOrder(RequestModel requestModel)
        {
            string EncryptedText = "";
            UpdatePickupFranchiseResponse objres = new UpdatePickupFranchiseResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {

                CreateOrder createOrder = new CreateOrder();

                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                createOrder = JsonConvert.DeserializeObject<CreateOrder>(dcdata);


                DataSet dataSet = createOrder.UpdatePickupFranchiseForFranchiseOrder();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                            {

                                objres.Message = "";
                                objres.Status = 1;
                            }
                            else
                            {
                                objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                                objres.Status = 0;

                            }
                        }
                        else
                        {
                            objres.Message = Messages.ProblemInConnection;
                            objres.Status = 0;

                        }


                    }
                    else
                    {
                        objres.Message = Messages.ProblemInConnection;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(UpdatePickupFranchiseResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("BusinessReport")]
        [Produces("application/json")]
        public ResponseModel BusinessReport(RequestModel requestModel)
        {
            string EncryptedText = "";
            BusinessReposne objres = new BusinessReposne();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                Business business = new Business();
                BusinessReport businessReport = new BusinessReport();

                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                businessReport = JsonConvert.DeserializeObject<BusinessReport>(dcdata);
                businessReport.Size = SessionManager.Size;

                DataSet dataSet = businessReport.GetBusinessReport();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            List<BusinessData> listResponses = new List<BusinessData>();
                            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                BusinessData businessData = new BusinessData();
                                businessData.ClosingDate = dataSet.Tables[0].Rows[i]["ClosingDate"].ToString();
                                businessData.PayoutNo = dataSet.Tables[0].Rows[i]["PayoutNo"].ToString();
                                businessData.Harmony = decimal.Parse(dataSet.Tables[0].Rows[i]["Harmony"].ToString());
                                businessData.HarmonyPoints = decimal.Parse(dataSet.Tables[0].Rows[i]["HarmonyPoints"].ToString());
                                businessData.AllHormony = decimal.Parse(dataSet.Tables[0].Rows[i]["AllHormony"].ToString());
                                businessData.PrevLeft = decimal.Parse(dataSet.Tables[0].Rows[i]["PrevLeft"].ToString());
                                businessData.PrevRight = decimal.Parse(dataSet.Tables[0].Rows[i]["PrevRight"].ToString());
                                businessData.CurrLeft = decimal.Parse(dataSet.Tables[0].Rows[i]["CurrLeft"].ToString());
                                businessData.CurrRight = decimal.Parse(dataSet.Tables[0].Rows[i]["CurrRight"].ToString());
                                businessData.BalLeft = decimal.Parse(dataSet.Tables[0].Rows[i]["BalLeft"].ToString());
                                businessData.BalRight = decimal.Parse(dataSet.Tables[0].Rows[i]["BalRight"].ToString());

                                listResponses.Add(businessData);
                            }
                            business.BusinessList = listResponses;
                            objres.Response = business;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.NoRecordFound;
                            objres.Status = 0;

                        }


                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BusinessReposne));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("Business")]
        [Produces("application/json")]
        public ResponseModel Business(RequestModel requestModel)
        {
            string EncryptedText = "";
            BusinessReposne objres = new BusinessReposne();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                Business business = new Business();
                BusinessReport businessReport = new BusinessReport();

                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                businessReport = JsonConvert.DeserializeObject<BusinessReport>(dcdata);
                businessReport.Size = SessionManager.Size;

                DataSet dataSet = businessReport.GetBusiness();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            List<BusinessData> listResponses = new List<BusinessData>();
                            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                BusinessData businessData = new BusinessData();

                                businessData.PaidZoneA = decimal.Parse(dataSet.Tables[0].Rows[i]["PaidZoneA"].ToString());
                                businessData.PaidZoneB = decimal.Parse(dataSet.Tables[0].Rows[i]["PaidZoneB"].ToString());
                                businessData.ZoneABusiness = decimal.Parse(dataSet.Tables[0].Rows[i]["ZoneABusiness"].ToString());
                                businessData.ZoneBBusiness = decimal.Parse(dataSet.Tables[0].Rows[i]["ZoneBBusiness"].ToString());
                                businessData.EntryDate = dataSet.Tables[0].Rows[i]["EntryDate"].ToString();
                                businessData.TotalRecords = dataSet.Tables[0].Rows[i]["TotalRecord"].ToString();


                                listResponses.Add(businessData);
                            }
                            business.BusinessList = listResponses;
                            objres.Response = business;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.NoRecordFound;
                            objres.Status = 0;

                        }


                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BusinessReposne));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("AddUpdateAddress")]
        [Produces("application/json")]
        public ResponseModel AddAddress(RequestModel requestModel)
        {
            string EncryptedText = "";
            AddressResponse objres = new AddressResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {

                AddressRequest addressRequest = new AddressRequest();
                Address address = new Address();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                addressRequest = JsonConvert.DeserializeObject<AddressRequest>(dcdata);
                addressRequest.OpCode = addressRequest.Pk_AddressId == 0 ? 1 : 2;
                DataSet dataSet = addressRequest.ManageAddress();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            List<AddressList> listResponses = new List<AddressList>();
                            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                AddressList addressList = new AddressList();
                                addressList.Name = dataSet.Tables[0].Rows[i]["Name"].ToString();
                                addressList.MobileNo = dataSet.Tables[0].Rows[i]["MobileNo"].ToString();
                                addressList.Address = dataSet.Tables[0].Rows[i]["Address"].ToString();
                                addressList.Locality = dataSet.Tables[0].Rows[i]["Locality"].ToString();
                                addressList.Pincode = dataSet.Tables[0].Rows[i]["Pincode"].ToString();
                                addressList.State = dataSet.Tables[0].Rows[i]["State"].ToString();
                                addressList.City = dataSet.Tables[0].Rows[i]["City"].ToString();
                                addressList.Landmark = dataSet.Tables[0].Rows[i]["Landmark"].ToString();
                                addressList.AddressType = dataSet.Tables[0].Rows[i]["AddressType"].ToString();
                                addressList.Pk_AddressId = int.Parse(dataSet.Tables[0].Rows[i]["Pk_AddressId"].ToString());


                                listResponses.Add(addressList);
                            }
                            address.AddressList = listResponses;
                            objres.Response = address;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.NoRecordFound;
                            objres.Status = 0;

                        }


                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AddressResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("GetAddress")]
        [Produces("application/json")]
        public ResponseModel GetAddress(RequestModel requestModel)
        {
            string EncryptedText = "";
            AddressResponse objres = new AddressResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {

                AddressRequest addressRequest = new AddressRequest();
                Address address = new Address();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                addressRequest = JsonConvert.DeserializeObject<AddressRequest>(dcdata);
                addressRequest.OpCode = 4;
                DataSet dataSet = addressRequest.ManageAddress();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            List<AddressList> listResponses = new List<AddressList>();
                            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                AddressList addressList = new AddressList();
                                addressList.Pk_AddressId = int.Parse(dataSet.Tables[0].Rows[i]["Pk_AddressId"].ToString());
                                addressList.Name = dataSet.Tables[0].Rows[i]["Name"].ToString();
                                addressList.MobileNo = dataSet.Tables[0].Rows[i]["MobileNo"].ToString();
                                addressList.Address = dataSet.Tables[0].Rows[i]["Address"].ToString();
                                addressList.Locality = dataSet.Tables[0].Rows[i]["Locality"].ToString();
                                addressList.Pincode = dataSet.Tables[0].Rows[i]["Pincode"].ToString();
                                addressList.State = dataSet.Tables[0].Rows[i]["State"].ToString();
                                addressList.City = dataSet.Tables[0].Rows[i]["City"].ToString();
                                addressList.Landmark = dataSet.Tables[0].Rows[i]["Landmark"].ToString();
                                addressList.AddressType = dataSet.Tables[0].Rows[i]["AddressType"].ToString();


                                listResponses.Add(addressList);
                            }
                            address.AddressList = listResponses;
                            objres.Response = address;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.NoRecordFound;
                            objres.Status = 0;

                        }


                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AddressResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }



        [HttpPost("BusinessReportDetails")]
        [Produces("application/json")]
        public ResponseModel BusinessReportDetails(RequestModel requestModel)
        {
            string EncryptedText = "";
            BusinessReposne objres = new BusinessReposne();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                Business business = new Business();
                BusinessReport businessReport = new BusinessReport();

                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                businessReport = JsonConvert.DeserializeObject<BusinessReport>(dcdata);
                businessReport.EntryDate = Common.ConvertToSystemDate(businessReport.EntryDate, "dd/MM/yyyy");

                DataSet dataSet = businessReport.GetBusinessReportDetails();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            List<BusinessData> listResponses = new List<BusinessData>();
                            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                BusinessData businessData = new BusinessData();

                                businessData.Name = dataSet.Tables[0].Rows[i]["Name"].ToString();
                                businessData.LoginId = dataSet.Tables[0].Rows[i]["LoginId"].ToString();
                                businessData.ZoneABusiness = decimal.Parse(dataSet.Tables[0].Rows[i]["ZoneABusiness"].ToString());
                                businessData.ZoneBBusiness = decimal.Parse(dataSet.Tables[0].Rows[i]["ZoneBBusiness"].ToString());



                                listResponses.Add(businessData);
                            }
                            business.BusinessList = listResponses;
                            objres.Response = business;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.NoRecordFound;
                            objres.Status = 0;

                        }


                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BusinessReposne));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("FranchiseViewProfile")]
        [Produces("application/json")]
        public ResponseModel FranchiseViewProfile(RequestModel requestModel)
        {
            string EncryptedText = "";
            ProfileReposne objres = new ProfileReposne();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                ProfileRequest profileRequest = new ProfileRequest();
                Profile profile = new Profile();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                profileRequest = JsonConvert.DeserializeObject<ProfileRequest>(dcdata);


                DataSet dataSet = profileRequest.ViewFranchiseeProfile();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        profile.LoginId = dataSet.Tables[0].Rows[0]["LoginId"].ToString();
                        profile.ContactPerson = dataSet.Tables[0].Rows[0]["ContactPerson"].ToString();
                        profile.CompanyName = dataSet.Tables[0].Rows[0]["CompanyName"].ToString();
                        profile.MobileNo = dataSet.Tables[0].Rows[0]["Mobile"].ToString();
                        //profile.Gender = dataSet.Tables[0].Rows[0]["Gender"].ToString();
                        profile.State = dataSet.Tables[0].Rows[0]["StateName"].ToString();
                        profile.City = dataSet.Tables[0].Rows[0]["City"].ToString();
                        profile.Pincode = dataSet.Tables[0].Rows[0]["PinCode"].ToString();
                        profile.PanCard = dataSet.Tables[0].Rows[0]["PanCard"].ToString();
                        profile.Address = dataSet.Tables[0].Rows[0]["Address"].ToString();
                        profile.Email = dataSet.Tables[0].Rows[0]["Email"].ToString();
                        profile.BankName = dataSet.Tables[0].Rows[0]["BankName"].ToString();
                        profile.Branch = dataSet.Tables[0].Rows[0]["Branch"].ToString();
                        profile.IFSC = dataSet.Tables[0].Rows[0]["IFSC"].ToString();
                        profile.AccNo = dataSet.Tables[0].Rows[0]["AccNo"].ToString();
                        profile.HolderName = dataSet.Tables[0].Rows[0]["HolderName"].ToString();
                        // profile.RankName = dataSet.Tables[0].Rows[0]["RankName"].ToString();
                        profile.TotalOrders = dataSet.Tables[0].Rows[0]["TotalOrders"].ToString();
                        profile.JoiningDate = dataSet.Tables[0].Rows[0]["AddedOn"].ToString();
                        // profile.Status = dataSet.Tables[0].Rows[0]["Status"].ToString();
                        profile.PanStatus = dataSet.Tables[0].Rows[0]["PanStatus"].ToString();
                        profile.AddressFrontStatus = dataSet.Tables[0].Rows[0]["AddressFrontStatus"].ToString();
                        profile.AddressBackStatus = dataSet.Tables[0].Rows[0]["AddressBackStatus"].ToString();
                        profile.BankStatus = dataSet.Tables[0].Rows[0]["BankStatus"].ToString();
                        profile.GSTNo = dataSet.Tables[0].Rows[0]["GSTNo"].ToString();
                        profile.Pk_FranchiseId = dataSet.Tables[0].Rows[0]["Pk_FranchiseId"].ToString();
                        profile.PendingOrderCount = dataSet.Tables[0].Rows[0]["PendingOrderCount"].ToString();
                        if (dataSet.Tables[0].Rows[0]["PanImage"].ToString() != "")
                        {
                            profile.PanImage = BaseUrl.LeftRightURL + dataSet.Tables[0].Rows[0]["PanImage"].ToString();
                        }
                        if (dataSet.Tables[0].Rows[0]["AddressProofFront"].ToString() != "")
                        {
                            profile.AddressProofFrontURL = BaseUrl.LeftRightURL + dataSet.Tables[0].Rows[0]["AddressProofFront"].ToString();
                        }
                        if (dataSet.Tables[0].Rows[0]["AddressProofBack"].ToString() != "")
                        {
                            profile.AddressProofBackURL = BaseUrl.LeftRightURL + dataSet.Tables[0].Rows[0]["AddressProofBack"].ToString();
                        }
                        if (dataSet.Tables[0].Rows[0]["BankDoc"].ToString() != "")
                        {
                            profile.BankDocURL = BaseUrl.LeftRightURL + dataSet.Tables[0].Rows[0]["BankDoc"].ToString();
                        }
                        if (dataSet.Tables[0].Rows[0]["ProfilePic"].ToString() != "")
                        {
                            profile.ProfilePicURL = BaseUrl.LeftRightURL + dataSet.Tables[0].Rows[0]["ProfilePic"].ToString();
                        }

                        objres.Message = "";
                        objres.Status = "1";
                        objres.Response = profile;

                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = "0";

                    }

                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = "0";

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = "0";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ProfileReposne));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("UpdateFranchiseProfile")]
        [Produces("application/json")]
        public ResponseModel UpdateFranchiseProfile(RequestModel requestModel)
        {
            string EncryptedText = "";
            ProfileReposne objres = new ProfileReposne();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                Profile profile = new Profile();
                ProfileRequest profileRequest = new ProfileRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                profileRequest = JsonConvert.DeserializeObject<ProfileRequest>(dcdata);
                DataSet dataSet = profileRequest.UpdateFranchiseProfile();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            /* objres.Message = "Registration Completed.Your LoginId is " + dataSet.Tables[0].Rows[0]["LoginId"].ToString() + " and password is " + registration.Password*/

                            objres.Status = "1";
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();

                        }
                        else
                        {

                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = "0";
                        }


                    }
                    else
                    {
                        objres.Message = Messages.ProblemInConnection;
                        objres.Status = "0";

                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = "0";

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = "0";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ProfileReposne));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("GetOrderList")]
        [Produces("application/json")]
        public ResponseModel GetOrderList(RequestModel requestModel)
        {
            string EncryptedText = "";
            CreateOrderResponse objres = new CreateOrderResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {

                CreateOrder createOrder = new CreateOrder();
                OrderListResponse orderListResponse = new OrderListResponse();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                createOrder = JsonConvert.DeserializeObject<CreateOrder>(dcdata);
                createOrder.OpCode = 1;
                createOrder.Size = 30;
                DataSet dataSet = createOrder.GetCustomerOrders();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<OrderData> listResponses = new List<OrderData>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            OrderData orderData = new OrderData();
                            orderData.Fk_OrderId = dataSet.Tables[0].Rows[i]["Pk_OrderId"].ToString();
                            orderData.OrderNo = dataSet.Tables[0].Rows[i]["OrderNo"].ToString();
                            orderData.OrderDate = dataSet.Tables[0].Rows[i]["OrderDate"].ToString();
                            orderData.OrderAmount = decimal.Parse(dataSet.Tables[0].Rows[i]["OrderAmount"].ToString());
                            orderData.TotalPV = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalPV"].ToString());
                            orderData.OrderType = dataSet.Tables[0].Rows[i]["OrderType"].ToString();
                            orderData.Status = dataSet.Tables[0].Rows[i]["Status"].ToString();
                            orderData.Fk_DispachFranchiseId = int.Parse(dataSet.Tables[0].Rows[i]["Fk_DispachFranchiseId"].ToString());
                            orderData.IsCancel = dataSet.Tables[0].Rows[i]["IsCancel"].ToString() == "1" ? true : false; ;

                            listResponses.Add(orderData);
                        }
                        orderListResponse.OrderList = listResponses;
                        objres.Response = orderListResponse;
                        objres.Message = "";
                        objres.Status = 1;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CreateOrderResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("AssociateDailyPayotReport")]
        [Produces("application/json")]
        public ResponseModel AssociateDailyPayotReport(RequestModel requestModel)
        {
            string EncryptedText = "";
            AssociateReportResponse objres = new AssociateReportResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                Associate associate = new Associate();
                AssociateReport associateReport = new AssociateReport();

                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                associateReport = JsonConvert.DeserializeObject<AssociateReport>(dcdata);
                associateReport.Size = SessionManager.Size;
                DataSet dataSet = associateReport.GetAssociatePayoutReport();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            List<AssociateData> listResponses = new List<AssociateData>();
                            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                AssociateData associateData = new AssociateData();
                                associateData.ClosingDate = dataSet.Tables[0].Rows[i]["ClosingDate"].ToString();
                                associateData.PayoutNo = dataSet.Tables[0].Rows[i]["PayoutNo"].ToString();
                                associateData.Creator = dataSet.Tables[0].Rows[i]["Creator"].ToString();
                                associateData.Harmony = dataSet.Tables[0].Rows[i]["Harmony"].ToString();
                                associateData.Advisor1 = dataSet.Tables[0].Rows[i]["Advisor1"].ToString();
                                associateData.Royal = dataSet.Tables[0].Rows[i]["Royal"].ToString();
                                associateData.CreatorHarmony = dataSet.Tables[0].Rows[i]["CreatorHarmony"].ToString();
                                associateData.HarmonyPoints = dataSet.Tables[0].Rows[i]["HarmonyPoints"].ToString();
                                associateData.GrossAmount = dataSet.Tables[0].Rows[i]["GrossAmount"].ToString();
                                associateData.TDSAmount = dataSet.Tables[0].Rows[i]["TDSAmount"].ToString();
                                associateData.ProcessingFee = dataSet.Tables[0].Rows[i]["ProcessingFee"].ToString();
                                associateData.NetAmount = dataSet.Tables[0].Rows[i]["NetAmount"].ToString();
                                associateData.IsClosed = dataSet.Tables[0].Rows[i]["isClosed"].ToString();
                                associateData.TotalRecords = int.Parse(dataSet.Tables[0].Rows[i]["TotalRecords"].ToString());

                                listResponses.Add(associateData);
                            }
                            associate.AssociateList = listResponses;
                            objres.Response = associate;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.NoRecordFound;
                            objres.Status = 0;

                        }


                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AssociateReportResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("AssociateWeeklyPayotReport")]
        [Produces("application/json")]
        public ResponseModel AssociateWeeklyPayotReport(RequestModel requestModel)
        {
            string EncryptedText = "";
            AssociateReportResponse objres = new AssociateReportResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                Associate associate = new Associate();
                AssociateReport associateReport = new AssociateReport();

                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                associateReport = JsonConvert.DeserializeObject<AssociateReport>(dcdata);

                associateReport.Size = SessionManager.Size;
                DataSet dataSet = associateReport.GetWeeklyAssociatePayoutReport();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            List<AssociateData> listResponses = new List<AssociateData>();
                            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                AssociateData associateData = new AssociateData();
                                associateData.ClosingDate = dataSet.Tables[0].Rows[i]["ClosingDate"].ToString();
                                associateData.PayoutNo = dataSet.Tables[0].Rows[i]["PayoutNo"].ToString();
                                associateData.Creator = dataSet.Tables[0].Rows[i]["Creator"].ToString();
                                associateData.Harmony = dataSet.Tables[0].Rows[i]["Harmony"].ToString();
                                associateData.Advisor1 = dataSet.Tables[0].Rows[i]["Advisor1"].ToString();
                                associateData.CreatorHarmony = dataSet.Tables[0].Rows[i]["CreatorHarmony"].ToString();
                                associateData.Smart = dataSet.Tables[0].Rows[i]["Smart"].ToString();
                                associateData.Leadership = dataSet.Tables[0].Rows[i]["Leadership"].ToString();
                                associateData.Royal = dataSet.Tables[0].Rows[i]["Royal"].ToString();
                                associateData.GrossAmount = dataSet.Tables[0].Rows[i]["GrossAmount"].ToString();
                                associateData.TDSAmount = dataSet.Tables[0].Rows[i]["TDSAmount"].ToString();
                                associateData.ProcessingFee = dataSet.Tables[0].Rows[i]["ProcessingFee"].ToString();
                                associateData.NetAmount = dataSet.Tables[0].Rows[i]["NetAmount"].ToString();
                                associateData.TotalRecords = int.Parse(dataSet.Tables[0].Rows[i]["TotalRecord"].ToString());

                                listResponses.Add(associateData);
                            }
                            associate.AssociateList = listResponses;
                            objres.Response = associate;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.NoRecordFound;
                            objres.Status = 0;

                        }


                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AssociateReportResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("LoginByAdmin")]
        [Produces("application/json")]
        public ResponseModel LoginByAdmin(RequestModel requestModel)
        {
            string EncryptedText = "";
            LoginResponse objres = new LoginResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {

                LoginRequest loginRequest = new LoginRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                loginRequest = JsonConvert.DeserializeObject<LoginRequest>(dcdata);
                DataSet dataSet = loginRequest.LoginByAdmin();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {

                            Login login = new Login();
                            login.CustomerId = long.Parse(dataSet.Tables[0].Rows[0]["CustomerId"].ToString());
                            login.FirstName = dataSet.Tables[0].Rows[0]["FirstName"].ToString();
                            login.LastName = dataSet.Tables[0].Rows[0]["LastName"].ToString();
                            login.LoginId = dataSet.Tables[0].Rows[0]["LoginId"].ToString();
                            login.UserType = dataSet.Tables[0].Rows[0]["UserType"].ToString();
                            login.TotalOrder = int.Parse(dataSet.Tables[0].Rows[0]["TotalOrder"].ToString());
                            login.RegistrationAmount = decimal.Parse(dataSet.Tables[0].Rows[0]["RegistrationAmount"].ToString());
                            login.FK_FranchiseTypeId = long.Parse(dataSet.Tables[0].Rows[0]["FK_FranchiseTypeId"].ToString());
                            objres.Response = login;
                            objres.Message = "";
                            objres.Status = 1;

                        }
                        else
                        {
                            objres.Message = Messages.InvalidLoginId;
                            objres.Status = 0;

                        }


                    }
                    else
                    {
                        objres.Message = Messages.InvalidLoginId;
                        objres.Status = 0;

                    }
                }
                else
                {
                    objres.Message = Messages.InvalidLoginId;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(LoginResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("CounterCollect")]
        [Produces("application/json")]
        public ResponseModel CounterCollect(RequestModel requestModel)
        {
            string EncryptedText = "";
            CounterCollectResponse objres = new CounterCollectResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                CreateOrder createOrder = new CreateOrder();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                createOrder = JsonConvert.DeserializeObject<CreateOrder>(dcdata);
                DataSet dataSet = createOrder.CounterPickup();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            objres.Message = "";
                            objres.Status = 1;

                        }
                        else
                        {
                            objres.Message = Messages.InvalidLoginId;
                            objres.Status = 0;

                        }
                    }
                    else
                    {
                        objres.Message = Messages.InvalidLoginId;
                        objres.Status = 0;

                    }
                }
                else
                {
                    objres.Message = Messages.InvalidLoginId;
                    objres.Status = 0;

                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CounterCollectResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("AssociateLedger")]
        [Produces("application/json")]
        public ResponseModel AssociateLedger(RequestModel requestModel)
        {
            string EncryptedText = "";
            AssociateWalletResponse objres = new AssociateWalletResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                AssociateWallet associateWallet = new AssociateWallet();
                AssociateWalletRequest associateWalletRequest = new AssociateWalletRequest();

                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                associateWalletRequest = JsonConvert.DeserializeObject<AssociateWalletRequest>(dcdata);
                associateWalletRequest.Size = SessionManager.Size;
                DataSet dataSet = associateWalletRequest.GetAssociateLedger();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            List<AssociateWalletData> listResponses = new List<AssociateWalletData>();
                            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                AssociateWalletData walletData = new AssociateWalletData();
                                walletData.OrderNo = dataSet.Tables[0].Rows[i]["OrderNo"].ToString();
                                walletData.TransactionDate = dataSet.Tables[0].Rows[i]["TransactionDate"].ToString();
                                walletData.Narration = dataSet.Tables[0].Rows[i]["Narration"].ToString();
                                walletData.Cramount = decimal.Parse(dataSet.Tables[0].Rows[i]["Cramount"].ToString());
                                walletData.Dramount = decimal.Parse(dataSet.Tables[0].Rows[i]["Dramount"].ToString());
                                walletData.Balance = decimal.Parse(dataSet.Tables[0].Rows[i]["Balance"].ToString());
                                listResponses.Add(walletData);
                            }
                            associateWallet.AssociateWalletList = listResponses;
                            objres.Response = associateWallet;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.NoRecordFound;
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AssociateWalletResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("AssociateWalletBalance")]
        [Produces("application/json")]
        public ResponseModel AssociateWalletBalance(RequestModel requestModel)
        {
            string EncryptedText = "";
            WalletBalanceResponse objres = new WalletBalanceResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                WalletBalance associateWallet = new WalletBalance();
                AssociateWalletRequest associateWalletRequest = new AssociateWalletRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                associateWalletRequest = JsonConvert.DeserializeObject<AssociateWalletRequest>(dcdata);

                DataSet dataSet = associateWalletRequest.GetAssociateWalletBalance();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {


                            WalletBalanceData walletData = new WalletBalanceData();
                            walletData.Balance = decimal.Parse(dataSet.Tables[0].Rows[0]["WalletBalance"].ToString());
                            walletData.FUPWallet = decimal.Parse(dataSet.Tables[0].Rows[0]["FUPWallet"].ToString());
                            walletData.OFPWallet = decimal.Parse(dataSet.Tables[0].Rows[0]["OFPWallet"].ToString());

                            associateWallet.WalletBalanceData = walletData;
                            objres.Response = associateWallet;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.NoRecordFound;
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(WalletBalanceResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;


        }

        [HttpPost("GetAssociateOrderDetails")]
        [Produces("application/json")]
        public ResponseModel GetAssociateOrderDetails(RequestModel requestModel)
        {
            string EncryptedText = "";
            CustomerOrderResponse objres = new CustomerOrderResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                CustomerOrderRequest orderRequest = new CustomerOrderRequest();

                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                orderRequest = JsonConvert.DeserializeObject<CustomerOrderRequest>(dcdata);
                DataSet dataSet = orderRequest.GetOrderDetails();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            CustomerOrder customer = new CustomerOrder();
                            List<CustomerOrderData> lstCustomerOrder = new List<CustomerOrderData>();
                            customer.Fk_OrderId = dataSet.Tables[0].Rows[0]["Fk_OrderId"].ToString();
                            customer.OrderNo = dataSet.Tables[0].Rows[0]["OrderNo"].ToString();
                            customer.OrderType = dataSet.Tables[0].Rows[0]["OrderType"].ToString();
                            customer.OrderDate = dataSet.Tables[0].Rows[0]["OrderDate"].ToString();
                            customer.OrderAmount = decimal.Parse(dataSet.Tables[0].Rows[0]["OrderAmount"].ToString());
                            customer.Status = dataSet.Tables[0].Rows[0]["Status"].ToString();
                            customer.Name = dataSet.Tables[0].Rows[0]["Name"].ToString();
                            customer.Address = dataSet.Tables[0].Rows[0]["Address"].ToString();
                            customer.City = dataSet.Tables[0].Rows[0]["City"].ToString();
                            customer.Pincode = dataSet.Tables[0].Rows[0]["Pincode"].ToString();
                            customer.ShippingCharges = dataSet.Tables[0].Rows[0]["ShippingCharges"].ToString();
                            customer.CounterOTP = dataSet.Tables[0].Rows[0]["CounterOTP"].ToString();
                            for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                            {
                                CustomerOrderData customerOrder = new CustomerOrderData();

                                customerOrder.Fk_VarientId = dataSet.Tables[0].Rows[j]["Fk_VarientId"].ToString();
                                customerOrder.ProductName = dataSet.Tables[0].Rows[j]["ProductName"].ToString();
                                customerOrder.Quantity = int.Parse(dataSet.Tables[0].Rows[j]["Quantity"].ToString());
                                customerOrder.Unit = dataSet.Tables[0].Rows[j]["Unit"].ToString();
                                customerOrder.PV = decimal.Parse(dataSet.Tables[0].Rows[j]["PV"].ToString());
                                customerOrder.MRP = decimal.Parse(dataSet.Tables[0].Rows[j]["MRP"].ToString());
                                customerOrder.GST = decimal.Parse(dataSet.Tables[0].Rows[j]["GST"].ToString());
                                customerOrder.Image = dataSet.Tables[0].Rows[j]["Image"].ToString();

                                customerOrder.Fk_DispachFranchiseId = dataSet.Tables[0].Rows[j]["Fk_DispachFranchiseId"].ToString();

                                RatingList ratingList = new RatingList();
                                List<RatingList> lstRatingData = new List<RatingList>();
                                if (dataSet.Tables[1].Rows.Count > 0)
                                {
                                    for (int m = 0; m <= dataSet.Tables[1].Rows.Count - 1; m++)
                                    {
                                        if (dataSet.Tables[0].Rows[j]["ProductId"].ToString() == dataSet.Tables[1].Rows[m]["ProductId"].ToString())
                                        {
                                            ratingList.Star = long.Parse(dataSet.Tables[1].Rows[m]["Star"].ToString());
                                            ratingList.Rating = dataSet.Tables[1].Rows[m]["Rating"].ToString();
                                            ratingList.AddedOn = dataSet.Tables[1].Rows[m]["AddedOn"].ToString();
                                            lstRatingData.Add(ratingList);
                                        }
                                    }
                                }
                                customerOrder.RatingList = lstRatingData;
                                lstCustomerOrder.Add(customerOrder);
                            }

                            customer.OrderDetailsList = lstCustomerOrder;
                            objres.Response = customer;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.InvalidOrderNo;
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.InvalidOrderNo;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.InvalidOrderNo;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CustomerOrderResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("ProductsReview")]
        [Produces("application/json")]
        public ResponseModel ProductsReview(RequestModel requestModel)
        {
            string EncryptedText = "";
            ProductReviewResponse objres = new ProductReviewResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                ProductReviewRequest reviewRequest = new ProductReviewRequest();
                ProductReview productReview = new ProductReview();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                reviewRequest = JsonConvert.DeserializeObject<ProductReviewRequest>(dcdata);
                DataSet dataSet = reviewRequest.AddProductReview();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            List<ProductReviewData> lstProductReview = new List<ProductReviewData>();
                            for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                            {
                                ProductReviewData review = new ProductReviewData();

                                review.LoginId = dataSet.Tables[0].Rows[j]["LoginId"].ToString();
                                review.ContactName = dataSet.Tables[0].Rows[j]["ContactName"].ToString();
                                review.ProductName = dataSet.Tables[0].Rows[j]["ProductName"].ToString();
                                review.Star = decimal.Parse(dataSet.Tables[0].Rows[j]["Star"].ToString());
                                review.Rating = dataSet.Tables[0].Rows[j]["Rating"].ToString();
                                review.Fk_OrderId = int.Parse(dataSet.Tables[0].Rows[j]["Fk_OrderId"].ToString());
                                lstProductReview.Add(review);
                            }
                            productReview.ReviewList = lstProductReview;
                            objres.Response = productReview;
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.InvalidLoginId;
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.InvalidLoginId;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.InvalidLoginId;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ProductReviewResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("BinaryTree")]
        [Produces("application/json")]
        public ResponseModel BinaryTree(RequestModel requestModel)
        {
            string EncryptedText = "";
            BinaryTreeResponse objres = new BinaryTreeResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                BinaryTreeRequest reviewRequest = new BinaryTreeRequest();
                BinaryTree binaryTree = new BinaryTree();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                reviewRequest = JsonConvert.DeserializeObject<BinaryTreeRequest>(dcdata);
                DataSet dataSet = reviewRequest.Tree();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            List<BinaryTreeData> lstBinaryTree = new List<BinaryTreeData>();
                            for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                            {
                                BinaryTreeData tree = new BinaryTreeData();

                                tree.Fk_MemId = dataSet.Tables[0].Rows[j]["Fk_MemId"].ToString();
                                tree.Fk_SponorId = dataSet.Tables[0].Rows[j]["Fk_SponorId"].ToString();
                                tree.Fk_ParentId = dataSet.Tables[0].Rows[j]["Fk_ParentId"].ToString();
                                tree.LoginId = dataSet.Tables[0].Rows[j]["LoginId"].ToString();
                                tree.Name = dataSet.Tables[0].Rows[j]["Name"].ToString();
                                tree.Leg = dataSet.Tables[0].Rows[j]["Leg"].ToString();
                                tree.Level = dataSet.Tables[0].Rows[j]["Level"].ToString();
                                tree.Status = dataSet.Tables[0].Rows[j]["Status"].ToString();
                                tree.PermanentDate = dataSet.Tables[0].Rows[j]["PermanentDate"].ToString();
                                tree.JoiningDate = dataSet.Tables[0].Rows[j]["JoiningDate"].ToString();
                                tree.SponsorLoginId = dataSet.Tables[0].Rows[j]["SponsorLoginId"].ToString();
                                tree.SponsorName = dataSet.Tables[0].Rows[j]["SponsorName"].ToString();
                                tree.AllLeg1 = dataSet.Tables[0].Rows[j]["AllLeg1"].ToString();
                                tree.AllLeg2 = dataSet.Tables[0].Rows[j]["AllLeg2"].ToString();
                                tree.PermanentLeg1 = dataSet.Tables[0].Rows[j]["PermanentLeg1"].ToString();
                                tree.PermanentLeg2 = dataSet.Tables[0].Rows[j]["PermanentLeg2"].ToString();
                                tree.InactiveLeft = dataSet.Tables[0].Rows[j]["InactiveLeft"].ToString();
                                tree.InactiveRight = dataSet.Tables[0].Rows[j]["InactiveRight"].ToString();
                                tree.ImageURL = dataSet.Tables[0].Rows[j]["ImageURL"].ToString();
                                tree.PcountLeg1 = decimal.Parse(dataSet.Tables[0].Rows[j]["PcountLeg1"].ToString());
                                tree.PCountLeg2 = decimal.Parse(dataSet.Tables[0].Rows[j]["PCountLeg2"].ToString());
                                tree.PaidLeg1 = decimal.Parse(dataSet.Tables[0].Rows[j]["PaidLeg1"].ToString());
                                tree.PaidLeg2 = decimal.Parse(dataSet.Tables[0].Rows[j]["PaidLeg2"].ToString());
                                lstBinaryTree.Add(tree);
                            }
                            binaryTree.TreeList = lstBinaryTree;
                            objres.Response = binaryTree;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.NoRecordFound;
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BinaryTreeResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("HarmonyPayoutReport")]
        [Produces("application/json")]
        public ResponseModel HarmonyPayoutReport(RequestModel requestModel)
        {
            string EncryptedText = "";
            AssociateReportResponse objres = new AssociateReportResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                Associate associate = new Associate();
                AssociateReport associateReport = new AssociateReport();

                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                associateReport = JsonConvert.DeserializeObject<AssociateReport>(dcdata);


                DataSet dataSet = associateReport.GetAllIncomeReport();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            List<AssociateData> listResponses = new List<AssociateData>();
                            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                AssociateData associateData = new AssociateData();
                                associateData.ClosingDate = dataSet.Tables[0].Rows[i]["ClosingDate"].ToString();
                                associateData.PayoutNo = dataSet.Tables[0].Rows[i]["PayoutNo"].ToString();
                                associateData.Harmony = dataSet.Tables[0].Rows[i]["Harmony"].ToString();
                                associateData.TotalRecords = int.Parse(dataSet.Tables[0].Rows[i]["TotalRecords"].ToString());
                                listResponses.Add(associateData);
                            }
                            associate.AssociateList = listResponses;
                            objres.Response = associate;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.NoRecordFound;
                            objres.Status = 0;

                        }


                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AssociateReportResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("CreatorPayoutReport")]
        [Produces("application/json")]
        public ResponseModel CreatorPayoutReport(RequestModel requestModel)
        {
            string EncryptedText = "";
            AssociateReportResponse objres = new AssociateReportResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                Associate associate = new Associate();
                AssociateReport associateReport = new AssociateReport();

                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                associateReport = JsonConvert.DeserializeObject<AssociateReport>(dcdata);
                associateReport.Size = SessionManager.Size;

                DataSet dataSet = associateReport.GetAllIncomeReport();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            List<AssociateData> listResponses = new List<AssociateData>();
                            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                            {

                                AssociateData associateData = new AssociateData();
                                if (associateReport.Type == "Creator")
                                {
                                    associateData.LoginId = dataSet.Tables[0].Rows[i]["LoginId"].ToString();
                                    associateData.Name = dataSet.Tables[0].Rows[i]["Name"].ToString();
                                    associateData.OrderNo = dataSet.Tables[0].Rows[i]["OrderNo"].ToString();
                                    associateData.PayStatus = dataSet.Tables[0].Rows[i]["PayStatus"].ToString();
                                    associateData.BusinessAmount = dataSet.Tables[0].Rows[i]["BusinessAmount"].ToString();
                                    associateData.BusinessDate = dataSet.Tables[0].Rows[i]["BusinessDate"].ToString();
                                    associateData.ClosingDate = dataSet.Tables[0].Rows[i]["ClosingDate"].ToString();
                                    associateData.PayoutNo = dataSet.Tables[0].Rows[i]["PayoutNo"].ToString();
                                    associateData.Creator = dataSet.Tables[0].Rows[i]["Income"].ToString();
                                    associateData.TotalRecords = int.Parse(dataSet.Tables[0].Rows[i]["TotalRecords"].ToString());
                                }
                                else
                                {
                                    associateData.ClosingDate = dataSet.Tables[0].Rows[i]["ClosingDate"].ToString();
                                    associateData.PayoutNo = dataSet.Tables[0].Rows[i]["PayoutNo"].ToString();
                                    associateData.Creator = dataSet.Tables[0].Rows[i]["Income"].ToString();
                                    associateData.PrevLeft = dataSet.Tables[0].Rows[i]["PrevLeft"].ToString();
                                    associateData.PrevRight = dataSet.Tables[0].Rows[i]["PrevRight"].ToString();
                                    associateData.CurrLeft = dataSet.Tables[0].Rows[i]["CurrLeft"].ToString();
                                    associateData.CurrRight = dataSet.Tables[0].Rows[i]["CurrRight"].ToString();
                                    associateData.BalLeft = dataSet.Tables[0].Rows[i]["BalLeft"].ToString();
                                    associateData.BalRight = dataSet.Tables[0].Rows[i]["BalRight"].ToString();
                                    associateData.TotalRecords = int.Parse(dataSet.Tables[0].Rows[i]["TotalRecords"].ToString());
                                }

                                //associateData.Harmony = dataSet.Tables[0].Rows[i]["Harmony"].ToString();
                                //associateData.Advisor1 = dataSet.Tables[0].Rows[i]["Advisor1"].ToString();
                                //associateData.Advisor2 = dataSet.Tables[0].Rows[i]["Advisor2"].ToString();
                                //associateData.Advisor3 = dataSet.Tables[0].Rows[i]["Advisor3"].ToString();
                                listResponses.Add(associateData);
                            }
                            associate.AssociateList = listResponses;
                            objres.Response = associate;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.NoRecordFound;
                            objres.Status = 0;

                        }


                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AssociateReportResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("Advisor1PayoutReport")]
        [Produces("application/json")]
        public ResponseModel Advisor1PayoutReport(RequestModel requestModel)
        {
            string EncryptedText = "";
            AssociateReportResponse objres = new AssociateReportResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                Associate associate = new Associate();
                AssociateReport associateReport = new AssociateReport();

                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                associateReport = JsonConvert.DeserializeObject<AssociateReport>(dcdata);


                DataSet dataSet = associateReport.GetAllIncomeReport();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            List<AssociateData> listResponses = new List<AssociateData>();
                            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                AssociateData associateData = new AssociateData();
                                associateData.ClosingDate = dataSet.Tables[0].Rows[i]["ClosingDate"].ToString();
                                associateData.PayoutNo = dataSet.Tables[0].Rows[i]["PayoutNo"].ToString();
                                associateData.Advisor1 = dataSet.Tables[0].Rows[i]["Advisor1"].ToString();
                                associateData.TotalRecords = int.Parse(dataSet.Tables[0].Rows[i]["TotalRecords"].ToString());
                                listResponses.Add(associateData);
                            }
                            associate.AssociateList = listResponses;
                            objres.Response = associate;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.NoRecordFound;
                            objres.Status = 0;

                        }


                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AssociateReportResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("Advisor2PayoutReport")]
        [Produces("application/json")]
        public ResponseModel Advisor2PayoutReport(RequestModel requestModel)
        {
            string EncryptedText = "";
            AssociateReportResponse objres = new AssociateReportResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                Associate associate = new Associate();
                AssociateReport associateReport = new AssociateReport();

                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                associateReport = JsonConvert.DeserializeObject<AssociateReport>(dcdata);


                DataSet dataSet = associateReport.GetAllIncomeReport();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            List<AssociateData> listResponses = new List<AssociateData>();
                            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                AssociateData associateData = new AssociateData();
                                associateData.ClosingDate = dataSet.Tables[0].Rows[i]["ClosingDate"].ToString();
                                associateData.PayoutNo = dataSet.Tables[0].Rows[i]["PayoutNo"].ToString();
                                associateData.Advisor2 = dataSet.Tables[0].Rows[i]["Advisor2"].ToString();
                                associateData.TotalRecords = int.Parse(dataSet.Tables[0].Rows[i]["TotalRecords"].ToString());
                                listResponses.Add(associateData);
                            }
                            associate.AssociateList = listResponses;
                            objres.Response = associate;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.NoRecordFound;
                            objres.Status = 0;

                        }


                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AssociateReportResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("Advisor3PayoutReport")]
        [Produces("application/json")]
        public ResponseModel Advisor3PayoutReport(RequestModel requestModel)
        {
            string EncryptedText = "";
            AssociateReportResponse objres = new AssociateReportResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                Associate associate = new Associate();
                AssociateReport associateReport = new AssociateReport();

                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                associateReport = JsonConvert.DeserializeObject<AssociateReport>(dcdata);


                DataSet dataSet = associateReport.GetAllIncomeReport();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            List<AssociateData> listResponses = new List<AssociateData>();
                            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                AssociateData associateData = new AssociateData();
                                associateData.ClosingDate = dataSet.Tables[0].Rows[i]["ClosingDate"].ToString();
                                associateData.PayoutNo = dataSet.Tables[0].Rows[i]["PayoutNo"].ToString();
                                associateData.Advisor3 = dataSet.Tables[0].Rows[i]["Advisor3"].ToString();
                                associateData.TotalRecords = int.Parse(dataSet.Tables[0].Rows[i]["TotalRecords"].ToString());
                                listResponses.Add(associateData);
                            }
                            associate.AssociateList = listResponses;
                            objres.Response = associate;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.NoRecordFound;
                            objres.Status = 0;

                        }


                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AssociateReportResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("RoyalPayoutReport")]
        [Produces("application/json")]
        public ResponseModel RoyalPayoutReport(RequestModel requestModel)
        {
            string EncryptedText = "";
            AssociateReportResponse objres = new AssociateReportResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                Associate associate = new Associate();
                AssociateReport associateReport = new AssociateReport();

                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                associateReport = JsonConvert.DeserializeObject<AssociateReport>(dcdata);


                DataSet dataSet = associateReport.GetAllIncomeReport();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            List<AssociateData> listResponses = new List<AssociateData>();
                            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                AssociateData associateData = new AssociateData();
                                associateData.ClosingDate = dataSet.Tables[0].Rows[i]["ClosingDate"].ToString();
                                associateData.PayoutNo = dataSet.Tables[0].Rows[i]["PayoutNo"].ToString();
                                associateData.Royal = dataSet.Tables[0].Rows[i]["Royal"].ToString();
                                associateData.TotalRecords = int.Parse(dataSet.Tables[0].Rows[i]["TotalRecords"].ToString());
                                listResponses.Add(associateData);
                            }
                            associate.AssociateList = listResponses;
                            objres.Response = associate;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.NoRecordFound;
                            objres.Status = 0;

                        }


                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;

                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AssociateReportResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("FamilynWalletRequest")]
        [Produces("application/json")]
        public ResponseModel FamilynWalletRequest(RequestModel requestModel)
        {
            string EncryptedText = "";
            FamilynWalletResponse objres = new FamilynWalletResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                FamilynWalletRequest walletRequest = new FamilynWalletRequest();
                FamilynWallet familynWallet = new FamilynWallet();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                walletRequest = JsonConvert.DeserializeObject<FamilynWalletRequest>(dcdata);
                DataSet dataSet = walletRequest.RequestFamilynWallet();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            List<FamilynWalletData> listwalletData = new List<FamilynWalletData>();
                            for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                            {
                                FamilynWalletData wallet = new FamilynWalletData();

                                wallet.Amount = decimal.Parse(dataSet.Tables[0].Rows[j]["Amount"].ToString());
                                wallet.RequestedDate = dataSet.Tables[0].Rows[j]["RequestedDate"].ToString();
                                wallet.Status = dataSet.Tables[0].Rows[j]["Status"].ToString();
                                wallet.TransactionDate = dataSet.Tables[0].Rows[j]["TransactionDate"].ToString();
                                wallet.PaymentMode = dataSet.Tables[0].Rows[j]["PaymentMode"].ToString();
                                wallet.TransactionNo = dataSet.Tables[0].Rows[j]["TransactionNo"].ToString();

                                listwalletData.Add(wallet);
                            }
                            familynWallet.RequestList = listwalletData;
                            objres.Response = familynWallet;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FamilynWalletResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("FamilynWalletList")]
        [Produces("application/json")]
        public ResponseModel FamilynWalletList(RequestModel requestModel)
        {
            string EncryptedText = "";
            FamilynWalletListResponse objres = new FamilynWalletListResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                FamilynWalletListRequest walletListRequest = new FamilynWalletListRequest();
                FamilynWalletList familynWalletList = new FamilynWalletList();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                walletListRequest = JsonConvert.DeserializeObject<FamilynWalletListRequest>(dcdata);
                DataSet dataSet = walletListRequest.GetFamilynWalletList();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<FamilynWalletListData> listwalletData = new List<FamilynWalletListData>();
                        for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                        {
                            FamilynWalletListData walletList = new FamilynWalletListData();

                            walletList.Amount = decimal.Parse(dataSet.Tables[0].Rows[j]["Amount"].ToString());
                            walletList.RequestedDate = dataSet.Tables[0].Rows[j]["RequestedDate"].ToString();
                            walletList.Status = dataSet.Tables[0].Rows[j]["Status"].ToString();
                            walletList.TransactionDate = dataSet.Tables[0].Rows[j]["TransactionDate"].ToString();
                            walletList.PaymentMode = dataSet.Tables[0].Rows[j]["PaymentMode"].ToString();
                            walletList.TransactionNo = dataSet.Tables[0].Rows[j]["TransactionNo"].ToString();

                            listwalletData.Add(walletList);
                        }
                        familynWalletList.WalletList = listwalletData;
                        objres.Response = familynWalletList;
                        objres.Message = "";
                        objres.Status = 1;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FamilynWalletListResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("FamilynWalletLedger")]
        [Produces("application/json")]
        public ResponseModel FamilynWalletLedger(RequestModel requestModel)
        {
            string EncryptedText = "";
            FamilynWalletLedgerResponse objres = new FamilynWalletLedgerResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                FamilynWalletLedgerRequest walletLedgerRequest = new FamilynWalletLedgerRequest();
                FamilynWalletLedger familynWalletLedger = new FamilynWalletLedger();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                walletLedgerRequest = JsonConvert.DeserializeObject<FamilynWalletLedgerRequest>(dcdata);
                walletLedgerRequest.Size = SessionManager.Size;
                DataSet dataSet = walletLedgerRequest.GetFamilynWalletLedger();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<FamilynWalletLedgerData> listwalletData = new List<FamilynWalletLedgerData>();
                        for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                        {
                            FamilynWalletLedgerData walletLedger = new FamilynWalletLedgerData();

                            walletLedger.TransactionDate = dataSet.Tables[0].Rows[j]["TransactionDate"].ToString();
                            walletLedger.Narration = dataSet.Tables[0].Rows[j]["Narration"].ToString();
                            walletLedger.Cramount = decimal.Parse(dataSet.Tables[0].Rows[j]["Cramount"].ToString());
                            walletLedger.Dramount = decimal.Parse(dataSet.Tables[0].Rows[j]["Dramount"].ToString());
                            walletLedger.Balance = decimal.Parse(dataSet.Tables[0].Rows[j]["Balance"].ToString());
                            walletLedger.TotalRecords = int.Parse(dataSet.Tables[0].Rows[j]["TotalRecords"].ToString());

                            listwalletData.Add(walletLedger);
                        }
                        familynWalletLedger.WalletLedger = listwalletData;
                        objres.Response = familynWalletLedger;
                        objres.Message = "";
                        objres.Status = 1;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FamilynWalletLedgerResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("FranchiseeFamilynWalletRequest")]
        [Produces("application/json")]
        public ResponseModel FranchiseeFamilynWalletRequest(RequestModel requestModel)
        {
            string EncryptedText = "";
            FamilynFranchiseeWalletResponse objres = new FamilynFranchiseeWalletResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                FamilynFranchiseeWalletRequest walletRequest = new FamilynFranchiseeWalletRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                walletRequest = JsonConvert.DeserializeObject<FamilynFranchiseeWalletRequest>(dcdata);
                DataSet dataSet = walletRequest.FamilynWalletRequest();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            objres.Message = Messages.WalletSuccess;
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FamilynFranchiseeWalletResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("FranchiseeFamilynWalletList")]
        [Produces("application/json")]
        public ResponseModel FranchiseeFamilynWalletList(RequestModel requestModel)
        {
            string EncryptedText = "";
            FamilynFranchiseeWalletListResponse objres = new FamilynFranchiseeWalletListResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                FamilynFranchiseeWalletListRequest walletListRequest = new FamilynFranchiseeWalletListRequest();
                FamilynFranchiseeWalletList familynFranchiseeWalletList = new FamilynFranchiseeWalletList();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                walletListRequest = JsonConvert.DeserializeObject<FamilynFranchiseeWalletListRequest>(dcdata);
                walletListRequest.Size = SessionManager.Size;
                DataSet dataSet = walletListRequest.GetFamilynFranchiseeWalletList();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<FamilynFranchiseeWalletListData> listwalletData = new List<FamilynFranchiseeWalletListData>();
                        for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                        {
                            FamilynFranchiseeWalletListData walletList = new FamilynFranchiseeWalletListData();

                            walletList.Amount = decimal.Parse(dataSet.Tables[0].Rows[j]["Amount"].ToString());
                            walletList.RequestedDate = dataSet.Tables[0].Rows[j]["RequestedDate"].ToString();
                            walletList.Status = dataSet.Tables[0].Rows[j]["Status"].ToString();
                            walletList.TransactionDate = dataSet.Tables[0].Rows[j]["TransactionDate"].ToString();
                            walletList.PaymentMode = dataSet.Tables[0].Rows[j]["PaymentMode"].ToString();
                            walletList.TransactionNo = dataSet.Tables[0].Rows[j]["TransactionNo"].ToString();
                            walletList.TotalRecords = int.Parse(dataSet.Tables[0].Rows[j]["TotalRecords"].ToString());

                            listwalletData.Add(walletList);
                        }
                        familynFranchiseeWalletList.WalletList = listwalletData;
                        objres.Response = familynFranchiseeWalletList;
                        objres.Message = "";
                        objres.Status = 1;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FamilynFranchiseeWalletListResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("FranchiseeFamilynWalletLedger")]
        [Produces("application/json")]
        public ResponseModel FranchiseeFamilynWalletLedger(RequestModel requestModel)
        {
            string EncryptedText = "";
            FamilynFranchiseeWalletLedgerResponse objres = new FamilynFranchiseeWalletLedgerResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                FamilynFranchiseeWalletLedgerRequest walletLedgerRequest = new FamilynFranchiseeWalletLedgerRequest();
                FamilynFranchiseeWalletLedger familynWalletLedger = new FamilynFranchiseeWalletLedger();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                walletLedgerRequest = JsonConvert.DeserializeObject<FamilynFranchiseeWalletLedgerRequest>(dcdata);
                walletLedgerRequest.Size = SessionManager.Size;
                DataSet dataSet = walletLedgerRequest.GetFamilynWalletLedger();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<FamilynFranchiseeWalletLedgerData> listwalletData = new List<FamilynFranchiseeWalletLedgerData>();
                        for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                        {
                            FamilynFranchiseeWalletLedgerData walletLedger = new FamilynFranchiseeWalletLedgerData();

                            walletLedger.TransactionDate = dataSet.Tables[0].Rows[j]["TransactionDate"].ToString();
                            walletLedger.Narration = dataSet.Tables[0].Rows[j]["Narration"].ToString();
                            walletLedger.Cramount = decimal.Parse(dataSet.Tables[0].Rows[j]["Cramount"].ToString());
                            walletLedger.Dramount = decimal.Parse(dataSet.Tables[0].Rows[j]["Dramount"].ToString());
                            walletLedger.Balance = decimal.Parse(dataSet.Tables[0].Rows[j]["Balance"].ToString());
                            walletLedger.TotalRecords = int.Parse(dataSet.Tables[0].Rows[j]["TotalRecords"].ToString());

                            listwalletData.Add(walletLedger);
                        }
                        familynWalletLedger.WalletLedger = listwalletData;
                        objres.Response = familynWalletLedger;
                        objres.Message = "";
                        objres.Status = 1;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FamilynFranchiseeWalletLedgerResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("DeleteAccount")]
        [Produces("application/json")]
        public ResponseModel DeleteAccount(RequestModel requestModel)
        {
            string EncryptedText = "";
            AccountDeletionResponse objres = new AccountDeletionResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                AccountDeletionRequest accountDeletionRequest = new AccountDeletionRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                accountDeletionRequest = JsonConvert.DeserializeObject<AccountDeletionRequest>(dcdata);
                DataSet dataSet = accountDeletionRequest.DeleteAccount();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AccountDeletionResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("SendOtp")]
        [Produces("application/json")]
        public ResponseModel SendOtp(RequestModel requestModel)
        {
            string EncryptedText = "";
            SendOtpResponse objres = new SendOtpResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                ChangePassword sendOtp = new ChangePassword();
                SendOtpforLogin sendOtpforLogin = new SendOtpforLogin();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                sendOtp = JsonConvert.DeserializeObject<ChangePassword>(dcdata);
                DataSet dataSet = sendOtp.OTPSend();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            string Msg = "Dear Associate, OTP login request is " + dataSet.Tables[0].Rows[0]["OTP"].ToString() + ", OTP valid for 30 minute. http://karyon.organic/";
                            //string Msg = "Dear Associate OTP for login request is "+ dataSet.Tables[0].Rows[0]["OTP"].ToString() + ", OTP is valid for 30 minute. "+CompanyDetails.Website;

                            BLSMS.SendSMS(dataSet.Tables[0].Rows[0]["MobileNo"].ToString(), Msg, TemplateId.OTPTemplateId);

                            List<SendOtpforLoginData>? listresponse = new List<SendOtpforLoginData>();
                            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                            {
                                SendOtpforLoginData IdData = new SendOtpforLoginData();
                                IdData.LoginId = dataSet.Tables[0].Rows[i]["LoginId"].ToString();
                                IdData.Name = dataSet.Tables[0].Rows[i]["Name"].ToString();

                                listresponse.Add(IdData);

                            }
                            sendOtpforLogin.IdList = listresponse;
                            objres.Response = sendOtpforLogin;
                            objres.IsMultiple = dataSet.Tables[0].Rows.Count > 1 ? 1 : 0;
                            objres.Message = Messages.OtpSendSuccess;
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(SendOtpResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("VerifyOtp")]
        [Produces("application/json")]
        public ResponseModel VerifyOtp(RequestModel requestModel)
        {
            string EncryptedText = "";
            VerifyOtpResponse objres = new VerifyOtpResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                ChangePassword verifyotp = new ChangePassword();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                verifyotp = JsonConvert.DeserializeObject<ChangePassword>(dcdata);
                DataSet dataSet = verifyotp.VerifyOTP();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            objres.LoginId = dataSet.Tables[0].Rows[0]["LoginId"].ToString();
                            objres.Old_LoginId = dataSet.Tables[0].Rows[0]["Old_LoginId"].ToString();
                            objres.Name = dataSet.Tables[0].Rows[0]["ContactPerson"].ToString();
                            objres.Message = Messages.OtpVerifiedSuccess;
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(VerifyOtpResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("ForgetPassword")]
        [Produces("application/json")]
        public ResponseModel ForgetPassword(RequestModel requestModel)
        {
            string EncryptedText = "";
            ForgetPasswordResponse objres = new ForgetPasswordResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                ChangePassword verifyotp = new ChangePassword();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                verifyotp = JsonConvert.DeserializeObject<ChangePassword>(dcdata);
                DataSet dataSet = verifyotp.ForgetPassword();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ForgetPasswordResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("FranchiseeWalletBalance")]
        [Produces("application/json")]
        public ResponseModel FranchiseeWalletBalance(RequestModel requestModel)
        {
            string EncryptedText = "";
            FranchiseeWalletResponse objres = new FranchiseeWalletResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                FranchiseeWalletBalance walletBalance = new FranchiseeWalletBalance();
                FranchiseWallet franchiseWallet = new FranchiseWallet();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                franchiseWallet = JsonConvert.DeserializeObject<FranchiseWallet>(dcdata);

                DataSet dataSet = franchiseWallet.GetFranchiseeBalance();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            FranchiseeWalletData walletData = new FranchiseeWalletData();
                            walletData.KaryonWallet = decimal.Parse(dataSet.Tables[0].Rows[0]["Balance"].ToString());
                            walletData.FUPWallet = decimal.Parse(dataSet.Tables[0].Rows[0]["FUPPoints"].ToString());

                            walletBalance.WalletBalance = walletData;
                            objres.Response = walletBalance;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.NoRecordFound;
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FranchiseeWalletResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;


        }

        [HttpPost("FranchiseePayoutLedger")]
        [Produces("application/json")]
        public ResponseModel FranchiseePayoutLedger(RequestModel requestModel)
        {
            string EncryptedText = "";
            FranchiseePayoutLedgerResponse objres = new FranchiseePayoutLedgerResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                FranchiseePayoutLedgerRequest payoutLedgerRequest = new FranchiseePayoutLedgerRequest();
                FranchiseePayoutLedger franchiseePayoutLedger = new FranchiseePayoutLedger();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                payoutLedgerRequest = JsonConvert.DeserializeObject<FranchiseePayoutLedgerRequest>(dcdata);
                payoutLedgerRequest.Size = SessionManager.Size;
                DataSet dataSet = payoutLedgerRequest.GetFranchisePayoutLedger();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<FranchiseePayoutLedgerData> ledgerlist = new List<FranchiseePayoutLedgerData>();
                        for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                        {
                            FranchiseePayoutLedgerData payoutLedgerData = new FranchiseePayoutLedgerData();

                            payoutLedgerData.TransactionDate = dataSet.Tables[0].Rows[j]["TransactionDate"].ToString();
                            payoutLedgerData.Narration = dataSet.Tables[0].Rows[j]["Narration"].ToString();
                            payoutLedgerData.Cramount = decimal.Parse(dataSet.Tables[0].Rows[j]["Cramount"].ToString());
                            payoutLedgerData.Dramount = decimal.Parse(dataSet.Tables[0].Rows[j]["Dramount"].ToString());
                            payoutLedgerData.Balance = decimal.Parse(dataSet.Tables[0].Rows[j]["Balance"].ToString());
                            payoutLedgerData.TotalRecords = int.Parse(dataSet.Tables[0].Rows[j]["TotalRecord"].ToString());

                            ledgerlist.Add(payoutLedgerData);
                        }
                        franchiseePayoutLedger.payoutList = ledgerlist;
                        objres.Response = franchiseePayoutLedger;
                        objres.Message = "";
                        objres.Status = 1;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FranchiseePayoutLedgerResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("AssociatePayoutLedger")]
        [Produces("application/json")]
        public ResponseModel AssociatePayoutLedger(RequestModel requestModel)
        {
            string EncryptedText = "";
            PayoutLedgerResponse objres = new PayoutLedgerResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                PayoutLedgerRequest payoutLedgerRequest = new PayoutLedgerRequest();
                PayoutLedger payoutLedger = new PayoutLedger();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                payoutLedgerRequest = JsonConvert.DeserializeObject<PayoutLedgerRequest>(dcdata);
                payoutLedgerRequest.Size = SessionManager.Size;
                DataSet dataSet = payoutLedgerRequest.GetPayoutLedger();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<PayoutLedgerData> ledgerlist = new List<PayoutLedgerData>();
                        for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                        {
                            PayoutLedgerData payoutLedgerData = new PayoutLedgerData();

                            payoutLedgerData.TransactionDate = dataSet.Tables[0].Rows[j]["TransactionDate"].ToString();
                            payoutLedgerData.Narration = dataSet.Tables[0].Rows[j]["Narration"].ToString();
                            payoutLedgerData.Cramount = decimal.Parse(dataSet.Tables[0].Rows[j]["Cramount"].ToString());
                            payoutLedgerData.Dramount = decimal.Parse(dataSet.Tables[0].Rows[j]["Dramount"].ToString());
                            payoutLedgerData.Balance = decimal.Parse(dataSet.Tables[0].Rows[j]["Balance"].ToString());
                            payoutLedgerData.TotalRecords = int.Parse(dataSet.Tables[0].Rows[j]["TotalRecord"].ToString());

                            ledgerlist.Add(payoutLedgerData);
                        }
                        payoutLedger.payoutList = ledgerlist;
                        objres.Response = payoutLedger;
                        objres.Message = "";
                        objres.Status = 1;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(PayoutLedgerResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("ForgotId")]
        [Produces("application/json")]
        public ResponseModel ForgotId(RequestModel requestModel)
        {
            string EncryptedText = "";
            ForgotIdResponse objres = new ForgotIdResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                ForgotIdRequest forgotIdRequest = new ForgotIdRequest();
                ForgotId forgotId = new ForgotId();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                forgotIdRequest = JsonConvert.DeserializeObject<ForgotIdRequest>(dcdata);
                DataSet dataSet = forgotIdRequest.ForgotId();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            List<ForgotIdData> list = new List<ForgotIdData>();
                            for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                            {
                                ForgotIdData forgotIddata = new ForgotIdData();

                                forgotIddata.LoginId = dataSet.Tables[0].Rows[j]["LoginId"].ToString();
                                forgotIddata.OldLoginId = dataSet.Tables[0].Rows[j]["Old_LoginId"].ToString();
                                forgotIddata.Name = dataSet.Tables[0].Rows[j]["ContactPerson"].ToString();
                                list.Add(forgotIddata);
                            }
                            forgotId.IdList = list;
                            objres.Response = forgotId;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ForgotIdResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("CallBackURL")]
        [Produces("application/json")]
        public ResponseModel CallBackURL(CallBackRequest callBackRequest)
        {

            string Body = "";
            string obj1 = "{\"Status\":\"" + callBackRequest.STATUS + "\",\"OrderNo\":\"" + callBackRequest.ORDERID + "\",\"TxnId\":\"" + callBackRequest.TXNID + "\",\"BankTxnId\":\"" + callBackRequest.BANKTXNID + "\",\"CheckSumHash\":\"" + callBackRequest.CHECKSUMHASH + "\",\"RespMsg\":\"" + callBackRequest.RESPMSG + "\",\"OpCode\":\"1\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.PlaceOrder, Body);
            PlaceOrderResponse placeOrderResponse = new PlaceOrderResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            placeOrderResponse = JsonConvert.DeserializeObject<PlaceOrderResponse>(dcdata);
            return null;
        }

        [HttpPost("UpdatePaytmPaymentStatus")]
        [Produces("application/json")]
        public ResponseModel UpdatePaytmPaymentStatus(RequestModel requestModel)
        {
            CallBackRequest callBackRequest = new CallBackRequest();
            string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
            callBackRequest = JsonConvert.DeserializeObject<CallBackRequest>(dcdata);

            ForgotIdResponse objres = new ForgotIdResponse();
            Dictionary<string, string> body = new Dictionary<string, string>();
            Dictionary<string, string> head = new Dictionary<string, string>();
            Dictionary<string, Dictionary<string, string>> requestBody = new Dictionary<string, Dictionary<string, string>>();
            /*
            * Generate checksum by parameters we have in body
            * Find your Merchant Key in your Paytm Dashboard at https://dashboard.paytm.com/next/apikeys 
            */


            body.Add("mid", Gateway.MID);
            body.Add("orderId", callBackRequest.ORDERID);

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
            //string url = "https://securegw-stage.paytm.in/v3/order/status";

            //For  Production 
            string url = "https://securegw.paytm.in/v3/order/status";

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
                PaytmInitiateResponse deserializedProduct = JsonConvert.DeserializeObject<PaytmInitiateResponse>(responseData);

                string Body = "";
                string obj1 = "{\"Status\":\"" + deserializedProduct.body.resultInfo.resultStatus + "\",\"OrderNo\":\"" + callBackRequest.ORDERID + "\",\"TxnId\":\"" + deserializedProduct.body.txnId + "\",\"BankTxnId\":\"" + deserializedProduct.body.bankTxnId + "\",\"CheckSumHash\":\"" + deserializedProduct.head.signature + "\",\"RespMsg\":\"" + deserializedProduct.body.resultInfo.resultMsg + "\",\"OpCode\":\"1\"}";
                Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                string result = Common.HITAPI(APIURL.PlaceOrder, Body);
                PlaceOrderResponse placeOrderResponse = new PlaceOrderResponse();
                ShoppingWebResponse deserializedProduct1 = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct1.body);
                placeOrderResponse = JsonConvert.DeserializeObject<PlaceOrderResponse>(dcdata);


                Console.WriteLine(responseData);
            }
            return null;
        }

        [HttpGet("UpdateCustomerOnlineOrders")]
        [Produces("application/json")]
        public ResponseModel UpdateCustomerOnlineOrders()
        {
            CallBackRequest callBackRequest = new CallBackRequest();
            CreateOrder createOrder = new CreateOrder();
            createOrder.OpCode = 7;
            DataSet dataSet = createOrder.GetCustomerOrders();
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                {
                    Dictionary<string, string> body = new Dictionary<string, string>();
                    Dictionary<string, string> head = new Dictionary<string, string>();
                    Dictionary<string, Dictionary<string, string>> requestBody = new Dictionary<string, Dictionary<string, string>>();
                    /*
                    * Generate checksum by parameters we have in body
                    * Find your Merchant Key in your Paytm Dashboard at https://dashboard.paytm.com/next/apikeys 
                    */


                    body.Add("mid", Gateway.MID);
                    body.Add("orderId", dataSet.Tables[0].Rows[i]["OrderNo"].ToString());

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
                    //string url = "https://securegw-stage.paytm.in/v3/order/status";

                    //For  Production 
                    string url = "https://securegw.paytm.in/v3/order/status";

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
                        PaytmInitiateResponse deserializedProduct = JsonConvert.DeserializeObject<PaytmInitiateResponse>(responseData);

                        string Body = "";
                        string obj1 = "{\"Status\":\"" + deserializedProduct.body.resultInfo.resultStatus + "\",\"OrderNo\":\"" + dataSet.Tables[0].Rows[i]["OrderNo"].ToString() + "\",\"TxnId\":\"" + deserializedProduct.body.txnId + "\",\"BankTxnId\":\"" + deserializedProduct.body.bankTxnId + "\",\"CheckSumHash\":\"" + deserializedProduct.head.signature + "\",\"RespMsg\":\"" + deserializedProduct.body.resultInfo.resultMsg + "\",\"OpCode\":\"1\"}";
                        Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                        string result = Common.HITAPI(APIURL.PlaceOrder, Body);
                        PlaceOrderResponse placeOrderResponse = new PlaceOrderResponse();
                        ShoppingWebResponse deserializedProduct1 = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                        string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct1.body);
                        placeOrderResponse = JsonConvert.DeserializeObject<PlaceOrderResponse>(dcdata);


                        Console.WriteLine(responseData);
                    }

                }

            }
            return null;
        }
        [HttpPost("Stock")]
        [Produces("application/json")]
        public ResponseModel Stock(RequestModel requestModel)
        {
            string EncryptedText = "";
            StockResponse objres = new StockResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                Stock stockRequest = new Stock();
                FranchiseeStock franchiseeStock = new FranchiseeStock();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                stockRequest = JsonConvert.DeserializeObject<Stock>(dcdata);
                stockRequest.Size = SessionManager.Size;
                DataSet dataSet = stockRequest.GetFranchiseStock();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<StockData> list = new List<StockData>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            StockData stockData = new StockData();
                            stockData.Fk_VarientId = dataSet.Tables[0].Rows[i]["Id"].ToString();
                            stockData.ProductName = dataSet.Tables[0].Rows[i]["ProductName"].ToString();
                            stockData.CreditQty = dataSet.Tables[0].Rows[i]["CreditQty"].ToString();
                            stockData.DebitQty = dataSet.Tables[0].Rows[i]["DebitQty"].ToString();
                            list.Add(stockData);
                        }
                        franchiseeStock.StockList = list;
                        objres.Response = franchiseeStock;
                        objres.Message = "";
                        objres.Status = 1;

                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(StockResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("StockDetails")]
        [Produces("application/json")]
        public ResponseModel StockDetails(RequestModel requestModel)
        {
            string EncryptedText = "";
            StockDetailsResponse objres = new StockDetailsResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                Stock stockRequest = new Stock();
                StockDetails franchiseeStockDetails = new StockDetails();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                stockRequest = JsonConvert.DeserializeObject<Stock>(dcdata);
                DataSet dataSet = stockRequest.GetStockDetails();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<StockDetailsData> list = new List<StockDetailsData>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            StockDetailsData stockDetails = new StockDetailsData();
                            stockDetails.OrderNo = dataSet.Tables[0].Rows[i]["OrderNo"].ToString();
                            stockDetails.LoginId = dataSet.Tables[0].Rows[i]["LoginId"].ToString();
                            stockDetails.ContactPerson = dataSet.Tables[0].Rows[i]["ContactPerson"].ToString();
                            stockDetails.Credit = dataSet.Tables[0].Rows[i]["Credit"].ToString();
                            stockDetails.Debit = dataSet.Tables[0].Rows[i]["Debit"].ToString();
                            list.Add(stockDetails);
                        }
                        franchiseeStockDetails.StockList = list;
                        objres.Response = franchiseeStockDetails;
                        objres.Message = "";
                        objres.Status = 1;

                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(StockDetailsResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("FranchiseeKaryonWalletRequest")]
        [Produces("application/json")]
        public ResponseModel FranchiseeKaryonWalletRequest(RequestModel requestModel)
        {
            string EncryptedText = "";
            FranchiseeKaryonPointsResponse objres = new FranchiseeKaryonPointsResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                FranchiseeKaryonPointsRequest karyonPointsRequest = new FranchiseeKaryonPointsRequest();
                FranchiseeKaryonPoints karyonPoints = new FranchiseeKaryonPoints();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                karyonPointsRequest = JsonConvert.DeserializeObject<FranchiseeKaryonPointsRequest>(dcdata);
                karyonPointsRequest.TransactionDate = string.IsNullOrEmpty(karyonPointsRequest.TransactionDate) ? null : Common.ConvertToSystemDate(karyonPointsRequest.TransactionDate, "dd/MM/yyyy");
                DataSet dataSet = karyonPointsRequest.FranchisewalletRequest();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            List<FranchiseeKaryonPointsData> listResponses = new List<FranchiseeKaryonPointsData>();
                            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                FranchiseeKaryonPointsData karyonPointsData = new FranchiseeKaryonPointsData();
                                karyonPointsData.Amount = decimal.Parse(dataSet.Tables[0].Rows[i]["Amount"].ToString());
                                karyonPointsData.RequestedDate = dataSet.Tables[0].Rows[i]["RequestedDate"].ToString();
                                karyonPointsData.Status = dataSet.Tables[0].Rows[i]["Status"].ToString();
                                karyonPointsData.TransactionDate = dataSet.Tables[0].Rows[i]["TransactionDate"].ToString();
                                karyonPointsData.PaymentMode = dataSet.Tables[0].Rows[i]["PaymentMode"].ToString();
                                karyonPointsData.TransactionNo = dataSet.Tables[0].Rows[i]["TransactionNo"].ToString();

                                listResponses.Add(karyonPointsData);
                            }
                            karyonPoints.WalletList = listResponses;
                            objres.Message = "Karyon Points Request Completed";
                            objres.Status = 1;
                            objres.Response = karyonPoints;
                        }
                        else
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FranchiseeKaryonPointsResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("FranchiseeWalletList")]
        [Produces("application/json")]
        public ResponseModel FranchiseeWalletList(RequestModel requestModel)
        {
            string EncryptedText = "";
            FranchiseeWalletListResponse objres = new FranchiseeWalletListResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                FranchiseeKaryonPointsRequest karyonPointsRequest = new FranchiseeKaryonPointsRequest();
                FranchiseeKaryonPoints karyonPoints = new FranchiseeKaryonPoints();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                karyonPointsRequest = JsonConvert.DeserializeObject<FranchiseeKaryonPointsRequest>(dcdata);
                karyonPointsRequest.Size = SessionManager.Size;
                DataSet dataSet = karyonPointsRequest.FranchiseWalletList();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<FranchiseeKaryonPointsData> listResponses = new List<FranchiseeKaryonPointsData>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            FranchiseeKaryonPointsData karyonPointsData = new FranchiseeKaryonPointsData();
                            karyonPointsData.Amount = decimal.Parse(dataSet.Tables[0].Rows[i]["Amount"].ToString());
                            karyonPointsData.RequestedDate = dataSet.Tables[0].Rows[i]["RequestedDate"].ToString();
                            karyonPointsData.Status = dataSet.Tables[0].Rows[i]["Status"].ToString();
                            karyonPointsData.TransactionDate = dataSet.Tables[0].Rows[i]["TransactionDate"].ToString();
                            karyonPointsData.PaymentMode = dataSet.Tables[0].Rows[i]["PaymentMode"].ToString();
                            karyonPointsData.TransactionNo = dataSet.Tables[0].Rows[i]["TransactionNo"].ToString();

                            listResponses.Add(karyonPointsData);
                        }
                        karyonPoints.WalletList = listResponses;
                        objres.Message = "";
                        objres.Status = 1;
                        objres.Response = karyonPoints;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FranchiseeWalletListResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("FranchiseeWalletLedger")]
        [Produces("application/json")]
        public ResponseModel FranchiseeWalletLedger(RequestModel requestModel)
        {
            string EncryptedText = "";
            FranchiseeLedgerResponse objres = new FranchiseeLedgerResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                FranchiseWallet franchiseWalletRequest = new FranchiseWallet();
                FranchiseeLedger franchiseeLedger = new FranchiseeLedger();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                franchiseWalletRequest = JsonConvert.DeserializeObject<FranchiseWallet>(dcdata);
                franchiseWalletRequest.Size = SessionManager.Size;
                DataSet dataSet = franchiseWalletRequest.GetFranchiseLedger();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<FranchiseeLedgerData> listResponses = new List<FranchiseeLedgerData>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            FranchiseeLedgerData ledgerData = new FranchiseeLedgerData();
                            ledgerData.TransactionDate = dataSet.Tables[0].Rows[i]["TransactionDate"].ToString();
                            ledgerData.Narration = dataSet.Tables[0].Rows[i]["Narration"].ToString();
                            ledgerData.Cramount = decimal.Parse(dataSet.Tables[0].Rows[i]["Cramount"].ToString());
                            ledgerData.Dramount = decimal.Parse(dataSet.Tables[0].Rows[i]["Dramount"].ToString());
                            ledgerData.Balance = decimal.Parse(dataSet.Tables[0].Rows[i]["Balance"].ToString());

                            listResponses.Add(ledgerData);
                        }
                        franchiseeLedger.LedgerList = listResponses;
                        objres.Message = "";
                        objres.Status = 1;
                        objres.Response = franchiseeLedger;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FranchiseeLedgerResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("AddCustomerOrder")]
        [Produces("application/json")]
        public ResponseModel AddCustomerOrder(RequestModel requestModel)
        {
            string EncryptedText = "";
            CreateCustomerOrderResponse objres = new CreateCustomerOrderResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                CreateCustomerOrderRequest createCustomerOrderRequest = new CreateCustomerOrderRequest();
                CreateCustomerOrder createCustomerOrder = new CreateCustomerOrder();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                createCustomerOrderRequest = JsonConvert.DeserializeObject<CreateCustomerOrderRequest>(dcdata);
                DataSet dataSet = createCustomerOrderRequest.CreateCustomerOrderTemp();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            List<CreateCustomerOrderData> listResponses = new List<CreateCustomerOrderData>();
                            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                CreateCustomerOrderData orderData = new CreateCustomerOrderData();
                                orderData.Pk_Id = dataSet.Tables[0].Rows[i]["Pk_Id"].ToString();
                                orderData.ProductName = dataSet.Tables[0].Rows[i]["ProductName"].ToString();
                                orderData.Total = decimal.Parse(dataSet.Tables[0].Rows[i]["Total"].ToString());
                                orderData.TotalBv = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalBv"].ToString());
                                orderData.GSTAmt = decimal.Parse(dataSet.Tables[0].Rows[i]["GSTAmt"].ToString());
                                orderData.GSTPerc = decimal.Parse(dataSet.Tables[0].Rows[i]["GSTPerc"].ToString());
                                orderData.TotalPrice = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalPrice"].ToString());
                                orderData.Quantity = decimal.Parse(dataSet.Tables[0].Rows[i]["Qty"].ToString());
                                orderData.Price = decimal.Parse(dataSet.Tables[0].Rows[i]["Price"].ToString());
                                orderData.BV = decimal.Parse(dataSet.Tables[0].Rows[i]["BV"].ToString());
                                listResponses.Add(orderData);
                            }
                            createCustomerOrder.CartList = listResponses;
                            objres.Response = createCustomerOrder;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CreateCustomerOrderResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("CustomerOrderList")]
        [Produces("application/json")]
        public ResponseModel CustomerOrderList(RequestModel requestModel)
        {
            string EncryptedText = "";
            CustomerOrderListResponse objres = new CustomerOrderListResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                CreateCustomerOrderRequest createCustomerOrderRequest = new CreateCustomerOrderRequest();
                CreateCustomerOrder createCustomerOrder = new CreateCustomerOrder();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                createCustomerOrderRequest = JsonConvert.DeserializeObject<CreateCustomerOrderRequest>(dcdata);
                DataSet dataSet = createCustomerOrderRequest.GetCustomerTempOrder();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<CreateCustomerOrderData> listResponses = new List<CreateCustomerOrderData>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            CreateCustomerOrderData orderData = new CreateCustomerOrderData();
                            orderData.Pk_Id = dataSet.Tables[0].Rows[i]["Pk_Id"].ToString();
                            orderData.ProductName = dataSet.Tables[0].Rows[i]["ProductName"].ToString();
                            orderData.Total = decimal.Parse(dataSet.Tables[0].Rows[i]["Total"].ToString());
                            orderData.TotalBv = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalBv"].ToString());
                            orderData.GSTAmt = decimal.Parse(dataSet.Tables[0].Rows[i]["GSTAmt"].ToString());
                            orderData.GSTPerc = decimal.Parse(dataSet.Tables[0].Rows[i]["GSTPerc"].ToString());
                            orderData.TotalPrice = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalPrice"].ToString());
                            orderData.Quantity = decimal.Parse(dataSet.Tables[0].Rows[i]["Qty"].ToString());
                            orderData.Price = decimal.Parse(dataSet.Tables[0].Rows[i]["Price"].ToString());
                            orderData.BV = decimal.Parse(dataSet.Tables[0].Rows[i]["BV"].ToString());
                            listResponses.Add(orderData);
                        }
                        createCustomerOrder.CartList = listResponses;
                        objres.Message = "";
                        objres.Status = 1;
                        objres.Response = createCustomerOrder;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CustomerOrderListResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("DeleteCustomerOrder")]
        [Produces("application/json")]
        public ResponseModel DeleteCustomerOrder(RequestModel requestModel)
        {
            string EncryptedText = "";
            DeleteCustomerOrderResponse objres = new DeleteCustomerOrderResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                CreateCustomerOrderRequest createCustomerOrderRequest = new CreateCustomerOrderRequest();
                CreateCustomerOrder createCustomerOrder = new CreateCustomerOrder();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                createCustomerOrderRequest = JsonConvert.DeserializeObject<CreateCustomerOrderRequest>(dcdata);
                DataSet dataSet = createCustomerOrderRequest.DeleteCustomerTempOrder();
                if (dataSet != null)
                {
                    List<CreateCustomerOrderData> listResponses = new List<CreateCustomerOrderData>();
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {

                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            CreateCustomerOrderData orderData = new CreateCustomerOrderData();
                            orderData.Pk_Id = dataSet.Tables[0].Rows[i]["Pk_Id"].ToString();
                            orderData.ProductName = dataSet.Tables[0].Rows[i]["ProductName"].ToString();
                            orderData.Total = decimal.Parse(dataSet.Tables[0].Rows[i]["Total"].ToString());
                            orderData.TotalBv = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalBv"].ToString());
                            orderData.GSTAmt = decimal.Parse(dataSet.Tables[0].Rows[i]["GSTAmt"].ToString());
                            orderData.GSTPerc = decimal.Parse(dataSet.Tables[0].Rows[i]["GSTPerc"].ToString());
                            orderData.TotalPrice = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalPrice"].ToString());
                            orderData.Quantity = decimal.Parse(dataSet.Tables[0].Rows[i]["Qty"].ToString());
                            orderData.Price = decimal.Parse(dataSet.Tables[0].Rows[i]["Price"].ToString());
                            orderData.BV = decimal.Parse(dataSet.Tables[0].Rows[i]["BV"].ToString());
                            listResponses.Add(orderData);
                        }
                        createCustomerOrder.CartList = listResponses;
                        objres.Message = "";
                        objres.Status = 1;
                        objres.Response = createCustomerOrder;

                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                        createCustomerOrder.CartList = listResponses;
                        objres.Response = createCustomerOrder;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(DeleteCustomerOrderResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("PlaceCustomerOrder")]
        [Produces("application/json")]
        public ResponseModel PlaceCustomerOrder(RequestModel requestModel)
        {
            string EncryptedText = "";
            PlaceCustomerOrderResponse objres = new PlaceCustomerOrderResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                CreateCustomerOrderRequest createCustomerOrderRequest = new CreateCustomerOrderRequest();
                CreateCustomerOrder createCustomerOrder = new CreateCustomerOrder();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                createCustomerOrderRequest = JsonConvert.DeserializeObject<CreateCustomerOrderRequest>(dcdata);
                createCustomerOrderRequest.TransactionDate = string.IsNullOrEmpty(createCustomerOrderRequest.TransactionDate) ? null : Common.ConvertToSystemDate(createCustomerOrderRequest.TransactionDate, "dd/MM/yyyy");
                DataSet dataSet = createCustomerOrderRequest.GenerateCustomerOrder();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            objres.Message = Messages.OrderPlaced;
                            objres.Status = 1;
                            //Pushnotification pushnotification = new Pushnotification();
                            //if (dataSet.Tables[0].Rows.Count > 0)
                            //{
                            //    pushnotification.Notification(dataSet.Tables[1].Rows[0]["DeviceToken"].ToString(), "Your Order No " + dataSet.Tables[0].Rows[0]["OrderNo"].ToString() + "Placed Seccussfully !", "Place Order", dataSet.Tables[1].Rows[0]["LoginFrom"].ToString(), "OrderList");
                            //}
                        }
                        else
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(PlaceCustomerOrderResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("DispatchedOrders")]
        [Produces("application/json")]
        public ResponseModel DispatchedOrders(RequestModel requestModel)
        {
            string EncryptedText = "";
            DispatchedOrdersResponse objres = new DispatchedOrdersResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                DispatchedOrdersRequest dispatchedOrdersRequest = new DispatchedOrdersRequest();
                DispatchedOrders dispatchOrder = new DispatchedOrders();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                dispatchedOrdersRequest = JsonConvert.DeserializeObject<DispatchedOrdersRequest>(dcdata);
                dispatchedOrdersRequest.Size = SessionManager.Size;
                DataSet dataSet = dispatchedOrdersRequest.GetFranchiseOrders();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<DispatchedOrdersData> listResponses = new List<DispatchedOrdersData>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            DispatchedOrdersData dispatchedOrders = new DispatchedOrdersData();
                            dispatchedOrders.DispatchCount = int.Parse(dataSet.Tables[0].Rows[i]["DispatchCount"].ToString());
                            dispatchedOrders.OrderNo = dataSet.Tables[0].Rows[i]["OrderNo"].ToString();
                            dispatchedOrders.Fk_ParentFranchiseId = dataSet.Tables[0].Rows[i]["Fk_DispachFranchiseId"].ToString();
                            dispatchedOrders.Name = dataSet.Tables[0].Rows[i]["Name"].ToString();
                            dispatchedOrders.MobileNo = dataSet.Tables[0].Rows[i]["MobileNo"].ToString();
                            dispatchedOrders.OrderDate = dataSet.Tables[0].Rows[i]["OrderDate"].ToString();
                            dispatchedOrders.DispatchedDate = dataSet.Tables[0].Rows[i]["orderdt"].ToString();
                            dispatchedOrders.Status = dataSet.Tables[0].Rows[i]["Status"].ToString();
                            dispatchedOrders.ParentContactPerson = dataSet.Tables[0].Rows[i]["ParentContactPerson"].ToString();
                            dispatchedOrders.OrderAmount = decimal.Parse(dataSet.Tables[0].Rows[i]["OrderAmount"].ToString());
                            dispatchedOrders.TotalPV = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalPV"].ToString());

                            listResponses.Add(dispatchedOrders);
                        }
                        dispatchOrder.OrdersList = listResponses;
                        objres.Message = "";
                        objres.Status = 1;
                        objres.Response = dispatchOrder;
                        //Pushnotification pushnotification = new Pushnotification();
                        //if (dataSet.Tables[0].Rows.Count > 0)
                        //{
                        //    pushnotification.Notification(dataSet.Tables[1].Rows[0]["DeviceToken"].ToString(), "Your Order No " + dataSet.Tables[0].Rows[0]["OrderNo"].ToString() + "Dispatched Seccussfully !", "Dispatch Order", dataSet.Tables[1].Rows[0]["LoginFrom"].ToString(), "OrderList");
                        //}
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(DispatchedOrdersResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("AllIncomeTypeReport")]
        [Produces("application/json")]
        public ResponseModel AllIncomeTypeReport(RequestModel requestModel)
        {
            string EncryptedText = "";
            AllIncomeReportResponse objres = new AllIncomeReportResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                AllIncomeReport incomeReport = new AllIncomeReport();
                AssociateReport associateReport = new AssociateReport();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                associateReport = JsonConvert.DeserializeObject<AssociateReport>(dcdata);
                associateReport.Size = SessionManager.Size;
                DataSet dataSet = associateReport.GetAllIncomeReport();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            List<AllIncomeReportData> listResponses = new List<AllIncomeReportData>();
                            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                AllIncomeReportData associateData = new AllIncomeReportData();
                                associateData.ClosingDate = dataSet.Tables[0].Rows[i]["ClosingDate"].ToString();
                                associateData.PayoutNo = dataSet.Tables[0].Rows[i]["PayoutNo"].ToString();
                                associateData.Income = dataSet.Tables[0].Rows[i]["Income"].ToString();
                                listResponses.Add(associateData);
                            }
                            incomeReport.IncomeReport = listResponses;
                            objres.Response = incomeReport;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.NoRecordFound;
                            objres.Status = 0;

                        }


                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AllIncomeReportResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("PendingOrders")]
        [Produces("application/json")]
        public ResponseModel PendingOrders(RequestModel requestModel)
        {
            string EncryptedText = "";
            PendingOrdersRersponse objres = new PendingOrdersRersponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                PendingOrdersRequest pendingOrdersRequest = new PendingOrdersRequest();
                PendingOrders pendingOrders = new PendingOrders();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                pendingOrdersRequest = JsonConvert.DeserializeObject<PendingOrdersRequest>(dcdata);
                pendingOrdersRequest.Size = SessionManager.Size;
                DataSet dataSet = pendingOrdersRequest.GetFranchiseOrders();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<PendingOrdersData> listResponses = new List<PendingOrdersData>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            PendingOrdersData pendingOrdersData = new PendingOrdersData();
                            pendingOrdersData.Pk_OrderId = dataSet.Tables[0].Rows[i]["Pk_OrderId"].ToString();
                            pendingOrdersData.DispatchCount = int.Parse(dataSet.Tables[0].Rows[i]["DispatchCount"].ToString());
                            pendingOrdersData.Mobile = dataSet.Tables[0].Rows[i]["Mobile"].ToString();
                            pendingOrdersData.LoginId = dataSet.Tables[0].Rows[i]["LoginId"].ToString();
                            pendingOrdersData.CompanyName = dataSet.Tables[0].Rows[i]["CompanyName"].ToString();
                            pendingOrdersData.Franchisee = dataSet.Tables[0].Rows[i]["Franchisee"].ToString();
                            pendingOrdersData.OrderNo = dataSet.Tables[0].Rows[i]["OrderNo"].ToString();
                            pendingOrdersData.OrderDate = dataSet.Tables[0].Rows[i]["OrderDate"].ToString();
                            pendingOrdersData.OrderAmount = decimal.Parse(dataSet.Tables[0].Rows[i]["OrderAmount"].ToString());
                            pendingOrdersData.TotalPV = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalPV"].ToString());
                            pendingOrdersData.Status = dataSet.Tables[0].Rows[i]["Status"].ToString();
                            pendingOrdersData.ParentContactPerson = dataSet.Tables[0].Rows[i]["ParentContactPerson"].ToString();
                            pendingOrdersData.ParentCompanyName = dataSet.Tables[0].Rows[i]["ParentCompanyName"].ToString();
                            pendingOrdersData.ParentPinCode = dataSet.Tables[0].Rows[i]["ParentPinCode"].ToString();
                            pendingOrdersData.Fk_ParentFranchiseId = dataSet.Tables[0].Rows[i]["Fk_ParentFranchiseId"].ToString();

                            listResponses.Add(pendingOrdersData);
                        }
                        pendingOrders.OrdersList = listResponses;
                        objres.Message = "";
                        objres.Status = 1;
                        objres.Response = pendingOrders;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(PendingOrdersRersponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("PendingCustomerOrders")]
        [Produces("application/json")]
        public ResponseModel PendingCustomerOrders(RequestModel requestModel)
        {
            string EncryptedText = "";
            PendingCustomerOrdersResponse objres = new PendingCustomerOrdersResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                PendingCustomerOrdersRequest pendingCustomerOrdersRequest = new PendingCustomerOrdersRequest();
                PendingCustomerOrders pendingCustomerOrders = new PendingCustomerOrders();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                pendingCustomerOrdersRequest = JsonConvert.DeserializeObject<PendingCustomerOrdersRequest>(dcdata);
                pendingCustomerOrdersRequest.Size = SessionManager.Size;
                DataSet dataSet = pendingCustomerOrdersRequest.GetCustomerOrders();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<PendingCustomerOrdersData> listResponses = new List<PendingCustomerOrdersData>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            PendingCustomerOrdersData pendingOrdersData = new PendingCustomerOrdersData();

                            pendingOrdersData.DispatchCount = int.Parse(dataSet.Tables[0].Rows[i]["DispatchCount"].ToString());
                            pendingOrdersData.Name = dataSet.Tables[0].Rows[i]["Name"].ToString();
                            pendingOrdersData.MobileNo = dataSet.Tables[0].Rows[i]["MobileNo"].ToString();
                            pendingOrdersData.OrderNo = dataSet.Tables[0].Rows[i]["OrderNo"].ToString();
                            pendingOrdersData.OrderDate = dataSet.Tables[0].Rows[i]["OrderDate"].ToString();
                            pendingOrdersData.OrderAmount = decimal.Parse(dataSet.Tables[0].Rows[i]["OrderAmount"].ToString());
                            pendingOrdersData.TotalPV = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalPV"].ToString());
                            pendingOrdersData.Status = dataSet.Tables[0].Rows[i]["Status"].ToString();
                            pendingOrdersData.Fk_ParentFranchiseId = dataSet.Tables[0].Rows[i]["Fk_DispachFranchiseId"].ToString();

                            listResponses.Add(pendingOrdersData);
                        }
                        pendingCustomerOrders.OrdersList = listResponses;
                        objres.Message = "";
                        objres.Status = 1;
                        objres.Response = pendingCustomerOrders;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(PendingCustomerOrdersResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("FranchiseeCartList")]
        [Produces("application/json")]
        public ResponseModel FranchiseeCartList(RequestModel requestModel)
        {
            string EncryptedText = "";
            FranchiseeCartListResponse objres = new FranchiseeCartListResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                FranchiseeOrderRequest franchiseeOrderRequest = new FranchiseeOrderRequest();
                FranchiseeCartList franchiseeCart = new FranchiseeCartList();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                franchiseeOrderRequest = JsonConvert.DeserializeObject<FranchiseeOrderRequest>(dcdata);
                DataSet dataSet = franchiseeOrderRequest.GetTempOrder();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<FranchiseeCartListData> cartList = new List<FranchiseeCartListData>();
                        for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                        {
                            FranchiseeCartListData cartitems = new FranchiseeCartListData();

                            cartitems.Pk_Id = dataSet.Tables[0].Rows[j]["Pk_Id"].ToString();
                            cartitems.ProductName = dataSet.Tables[0].Rows[j]["ProductName"].ToString();
                            cartitems.Quantity = int.Parse(dataSet.Tables[0].Rows[j]["Qty"].ToString());
                            cartitems.Price = decimal.Parse(dataSet.Tables[0].Rows[j]["Price"].ToString());
                            cartitems.MRP = decimal.Parse(dataSet.Tables[0].Rows[j]["MRP"].ToString());
                            cartitems.BV = decimal.Parse(dataSet.Tables[0].Rows[j]["BV"].ToString());
                            cartitems.TotalBv = decimal.Parse(dataSet.Tables[0].Rows[j]["TotalBv"].ToString());
                            cartitems.Bottles = decimal.Parse(dataSet.Tables[0].Rows[j]["Bottles"].ToString());
                            cartitems.GSTPerc = decimal.Parse(dataSet.Tables[0].Rows[j]["GSTPerc"].ToString());
                            cartitems.GSTAmt = decimal.Parse(dataSet.Tables[0].Rows[j]["GSTAmt"].ToString());
                            cartitems.TotalPrice = decimal.Parse(dataSet.Tables[0].Rows[j]["TotalPrice"].ToString());
                            cartitems.Total = decimal.Parse(dataSet.Tables[0].Rows[j]["Total"].ToString());

                            cartList.Add(cartitems);
                        }
                        franchiseeCart.CartList = cartList;
                        objres.Response = franchiseeCart;
                        objres.Message = "";
                        objres.Status = 1;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FranchiseeCartListResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("FranchiseeAddToCart")]
        [Produces("application/json")]
        public ResponseModel FranchiseeAddToCart(RequestModel requestModel)
        {
            string EncryptedText = "";
            FranchiseeAddtoCartResponse objres = new FranchiseeAddtoCartResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                FranchiseeOrderRequest franchiseeOrderRequest = new FranchiseeOrderRequest();
                FranchiseeCartList franchiseeCart = new FranchiseeCartList();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                franchiseeOrderRequest = JsonConvert.DeserializeObject<FranchiseeOrderRequest>(dcdata);
                DataSet dataSet = franchiseeOrderRequest.CreateOrderTemp();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<FranchiseeCartListData> cartList = new List<FranchiseeCartListData>();
                        for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                        {
                            FranchiseeCartListData cartitems = new FranchiseeCartListData();

                            cartitems.Pk_Id = dataSet.Tables[0].Rows[j]["Pk_Id"].ToString();
                            cartitems.ProductName = dataSet.Tables[0].Rows[j]["ProductName"].ToString();
                            cartitems.Quantity = int.Parse(dataSet.Tables[0].Rows[j]["Qty"].ToString());
                            cartitems.Price = decimal.Parse(dataSet.Tables[0].Rows[j]["Price"].ToString());
                            cartitems.MRP = decimal.Parse(dataSet.Tables[0].Rows[j]["MRP"].ToString());
                            cartitems.BV = decimal.Parse(dataSet.Tables[0].Rows[j]["BV"].ToString());
                            cartitems.TotalBv = decimal.Parse(dataSet.Tables[0].Rows[j]["TotalBv"].ToString());
                            cartitems.Bottles = decimal.Parse(dataSet.Tables[0].Rows[j]["Bottles"].ToString());
                            cartitems.GSTPerc = decimal.Parse(dataSet.Tables[0].Rows[j]["GSTPerc"].ToString());
                            cartitems.GSTAmt = decimal.Parse(dataSet.Tables[0].Rows[j]["GSTAmt"].ToString());
                            cartitems.TotalPrice = decimal.Parse(dataSet.Tables[0].Rows[j]["TotalPrice"].ToString());
                            cartitems.Total = decimal.Parse(dataSet.Tables[0].Rows[j]["Total"].ToString());

                            cartList.Add(cartitems);
                        }
                        franchiseeCart.CartList = cartList;
                        objres.Response = franchiseeCart;
                        objres.Message = "";
                        objres.Status = 1;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FranchiseeAddtoCartResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("FranchiseeDeleteCart")]
        [Produces("application/json")]
        public ResponseModel FranchiseeDeleteCart(RequestModel requestModel)
        {
            string EncryptedText = "";
            FranchiseeDeleteCartResponse objres = new FranchiseeDeleteCartResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                FranchiseeOrderRequest franchiseeOrderRequest = new FranchiseeOrderRequest();
                FranchiseeCartList franchiseeCart = new FranchiseeCartList();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                franchiseeOrderRequest = JsonConvert.DeserializeObject<FranchiseeOrderRequest>(dcdata);
                DataSet dataSet = franchiseeOrderRequest.DeleteTempOrder();
                if (dataSet != null)
                {
                    List<FranchiseeCartListData> cartList = new List<FranchiseeCartListData>();
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                        {
                            FranchiseeCartListData cartitems = new FranchiseeCartListData();

                            cartitems.Pk_Id = dataSet.Tables[0].Rows[j]["Pk_Id"].ToString();
                            cartitems.ProductName = dataSet.Tables[0].Rows[j]["ProductName"].ToString();
                            cartitems.Quantity = int.Parse(dataSet.Tables[0].Rows[j]["Qty"].ToString());
                            cartitems.Price = decimal.Parse(dataSet.Tables[0].Rows[j]["Price"].ToString());
                            cartitems.MRP = decimal.Parse(dataSet.Tables[0].Rows[j]["MRP"].ToString());
                            cartitems.BV = decimal.Parse(dataSet.Tables[0].Rows[j]["BV"].ToString());
                            cartitems.TotalBv = decimal.Parse(dataSet.Tables[0].Rows[j]["TotalBv"].ToString());
                            cartitems.Bottles = decimal.Parse(dataSet.Tables[0].Rows[j]["Bottles"].ToString());
                            cartitems.GSTPerc = decimal.Parse(dataSet.Tables[0].Rows[j]["GSTPerc"].ToString());
                            cartitems.GSTAmt = decimal.Parse(dataSet.Tables[0].Rows[j]["GSTAmt"].ToString());
                            cartitems.TotalPrice = decimal.Parse(dataSet.Tables[0].Rows[j]["TotalPrice"].ToString());
                            cartitems.Total = decimal.Parse(dataSet.Tables[0].Rows[j]["Total"].ToString());

                            cartList.Add(cartitems);
                        }
                        franchiseeCart.CartList = cartList;
                        objres.Response = franchiseeCart;
                        objres.Message = "";
                        objres.Status = 1;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                        franchiseeCart.CartList = cartList;
                        objres.Response = franchiseeCart;
                    }
                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;

                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FranchiseeDeleteCartResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("CheckProductType")]
        [Produces("application/json")]
        public ResponseModel CheckProductType(RequestModel requestModel)
        {
            string EncryptedText = "";
            CheckProductTypeResponse objres = new CheckProductTypeResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                CheckProductTypeRequest productTypeRequest = new CheckProductTypeRequest();
                CheckProductTypeData cartData = new CheckProductTypeData();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                productTypeRequest = JsonConvert.DeserializeObject<CheckProductTypeRequest>(dcdata);
                DataSet dataSet = productTypeRequest.CheckProductType();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
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
                            cartData.CartDetails = listResponses;
                            objres.Response = cartData;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.NoRecordFound;
                            objres.Status = 0;

                        }
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;

                    }
                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;

                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CheckProductTypeResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("UploadFile")]
        [Produces("application/json")]
        public async Task<ResponseModel> UploadFile(IFormFile File, [FromForm] RequestModel requestModel)
        {
            string EncryptedText = "";
            string filePath = "";
            ResponseModel returnResponse = new ResponseModel();
            UploadFileResponse objres = new UploadFileResponse();
            try
            {
                UploadFileRequest uploadFileRequest = new UploadFileRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                uploadFileRequest = JsonConvert.DeserializeObject<UploadFileRequest>(dcdata);
                if (File != null)
                {
                    filePath = await FileManagement.WriteFiles(File, uploadFileRequest.FolderName);
                    if (filePath != null)
                    {
                        objres.Status = 1;
                        objres.FilePath = filePath;
                        objres.Message = Messages.FileUploadedSuccess;
                    }
                }
                else
                {
                    objres.Status = 0;
                    objres.Message = "";
                    objres.FilePath = "";
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(UploadFileResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("DeleteAddress")]
        [Produces("application/json")]
        public ResponseModel DeleteAddress(RequestModel requestModel)
        {
            string EncryptedText = "";
            AddressResponse objres = new AddressResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                AddressRequest addressRequest = new AddressRequest();
                Address address = new Address();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                addressRequest = JsonConvert.DeserializeObject<AddressRequest>(dcdata);
                addressRequest.OpCode = 3;
                DataSet dataSet = addressRequest.ManageAddress();
                if (dataSet != null)
                {
                    List<AddressList> listResponses = new List<AddressList>();
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                AddressList addressList = new AddressList();
                                addressList.Name = dataSet.Tables[0].Rows[i]["Name"].ToString();
                                addressList.MobileNo = dataSet.Tables[0].Rows[i]["MobileNo"].ToString();
                                addressList.Address = dataSet.Tables[0].Rows[i]["Address"].ToString();
                                addressList.Locality = dataSet.Tables[0].Rows[i]["Locality"].ToString();
                                addressList.Pincode = dataSet.Tables[0].Rows[i]["Pincode"].ToString();
                                addressList.State = dataSet.Tables[0].Rows[i]["State"].ToString();
                                addressList.City = dataSet.Tables[0].Rows[i]["City"].ToString();
                                addressList.Landmark = dataSet.Tables[0].Rows[i]["Landmark"].ToString();
                                addressList.AddressType = dataSet.Tables[0].Rows[i]["AddressType"].ToString();


                                listResponses.Add(addressList);
                            }
                            address.AddressList = listResponses;
                            objres.Response = address;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                        address.AddressList = listResponses;
                        objres.Response = address;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AddressResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("UpdateFranchiseKYC")]
        [Produces("application/json")]
        public ResponseModel UpdateFranchiseKYC(RequestModel requestModel)
        {
            string EncryptedText = "";
            KYCResponse objres = new KYCResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                ProfileRequest profileRequest = new ProfileRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                profileRequest = JsonConvert.DeserializeObject<ProfileRequest>(dcdata);
                DataSet dataSet = profileRequest.FranchiseUpdateKYC();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(KYCResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("PlaceFranchiseeOrder")]
        [Produces("application/json")]
        public ResponseModel PlaceFranchiseeOrder(RequestModel requestModel)
        {
            string EncryptedText = "";
            PlaceFranchiseeOrderResponse objres = new PlaceFranchiseeOrderResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                FranchiseeOrderRequest franchiseeOrderRequest = new FranchiseeOrderRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                franchiseeOrderRequest = JsonConvert.DeserializeObject<FranchiseeOrderRequest>(dcdata);
                DataSet dataSet = franchiseeOrderRequest.GenerateFranchiseOrder();
                if (dataSet != null)
                {
                    if (dataSet.Tables[1].Rows.Count > 0)
                    {
                        if (dataSet.Tables[1].Rows[0]["Flag"].ToString() == "1")
                        {
                            PlaceFranchiseeOrder placeFranchiseeOrder = new PlaceFranchiseeOrder();
                            placeFranchiseeOrder.OrderNo = dataSet.Tables[1].Rows[0]["OrderNo"].ToString();
                            placeFranchiseeOrder.OrderAmount = decimal.Parse(dataSet.Tables[1].Rows[0]["OrderAmount"].ToString());
                            objres.Response = placeFranchiseeOrder;
                            objres.Message = Messages.OrderPlaced;
                            objres.Status = 1;
                            //Pushnotification pushnotification = new Pushnotification();
                            //if (dataSet.Tables[0].Rows.Count > 0)
                            //{
                            //    pushnotification.Notification(dataSet.Tables[1].Rows[0]["DeviceToken"].ToString(), "Your Order No " + dataSet.Tables[0].Rows[0]["OrderNo"].ToString() + "Placed Seccussfully !", "Placed Order", dataSet.Tables[1].Rows[0]["LoginFrom"].ToString(), "OrderList");
                            //}
                        }
                        else
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(PlaceFranchiseeOrderResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("RewardDetails")]
        [Produces("application/json")]
        public ResponseModel RewardDetails(RequestModel requestModel)
        {
            string EncryptedText = "";
            RewardReposne objres = new RewardReposne();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                RewardDetails rewardDetailsRequest = new RewardDetails();
                Reward reward = new Reward();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                rewardDetailsRequest = JsonConvert.DeserializeObject<RewardDetails>(dcdata);
                DataSet dataSet = rewardDetailsRequest.GetRewardDetails();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<RewardData> Rewardlist = new List<RewardData>();
                        for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                        {
                            RewardData rewardData = new RewardData();

                            //body.Add("mid", Gateway.MID);
                            //body.Add("orderId", callBackRequest.ORDERID);

                            rewardData.Action = dataSet.Tables[0].Rows[j]["RewardName"].ToString();
                            rewardData.RewardName = dataSet.Tables[0].Rows[j]["RewardName"].ToString();
                            rewardData.TargetPV = decimal.Parse(dataSet.Tables[0].Rows[j]["TargetPV"].ToString());
                            rewardData.LeftBusiness = decimal.Parse(dataSet.Tables[0].Rows[j]["LeftBusiness"].ToString());
                            rewardData.RightBusiness = decimal.Parse(dataSet.Tables[0].Rows[j]["RightBusiness"].ToString());
                            rewardData.BalanceLeft = dataSet.Tables[0].Rows[j]["BalanceLeft"].ToString();
                            rewardData.BalanceRight = dataSet.Tables[0].Rows[j]["BalanceRight"].ToString();
                            rewardData.RewardAmount = decimal.Parse(dataSet.Tables[0].Rows[j]["RewardAmount"].ToString());
                            rewardData.Status = dataSet.Tables[0].Rows[j]["RewardStatus"].ToString();
                            rewardData.AchievedOn = dataSet.Tables[0].Rows[j]["QDate"].ToString();
                            rewardData.Rcolor = dataSet.Tables[0].Rows[j]["Rcolor"].ToString();
                            rewardData.IsVisible = bool.Parse(dataSet.Tables[0].Rows[j]["IsVisible"].ToString());
                            rewardData.PK_RId = int.Parse(dataSet.Tables[0].Rows[j]["PK_MemberRewardID"].ToString());
                            rewardData.RewardImg = dataSet.Tables[0].Rows[j]["RewardImg"].ToString();
                            rewardData.Rank = dataSet.Tables[0].Rows[j]["Ranks"].ToString();
                            rewardData.Designation = dataSet.Tables[0].Rows[j]["Designation"].ToString();
                            rewardData.Recognition = dataSet.Tables[0].Rows[j]["Recognition"].ToString();
                            rewardData.TargetPoint = dataSet.Tables[0].Rows[j]["RawardTarget"].ToString();

                            Rewardlist.Add(rewardData);
                        }
                        reward.RewardList = Rewardlist;
                        objres.Response = reward;
                        objres.Message = "";
                        objres.Status = 1;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(RewardReposne));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("CustomerDispatchedOrders")]
        [Produces("application/json")]
        public ResponseModel CustomerDispatchedOrders(RequestModel requestModel)
        {
            string EncryptedText = "";
            CustomerDispatchedOrdersResponse objres = new CustomerDispatchedOrdersResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                CustomerDispatchedOrdersRequest dispatchedOrdersRequest = new CustomerDispatchedOrdersRequest();
                CustomerDispatchedOrders dispatchOrder = new CustomerDispatchedOrders();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                dispatchedOrdersRequest = JsonConvert.DeserializeObject<CustomerDispatchedOrdersRequest>(dcdata);
                dispatchedOrdersRequest.Size = SessionManager.Size;
                DataSet dataSet = dispatchedOrdersRequest.GetCustomerOrders();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<CustomerDispatchedOrdersData> listResponses = new List<CustomerDispatchedOrdersData>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            CustomerDispatchedOrdersData dispatchedOrders = new CustomerDispatchedOrdersData();
                            dispatchedOrders.OrderNo = dataSet.Tables[0].Rows[i]["OrderNo"].ToString();
                            dispatchedOrders.DispatchCount = int.Parse(dataSet.Tables[0].Rows[i]["DispatchCount"].ToString());
                            dispatchedOrders.Name = dataSet.Tables[0].Rows[i]["Name"].ToString();
                            dispatchedOrders.MobileNo = dataSet.Tables[0].Rows[i]["MobileNo"].ToString();
                            dispatchedOrders.OrderDate = dataSet.Tables[0].Rows[i]["OrderDate"].ToString();
                            dispatchedOrders.OrderAmount = decimal.Parse(dataSet.Tables[0].Rows[i]["OrderAmount"].ToString());
                            dispatchedOrders.TotalPV = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalPV"].ToString());
                            dispatchedOrders.Status = dataSet.Tables[0].Rows[i]["Status"].ToString();
                            dispatchedOrders.ParentContactPerson = dataSet.Tables[0].Rows[i]["ParentContactPerson"].ToString();
                            dispatchedOrders.Fk_DispatchFranchiseId = dataSet.Tables[0].Rows[i]["Fk_DispachFranchiseId"].ToString();

                            listResponses.Add(dispatchedOrders);
                        }
                        dispatchOrder.OrderList = listResponses;
                        objres.Message = "";
                        objres.Status = 1;
                        objres.Response = dispatchOrder;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CustomerDispatchedOrdersResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("CustomerDispatchedOrdersDetail")]
        [Produces("application/json")]
        public ResponseModel CustomerDispatchedOrdersDetail(RequestModel requestModel)
        {
            string EncryptedText = "";
            CustomerDispatchedOrderDetailsResponse objres = new CustomerDispatchedOrderDetailsResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                CustomerDispatchedOrderDetailsRequest dispatchedOrdersRequest = new CustomerDispatchedOrderDetailsRequest();
                CustomerDispatchedOrderDetails dispatchOrder = new CustomerDispatchedOrderDetails();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                dispatchedOrdersRequest = JsonConvert.DeserializeObject<CustomerDispatchedOrderDetailsRequest>(dcdata);
                dispatchedOrdersRequest.OpCode = 2;
                DataSet dataSet = dispatchedOrdersRequest.GetCustomerOrders();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<CustomerDispatchedOrderDetailsData> listResponses = new List<CustomerDispatchedOrderDetailsData>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            CustomerDispatchedOrderDetailsData orderDetails = new CustomerDispatchedOrderDetailsData();
                            orderDetails.OrderNo = dataSet.Tables[0].Rows[i]["OrderNo"].ToString();
                            orderDetails.Fk_OrderId = dataSet.Tables[0].Rows[i]["Fk_OrderId"].ToString();
                            orderDetails.Fk_VarientId = dataSet.Tables[0].Rows[i]["Fk_VarientId"].ToString();
                            orderDetails.ProductName = dataSet.Tables[0].Rows[i]["ProductName"].ToString();
                            orderDetails.IsDispatched = dataSet.Tables[0].Rows[i]["IsDispatched"].ToString();
                            orderDetails.CompanyName = dataSet.Tables[0].Rows[i]["CompanyName"].ToString();
                            orderDetails.CustomerName = dataSet.Tables[0].Rows[i]["CustomerName"].ToString();
                            orderDetails.MobileNo = dataSet.Tables[0].Rows[i]["MobileNo"].ToString();
                            orderDetails.Address = dataSet.Tables[0].Rows[i]["Address"].ToString();
                            orderDetails.PaymentMode = dataSet.Tables[0].Rows[i]["PaymentMode"].ToString();
                            orderDetails.PurchseBy = dataSet.Tables[0].Rows[i]["PurchseBy"].ToString();
                            orderDetails.MRP = decimal.Parse(dataSet.Tables[0].Rows[i]["MRP"].ToString());
                            orderDetails.PV = decimal.Parse(dataSet.Tables[0].Rows[i]["PV"].ToString());
                            orderDetails.TotalPrice = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalPrice"].ToString());
                            orderDetails.Quantity = int.Parse(dataSet.Tables[0].Rows[i]["Qty"].ToString());
                            orderDetails.Stock = int.Parse(dataSet.Tables[0].Rows[i]["Stock"].ToString());
                            orderDetails.PendingQty = int.Parse(dataSet.Tables[0].Rows[i]["PendingQty"].ToString());
                            orderDetails.DispatchQty = int.Parse(dataSet.Tables[0].Rows[i]["DispatchQty"].ToString());

                            listResponses.Add(orderDetails);
                        }

                        dispatchOrder.OrderDetails = listResponses;
                        objres.Message = "";
                        objres.Status = 1;
                        objres.Response = dispatchOrder;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CustomerDispatchedOrderDetailsResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("SkipOrders")]
        [Produces("application/json")]
        public ResponseModel SkipOrders(RequestModel requestModel)
        {
            string EncryptedText = "";
            SkipOrdersResponse objres = new SkipOrdersResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                CreateOrder createOrder = new CreateOrder();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                createOrder = JsonConvert.DeserializeObject<CreateOrder>(dcdata);
                createOrder.Fk_ProductId = createOrder.Fk_VarientId;
                DataSet dataSet = new DataSet();
                if (createOrder.Type == "Customer")
                {
                    dataSet = createOrder.SkipOrders();
                }
                else
                {
                    dataSet = createOrder.SkipFranchiseOrders();
                }
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {

                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.ProblemInConnection;
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.ProblemInConnection;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(SkipOrdersResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("DispatchOrder")]
        [Produces("application/json")]
        public ResponseModel DispatchOrder(RequestModel requestModel)
        {
            string EncryptedText = "";
            DispatchOrdersReponse objres = new DispatchOrdersReponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                CreateOrder createOrder = new CreateOrder();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                createOrder = JsonConvert.DeserializeObject<CreateOrder>(dcdata);
                createOrder.Fk_ProductId = createOrder.Fk_VarientId;
                DataSet dataSet = new DataSet();
                if (createOrder.Type == "Customer")
                {
                    dataSet = createOrder.DispatchCustomerOrder();
                }
                else
                {
                    dataSet = createOrder.DispatchOrder();
                }
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {

                            objres.Message = Messages.DispatchSuccess;
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.ProblemInConnection;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(DispatchOrdersReponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("DispatchCounterCollectOrder")]
        [Produces("application/json")]
        public ResponseModel DispatchCounterCollectOrder(RequestModel requestModel)
        {
            string EncryptedText = "";
            DispatchCounterCollectResponse objres = new DispatchCounterCollectResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                CreateOrder createOrder = new CreateOrder();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                createOrder = JsonConvert.DeserializeObject<CreateOrder>(dcdata);
                createOrder.Fk_ProductId = createOrder.Fk_VarientId;
                DataSet dataSet = createOrder.DispatchCounterCollectOrder();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {

                            objres.Message = Messages.DispatchSuccess;
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.ProblemInConnection;
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.ProblemInConnection;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(DispatchCounterCollectResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("ViewFranhiseeOrder")]
        [Produces("application/json")]
        public ResponseModel ViewFranhiseeOrder(RequestModel requestModel)
        {
            string EncryptedText = "";
            ViewOrdersResponse objres = new ViewOrdersResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                ViewOrdersRequest viewOrdersRequest = new ViewOrdersRequest();
                ViewFranchiseeOrders franchiseeOrders = new ViewFranchiseeOrders();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                viewOrdersRequest = JsonConvert.DeserializeObject<ViewOrdersRequest>(dcdata);
                DataSet dataSet = viewOrdersRequest.ViewOrders();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<ViewOrdersData> listResponses = new List<ViewOrdersData>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            ViewOrdersData viewOrders = new ViewOrdersData();

                            viewOrders.OrderNo = dataSet.Tables[0].Rows[i]["OrderNo"].ToString();
                            viewOrders.OrderDate = dataSet.Tables[0].Rows[i]["OrderDate"].ToString();
                            viewOrders.OrderAmount = decimal.Parse(dataSet.Tables[0].Rows[i]["OrderAmount"].ToString());
                            viewOrders.TotalPV = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalPV"].ToString());
                            viewOrders.OrderStatus = dataSet.Tables[0].Rows[i]["OrderStatus"].ToString();
                            viewOrders.ParentFranchiseId = dataSet.Tables[0].Rows[i]["ParentFranchiseId"].ToString();
                            viewOrders.Invoice = "https://karyon.organic/Franchisee/PrintFranchiseOrder?OrderNo=" + dataSet.Tables[0].Rows[i]["OrderNo"].ToString();
                            List<OrderDetailsData> orderlist = new List<OrderDetailsData>();
                            for (int j = 0; j <= dataSet.Tables[1].Rows.Count - 1; j++)
                            {
                                if (dataSet.Tables[0].Rows[i]["Pk_OrderId"].ToString() == dataSet.Tables[1].Rows[j]["Fk_OrderId"].ToString())
                                {
                                    OrderDetailsData orderDetails = new OrderDetailsData();

                                    orderDetails.ProductName = dataSet.Tables[1].Rows[j]["ProductName"].ToString();
                                    orderDetails.Fk_VarientId = dataSet.Tables[1].Rows[j]["Fk_VarientId"].ToString();
                                    orderDetails.DispatchStatus = dataSet.Tables[1].Rows[j]["DispatchStatus"].ToString();
                                    orderDetails.Fk_OrderId = dataSet.Tables[1].Rows[j]["Fk_OrderId"].ToString();
                                    orderDetails.CompanyName = dataSet.Tables[1].Rows[j]["CompanyName"].ToString();
                                    orderDetails.DispatchFranchisee = dataSet.Tables[1].Rows[j]["DispatchFranchisee"].ToString();
                                    orderDetails.MRP = decimal.Parse(dataSet.Tables[1].Rows[j]["MRP"].ToString());
                                    orderDetails.PV = decimal.Parse(dataSet.Tables[1].Rows[j]["PV"].ToString());
                                    orderDetails.TotalPrice = decimal.Parse(dataSet.Tables[1].Rows[j]["TotalPrice"].ToString());
                                    orderDetails.Qty = int.Parse(dataSet.Tables[1].Rows[j]["Qty"].ToString());
                                    orderDetails.PendingQty = int.Parse(dataSet.Tables[1].Rows[j]["PendingQty"].ToString());
                                    orderDetails.DispatchQty = int.Parse(dataSet.Tables[1].Rows[j]["DispatchQty"].ToString());
                                    orderDetails.Stock = int.Parse(dataSet.Tables[1].Rows[j]["Stock"].ToString());
                                    orderDetails.Bottles = int.Parse(dataSet.Tables[1].Rows[j]["Bottles"].ToString());

                                    orderlist.Add(orderDetails);
                                }
                            }
                            viewOrders.OrderDetails = orderlist;
                            listResponses.Add(viewOrders);
                        }
                        franchiseeOrders.OrderList = listResponses;
                        objres.Message = "";
                        objres.Status = 1;
                        objres.Response = franchiseeOrders;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ViewOrdersResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("ViewCustomerOrder")]
        [Produces("application/json")]
        public ResponseModel ViewCustomerOrder(RequestModel requestModel)
        {
            string EncryptedText = "";
            ViewCustomerOrderResponse objres = new ViewCustomerOrderResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                ViewCustomerOrderRequest viewOrdersRequest = new ViewCustomerOrderRequest();
                ViewCustomerOrder viewCustomerOrder = new ViewCustomerOrder();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                viewOrdersRequest = JsonConvert.DeserializeObject<ViewCustomerOrderRequest>(dcdata);
                DataSet dataSet = viewOrdersRequest.ViewOrders();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<ViewCustomerOrderData> listResponses = new List<ViewCustomerOrderData>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            ViewCustomerOrderData viewOrders = new ViewCustomerOrderData();

                            viewOrders.OrderNo = dataSet.Tables[0].Rows[i]["OrderNo"].ToString();
                            viewOrders.OrderDate = dataSet.Tables[0].Rows[i]["OrderDate"].ToString();
                            viewOrders.OrderAmount = decimal.Parse(dataSet.Tables[0].Rows[i]["OrderAmount"].ToString());
                            viewOrders.TotalPV = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalPV"].ToString());
                            viewOrders.OrderStatus = dataSet.Tables[0].Rows[i]["OrderStatus"].ToString();
                            viewOrders.Name = dataSet.Tables[0].Rows[i]["Name"].ToString();
                            viewOrders.MobileNo = dataSet.Tables[0].Rows[i]["MobileNo"].ToString();

                            List<CustomerOrderDetails> orderlist = new List<CustomerOrderDetails>();
                            for (int j = 0; j <= dataSet.Tables[1].Rows.Count - 1; j++)
                            {
                                if (dataSet.Tables[0].Rows[i]["Pk_OrderId"].ToString() == dataSet.Tables[1].Rows[j]["Fk_OrderId"].ToString())
                                {
                                    CustomerOrderDetails orderDetails = new CustomerOrderDetails();

                                    orderDetails.ProductName = dataSet.Tables[1].Rows[j]["ProductName"].ToString();
                                    orderDetails.MRP = decimal.Parse(dataSet.Tables[1].Rows[j]["MRP"].ToString());
                                    orderDetails.PV = decimal.Parse(dataSet.Tables[1].Rows[j]["PV"].ToString());
                                    orderDetails.TotalPrice = decimal.Parse(dataSet.Tables[1].Rows[j]["TotalPrice"].ToString());
                                    orderDetails.Qty = int.Parse(dataSet.Tables[1].Rows[j]["Qty"].ToString());

                                    orderlist.Add(orderDetails);
                                }
                            }
                            viewOrders.OrderDetails = orderlist;
                            listResponses.Add(viewOrders);
                        }
                        viewCustomerOrder.OrderList = listResponses;
                        objres.Message = "";
                        objres.Status = 1;
                        objres.Response = viewCustomerOrder;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ViewCustomerOrderResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("UpdateFranchisePaytmPaymentStatus")]
        [Produces("application/json")]
        public ResponseModel UpdateFranchisePaytmPaymentStatus(RequestModel requestModel)
        {
            CallBackRequest callBackRequest = new CallBackRequest();
            string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
            callBackRequest = JsonConvert.DeserializeObject<CallBackRequest>(dcdata);

            ForgotIdResponse objres = new ForgotIdResponse();
            Dictionary<string, string> body = new Dictionary<string, string>();
            Dictionary<string, string> head = new Dictionary<string, string>();
            Dictionary<string, Dictionary<string, string>> requestBody = new Dictionary<string, Dictionary<string, string>>();
            /*
            * Generate checksum by parameters we have in body
            * Find your Merchant Key in your Paytm Dashboard at https://dashboard.paytm.com/next/apikeys 
            */


            body.Add("mid", Gateway.MID);
            body.Add("orderId", callBackRequest.ORDERID);

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
            //string url = "https://securegw-stage.paytm.in/v3/order/status";

            //For  Production 
            string url = "https://securegw.paytm.in/v3/order/status";

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
                PaytmInitiateResponse deserializedProduct = JsonConvert.DeserializeObject<PaytmInitiateResponse>(responseData);


                CreateOrder createOrder = new CreateOrder();
                createOrder.OpCode = 1;
                createOrder.Status = deserializedProduct.body.resultInfo.resultStatus;
                createOrder.OrderNo = callBackRequest.ORDERID;
                createOrder.TxnId = deserializedProduct.body.txnId;
                createOrder.BankTxnId = deserializedProduct.body.bankTxnId;
                createOrder.CheckSumHash = deserializedProduct.head.signature;
                createOrder.RespMsg = deserializedProduct.body.resultInfo.resultMsg;
                DataSet dataSet = createOrder.GenerateFranchiseOrder();

                Console.WriteLine(responseData);
            }
            return null;
        }

        [HttpPost("FranchiseePayout")]
        [Produces("application/json")]
        public ResponseModel FranchiseePayout(RequestModel requestModel)
        {
            string EncryptedText = "";
            FranchiseePayoutResponse objres = new FranchiseePayoutResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                FranchiseePayout franchiseePayout = new FranchiseePayout();
                FranchiseePayoutRequest franchiseePayoutRequest = new FranchiseePayoutRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                franchiseePayoutRequest = JsonConvert.DeserializeObject<FranchiseePayoutRequest>(dcdata);
                franchiseePayoutRequest.Size = SessionManager.Size;
                franchiseePayoutRequest.OpCode = 1;
                DataSet dataSet = franchiseePayoutRequest.GetFranchiseePayoutDetails();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<FranchiseePayoutData> listResponses = new List<FranchiseePayoutData>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            FranchiseePayoutData ledgerData = new FranchiseePayoutData();
                            ledgerData.PayoutNo = int.Parse(dataSet.Tables[0].Rows[i]["PayoutNo"].ToString());
                            ledgerData.Month = dataSet.Tables[0].Rows[i]["Month"].ToString();
                            ledgerData.TotalBusiness = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalBusiness"].ToString());
                            ledgerData.Incentive = decimal.Parse(dataSet.Tables[0].Rows[i]["Incentive"].ToString());
                            ledgerData.TDS = decimal.Parse(dataSet.Tables[0].Rows[i]["TDS"].ToString());
                            ledgerData.AdminCharges = decimal.Parse(dataSet.Tables[0].Rows[i]["AdminCharges"].ToString());
                            ledgerData.Payable = decimal.Parse(dataSet.Tables[0].Rows[i]["Payable"].ToString());
                            ledgerData.TotalRecords = int.Parse(dataSet.Tables[0].Rows[i]["TotalRecords"].ToString());

                            listResponses.Add(ledgerData);
                        }
                        franchiseePayout.PayoutList = listResponses;
                        objres.Message = "";
                        objres.Status = 1;
                        objres.Response = franchiseePayout;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FranchiseePayoutResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("FranchiseePayoutDetails")]
        [Produces("application/json")]
        public ResponseModel FranchiseePayoutDetails(RequestModel requestModel)
        {
            string EncryptedText = "";
            FranchiseePayoutDetailsResponse objres = new FranchiseePayoutDetailsResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                FranchiseePayoutDetails franchiseePayout = new FranchiseePayoutDetails();
                FranchiseePayoutRequest franchiseePayoutRequest = new FranchiseePayoutRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                franchiseePayoutRequest = JsonConvert.DeserializeObject<FranchiseePayoutRequest>(dcdata);
                franchiseePayoutRequest.OpCode = 2;
                DataSet dataSet = franchiseePayoutRequest.GetFranchiseePayoutDetails();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<FranchiseePayoutDetailsData> listResponses = new List<FranchiseePayoutDetailsData>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            FranchiseePayoutDetailsData ledgerData = new FranchiseePayoutDetailsData();
                            ledgerData.IncomeType = dataSet.Tables[0].Rows[i]["IncomeType"].ToString();
                            ledgerData.OrderNo = dataSet.Tables[0].Rows[i]["OrderNo"].ToString();
                            ledgerData.FromLoginId = dataSet.Tables[0].Rows[i]["FromLoginId"].ToString();
                            ledgerData.FromContactPerson = dataSet.Tables[0].Rows[i]["FromContactPerson"].ToString();
                            ledgerData.BusinessAmount = decimal.Parse(dataSet.Tables[0].Rows[i]["BusinessAmount"].ToString());
                            ledgerData.Percentage = decimal.Parse(dataSet.Tables[0].Rows[i]["Percentage"].ToString());
                            ledgerData.Amount = decimal.Parse(dataSet.Tables[0].Rows[i]["Amount"].ToString());

                            listResponses.Add(ledgerData);
                        }
                        franchiseePayout.PayoutList = listResponses;
                        objres.Message = "";
                        objres.Status = 1;
                        objres.Response = franchiseePayout;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FranchiseePayoutDetailsResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("FranchiseeStockProducts")]
        [Produces("application/json")]
        public ResponseModel FranchiseeStockProducts(RequestModel requestModel)
        {
            string EncryptedText = "";
            FranchiseeStockResponse objres = new FranchiseeStockResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                FranchiseeStockDetails franchiseeStockDetails = new FranchiseeStockDetails();
                Stock stockRequest = new Stock();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                stockRequest = JsonConvert.DeserializeObject<Stock>(dcdata);
                stockRequest.Page = 1;
                stockRequest.Size = 100;
                DataSet dataSet = stockRequest.GetFranchiseStock();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<FranchiseeProductData> listResponses = new List<FranchiseeProductData>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            FranchiseeProductData productList = new FranchiseeProductData();
                            productList.VarientId = dataSet.Tables[0].Rows[i]["Id"].ToString();
                            productList.ProductName = dataSet.Tables[0].Rows[i]["ProductName"].ToString();
                            productList.OfferedPrice = decimal.Parse(dataSet.Tables[0].Rows[i]["OfferedPrice"].ToString());
                            productList.MRP = decimal.Parse(dataSet.Tables[0].Rows[i]["MRP"].ToString());
                            productList.PV = decimal.Parse(dataSet.Tables[0].Rows[i]["PV"].ToString());

                            listResponses.Add(productList);
                        }
                        franchiseeStockDetails.ProductList = listResponses;
                        objres.Message = "";
                        objres.Status = 1;
                        objres.Response = franchiseeStockDetails;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FranchiseeStockResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("ProductsListForFranchise")]
        [Produces("application/json")]
        public ResponseModel ProductsListForFranchise(RequestModel requestModel)
        {
            string EncryptedText = "";
            ProductsListResponse objres = new ProductsListResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                ProductsLists productLists = new ProductsLists();
                Stock stockRequest = new Stock();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                stockRequest = JsonConvert.DeserializeObject<Stock>(dcdata);

                DataSet dataSet = stockRequest.GetProductList();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<ProductsListData> listResponses = new List<ProductsListData>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            ProductsListData productList = new ProductsListData();
                            productList.VarientId = dataSet.Tables[0].Rows[i]["VarientId"].ToString();
                            productList.ProductName = dataSet.Tables[0].Rows[i]["Name"].ToString();
                            productList.OfferedPrice = decimal.Parse(dataSet.Tables[0].Rows[i]["OfferedPrice"].ToString());
                            productList.MRP = decimal.Parse(dataSet.Tables[0].Rows[i]["MRP"].ToString());
                            productList.PV = decimal.Parse(dataSet.Tables[0].Rows[i]["PV"].ToString());
                            productList.BoxPrice = decimal.Parse(dataSet.Tables[0].Rows[i]["BoxPrice"].ToString());
                            productList.BoxOty = int.Parse(dataSet.Tables[0].Rows[i]["BoxOty"].ToString());

                            listResponses.Add(productList);
                        }
                        productLists.ProductList = listResponses;
                        objres.Message = "";
                        objres.Status = 1;
                        objres.Response = productLists;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ProductsListResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("CreatorBusiness")]
        [Produces("application/json")]
        public ResponseModel CreatorBusiness(RequestModel requestModel)
        {
            string EncryptedText = "";
            CreatorBusinessResponse objres = new CreatorBusinessResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                CreatorBusinessList creatorBusinessList = new CreatorBusinessList();
                CreatorBusiness creatorBusiness = new CreatorBusiness();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                creatorBusiness = JsonConvert.DeserializeObject<CreatorBusiness>(dcdata);
                creatorBusiness.IsCHarmony = 0;
                DataSet dataSet = creatorBusiness.GetCreatorBusiness();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<CreatorBusinessData> listResponses = new List<CreatorBusinessData>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            CreatorBusinessData creatorBusinessData = new CreatorBusinessData();
                            creatorBusinessData.LoginId = dataSet.Tables[0].Rows[i]["LoginId"].ToString();
                            creatorBusinessData.Name = dataSet.Tables[0].Rows[i]["Name"].ToString();
                            creatorBusinessData.OrderDate = dataSet.Tables[0].Rows[i]["OrderDate"].ToString();
                            creatorBusinessData.OrderAmount = decimal.Parse(dataSet.Tables[0].Rows[i]["OrderAmount"].ToString());
                            creatorBusinessData.TotalPV = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalPV"].ToString());
                            creatorBusinessData.Iscalc = bool.Parse(dataSet.Tables[0].Rows[i]["Iscalc"].ToString());
                            creatorBusinessData.Status = dataSet.Tables[0].Rows[i]["Status"].ToString();
                            creatorBusinessData.CalcDate = dataSet.Tables[0].Rows[i]["CalcDate"].ToString();
                            creatorBusinessData.TotalRecords = int.Parse(dataSet.Tables[0].Rows[i]["TotalRecords"].ToString());

                            listResponses.Add(creatorBusinessData);
                        }
                        creatorBusinessList.CreatorBusinessLst = listResponses;
                        objres.Message = "";
                        objres.Status = 1;
                        objres.Response = creatorBusinessList;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CreatorBusinessResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("CreatorHarmony")]
        [Produces("application/json")]
        public ResponseModel CreatorHarmony(RequestModel requestModel)
        {
            string EncryptedText = "";
            CreatorHarmonyResponse objres = new CreatorHarmonyResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {

                CreatorHarmonyList creatorBusinessList = new CreatorHarmonyList();
                CreatorBusiness creatorBusiness = new CreatorBusiness();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                creatorBusiness = JsonConvert.DeserializeObject<CreatorBusiness>(dcdata);
                creatorBusiness.IsCHarmony = 1;
                DataSet dataSet = creatorBusiness.GetCreatorBusiness();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<CreatorBusinessData> listResponsesLeft = new List<CreatorBusinessData>();
                        List<CreatorBusinessData> listResponsesRight = new List<CreatorBusinessData>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            CreatorBusinessData creatorBusinessData = new CreatorBusinessData();
                            creatorBusinessData.LoginId = dataSet.Tables[0].Rows[i]["LoginId"].ToString();
                            creatorBusinessData.Name = dataSet.Tables[0].Rows[i]["Name"].ToString();
                            creatorBusinessData.OrderDate = dataSet.Tables[0].Rows[i]["OrderDate"].ToString();
                            creatorBusinessData.OrderAmount = decimal.Parse(dataSet.Tables[0].Rows[i]["OrderAmount"].ToString());
                            creatorBusinessData.TotalPV = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalPV"].ToString());
                            creatorBusinessData.Iscalc = bool.Parse(dataSet.Tables[0].Rows[i]["Iscalc"].ToString());
                            creatorBusinessData.Status = dataSet.Tables[0].Rows[i]["Status"].ToString();
                            creatorBusinessData.CalcDate = dataSet.Tables[0].Rows[i]["CalcDate"].ToString();
                            creatorBusinessData.TotalRecords = int.Parse(dataSet.Tables[0].Rows[i]["TotalRecords"].ToString());

                            listResponsesLeft.Add(creatorBusinessData);

                        }
                        for (int i = 0; i <= dataSet.Tables[1].Rows.Count - 1; i++)
                        {
                            CreatorBusinessData creatorBusinessData = new CreatorBusinessData();
                            creatorBusinessData.LoginId = dataSet.Tables[1].Rows[i]["LoginId"].ToString();
                            creatorBusinessData.Name = dataSet.Tables[1].Rows[i]["Name"].ToString();
                            creatorBusinessData.OrderDate = dataSet.Tables[1].Rows[i]["OrderDate"].ToString();
                            creatorBusinessData.OrderAmount = decimal.Parse(dataSet.Tables[1].Rows[i]["OrderAmount"].ToString());
                            creatorBusinessData.TotalPV = decimal.Parse(dataSet.Tables[1].Rows[i]["TotalPV"].ToString());
                            creatorBusinessData.Iscalc = bool.Parse(dataSet.Tables[1].Rows[i]["Iscalc"].ToString());
                            creatorBusinessData.Status = dataSet.Tables[1].Rows[i]["Status"].ToString();
                            creatorBusinessData.CalcDate = dataSet.Tables[1].Rows[i]["CalcDate"].ToString();
                            creatorBusinessData.TotalRecords = int.Parse(dataSet.Tables[1].Rows[i]["TotalRecords"].ToString());

                            listResponsesRight.Add(creatorBusinessData);
                        }
                        creatorBusinessList.CreatorHamonyL = listResponsesLeft;
                        creatorBusinessList.CreatorHamonyR = listResponsesRight;


                        objres.Message = "";
                        objres.Status = 1;
                        objres.Response = creatorBusinessList;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CreatorHarmonyResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        //[HttpPost("GetProductReview")]
        //[Produces("application/json")]
        //public ResponseModel GetProductReview(RequestModel requestModel)
        //{
        //    ResponseModel returnResponse = new ResponseModel();
        //    ProductReviewRequest reviewRequest=new ProductReviewRequest();
        //    string EncryptedText = "";
        //    ProductDetailsResponse objres = new ProductDetailsResponse();
        //    try
        //    {

        //        List<RatingData> lstRating = new List<RatingData>();
        //        List<ImageData> lstImage = new List<ImageData>();
        //        List<Collection> lstRelatedProd = new List<Collection>();
        //        List<Collection> lstSingleProduct = new List<Collection>();
        //        ProductDetail productDetail = new ProductDetail();
        //        string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
        //        productDetail = JsonConvert.DeserializeObject<ProductDetail>(dcdata);
        //        //DataSet dataSet = reviewRequest.GetProductDeatils();
        //        if (dataSet != null)
        //        {

        //            ProductData productData = new ProductData();

        //            if (dataSet.Tables[1] != null)
        //            {
        //                if (dataSet.Tables[1].Rows.Count > 0)
        //                {
        //                    for (int j = 0; j <= dataSet.Tables[1].Rows.Count - 1; j++)
        //                    {
        //                        RatingData ratingData = new RatingData();
        //                        ratingData.RatingDate = dataSet.Tables[1].Rows[j]["RatingDate"].ToString();
        //                        ratingData.Name = dataSet.Tables[1].Rows[j]["Name"].ToString();
        //                        ratingData.LoginId = dataSet.Tables[1].Rows[j]["LoginId"].ToString();
        //                        ratingData.Rating = dataSet.Tables[1].Rows[j]["Rating"].ToString();
        //                        ratingData.Star = dataSet.Tables[1].Rows[j]["Star"].ToString();

        //                        lstRating.Add(ratingData);
        //                    }
        //                    productData.RatingList = lstRating;

        //                }
        //            }

        //            objres.Message = "";
        //            objres.Status = 1;
        //            objres.Response = productData;

        //        }
        //        else
        //        {
        //            objres.Message = Messages.NoRecordFound;
        //            objres.Status = 0;

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        objres.Message = Messages.ProblemInConnection;
        //        objres.Status = 0;
        //    }
        //    string CustData = "";
        //    DataContractJsonSerializer js;
        //    MemoryStream ms;
        //    js = new DataContractJsonSerializer(typeof(ProductDetailsResponse));
        //    ms = new MemoryStream();
        //    js.WriteObject(ms, objres);
        //    ms.Position = 0;
        //    StreamReader sr = new StreamReader(ms);
        //    CustData = sr.ReadToEnd();
        //    sr.Close();
        //    ms.Close();
        //    EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
        //    returnResponse.Body = EncryptedText;
        //    return returnResponse;
        //}

        [HttpPost("CancelOrder")]
        [Produces("application/json")]
        public ResponseModel CancelOrder(RequestModel requestModel)
        {
            string EncryptedText = "";
            CancelOrderResponse objres = new CancelOrderResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                CreateOrder createOrder = new CreateOrder();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                createOrder = JsonConvert.DeserializeObject<CreateOrder>(dcdata);
                DataSet dataSet = createOrder.CancelOrder();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                        objres.Status = 1;
                        Pushnotification pushnotification = new Pushnotification();
                        //if (dataSet.Tables[0].Rows.Count > 0)
                        //{
                        //    pushnotification.Notification(dataSet.Tables[1].Rows[0]["DeviceToken"].ToString(), "Your Order No " + dataSet.Tables[0].Rows[0]["OrderNo"].ToString() + "Seccussfully !", "Cancel Order", dataSet.Tables[1].Rows[0]["LoginFrom"].ToString(), "OrderList");
                        //}
                    }
                    else
                    {
                        objres.Message = Messages.ProblemInConnection;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CancelOrderResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("CancelFranchiseeOrder")]
        [Produces("application/json")]
        public ResponseModel CancelFranchiseeOrder(RequestModel requestModel)
        {
            string EncryptedText = "";
            CancelFranchiseeOrderResponse objres = new CancelFranchiseeOrderResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                CreateOrder createOrder = new CreateOrder();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                createOrder = JsonConvert.DeserializeObject<CreateOrder>(dcdata);
                DataSet dataSet = createOrder.CancelFranchiseeOrder();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                        objres.Status = 1;
                        //Pushnotification pushnotification = new Pushnotification();
                        //if (dataSet.Tables[0].Rows.Count > 0)
                        //{
                        //    pushnotification.Notification(dataSet.Tables[1].Rows[0]["DeviceToken"].ToString(), "Your Order No " + dataSet.Tables[0].Rows[0]["OrderNo"].ToString() + "Seccussfully !", "Cancel Order", dataSet.Tables[1].Rows[0]["LoginFrom"].ToString(), "OrderList");
                        //}
                    }
                    else
                    {
                        objres.Message = Messages.ProblemInConnection;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CancelFranchiseeOrderResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("FeedBack")]
        [Produces("application/json")]
        public ResponseModel FeedBack(RequestModel requestModel)
        {
            string EncryptedText = "";
            ContactUsResponse objres = new ContactUsResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {

                ContactUs contactUs = new ContactUs();

                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                contactUs = JsonConvert.DeserializeObject<ContactUs>(dcdata);


                DataSet dataSet = contactUs.SaveFeedback();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {

                            objres.Message = "Your feedback has been submitted successfully please wait for admin approval";
                            objres.Status = 1;

                        }
                        else
                        {
                            objres.Message = "Something Went Wrong";
                            objres.Status = 0;

                        }


                    }
                    else
                    {
                        objres.Message = Messages.ProblemInConnection;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ContactUsResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("GetFranchiseType")]
        [Produces("application/json")]
        public ResponseModel GetFranchiseType(RequestModel requestModel)
        {
            string EncryptedText = "";
            FranchiseTypeResponse objres = new FranchiseTypeResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {

                FranchiseTypeRequest franchiseTypeRequest = new FranchiseTypeRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                franchiseTypeRequest = JsonConvert.DeserializeObject<FranchiseTypeRequest>(dcdata);
                DataSet dataSet = franchiseTypeRequest.GetFranchiseeType();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<FranchiseType> list = new List<FranchiseType>();
                        foreach (DataRow row in dataSet.Tables[0].Rows)
                        {
                            FranchiseType franchiseType = new FranchiseType();
                            franchiseType.FranchiseTypeName = row["TypeName"].ToString();
                            franchiseType.FranchiseTypeId = row["FranchiseTypeId"].ToString();
                            list.Add(franchiseType);

                        }

                        objres.Response = list;
                        objres.Message = "";
                        objres.Status = 1;

                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FranchiseTypeResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("DispatchedOrdersDetails")]
        [Produces("application/json")]
        public ResponseModel DispatchedOrdersDetails(RequestModel requestModel)
        {
            string EncryptedText = "";
            DispatchedOrderDetailsResponse objres = new DispatchedOrderDetailsResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                DispatchedOrderDetailsRequest dispatchedOrdersRequest = new DispatchedOrderDetailsRequest();
                DispatchedOrderDetails dispatchOrder = new DispatchedOrderDetails();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                dispatchedOrdersRequest = JsonConvert.DeserializeObject<DispatchedOrderDetailsRequest>(dcdata);
                dispatchedOrdersRequest.OpCode = 2;
                DataSet dataSet = dispatchedOrdersRequest.GetFranchiseOrders();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<DispatchedOrderDetailsData> listResponses = new List<DispatchedOrderDetailsData>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            DispatchedOrderDetailsData orderDetails = new DispatchedOrderDetailsData();
                            orderDetails.OrderNo = dataSet.Tables[0].Rows[i]["OrderNo"].ToString();
                            orderDetails.ProductName = dataSet.Tables[0].Rows[i]["ProductName"].ToString();
                            orderDetails.Amount = decimal.Parse(dataSet.Tables[0].Rows[i]["MRP"].ToString());
                            orderDetails.PV = decimal.Parse(dataSet.Tables[0].Rows[i]["PV"].ToString());
                            orderDetails.Quantity = int.Parse(dataSet.Tables[0].Rows[i]["Qty"].ToString());
                            orderDetails.SubTotal = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalPrice"].ToString());
                            orderDetails.Fk_ProductId = int.Parse(dataSet.Tables[0].Rows[i]["Fk_VarientId"].ToString());
                            orderDetails.Fk_OrderId = int.Parse(dataSet.Tables[0].Rows[i]["Fk_OrderId"].ToString());
                            orderDetails.IsDispatched = dataSet.Tables[0].Rows[i]["IsDispatched"].ToString();
                            orderDetails.PendingQty = int.Parse(dataSet.Tables[0].Rows[i]["PendingQty"].ToString());
                            orderDetails.CompanyName = dataSet.Tables[0].Rows[i]["CompanyName"].ToString();
                            orderDetails.DispatchQty = int.Parse(dataSet.Tables[0].Rows[i]["DispatchQty"].ToString());
                            orderDetails.Stock = int.Parse(dataSet.Tables[0].Rows[i]["Stock"].ToString());
                            listResponses.Add(orderDetails);
                        }

                        dispatchOrder.OrdersList = listResponses;
                        objres.Message = "";
                        objres.Status = 1;
                        objres.Response = dispatchOrder;
                        //Pushnotification pushnotification = new Pushnotification();
                        //if (dataSet.Tables[0].Rows.Count > 0)
                        //{
                        //    pushnotification.Notification(dataSet.Tables[1].Rows[0]["DeviceToken"].ToString(), "Your Order No " + dataSet.Tables[0].Rows[0]["OrderNo"].ToString() + "Dispatched Seccussfully !", "Dispatch Order", dataSet.Tables[1].Rows[0]["LoginFrom"].ToString(), "OrderList");
                        //}
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(DispatchedOrderDetailsResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("GetTempOpenOrder")]
        [Produces("application/json")]
        public ResponseModel GetTempOpenOrder(RequestModel requestModel)
        {
            string EncryptedText = "";
            OpenOrderTempResponse objres = new OpenOrderTempResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                OpenOrder openOrder = new OpenOrder();
                OpenOrderRequest openOrderRequest = new OpenOrderRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                openOrderRequest = JsonConvert.DeserializeObject<OpenOrderRequest>(dcdata);
                DataSet dataSet = openOrderRequest.GetCustomerTempOpenOrder();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<OpenOrderTempData> listResponses = new List<OpenOrderTempData>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            OpenOrderTempData temporderList = new OpenOrderTempData();
                            temporderList.Pk_Id = dataSet.Tables[0].Rows[i]["Pk_Id"].ToString();
                            temporderList.ProductName = dataSet.Tables[0].Rows[i]["ProductName"].ToString();
                            temporderList.Price = decimal.Parse(dataSet.Tables[0].Rows[i]["Price"].ToString());
                            temporderList.Qty = int.Parse(dataSet.Tables[0].Rows[i]["Qty"].ToString());
                            temporderList.TotalPrice = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalPrice"].ToString());
                            temporderList.GSTPerc = decimal.Parse(dataSet.Tables[0].Rows[i]["GSTPerc"].ToString());
                            temporderList.GSTAmt = decimal.Parse(dataSet.Tables[0].Rows[i]["GSTAmt"].ToString());
                            temporderList.Total = decimal.Parse(dataSet.Tables[0].Rows[i]["Total"].ToString());
                            temporderList.MRP = decimal.Parse(dataSet.Tables[0].Rows[i]["MRP"].ToString());
                            temporderList.TotalBv = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalBv"].ToString());

                            listResponses.Add(temporderList);
                        }

                        openOrder.TempOpenOrderList = listResponses;
                        objres.Message = "";
                        objres.Status = 1;
                        objres.Response = openOrder;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(OpenOrderTempResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("AddTempOpenOrder")]
        [Produces("application/json")]
        public ResponseModel AddTempOpenOrder(RequestModel requestModel)
        {
            string EncryptedText = "";
            OpenOrderTempResponse objres = new OpenOrderTempResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                OpenOrder openOrder = new OpenOrder();
                OpenOrderRequest openOrderRequest = new OpenOrderRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                openOrderRequest = JsonConvert.DeserializeObject<OpenOrderRequest>(dcdata);
                DataSet dataSet = openOrderRequest.CreateTempOpenOrder();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            List<OpenOrderTempData> listResponses = new List<OpenOrderTempData>();
                            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                OpenOrderTempData temporderList = new OpenOrderTempData();
                                temporderList.Pk_Id = dataSet.Tables[0].Rows[i]["Pk_Id"].ToString();
                                temporderList.ProductName = dataSet.Tables[0].Rows[i]["ProductName"].ToString();
                                temporderList.MRP = decimal.Parse(dataSet.Tables[0].Rows[i]["MRP"].ToString());
                                temporderList.Qty = int.Parse(dataSet.Tables[0].Rows[i]["Qty"].ToString());
                                temporderList.GSTPerc = decimal.Parse(dataSet.Tables[0].Rows[i]["GSTPerc"].ToString());
                                temporderList.Total = decimal.Parse(dataSet.Tables[0].Rows[i]["Total"].ToString());
                                temporderList.Price = decimal.Parse(dataSet.Tables[0].Rows[i]["Price"].ToString());
                                temporderList.GSTAmt = decimal.Parse(dataSet.Tables[0].Rows[i]["GSTAmt"].ToString());
                                temporderList.TotalPrice = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalPrice"].ToString());
                                temporderList.TotalBv = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalBv"].ToString());

                                listResponses.Add(temporderList);
                            }

                            openOrder.TempOpenOrderList = listResponses;
                            objres.Message = "";
                            objres.Status = 1;
                            objres.Response = openOrder;
                        }
                        else
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(OpenOrderTempResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("DeleteTempOpenOrder")]
        [Produces("application/json")]
        public ResponseModel DeleteTempOpenOrder(RequestModel requestModel)
        {
            string EncryptedText = "";
            OpenOrderTempResponse objres = new OpenOrderTempResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                OpenOrder openOrder = new OpenOrder();
                OpenOrderRequest openOrderRequest = new OpenOrderRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                openOrderRequest = JsonConvert.DeserializeObject<OpenOrderRequest>(dcdata);
                DataSet dataSet = openOrderRequest.DeleteCustomerTempOpenOrder();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            List<OpenOrderTempData> listResponses = new List<OpenOrderTempData>();
                            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                OpenOrderTempData temporderList = new OpenOrderTempData();
                                temporderList.Pk_Id = dataSet.Tables[0].Rows[i]["Pk_Id"].ToString();
                                temporderList.ProductName = dataSet.Tables[0].Rows[i]["ProductName"].ToString();
                                temporderList.MRP = decimal.Parse(dataSet.Tables[0].Rows[i]["MRP"].ToString());
                                temporderList.Qty = int.Parse(dataSet.Tables[0].Rows[i]["Qty"].ToString());
                                temporderList.GSTPerc = decimal.Parse(dataSet.Tables[0].Rows[i]["GSTPerc"].ToString());
                                temporderList.Total = decimal.Parse(dataSet.Tables[0].Rows[i]["Total"].ToString());
                                temporderList.Price = decimal.Parse(dataSet.Tables[0].Rows[i]["Price"].ToString());
                                temporderList.GSTAmt = decimal.Parse(dataSet.Tables[0].Rows[i]["GSTAmt"].ToString());
                                temporderList.TotalPrice = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalPrice"].ToString());
                                temporderList.TotalBv = decimal.Parse(dataSet.Tables[0].Rows[i]["TotalBv"].ToString());

                                listResponses.Add(temporderList);
                            }

                            openOrder.TempOpenOrderList = listResponses;
                            objres.Message = "";
                            objres.Status = 1;
                            objres.Response = openOrder;
                        }
                        else
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(OpenOrderTempResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("GenerateOpenOrder")]
        [Produces("application/json")]
        public ResponseModel GenerateOpenOrder(RequestModel requestModel)
        {
            string EncryptedText = "";
            OpenOrderResponse objres = new OpenOrderResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                OpenOrder openOrder = new OpenOrder();
                OpenOrderRequest openOrderRequest = new OpenOrderRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                openOrderRequest = JsonConvert.DeserializeObject<OpenOrderRequest>(dcdata);
                DataSet dataSet = openOrderRequest.GenerateCustomerOpenOrder();
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        objres.Message = Messages.OrderPlaced;
                        objres.Status = 1;
                    }
                    else
                    {
                        objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(OpenOrderResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("LoginFromMobileNumber")]
        [Produces("application/json")]
        public ResponseModel LoginFromMobileNumber(RequestModel requestModel)
        {
            string EncryptedText = "";
            LoginResponse objres = new LoginResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                LoginRequest loginRequest = new LoginRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                loginRequest = JsonConvert.DeserializeObject<LoginRequest>(dcdata);
                DataSet dataSet = loginRequest.LoginFromOtp();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            Login login = new Login();
                            login.CustomerId = long.Parse(dataSet.Tables[0].Rows[0]["CustomerId"].ToString());
                            login.FirstName = dataSet.Tables[0].Rows[0]["FirstName"].ToString();
                            login.LastName = dataSet.Tables[0].Rows[0]["LastName"].ToString();
                            login.LoginId = dataSet.Tables[0].Rows[0]["LoginId"].ToString();
                            login.UserType = dataSet.Tables[0].Rows[0]["UserType"].ToString();
                            login.RegistrationAmount = decimal.Parse(dataSet.Tables[0].Rows[0]["RegistrationAmount"].ToString());
                            login.FK_FranchiseTypeId = long.Parse(dataSet.Tables[0].Rows[0]["FK_FranchiseTypeId"].ToString());
                            login.IskycApproved = dataSet.Tables[0].Rows[0]["IskycApproved"].ToString();
                            login.IsBusinessId = dataSet.Tables[0].Rows[0]["IsBusinessId"].ToString();
                            objres.Response = login;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 0;
                        }


                    }
                    else
                    {
                        objres.Message = Messages.InvalidLoginId;
                        objres.Status = 0;

                    }
                }
                else
                {
                    objres.Message = Messages.InvalidLoginId;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(LoginResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("UpdateKYC")]
        [Produces("application/json")]
        public ResponseModel UpdateKYC(RequestModel requestModel)
        {
            string EncryptedText = "";
            KYCResponse objres = new KYCResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                ProfileRequest profileRequest = new ProfileRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                profileRequest = JsonConvert.DeserializeObject<ProfileRequest>(dcdata);
                DataSet dataSet = profileRequest.UpdateKYC();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(KYCResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("GetAssociateAddressDetails")]
        [Produces("application/json")]
        public ResponseModel GetAssociateAddressDetails(RequestModel requestModel)
        {
            string EncryptedText = "";
            OpenOrderGetAddressResponse objres = new OpenOrderGetAddressResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                OpenOrderRequest openOrderRequest = new OpenOrderRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                openOrderRequest = JsonConvert.DeserializeObject<OpenOrderRequest>(dcdata);
                DataSet dataSet = openOrderRequest.GetAddressDetails();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            AddressDetailsData addressDetailsData = new AddressDetailsData();
                            addressDetailsData.ContactNo = dataSet.Tables[0].Rows[0]["ContactNo"].ToString();
                            addressDetailsData.Address = dataSet.Tables[0].Rows[0]["Address"].ToString();
                            addressDetailsData.Locality = dataSet.Tables[0].Rows[0]["Locality"].ToString();
                            addressDetailsData.Pincode = dataSet.Tables[0].Rows[0]["Pincode"].ToString();
                            addressDetailsData.State = dataSet.Tables[0].Rows[0]["State"].ToString();
                            addressDetailsData.City = dataSet.Tables[0].Rows[0]["City"].ToString();
                            addressDetailsData.Landmark = dataSet.Tables[0].Rows[0]["Landmark"].ToString();

                            objres.Response = addressDetailsData;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.ProblemInConnection;
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(OpenOrderGetAddressResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("GetFranchiseeOrderDetails")]
        [Produces("application/json")]
        public ResponseModel GetFranchiseeOrderDetails(RequestModel requestModel)
        {
            string EncryptedText = "";
            OrderDeteailsResponse objres = new OrderDeteailsResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                CreateOrder createOrder = new CreateOrder();
                OrderDeteails orderDeteails = new OrderDeteails();


                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                createOrder = JsonConvert.DeserializeObject<CreateOrder>(dcdata);
                createOrder.Fk_ParentFranchiseId = HttpContext.Session.GetString("FranchiseId");
                createOrder.OpCode = 2;
                DataSet dataSet = createOrder.GetFranchiseOrders();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<OrderDeteailsData> lst = new List<OrderDeteailsData>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            OrderDeteailsData orderDeteailsData = new OrderDeteailsData();
                            orderDeteailsData.OrderNo = dataSet.Tables[0].Rows[i]["OrderNo"].ToString();
                            orderDeteailsData.ProductName = dataSet.Tables[0].Rows[i]["ProductName"].ToString();
                            orderDeteailsData.Amount = decimal.Parse(dataSet.Tables[0].Rows[i]["MRP"].ToString());
                            orderDeteailsData.PV = decimal.Parse(dataSet.Tables[0].Rows[i]["PV"].ToString());
                            orderDeteailsData.Qty = int.Parse(dataSet.Tables[0].Rows[i]["Qty"].ToString());
                            orderDeteailsData.CompanyName = dataSet.Tables[0].Rows[i]["CompanyName"].ToString();
                            orderDeteailsData.DispatchStatus = dataSet.Tables[0].Rows[i]["IsDispatched"].ToString();
                            lst.Add(orderDeteailsData);
                        }
                        objres.Status = 1;
                        objres.Message = "";
                        orderDeteails.listOrderDeteails = lst;
                        objres.Response = orderDeteails;
                    }
                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(OrderDeteailsResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("ValidateInfo")]
        [Produces("application/json")]
        public ResponseModel ValidateInfo(RequestModel requestModel)
        {
            string EncryptedText = "";
            ValidateUserInfoResponse objres = new ValidateUserInfoResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                ValidateUserInfoRequest validateUserInfoRequest = new ValidateUserInfoRequest();

                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                validateUserInfoRequest = JsonConvert.DeserializeObject<ValidateUserInfoRequest>(dcdata);

                DataSet dataSet = validateUserInfoRequest.ValidateDetails();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0 && dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        objres.Status = 1;
                        objres.Message = "";
                    }
                    else
                    {
                        objres.Status = 0;
                        objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();

                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ValidateUserInfoResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("QrCode")]
        [Produces("application/json")]
        public ResponseModel QrCode(RequestModel requestModel)
        {
            string EncryptedText = "";
            QrCodeResponse objres = new QrCodeResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                QrCodeRequest modelRequest = new QrCodeRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                modelRequest = JsonConvert.DeserializeObject<QrCodeRequest>(dcdata);

                DataSet dataSet = modelRequest.QrCodeStatus();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0 && dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        objres.QrStatus = dataSet.Tables[0].Rows[0]["QrStatus"].ToString();
                        objres.Status = 1;
                        objres.Message = "Scanned Successfully";
                    }
                    else
                    {
                        objres.Status = 0;
                        objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(QrCodeResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("GetEventList")]
        [Produces("application/json")]
        public ResponseModel GetEventList(RequestModel requestModel)
        {
            string EncryptedText = "";
            EventListResponse objres = new EventListResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                EventList eventList = new EventList();
                EventListRequest eventListRequest = new EventListRequest();

                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                eventListRequest = JsonConvert.DeserializeObject<EventListRequest>(dcdata);
                eventListRequest.OpCode = 5;

                DataSet dataSet = eventListRequest.GetEventList();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<EventListData> lst = new List<EventListData>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            EventListData eventListdata = new EventListData();
                            eventListdata.EventName = dataSet.Tables[0].Rows[0]["EventName"].ToString();
                            eventListdata.Pk_EventId = dataSet.Tables[0].Rows[0]["Pk_EventId"].ToString();
                            eventListdata.EventDescription = dataSet.Tables[0].Rows[0]["EventDescription"].ToString();
                            eventListdata.EventImage = dataSet.Tables[0].Rows[0]["EventImage"].ToString();
                            eventListdata.FromDate = dataSet.Tables[0].Rows[0]["FromDate"].ToString();
                            eventListdata.ToDate = dataSet.Tables[0].Rows[0]["ToDate"].ToString();
                            eventListdata.IsConfirmed = bool.Parse(dataSet.Tables[0].Rows[0]["IsConfirmed"].ToString());
                            eventListdata.NoOfSeats = dataSet.Tables[0].Rows[0]["NoOfSeats"].ToString();
                            eventListdata.EventTime = dataSet.Tables[0].Rows[0]["EventTime"].ToString();
                            eventListdata.Amount = dataSet.Tables[0].Rows[0]["Amount"].ToString();
                            eventListdata.PV = dataSet.Tables[0].Rows[0]["PV"].ToString();
                            eventListdata.Address = dataSet.Tables[0].Rows[0]["Address"].ToString();
                            eventListdata.Fk_MemId = dataSet.Tables[0].Rows[0]["Fk_MemId"].ToString();
                            eventListdata.AssociateImage = dataSet.Tables[0].Rows[0]["AssociateImage"].ToString();
                            eventListdata.AvaliableSeats = dataSet.Tables[0].Rows[0]["AvaliableSeats"].ToString();
                            eventListdata.Pk_EventBookingId = dataSet.Tables[0].Rows[0]["Pk_EventBookingId"].ToString();
                            eventListdata.ProfilePic = dataSet.Tables[0].Rows[0]["ProfilePic"].ToString();
                            eventListdata.PanCard = dataSet.Tables[0].Rows[0]["PanCard"].ToString();
                            string id = eventListdata.Pk_EventId + '-' + eventListdata.Fk_MemId;
                            QRCodeGenerator ObjQr = new QRCodeGenerator();
                            QRCodeData qrCodeData = ObjQr.CreateQrCode(id, QRCodeGenerator.ECCLevel.Q);
                            Bitmap bitMap = new QRCode(qrCodeData).GetGraphic(20);
                            using (MemoryStream ms1 = new MemoryStream())
                            {
                                bitMap.Save(ms1, System.Drawing.Imaging.ImageFormat.Png);
                                byte[] byteImage = ms1.ToArray();
                                eventListdata.QrCode = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                            }
                            lst.Add(eventListdata);
                        }
                        objres.Status = 1;
                        objres.Message = "";
                        eventList.EventListDetails = lst;
                        objres.Response = eventList;
                    }
                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(EventListResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("ConfirmEvent")]
        [Produces("application/json")]
        public ResponseModel ConfirmEvent(RequestModel requestModel)
        {
            ConfirmEventResponse objres = new ConfirmEventResponse();
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                ConfirmEventRequest modelRequest = new ConfirmEventRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                modelRequest = JsonConvert.DeserializeObject<ConfirmEventRequest>(dcdata);
                DataSet dataSet = modelRequest.ConfirmEventBooking();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0 && dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        objres.Status = 1;
                        objres.Message = "";
                    }
                    else
                    {
                        objres.Status = 0;
                        objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();

                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ConfirmEventResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("UploadAssociateImage")]
        [Produces("application/json")]
        public ResponseModel UploadAssociateImage(RequestModel requestModel)
        {
            AssociateImageResponse objres = new AssociateImageResponse();
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                AssociateImageList associateImage = new AssociateImageList();
                EventListRequest modelRequest = new EventListRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                modelRequest = JsonConvert.DeserializeObject<EventListRequest>(dcdata);
                DataSet dataSet = modelRequest.UploadAssociateImage();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0 && dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        objres.Status = 1;
                        List<AssociateImageData> lst = new List<AssociateImageData>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            AssociateImageData associateImageData = new AssociateImageData();
                            associateImageData.AssociateImage = dataSet.Tables[0].Rows[0]["AssociateImage"].ToString();
                            associateImageData.Pk_EventBookingId = dataSet.Tables[0].Rows[0]["Pk_EventBookingId"].ToString();

                            lst.Add(associateImageData);
                        }

                        associateImage.EventAssociateImage = lst;
                        objres.Response = associateImage;
                        objres.Message = "";
                    }
                    else
                    {
                        objres.Status = 0;
                        objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();

                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AssociateImageResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("PromoCode")]
        [Produces("application/json")]
        public ResponseModel PromoCode(RequestModel requestModel)
        {
            PromoCodeResponse objres = new PromoCodeResponse();
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                CreateOrder modelRequest = new CreateOrder();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                modelRequest = JsonConvert.DeserializeObject<CreateOrder>(dcdata);
                DataSet dataSet = modelRequest.ApplyPromoCode();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0 && dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                    {
                        objres.Status = 1;
                        objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                        objres.OrderAmount = Convert.ToDecimal(dataSet.Tables[0].Rows[0]["OrderAmount"].ToString());
                    }
                    else
                    {
                        objres.Status = 0;
                        objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                        objres.OrderAmount = Convert.ToDecimal(dataSet.Tables[0].Rows[0]["OrderAmount"].ToString());
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(PromoCodeResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("AutoLogin")]
        [Produces("application/json")]
        public ResponseModel AutoLogin(RequestModel requestModel)
        {
            string EncryptedText = "";
            LoginResponse objres = new LoginResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                LoginRequest loginRequest = new LoginRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                loginRequest = JsonConvert.DeserializeObject<LoginRequest>(dcdata);
                DataSet dataSet = loginRequest.AutoLoginAction();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            Login login = new Login();
                            login.CustomerId = long.Parse(dataSet.Tables[0].Rows[0]["CustomerId"].ToString());
                            login.FirstName = dataSet.Tables[0].Rows[0]["FirstName"].ToString();
                            login.LastName = dataSet.Tables[0].Rows[0]["LastName"].ToString();
                            login.LoginId = dataSet.Tables[0].Rows[0]["LoginId"].ToString();
                            login.UserType = dataSet.Tables[0].Rows[0]["UserType"].ToString();
                            login.RegistrationAmount = decimal.Parse(dataSet.Tables[0].Rows[0]["RegistrationAmount"].ToString());
                            login.FK_FranchiseTypeId = long.Parse(dataSet.Tables[0].Rows[0]["FK_FranchiseTypeId"].ToString());
                            login.IskycApproved = dataSet.Tables[0].Rows[0]["IskycApproved"].ToString();
                            login.IsBusinessId = dataSet.Tables[0].Rows[0]["IsBusinessId"].ToString();
                            login.TotalOrder = int.Parse(dataSet.Tables[0].Rows[0]["TotalOrder"].ToString());
                            login.Title = dataSet.Tables[0].Rows[0]["Title"].ToString();
                            login.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            //login.AppStatus = dataSet.Tables[0].Rows[0]["AppStatus"].ToString();
                            objres.Response = login;
                            objres.Message = "Success";
                            objres.Status = int.Parse(dataSet.Tables[0].Rows[0]["Status"].ToString());
                        }
                        else
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.InvalidLoginId;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.InvalidLoginId;
                    objres.Status = 0;
                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(LoginResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpGet("GetWelcomeLetter")]
        //[Produces("application/json")]
        public async Task<ActionResult> GetWelcomeLetter(string CustomerId)
        {
            string EncryptedText = "";
            ProfileReposne objres = new ProfileReposne();
            ResponseModel returnResponse = new ResponseModel();
            StringBuilder sb = new StringBuilder();
            try
            {
                ProfileRequest profileRequest = new ProfileRequest();
                //string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                //profileRequest = JsonConvert.DeserializeObject<ProfileRequest>(dcdata);
                profileRequest.CustomerId = int.Parse(CustomerId);
                DataSet dataSet = profileRequest.GetAssociateProfile();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        Profile profile = new Profile();
                        profile.LoginId = dataSet.Tables[0].Rows[0]["LoginId"].ToString();
                        profile.FirstName = dataSet.Tables[0].Rows[0]["FirstName"].ToString();
                        profile.LastName = dataSet.Tables[0].Rows[0]["LastName"].ToString();
                        profile.MobileNo = dataSet.Tables[0].Rows[0]["MobileNo"].ToString();
                        profile.Gender = dataSet.Tables[0].Rows[0]["Gender"].ToString();
                        profile.Status = dataSet.Tables[0].Rows[0]["Status"].ToString();
                        profile.PanCard = dataSet.Tables[0].Rows[0]["PanCard"].ToString();
                        profile.JoiningDate = dataSet.Tables[0].Rows[0]["JoiningDate"].ToString();
                        profile.Address = dataSet.Tables[0].Rows[0]["Address"].ToString();
                        profile.Email = dataSet.Tables[0].Rows[0]["Email"].ToString();
                        objres.Message = "";
                        objres.Status = "1";
                        objres.Response = profile;

                        sb.Append("<html lang='en'>");
                        sb.Append("<head>");
                        sb.Append("<meta charset='UTF-8'>");
                        sb.Append("<meta http-equiv='X-UA-Compatible' content='IE=edge'>");
                        sb.Append("<meta name='viewport' content='width=device-width, initial-scale=1.0'>");
                        sb.Append("<link id='style' href='https://www.karyon.organic/AssociatePanel/assets/plugins/bootstrap/css/bootstrap.min.css' rel='stylesheet'/>");
                        //sb.Append("<link href='https://www.karyon.organic/AssociatePanel/assets/css/style.css' rel='stylesheet'>");
                        sb.Append("<title>Document</title>");
                        sb.Append("</head>");
                        sb.Append("<body style='padding: 25px 25px;'>");
                        sb.Append("<div>");
                        sb.Append("<div>");
                        sb.Append("<div>");
                        sb.Append("<div>");
                        sb.Append("<div>");
                        sb.Append("<div>");
                        sb.Append("<div>");
                        sb.Append("<div style='display: flex; flex-direction: row; align-items: center; justify-content: space-between;'>");
                        sb.Append("<div>");
                        sb.Append("<img style='width: 150px; padding-left: 5px; margin-top: 0;' src='https://www.karyon.organic/assets/images/logo.png' alt=''>");
                        sb.Append("</div>");
                        sb.Append("<div>");
                        sb.Append("<p>");
                        sb.Append("<span style='font-weight: bold; color: rgb(71, 67, 67);'>Corporate Office :</span>");
                        sb.Append("" + CompanyDetails.Address1 + "<br>");
                        sb.Append("" + CompanyDetails.Address2 + "<br>");
                        sb.Append("" + CompanyDetails.Address3 + "<br>");
                        sb.Append("</p>");
                        sb.Append("</div>");
                        sb.Append("</div>");
                        sb.Append("<hr>");
                        sb.Append("<h4>Welcome Letter</h4>");
                        sb.Append("<div>");
                        sb.Append("<div>");
                        sb.Append("<div style='display: flex; flex-direction: row; align-items: center; justify-content: space-between;'>");
                        sb.Append("<div><p>Name: " + profile.FirstName + " " + profile.LastName + " </div>");
                        sb.Append("</div>");
                        sb.Append("<p>Joining Date: " + profile.JoiningDate + "</p>");
                        sb.Append("<p>Mobile No.: " + profile.MobileNo + "</p>");
                        sb.Append("<p>PAN No.: " + profile.PanCard + "</p>");
                        sb.Append("<p>Address: " + profile.Address + "</p>");
                        sb.Append("<p>");
                        sb.Append(" The <span style='font-weight: bold;'>" + CompanyDetails.ComapanyName + "</span>");
                        sb.Append(" Would like to welcome you as a valuable customer to our company.");
                        sb.Append("We know that you will be satisfied with our projects and the services which we provide to our customers.");
                        sb.Append("</p>");
                        sb.Append("<p>");
                        sb.Append("We recommend you to visit our website <span style='font-weight: bold;'>" + CompanyDetails.Website + "</span>");
                        sb.Append(" for login your user profile for edit your profile, view your business, register your clients etc");
                        sb.Append(" and also to access other products & Services being offered by <span style='font-weight: bold;'>" + @CompanyDetails.ComapanyName + ".</span>");
                        sb.Append(" Please intimate us by email on <span style='font-weight: bold;'>" + @CompanyDetails.Email + "</span> for any changes in your information i.e. Bank Details, mobile number with Proof along with a scanned copy of request letter duly signed by you so that we can update your information accordingly. Your login details are:");
                        sb.Append("</p>");
                        sb.Append("<p style='font-weight: bold; color: rgb(71, 67, 67);'>User ID: " + profile.LoginId + "</p>");
                        sb.Append("<p>We once again thank you for choosing <span style='font-weight: bold;'>" + @CompanyDetails.ComapanyName + "</span> and assure you of our best services at all times. I do hope you will afford us the opportunity to serve you in the near future.</p>");
                        sb.Append("</div>");
                        sb.Append("<div>");
                        sb.Append("<img style='width: 200px; padding-left: 25px;' src='https://www.karyon.organic/assets/images/logo.png' alt=''>");
                        sb.Append("</div>");
                        sb.Append("</div>");
                        sb.Append("<div class='offset-10'>");
                        //sb.Append("<button type='button' class='btn btn-success btn-rounded btnprint' onclick='printDiv('testTable')' title='' data-toggle='tooltip' data-original-title='Print'><i class='fa fa-print'></i></button>");
                        sb.Append("</div>");
                        sb.Append("</div>");
                        sb.Append("</div>");
                        sb.Append("</div>");
                        sb.Append("</div>");
                        sb.Append("</div>");
                        sb.Append("</div>");
                        sb.Append("</div>");
                        sb.Append("</body>");
                        sb.Append("</html>");
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = "0";
                    }

                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = "0";
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = "0";
            }
            string CustData = "";
            //SelectPdf.HtmlToPdf ToPdf = new SelectPdf.HtmlToPdf();
            //PdfDocument Document = ToPdf.ConvertHtmlString(sb.ToString());
            //byte[] pdf = Document.Save();
            //Document.Close();
            return null;
        }

        [HttpPost("GetPackPurchased")]
        [Produces("application/json")]
        public ResponseModel GetPackPurchased()
        {
            string EncryptedText = "";
            PackPurchase packPurchase = new PackPurchase();
            PackPurchaseResponse objres = new PackPurchaseResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                DataSet ds = packPurchase.GetPackPurchase();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        PackPurchaseList packPurchaseList = new PackPurchaseList();
                        List<PackPurchaseData> list = new List<PackPurchaseData>();
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            PackPurchaseData packPurchaseData = new PackPurchaseData();
                            packPurchaseData.Pk_PackId = ds.Tables[0].Rows[i]["Pk_PackId"].ToString();
                            packPurchaseData.PackName = ds.Tables[0].Rows[i]["PackName"].ToString();
                            packPurchaseData.Amount = ds.Tables[0].Rows[i]["Amount"].ToString();
                            packPurchaseData.Offer = ds.Tables[0].Rows[i]["Offer"].ToString();
                            packPurchaseData.Note = ds.Tables[0].Rows[i]["Note"].ToString();
                            list.Add(packPurchaseData);
                        }
                        objres.Status = 1;
                        objres.Message = "Success";
                        packPurchaseList.PackList = list;
                        packPurchaseList.OpCode = "{\"OpCode\":\"22\"}";
                        objres.Response = packPurchaseList;
                    }
                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(PackPurchaseResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("GetCriteria")]
        [Produces("application/json")]
        public ResponseModel GetCriteria(RequestModel requestModel)
        {
            string EncryptedText = "";
            Criteria model = new Criteria();
            ResponseModel returnResponse = new ResponseModel();
            CriteriaResponse objres = new CriteriaResponse();
            try
            {
                CriteriaRequest criteriaRequest = new CriteriaRequest();
                string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                criteriaRequest = JsonConvert.DeserializeObject<CriteriaRequest>(dcdata);
                model.Fk_MemId = criteriaRequest.Fk_MemId;
                DataSet dataSet = model.CriteriaDashboard();
                if (dataSet != null)
                {
                    if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {
                        CriteriaResponseList criteriaResponseList = new CriteriaResponseList();
                        CriteriaResponseDataList criteriaResponseDataList = new CriteriaResponseDataList();
                        CriteriaResponseDataList1 criteriaResponseDataList1 = new CriteriaResponseDataList1();
                        List<CriteriaResponseData> list = new List<CriteriaResponseData>();
                        for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                        {
                            CriteriaResponseData criteriaResponseData = new CriteriaResponseData();
                            criteriaResponseData.Biztype = dataSet.Tables[0].Rows[i]["Biztype"].ToString();
                            criteriaResponseData.AzoneTarget = dataSet.Tables[0].Rows[i]["AzoneTarget"].ToString();
                            criteriaResponseData.BzoneTarget = dataSet.Tables[0].Rows[i]["BzoneTarget"].ToString();
                            criteriaResponseData.azoneachieved = dataSet.Tables[0].Rows[i]["azoneachieved"].ToString();
                            criteriaResponseData.bzoneachieved = dataSet.Tables[0].Rows[i]["bzoneachieved"].ToString();
                            criteriaResponseData.azonebal = dataSet.Tables[0].Rows[i]["azonebal"].ToString();
                            criteriaResponseData.bzonebal = dataSet.Tables[0].Rows[i]["bzonebal"].ToString();
                            criteriaResponseData.Status = dataSet.Tables[0].Rows[i]["Status"].ToString();
                            list.Add(criteriaResponseData);
                        }

                        List<CriteriaResponseData1> list1 = new List<CriteriaResponseData1>();
                        for (int i = 0; i < dataSet.Tables[1].Rows.Count; i++)
                        {
                            CriteriaResponseData1 criteriaResponseData1 = new CriteriaResponseData1();
                            criteriaResponseData1.Biztype = dataSet.Tables[1].Rows[i]["Biztype"].ToString();
                            criteriaResponseData1.AzoneTarget = dataSet.Tables[1].Rows[i]["AzoneTarget"].ToString();
                            criteriaResponseData1.BzoneTarget = dataSet.Tables[1].Rows[i]["BzoneTarget"].ToString();
                            criteriaResponseData1.azoneachieved = dataSet.Tables[1].Rows[i]["azoneachieved"].ToString();
                            criteriaResponseData1.bzoneachieved = dataSet.Tables[1].Rows[i]["bzoneachieved"].ToString();
                            criteriaResponseData1.azonebal = dataSet.Tables[1].Rows[i]["azonebal"].ToString();
                            criteriaResponseData1.bzonebal = dataSet.Tables[1].Rows[i]["bzonebal"].ToString();
                            criteriaResponseData1.Status = dataSet.Tables[1].Rows[i]["Status"].ToString();
                            list1.Add(criteriaResponseData1);
                        }
                        objres.Status = 1;
                        objres.Message = "Success";
                        criteriaResponseDataList.CriteriaResponseData = list;
                        criteriaResponseDataList1.CriteriaResponseData1 = list1;
                        criteriaResponseList.ResponseList = criteriaResponseDataList;
                        criteriaResponseList.ResponseList1 = criteriaResponseDataList1;
                        objres.Response = criteriaResponseList;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CriteriaResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("GetCriteriaDetails")]
        [Produces("application/json")]
        public ResponseModel GetCriteriaDetails(RequestModel requestModel)
        {
            string EncryptedText = "";
            Criteria model = new Criteria();
            ResponseModel returnResponse = new ResponseModel();
            CriteriaDetailsResponse objres = new CriteriaDetailsResponse();
            CriteriaDetailsResponseList criteriaResponseList = new CriteriaDetailsResponseList();
            try
            {
                CriteriaRequest criteriaRequest = new CriteriaRequest();
                string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                criteriaRequest = JsonConvert.DeserializeObject<CriteriaRequest>(dcdata);
                model.Fk_MemId = criteriaRequest.Fk_MemId;
                DataSet dataSet = model.GetCriteriaDetails();
                if (dataSet != null)
                {
                    CriteriaDetailsDataList criteriaDetailsDataList = new CriteriaDetailsDataList();
                    CriteriaDetailsDataList1 criteriaDetailsDataList1 = new CriteriaDetailsDataList1();

                    if (dataSet.Tables.Count > 0)
                    {
                        List<CriteriaDetailsData> list = new List<CriteriaDetailsData>();
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                            {
                                CriteriaDetailsData datalist = new CriteriaDetailsData();
                                datalist.LoginId = dataSet.Tables[0].Rows[i]["LoginId"].ToString();
                                datalist.Name = dataSet.Tables[0].Rows[i]["Name"].ToString();
                                datalist.PV = dataSet.Tables[0].Rows[i]["TotalPV"].ToString();
                                list.Add(datalist);
                            }
                        }
                        List<CriteriaDetailsData1> list1 = new List<CriteriaDetailsData1>();
                        if (dataSet.Tables[1].Rows.Count > 0)
                        {
                            for (int i = 0; i < dataSet.Tables[1].Rows.Count; i++)
                            {
                                CriteriaDetailsData1 datalist1 = new CriteriaDetailsData1();
                                datalist1.LoginId = dataSet.Tables[1].Rows[i]["LoginId"].ToString();
                                datalist1.Name = dataSet.Tables[1].Rows[i]["Name"].ToString();
                                datalist1.PV = dataSet.Tables[1].Rows[i]["TotalPV"].ToString();
                                list1.Add(datalist1);
                            }
                        }
                        objres.Status = "1";
                        objres.Message = "Success";
                        criteriaDetailsDataList.CriteriaDetailsList = list;
                        criteriaDetailsDataList1.CriteriaDetailsList1 = list1;
                        //criteriaResponseList.CriteriaList = criteriaDetailsDataList;
                        //criteriaResponseList.CriteriaList1 = criteriaDetailsDataList1;
                        objres.Response = criteriaResponseList;
                    }
                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = "0";
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = "0";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CriteriaDetailsResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("GetFranchiseeMenu")]
        [Produces("application/json")]
        public ResponseModel GetFranchiseeMenu(RequestModel requestModel)
        {
            string EncryptedText = "";
            Criteria model = new Criteria();
            ResponseModel returnResponse = new ResponseModel();
            MenuResponse objres = new MenuResponse();
            MenuResponseList menuResponseList = new MenuResponseList();
            try
            {
                CriteriaRequest criteriaRequest = new CriteriaRequest();
                string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                criteriaRequest = JsonConvert.DeserializeObject<CriteriaRequest>(dcdata);
                model.Fk_MemId = criteriaRequest.Fk_MemId;
                model.OpCode = 1;
                DataSet dataSet = model.GetMenuDetails();
                if (dataSet != null)
                {
                    ; MenuDataList menuDataList = new MenuDataList();

                    if (dataSet.Tables.Count > 0)
                    {


                        List<MenuData> list = new List<MenuData>();
                        MenuResponseList menuresponse = new MenuResponseList();
                        for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                        {
                            MenuData ListData = new MenuData();

                            ListData.MenuName = dataSet.Tables[0].Rows[i]["MenuName"].ToString();
                            ListData.Icon = BaseUrl.LeftRightURL + "" + dataSet.Tables[0].Rows[i]["IconsForApp"].ToString();
                            List<SubMenuData> list1 = new List<SubMenuData>();
                            for (int j = 0; j <= dataSet.Tables[1].Rows.Count - 1; j++)
                            {
                                SubMenuData SubList = new SubMenuData();

                                if (dataSet.Tables[0].Rows[i]["MenuId"].ToString() == dataSet.Tables[1].Rows[j]["Fk_MenuId"].ToString())

                                {

                                    SubList.SubMenuName = dataSet.Tables[1].Rows[j]["SubMenuName"].ToString();
                                    list1.Add(SubList);
                                }
                                ListData.SubMenu = list1;

                            }

                            list.Add(ListData);

                            menuDataList.Menu = list;
                            menuresponse.MenuList = menuDataList;
                        }
                        objres.Status = 1;
                        objres.Message = "Success";

                        objres.Response = menuresponse;
                    }
                }

                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(MenuResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("TransactionStatus")]
        [Produces("application/json")]
        public ResponseModel TransactionStatus(RequestModel requestModel)
        {
            string EncryptedText = "";
            PAYTMResponse objres = new PAYTMResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                PAYTMStatus status = new PAYTMStatus();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                status = JsonConvert.DeserializeObject<PAYTMStatus>(dcdata);
                string orderId = status.TransactionNo;
                string merchantKey = "";
                string MID = "";
                string url = "";
                if (BaseUrl.LocalUrl == "https://karyon.organic")
                {
                    merchantKey = PAYTMGateway.PAYTM_LIVE_MERCHANT_KEY;
                    MID = PAYTMGateway.PAYTM_LIVE_MID;
                    url = PAYTMGateway.PAYTMLive_URL_Status;
                }
                else
                {
                    merchantKey = PAYTMGateway.PAYTM_TEST_MERCHANT_KEY;
                    MID = PAYTMGateway.PAYTM_TEST_MID;
                    url = PAYTMGateway.PAYTMTEST_URL_Status;
                }
                Dictionary<string, string> body = new Dictionary<string, string>();
                Dictionary<string, string> head = new Dictionary<string, string>();
                Dictionary<string, Dictionary<string, string>> requestBody = new Dictionary<string, Dictionary<string, string>>();
                body.Add("mid", MID);
                body.Add("orderId", orderId);
                string paytmChecksum = Checksum.generateSignature(JsonConvert.SerializeObject(body), merchantKey);
                head.Add("signature", paytmChecksum);
                requestBody.Add("body", body);
                requestBody.Add("head", head);
                string post_data = JsonConvert.SerializeObject(requestBody);
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
                    PaytmStatusResponse deserializedres = JsonConvert.DeserializeObject<PaytmStatusResponse>(responseData);
                    if (deserializedres.body.resultInfo.resultCode == "01")
                    {
                        objres.TxnId = deserializedres.body.txnId;
                        objres.Message = deserializedres.body.resultInfo.resultMsg;
                        objres.OrderId = deserializedres.body.orderId;
                        objres.Status = 1;
                    }
                    else
                    {
                        objres.OrderId = deserializedres.body.orderId;
                        objres.Message = deserializedres.body.resultInfo.resultMsg;
                        objres.Status = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(PAYTMResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("GetAssociateAddress")]
        [Produces("application/json")]
        public ResponseModel MemberAddress(RequestModel requestModel)
        {
            string EncryptedText = "";
            OpenOrderGetAddressResponse objres = new OpenOrderGetAddressResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                OpenOrderRequest openOrderRequest = new OpenOrderRequest();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                openOrderRequest = JsonConvert.DeserializeObject<OpenOrderRequest>(dcdata);
                DataSet dataSet = openOrderRequest.GetAssociateAddress();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            AddressDetailsData addressDetailsData = new AddressDetailsData();
                            addressDetailsData.ContactNo = dataSet.Tables[0].Rows[0]["ContactNo"].ToString();
                            addressDetailsData.Address = dataSet.Tables[0].Rows[0]["Address"].ToString();
                            addressDetailsData.Locality = dataSet.Tables[0].Rows[0]["Locality"].ToString();
                            addressDetailsData.Pincode = dataSet.Tables[0].Rows[0]["Pincode"].ToString();
                            addressDetailsData.State = dataSet.Tables[0].Rows[0]["State"].ToString();
                            addressDetailsData.City = dataSet.Tables[0].Rows[0]["City"].ToString();
                            addressDetailsData.Landmark = dataSet.Tables[0].Rows[0]["Landmark"].ToString();
                            addressDetailsData.GSTNo = dataSet.Tables[0].Rows[0]["GSTNo"].ToString();

                            objres.Response = addressDetailsData;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.ProblemInConnection;
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(OpenOrderGetAddressResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("GetAssociateDashBoard2")]
        [Produces("application/json")]
        public ResponseModel GetAssociateDashBoard2(RequestModel requestModel)
        {
            string EncryptedText = "";
            AssociateDashBoard2Response objres = new AssociateDashBoard2Response();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                AssociateDashBoard2Request dashBoardRequest = new AssociateDashBoard2Request();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                dashBoardRequest = JsonConvert.DeserializeObject<AssociateDashBoard2Request>(dcdata);
                AssociateDashBoard2 dashBoard = new AssociateDashBoard2();
                DataSet dataSet = dashBoardRequest.GetAssociateDashBoard();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        AssociateDashBoard2 associateDashBoard = new AssociateDashBoard2();
                        associateDashBoard.MemberStatus = dataSet.Tables[0].Rows[0]["MemberStatus"].ToString();
                        associateDashBoard.BusinessStatus = dataSet.Tables[0].Rows[0]["BusinessStatus"].ToString();
                        associateDashBoard.PerformanceLevel = dataSet.Tables[0].Rows[0]["PerformanceLevel"].ToString();
                        associateDashBoard.LevelPercentage = dataSet.Tables[0].Rows[0]["LevelPercentage"].ToString();
                        associateDashBoard.NextLevel = dataSet.Tables[0].Rows[0]["NextLevel"].ToString();
                        associateDashBoard.TargetPVTo = dataSet.Tables[0].Rows[0]["TargetPVTo"].ToString();
                        associateDashBoard.AchivedPV = dataSet.Tables[0].Rows[0]["AchivedPV"].ToString();
                        associateDashBoard.BalancePv = dataSet.Tables[0].Rows[0]["BalancePv"].ToString();
                        associateDashBoard.MaxLegId = dataSet.Tables[0].Rows[0]["MaxLegId"].ToString();
                        associateDashBoard.MaxLegBusiness = dataSet.Tables[0].Rows[0]["MaxLegBusiness"].ToString();
                        associateDashBoard.TotalPV = dataSet.Tables[0].Rows[0]["TotalPV"].ToString();
                        associateDashBoard.SelfUIDPV = dataSet.Tables[0].Rows[0]["SelfUIDPV"].ToString();
                        associateDashBoard.OtherPv = dataSet.Tables[0].Rows[0]["OtherPv"].ToString();
                        associateDashBoard.SelfBusiness = dataSet.Tables[0].Rows[0]["SelfBusiness"].ToString();
                        associateDashBoard.UIDBusiness = dataSet.Tables[0].Rows[0]["UIDBusiness"].ToString();
                        associateDashBoard.TeamBusiness = dataSet.Tables[0].Rows[0]["TeamBusiness"].ToString();
                        associateDashBoard.IsBid = dataSet.Tables[0].Rows[0]["IsBid"].ToString();
                        associateDashBoard.MobileNo = dataSet.Tables[0].Rows[0]["MobileNo"].ToString();
                        associateDashBoard.UnderLevelCount = dataSet.Tables[0].Rows[0]["UnderLevelCount"].ToString();
                        associateDashBoard.CurrentSelfBusiness = dataSet.Tables[0].Rows[0]["CurrentSelfBusiness"].ToString();
                        associateDashBoard.CurrentUIDBusiness = dataSet.Tables[0].Rows[0]["CurrentUIDBusiness"].ToString();
                        associateDashBoard.CurrentTeamBusiness = dataSet.Tables[0].Rows[0]["CurrentTeamBusiness"].ToString();
                        associateDashBoard.PreviousSelfBusiness = dataSet.Tables[0].Rows[0]["PreviousSelfBusiness"].ToString();
                        associateDashBoard.PreviousUIDBusiness = dataSet.Tables[0].Rows[0]["PreviousUIDBusiness"].ToString();
                        associateDashBoard.PreviousTeamBusiness = dataSet.Tables[0].Rows[0]["PreviousTeamBusiness"].ToString();
                        associateDashBoard.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                        associateDashBoard.Consideredbusiness = dataSet.Tables[0].Rows[0]["Consideredbusiness"].ToString();
                        associateDashBoard.OtherUnderLevelCount = dataSet.Tables[0].Rows[0]["OtherUnderLevelCount"].ToString();
                        associateDashBoard.OtherUnderAchivedCount = dataSet.Tables[0].Rows[0]["OtherUnderAchivedCount"].ToString();

                        associateDashBoard.UnderAchivedCount = dataSet.Tables[0].Rows[0]["UnderAchivedCount"].ToString();
                        associateDashBoard.CurrentTotalPv = dataSet.Tables[0].Rows[0]["CurrentTotalPv"].ToString();

                        objres.Response = associateDashBoard;

                    }
                    List<TeamDetails> associalteList = new List<TeamDetails>();

                    if (dataSet.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i < dataSet.Tables[1].Rows.Count; i++)
                        {
                            TeamDetails listdata = new TeamDetails();
                            listdata.Name = dataSet.Tables[1].Rows[i]["Name"].ToString();
                            listdata.PerformanceLevel = dataSet.Tables[1].Rows[i]["PerformanceLevel"].ToString();
                            listdata.Business = dataSet.Tables[1].Rows[i]["Business"].ToString();
                            listdata.NextLevel = dataSet.Tables[1].Rows[i]["NextLevel"].ToString();
                            listdata.Target = dataSet.Tables[1].Rows[i]["Target"].ToString();
                            associalteList.Add(listdata);
                        }

                    }
                    objres.Response.lstAssociates = associalteList;

                    objres.Status = 1;


                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AssociateDashBoard2Response));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("DirectTree")]
        [Produces("application/json")]
        public ResponseModel DirectTree(RequestModel requestModel)
        {
            string EncryptedText = "";
            DirectResponse objres = new DirectResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {

                DirectRequest directRequest = new DirectRequest();
                Direct direct = new Direct();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                directRequest = JsonConvert.DeserializeObject<DirectRequest>(dcdata);

                directRequest.Zone = string.IsNullOrEmpty(directRequest.Zone) ? null : directRequest.Zone;
                directRequest.LoginId = string.IsNullOrEmpty(directRequest.LoginId) ? null : directRequest.LoginId;
                directRequest.PlaceUnderId = string.IsNullOrEmpty(directRequest.PlaceUnderId) ? null : directRequest.PlaceUnderId;
                DataSet dataSet = directRequest.GetDirectListTree();
                if (dataSet != null)
                {

                    if (dataSet.Tables[0].Rows.Count > 0)
                    {

                        List<Data> listResponses = new List<Data>();
                        for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                        {
                            Data data = new Data();

                            data.LoginId = dataSet.Tables[0].Rows[i]["LoginId"].ToString();
                            data.FirstName = dataSet.Tables[0].Rows[i]["Name"].ToString();
                            data.Status = dataSet.Tables[0].Rows[i]["Status"].ToString();
                            data.JoiningDate = dataSet.Tables[0].Rows[i]["JoiningDate"].ToString();
                            data.PermanentDate = dataSet.Tables[0].Rows[i]["PermanentDate"].ToString();
                            data.SponsorName = dataSet.Tables[0].Rows[i]["SponsorName"].ToString();
                            data.SponsorLoginId = dataSet.Tables[0].Rows[i]["SponsorLoginId"].ToString();
                            data.ImageURL = dataSet.Tables[0].Rows[i]["ImageURL"].ToString();
                            data.FK_MemId = dataSet.Tables[0].Rows[i]["fk_memid"].ToString();
                            data.PerformanceLevel = dataSet.Tables[0].Rows[i]["PerformanceLevel"].ToString();
                            data.TeamPV = dataSet.Tables[0].Rows[i]["TeamPV"].ToString();
                            data.SelfPV = dataSet.Tables[0].Rows[i]["SelfPV"].ToString();
                            data.TotalPv = dataSet.Tables[0].Rows[i]["TotalPv"].ToString();
                            data.CurrentSelfPV = dataSet.Tables[0].Rows[i]["CurrentSelfPV"].ToString();
                            data.CurrentTeamPV = dataSet.Tables[0].Rows[i]["CurrentTeamPV"].ToString();
                            data.CurrentTotalPV = dataSet.Tables[0].Rows[i]["CurrentTotalPV"].ToString();
                            data.CurrentUIDPV = dataSet.Tables[0].Rows[i]["CurrentUIDPV"].ToString();
                            data.UIDPV = dataSet.Tables[0].Rows[i]["UIDPV"].ToString();

                            listResponses.Add(data);





                        }
                        List<Data> listResponses1 = new List<Data>();
                        for (int i = 0; i <= dataSet.Tables[1].Rows.Count - 1; i++)
                        {
                            Data data = new Data();

                            data.LoginId = dataSet.Tables[1].Rows[i]["LoginId"].ToString();
                            data.FirstName = dataSet.Tables[1].Rows[i]["FirstName"].ToString();
                            data.LastName = dataSet.Tables[1].Rows[i]["LastName"].ToString();
                            data.Status = dataSet.Tables[1].Rows[i]["Status"].ToString();
                            data.JoiningDate = dataSet.Tables[1].Rows[i]["JoiningDate"].ToString();
                            data.PermanentDate = dataSet.Tables[1].Rows[i]["PermanentDate"].ToString();
                            data.FK_MemId = dataSet.Tables[1].Rows[i]["FK_MemId"].ToString();
                            data.SponsorName = dataSet.Tables[1].Rows[i]["SponsorName"].ToString();
                            data.SponsorLoginId = dataSet.Tables[1].Rows[i]["SponsorLoginId"].ToString();
                            data.ImageURL = dataSet.Tables[1].Rows[i]["ImageURL"].ToString();
                            data.PerformanceLevel = dataSet.Tables[1].Rows[i]["PerformanceLevel"].ToString();
                            data.TeamPV = dataSet.Tables[1].Rows[i]["TeamPV"].ToString();
                            data.SelfPV = dataSet.Tables[1].Rows[i]["SelfPV"].ToString();
                            data.TotalPv = dataSet.Tables[1].Rows[i]["TotalPv"].ToString();
                            data.CurrentSelfPV = dataSet.Tables[1].Rows[i]["CurrentSelfPV"].ToString();
                            data.CurrentTeamPV = dataSet.Tables[1].Rows[i]["CurrentTeamPV"].ToString();
                            data.CurrentTotalPV = dataSet.Tables[1].Rows[i]["CurrentTotalPV"].ToString();
                            data.UIDPV = dataSet.Tables[1].Rows[i]["UIDPV"].ToString();
                            data.CurrentUIDPV = dataSet.Tables[1].Rows[i]["CurrentUIDPV"].ToString();

                            listResponses1.Add(data);

                        }
                        List<Data> listResponses2 = new List<Data>();
                        for (int i = 0; i <= dataSet.Tables[2].Rows.Count - 1; i++)
                        {
                            Data data = new Data();

                            data.LoginId = dataSet.Tables[2].Rows[i]["LoginId"].ToString();
                            data.FirstName = dataSet.Tables[2].Rows[i]["FirstName"].ToString();
                            data.LastName = dataSet.Tables[2].Rows[i]["LastName"].ToString();
                            data.Status = dataSet.Tables[2].Rows[i]["Status"].ToString();
                            data.JoiningDate = dataSet.Tables[2].Rows[i]["JoiningDate"].ToString();
                            data.PermanentDate = dataSet.Tables[2].Rows[i]["PermanentDate"].ToString();
                            data.FK_MemId = dataSet.Tables[2].Rows[i]["FK_MemId"].ToString();
                            data.SponsorName = dataSet.Tables[2].Rows[i]["SponsorName"].ToString();
                            data.SponsorLoginId = dataSet.Tables[2].Rows[i]["SponsorLoginId"].ToString();
                            data.ImageURL = dataSet.Tables[2].Rows[i]["ImageURL"].ToString();
                            data.FK_MemId = dataSet.Tables[2].Rows[i]["Fk_SponsorId"].ToString();
                            data.PerformanceLevel = dataSet.Tables[2].Rows[i]["PerformanceLevel"].ToString();

                            data.TeamPV = dataSet.Tables[2].Rows[i]["TeamPV"].ToString();
                            data.SelfPV = dataSet.Tables[2].Rows[i]["SelfPV"].ToString();
                            data.TotalPv = dataSet.Tables[2].Rows[i]["TotalPv"].ToString();
                            data.CurrentSelfPV = dataSet.Tables[2].Rows[i]["CurrentSelfPV"].ToString();
                            data.CurrentTeamPV = dataSet.Tables[2].Rows[i]["CurrentTeamPV"].ToString();
                            data.CurrentTotalPV = dataSet.Tables[2].Rows[i]["CurrentTotalPV"].ToString();
                            data.UIDPV = dataSet.Tables[2].Rows[i]["UIDPV"].ToString();
                            data.CurrentUIDPV = dataSet.Tables[2].Rows[i]["CurrentUIDPV"].ToString();


                            listResponses2.Add(data);

                        }
                        direct.DirectList = listResponses;
                        direct.SelfData = listResponses1;
                        direct.SecoundLevelData = listResponses2;

                        objres.Message = "";
                        objres.Status = 1;
                        objres.Response = direct;

                    }

                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;

                    }
                    direct.PrevoiusLoginId = dataSet.Tables[3].Rows[0]["LoginId"].ToString();
                    direct.PrevoiusFK_MemId = dataSet.Tables[3].Rows[0]["FK_MemId"].ToString();
                    objres.Response = direct;
                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(DirectResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("PromoTracker")]
        [Produces("application/json")]
        public ResponseModel PromoTracker(RequestModel requestModel)
        {
            string EncryptedText = "";
            PromoTracterResponse objres = new PromoTracterResponse();

            ResponseModel returnResponse = new ResponseModel();
            try
            {
                List<Promotracter> lst = new List<Promotracter>();
                PromotackerLit promotackerLit = new PromotackerLit();
                Promotracter promoRequest = new Promotracter();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                promoRequest = JsonConvert.DeserializeObject<Promotracter>(dcdata);
                DataSet dataSet = promoRequest.GetPromotracker();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["flag"].ToString() == "1")
                        {
                            List<Promotracter> lstpromo = new List<Promotracter>();
                            for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                            {
                                Promotracter listData = new Promotracter();

                                listData.TargetPVFrom = dataSet.Tables[0].Rows[j]["TargetPVFrom"].ToString();
                                listData.TargetPVTo = dataSet.Tables[0].Rows[j]["TargetPVTo"].ToString();
                                listData.DirectLeg = dataSet.Tables[0].Rows[j]["DirectLeg"].ToString();
                                listData.ImageUrl = dataSet.Tables[0].Rows[j]["ImageUrl"].ToString();
                                listData.PerformanceLevel = dataSet.Tables[0].Rows[j]["PerformanceLevel"].ToString();
                                listData.AchiveStatus = dataSet.Tables[0].Rows[j]["Status"].ToString();
                                listData.PK_LevelId = dataSet.Tables[0].Rows[j]["PK_LevelId"].ToString();
                                lstpromo.Add(listData);
                            }

                            promotackerLit.list = lstpromo;
                            objres.Response = promotackerLit;
                            objres.Status = "1";
                        }
                        else
                        {
                            objres.message = Messages.InvalidLoginId;
                            objres.Status = "0";
                        }
                    }
                    else
                    {
                        objres.message = Messages.InvalidLoginId;
                        objres.Status = "0";
                    }
                }
                else
                {
                    objres.message = Messages.InvalidLoginId;
                    objres.Status = "0";
                }
            }
            catch (Exception ex)
            {
                objres.message = Messages.ProblemInConnection;
                objres.Status = "0";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(PromoTracterResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("PendingPromoTracker")]
        [Produces("application/json")]
        public ResponseModel PendingPromoTracker(RequestModel requestModel)
        {
            string EncryptedText = "";
            PromoTracterResponse objres = new PromoTracterResponse();

            ResponseModel returnResponse = new ResponseModel();
            try
            {
                List<Promotracter> lst = new List<Promotracter>();
                PromotackerLit promotackerLit = new PromotackerLit();
                Promotracter promoRequest = new Promotracter();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                promoRequest = JsonConvert.DeserializeObject<Promotracter>(dcdata);
                DataSet dataSet = promoRequest.GetPendingPromotracker();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["flag"].ToString() == "1")
                        {
                            List<Promotracter> lstpromo = new List<Promotracter>();
                            for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                            {
                                Promotracter listData = new Promotracter();

                                listData.TotalAchivedCount = dataSet.Tables[0].Rows[j]["TotalAchivedCount"].ToString();
                                listData.AchivedBusiness = dataSet.Tables[0].Rows[j]["AchivedBusiness"].ToString();
                                listData.TargetPVTo = dataSet.Tables[0].Rows[j]["TargetPVTo"].ToString();
                                listData.DirectLeg = dataSet.Tables[0].Rows[j]["DirectLeg"].ToString();
                                lstpromo.Add(listData);
                            }

                            promotackerLit.list = lstpromo;
                            objres.Response = promotackerLit;
                            objres.Status = "1";
                        }
                        else
                        {
                            objres.message = Messages.InvalidLoginId;
                            objres.Status = "0";
                        }
                    }
                    else
                    {
                        objres.message = Messages.InvalidLoginId;
                        objres.Status = "0";
                    }
                }
                else
                {
                    objres.message = Messages.InvalidLoginId;
                    objres.Status = "0";
                }
            }
            catch (Exception ex)
            {
                objres.message = Messages.ProblemInConnection;
                objres.Status = "0";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(PromoTracterResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("AssociateUnpaidPayout")]
        [Produces("application/json")]
        public ResponseModel AssociateUnpaidPayout(RequestModel requestModel)
        {
            string EncryptedText = "";
            UnpaidPayoutResponse objres = new UnpaidPayoutResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                UnpaidPayoutRequest unpaidpayoutRequest = new UnpaidPayoutRequest();
                UnpaidPayout unpaidpayout = new UnpaidPayout();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                unpaidpayoutRequest = JsonConvert.DeserializeObject<UnpaidPayoutRequest>(dcdata);
                unpaidpayoutRequest.Size = SessionManager.Size;
                DataSet dataSet = unpaidpayoutRequest.GetUnpaidPayout();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<UnpaidPayoutData> unpaidlist = new List<UnpaidPayoutData>();
                        for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                        {
                            UnpaidPayoutData unpaidpayoutData = new UnpaidPayoutData();

                            unpaidpayoutData.FromLoginId = dataSet.Tables[0].Rows[j]["FromLoginId"].ToString();
                            unpaidpayoutData.FromName = dataSet.Tables[0].Rows[j]["FromName"].ToString();
                            unpaidpayoutData.ToLoginId = dataSet.Tables[0].Rows[j]["ToLoginId"].ToString();
                            unpaidpayoutData.ToName = dataSet.Tables[0].Rows[j]["ToName"].ToString();
                            unpaidpayoutData.BusinessAmount = decimal.Parse(dataSet.Tables[0].Rows[j]["BusinessAmount"].ToString());
                            unpaidpayoutData.BV = decimal.Parse(dataSet.Tables[0].Rows[j]["BV"].ToString());
                            unpaidpayoutData.CommissionPercentage = decimal.Parse(dataSet.Tables[0].Rows[j]["CommissionPercentage"].ToString());
                            unpaidpayoutData.Amount = decimal.Parse(dataSet.Tables[0].Rows[j]["Amount"].ToString());
                            unpaidpayoutData.IncomeType = dataSet.Tables[0].Rows[j]["IncomeType"].ToString();
                            unpaidpayoutData.LevelInfo = dataSet.Tables[0].Rows[j]["LevelInfo"].ToString();
                            unpaidpayoutData.TotalRecords = int.Parse(dataSet.Tables[0].Rows[j]["TotalRecords"].ToString());

                            unpaidlist.Add(unpaidpayoutData);
                        }
                        unpaidpayout.unpaidpayoutList = unpaidlist;
                        objres.Response = unpaidpayout;
                        objres.Message = "";
                        objres.Status = 1;
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.NoRecordFound;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(UnpaidPayoutResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("AssociateRepurchasePayotReport")]
        [Produces("application/json")]
        public ResponseModel AssociateRepurchasePayotReport(RequestModel requestModel)
        {
            string EncryptedText = "";
            AssociateReportResponse objres = new AssociateReportResponse();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                Associate associate = new Associate();
                AssociateReport associateReport = new AssociateReport();

                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                associateReport = JsonConvert.DeserializeObject<AssociateReport>(dcdata);

                associateReport.Size = SessionManager.Size;
                DataSet dataSet = associateReport.GetRepAssociatePayout();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            List<AssociateData> listResponses = new List<AssociateData>();
                            for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                            {
                                AssociateData associateData = new AssociateData();
                                associateData.ClosingDate = dataSet.Tables[0].Rows[i]["ClosingDate"].ToString();
                                associateData.PayoutNo = dataSet.Tables[0].Rows[i]["PayoutNo"].ToString();
                                associateData.DirectIncome = dataSet.Tables[0].Rows[i]["DirectIncome"].ToString();
                                associateData.TeamIncome = dataSet.Tables[0].Rows[i]["TeamIncome"].ToString();
                                associateData.SuperStartSale = dataSet.Tables[0].Rows[i]["SuperStartSale"].ToString();
                                associateData.SuperStartClub = dataSet.Tables[0].Rows[i]["SuperStartClub"].ToString();
                                associateData.PremiumClub = dataSet.Tables[0].Rows[i]["PremiumClub"].ToString();
                                associateData.SuperPremiumClub = dataSet.Tables[0].Rows[i]["SuperPremiumClub"].ToString();
                                associateData.Smart = dataSet.Tables[0].Rows[i]["Smart"].ToString();
                                associateData.Leadership = dataSet.Tables[0].Rows[i]["Leadership"].ToString();
                                associateData.GrossAmount = dataSet.Tables[0].Rows[i]["GrossAmount"].ToString();
                                associateData.TDSAmount = dataSet.Tables[0].Rows[i]["TDSAmount"].ToString();
                                associateData.ProcessingFee = dataSet.Tables[0].Rows[i]["ProcessingFee"].ToString();
                                associateData.NetAmount = dataSet.Tables[0].Rows[i]["NetAmount"].ToString();
                                associateData.TotalRecords = int.Parse(dataSet.Tables[0].Rows[i]["TotalRecords"].ToString());

                                listResponses.Add(associateData);
                            }
                            associate.AssociateList = listResponses;
                            objres.Response = associate;
                            objres.Message = "";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.NoRecordFound;
                            objres.Status = 0;

                        }


                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = 0;

                    }

                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;

                }

            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AssociateReportResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("GetPayoutDetails")]
        [Produces("application/json")]
        public ResponseModel GetPayoutDetails(RequestModel requestModel)
        {
            string EncryptedText = "";
            PayoutDetailResponse objres = new PayoutDetailResponse();

            ResponseModel returnResponse = new ResponseModel();
            try
            {
                List<PayoutDetail> lst = new List<PayoutDetail>();
                PayoutDetailLit payoutDetailsLit = new PayoutDetailLit();
                PayoutDetail payoutRequest = new PayoutDetail();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                payoutRequest = JsonConvert.DeserializeObject<PayoutDetail>(dcdata);
                // string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey);
                //payoutRequest = JsonConvert.DeserializeObject<PayoutDetail>(dcdata);
                DataSet dataSet = payoutRequest.GetPayoutMaster();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["flag"].ToString() == "1")
                        {
                            List<PayoutDetail> lstpayout = new List<PayoutDetail>();
                            for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                            {
                                PayoutDetail listData = new PayoutDetail();

                                listData.PK_PayoutMaster = dataSet.Tables[0].Rows[j]["PK_PayoutMaster"].ToString();
                                listData.PayoutNo = dataSet.Tables[0].Rows[j]["PayoutNo"].ToString();
                                listData.ClosingDate = dataSet.Tables[0].Rows[j]["ClosingDate"].ToString();
                                //listData.TotalOfferpoint = dataSet.Tables[0].Rows[j]["TotalOfferpoint"].ToString();
                                lstpayout.Add(listData);
                            }

                            payoutDetailsLit.list = lstpayout;
                            objres.Response = payoutDetailsLit;
                            objres.Status = "1";
                        }
                        else
                        {
                            objres.message = Messages.InvalidLoginId;
                            objres.Status = "0";
                        }
                    }
                    else
                    {
                        objres.message = Messages.InvalidLoginId;
                        objres.Status = "0";
                    }
                }
                else
                {
                    objres.message = Messages.InvalidLoginId;
                    objres.Status = "0";
                }
            }
            catch (Exception ex)
            {
                objres.message = Messages.ProblemInConnection;
                objres.Status = "0";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(PayoutDetailResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("GetPayoutDetailsStatement")]
        [Produces("application/json")]
        public ResponseModel GetPayoutDetailsStatement(RequestModel requestModel)
        {
            string EncryptedText = "";
            PayoutDetailResponse objres = new PayoutDetailResponse();

            ResponseModel returnResponse = new ResponseModel();
            try
            {
                List<PayoutDetail> lst = new List<PayoutDetail>();
                PayoutDetailLit payoutDetailsLit = new PayoutDetailLit();
                PayoutDetail payoutRequest = new PayoutDetail();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                payoutRequest = JsonConvert.DeserializeObject<PayoutDetail>(dcdata);
                DataSet dataSet = payoutRequest.GetPayoutClosingDetails();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["flag"].ToString() == "1")
                        {
                            List<PayoutDetail> lstpayout = new List<PayoutDetail>();
                            for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                            {
                                PayoutDetail listData = new PayoutDetail();

                                listData.Fk_MemId = dataSet.Tables[0].Rows[j]["FK_MemId"].ToString();
                                listData.Name = dataSet.Tables[0].Rows[j]["Name"].ToString();
                                listData.PayoutNo = dataSet.Tables[0].Rows[j]["PayoutNo"].ToString();
                                listData.ClosingDate = dataSet.Tables[0].Rows[j]["ClosingDate"].ToString();
                                listData.LoginId = dataSet.Tables[0].Rows[j]["LoginId"].ToString();
                                listData.DirectIncome = dataSet.Tables[0].Rows[j]["DirectIncome"].ToString();
                                listData.TeamIncome = dataSet.Tables[0].Rows[j]["TeamIncome"].ToString();
                                listData.Smart = dataSet.Tables[0].Rows[j]["Smart"].ToString();
                                listData.Leadership = dataSet.Tables[0].Rows[j]["Leadership"].ToString();
                                listData.SuperStartSale = dataSet.Tables[0].Rows[j]["SuperStartSale"].ToString();
                                listData.SuperStartClub = dataSet.Tables[0].Rows[j]["SuperStartClub"].ToString();
                                listData.PremiumClub = dataSet.Tables[0].Rows[j]["PremiumClub"].ToString();
                                listData.SuperPremiumClub = dataSet.Tables[0].Rows[j]["SuperPremiumClub"].ToString();
                                listData.SeminarBonus = dataSet.Tables[0].Rows[j]["SeminarBonus"].ToString();
                                listData.CarClubBonus = dataSet.Tables[0].Rows[j]["CarClubBonus"].ToString();
                                listData.TravelClubBonus = dataSet.Tables[0].Rows[j]["TravelClubBonus"].ToString();
                                listData.ProvidentClubBonus = dataSet.Tables[0].Rows[j]["ProvidentClubBonus"].ToString();
                                listData.HouseClubBonus = dataSet.Tables[0].Rows[j]["HouseClubBonus"].ToString();
                                listData.BlueDiamondClubBonus = dataSet.Tables[0].Rows[j]["BlueDiamondClubBonus"].ToString();
                                listData.CrownAmbassadorClubBonus = dataSet.Tables[0].Rows[j]["CrownAmbassadorClubBonus"].ToString();
                                listData.PresidentClubBonus = dataSet.Tables[0].Rows[j]["PresidentClubBonus"].ToString();
                                listData.GrossAmount = dataSet.Tables[0].Rows[j]["GrossAmount"].ToString();
                                listData.TDSAmount = dataSet.Tables[0].Rows[j]["TDSAmount"].ToString();
                                listData.ProcessingFee = dataSet.Tables[0].Rows[j]["ProcessingFee"].ToString();
                                listData.NetAmount = dataSet.Tables[0].Rows[j]["NetAmount"].ToString();
                                listData.TotalRecords = dataSet.Tables[0].Rows[j]["TotalRecords"].ToString();
                                listData.PerformanceLevel = dataSet.Tables[0].Rows[j]["PerformanceLevel"].ToString();
                                listData.LevelPercentage = dataSet.Tables[0].Rows[j]["LevelPercentage"].ToString();
                                listData.SelfPV = dataSet.Tables[0].Rows[j]["SelfPV"].ToString();
                                listData.TeamPV = dataSet.Tables[0].Rows[j]["TeamPV"].ToString();
                                listData.TotalOfferpoint = dataSet.Tables[0].Rows[j]["TotalOfferpoint"].ToString();
                                lstpayout.Add(listData);
                            }

                            payoutDetailsLit.list = lstpayout;
                            objres.Response = payoutDetailsLit;
                            objres.Status = "1";
                        }
                        else
                        {
                            objres.message = Messages.NoRecordFound;
                            objres.Status = "0";
                        }
                    }
                    else
                    {
                        objres.message = Messages.NoRecordFound;
                        objres.Status = "0";
                    }
                }
                else
                {
                    objres.message = Messages.NoRecordFound;
                    objres.Status = "0";
                }
            }
            catch (Exception ex)
            {
                objres.message = Messages.ProblemInConnection;
                objres.Status = "0";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(PayoutDetailResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("GetPayoutDetailSummary")]
        [Produces("application/json")]
        public ResponseModel GetPayoutDetailSummary(RequestModel requestModel)
        {
            string EncryptedText = "";
            PayoutDetailResponse objres = new PayoutDetailResponse();

            ResponseModel returnResponse = new ResponseModel();
            try
            {
                List<PayoutDetail> lst = new List<PayoutDetail>();
                PayoutDetailLit payoutDetailsLit = new PayoutDetailLit();
                PayoutDetail payoutRequest = new PayoutDetail();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                payoutRequest = JsonConvert.DeserializeObject<PayoutDetail>(dcdata);
                DataSet dataSet = payoutRequest.GetPayoutSummaryDetails();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["flag"].ToString() == "1")
                        {
                            List<PayoutDetail> lstpayout = new List<PayoutDetail>();
                            for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                            {
                                PayoutDetail listData = new PayoutDetail();

                                
                                listData.Name = dataSet.Tables[0].Rows[j]["FromName"].ToString();
                                listData.LoginId = dataSet.Tables[0].Rows[j]["FromLoginId"].ToString();
                                listData.BusinessAmount = decimal.Parse(dataSet.Tables[0].Rows[j]["BusinessAmount"].ToString());
                                
                                listData.CommissionPercentage = decimal.Parse(dataSet.Tables[0].Rows[j]["CommissionPercentage"].ToString());
                                listData.Amount = decimal.Parse(dataSet.Tables[0].Rows[j]["Amount"].ToString());
                                listData.IncomeType = dataSet.Tables[0].Rows[j]["IncomeType"].ToString();
                                
                                
                                lstpayout.Add(listData);
                            }

                            payoutDetailsLit.list = lstpayout;
                            objres.Response = payoutDetailsLit;
                            objres.Status = "1";
                        }
                        else
                        {
                            objres.message = Messages.NoRecordFound;
                            objres.Status = "0";
                        }
                    }
                    else
                    {
                        objres.message = Messages.NoRecordFound;
                        objres.Status = "0";
                    }
                }
                else
                {
                    objres.message = Messages.NoRecordFound;
                    objres.Status = "0";
                }
            }
            catch (Exception ex)
            {
                objres.message = Messages.ProblemInConnection;
                objres.Status = "0";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(PayoutDetailResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("UpdateBIDStatus")]
        [Produces("application/json")]
        public ResponseModel UpdateBIDStatus(RequestModel requestModel)
        {
            string EncryptedText = "";
            BIDReposne objres = new BIDReposne();

            ResponseModel returnResponse = new ResponseModel();
            try
            {
                List<Profile> lst = new List<Profile>();
                ProfileListres bidList = new ProfileListres();
                Profile profileRequest = new Profile();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                profileRequest = JsonConvert.DeserializeObject<Profile>(dcdata);
                DataSet dataSet = profileRequest.UpdateBID();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["flag"].ToString() == "1")
                        {
                            List<Profile> lstBId = new List<Profile>();
                            for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                            {
                                Profile listData = new Profile();


                                listData.Flag = dataSet.Tables[0].Rows[j]["Flag"].ToString();
                                listData.Status = dataSet.Tables[0].Rows[j]["msg"].ToString();

                                lstBId.Add(listData);
                            }

                            bidList.profilelist = lstBId;
                            objres.Response = bidList;
                            objres.Status = "1";
                        }
                        else
                        {
                            objres.Message = Messages.InvalidLoginId;
                            objres.Status = "0";
                        }
                    }
                    else
                    {
                        objres.Message = Messages.InvalidLoginId;
                        objres.Status = "0";
                    }
                }
                else
                {
                    objres.Message = Messages.InvalidLoginId;
                    objres.Status = "0";
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = "0";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BIDReposne));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("Criteria")]
        [Produces("application/json")]
        public ResponseModel Criteria(ResponseModel responseModel)
        {
            string EncryptedText = "";
            Criterias criteria = new Criterias();
            CriteriaDataResp objres = new CriteriaDataResp();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                CriteriasList catriaListobj = new CriteriasList();
                //List<CriteriasList> lst1 = new List<CriteriasList>();

                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, responseModel.Body);
                criteria = JsonConvert.DeserializeObject<Criterias>(dcdata);
                DataSet dataSet = criteria.getCriteriaDetails();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        List<Criterias> listResponse = new List<Criterias>();
                        for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                        {
                            Criterias listData = new Criterias();
                            listData.Name = dataSet.Tables[0].Rows[j]["Name"].ToString();
                            listData.Description = dataSet.Tables[0].Rows[j]["Description"].ToString();
                            listData.ToTarget = dataSet.Tables[0].Rows[j]["ToTarget"].ToString();
                            listData.FromTarget = dataSet.Tables[0].Rows[j]["FromTarget"].ToString();
                            listData.UnderLegCount = dataSet.Tables[0].Rows[j]["UnderLegCount"].ToString();
                            listData.UnderLegId = dataSet.Tables[0].Rows[j]["UnderLegId"].ToString();
                            listData.Status = dataSet.Tables[0].Rows[j]["Status"].ToString();
                            listData.Achived = dataSet.Tables[0].Rows[j]["Achived"].ToString();
                            listData.Balance = dataSet.Tables[0].Rows[j]["Balance"].ToString();
                            listResponse.Add(listData);
                        }

                        catriaListobj.Criteriaslist = listResponse;
                        objres.responselist = catriaListobj;
                        objres.Message = "";
                        objres.Status = "1";
                    }
                    if (dataSet != null)
                    {
                        List<Criterias> listResponses1 = new List<Criterias>();
                        for (int j = 0; j <= dataSet.Tables[1].Rows.Count - 1; j++)
                        {
                            Criterias listData = new Criterias();
                            listData.SelfBusiness = dataSet.Tables[1].Rows[j]["SelfBusiness"].ToString();
                            listData.UIDBusiness = dataSet.Tables[1].Rows[j]["UIDBusiness"].ToString();
                            listData.Total = dataSet.Tables[1].Rows[j]["Total"].ToString();
                            listResponses1.Add(listData);
                        }
                        catriaListobj.Criteriaslist1 = listResponses1;
                        objres.responselist = catriaListobj;
                        objres.Message = "";
                        objres.Status = "1";
                    }
                    else
                    {
                        objres.Message = Messages.NoRecordFound;
                        objres.Status = "0";

                    }

                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = "0";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CriteriaDataResp));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms)
        ;
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("GetBusiness")]
        [Produces("application/json")]
        public ResponseModel GetBusiness(ResponseModel responseModel)
        {
            string EncryptedText = "";
            AssociateReport associateReport = new AssociateReport();
            AssociateReportResp objresponse = new AssociateReportResp();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                AssociateReportlist objlist = new AssociateReportlist();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, responseModel.Body);
                associateReport = JsonConvert.DeserializeObject<AssociateReport>(dcdata);
                DataSet dataset = associateReport.GetBusiness();
                if (dataset != null)
                {
                    if (dataset.Tables[0].Rows.Count > 0)
                    {
                        List<AssociateReport> listResponse = new List<AssociateReport>();
                        for (int j = 0; j <= dataset.Tables[0].Rows.Count - 1; j++)
                        {
                            AssociateReport list = new AssociateReport();
                            list.OrderNo = dataset.Tables[0].Rows[j]["OrderNo"].ToString();
                            list.OrderDate = dataset.Tables[0].Rows[j]["OrderDate"].ToString();
                            list.OrderAmount = dataset.Tables[0].Rows[j]["OrderAmount"].ToString();
                            list.TotalPV = dataset.Tables[0].Rows[j]["TotalPV"].ToString();
                            list.LoginId = dataset.Tables[0].Rows[j]["LoginId"].ToString();
                            list.Name = dataset.Tables[0].Rows[j]["Name"].ToString();
                            list.Fk_MemId = dataset.Tables[0].Rows[j]["Fk_MemId"].ToString();
                            listResponse.Add(list);
                        }

                        objlist.associatelist = listResponse;
                        objresponse.Response = objlist;
                        objresponse.Status = "1";

                    }
                    else
                    {

                        objresponse.Message = Messages.NoRecordFound;
                        objresponse.Status = "0";
                        
                    }
                    
                    objresponse.PreviousId = dataset.Tables[1].Rows[0]["PreviousId"].ToString();
                    objresponse.SelfBusiness = dataset.Tables[1].Rows[0]["SelfBusiness"].ToString();
                }
            }
            catch (Exception ex)
            {
                objresponse.Message = Messages.ProblemInConnection;
                objresponse.Status = "0";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AssociateReportResp));
            ms = new MemoryStream();
            js.WriteObject(ms, objresponse);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms)
        ;
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("AdminRenderMenu")]
        [Produces("application/json")]
        public ResponseModel AdminRenderMenu(ResponseModel responseModel)
        {
            string EncryptedText = "";
            Menu menu = new Menu();
            MenuDataResp obj = new MenuDataResp();
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                MenuList objlistresponse = new MenuList();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, responseModel.Body);
                menu = JsonConvert.DeserializeObject<AssociateReport>(dcdata);
                DataSet dataset = menu.GetMenuDetails();
                if (dataset != null)
                {
                    if (dataset.Tables[0].Rows.Count > 0)
                    {
                        List<Menu> listresponse = new List<Menu>();
                        for (int i = 0; i <= dataset.Tables[0].Rows.Count - 1; i++)
                        {
                            Menu ListData = new Menu();
                            ListData.MenuName = dataset.Tables[0].Rows[i]["MenuName"].ToString();
                            ListData.MenuId = dataset.Tables[0].Rows[i]["MenuId"].ToString();
                            ListData.Url = dataset.Tables[0].Rows[i]["MenuURL"].ToString();
                            ListData.Class = dataset.Tables[0].Rows[i]["Class"].ToString();
                            ListData.IsDropdown = dataset.Tables[0].Rows[i]["Isdropdown"].ToString();
                            ListData.Icon = dataset.Tables[0].Rows[i]["MenuIcon"].ToString();
                            ListData.MobileIcon = dataset.Tables[0].Rows[i]["MobileIcon"].ToString();
                            listresponse.Add(ListData);
                        }

                        objlistresponse.menuList = listresponse;
                        obj.responselist = objlistresponse;
                        obj.Message = "";
                        obj.Status = "1";

                    }

                    if (dataset != null)
                    {
                          List<Menu> listresponse1 = new List<Menu>();
                        for (int j = 0; j <= dataset.Tables[1].Rows.Count - 1; j++)
                        {
                            Menu SubList = new Menu();
                            SubList.SubMenuName = dataset.Tables[1].Rows[j]["SubMenuName"].ToString();
                            SubList.Fk_MenuId = dataset.Tables[1].Rows[j]["Fk_MenuId"].ToString();
                            SubList.SubMenuId = int.Parse(dataset.Tables[1].Rows[j]["SubMenuId"].ToString());
                            SubList.Url = dataset.Tables[1].Rows[j]["SubMenuUrl"].ToString();
                            SubList.Icon = dataset.Tables[1].Rows[j]["SubMenuIcon"].ToString();
                            listresponse1.Add(SubList);
                        }
                        objlistresponse.subMenuList = listresponse1;
                        obj.responselist = objlistresponse;
                        obj.Message = "";
                        obj.Status = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Message = Messages.ProblemInConnection;
                obj.Status = "0";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(MenuDataResp));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("GetSmartPointDetails")]
        [Produces("application/json")]
        public ResponseModel GetSmartPointDetails(RequestModel requestModel)
        {
            string EncryptedText = "";
            PayoutDetailResponse objres = new PayoutDetailResponse();

            ResponseModel returnResponse = new ResponseModel();
            try
            {
                List<PayoutDetail> lst = new List<PayoutDetail>();
                PayoutDetailLit payoutDetailsLit = new PayoutDetailLit();
                PayoutDetail payoutRequest = new PayoutDetail();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                payoutRequest = JsonConvert.DeserializeObject<PayoutDetail>(dcdata);
                DataSet dataSet = payoutRequest.GetSmartPointDetails();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["flag"].ToString() == "1")
                        {
                            List<PayoutDetail> lstpayout = new List<PayoutDetail>();
                            for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                            {
                                PayoutDetail listData = new PayoutDetail();


                                listData.Name = dataSet.Tables[0].Rows[j]["Name"].ToString();
                                listData.LoginId = dataSet.Tables[0].Rows[j]["LoginId"].ToString();
                                listData.OfferPoint = decimal.Parse(dataSet.Tables[0].Rows[j]["OfferPoint"].ToString());
                                listData.OrderNo = dataSet.Tables[0].Rows[j]["OrderNo"].ToString();

                                listData.AchiveDate = dataSet.Tables[0].Rows[j]["AchiveDate"].ToString();
                                lstpayout.Add(listData);
                            }

                            payoutDetailsLit.list = lstpayout;
                            objres.Response = payoutDetailsLit;
                            objres.Status = "1";
                        }
                        else
                        {
                            objres.message = Messages.InvalidLoginId;
                            objres.Status = "0";
                        }
                    }
                    else
                    {
                        objres.message = Messages.InvalidLoginId;
                        objres.Status = "0";
                    }
                }
                else
                {
                    objres.message = Messages.InvalidLoginId;
                    objres.Status = "0";
                }
            }
            catch (Exception ex)
            {
                objres.message = Messages.ProblemInConnection;
                objres.Status = "0";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(PayoutDetailResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("GetOfferLedger")]
        [Produces("application/json")]
        public ResponseModel GetOfferLedger(RequestModel requestModel)
        {
            string EncryptedText = "";
            PayoutDetailResponse objres = new PayoutDetailResponse();

            ResponseModel returnResponse = new ResponseModel();
            try
            {
                List<PayoutDetail> lst = new List<PayoutDetail>();
                PayoutDetailLit payoutDetailsLit = new PayoutDetailLit();
                PayoutDetail payoutRequest = new PayoutDetail();
                 string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                payoutRequest = JsonConvert.DeserializeObject<PayoutDetail>(dcdata);
                DataSet dataSet = payoutRequest.GetofferPointLedger();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            List<PayoutDetail> lstpayout = new List<PayoutDetail>();
                            for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                            {
                                PayoutDetail listData = new PayoutDetail();


                                listData.LoginId = dataSet.Tables[0].Rows[j]["LoginId"].ToString();
                                listData.Name = dataSet.Tables[0].Rows[j]["ToName"].ToString();
                                listData.CrPoint = dataSet.Tables[0].Rows[j]["CrPoint"].ToString();
                                listData.DrPoint = dataSet.Tables[0].Rows[j]["DrPoint"].ToString();
                                listData.Narration = dataSet.Tables[0].Rows[j]["Narration"].ToString();
                                listData.Transactiondate = dataSet.Tables[0].Rows[j]["Transactiondate"].ToString();
                                listData.TotalRecords = dataSet.Tables[0].Rows[j]["TotalRecords"].ToString();
                                listData.Balance = decimal.Parse(dataSet.Tables[0].Rows[j]["Balance"].ToString());
                                lstpayout.Add(listData);
                            }

                            payoutDetailsLit.list = lstpayout;
                            objres.Response = payoutDetailsLit;
                            objres.Status = "1";
                        }
                        else
                        {
                            objres.message = Messages.InvalidLoginId;
                            objres.Status = "0";
                        }
                    }
                    else
                    {
                        objres.message = Messages.InvalidLoginId;
                        objres.Status = "0";
                    }
                }
                else
                {
                    objres.message = Messages.InvalidLoginId;
                    objres.Status = "0";
                }
            }
            catch (Exception ex)
            {
                objres.message = Messages.ProblemInConnection;
                objres.Status = "0";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(PayoutDetailResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }


        [HttpPost("GetOfferPointDetails")]
        [Produces("application/json")]
        public ResponseModel GetOfferPointDetails(RequestModel requestModel)
        {
            string EncryptedText = "";
            PayoutDetailResponse objres = new PayoutDetailResponse();

            ResponseModel returnResponse = new ResponseModel();
            try
            {
                List<PayoutDetail> lst = new List<PayoutDetail>();
                PayoutDetailLit payoutDetailsLit = new PayoutDetailLit();
                PayoutDetail payoutRequest = new PayoutDetail();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                payoutRequest = JsonConvert.DeserializeObject<PayoutDetail>(dcdata);
                DataSet dataSet = payoutRequest.GetofferPointDetails();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["flag"].ToString() == "1")
                        {
                            List<PayoutDetail> lstpayout = new List<PayoutDetail>();
                            for (int j = 0; j <= dataSet.Tables[0].Rows.Count - 1; j++)
                            {
                                PayoutDetail listData = new PayoutDetail();


                                listData.PayoutNo = dataSet.Tables[0].Rows[j]["PayoutNo"].ToString();
                                listData.TotalOfferpoint = dataSet.Tables[0].Rows[j]["TotalOfferpoint"].ToString();
                                listData.ClosingDate = dataSet.Tables[0].Rows[j]["ClosingDate"].ToString();

                                lstpayout.Add(listData);
                            }

                            payoutDetailsLit.list = lstpayout;
                            objres.Response = payoutDetailsLit;
                            objres.Status = "1";
                        }
                        else
                        {
                            objres.message = Messages.NoRecordFound;
                            objres.Status = "0";
                        }
                    }
                    else
                    {
                        objres.message = Messages.NoRecordFound;
                        objres.Status = "0";
                    }
                }
                else
                {
                    objres.message = Messages.NoRecordFound;
                    objres.Status = "0";
                }
            }
            catch (Exception ex)
            {
                objres.message = Messages.ProblemInConnection;
                objres.Status = "0";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(PayoutDetailResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }

        [HttpPost("CreateRazorPayOrder")]
        [Produces("application/json")]
        public ResponseModel CreateRazorPayOrder(RequestModel requestModel)
        {
            string OrderId = "", Status = "";
            string EncryptedText = "";
            RazCreateOrderResponse objres = new RazCreateOrderResponse();
            Razorpay.Api.Order objorder = null;
            ResponseModel returnResponse = new ResponseModel();
            try
            {
                string Key = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("RazorpayKeyIdLocal").Value;
                string SecretKey = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("RazorpaySecretKeyLocal").Value;
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                RazCreateOrder createOrder = JsonConvert.DeserializeObject<RazCreateOrder>(dcdata);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                RazorpayClient client = null;
                client = new RazorpayClient(Key, SecretKey);
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", createOrder.TotalAmount * 100);
                options.Add("receipt", "");
                options.Add("currency", "INR");
                options.Add("payment_capture", 1);
                objorder = client.Order.Create(options);
                OrderId = objorder["id"].ToString();
                createOrder.OrderId = OrderId;
                DataSet dataSet = createOrder.CreateRazOrder();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["flag"].ToString() == "1")
                        {
                            RazCreateOrderRes razCreateOrderRes = new RazCreateOrderRes();
                            razCreateOrderRes.OrderId = OrderId;
                            objres.Response = razCreateOrderRes;
                            objres.Message = "Order Created Successfully";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = Messages.ProblemInConnection;
                            objres.Status = 0;
                        }
                    }
                    else
                    {
                        objres.Message = Messages.ProblemInConnection;
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = Messages.ProblemInConnection;
                    objres.Status = 0;
                }
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(RazCreateOrderResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
        [HttpPost("GetProductDetailsForOfferPoint")]
        [Produces("application/json")]
        public ResponseModel GetProductDetailsForOfferPoint(RequestModel requestModel)
        {
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            ProductDetailsForOfferPointResponse objres = new ProductDetailsForOfferPointResponse();
            try
            {
                List<ProductForOfferPointData> lstProductforofferpoint = new List<ProductForOfferPointData>();
                ProductDetailForOfferPoint productDetailforofferpoint = new ProductDetailForOfferPoint();
                string dcdata = Common.ApiEncrypt_Decrypt.DecryptString(Aeskey, requestModel.Body);
                productDetailforofferpoint = JsonConvert.DeserializeObject<ProductDetailForOfferPoint>(dcdata);
                DataSet dataSet = productDetailforofferpoint.GetProductDeatilsForOfferPoint();
                if (dataSet != null)
                {

                    ProductForOfferPointData productforofferpointData = new ProductForOfferPointData();
                    if (dataSet.Tables[0] != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            productforofferpointData.ProductId = long.Parse(dataSet.Tables[0].Rows[0]["VarientId"].ToString());                          
                            productforofferpointData.ProductEncryptId = Crypto.Encrypt(dataSet.Tables[0].Rows[0]["VarientId"].ToString());                           
                            productforofferpointData.ProductName = dataSet.Tables[0].Rows[0]["ProductName"].ToString();                                                       
                            productforofferpointData.OfferPoint = float.Parse(dataSet.Tables[0].Rows[0]["OfferPoint"].ToString());
                            objres.Response = productforofferpointData;
                           
                            objres.Status = 1;
                        }                                               
                    }                 
                }              
            }
            catch (Exception ex)
            {
                objres.Message = Messages.ProblemInConnection;
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ProductDetailsForOfferPointResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(Common.Aeskey, CustData);
            returnResponse.Body = EncryptedText;
            return returnResponse;
        }
    }
}
    

   


