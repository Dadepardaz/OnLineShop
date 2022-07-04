using Microsoft.AspNetCore.Http;
using System;

namespace EndPoint.Site.Utilites
{
    public interface ICookiesManager
    {
        void AddCookie(HttpContext context, string key, string value);
        Guid GetBrowserId(HttpContext context);
        bool HasCookie(HttpContext context, string key);
        void RemoveCookie(HttpContext context, string key, string value);
        string ValueCookie(HttpContext context, string key);
    }
}