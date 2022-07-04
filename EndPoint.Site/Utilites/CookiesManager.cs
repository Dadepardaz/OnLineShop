using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndPoint.Site.Utilites
{
    public class CookiesManager : ICookiesManager
    {
        public void AddCookie(HttpContext context, string key, string value)
        {
            context.Response.Cookies.Append(key, value, GetCookieOptions(context));
        }
        public bool HasCookie(HttpContext context, string key)
        {
            var result = context.Request.Cookies.ContainsKey(key);
            return result;
        }

        public string ValueCookie(HttpContext context, string key)
        {
            string cookieValue;
            if (!context.Request.Cookies.TryGetValue(key, out cookieValue))
            {
                return null;
            }

            return cookieValue;
        }
        public void RemoveCookie(HttpContext context, string key, string value)
        {
            if (HasCookie(context, key))
            {
                context.Response.Cookies.Delete(key, GetCookieOptions(context));

            }
        }

        public Guid GetBrowserId(HttpContext context)
        {
            string valueCookie = ValueCookie(context, "BrowserId");
            if (valueCookie == null)
            {
                string newValueCookie = Guid.NewGuid().ToString();
                AddCookie(context, "BrowserId", newValueCookie);
                valueCookie = newValueCookie;
            }

            Guid guidBrowser;
            Guid.TryParse(valueCookie, out guidBrowser);
            return guidBrowser;
        }
        private CookieOptions GetCookieOptions(HttpContext context)
        {
            return new CookieOptions
            {
                HttpOnly = true,
                Path = context.Request.PathBase.HasValue ? context.Request.PathBase.ToString() : "/",
                Secure = context.Request.IsHttps,
                Expires = DateTime.Now.AddDays(100)
            };
        }

    }
}
