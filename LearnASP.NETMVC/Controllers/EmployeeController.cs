using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using LearnASP.NETMVC.Models;
using LearnASP.NETMVC.ViewModels;
using LearnASP.NETMVC.DataAccessLayer;
using LearnASP.NETMVC.Filters;
using static LearnASP.NETMVC.Filters.CHeaderFooterFilter;

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


        public bool IsValidUser(UserDetails u)
        {
            if (u.UserName == "Admin" && u.Password == "Admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public UserStatus GetUserValidity(UserDetails u)
        {
            if (u.UserName == "Admin" && u.Password == "Admin")
            {
                return UserStatus.AuthenticatedAdmin;
            }
            else if (u.UserName == "Sukesh" && u.Password == "Sukesh")
            {
                return UserStatus.AuthentucatedUser;
            }
            else
            {
                return UserStatus.NonAuthenticatedUser;
            }
        }


    }
    public class EmployeeController : Controller
    {
        // GET: Employee
        [Authorize]
        [HeaderFooterFilter]
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


            //employeeListViewModel.UserName = User.Identity.Name; //New Line


            //employeeListViewModel.FooterData = new FooterViewModel();
            //      employeeListViewModel.FooterData.CompanyName = "StepByStepSchools";
            //      employeeListViewModel.FooterData.Year = DateTime.Now.Year.ToString();


            return View(employeeListViewModel);
        }



        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult AddNew()
        {

            CreateEmployeeViewModel employeeListViewModel = new CreateEmployeeViewModel();
            //employeeListViewModel.FooterData = new FooterViewModel();
            //employeeListViewModel.FooterData.CompanyName = "StepByStepSchools";//Can be set to dynamic value
            //employeeListViewModel.FooterData.Year = DateTime.Now.Year.ToString();
            //employeeListViewModel.UserName = User.Identity.Name; //New Line


            return View("CreateEmployee", employeeListViewModel);
        }

        [AdminFilter]
        [HeaderFooterFilter]
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


                        //vm.FooterData = new FooterViewModel();
                        //vm.FooterData.CompanyName = "StepByStepSchools";//Can be set to dynamic value
                        //vm.FooterData.Year = DateTime.Now.Year.ToString();
                        //vm.UserName = User.Identity.Name; //New Line




                        return View("CreateEmployee", vm);
                    }
                       
                case "Cancel":
                    return RedirectToAction("Index");// java中重定向
            }
            return new EmptyResult();
        }



        public ActionResult GetAddNewLink()
        {
            if (Convert.ToBoolean(Session["IsAdmin"]))
            {
                return PartialView("AddNewLink");
            }
            else
            {
                return new EmptyResult();
            }
        }
    }
}
