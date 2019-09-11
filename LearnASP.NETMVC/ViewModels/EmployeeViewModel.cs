using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnASP.NETMVC.ViewModels
{

    //Model  与 ViewModel 类似于 java  do dto 这种概念

    public class EmployeeViewModel
    {

                 public string EmployeeName { get; set; }
         public string Salary { get; set; }
         public string SalaryColor { get; set; }


    }
}