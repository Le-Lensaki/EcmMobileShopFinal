using System.Web.Mvc;

namespace EcmMobileShop.Areas.OneTechAdmin
{
    public class OneTechAdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "OneTechAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "OneTechAdmin_default",
                "OneTechAdmin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }


    }
}