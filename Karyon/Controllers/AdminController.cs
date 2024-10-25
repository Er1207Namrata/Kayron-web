using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.EMMA;
using Karyon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nancy;
using Nancy.Responses;
using Newtonsoft.Json;
using System.Data;
using static Karyon.Models.Common;


namespace Karyon.Controllers
{
    public class AdminController : AdminBaseController
    {
        public ActionResult DashBoard()
        {
            return View();
        }
        public IActionResult Tree(DirectRequest direct, string Search, string LoginId)
        {
            if (!string.IsNullOrEmpty(LoginId) && string.IsNullOrEmpty(Search))
            {
                Search = "Search";
                direct.LoginId = (LoginId);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                direct.Size = 10000;
                string obj1 = "{\"LoginId\":\"" + direct.LoginId + "\",\"Page\":\"" + direct.Page + "\",\"Size\":\"" + direct.Size + "\",\"PlaceUnderId\":\"" + direct.PlaceUnderId + "\",\"Zone\":\"" + direct.Zone + "\"}";
                string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                string result = Common.HITAPI(APIURL.DirectTree, Body);

                DirectResponse directResponse = new DirectResponse();
                ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
                directResponse = JsonConvert.DeserializeObject<DirectResponse>(dcdata);
                TempData["OldLoginId"] = directResponse.PreviousId;
                if (directResponse.Status == 1)
                {

                    DataTable dataTable = Common.ToDataTable(directResponse.Response.DirectList);
                    direct.dtDetails = dataTable;

                    DataTable dataTable1 = Common.ToDataTable(directResponse.Response.SelfData);
                    direct.dtSelfData = dataTable1;

                    DataTable dataTable2 = Common.ToDataTable(directResponse.Response.SecoundLevelData);
                    direct.dtSecoundLevel = dataTable2;


                }
            }


            return View(direct);
        }
        public ActionResult ChangeSponsorLeg(ChangeSponsor changeSponsor, string Change)
        {
            if (!string.IsNullOrEmpty(Change))
            {
                changeSponsor.AddedBy = int.Parse(HttpContext.Session.GetString("AdminId").ToString());
                DataSet dataSet = changeSponsor.ChangeSponsorLeg();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            TempData["ChangeSponsor"] = "Updated Successfully";
                            return RedirectToAction("ChangeSponsorLeg");
                        }
                        else
                        {
                            TempData["ChangeSponsor"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            return View(changeSponsor);
                        }
                    }
                }
            }
            DataSet dataSet1 = changeSponsor.ChangeSponsorList();
            changeSponsor.dtDetails = dataSet1.Tables[0];
            return View(changeSponsor);
        }
        public ActionResult SetCappingMaster(CappingMaster obj, string btnSave, string Id)
        {
            DataSet dataSet = new DataSet();
            obj.OpCode = 2;
            if (btnSave == "Save")
            {
                obj.OpCode = 1;
                dataSet = obj.InsertCappingMaster();
                return RedirectToAction("SetCappingMaster");
            }
            if (Id != null)
            {
                obj.OpCode = 3;
                obj.Pk_Id = Id;
                dataSet = obj.InsertCappingMaster();
                return RedirectToAction("SetCappingMaster");
            }

            dataSet = obj.InsertCappingMaster();
            obj.dt = dataSet.Tables[0];
            return View(obj);
        }
        public ActionResult SetId(SetId obj)
        {
            DataSet dataSet = new DataSet();
            dataSet = obj.InsertSetId();
            return View(obj);
        }

        public ActionResult GetDirectTeam(DirectRequest data, string Update, string LoginId)
        {
            string Parametrvalue = HttpContext.Request.Query["LoginId"];

            if (!string.IsNullOrEmpty(LoginId))
            {
                data.LoginId = LoginId;

            }
            if (!string.IsNullOrEmpty(data.LoginId) && string.IsNullOrEmpty(Update))
            {
                data.OpCode = 1;
                DataSet dataSet = data.GetDirectTeam();
                data.dtDetails = dataSet.Tables[0];
                TempData["OldLoginId"] = dataSet.Tables[1].Rows[0]["LoginId"].ToString();
            }
            return View(data);
        }

        public ActionResult UpdateMemberDetails(UpdateMemberDetails updateMemberDetails)
        {
            if (!string.IsNullOrEmpty(updateMemberDetails.LoginId))
            {
                updateMemberDetails.AddedBy = int.Parse(HttpContext.Session.GetString("AdminId").ToString());
                DataSet dataSet = updateMemberDetails.UpdateProfileByAdmin();
                // updateMemberDetails.dtDetails = dataSet.Tables[0];
                if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "0")
                {
                    ViewBag.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                }
                else
                {
                    ViewBag.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                }

            }
            else
            {
                ViewBag.Message = "";
            }

            return View(updateMemberDetails);
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

        public ActionResult ActiveMember(ChangeSponsor changeSponsor, string ActivateMember)
        {
            if (!string.IsNullOrEmpty(ActivateMember))
            {
                changeSponsor.AddedBy = int.Parse(HttpContext.Session.GetString("AdminId").ToString());
                DataSet dataSet = changeSponsor.ActivateMember();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            TempData["ActivateMember"] = "Id Activated Successfully";
                            return RedirectToAction("ActiveMember");
                        }
                        else
                        {
                            TempData["ActivateMember"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            return View(changeSponsor);
                        }
                    }
                }
            }
            return View();
        }

        public ActionResult RegisterNewId(SetId obj, string btnSave)
        {
            if (!string.IsNullOrEmpty(btnSave))
            {
                DataSet dataSet = obj.RegistrationByAdmin();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            TempData["RegistrationByAdmin"] = "Registration Completed.Your LoginId is " + dataSet.Tables[0].Rows[0]["LoginId"].ToString() + " and pasword is " + dataSet.Tables[0].Rows[0]["Password"].ToString();
                            return RedirectToAction("RegisterNewId");
                        }
                        else
                        {
                            TempData["RegistrationByAdmin"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                        }
                    }
                }
            }
            return View();
        }
        public ActionResult GetAssociateList(UpdateMemberDetails model)
        {
            if (model.Page == null || model.Page == 0)
            {
                model.Page = 1;
            }
            model.Size = SessionManager.Size;
            model.FromDate = String.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = String.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet dataSet = model.GetAssociateList();
            model.dtDetails = dataSet.Tables[0];
            int? totalRecords = 0;
            if (model.dtDetails.Rows.Count > 0)
            {
                totalRecords = Convert.ToInt32(model.dtDetails.Rows[0]["TotalRecord"]);
                var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                model.Pager = pager;
                // ViewBag.pager = pager;
            }

            return View(model);
        }
        public ActionResult BlockUnblockAssociate(string Id, string Status)
        {
            UpdateMemberDetails model = new UpdateMemberDetails();
            if (!string.IsNullOrEmpty(Id))
            {
                model.Fk_MemId = Id;
                model.AddedBy = int.Parse(HttpContext.Session.GetString("AdminId").ToString());
                model.Status = Status;

                DataSet dataSet = model.BlockUnblockAssociate();
                model.dtDetails = dataSet.Tables[0];
                if (model.dtDetails.Rows[0]["Flag"].ToString() == "1")
                {
                    TempData["AssociateList"] = model.dtDetails.Rows[0]["Message"].ToString();
                }
                else
                {
                    TempData["AssociateList"] = model.dtDetails.Rows[0]["Message"].ToString();
                }

            }
            return Redirect("GetAssociateList");
        }

        public ActionResult UpdateRank(UpdateMemberDetails updateMemberDetails,string UpdateRank)
        {
            Models.Common common = new Models.Common();
            #region Bind Zone
            List<SelectListItem> ddlRank = new List<SelectListItem>();
            common.OpCode = 51;
            DataSet dataSet = common.GetAllMasters();
            ddlRank.Add(new SelectListItem { Text = "Select Rank", Value = "0" });
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dataSet.Tables[0].Rows)
                {
                    ddlRank.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_LevelId"].ToString() });
                }
            }
            ViewBag.ddlRank = ddlRank;
            #endregion Bind Zone

            if(!string.IsNullOrEmpty(UpdateRank))
            {
                DataSet dataSet1 = updateMemberDetails.UpdateRank();
                if (dataSet1 != null)
                {
                    if (dataSet1.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet1.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            TempData["UpdateRank"] = "Rank Updated Successfully";
                            return RedirectToAction("UpdateRank");
                        }
                        else
                        {
                            TempData["UpdateRank"] = dataSet1.Tables[0].Rows[0]["Message"].ToString();
                            return RedirectToAction("UpdateRank");
                        }
                    }
                }
            }
            return View();
        }
        public ActionResult ActivationMemberLog(ActivationMember model,string LoginId)
        {
            try
            {
                if (model.Page == null || model.Page == 0)
                {
                    model.Page = 1;
                }
                model.Size = SessionManager.Size;
                DataSet dataSet = model.GetActivationMember();
                model.dtDetails = dataSet.Tables[0];
                int? totalRecords = 0;
                if (model.dtDetails.Rows.Count > 0)
                {
                    totalRecords = int.Parse(dataSet.Tables[0].Rows[0]["TotalRecord"].ToString());
                    var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                    model.Pager = pager;
                    // ViewBag.pager = pager;
                }
            }
            catch(Exception ex)
            {
                throw;
            }
             return View(model);
        }
        public ActionResult RankUpdationLog(ActivationMember model, string LoginId)
        {
            try
            {
                if (model.Page == null || model.Page == 0)
                {
                    model.Page = 1;
                }
                model.Size = SessionManager.Size;
                DataSet dataSet = model.GetUpdationLog();
                model.dtDetails = dataSet.Tables[0];
                int? totalRecords = 0;
                if (model.dtDetails.Rows.Count > 0)
                {
                    totalRecords = int.Parse(dataSet.Tables[0].Rows[0]["TotalRecord"].ToString());
                    var pager = new Pager(totalRecords, model.Page, SessionManager.Size);
                    model.Pager = pager;                   
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(model);
        }
    }
}