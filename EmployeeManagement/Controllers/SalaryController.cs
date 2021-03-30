using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using EmployeeManagement.Models;
using ClosedXML.Excel;

namespace EmployeeManagement.Controllers
{
    public class SalaryController : Controller
    {
        // GET: Salary
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SalaryReport()
        {
            return View();
        }
        public ActionResult SaveSalary(SalaryModel model)
        {
            return Json(new { message = (new SalaryModel().SaveSalary(model)) }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetSalary()
        {
            try
            {
                return Json(new { model = (new SalaryModel().GetSalary()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetEmployeeSalReport(int EmployeeId, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return Json(new { model = (new SalaryModel().GetEmployeeSalReport(EmployeeId, FromDate, ToDate)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ExportToExcel(int EmployeeId, DateTime FromDate, DateTime ToDate)
        {
            var gv = new GridView();
            gv.DataSource = new SalaryModel().GetEmployeeSalReport(EmployeeId, FromDate, ToDate);
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return View("SalaryReport");
        }
    }
}