using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeManagement.Data;

namespace EmployeeManagement.Models
{
    public class EmployeeModel
    {

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }

        public string SaveEmployee(EmployeeModel model)
        {
            string message = "";
            EmployeeEntities db = new EmployeeEntities();
            var saveEmployee = new tbl_employee()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Department = model.Department,
             
             
            };
            db.tbl_employee.Add(saveEmployee);
            db.SaveChanges();
            message = "Employee Added Succesfully";
            return message;
        }


        // code for show list
        public List<EmployeeModel> GetEmployee()
        {
            EmployeeEntities db = new EmployeeEntities();
            List<EmployeeModel> lstReg = new List<EmployeeModel>();
            var RegistrationList = db.tbl_employee.ToList();
            if (RegistrationList != null)
            {
                foreach (var Registration in RegistrationList)
                {
                    lstReg.Add(new EmployeeModel()
                    {
                        EmployeeId = Registration.EmployeeId,
                        FirstName = Registration.FirstName,
                        LastName = Registration.LastName,
                        Department = Registration.Department,
                      

                    });
                }
            }
            return lstReg;
        }
        // code for delete string
        public string DeleteEmployee(long EmployeeId)
        {
            string message = "";
            EmployeeEntities db = new EmployeeEntities();

            var deleteReg = db.tbl_employee.Where(p => p.EmployeeId == EmployeeId).FirstOrDefault();
            if (deleteReg != null)
            {
                db.tbl_employee.Remove(deleteReg);
            };

            db.SaveChanges();
            message = "Registration Deleted Successfully";

            return message;
        }

        // code for detail view
        public EmployeeModel GetEmployeeDetail(int EmployeeId)
        {
            EmployeeEntities db = new EmployeeEntities();
            try
            {
                EmployeeModel model = new EmployeeModel();
                var RegDet = db.tbl_employee.Where(p => p.EmployeeId == EmployeeId).FirstOrDefault();
                if (RegDet != null)
                {
                    model.EmployeeId = RegDet.EmployeeId;
                    model.FirstName = RegDet.FirstName;
                    model.LastName = RegDet.LastName;
                    model.Department = RegDet.Department;
                  
                };

                db.Dispose();
                return model;
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
    }

}