using System;
using System.Web.Mvc;

namespace JJServicios.Web.Models
{
    public class AccessControlAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                string token = AuthenticationHelper.AuthenticationHelper.GetToken();

                if (string.IsNullOrEmpty(token))
                {
                    filterContext.Result = new RedirectResult("~/Account/Login");
                }
            }
            catch (Exception)
            {

                filterContext.Result = new RedirectResult("~/Account/Login");
            }
        }
    }
}