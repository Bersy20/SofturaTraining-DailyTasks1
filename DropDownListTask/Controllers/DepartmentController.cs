using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DropDownListTask.Models;

namespace DropDownListTask.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        // 1. Display the DepartmentNames in a dropdownlist from HumanResources.Department Table.
        // 2. For the Chosen department display Employees BusinessEntityID, Firstname, Gender, MaritalStatus, HireDate in a table.


        //public ActionResult Index()
        //{
        //    using (AdventureWorks2019Entities mEntity = new AdventureWorks2019Entities())
        //    {
        //        var dataEF = new SelectList(mEntity.Departments.ToList(), "DepartmentID", "Name");
        //        ViewData["Depts"] = dataEF;
        //    }
        //    return View();
        //}
        public ActionResult Index()
        {
            DeptModel mdl = new DeptModel();
            DataTable dt = mdl.DisplayDept();
            return View("Index", dt);
        }
        public ActionResult SearchByDept(FormCollection frm, string action)
        {
            if (action == "Submit")
            {
                DeptModel mdl = new DeptModel();
                string Department = frm["txtDept"];
                TempData["Dept"] = Department;
                DataTable rowIns = mdl.GetEmpByDept(Department);
                return View("Search",rowIns);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Search()
        {
            DeptModel mdl = new DeptModel();
            return View();
        }
    }
}