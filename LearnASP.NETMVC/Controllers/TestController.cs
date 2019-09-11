using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// 引入包名 类似JAVA
using LearnASP.NETMVC.Models;
using LearnASP.NETMVC.ViewModels;
using LearnASP.NETMVC.DataAccessLayer;



namespace LearnASP.NETMVC.Controllers
{

    public class Customer
    {
        public string CustomerName { get; set; }
        public string Address { get; set; }


        public override string ToString()
    {
         return this.CustomerName+"|"+this.Address;
    }

}


    public class EmployeeBusinessLayer
    {

        public List<Employee> GetEmployees2()
     {
         SalesERPDAL salesDal = new SalesERPDAL();
         return salesDal.Employees.ToList();
     }





    public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            Employee emp = new Employee();
            emp.FirstName = "johnson";
            emp.LastName = " fernandes";
            emp.Salary = 14000;
            employees.Add(emp);

            emp = new Employee();
            emp.FirstName = "michael";
            emp.LastName = "jackson";
            emp.Salary = 16000;
            employees.Add(emp);

            emp = new Employee();
            emp.FirstName = "robert";
            emp.LastName = " pattinson";
            emp.Salary = 20000;
            employees.Add(emp);

            return employees;
        }
    }


    public class TestController : Controller
    {

    // GET: Test
    public string GetString()
        {
            return "Hello World is old now. It’s time for wassup bro ;)";
        }


        public Customer GetCustomer()
        {
            Customer c = new Customer();
            c.CustomerName = "Customer 1";
            c.Address = "Address1";
            return c;
        }


             public ActionResult GetView()
     {


            //构造对象
              Employee emp = new Employee();
              emp.FirstName = "Sukesh";
              emp.LastName = "Marla";
              emp.Salary = 20000;


            //ViewData,   ViewBag可以称为ViewData的一块关于语法的辅助的糖果，ViewBag使用C# 4.0的动态特征，使得ViewData也具有动态特性。 

            ViewData["Employee"] = emp;


            ViewBag.Employee1 = emp;

            //这种类似于使用SpringMVC中的moduleView 返回视图层的是讲数据模型一块进行传输
            return View(emp);
     }


        public ActionResult GetView2()
        {


                 EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();

        //    EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
        //          List<Employee> employees = empBal.GetEmployees2();
               
        // List<EmployeeViewModel> empViewModels = new List<EmployeeViewModel>();
               
        //foreach (Employee emp in employees)
        //              {
        //                  EmployeeViewModel empViewModel = new EmployeeViewModel();
        //                  empViewModel.EmployeeName = emp.FirstName + " " + emp.LastName;
        //                  empViewModel.Salary = emp.Salary.ToString("C");
        //                  if (emp.Salary > 15000)
        //                      {
        //                          empViewModel.SalaryColor = "yellow";
        //                      }
        //                  else
        //                      {
        //                          empViewModel.SalaryColor = "green";
        //                      }
        //                  empViewModels.Add(empViewModel);
        //              }
        //          employeeListViewModel.Employees = empViewModels;
        //          employeeListViewModel.UserName = "Admin";
                  return View( employeeListViewModel);
        }

    }

   
}