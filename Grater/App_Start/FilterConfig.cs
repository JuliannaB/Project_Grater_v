using System.Web;
using System.Web.Mvc;

namespace Grater
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute()); //Komentarz filter for whole application
        }
    }
}
