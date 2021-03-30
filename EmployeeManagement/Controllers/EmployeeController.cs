using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeManagement.Models;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaveEmployee(EmployeeModel model)
        {
            return Json(new { message = (new EmployeeModel().SaveEmployee(model)) }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetEmployee()
        {
            try
            {
                return Json(new { model = (new EmployeeModel().GetEmployee()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteEmployee(long EmployeeId)
        {
            try
            {
                return Json(new { model = (new EmployeeModel().DeleteEmployee(EmployeeId)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetEmployeeDetail(int EmployeeId)
        {

            try
            {
                return Json(new { model = (new EmployeeModel().GetEmployeeDetail(EmployeeId)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}