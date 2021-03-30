using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using EmployeeManagement.Data;

namespace EmployeeManagement.Models
{
    public class SalaryModel
    {
        public long TransactionId { get; set; }
        public int EmployeeId { get; set; }
        public decimal Salary { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public string SalaryMonth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SaveSalary(SalaryModel model)
        {
            string message = "";
            EmployeeEntities db = new EmployeeEntities();
            var saveSalary = new tbl_employeeSalary()
            {
                EmployeeId = model.EmployeeId,
                Salary = model.Salary,
                TransactionDate = DateTime.Now,
                SalaryMonth = model.SalaryMonth,


            };
            db.tbl_employeeSalary.Add(saveSalary);
            db.SaveChanges();
            message = "Salary Added for Employee Succesfully";
            return message;
        }


        // code for show list
        public List<SalaryModel> GetSalary()
        {
            EmployeeEntities db = new EmployeeEntities();
            List<SalaryModel> lstReg = new List<SalaryModel>();
            var RegistrationList = db.tbl_employeeSalary.ToList();
            if (RegistrationList != null)
            {
                foreach (var Registration in RegistrationList)
                {
                    lstReg.Add(new SalaryModel()
                    {
                        TransactionId = Registration.TransactionId,
                        EmployeeId = Registration.EmployeeId,
                        Salary = Registration.Salary,
                        TransactionDate = Registration.TransactionDate,
                        SalaryMonth = Registration.SalaryMonth,


                    });
                }
            }
            return lstReg;
        }

        public List<SalaryModel> GetsalaryReport()
        {
            EmployeeEntities db = new EmployeeEntities();
            List<SalaryModel> lstReg = new List<SalaryModel>();
            var RegistrationList = db.tbl_employeeSalary.ToList();
            if (RegistrationList != null)
            {
                foreach (var Registration in RegistrationList)
                {
                    lstReg.Add(new SalaryModel()
                    {
                        TransactionId = Registration.TransactionId,
                        EmployeeId = Registration.EmployeeId,
                        Salary = Registration.Salary,
                        TransactionDate = Registration.TransactionDate,
                        SalaryMonth = Registration.SalaryMonth,


                    });
                }
            }
            return lstReg;
        }
        public List<SalaryModel> GetEmployeeSalReport(int EmployeeId, DateTime FromDate, DateTime ToDate)
        {
            EmployeeEntities db = new EmployeeEntities();
            List<SalaryModel> lstSalary = new List<SalaryModel>();

            //// By using Stored Procedure
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetEmployeeSalReport";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramName = cmd.CreateParameter();
                    paramName.ParameterName = "EmployeeId";
                    paramName.Value = EmployeeId;
                    cmd.Parameters.Add(paramName);

                    DbParameter paramFromDate = cmd.CreateParameter();
                    paramFromDate.ParameterName = "FromDate";
                    paramFromDate.Value = FromDate;
                    cmd.Parameters.Add(paramFromDate);

                    DbParameter paramToDate = cmd.CreateParameter();
                    paramToDate.ParameterName = "ToDate";
                    paramToDate.Value = ToDate;
                    cmd.Parameters.Add(paramToDate);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    SalaryModel blmodel = new SalaryModel();
                    blmodel.TransactionId = Convert.ToInt32(dr["TransactionId"].ToString());
                    blmodel.Salary = Convert.ToDecimal(dr["Salary"]);
                    blmodel.EmployeeId = Convert.ToInt32(dr["EmployeeId"].ToString());
                    blmodel.FirstName = dr["FirstName"].ToString();
                    blmodel.LastName = dr["LastName"].ToString();
                    blmodel.SalaryMonth = dr["SalaryMonth"].ToString();

                    lstSalary.Add(blmodel);
                }
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }

            db.Dispose();
            return lstSalary;
        }
    }
}