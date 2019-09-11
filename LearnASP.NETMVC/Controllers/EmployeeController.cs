using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using LearnASP.NETMVC.Models;
using LearnASP.NETMVC.ViewModels;
using LearnASP.NETMVC.DataAccessLayer;

namespace LearnASP.NETMVC.Controllers
{





    public class EmployeeBusinessLayerr
    {

        public List<Employee> GetEmployees()
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            return salesDal.Employees.ToList();
        }


        public Employee SaveEmployee(Employee e)
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            salesDal.Employees.Add(e);
            salesDal.SaveChanges();
            return e;
        }



    }
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();

            EmployeeBusinessLayerr empBal = new EmployeeBusinessLayerr();
            List<Employee> employees = empBal.GetEmployees();

            List<EmployeeViewModel> empViewModels = new List<EmployeeViewModel>();

            foreach (Employee emp in employees)
            {
                EmployeeViewModel empViewModel = new EmployeeViewModel();
                empViewModel.EmployeeName = emp.FirstName + " " + emp.LastName;
                empViewModel.Salary = emp.Salary.ToString();
                if (emp.Salary > 15000)
                {
                    empViewModel.SalaryColor = "yellow";
                }
                else
                {
                    empViewModel.SalaryColor = "green";
                }
                empViewModels.Add(empViewModel);
            }
            employeeListViewModel.Employees = empViewModels;
            return View(employeeListViewModel);
        }




        public ActionResult AddNew()
        {
            return View("CreateEmployee",new CreateEmployeeViewModel());
        }

        public ActionResult SaveEmployee(Employee e, string BtnSubmit)
        {
            switch (BtnSubmit)
            {
                case "Save Employee":
                    if (ModelState.IsValid)
                                     {
                        EmployeeBusinessLayerr empBal = new EmployeeBusinessLayerr();
                        empBal.SaveEmployee(e);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        CreateEmployeeViewModel vm = new CreateEmployeeViewModel();
                        vm.FirstName = e.FirstName;
                        vm.LastName = e.LastName;

                        if (e.Salary.HasValue)
                                             {
                                                vm.Salary = e.Salary.ToString();
                                             }
                                         else
                                              {
                                                  vm.Salary = ModelState["Salary"].Value.AttemptedValue;
                                              }



                        return View("CreateEmployee", vm);
                    }
                       
                case "Cancel":
                    return RedirectToAction("Index");// java中重定向
            }
            return new EmptyResult();
        }
    }
}
