using DocumentFormat.OpenXml.Spreadsheet;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Karyon.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Karyon.Controllers
{
    public class ExportDataController : Controller
    {
        public IActionResult PayoutStatement(string PayoutNo,string LoginId)
        {
            int j = 1;
            PayoutDetail payoutRequest = new PayoutDetail();
            payoutRequest.PayoutNo = PayoutNo;
            payoutRequest.LoginId = LoginId;
            DataSet dataSet = payoutRequest.GetPayoutSummaryDetails();
            payoutRequest.dtDetails = dataSet.Tables[0];
            DataSet dataSet1 = payoutRequest.GetPayoutClosingDetails();
            payoutRequest.dtSelfData = dataSet1.Tables[0];
            return View(payoutRequest);
           
        }
       
       
    }
}
