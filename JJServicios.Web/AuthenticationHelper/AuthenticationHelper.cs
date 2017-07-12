using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using JJServicios.DB.Contracts;

namespace JJServicios.Web.AuthenticationHelper
{
    public class AuthenticationHelper
    {
        private static readonly JJServiciosEntities Db = new JJServiciosEntities();
        private static List<Agent> _agents;

        public static string GetToken()
        {
            var username = GetUserName();

            var tokenObject = HttpRuntime.Cache[username];
            if (tokenObject != null)
                HttpRuntime.Cache.Insert(username, tokenObject.ToString(), null, DateTime.Now.AddDays(Convert.ToInt16(WebConfigurationManager.AppSettings["UserSessionTimeExpiration"])), System.Web.Caching.Cache.NoSlidingExpiration);
            return tokenObject == null ? string.Empty : tokenObject.ToString();
        }

        public static string GetUserName()
        {
            var cookiauth = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookiauth == null) return string.Empty;

            var username = FormsAuthentication.Decrypt(cookiauth.Value).Name;
            return username;
        }

        public static long GetAgentId()
        {
            if (_agents == null)
                _agents = Db.Agent.ToList();

            var name = GetUserName();

            var agent = _agents.First(x => x.Name == name);

            return agent.Id;
        }
    }
}