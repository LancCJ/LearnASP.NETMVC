using LearnASP.NETMVC.Filters;
using System.Web;
using System.Web.Mvc;

namespace LearnASP.NETMVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {

            //filters.Add(new HandleErrorAttribute());

            filters.Add(new EmployeeExceptionFilter());
        }
    }
}
