using System.Web;
using System.Web.Mvc;

namespace CRMS_Web_Clinte
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
