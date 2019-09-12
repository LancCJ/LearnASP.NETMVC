using LearnASP.NETMVC.Filters;
using LearnASP.NETMVC.Models;
using LearnASP.NETMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static LearnASP.NETMVC.Filters.CHeaderFooterFilter;

namespace LearnASP.NETMVC.Controllers
{
    public class BulkUploadController : AsyncController
    {
        // GET: BulkUpload
        [HeaderFooterFilter]
        [AdminFilter]
        public ActionResult Index()
        {
            return View(new FileUploadViewModel());
        }




        [AdminFilter]
        public async Task<ActionResult> Upload(FileUploadViewModel model)
        {
            //List<Employee> employees = GetEmployees(model);
            //EmployeeBusinessLayerr bal = new EmployeeBusinessLayerr();
            //bal.UploadEmployees(employees);
            //return RedirectToAction("Index", "Employee");



            int t1 = Thread.CurrentThread.ManagedThreadId;
            List<Employee> employees = await Task.Factory.StartNew<List<Employee>>
                (() => GetEmployees(model));
            int t2 = Thread.CurrentThread.ManagedThreadId;
            EmployeeBusinessLayerr bal = new EmployeeBusinessLayerr();
            bal.UploadEmployees(employees);
            return RedirectToAction("Index", "Employee");


        }

        private List<Employee> GetEmployees(FileUploadViewModel model)
        {
            List<Employee> employees = new List<Employee>();
            StreamReader csvreader = new StreamReader(model.fileUpload.InputStream);
            csvreader.ReadLine(); // Assuming first line is header
            while (!csvreader.EndOfStream)
            {
                var line = csvreader.ReadLine();
                var values = line.Split(',');//Values are comma separated
                Employee e = new Employee();
                e.FirstName = values[0];
                e.LastName = values[1];
                e.Salary = int.Parse(values[2]);
                employees.Add(e);
            }
            return employees;
        }

    }
}