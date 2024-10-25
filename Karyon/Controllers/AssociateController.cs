using ClosedXML.Excel;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using Karyon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nancy;
using Newtonsoft.Json;
using paytm;
using System.Data;
using System.Text;
//using paytm;
using static Karyon.Models.Common;
using DataTable = System.Data.DataTable;

namespace Karyon.Controllers
{
    public class AssociateController : AssociateBaseController
    {
        //private readonly IWebHostEnvironment _webHostEnvironment;

        //public AssociateController(IWebHostEnvironment webHostEnvironment)
        //{

        //    _webHostEnvironment = webHostEnvironment;
        //}
        //public IActionResult Tree()
        //{
        //    ViewBag.FK_MemId = HttpContext.Session.GetString("CustomerId").ToString();
        //    //Tree tree = new Tree();
        //    //tree.Fk_UserId = HttpContext.Session.GetString("CustomerId").ToString();
        //    //tree.SessionId = HttpContext.Session.GetString("CustomerId").ToString();
        //    //DataTable dataTable = tree.GetUserFirst();
        //    //DataTable dataTable1 = tree.GetTreeMembers();
        //    //tree.FirstUserDate = dataTable;
        //    //tree.TreeData = dataTable1;
        //    return View();

        //}
        public IActionResult Tree(DirectRequest direct, string LoginId)
        {
            try
            {
                direct.Size = 10000;
                string FK_MemId = "";
                if (!string.IsNullOrEmpty(LoginId))
                {
                    FK_MemId = Crypto.Decrypt(LoginId);
                }
                else
                {
                    FK_MemId = HttpContext.Session.GetString("CustomerId");
                }
                if (HttpContext.Session.GetString("CustomerId") == FK_MemId)
                {
                    ViewBag.displaynone = "display:none";
                }
                else
                {
                    ViewBag.displaynone = "";
                }
                string obj1 = "{\"CustomerId\":\"" + FK_MemId + "\",\"Page\":\"" + direct.Page + "\",\"Size\":\"" + direct.Size + "\",\"LoginId\":\"" + direct.LoginId + "\",\"PlaceUnderId\":\"" + direct.PlaceUnderId + "\",\"Zone\":\"" + direct.Zone + "\"}";
                string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                string result = Common.HITAPI(APIURL.DirectTree, Body);

                DirectResponse directResponse = new DirectResponse();
                ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
                directResponse = JsonConvert.DeserializeObject<DirectResponse>(dcdata);
                if (directResponse.Status == 1)
                {
                    DataTable dataTable = Common.ToDataTable(directResponse.Response.DirectList);
                    direct.dtDetails = dataTable;

                    DataTable dataTable1 = Common.ToDataTable(directResponse.Response.SelfData);
                    direct.dtSelfData = dataTable1;

                    DataTable dataTable2 = Common.ToDataTable(directResponse.Response.SecoundLevelData);
                    direct.dtSecoundLevel = dataTable2;
                }

                if (directResponse.Response != null)
                {
                    TempData["OldLoginId"] = directResponse.Response.PrevoiusFK_MemId;
                }
                else
                {
                    TempData["OldLoginId"] = "";
                }

            }
            catch (Exception)
            {

                direct.Status = "0";
            }
            return View(direct);
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
                //string Body = "";
                //string obj1 = "{\"SponsorLoginId\":\"" + registration.SponsorLoginId + "\",\"Leg\":\"" + registration.Leg + "\",\"FirstName\":\"" + registration.FirstName + "\",\"LastName\":\"" + registration.LastName + "\",\"MobileNo\":\"" + registration.MobileNo + "\",\"PanCard\":\"" + registration.PanCard + "\",\"Email\":\"" + registration.Email + "\",\"Address\":\"\",\"Pincode\":\"" + registration.Pincode + "\",\"State\":\"" + registration.State + "\",\"City\":\"" + registration.City + "\",\"DeviceId\":\"\",\"DeviceToken\":\"\",\"Password\":\"" + registration.Password + "\",\"RegistrationFrom\":\"Web\"}";
                Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                string result = Common.HITAPI(APIURL.Registration, Body);
                LoginResponse loginResponse = new LoginResponse();
                ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
                loginResponse = JsonConvert.DeserializeObject<LoginResponse>(dcdata);
                if (loginResponse.Status == 1)
                {
                    HttpContext.Session.SetString("FirstName", loginResponse.Response.FirstName);
                    HttpContext.Session.SetString("LastName", loginResponse.Response.LastName);
                    HttpContext.Session.SetString("MiddleName", loginResponse.Response.MiddleName);
                    HttpContext.Session.SetString("Password", registration.Password);
                    HttpContext.Session.SetString("LoginId", loginResponse.Response.LoginId);
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
        public ActionResult WelcomeLetter()
        {
            string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetAssociateProfile, Body);
            ProfileReposne profileReposne = new ProfileReposne();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            profileReposne = JsonConvert.DeserializeObject<ProfileReposne>(dcdata);
            return View(profileReposne);

        }
        public ActionResult Congratulations()
        {
            return View();
        }
        public ActionResult DirectList(DirectRequest direct, string LoginId, string Back)
        {
            string SearchLoginId = "";

            if (!string.IsNullOrEmpty(LoginId))
            {
                SearchLoginId = LoginId;

            }
            else
            {
                SearchLoginId = HttpContext.Session.GetString("CustomerId");
            }
            if (HttpContext.Session.GetString("CustomerId") == SearchLoginId)
            {
                ViewBag.dispaynone = "display:none";
            }
            else
            {
                ViewBag.dispaynone = "";
            }

            Common common = new Common();
            DataSet dataSet = new DataSet();


            if (direct.Page == null || direct.Page == 0)
            {
                direct.Page = 1;
            }

            direct.Size = SessionManager.Size;
            string obj1 = "{\"CustomerId\":\"" + SearchLoginId + "\",\"Page\":\"" + direct.Page + "\",\"Size\":\"" + direct.Size + "\",\"LoginId\":\"" + direct.LoginId + "\",\"PlaceUnderId\":\"" + direct.PlaceUnderId + "\",\"Zone\":\"" + direct.Zone + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.DirectList, Body);

            DirectResponse directResponse = new DirectResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            directResponse = JsonConvert.DeserializeObject<DirectResponse>(dcdata);
            TempData["OldLoginId"] = directResponse.PreviousId;
            if (directResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(directResponse.Response.DirectList);
                direct.dtDetails = dataTable;

                int? totalRecords = 0;
                if (directResponse.Response.DirectList.Count > 0)
                {
                    totalRecords = directResponse.Response.DirectList[0].TotalRecords;
                    var pager = new Pager(totalRecords, direct.Page, SessionManager.Size);
                    direct.Pager = pager;
                    // ViewBag.pager = pager;
                }
            }

            return View(direct);

        }




        public ActionResult DownlineList(DownlineRequest downline)
        {
            Common common = new Common();
            DataSet dataSet = new DataSet();
            #region Bind Zone
            List<SelectListItem> ddlZone = new List<SelectListItem>();
            common.OpCode = 3;
            dataSet = common.GetAllMasters();
            ddlZone.Add(new SelectListItem { Text = "All Zone", Value = null });
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dataSet.Tables[0].Rows)
                {
                    ddlZone.Add(new SelectListItem { Text = r["Text"].ToString(), Value = r["Id"].ToString() });
                }
            }
            ViewBag.ddlZone = ddlZone;
            #endregion Bind Zone
            #region Bind Status
            List<SelectListItem> ddlStatus = new List<SelectListItem>();
            ddlStatus.Add(new SelectListItem { Text = "All", Value = null });
            ddlStatus.Add(new SelectListItem { Text = "Active", Value = "P" });
            ddlStatus.Add(new SelectListItem { Text = "In Active", Value = "T" });
            ViewBag.ddlStatus = ddlStatus;
            #endregion Bind Status

            if (downline.Page == null || downline.Page == 0)
            {
                downline.Page = 1;
            }

            downline.Size = SessionManager.Size;
            string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"Page\":\"" + downline.Page + "\",\"Size\":\"" + downline.Size + "\",\"LoginId\":\"" + downline.LoginId + "\",\"PlaceUnderId\":\"" + downline.PlaceUnderId + "\",\"Zone\":\"" + downline.Zone + "\",\"Status\":\"" + downline.Status + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.DownlineList, Body);

            DownlineResponse downlineResponse = new DownlineResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            downlineResponse = JsonConvert.DeserializeObject<DownlineResponse>(dcdata);
            if (downlineResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(downlineResponse.Response.DownlineList);
                downline.dtDetails = dataTable;
                int? totalRecords = 0;
                if (downlineResponse.Response.DownlineList.Count > 0)
                {
                    totalRecords = downlineResponse.Response.DownlineList[0].TotalRecords;
                    var pager = new Pager(totalRecords, downline.Page, SessionManager.Size);
                    downline.Pager = pager;
                    // ViewBag.pager = pager;
                }
            }

            return View(downline);

        }

        public async Task<ActionResult> ViewProfile(ProfileRequest profileRequest, string btnUpdate)
        {
            ViewBag.KYCTabStatus = "";
            ViewBag.Tab3Status = "";
            ViewBag.PersonalDetails = "active";
            ProfileReposne profileReposne = new ProfileReposne();
            if (profileRequest.ImageFile != null)
            {
                string fileName = await FileManagement.WriteFiles(profileRequest.ImageFile, "AssociatePanCard");
                profileRequest.PanImage = fileName;
                profileRequest.CustomerId = int.Parse(HttpContext.Session.GetString("CustomerId"));
                DataSet dataSet = profileRequest.UpdateKYC();
                ViewBag.KYCTabStatus = "active";
                ViewBag.Tab3Status = "active";
                ViewBag.PersonalDetails = "";

            }
            if (profileRequest.AddressProofFront != null)
            {
                string fileName = await FileManagement.WriteFiles(profileRequest.AddressProofFront, "AddressProofFront");
                profileRequest.AddressProofImage = fileName;
                profileRequest.CustomerId = int.Parse(HttpContext.Session.GetString("CustomerId"));
                DataSet dataSet = profileRequest.UpdateKYC();
                ViewBag.KYCTabStatus = "active";
                ViewBag.Tab3Status = "active";
                ViewBag.PersonalDetails = "";
            }
            if (profileRequest.AddressProofBack != null)
            {
                string fileName = await FileManagement.WriteFiles(profileRequest.AddressProofBack, "AddressProofBack");
                profileRequest.AddressProofBackImage = fileName;
                profileRequest.CustomerId = int.Parse(HttpContext.Session.GetString("CustomerId"));
                DataSet dataSet = profileRequest.UpdateKYC();
                ViewBag.KYCTabStatus = "active";
                ViewBag.Tab3Status = "active";
                ViewBag.PersonalDetails = "";
            }
            if (profileRequest.BankDoc != null)
            {
                string fileName = await FileManagement.WriteFiles(profileRequest.BankDoc, "BankDoc");
                profileRequest.BankDocImage = fileName;
                profileRequest.CustomerId = int.Parse(HttpContext.Session.GetString("CustomerId"));
                DataSet dataSet = profileRequest.UpdateKYC();
                ViewBag.KYCTabStatus = "active";
                ViewBag.Tab3Status = "active";
                ViewBag.PersonalDetails = "";
            }
            if (profileRequest.ProfilePic != null)
            {
                string fileName = await FileManagement.WriteFiles(profileRequest.ProfilePic, "BankDoc");
                profileRequest.ProfileImageUrl = fileName;
                profileRequest.CustomerId = int.Parse(HttpContext.Session.GetString("CustomerId"));
                DataSet dataSet = profileRequest.UpdateKYC();
                ViewBag.KYCTabStatus = "active";
                ViewBag.Tab3Status = "active";
                ViewBag.PersonalDetails = "";
            }
            if (!string.IsNullOrEmpty(btnUpdate))
            {


                string obj = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"Gender\":\"" + profileRequest.Gender + "\",\"Email\":\"" + profileRequest.Email + "\",\"Address\":\"" + profileRequest.Address + "\",\"Pincode\":\"" + profileRequest.Pincode + "\",\"State\":\"" + profileRequest.State + "\",\"City\":\"" + profileRequest.City + "\",\"AccNo\":\"" + profileRequest.AccNo + "\",\"PanImage\":\"" + profileRequest.PanImage + "\",\"BankName\":\"" + profileRequest.BankName + "\",\"Branch\":\"" + profileRequest.Branch + "\",\"IFSC\":\"" + profileRequest.IFSC + "\",\"PanCard\":\"" + profileRequest.PanCard + "\",\"AddressProofImage\":\"" + profileRequest.AddressProofImage + "\",\"AddressProofBackImage\":\"" + profileRequest.AddressProofBackImage + "\",\"BankDoc\":\"" + profileRequest.BankDoc + "\",\"HolderName\":\"" + profileRequest.HolderName + "\",\"FirmName\":\"" + profileRequest.FirmName + "\",\"GSTNo\":\"" + profileRequest.GSTNo + "\"}";
                string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj);
                string result2 = Common.HITAPI(APIURL.UpdateAssociateProfile, Body1);
                ShoppingWebResponse deserializedProduct1 = JsonConvert.DeserializeObject<ShoppingWebResponse>(result2);
                string dcdata2 = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct1.body);
                profileReposne = JsonConvert.DeserializeObject<ProfileReposne>(dcdata2);
                if (profileReposne.Status == "1")
                {
                    TempData["RegistraionMember"] = profileReposne.Message;
                }
                else
                {
                    TempData["RegistraionMember"] = profileReposne.Message;
                }
                return RedirectToAction("ViewProfile");
            }
            string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.ViewProfile, Body);
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            profileReposne = JsonConvert.DeserializeObject<ProfileReposne>(dcdata);
            if (profileReposne.Status == "1")
            {
                profileRequest.Address = profileReposne.Response.Address;
                profileRequest.Pincode = profileReposne.Response.Pincode;
                profileRequest.City = profileReposne.Response.City;
                profileRequest.State = profileReposne.Response.State;
                profileRequest.Pincode = profileReposne.Response.Pincode;
                profileRequest.FirstName = profileReposne.Response.FirstName;
                profileRequest.LastName = profileReposne.Response.LastName;
                profileRequest.MiddleName = profileReposne.Response.MiddleName;
                profileRequest.Email = profileReposne.Response.Email;
                profileRequest.Gender = profileReposne.Response.Gender;
                profileRequest.MobileNo = profileReposne.Response.MobileNo;
                profileRequest.AccNo = profileReposne.Response.AccNo;
                profileRequest.Branch = profileReposne.Response.Branch;
                profileRequest.IFSC = profileReposne.Response.IFSC;
                TempData["PanImage"] = "https://www.karyon.organic" + profileReposne.Response.PanImage;
                profileRequest.PanCard = profileReposne.Response.PanCard;
                profileRequest.BankName = profileReposne.Response.BankName;
                profileRequest.HolderName = profileReposne.Response.HolderName;
                profileRequest.RankName = profileReposne.Response.RankName;
                profileRequest.TotalOrders = profileReposne.Response.TotalOrders;
                profileRequest.JoiningDate = profileReposne.Response.JoiningDate;
                profileRequest.Status = profileReposne.Response.Status;
                profileRequest.SponsorId = profileReposne.Response.SponsorId;
                profileRequest.SponsorName = profileReposne.Response.SponsorName;
                profileRequest.PanStatus = profileReposne.Response.PanStatus;
                profileRequest.AddressFrontStatus = profileReposne.Response.AddressFrontStatus;
                profileRequest.AddressBackStatus = profileReposne.Response.AddressBackStatus;
                profileRequest.BankStatus = profileReposne.Response.BankStatus;
                profileRequest.FirmName = profileReposne.Response.FirmName;
                profileRequest.GSTNo = profileReposne.Response.GSTNo;
                TempData["AddressProofFrontURL"] = "https://www.karyon.organic" + profileReposne.Response.AddressProofFrontURL;
                TempData["AddressProofBackURL"] = "https://www.karyon.organic" + profileReposne.Response.AddressProofBackURL;
                TempData["BankDocURL"] = "https://www.karyon.organic" + profileReposne.Response.BankDocURL;
                TempData["ProfilePicURL"] = "https://www.karyon.organic" + profileReposne.Response.ProfilePicURL;
                //TempData["ViewProfile"] = profileReposne.Message;
            }
            return View(profileRequest);
        }

        public ActionResult ChangePassword(ChangePassword changePassword, string Change)
        {
            if (!string.IsNullOrEmpty(Change))
            {
                string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"OldPassword\":\"" + changePassword.OldPassword + "\",\"NewPassword\":\"" + changePassword.NewPassword + "\"}";
                string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                string result = Common.HITAPI(APIURL.ChangePassword, Body);
                ChangePasswordResponse changePasswordResponse = new ChangePasswordResponse();
                ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
                changePasswordResponse = JsonConvert.DeserializeObject<ChangePasswordResponse>(dcdata);
                if (changePasswordResponse.Status == 0)
                {
                    return View(changePassword);
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
                TempData["ChangePassword"] = changePasswordResponse.Message;

            }
            return View();
        }

        public ActionResult Support()
        {
            return View();
        }
        public async Task<ActionResult> KaryonPointsRequest(KaryonPointsRequest karyonPointsRequest, string Request)
        {
            karyonPointsRequest.LoginId = HttpContext.Session.GetString("LoginId").ToString();
            karyonPointsRequest.Name = HttpContext.Session.GetString("FirstName").ToString() + ' ' + HttpContext.Session.GetString("LastName").ToString();

            #region BindPaymentMode
            string obj1 = "{\"OpCode\":\"" + 4 + "\",\"Value\":\"" + "" + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetMasterData, Body);

            MasterDataResponse masterDataResponse = new MasterDataResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);
                List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
                ddlPaymentMode.Add(new SelectListItem { Text = "Payment Mode", Value = "0" });
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Name"].ToString() });
                    }
                }
                ViewBag.ddlPaymentMode = ddlPaymentMode;
            }
            #endregion
            #region BankName
            obj1 = "{\"OpCode\":\"" + 24 + "\",\"Value\":\"" + "" + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            result = Common.HITAPI(APIURL.GetMasterData, Body);

            deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);
                List<SelectListItem> ddlBankName = new List<SelectListItem>();
                ddlBankName.Add(new SelectListItem { Text = "BankName", Value = "0" });
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        ddlBankName.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_Id"].ToString() });
                    }
                }
                ViewBag.ddlBankName = ddlBankName;
            }
            #endregion
            #region Bank
            obj1 = "{\"OpCode\":\"" + 45 + "\",\"Value\":\"" + "" + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            result = Common.HITAPI(APIURL.GetMasterData, Body);

            deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);
                List<SelectListItem> ddlBankName = new List<SelectListItem>();
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        ddlBankName.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_Id"].ToString() });
                    }
                }
                ViewBag.ddlBank = ddlBankName;
            }
            #endregion
            karyonPointsRequest.MerchantTranId = DateTime.Now.ToString("ddMMyyyyhhMMss");

            if (!string.IsNullOrEmpty(Request))
            {
                if (karyonPointsRequest.ImageFile != null)
                {
                    string fileName = await FileManagement.WriteFiles(karyonPointsRequest.ImageFile, "KaryonPointsRequest");
                    karyonPointsRequest.PaymentSlip = fileName;
                }
                if (karyonPointsRequest.PaymentMode == "UPI/NET Banking/Debit Card/Credit Card")
                {
                    karyonPointsRequest.TransactionNo = DateTime.Now.ToString("ddMMyyyyhhMMss");
                }
                if (karyonPointsRequest.TransactionDate != null)
                {
                    karyonPointsRequest.TransactionDate = string.IsNullOrEmpty(karyonPointsRequest.TransactionDate) ? null : Common.ConvertToSystemDate(karyonPointsRequest.TransactionDate, "dd/MM/yyyy");
                }
                karyonPointsRequest.LoginId = HttpContext.Session.GetString("LoginId");
                DataSet dataSet = karyonPointsRequest.RequestKaryonPoints();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            if (karyonPointsRequest.PaymentMode == "UPI/NET Banking/Debit Card/Credit Card")
                            {
                                HttpContext.Session.SetString("TransactionNo", karyonPointsRequest.TransactionNo);
                                HttpContext.Session.SetString("OrderAmount", karyonPointsRequest.Amount.ToString());
                                return RedirectToAction("PaymentIntegration", new { PageName = "EwalletRequest" });
                            }
                            TempData["KaryonPointsRequest"] = Messages.WalletSuccess;
                            return RedirectToAction("KaryonPointsList");
                        }
                        else
                        {
                            TempData["KaryonPointsRequest"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                        }
                    }
                }


            }
            return View(karyonPointsRequest);
        }

        //private async Task<string> UploadImage(string folderPath, IFormFile file, string LoginId)
        //{


        //    folderPath +=  LoginId + Guid.NewGuid().ToString() + "_" + file.FileName;

        //    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

        //    await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

        //    return "/" + folderPath;
        //}

        public ActionResult KaryonPointsList(KaryonPointsRequest karyonPointsRequest)

        {
            HttpContext.Session.SetString("VPA", "");
            string obj1 = "{\"LoginId\":\"" + HttpContext.Session.GetString("LoginId") + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.KaryonPointsList, Body);

            KaryonPointsResponse karyonPointsResponse = new KaryonPointsResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            karyonPointsResponse = JsonConvert.DeserializeObject<KaryonPointsResponse>(dcdata);
            if (karyonPointsResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(karyonPointsResponse.Response.RequestList);
                karyonPointsRequest.dtdetails = dataTable;
            }
            return View(karyonPointsRequest);
        }
        public ActionResult GetKaryonPointsList(KaryonPointsRequest karyonPointsRequest, string merchantTranId)
        {
            string obj1 = "{\"LoginId\":\"" + HttpContext.Session.GetString("LoginId") + "\",\"MerchantTranId\":\"" + merchantTranId + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.KaryonPointsList, Body);

            KaryonPointsResponse karyonPointsResponse = new KaryonPointsResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            karyonPointsResponse = JsonConvert.DeserializeObject<KaryonPointsResponse>(dcdata);

            return Json(karyonPointsResponse);

        }
        public ActionResult PaymentSuccessWallet()
        {
            return View();
        }

        public ActionResult AssociatePayotReport(AssociateReport model, string BtnExport)
        {
            #region PayoutNO
            string obj = "{\"OpCode\":\"" + 21 + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj);
            string result = Common.HITAPI(APIURL.GetMasterData, Body);
            MasterDataResponse masterDataResponse = new MasterDataResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            List<SelectListItem> ddlPayoutNo = new List<SelectListItem>();
            ddlPayoutNo.Add(new SelectListItem { Text = "Payout No.", Value = "0" });
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        ddlPayoutNo.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_Id"].ToString() });
                    }
                }
                ViewBag.ddlPayoutNo = ddlPayoutNo;
            }
            #endregion PayoutNo

            if (model.Page == 0 || model.Page == null)
            {
                model.Page = 1;
            }
            AssociateReportResponse associateReportResponse = new AssociateReportResponse();
            obj = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"PayoutNo\":\"" + model.PayoutNo + "\",\"Page\":\"" + model.Page + "\",\"Size\":\"" + SessionManager.Size + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj);
            result = Common.HITAPI(APIURL.GetAssociatePayotReport, Body);
            deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            associateReportResponse = JsonConvert.DeserializeObject<AssociateReportResponse>(dcdata);
            if (associateReportResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(associateReportResponse.Response.AssociateList);
                model.dtDetails = dataTable;
                int? totalRecords = 0;
                if (dataTable.Rows.Count > 0)
                {
                    totalRecords = int.Parse(model.dtDetails.Rows[0]["TotalRecords"].ToString());
                    var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                    model.Pager = pager;
                }
            }

            if (!string.IsNullOrEmpty(BtnExport))
            {
                try
                {
                    model.ExportToExcel = "1";
                    // model.PayoutNo = associateReportResponse.Pk_PayoutId.ToString();
                    model.CustomerId = Convert.ToInt32(HttpContext.Session.GetString("CustomerId"));
                    DataSet dataSet = model.GetAssociatePayoutReport();
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        model.dtDetails = dataSet.Tables[0];
                        DataTable dt = model.dtDetails;
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dt);
                            using (MemoryStream stream = new MemoryStream())
                            {
                                wb.SaveAs(stream);
                                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PayoutDetails.xlsx");
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return View(model);
        }

        //public ActionResult ExportToExcelPayoutReport(AssociateReportResponse associateReportResponse, AssociateReport model, string ExporttoExcel, string PayoutNo)
        //{
        //    try
        //    {
        //        //AssociateReport model = new AssociateReport();
        //        model.ExportToExcel = "1";
        //        model.PayoutNo = model.PayoutNo;
        //        model.CustomerId = Convert.ToInt32(HttpContext.Session.GetString("CustomerId"));
        //        DataSet dataSet = model.GetAssociatePayoutReport();
        //        model.dtDetails = dataSet.Tables[0];
        //        DataTable dt = model.dtDetails;
        //        using (XLWorkbook wb = new XLWorkbook())
        //        {
        //            wb.Worksheets.Add(dt);
        //            using (MemoryStream stream = new MemoryStream())
        //            {
        //                wb.SaveAs(stream);
        //                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PayoutDetails.xlsx");
        //            }
        //        }
        //        
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }


        //}
        public ActionResult GetPincodeFranchise(string Pincode)
        {

            string Body = "";
            string obj1 = "{\"OpCode\":\"" + 12 + "\",\"Value\":\"" + Pincode + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetPincodeFranchise, Body);
            MasterDataResponse masterDataResponse = new MasterDataResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);


            return Json(masterDataResponse);
        }
        public ActionResult CheckOut(PlaceOrder placeOrder, string Status, string PromoCode, string PromoCodeDiscountPrice)
        {
            if (!string.IsNullOrEmpty(Status))
            {
                TempData["Placeorder"] = "Insufficient Balance";
                placeOrder.LoginId = HttpContext.Session.GetString("LoginId");
                return View(placeOrder);
            }
            string Body = "";
            string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"PaymentMode\":\"Wallet\",\"Pk_AddressId\":\"" + HttpContext.Session.GetString("Pk_AddressId") + "\",\"WalletType\":\"" + HttpContext.Session.GetString("WalletType") + "\",\"PromoCode\":\"" + PromoCode + "\",\"PromoCodeDiscountPrice\":\"" + PromoCodeDiscountPrice + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.PlaceOrder, Body);
            PlaceOrderResponse placeOrderResponse = new PlaceOrderResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            placeOrderResponse = JsonConvert.DeserializeObject<PlaceOrderResponse>(dcdata);
            if (placeOrderResponse.Status == 1)
            {
                HttpContext.Session.SetString("OrderNo", placeOrderResponse.OrderNo);
                return RedirectToAction("PickUpFranchisee");
            }
            else
            {
                TempData["Placeorder"] = placeOrderResponse.Message;
            }
            placeOrder.LoginId = HttpContext.Session.GetString("LoginId");
            return View(placeOrder);
        }
        public ActionResult OrderList(CreateOrder createOrder)
        {

            if (createOrder.Page == null || createOrder.Page == 0)
            {
                createOrder.Page = 1;
            }

            createOrder.Size = SessionManager.Size;
            createOrder.Status = createOrder.Status == "0" ? null : createOrder.Status;
            createOrder.OpCode = 1;
            createOrder.FromDate = string.IsNullOrEmpty(createOrder.FromDate) ? null : Common.ConvertToSystemDate(createOrder.FromDate, "dd/MM/yyyy");
            createOrder.ToDate = string.IsNullOrEmpty(createOrder.ToDate) ? null : Common.ConvertToSystemDate(createOrder.ToDate, "dd/MM/yyyy");
            createOrder.LoginId = HttpContext.Session.GetString("LoginId");
            DataSet dataSet = createOrder.GetCustomerOrders();
            createOrder.dtDetails = dataSet.Tables[0];
            int? totalRecords = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                totalRecords = int.Parse(dataSet.Tables[0].Rows[0]["TotalRecords"].ToString());
                var pager = new Pager(totalRecords, createOrder.Page, SessionManager.Size);
                createOrder.Pager = pager;
                // ViewBag.pager = pager;
            }
            return View(createOrder);
        }

        public ActionResult Ledger(FranchiseWallet franchiseWallet)
        {
            if (franchiseWallet.Page == null || franchiseWallet.Page == 0)
            {
                franchiseWallet.Page = 1;
            }

            franchiseWallet.Size = SessionManager.Size;
            franchiseWallet.LoginId = HttpContext.Session.GetString("LoginId");
            DataSet dataSet = franchiseWallet.GetAssociateLedger();
            franchiseWallet.dtDetails = dataSet.Tables[0];
            int? totalRecords = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                totalRecords = int.Parse(dataSet.Tables[0].Rows[0]["TotalRecords"].ToString());
                var pager = new Pager(totalRecords, franchiseWallet.Page, SessionManager.Size);
                franchiseWallet.Pager = pager;
                // ViewBag.pager = pager;
            }
            return View(franchiseWallet);
        }

        public ActionResult UpdatePickupFranchisee(string FranchiseeId, string OrderNo)
        {

            string Body = "";
            string obj1 = "{\"AddedBy\":\"" + int.Parse(HttpContext.Session.GetString("CustomerId")) + "\",\"OrderNo\":\"" + OrderNo + "\",\"FranchiseId\":\"" + FranchiseeId + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.UpdatePickupFranchisee, Body);
            UpdatePickupFranchiseResponse updatePickupFranchiseResponse = new UpdatePickupFranchiseResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            updatePickupFranchiseResponse = JsonConvert.DeserializeObject<UpdatePickupFranchiseResponse>(dcdata);
            return Json(updatePickupFranchiseResponse);

        }

        public ActionResult CounterCollect(string OrderNo)
        {
            CreateOrder createOrder = new CreateOrder();
            createOrder.OrderNo = OrderNo;
            createOrder.AddedBy = int.Parse(HttpContext.Session.GetString("CustomerId"));
            DataSet dataSet = createOrder.CounterPickup();
            return RedirectToAction("OrderList");
        }
        public ActionResult GetWeeklyAssociatePayoutReport(AssociateReport associateReport, string BtnExport)
        {
            if (associateReport.Page == null || associateReport.Page == 0)
            {
                associateReport.Page = 1;
            }
            string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"Page\":\"" + associateReport.Page + "\",\"Size\":\"" + SessionManager.Size + "\",\"OpCode\":\"" + 1 + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.AssociateWeeklyPayotReport, Body);

            AssociateReportResponse associateReportResponse = new AssociateReportResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            associateReportResponse = JsonConvert.DeserializeObject<AssociateReportResponse>(dcdata);
            if (associateReportResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(associateReportResponse.Response.AssociateList);
                associateReport.dtDetails = dataTable;
                int? totalRecords = 0;
                if (dataTable.Rows.Count > 0)
                {
                    totalRecords = int.Parse(associateReport.dtDetails.Rows[0]["TotalRecords"].ToString());
                    var pager = new Pager(totalRecords, associateReport.Page, SessionManager.Size);
                    associateReport.Pager = pager;
                }
            }
            if (!string.IsNullOrEmpty(BtnExport))
            {
                try
                {
                    AssociateReport model = new AssociateReport();
                    model.ExportToExcel = "1";
                    model.OpCode = 1;
                    //model.PayoutNo = associateReport.PayoutNo.ToString();
                    model.CustomerId = Convert.ToInt32(HttpContext.Session.GetString("CustomerId"));
                    DataSet dataSet = model.GetWeeklyAssociatePayoutReport();
                    if (dataSet.Tables[0] != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            model.dtDetails = dataSet.Tables[0];
                            DataTable dt = model.dtDetails;
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt);
                                using (MemoryStream stream = new MemoryStream())
                                {
                                    wb.SaveAs(stream);
                                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "WeeklyPayoutReport.xlsx");
                                }
                            }
                        }

                    }


                }
                catch (Exception)
                {
                    throw;
                }
            }
            return View(associateReport);


        }
        public ActionResult PrintCustomerOrder(string OrderNo)
        {
            CreateOrder createOrder = new CreateOrder();
            createOrder.OrderNo = OrderNo;
            DataSet dataSet = createOrder.PrintCustomerOrder();
            ViewBag.ContactPerson = dataSet.Tables[0].Rows[0]["ContactPerson"].ToString();
            ViewBag.LoginId = dataSet.Tables[0].Rows[0]["LoginId"].ToString();
            ViewBag.Address = dataSet.Tables[0].Rows[0]["Address"].ToString();
            ViewBag.Mobile = dataSet.Tables[0].Rows[0]["Mobile"].ToString();
            ViewBag.OrderNo = dataSet.Tables[0].Rows[0]["OrderNo"].ToString();
            ViewBag.DispatchCount = dataSet.Tables[0].Rows[0]["DispatchCount"].ToString();
            ViewBag.OrderDate = dataSet.Tables[0].Rows[0]["OrderDate"].ToString();
            ViewBag.InvoiceDate = dataSet.Tables[0].Rows[0]["InvoiceDate"].ToString();
            ViewBag.PinCode = dataSet.Tables[0].Rows[0]["PinCode"].ToString();
            ViewBag.ConsignerPinCode = dataSet.Tables[0].Rows[0]["ConsignerPinCode"].ToString();
            ViewBag.InvoiceNo = dataSet.Tables[0].Rows[0]["InvoiceNo"].ToString();
            ViewBag.AmountInWords = dataSet.Tables[2].Rows[0]["AmountInWords"].ToString();
            ViewBag.GSTNo = dataSet.Tables[0].Rows[0]["GSTNo"].ToString();
            ViewBag.ShippingCharges = decimal.Parse(dataSet.Tables[0].Rows[0]["ShippingCharges"].ToString());
            ViewBag.State = dataSet.Tables[0].Rows[0]["State"].ToString();
            ViewBag.EQRCodeUrl = dataSet.Tables[0].Rows[0]["EQRCodeUrl"].ToString();
            ViewBag.irn = dataSet.Tables[0].Rows[0]["irn"].ToString();
            ViewBag.AckDt = dataSet.Tables[0].Rows[0]["AckDt"].ToString();
            ViewBag.AckNo = dataSet.Tables[0].Rows[0]["AckNo"].ToString();
            createOrder.dtDetails = dataSet.Tables[1];
            if (dataSet.Tables[2].Rows[0]["AmountInWords"].ToString() == null || dataSet.Tables[2].Rows[0]["AmountInWords"].ToString() == "")
            {
                ViewBag.Title = "Gift Invoice";
            }
            else
            {
                ViewBag.Title = "Tax Invoice";
            }

            createOrder.dtOrderDetails = dataSet.Tables[3];
            return View(createOrder);
        }
        public ActionResult BusinessReport(BusinessReport model, string ExporttoExcel)
        {
            if (model.Page == 0 || model.Page == null)
            {
                model.Page = 1;
            }
            string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"Page\":\"" + model.Page + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetBusinessReport, Body);

            BusinessReposne businessReposne = new BusinessReposne();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            businessReposne = JsonConvert.DeserializeObject<BusinessReposne>(dcdata);
            if (businessReposne.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(businessReposne.Response.BusinessList);
                model.dtDetails = dataTable;

            }
            if (!string.IsNullOrEmpty(ExporttoExcel))
            {
                try
                {
                    //model.PayoutNo = associateReport.PayoutNo.ToString();
                    model.CustomerId = Convert.ToInt32(HttpContext.Session.GetString("CustomerId"));
                    model.ExportToExcel = "1";
                    DataSet dataSet = model.GetBusinessReport();
                    if (dataSet.Tables[0] != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            model.dtDetails = dataSet.Tables[0];
                            DataTable dt = model.dtDetails;
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt);
                                using (MemoryStream stream = new MemoryStream())
                                {
                                    wb.SaveAs(stream);
                                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BusinessReport.xlsx");
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }


            return View(model);
        }
        public ActionResult AddAddres(string Name, string MobileNo, string Address, string Locality, string Pincode, string State, string City, string Landmark, string Pk_AddressId)
        {
            AddressResponse addressResponse = new AddressResponse();

            string obj11 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"Name\":\"" + Name + "\",\"MobileNo\":\"" + MobileNo + "\",\"Address\":\"" + Address + "\",\"Locality\":\"" + Locality + "\",\"Pincode\":\"" + Pincode + "\",\"State\":\"" + State + "\",\"City\":\"" + City + "\",\"Landmark\":\"" + Landmark + "\",\"AddressType\":\"Home\",\"Pk_AddressId\":\"" + Pk_AddressId + "\"}";
            string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj11);
            string result1 = Common.HITAPI(APIURL.AddAddress, Body1);
            ShoppingWebResponse deserializedProduct1 = JsonConvert.DeserializeObject<ShoppingWebResponse>(result1);
            string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct1.body);
            addressResponse = JsonConvert.DeserializeObject<AddressResponse>(dcdata1);
            return Json(addressResponse);

        }
        public ActionResult AddAddress(AddressRequest addressRequest, string btnSave, string PlaceOrders, string PaymentGayewat, string Pk_AddressId, string PromoCode, string PromoCodeDiscountPrice)
        {
            AddressResponse addressResponse = new AddressResponse();
            if (!string.IsNullOrEmpty(btnSave))
            {
                string obj11 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"Name\":\"" + addressRequest.Name + "\",\"MobileNo\":\"" + addressRequest.MobileNo + "\",\"Address\":\"" + addressRequest.Address + "\",\"Locality\":\"" + addressRequest.Locality + "\",\"Pincode\":\"" + addressRequest.Pincode + "\",\"State\":\"" + addressRequest.State + "\",\"City\":\"" + addressRequest.City + "\",\"Landmark\":\"" + addressRequest.Landmark + "\",\"AddressType\":\"Home\",\"Pk_AddressId\":\"" + addressRequest.Pk_AddressId + "\"}";
                string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj11);
                string result1 = Common.HITAPI(APIURL.AddAddress, Body1);
                ShoppingWebResponse deserializedProduct1 = JsonConvert.DeserializeObject<ShoppingWebResponse>(result1);
                string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct1.body);
                addressResponse = JsonConvert.DeserializeObject<AddressResponse>(dcdata1);
                return Json(addressResponse);
            }

            if (!string.IsNullOrEmpty(PlaceOrders))
            {
                if (HttpContext.Session.GetString("WalletType") == "Paytm")
                {
                    string DiscountPrice = Crypto.Encrypt(PromoCodeDiscountPrice);
                    HttpContext.Session.SetString("Pk_AddressId", Pk_AddressId);
                    return RedirectToAction("PaymentIntegration", "Associate", new { PromoCode = PromoCode, PromoCodeDiscountPrice = DiscountPrice });
                }
                HttpContext.Session.SetString("Pk_AddressId", Pk_AddressId);
                return RedirectToAction("CheckOut", "Associate", new { PromoCode = PromoCode, PromoCodeDiscountPrice = PromoCodeDiscountPrice });


            }
            string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetAddress, Body);


            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            addressResponse = JsonConvert.DeserializeObject<AddressResponse>(dcdata);
            addressRequest.AddressList = addressResponse.Response.AddressList;
            return View(addressRequest);
        }

        public ActionResult BusinessReportDetails(string EntryDate)
        {
            string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"EntryDate\":\"" + EntryDate + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetBusinessReportDetails, Body);

            BusinessReposne businessReposne = new BusinessReposne();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            businessReposne = JsonConvert.DeserializeObject<BusinessReposne>(dcdata);


            return Json(businessReposne);

        }

        public ActionResult PaymentIntegration(AddressRequest addressRequest, string PageName, string PromoCodeDiscountPrice, string PromoCode)
        {
            string Discount = Crypto.Decrypt(PromoCodeDiscountPrice);
            PlaceOrderResponse placeOrderResponse = new PlaceOrderResponse();
            if (string.IsNullOrEmpty(PageName))
            {
                addressRequest.Pk_AddressId = int.Parse(HttpContext.Session.GetString("Pk_AddressId"));
                string Body = "";
                string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"PaymentMode\":\"Gateway\",\"Pk_AddressId\":\"" + HttpContext.Session.GetString("Pk_AddressId") + "\",\"PromoCodeDiscountPrice\":\"" + Discount + "\",\"PromoCode\":\"" + PromoCode + "\"}";
                Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                string result = Common.HITAPI(APIURL.PlaceOrder, Body);

                ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
                placeOrderResponse = JsonConvert.DeserializeObject<PlaceOrderResponse>(dcdata);
                if (placeOrderResponse.Status == 1)
                {
                    TempData["Placeorder"] = "";
                }
                else
                {
                    TempData["Placeorder"] = placeOrderResponse.Message;
                    return View();
                }
                placeOrderResponse.TransactionType = "CustomerOrder";
            }
            else
            {
                placeOrderResponse.OrderNo = HttpContext.Session.GetString("TransactionNo");
                placeOrderResponse.OrderAmount = HttpContext.Session.GetString("OrderAmount");
                placeOrderResponse.TransactionType = "KaryonWalletRequest";
            }

            addressRequest.CustomerId = int.Parse(HttpContext.Session.GetString("CustomerId"));
            DataSet dataSet = addressRequest.ManageAddress();

            if (!string.IsNullOrEmpty(Discount))
            {
                float PayAmount = float.Parse(placeOrderResponse.OrderAmount) - float.Parse(Discount);

                placeOrderResponse.OrderAmount = Convert.ToString(PayAmount);
            }
            string emailId = dataSet.Tables[0].Rows[0]["Email"].ToString();
            string mobileNumber = dataSet.Tables[0].Rows[0]["MobileNo"].ToString();
            String merchantKey = Gateway.MerchantId;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("MID", Gateway.MID);
            parameters.Add("CHANNEL_ID", Gateway.CHANNEL_ID);
            parameters.Add("INDUSTRY_TYPE_ID", Gateway.INDUSTRY_TYPE_ID);
            parameters.Add("WEBSITE", Gateway.WebSite);
            parameters.Add("EMAIL", emailId);
            parameters.Add("MOBILE_NO", mobileNumber);
            parameters.Add("CUST_ID", HttpContext.Session.GetString("CustomerId"));
            parameters.Add("ORDER_ID", placeOrderResponse.OrderNo);
            parameters.Add("TXN_AMOUNT", placeOrderResponse.OrderAmount);

            parameters.Add("CALLBACK_URL", Gateway.CallBackURL + "?Id=" + placeOrderResponse.TransactionType);

            string checksum = CheckSum.generateCheckSum(merchantKey, parameters);


            string paytmURL = Gateway.PaytmURL + "?orderid=" + parameters.FirstOrDefault(x => x.Key == "ORDER_ID").Value;
            //string paytmURL = "https://securegw.paytm.in/theia/processTransaction?orderid=" + parameters.FirstOrDefault(x => x.Key == "ORDER_ID").Value;

            string outputHTML = "<html>";
            outputHTML += "<head>";
            outputHTML += "<title>Merchant Check Out Page</title>";
            outputHTML += "</head>";
            outputHTML += "<body>";
            outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
            outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
            outputHTML += "<table border='1'>";
            outputHTML += "<tbody>";
            foreach (string key in parameters.Keys)
            {
                outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
            }
            outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            outputHTML += "<script type='text/javascript'>";
            outputHTML += "document.f1.submit();";
            outputHTML += "</script>";
            outputHTML += "</form>";
            outputHTML += "</body>";
            outputHTML += "</html>";
            ViewBag.htmlData = outputHTML;

            return View();
        }
        public ActionResult PickUpFranchisee()
        {
            PlaceOrder placeOrder = new PlaceOrder();
            placeOrder.OrderNo = HttpContext.Session.GetString("OrderNo");
            return View(placeOrder);
        }
        public ActionResult HarmonyReport(AssociateReport model, string BtnExport)
        {
            #region PayoutNo
            string obj = "{\"OpCode\":\"" + 21 + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj);
            string result = Common.HITAPI(APIURL.GetMasterData, Body);
            MasterDataResponse masterDataResponse = new MasterDataResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            List<SelectListItem> ddlPayoutNo = new List<SelectListItem>();
            ddlPayoutNo.Add(new SelectListItem { Text = "Payout No.", Value = "0" });
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        ddlPayoutNo.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_Id"].ToString() });
                    }
                }
                ViewBag.ddlPayoutNo = ddlPayoutNo;
            }
            #endregion PayoutNo
            AssociateReportResponse associateReportResponse = new AssociateReportResponse();
            if (model.Page == 0 || model.Page == null)
            {
                model.Page = 1;
            }
            string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"PayoutNo\":\"" + model.PayoutNo + "\",\"Type\":\"" + "Harmony" + "\",\"Page\":\"" + model.Page + "\",\"Size\":\"" + SessionManager.Size + "\"}";
            string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            result = Common.HITAPI(APIURL.HarmonyPayoutReport, Body1);
            deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            associateReportResponse = JsonConvert.DeserializeObject<AssociateReportResponse>(dcdata1);
            if (associateReportResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(associateReportResponse.Response.AssociateList);
                model.dtDetails = dataTable;
                int? totalRecords = 0;
                if (dataTable.Rows.Count > 0)
                {
                    totalRecords = int.Parse(model.dtDetails.Rows[0]["TotalRecords"].ToString());
                    var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                    model.Pager = pager;
                }
            }
            if (!string.IsNullOrEmpty(BtnExport))
            {
                try
                {
                    model.ExportToExcel = "1";
                    model.Type = "Harmony";
                    model.PayoutNo = associateReportResponse.Pk_PayoutId.ToString();
                    //model.PayoutNo = associateReport.PayoutNo.ToString();
                    model.CustomerId = Convert.ToInt32(HttpContext.Session.GetString("CustomerId"));
                    DataSet dataSet = model.GetAllIncomeReport();
                    if (dataSet.Tables[0] != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            model.dtDetails = dataSet.Tables[0];
                            DataTable dt = model.dtDetails;
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt);
                                using (MemoryStream stream = new MemoryStream())
                                {
                                    wb.SaveAs(stream);
                                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "HarmayReport.xlsx");
                                }
                            }
                        }

                    }


                }
                catch (Exception)
                {
                    throw;
                }
            }

            return View(model);

        }
        public ActionResult CreatorReport(AssociateReport model, string BtnExport)
        {
            #region PayoutNo
            string obj = "{\"OpCode\":\"" + 21 + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj);
            string result = Common.HITAPI(APIURL.GetMasterData, Body);
            MasterDataResponse masterDataResponse = new MasterDataResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            List<SelectListItem> ddlPayoutNo = new List<SelectListItem>();
            ddlPayoutNo.Add(new SelectListItem { Text = "Payout No.", Value = "0" });
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        ddlPayoutNo.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_Id"].ToString() });
                    }
                }
                ViewBag.ddlPayoutNo = ddlPayoutNo;
            }
            #endregion PayoutNo
            AssociateReportResponse associateReportResponse = new AssociateReportResponse();
            if (model.Page == 0 || model.Page == null)
            {
                model.Page = 1;
            }
            string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"PayoutNo\":\"" + model.PayoutNo + "\",\"Type\":\"" + "Creator" + "\",\"Page\":\"" + model.Page + "\"}";
            string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            result = Common.HITAPI(APIURL.CreatorPayoutReport, Body1);
            deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            associateReportResponse = JsonConvert.DeserializeObject<AssociateReportResponse>(dcdata1);

            if (associateReportResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(associateReportResponse.Response.AssociateList);
                model.dtDetails = dataTable;
                int? totalRecords = 0;
                if (dataTable.Rows.Count > 0)
                {
                    totalRecords = int.Parse(model.dtDetails.Rows[0]["TotalRecords"].ToString());
                    var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                    model.Pager = pager;
                }
            }
            if (!string.IsNullOrEmpty(BtnExport))
            {
                try
                {

                    model.ExportToExcel = "1";
                    model.Type = "Creator";
                    // model.PayoutNo = associateReport.Pk_PayoutId.ToString();
                    //model.PayoutNo = associateReport.PayoutNo.ToString();
                    model.CustomerId = Convert.ToInt32(HttpContext.Session.GetString("CustomerId"));
                    DataSet dataSet = model.GetAllIncomeReport();
                    if (dataSet.Tables[0] != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            model.dtDetails = dataSet.Tables[0];
                            DataTable dt = model.dtDetails;
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt);
                                using (MemoryStream stream = new MemoryStream())
                                {
                                    wb.SaveAs(stream);
                                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CreatorReport.xlsx");
                                }
                            }
                        }

                    }

                }
                catch (Exception)
                {
                    throw;
                }
            }

            return View(model);

        }
        public ActionResult Advisor1Report(AssociateReport model, string BtnExport)
        {
            #region PayoutNo
            string obj = "{\"OpCode\":\"" + 21 + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj);
            string result = Common.HITAPI(APIURL.GetMasterData, Body);
            MasterDataResponse masterDataResponse = new MasterDataResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            List<SelectListItem> ddlPayoutNo = new List<SelectListItem>();
            ddlPayoutNo.Add(new SelectListItem { Text = "Payout No.", Value = "0" });
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        ddlPayoutNo.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_Id"].ToString() });
                    }
                }
                ViewBag.ddlPayoutNo = ddlPayoutNo;
            }
            #endregion PayoutNo
            if (model.Page == 0 || model.Page == null)
            {
                model.Page = 1;
            }
            AssociateReportResponse associateReportResponse = new AssociateReportResponse();
            string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"PayoutNo\":\"" + model.PayoutNo + "\",\"Type\":\"" + "Advisor1" + "\",\"Page\":\"" + model.Page + "\",\"Size\":\"" + SessionManager.Size + "\"}";
            string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            result = Common.HITAPI(APIURL.Advisor1PayoutReport, Body1);
            deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            associateReportResponse = JsonConvert.DeserializeObject<AssociateReportResponse>(dcdata1);
            if (associateReportResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(associateReportResponse.Response.AssociateList);
                model.dtDetails = dataTable;
                int? totalRecords = 0;
                if (dataTable.Rows.Count > 0)
                {
                    totalRecords = int.Parse(model.dtDetails.Rows[0]["TotalRecords"].ToString());
                    var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                    model.Pager = pager;
                }
            }
            if (!string.IsNullOrEmpty(BtnExport))
            {
                try
                {

                    model.ExportToExcel = "1";
                    model.Type = "Advisor1";
                    //model.PayoutNo = associateReport.Pk_PayoutId.ToString();
                    //model.PayoutNo = model.PayoutNo.ToString();
                    model.CustomerId = Convert.ToInt32(HttpContext.Session.GetString("CustomerId"));
                    DataSet dataSet = model.GetAllIncomeReport();
                    if (dataSet.Tables[0] != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            model.dtDetails = dataSet.Tables[0];
                            DataTable dt = model.dtDetails;
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt);
                                using (MemoryStream stream = new MemoryStream())
                                {
                                    wb.SaveAs(stream);
                                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Advisor1Report.xlsx");
                                }
                            }
                        }

                    }


                }
                catch (Exception)
                {
                    throw;
                }
            }

            return View(model);

        }
        public ActionResult Advisor2Report(AssociateReport model, string BtnExport)
        {
            #region PayoutNo
            string obj = "{\"OpCode\":\"" + 21 + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj);
            string result = Common.HITAPI(APIURL.GetMasterData, Body);
            MasterDataResponse masterDataResponse = new MasterDataResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            List<SelectListItem> ddlPayoutNo = new List<SelectListItem>();
            ddlPayoutNo.Add(new SelectListItem { Text = "Payout No.", Value = "0" });
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        ddlPayoutNo.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_Id"].ToString() });
                    }
                }
                ViewBag.ddlPayoutNo = ddlPayoutNo;
            }
            #endregion PayoutNo
            AssociateReportResponse associateReportResponse = new AssociateReportResponse();
            if (model.Page == 0 || model.Page == null)
            {
                model.Page = 1;
            }
            string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"PayoutNo\":\"" + model.PayoutNo + "\",\"Type\":\"" + "Advisor2" + "\",\"Page\":\"" + model.Page + "\",\"Size\":\"" + SessionManager.Size + "\"}";
            string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            result = Common.HITAPI(APIURL.Advisor2PayoutReport, Body1);
            deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            associateReportResponse = JsonConvert.DeserializeObject<AssociateReportResponse>(dcdata1);
            if (associateReportResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(associateReportResponse.Response.AssociateList);
                model.dtDetails = dataTable;
                int? totalRecords = 0;
                if (dataTable.Rows.Count > 0)
                {
                    totalRecords = int.Parse(model.dtDetails.Rows[0]["TotalRecords"].ToString());
                    var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                    model.Pager = pager;
                }
            }
            if (!string.IsNullOrEmpty(BtnExport))
            {
                try
                {
                    //AssociateReport model = new AssociateReport();
                    model.ExportToExcel = "1";
                    model.Type = "Advisor2";
                    //model.PayoutNo = associateReport.Pk_PayoutId.ToString();
                    //model.PayoutNo = associateReport.PayoutNo.ToString();
                    model.CustomerId = Convert.ToInt32(HttpContext.Session.GetString("CustomerId"));
                    DataSet dataSet = model.GetAllIncomeReport();
                    if (dataSet.Tables[0] != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            model.dtDetails = dataSet.Tables[0];
                            DataTable dt = model.dtDetails;
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt);
                                using (MemoryStream stream = new MemoryStream())
                                {
                                    wb.SaveAs(stream);
                                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Advisor2Report.xlsx");
                                }
                            }
                        }

                    }


                }
                catch (Exception)
                {
                    throw;
                }
            }

            return View(model);

        }
        public ActionResult Advisor3Report(AssociateReport model, string BtnExport)
        {
            #region PayoutNo
            string obj = "{\"OpCode\":\"" + 21 + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj);
            string result = Common.HITAPI(APIURL.GetMasterData, Body);
            MasterDataResponse masterDataResponse = new MasterDataResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            List<SelectListItem> ddlPayoutNo = new List<SelectListItem>();
            ddlPayoutNo.Add(new SelectListItem { Text = "Payout No.", Value = "0" });
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        ddlPayoutNo.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_Id"].ToString() });
                    }
                }
                ViewBag.ddlPayoutNo = ddlPayoutNo;
            }
            #endregion PayoutNo
            AssociateReportResponse associateReportResponse = new AssociateReportResponse();
            if (model.Page == 0 || model.Page == null)
            {
                model.Page = 1;
            }
            string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"PayoutNo\":\"" + model.PayoutNo + "\",\"Type\":\"" + "Advisor3" + "\",\"Page\":\"" + model.Page + "\",\"Size\":\"" + SessionManager.Size + "\"}";
            string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            result = Common.HITAPI(APIURL.Advisor3PayoutReport, Body1);
            deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            associateReportResponse = JsonConvert.DeserializeObject<AssociateReportResponse>(dcdata1);
            if (associateReportResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(associateReportResponse.Response.AssociateList);
                model.dtDetails = dataTable;
                int? totalRecords = 0;
                if (dataTable.Rows.Count > 0)
                {
                    totalRecords = int.Parse(model.dtDetails.Rows[0]["TotalRecords"].ToString());
                    var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                    model.Pager = pager;
                }
            }
            if (!string.IsNullOrEmpty(BtnExport))
            {
                try
                {

                    model.ExportToExcel = "1";
                    model.Type = "Advisor3";
                    // model.PayoutNo = associateReport.Pk_PayoutId.ToString();
                    //model.PayoutNo = associateReport.PayoutNo.ToString();
                    model.CustomerId = Convert.ToInt32(HttpContext.Session.GetString("CustomerId"));
                    DataSet dataSet = model.GetAllIncomeReport();
                    if (dataSet.Tables[0] != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            model.dtDetails = dataSet.Tables[0];
                            DataTable dt = model.dtDetails;
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt);
                                using (MemoryStream stream = new MemoryStream())
                                {
                                    wb.SaveAs(stream);
                                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Advisor3Report.xlsx");
                                }
                            }
                        }

                    }


                }
                catch (Exception)
                {
                    throw;
                }
            }

            return View(model);

        }

        public ActionResult RoyalReport(AssociateReport model, string BtnExport)
        {
            #region PayoutNo
            string obj = "{\"OpCode\":\"" + 21 + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj);
            string result = Common.HITAPI(APIURL.GetMasterData, Body);
            MasterDataResponse masterDataResponse = new MasterDataResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            List<SelectListItem> ddlPayoutNo = new List<SelectListItem>();
            ddlPayoutNo.Add(new SelectListItem { Text = "Payout No.", Value = "0" });
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable1 = Common.ToDataTable(masterDataResponse.Response.RequestList);

                if (dataTable1 != null && dataTable1.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable1.Rows)
                    {
                        ddlPayoutNo.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_Id"].ToString() });
                    }
                }
                ViewBag.ddlPayoutNo = ddlPayoutNo;
            }
            #endregion PayoutNo
            if (model.Page == 0 || model.Page == null)
            {
                model.Page = 1;
            }
            AssociateReportResponse associateReportResponse = new AssociateReportResponse();
            string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"PayoutNo\":\"" + model.PayoutNo + "\",\"Type\":\"" + "Royal" + "\",\"Page\":\"" + model.Page + "\",\"Size\":\"" + SessionManager.Size + "\"}";
            string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            result = Common.HITAPI(APIURL.RoyalPayoutReport, Body1);
            deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            associateReportResponse = JsonConvert.DeserializeObject<AssociateReportResponse>(dcdata1);
            if (associateReportResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(associateReportResponse.Response.AssociateList);
                model.dtDetails = dataTable;
                int? totalRecords = 0;
                if (dataTable.Rows.Count > 0)
                {
                    totalRecords = int.Parse(model.dtDetails.Rows[0]["TotalRecords"].ToString());
                    var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                    model.Pager = pager;
                }
            }
            if (!string.IsNullOrEmpty(BtnExport))
            {

                try
                {
                    model.ExportToExcel = "1";
                    model.Type = "Royal";
                    // model.PayoutNo = associateReport.Pk_PayoutId.ToString();
                    //model.PayoutNo = associateReport.PayoutNo.ToString();
                    model.CustomerId = Convert.ToInt32(HttpContext.Session.GetString("CustomerId"));
                    DataSet dataSet = model.GetAllIncomeReport();
                    if (dataSet.Tables[0] != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            model.dtDetails = dataSet.Tables[0];
                            DataTable dt = model.dtDetails;
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt);
                                using (MemoryStream stream = new MemoryStream())
                                {
                                    wb.SaveAs(stream);
                                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "RoyalReport.xlsx");
                                }
                            }
                        }

                    }


                }
                catch (Exception)
                {
                    throw;
                }
            }
            return View(model);
        }
        public ActionResult PackPurchase()
        {
            ProfileReposne profileReposne = new ProfileReposne();
            string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.ViewProfile, Body);
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            profileReposne = JsonConvert.DeserializeObject<ProfileReposne>(dcdata);
            if (string.IsNullOrEmpty(profileReposne.Response.PanCard))
            {
                return RedirectToAction("UpdatePanPage");
            }
            return View();
        }
        public ActionResult UpdatePanPage()
        {
            return View();
        }
        public ActionResult GetProductDetails(string ProductId)
        {
            string Body = "";
            string obj1 = "{\"ProductId\":\"" + ProductId + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetProductDetailsForOfferPoint, Body);
            ProductDetailsForOfferPointResponse productDetailsforofferpoitResponse = new ProductDetailsForOfferPointResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            productDetailsforofferpoitResponse = JsonConvert.DeserializeObject<ProductDetailsForOfferPointResponse>(dcdata);

            return Json(productDetailsforofferpoitResponse);
        }
        public ActionResult PlacePack(CreateOrder createOrder, string btnAdd, string btnSave, string Id)
        {
            Common common = new Common();
            string Body = "";
            string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.AssociateWalletBalance, Body);
            WalletBalanceResponse walletBalanceResponse = new WalletBalanceResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            walletBalanceResponse = JsonConvert.DeserializeObject<WalletBalanceResponse>(dcdata);
            createOrder.WalletBalance = walletBalanceResponse.Response.WalletBalanceData.Balance.ToString();

            obj1 = "{\"OpCode\":\"" + 22 + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            result = Common.HITAPI(APIURL.GetMasterData, Body);
            MasterDataResponse masterDataResponse = new MasterDataResponse();
            deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            List<SelectListItem> ddlProducts = new List<SelectListItem>();
            ddlProducts.Add(new SelectListItem { Text = "Select Product", Value = "0" });
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        ddlProducts.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_Id"].ToString() });
                    }
                }

            }
            CartResponse cartResponse = new CartResponse();
            ViewBag.ddlProducts = ddlProducts;
            //createOrder.AddedBy = Convert.ToInt32(HttpContext.Session.GetString("CustomerId"));
            if (!string.IsNullOrEmpty(btnAdd))
            {
                obj1 = "{\"VarientId\":\"" + (createOrder.Fk_ProductId) + "\",\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"Quantity\":\"" + createOrder.Qty + "\",\"OrderType\":\"FUPPOINTS\"}";
                Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                result = Common.HITAPI(APIURL.AddToCart, Body);
                deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
                cartResponse = JsonConvert.DeserializeObject<CartResponse>(dcdata);
                if (cartResponse.Status == 0)
                {
                    TempData["CreateOrder"] = cartResponse.Message;
                }
            }
            if (!string.IsNullOrEmpty(btnSave))
            {
                if (decimal.Parse(createOrder.BeforeTax) < decimal.Parse(Crypto.Decrypt(HttpContext.Session.GetString("PackValue"))))
                {
                    TempData["CreateOrder"] = "Order amount before tax should be " + Crypto.Decrypt(HttpContext.Session.GetString("PackValue")) + " PV";
                }
                else
                {

                    obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"PaymentMode\":\"Wallet\",\"Pk_AddressId\":\"" + 0 + "\",\"OrderType\":\"FamilynPoints\",\"PackValue\":\"" + Crypto.Decrypt(HttpContext.Session.GetString("PackValue")) + "\"}";
                    Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                    result = Common.HITAPI(APIURL.PlaceOrder, Body);
                    PlaceOrderResponse placeOrderResponse = new PlaceOrderResponse();
                    deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                    dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
                    placeOrderResponse = JsonConvert.DeserializeObject<PlaceOrderResponse>(dcdata);
                    if (placeOrderResponse.Status == 1)
                    {
                        HttpContext.Session.SetString("OrderNo", placeOrderResponse.OrderNo);
                        return RedirectToAction("PickUpFranchisee");
                    }
                    else
                    {
                        TempData["Placeorder"] = placeOrderResponse.Message;
                    }
                }
            }
            if (!string.IsNullOrEmpty(Id))
            {
                HttpContext.Session.SetString("PackValue", Id);
            }
            obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            result = Common.HITAPI(APIURL.CartList, Body);

            deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            cartResponse = JsonConvert.DeserializeObject<CartResponse>(dcdata);
            if (cartResponse.Response != null)
            {
                createOrder.dtDetails = Common.ToDataTable(cartResponse.Response.CartList);

                // createOrder.OrderAmount = double.Parse(createOrder.dtDetails.Compute("sum(FinalPrice)", "").ToString()).ToString("0.00");

            }
            //createOrder.Balance = dataSet1.Tables[1].Rows[0]["Balance"].ToString();

            return View(createOrder);
        }
        public async Task<ActionResult> FamilynWalletRequest(FamilynWalletRequest familynWalletRequest, string Request)
        {
            familynWalletRequest.LoginId = HttpContext.Session.GetString("LoginId").ToString();
            familynWalletRequest.Name = HttpContext.Session.GetString("FirstName").ToString() + ' ' + HttpContext.Session.GetString("LastName").ToString();

            #region BindPaymentMode
            string obj1 = "{\"OpCode\":\"" + 4 + "\",\"Value\":\"" + "" + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetMasterData, Body);

            MasterDataResponse masterDataResponse = new MasterDataResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);
                List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
                ddlPaymentMode.Add(new SelectListItem { Text = "Payment Mode", Value = "0" });
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Name"].ToString() });
                    }
                }
                ViewBag.ddlPaymentMode = ddlPaymentMode;
            }
            #endregion

            if (!string.IsNullOrEmpty(Request))
            {
                if (familynWalletRequest.ImageFile != null)
                {
                    string fileName = await FileManagement.WriteFiles(familynWalletRequest.ImageFile, "FamilynWalletRequest");
                    familynWalletRequest.PaymentSlip = fileName;
                }
                obj1 = "{\"LoginId\":\"" + HttpContext.Session.GetString("LoginId") + "\",\"Amount\":\"" + familynWalletRequest.Amount + "\",\"PaymentMode\":\"" + familynWalletRequest.PaymentMode + "\",\"TransactionNo\":\"" + familynWalletRequest.TransactionNo + "\",\"TransactionDate\":\"" + familynWalletRequest.TransactionDate + "\",\"PaymentSlip\":\"" + familynWalletRequest.PaymentSlip + "\"}";
                Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                result = Common.HITAPI(APIURL.FamilynWalletRequest, Body);
                FamilynWalletResponse familynWalletResponse = new FamilynWalletResponse();
                deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
                familynWalletResponse = JsonConvert.DeserializeObject<FamilynWalletResponse>(dcdata);
                if (familynWalletResponse.Status == 1)
                {
                    TempData["FamilynWalletRequest"] = Messages.WalletSuccess;
                    return RedirectToAction("FamilynWalletList");
                }
                TempData["FamilynWalletRequest"] = familynWalletResponse.Message;
            }
            return View(familynWalletRequest);
        }
        public ActionResult FamilynWalletList(FamilynWalletListRequest familynWalletListRequest)
        {
            string obj1 = "{\"LoginId\":\"" + HttpContext.Session.GetString("LoginId") + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.FamilynWalletList, Body);

            FamilynWalletListResponse familynWalletListResponse = new FamilynWalletListResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            familynWalletListResponse = JsonConvert.DeserializeObject<FamilynWalletListResponse>(dcdata);
            if (familynWalletListResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(familynWalletListResponse.Response.WalletList);
                familynWalletListRequest.dtDetails = dataTable;
            }
            return View(familynWalletListRequest);
        }

        public ActionResult FamilynWalletLedger(FamilynWalletLedgerRequest familynWalletLedgerRequest)
        {
            if (familynWalletLedgerRequest.Page == 0 || familynWalletLedgerRequest.Page == null)
            {
                familynWalletLedgerRequest.Page = 1;
            }
            string obj1 = "{\"LoginId\":\"" + HttpContext.Session.GetString("LoginId") + "\",\"Page\":\"" + familynWalletLedgerRequest.Page + "\",\"Size\":\"" + SessionManager.Size + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.FamilynWalletLedger, Body);

            FamilynWalletLedgerResponse familynWalletResponse = new FamilynWalletLedgerResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            familynWalletResponse = JsonConvert.DeserializeObject<FamilynWalletLedgerResponse>(dcdata);
            if (familynWalletResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(familynWalletResponse.Response.WalletLedger);
                familynWalletLedgerRequest.dtDetails = dataTable;
                int? totalRecords = 0;
                if (dataTable.Rows.Count > 0)
                {
                    totalRecords = int.Parse(dataTable.Rows[0]["TotalRecords"].ToString());
                    var pager = new Pager(totalRecords, familynWalletLedgerRequest.Page, SessionManager.Size);
                    familynWalletLedgerRequest.Pager = pager;
                }
            }

            return View(familynWalletLedgerRequest);
        }
        public ActionResult PayoutLedger(PayoutLedgerRequest franchiseePayoutLedgerRequest)
        {
            if (franchiseePayoutLedgerRequest.Page == 0 || franchiseePayoutLedgerRequest.Page == null)
            {
                franchiseePayoutLedgerRequest.Page = 1;
            }
            string obj1 = "{\"LoginId\":\"" + HttpContext.Session.GetString("LoginId") + "\",\"Page\":\"" + franchiseePayoutLedgerRequest.Page + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.AssociatePayoutLedger, Body);

            PayoutLedgerResponse PayoutLedgerResponse = new PayoutLedgerResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            PayoutLedgerResponse = JsonConvert.DeserializeObject<PayoutLedgerResponse>(dcdata);
            if (PayoutLedgerResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(PayoutLedgerResponse.Response.payoutList);
                franchiseePayoutLedgerRequest.dtDetails = dataTable;
                int? totalRecords = 0;
                if (dataTable.Rows.Count > 0)
                {
                    totalRecords = int.Parse(dataTable.Rows[0]["TotalRecords"].ToString());
                    var pager = new Pager(totalRecords, franchiseePayoutLedgerRequest.Page, SessionManager.Size);
                    franchiseePayoutLedgerRequest.Pager = pager;
                }
            }

            return View(franchiseePayoutLedgerRequest);
        }

        public ActionResult PrintFUPOrder(string OrderNo)
        {
            CreateOrder createOrder = new CreateOrder();
            createOrder.OrderNo = OrderNo;

            DataSet dataSet = createOrder.PrintCustomerOrder();
            ViewBag.ContactPerson = dataSet.Tables[0].Rows[0]["ContactPerson"].ToString();
            ViewBag.LoginId = dataSet.Tables[0].Rows[0]["LoginId"].ToString();
            ViewBag.Address = dataSet.Tables[0].Rows[0]["Address"].ToString();
            ViewBag.Mobile = dataSet.Tables[0].Rows[0]["Mobile"].ToString();
            ViewBag.OrderNo = dataSet.Tables[0].Rows[0]["OrderNo"].ToString();
            ViewBag.DispatchCount = dataSet.Tables[0].Rows[0]["DispatchCount"].ToString();
            ViewBag.OrderDate = dataSet.Tables[0].Rows[0]["OrderDate"].ToString();
            ViewBag.InvoiceDate = dataSet.Tables[0].Rows[0]["InvoiceDate"].ToString();
            ViewBag.PinCode = dataSet.Tables[0].Rows[0]["PinCode"].ToString();
            ViewBag.ConsignerPinCode = dataSet.Tables[0].Rows[0]["ConsignerPinCode"].ToString();
            ViewBag.InvoiceNo = dataSet.Tables[0].Rows[0]["InvoiceNo"].ToString();
            ViewBag.AmountInWords = dataSet.Tables[2].Rows[0]["AmountInWords"].ToString();
            ViewBag.GSTNo = dataSet.Tables[0].Rows[0]["GSTNo"].ToString();

            createOrder.dtDetails = dataSet.Tables[1];

            return View(createOrder);
        }
        public ActionResult RewardDetails(RewardDetails model)
        {

            string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.RewardDetails, Body);

            RewardReposne RewardReposne = new RewardReposne();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            RewardReposne = JsonConvert.DeserializeObject<RewardReposne>(dcdata);
            //if (RewardReposne.Status == 1)
            //{
            //    DataTable dataTable = Common.ToDataTable(familynWalletResponse.Response.WalletLedger);
            //    RewardReposne.dtDetails = dataTable;

            //}
            return View(RewardReposne);
        }
        #region  Claim 
        // [HttpPost]
        //public ActionResult Claim(string rId)
        //{
        //    RewardDetails obj = new RewardDetails();
        //    //RewardsModel obj2 = new RewardsModel();

        //    obj.FkSetRewardId = rId;
        //    obj.Action = "Claim";
        //    obj.CustomerId = int.Parse(HttpContext.Session.GetString("CustomerId"));
        //    obj2 = obj.ClaimAndSkip();
        //    return Json("");
        //}
        #endregion Claim
        //#region Skip 
        //[HttpPost]
        //public ActionResult Skip(RewardData model, int rId)
        //{


        //    model.PK_RId = rId;
        //    model.Action = "Skip";
        //    //obj2.FkMemId = SessionManager.AssociateFk_MemId;
        //    //obj2 = obj.ClaimAndSkip(obj2);
        //    return Json("");
        //}
        //#endregionGetBusiness
        public ActionResult DeleteAddress(string Pk_AddressId)
        {
            AddressResponse addressResponse = new AddressResponse();
            if (!string.IsNullOrEmpty(Pk_AddressId))
            {
                string obj = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"Pk_AddressId\":\"" + Pk_AddressId + "\"}";
                string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj);
                string result = Common.HITAPI(APIURL.DeleteAddress, Body);
                ShoppingWebResponse deserializedProduct1 = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct1.body);
                addressResponse = JsonConvert.DeserializeObject<AddressResponse>(dcdata);
                return Json(addressResponse);
            }
            return View();
        }
        public ActionResult CreatorBusiness(CreatorBusiness model)
        {

            CreatorBusinessResponse creatorBusinessResponse = new CreatorBusinessResponse();
            if (model.Page == 0 || model.Page == null)
            {
                model.Page = 1;
            }
            string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"Page\":\"" + model.Page + "\"}";
            string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.CreatorBusiness, Body1);
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            creatorBusinessResponse = JsonConvert.DeserializeObject<CreatorBusinessResponse>(dcdata1);
            if (creatorBusinessResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(creatorBusinessResponse.Response.CreatorBusinessLst);
                model.dtDetails = dataTable;
                int? totalRecords = 0;
                if (dataTable.Rows.Count > 0)
                {
                    totalRecords = int.Parse(dataTable.Rows[0]["TotalRecords"].ToString());
                    var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                    model.Pager = pager;
                }
            }

            return View(model);

        }
        public ActionResult CreatorHarmony(CreatorBusiness model)
        {

            CreatorHarmonyResponse creatorBusinessResponse = new CreatorHarmonyResponse();
            if (model.Page == 0 || model.Page == null)
            {
                model.Page = 1;
            }
            string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"Page\":\"" + model.Page + "\"}";
            string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.CreatorHarmony, Body1);
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            creatorBusinessResponse = JsonConvert.DeserializeObject<CreatorHarmonyResponse>(dcdata1);
            if (creatorBusinessResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(creatorBusinessResponse.Response.CreatorHamonyL);
                DataTable dataTable1 = Common.ToDataTable(creatorBusinessResponse.Response.CreatorHamonyR);
                model.dtDetails = dataTable;
                model.dtDetailsR = dataTable1;

            }

            return View(model);

        }
        public ActionResult PCreatorHarmony(AssociateReport model, string BtnExport)
        {
            #region PayoutNo
            string obj = "{\"OpCode\":\"" + 21 + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj);
            string result = Common.HITAPI(APIURL.GetMasterData, Body);
            MasterDataResponse masterDataResponse = new MasterDataResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            List<SelectListItem> ddlPayoutNo = new List<SelectListItem>();
            ddlPayoutNo.Add(new SelectListItem { Text = "Payout No.", Value = "0" });
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        ddlPayoutNo.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_Id"].ToString() });
                    }
                }
                ViewBag.ddlPayoutNo = ddlPayoutNo;
            }
            #endregion PayoutNo
            AssociateReportResponse associateReportResponse = new AssociateReportResponse();
            if (model.Page == 0 || model.Page == null)
            {
                model.Page = 1;
            }
            string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"PayoutNo\":\"" + model.PayoutNo + "\",\"Type\":\"" + "Creator Harmony" + "\",\"Page\":\"" + model.Page + "\"}";
            string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            result = Common.HITAPI(APIURL.CreatorPayoutReport, Body1);
            deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            associateReportResponse = JsonConvert.DeserializeObject<AssociateReportResponse>(dcdata1);

            if (associateReportResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(associateReportResponse.Response.AssociateList);
                model.dtDetails = dataTable;
                int? totalRecords = 0;
                if (dataTable.Rows.Count > 0)
                {
                    totalRecords = int.Parse(model.dtDetails.Rows[0]["TotalRecords"].ToString());
                    var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                    model.Pager = pager;
                }
            }
            if (!string.IsNullOrEmpty(BtnExport))
            {
                try
                {

                    model.ExportToExcel = "1";
                    model.Type = "Creator";
                    // model.PayoutNo = associateReport.Pk_PayoutId.ToString();
                    //model.PayoutNo = associateReport.PayoutNo.ToString();
                    model.CustomerId = Convert.ToInt32(HttpContext.Session.GetString("CustomerId"));
                    DataSet dataSet = model.GetAllIncomeReport();
                    if (dataSet.Tables[0] != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            model.dtDetails = dataSet.Tables[0];
                            DataTable dt = model.dtDetails;
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt);
                                using (MemoryStream stream = new MemoryStream())
                                {
                                    wb.SaveAs(stream);
                                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CreatorReport.xlsx");
                                }
                            }
                        }

                    }

                }
                catch (Exception)
                {
                    throw;
                }
            }

            return View(model);

        }
        public ActionResult CancelOrder(string OrderId)
        {
            if (!string.IsNullOrEmpty(OrderId))
            {
                CancelOrderResponse cancelOrderResponse = new CancelOrderResponse();
                string obj1 = "{\"Fk_MemId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"Fk_OrderId\":\"" + OrderId + "\"}";
                string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                string result = Common.HITAPI(APIURL.CancelOrder, Body1);
                ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
                cancelOrderResponse = JsonConvert.DeserializeObject<CancelOrderResponse>(dcdata1);
                if (cancelOrderResponse.Status == 1)
                {
                    TempData["CancelOrder"] = cancelOrderResponse.Message;
                }
            }
            return RedirectToAction("OrderList");
        }
        public ActionResult ProductReview(CreateOrder createOrder)
        {
            ProductReviewResponse productReviewResponse = new ProductReviewResponse();
            string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"Fk_ProductId\":\"" + createOrder.Fk_ProductId + "\",\"Star\":\"" + createOrder.StarRating + "\",\"Rating\":\"" + createOrder.ReviewMsg + "\",\"Fk_OrderId\":\"" + createOrder.Fk_OrderId + "\"}";
            string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.ProductsReview, Body1);
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            productReviewResponse = JsonConvert.DeserializeObject<ProductReviewResponse>(dcdata1);
            if (productReviewResponse != null)
            {
                if (productReviewResponse.Status == 1)
                {
                    TempData["CancelOrder"] = productReviewResponse.Message;
                }
            }
            return RedirectToAction("OrderList");
        }
        public ActionResult Business(BusinessReport model, string ExporttoExcel)
        {
            if (model.Page == 0 || model.Page == null)
            {
                model.Page = 1;
            }
            string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"Page\":\"" + model.Page + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.Business, Body);

            BusinessReposne businessReposne = new BusinessReposne();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            businessReposne = JsonConvert.DeserializeObject<BusinessReposne>(dcdata);
            if (businessReposne.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(businessReposne.Response.BusinessList);
                model.dtDetails = dataTable;
                int? totalRecords = 0;
                if (dataTable.Rows.Count > 0)
                {
                    totalRecords = int.Parse(model.dtDetails.Rows[0]["TotalRecords"].ToString());
                    var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                    model.Pager = pager;
                }
            }
            if (!string.IsNullOrEmpty(ExporttoExcel))
            {
                try
                {
                    //model.PayoutNo = associateReport.PayoutNo.ToString();
                    model.CustomerId = Convert.ToInt32(HttpContext.Session.GetString("CustomerId"));
                    model.ExportToExcel = "1";
                    DataSet dataSet = model.GetBusinessReport();
                    if (dataSet.Tables[0] != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            model.dtDetails = dataSet.Tables[0];
                            DataTable dt = model.dtDetails;
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt);
                                using (MemoryStream stream = new MemoryStream())
                                {
                                    wb.SaveAs(stream);
                                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BusinessReport.xlsx");
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }


            return View(model);
        }
        public ActionResult GetOrderList(string OrderId)
        {
            CreateOrder createOrder = new CreateOrder();
            if (createOrder.Page == null || createOrder.Page == 0)
            {
                createOrder.Page = 1;
            }

            createOrder.Size = SessionManager.Size;
            createOrder.Status = createOrder.Status == "0" ? null : createOrder.Status;
            createOrder.OpCode = 7;
            createOrder.OrderNo = OrderId;

            DataSet dataSet = createOrder.GetCustomerOrders();
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                createOrder.Status = dataSet.Tables[0].Rows[0]["Status"].ToString();
            }
            else
            {
                createOrder.Status = "Pending";
            }
            return Json(createOrder);
        }
        public ActionResult GetEventDetials(EventListRequest model)
        {
            string obj1 = "{\"Fk_MemId\":\"" + HttpContext.Session.GetString("CustomerId") + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetEventList, Body);
            EventListResponse eventListResponse = new EventListResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            eventListResponse = JsonConvert.DeserializeObject<EventListResponse>(dcdata);
            if (eventListResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(eventListResponse.Response.EventListDetails);
                model.dtDetails = dataTable;
                model.Pk_EventBookingId = eventListResponse.Response.EventListDetails[0].Pk_EventBookingId;
            }
            return View(model);
        }
        public async Task<ActionResult> UploadAssociateImage(EventListRequest model)
        {
            if (model.Imagefile != null)
            {
                string fileName = await FileManagement.WriteFiles(model.Imagefile, "AssociateImage");
                model.Image = fileName;
            }

            string obj1 = "{\"Pk_EventBookingId\":\"" + model.Pk_EventBookingId + "\",\"Image\":\"" + model.Image + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.UploadAssociateImage, Body);

            AssociateImageResponse eventListResponse = new AssociateImageResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            eventListResponse = JsonConvert.DeserializeObject<AssociateImageResponse>(dcdata);
            if (eventListResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(eventListResponse.Response.EventAssociateImage);
                model.dtDetails = dataTable;

            }

            return RedirectToAction("GetEventDetials");
        }

        public ActionResult Criteria(Criterias model, string CustomerId)
        {
            try
            {
                Criterias criteria = new Criterias();
                CriteriaDataResp response = new CriteriaDataResp();
                // model.Fk_MemId = HttpContext.Session.GetString("CustomerId");
                if (!string.IsNullOrEmpty(CustomerId))
                {
                    model.Fk_MemId = CustomerId;
                }
                else
                {
                    model.Fk_MemId = HttpContext.Session.GetString("CustomerId");
                }
                CriteriaDataResp criteriaDetailsResponse = new CriteriaDataResp();
                string obj = "{\"Fk_MemId\":\"" + model.Fk_MemId + "\"}";
                string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj);
                string result = Common.HITAPI(APIURL.Criteria, Body);
                ShoppingWebResponse deserialized = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserialized.body);
                criteriaDetailsResponse = JsonConvert.DeserializeObject<CriteriaDataResp>(dcdata);
                if (criteriaDetailsResponse != null)
                {
                    if (criteriaDetailsResponse.Status == "1")
                    {
                        DataTable dataTable = Common.ToDataTable(criteriaDetailsResponse.responselist.Criteriaslist);
                        DataTable dataTable1 = Common.ToDataTable(criteriaDetailsResponse.responselist.Criteriaslist1);
                        model.dtDetails = dataTable;
                        model.dtDetailsR = dataTable1;

                    }
                }
                //DataSet dataSet = model.getCriteriaDetails();
                //if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                //{
                //    model.dtDetails = dataSet.Tables[0];

                //}
                //if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[1].Rows.Count > 0)
                //{
                //    model.dtDetailsR = dataSet.Tables[1];
                //}
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public ActionResult GetCriteriaDetails()
        {
            Criteria model = new Criteria();
            model.Fk_MemId = HttpContext.Session.GetString("CustomerId");
            DataSet dataSet = model.GetCriteriaDetails();
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    List<CriteriaDetails> listZoneA = new List<CriteriaDetails>();

                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        CriteriaDetails ZoneA = new CriteriaDetails();
                        ZoneA.LoginId = dataSet.Tables[0].Rows[i]["LoginId"].ToString();
                        ZoneA.Name = dataSet.Tables[0].Rows[i]["Name"].ToString();
                        ZoneA.PV = dataSet.Tables[0].Rows[i]["TotalPV"].ToString();
                        listZoneA.Add(ZoneA);
                    }
                    model.ZoneA = listZoneA;
                }
                if (dataSet.Tables[1].Rows.Count > 0)
                {
                    List<CriteriaDetails> listZoneB = new List<CriteriaDetails>();
                    for (int i = 0; i < dataSet.Tables[1].Rows.Count; i++)
                    {
                        CriteriaDetails ZoneB = new CriteriaDetails();
                        ZoneB.LoginId = dataSet.Tables[1].Rows[i]["LoginId"].ToString();
                        ZoneB.Name = dataSet.Tables[1].Rows[i]["Name"].ToString();
                        ZoneB.PV = dataSet.Tables[1].Rows[i]["TotalPV"].ToString();
                        listZoneB.Add(ZoneB);
                    }
                    model.ZoneB = listZoneB;
                }
            }
            return Json(model);
        }

        public ActionResult GetOrderListForQr(string OrderId)
        {
            DataSet dataSet = new DataSet();
            CreateOrder createOrder = new CreateOrder();
            KaryonPointsRequest karyonPointsRequest = new KaryonPointsRequest();
            if (createOrder.Page == null || createOrder.Page == 0)
            {
                createOrder.Page = 1;
            }

            createOrder.Size = SessionManager.Size;
            createOrder.Status = createOrder.Status == "0" ? null : createOrder.Status;
            createOrder.OpCode = 8;
            createOrder.OrderNo = OrderId;
            if (HttpContext.Session.GetString("Type").ToString()== "Karyon Wallet")
            {
                karyonPointsRequest.LoginId = HttpContext.Session.GetString("LoginId");
                karyonPointsRequest.MerchantTranId = OrderId;
                dataSet = karyonPointsRequest.GetKaryonPointsList();
                createOrder.Type = "KaryonWallet";
            }
            else
            {
                dataSet = createOrder.GetCustomerOrders();
                createOrder.Type = "CustomerOrder";
            }
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                createOrder.Status = dataSet.Tables[0].Rows[0]["Status"].ToString();
            }
            else
            {
                createOrder.Status = "Pending";
            }
            return Json(createOrder);
        }

        public ActionResult UpdateBID(string LoginId)
        {
            ProfileRequest model = new ProfileRequest();
            BIDReposne response = new BIDReposne();
            try
            {

                model.LoginId = LoginId;
                BIDReposne profileresponse = new BIDReposne();
                string obj1 = "{\"LoginId\":\"" + model.LoginId + "\"}";
                string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                string result = Common.HITAPI(APIURL.UpdateBIDStatus, Body1);
                ShoppingWebResponse deserialized = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserialized.body);
                response = JsonConvert.DeserializeObject<BIDReposne>(dcdata1);
                if (response != null)
                {
                    if (response.Status == "1")
                    {
                        DataTable dataTable = Common.ToDataTable(response.Response.profilelist);
                        response.dtDetails = dataTable;
                        if (response.dtDetails.Rows.Count > 0)
                        {
                            model.Flag = response.dtDetails.Rows[0]["Flag"].ToString();
                            model.Status = response.dtDetails.Rows[0]["Status"].ToString();
                        }
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }

            return Json(model);
        }

        public ActionResult Promotracker(string CustomerId)
        {
            Promotracter model = new Promotracter();
            PromoTracterResponse response = new PromoTracterResponse();
            try
            {

                if (!string.IsNullOrEmpty(CustomerId))
                {
                    model.Fk_MemId = CustomerId;
                }
                else
                {
                    model.Fk_MemId = HttpContext.Session.GetString("CustomerId");
                }
                PromoTracterResponse promotracterresponse = new PromoTracterResponse();
                string obj1 = "{\"Fk_MemId\":\"" + model.Fk_MemId + "\"}";
                string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                string result = Common.HITAPI(APIURL.PromoTracker, Body1);
                ShoppingWebResponse deserialized = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserialized.body);
                response = JsonConvert.DeserializeObject<PromoTracterResponse>(dcdata1);
                if (response != null)
                {
                    if (response.Status == "1")
                    {
                        TempData["CancelOrder"] = response.message;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(response);
        }
        [HttpPost]
        public ActionResult GetPendingPromoTrackerDetails(string levelId)
        {
            Promotracter model = new Promotracter();
            PromoTracterResponse response = new PromoTracterResponse();
            try
            {

                model.PK_LevelId = levelId;
                model.Fk_MemId = HttpContext.Session.GetString("CustomerId");

                PromoTracterResponse promotracterresponse = new PromoTracterResponse();
                string obj1 = "{\"Fk_MemId\":\"" + model.Fk_MemId + "\",\"PK_LevelId\":\"" + model.PK_LevelId + "\"}";
                string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                string result = Common.HITAPI(APIURL.PendingPromoTracker, Body1);
                ShoppingWebResponse deserialized = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserialized.body);
                response = JsonConvert.DeserializeObject<PromoTracterResponse>(dcdata1);
                if (response != null)
                {
                    if (response.Status == "1")
                    {
                        TempData["CancelOrder"] = response.message;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(response);
        }

        public ActionResult GetBusiness(string Type, string Fk_MemId)
        {

            try
            {

                AssociateReport associateReport = new AssociateReport();
                if (string.IsNullOrEmpty(Fk_MemId))
                {
                    associateReport.CustomerId = int.Parse(HttpContext.Session.GetString("CustomerId"));

                }
                else
                {
                    associateReport.CustomerId = int.Parse(Fk_MemId);


                }
                associateReport.Type = Type;

                associateReport.SessionId = int.Parse(HttpContext.Session.GetString("CustomerId"));
                DataSet dataSet = associateReport.GetBusiness();
                if (dataSet.Tables.Count > 0)
                {
                    HttpContext.Session.SetString("PreviousId", dataSet.Tables[1].Rows[0]["PreviousId"].ToString());
                    List<AssociateReport> listResponses = new List<AssociateReport>();

                    for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                    {
                        AssociateReport data = new AssociateReport();
                        data.OrderNo = dataSet.Tables[0].Rows[i]["OrderNo"].ToString();
                        data.OrderDate = dataSet.Tables[0].Rows[i]["OrderDate"].ToString();
                        data.OrderAmount = dataSet.Tables[0].Rows[i]["OrderAmount"].ToString();
                        data.TotalPV = dataSet.Tables[0].Rows[i]["TotalPV"].ToString();
                        data.LoginId = dataSet.Tables[0].Rows[i]["LoginId"].ToString();
                        data.Name = dataSet.Tables[0].Rows[i]["Name"].ToString();
                        data.Fk_MemId = dataSet.Tables[0].Rows[i]["Fk_MemId"].ToString();
                        data.Type = associateReport.Type;
                        listResponses.Add(data);

                    }
                    associateReport.SelfBusiness = dataSet.Tables[1].Rows[0]["SelfBusiness"].ToString();

                    associateReport.lstData = listResponses;
                }
                return Json(associateReport);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult GetPreviousBusiness(string Type)
        {
            AssociateReport associateReport = new AssociateReport();
            AssociateReportResp returnResponse = new AssociateReportResp();

            try
            {
                associateReport.CustomerId = int.Parse(HttpContext.Session.GetString("PreviousId"));
                associateReport.SessionId = int.Parse(HttpContext.Session.GetString("CustomerId"));
                associateReport.Type = Type;
                DataSet dataSet = associateReport.GetBusiness();
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    HttpContext.Session.SetString("PreviousId", dataSet.Tables[1].Rows[0]["PreviousId"].ToString());
                    List<AssociateReport> listResponses = new List<AssociateReport>();

                    for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                    {
                        AssociateReport data = new AssociateReport();
                        data.OrderNo = dataSet.Tables[0].Rows[i]["OrderNo"].ToString();
                        data.OrderDate = dataSet.Tables[0].Rows[i]["OrderDate"].ToString();
                        data.OrderAmount = dataSet.Tables[0].Rows[i]["OrderAmount"].ToString();
                        data.TotalPV = dataSet.Tables[0].Rows[i]["TotalPV"].ToString();
                        data.LoginId = dataSet.Tables[0].Rows[i]["LoginId"].ToString();
                        data.Name = dataSet.Tables[0].Rows[i]["Name"].ToString();
                        data.Fk_MemId = dataSet.Tables[0].Rows[i]["Fk_MemId"].ToString();

                        listResponses.Add(data);

                    }
                    associateReport.SelfBusiness = dataSet.Tables[1].Rows[0]["SelfBusiness"].ToString();

                    associateReport.lstData = listResponses;
                }
                return Json(associateReport);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public ActionResult GetPreviousTotal()
        {
            AssociateReport associateReport = new AssociateReport();
            AssociateReportResp returnResponse = new AssociateReportResp();

            try
            {
                associateReport.CustomerId = int.Parse(HttpContext.Session.GetString("PreviousId"));
                associateReport.SessionId = int.Parse(HttpContext.Session.GetString("CustomerId"));
                associateReport.Type = "Total";
                DataSet dataSet = associateReport.GetBusiness();
                HttpContext.Session.SetString("PreviousId", dataSet.Tables[1].Rows[0]["PreviousId"].ToString());
                List<AssociateReport> listResponses = new List<AssociateReport>();
                for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                {
                    AssociateReport data = new AssociateReport();
                    data.OrderNo = dataSet.Tables[0].Rows[i]["OrderNo"].ToString();
                    data.OrderDate = dataSet.Tables[0].Rows[i]["OrderDate"].ToString();
                    data.OrderAmount = dataSet.Tables[0].Rows[i]["OrderAmount"].ToString();
                    data.TotalPV = dataSet.Tables[0].Rows[i]["TotalPV"].ToString();
                    data.LoginId = dataSet.Tables[0].Rows[i]["LoginId"].ToString();
                    data.Name = dataSet.Tables[0].Rows[i]["Name"].ToString();
                    data.Fk_MemId = dataSet.Tables[0].Rows[i]["Fk_MemId"].ToString();

                    listResponses.Add(data);

                }
                associateReport.SelfBusiness = dataSet.Tables[1].Rows[0]["SelfBusiness"].ToString();
                associateReport.lstData = listResponses;
                return Json(associateReport);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult UnpaidPayout(UnpaidPayoutRequest unpaidPayoutRequest)
        {

            #region ddlIncometype
            string obj11 = "{\"OpCode\":\"" + 52 + "\",\"Value\":\"" + "" + "\"}";
            string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj11);
            string result1 = Common.HITAPI(APIURL.GetMasterData, Body1);
            MasterDataResponse masterDataResponse = new MasterDataResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result1);
            deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result1);
            string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata1);
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);
                List<SelectListItem> ddlIncometype = new List<SelectListItem>();
                ddlIncometype.Add(new SelectListItem { Text = "Select Income Type", Value = "0" });
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        ddlIncometype.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_Id"].ToString() });
                    }
                }
                ViewBag.ddlIncometype = ddlIncometype;
            }
            #endregion
            if (unpaidPayoutRequest.Page == 0 || unpaidPayoutRequest.Page == null)
            {
                unpaidPayoutRequest.Page = 1;
            }
            unpaidPayoutRequest.FromDate = string.IsNullOrEmpty(unpaidPayoutRequest.FromDate) ? null : Common.ConvertToSystemDate(unpaidPayoutRequest.FromDate, "dd/MM/yyyy");
            unpaidPayoutRequest.ToDate = string.IsNullOrEmpty(unpaidPayoutRequest.ToDate) ? null : Common.ConvertToSystemDate(unpaidPayoutRequest.ToDate, "dd/MM/yyyy");
            string obj1 = "{\"FromLoginId\":\"" + HttpContext.Session.GetString("LoginId") + "\",\"ToLoginId\":\"" + unpaidPayoutRequest.ToLoginId + "\",\"IncomeType\":\"" + unpaidPayoutRequest.IncomeType + "\",\"Page\":\"" + unpaidPayoutRequest.Page + "\",\"Size\":\"" + unpaidPayoutRequest.Size + "\",\"FromDate\":\"" + unpaidPayoutRequest.FromDate + "\",\"ToDate\":\"" + unpaidPayoutRequest.ToDate + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.AssociateUnpaidPayout, Body);

            UnpaidPayoutResponse unpaidPayoutResponse = new UnpaidPayoutResponse();
            ShoppingWebResponse deserializedProduct1 = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct1.body);
            unpaidPayoutResponse = JsonConvert.DeserializeObject<UnpaidPayoutResponse>(dcdata);
            if (unpaidPayoutResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(unpaidPayoutResponse.Response.unpaidpayoutList);
                unpaidPayoutRequest.dtDetails = dataTable;
                int? totalRecords = 0;
                if (dataTable.Rows.Count > 0)
                {
                    totalRecords = int.Parse(dataTable.Rows[0]["TotalRecords"].ToString());
                    var pager = new Pager(totalRecords, unpaidPayoutRequest.Page, SessionManager.Size);
                    unpaidPayoutRequest.Pager = pager;
                }
            }

            return View(unpaidPayoutRequest);
        }


        public ActionResult AssociateRepurchasePayoutReport(AssociateReport associateReport, string BtnExport)
        {

            #region ddlPayout
            string obj11 = "{\"OpCode\":\"" + 53 + "\",\"Value\":\"" + "" + "\"}";
            string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj11);
            string result1 = Common.HITAPI(APIURL.GetMasterData, Body1);
            MasterDataResponse masterDataResponse = new MasterDataResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result1);
            deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result1);
            string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata1);
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);
                List<SelectListItem> ddlpayoutNo = new List<SelectListItem>();
                ddlpayoutNo.Add(new SelectListItem { Text = "Select Payout No", Value = "0" });
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        ddlpayoutNo.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_Id"].ToString() });
                    }
                }
                ViewBag.ddlpayoutNo = ddlpayoutNo;
            }
            #endregion

            if (associateReport.Page == null || associateReport.Page == 0)
            {
                associateReport.Page = 1;
            }
            string obj1 = "{\"LoginId\":\"" + HttpContext.Session.GetString("LoginId") + "\",\"FormDate\":\"" + associateReport.FromDate + "\",\"ToDate\":\"" + associateReport.ToDate + "\",\"PayoutNo\":\"" + associateReport.PayoutNo + "\",\"Fk_MemId\":\"" + associateReport.Fk_MemId + "\",\"Page\":\"" + associateReport.Page + "\",\"Size\":\"" + SessionManager.Size + "\",\"OpCode\":\"" + 1 + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.AssociateRepurchasePayotReport, Body);

            AssociateReportResponse associateReportResponse = new AssociateReportResponse();
            ShoppingWebResponse deserializedProduct1 = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct1.body);
            associateReportResponse = JsonConvert.DeserializeObject<AssociateReportResponse>(dcdata);
            if (associateReportResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(associateReportResponse.Response.AssociateList);
                associateReport.dtDetails = dataTable;
                int? totalRecords = 0;
                if (dataTable.Rows.Count > 0)
                {
                    totalRecords = int.Parse(associateReport.dtDetails.Rows[0]["TotalRecords"].ToString());
                    var pager = new Pager(totalRecords, associateReport.Page, SessionManager.Size);
                    associateReport.Pager = pager;
                }
            }
            if (!string.IsNullOrEmpty(BtnExport))
            {
                try
                {
                    AssociateReport model = new AssociateReport();
                    model.ExportToExcel = "1";
                    model.OpCode = 1;
                    string sheetName = "RepurchasePayoutReport" + DateTime.Now + ".xlsx";
                    //model.PayoutNo = associateReport.PayoutNo.ToString();
                    model.CustomerId = Convert.ToInt32(HttpContext.Session.GetString("CustomerId"));
                    DataSet dataSet = model.GetRepAssociatePayout();
                    if (dataSet.Tables[0] != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            model.dtDetails = dataSet.Tables[0];
                            DataTable dt = model.dtDetails;
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt);
                                using (MemoryStream stream = new MemoryStream())
                                {
                                    wb.SaveAs(stream);
                                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sheetName);
                                }
                            }
                        }

                    }


                }
                catch (Exception)
                {
                    throw;
                }
            }
            return View(associateReport);


        }

        public ActionResult PayoutDetailsStatement(PayoutDetail model, string payoutno)
        {
            PayoutDetailResponse response = new PayoutDetailResponse();
            try
            {
                PayoutDetailResponse payoutesponse = new PayoutDetailResponse();
                string obj1 = "{\"Fk_MemId\":\"" + HttpContext.Session.GetString("CustomerId") + "\",\"PayoutNo\":\"" + payoutno + "\"}";
                string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                //string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                string result = Common.HITAPI(APIURL.GetPayoutDetailsStatement, Body1);
                ShoppingWebResponse deserialized = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserialized.body);
                response = JsonConvert.DeserializeObject<PayoutDetailResponse>(dcdata1);
                if (response != null)
                {
                    if (response.Status == "1")
                    {
                        DataTable dataTable = Common.ToDataTable(response.Response.list);
                        model.dtDetails = dataTable;
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            return View(model);
        }
        public ActionResult PayoutDetails(PayoutDetail model)
        {
            PayoutDetailResponse response = new PayoutDetailResponse();
            try
            {
                PayoutDetailResponse payoutesponse = new PayoutDetailResponse();
                model.Fk_MemId = HttpContext.Session.GetString("CustomerId").ToString();
                string obj1 = "{\"Fk_MemId\":\"" + model.Fk_MemId + "\"}";
                string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                //string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                string result = Common.HITAPI(APIURL.GetPayoutDetails, Body1);
                ShoppingWebResponse deserialized = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserialized.body);
                response = JsonConvert.DeserializeObject<PayoutDetailResponse>(dcdata1);
                if (response != null)
                {
                    if (response.Status == "1")
                    {

                        DataTable dataTable = Common.ToDataTable(response.Response.list);
                        model.dtDetails = dataTable;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View(model);
        }
        public ActionResult GetPayoutSummary(PayoutDetail model, string planId, string payoutNo)
        {
            PayoutDetailResponse response = new PayoutDetailResponse();
            try
            {
                List<PayoutDetail> lst = new List<PayoutDetail>();

                PayoutDetailResponse payoutesponse = new PayoutDetailResponse();
                model.PlanId = planId;
                model.PayoutNo = payoutNo;
                model.LoginId = HttpContext.Session.GetString("LoginId");
                string obj1 = "{\"PlanId\":\"" + model.PlanId + "\",\"PayoutNo\":\"" + model.PayoutNo + "\",\"LoginId\":\"" + model.LoginId + "\"}";
                string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                //string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                string result = Common.HITAPI(APIURL.GetPayoutDetailSummary, Body1);
                ShoppingWebResponse deserialized = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserialized.body);
                response = JsonConvert.DeserializeObject<PayoutDetailResponse>(dcdata1);
                if (response != null)
                {
                    if (response.Status == "1")
                    {
                        DataTable dataTable = Common.ToDataTable(response.Response.list);
                        model.dtDetails = dataTable;
                        for (int i = 0; i <= model.dtDetails.Rows.Count - 1; i++)
                        {
                            PayoutDetail report = new PayoutDetail();
                            report.LoginId = model.dtDetails.Rows[i]["LoginId"].ToString();
                            report.Name = model.dtDetails.Rows[i]["Name"].ToString();
                            report.BusinessAmount = decimal.Parse(model.dtDetails.Rows[i]["BusinessAmount"].ToString());

                            report.CommissionPercentage = decimal.Parse(model.dtDetails.Rows[i]["CommissionPercentage"].ToString());


                            report.Amount = decimal.Parse(model.dtDetails.Rows[i]["Amount"].ToString());

                            lst.Add(report);
                        }
                    }
                }
                return Json(lst);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public ActionResult GetSmartPoint(PayoutDetail model, string PayoutNo)
        {
            PayoutDetailResponse response = new PayoutDetailResponse();
            try
            {
                List<PayoutDetail> lst = new List<PayoutDetail>();

                PayoutDetailResponse payoutesponse = new PayoutDetailResponse();

                model.Fk_MemId = HttpContext.Session.GetString("CustomerId").ToString();
                model.PayoutNo = PayoutNo;
                string obj1 = "{\"Fk_MemId\":\"" + model.Fk_MemId + "\",\"PayoutNo\":\"" + model.PayoutNo + "\"}";
                string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                //string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                string result = Common.HITAPI(APIURL.GetSmartPointDetails, Body1);
                ShoppingWebResponse deserialized = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserialized.body);
                response = JsonConvert.DeserializeObject<PayoutDetailResponse>(dcdata1);
                if (response != null)
                {
                    if (response.Status == "1")
                    {
                        DataTable dataTable = Common.ToDataTable(response.Response.list);
                        model.dtDetails = dataTable;
                        for (int i = 0; i <= model.dtDetails.Rows.Count - 1; i++)
                        {
                            PayoutDetail report = new PayoutDetail();
                            report.LoginId = model.dtDetails.Rows[i]["LoginId"].ToString();
                            report.Name = model.dtDetails.Rows[i]["Name"].ToString();
                            report.OfferPoint = decimal.Parse(model.dtDetails.Rows[i]["OfferPoint"].ToString());
                            report.OrderNo = model.dtDetails.Rows[i]["OrderNo"].ToString();

                            report.AchiveDate = model.dtDetails.Rows[i]["AchiveDate"].ToString();

                            lst.Add(report);
                        }
                    }
                }
                return Json(lst);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult OfferLedger(PayoutDetail model)
        {
            PayoutDetailResponse response = new PayoutDetailResponse();
            try
            {

                if (model.Page == null || model.Page == 0)
                {
                    model.Page = 1;
                }

                model.Size = SessionManager.Size;
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                model.LoginId = HttpContext.Session.GetString("LoginId");
                string obj1 = "{\"Page\":\"" + model.Page + "\",\"Size\":\"" + model.Size + "\",\"FromDate\":\"" + model.FromDate + "\",\"ToDate\":\"" + model.ToDate + "\",\"LoginId\":\"" + model.LoginId + "\"}";
                string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                string result = Common.HITAPI(APIURL.GetOfferLedger, Body1);
                ShoppingWebResponse deserialized = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserialized.body);
                response = JsonConvert.DeserializeObject<PayoutDetailResponse>(dcdata1);
                if (response != null)
                {
                    if (response.Status == "1")
                    {

                        DataTable dataTable = Common.ToDataTable(response.Response.list);
                        model.dtDetails = dataTable;
                        if (model.ExportToExcel == "1")
                        {
                            try
                            {
                                string fileName = "";

                                fileName = "OfferPointLedger" + DateTime.Now + ".xlsx";

                                DataTable dt = model.dtDetails;
                                dt.Columns.Remove("TotalRecords");

                                using (XLWorkbook wb = new XLWorkbook())
                                {
                                    wb.Worksheets.Add(dt);
                                    using (MemoryStream stream = new MemoryStream())
                                    {
                                        wb.SaveAs(stream);
                                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                                    }
                                }

                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }
                        else
                        {
                            int? totalRecords = 0;
                            if (model.dtDetails.Rows.Count > 0)
                            {
                                totalRecords = int.Parse(model.dtDetails.Rows[0]["TotalRecords"].ToString());
                                var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                                model.Pager = pager;
                            }
                            else
                            {
                                TempData["AssociateLedger"] = "Data Not Found";

                            }
                        }

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View(model);
        }

        public ActionResult OfferPointsDetails(PayoutDetail model)
        {
            PayoutDetailResponse response = new PayoutDetailResponse();
            try
            {
                PayoutDetailResponse payoutesponse = new PayoutDetailResponse();
                model.Fk_MemId = HttpContext.Session.GetString("CustomerId").ToString();
                string obj1 = "{\"Fk_MemId\":\"" + model.Fk_MemId + "\"}";
                string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                //string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                string result = Common.HITAPI(APIURL.GetOfferPointDetails, Body1);
                ShoppingWebResponse deserialized = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserialized.body);
                response = JsonConvert.DeserializeObject<PayoutDetailResponse>(dcdata1);
                if (response != null)
                {
                    if (response.Status == "1")
                    {

                        DataTable dataTable = Common.ToDataTable(response.Response.list);
                        model.dtDetails = dataTable;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View(model);
        }
        public ActionResult CreateOrder(CreateOrder createOrder, string btnAdd, string btnSave)
        {
            #region WalletBalance

            string obj1 = "{\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.AssociateWalletBalance, Body);
            WalletBalanceResponse walletResponse = new WalletBalanceResponse();
            //FranchiseeWalletResponse walletResponse = new FranchiseeWalletResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            walletResponse = JsonConvert.DeserializeObject<WalletBalanceResponse>(dcdata);
            if (walletResponse.Status == 1)
            {
                createOrder.WalletBalance = walletResponse.Response.WalletBalanceData.Balance.ToString();
                createOrder.OFPPoints = walletResponse.Response.WalletBalanceData.OFPWallet.ToString();
            }
            #endregion
            #region BindPaymentMode
            obj1 = "{\"OpCode\":\"" + 4 + "\",\"Value\":\"" + "" + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            result = Common.HITAPI(APIURL.GetMasterData, Body);

            MasterDataResponse masterDataResponse = new MasterDataResponse();
            deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            ddlPaymentMode.Add(new SelectListItem { Text = "Payment Mode", Value = "0" });
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Name"].ToString() });
                    }
                }
                ViewBag.ddlPaymentMode = ddlPaymentMode;
            }
            #endregion

            #region BindProducts
            obj1 = "{\"OpCode\":\"" + 60 + "\",\"CustomerId\":\"" + HttpContext.Session.GetString("CustomerId") + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            result = Common.HITAPI(APIURL.GetMasterData, Body);

            deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            List<SelectListItem> ddlProducts = new List<SelectListItem>();
            ddlProducts.Add(new SelectListItem { Text = "Select Product", Value = "0" });
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        ddlProducts.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_Id"].ToString() });
                    }
                }

            }
            ViewBag.ddlProducts = ddlProducts;
            #endregion BindProducts
            createOrder.AddedBy = int.Parse(HttpContext.Session.GetString("CustomerId"));

            //createOrder.AddedBy = Convert.ToInt32(HttpContext.Session.GetString("FranchiseId"));
            if (!string.IsNullOrEmpty(btnAdd))
            {

                DataSet dataSet = createOrder.CreateOfferPointTemp();
            }
            if (!string.IsNullOrEmpty(btnSave))
            {
                createOrder.OrderFrom = "Web";
                DataSet dataSet = createOrder.GetRedeemofferpoint();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            if (createOrder.WalletType == "Paytm")
                            {
                                HttpContext.Session.SetString("OrderNo", dataSet.Tables[1].Rows[0]["OrderNo"].ToString());
                                HttpContext.Session.SetString("TransactionNo", dataSet.Tables[1].Rows[0]["OrderNo"].ToString());
                                HttpContext.Session.SetString("OrderAmount", dataSet.Tables[1].Rows[0]["OrderAmount"].ToString());

                            }
                            TempData["CreateOrder"] = "Offer Point Redeem Successfully";
                            return RedirectToAction("CreateOrder");
                        }
                        else
                        {
                            TempData["CreateOrder"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                        }
                    }
                }
            }
            DataSet dataSet1 = createOrder.GetOfferPointTempOrder();
            createOrder.dtDetails = dataSet1.Tables[0];
            createOrder.OrderAmount = dataSet1.Tables[1].Rows[0]["OrderAmount"].ToString();
            createOrder.Balance = dataSet1.Tables[1].Rows[0]["Balance"].ToString();
            DataSet ds = createOrder.GetAddressDetail();
            
            {
                ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                ViewBag.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                ViewBag.MobileNo = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                ViewBag.PinCode = ds.Tables[0].Rows[0]["PinCode"].ToString();
                ViewBag.State = ds.Tables[0].Rows[0]["State"].ToString();
                ViewBag.City = ds.Tables[0].Rows[0]["City"].ToString();
            }
            return View(createOrder);
        }
        public ActionResult DeleteTempOrder(string Id, string WalletType)
        {
            CreateOrder createOrder = new CreateOrder();
            createOrder.WalletType = "OFPWallet";
            if (!string.IsNullOrEmpty(WalletType))
            {
                createOrder.WalletType = WalletType;
            }
            createOrder.Pk_Id = int.Parse(Id);
            DataSet dataSet1 = createOrder.DeleteOfferPointTemp();
            return RedirectToAction("CreateOrder", createOrder);
        }

        public ActionResult PrintOfferPoint(CreateOrder createOrder, string OrderNo)
        {

            createOrder.OrderNo = OrderNo;
            DataSet dataSet = createOrder.PrintCustomerOrder();
            ViewBag.ContactPerson = dataSet.Tables[0].Rows[0]["ContactPerson"].ToString();
            ViewBag.LoginId = dataSet.Tables[0].Rows[0]["LoginId"].ToString();
            ViewBag.Address = dataSet.Tables[0].Rows[0]["Address"].ToString();
            ViewBag.Mobile = dataSet.Tables[0].Rows[0]["Mobile"].ToString();
            ViewBag.OrderNo = dataSet.Tables[0].Rows[0]["OrderNo"].ToString();
            ViewBag.DispatchCount = dataSet.Tables[0].Rows[0]["DispatchCount"].ToString();
            ViewBag.OrderDate = dataSet.Tables[0].Rows[0]["OrderDate"].ToString();
            ViewBag.InvoiceDate = dataSet.Tables[0].Rows[0]["InvoiceDate"].ToString();
            ViewBag.PinCode = dataSet.Tables[0].Rows[0]["PinCode"].ToString();
            ViewBag.ConsignerPinCode = dataSet.Tables[0].Rows[0]["ConsignerPinCode"].ToString();
            ViewBag.InvoiceNo = dataSet.Tables[0].Rows[0]["InvoiceNo"].ToString();
            ViewBag.AmountInWords = dataSet.Tables[2].Rows[0]["AmountInWords"].ToString();
            ViewBag.GSTNo = dataSet.Tables[0].Rows[0]["GSTNo"].ToString();
            ViewBag.ShippingCharges = decimal.Parse(dataSet.Tables[0].Rows[0]["ShippingCharges"].ToString());
            ViewBag.State = dataSet.Tables[0].Rows[0]["State"].ToString();
            ViewBag.EQRCodeUrl = dataSet.Tables[0].Rows[0]["EQRCodeUrl"].ToString();
            ViewBag.irn = dataSet.Tables[0].Rows[0]["irn"].ToString();
            ViewBag.AckDt = dataSet.Tables[0].Rows[0]["AckDt"].ToString();
            ViewBag.AckNo = dataSet.Tables[0].Rows[0]["AckNo"].ToString();
            createOrder.dtDetails = dataSet.Tables[1];
            if (dataSet.Tables[2].Rows[0]["AmountInWords"].ToString() == null || dataSet.Tables[2].Rows[0]["AmountInWords"].ToString() == "")
            {
                ViewBag.Title = "Gift Invoice";
            }
            else
            {
                ViewBag.Title = "Get Invoice";
            }

            createOrder.dtOrderDetails = dataSet.Tables[3];
            return View(createOrder);
        }


    }
}
