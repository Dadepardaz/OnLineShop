#pragma checksum "D:\AspExample\OnlineShopEnglish\EndPoint.Site\Views\Orders\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c54b3b4f5911c5af828c031eed14bf81cbb3a5bf"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Orders_Index), @"mvc.1.0.view", @"/Views/Orders/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\AspExample\OnlineShopEnglish\EndPoint.Site\Views\_ViewImports.cshtml"
using EndPoint.Site;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\AspExample\OnlineShopEnglish\EndPoint.Site\Views\_ViewImports.cshtml"
using EndPoint.Site.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\AspExample\OnlineShopEnglish\EndPoint.Site\Views\Orders\Index.cshtml"
using OnlineShop.Application.Services.Orders.Queries.GetOrderForUser;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c54b3b4f5911c5af828c031eed14bf81cbb3a5bf", @"/Views/Orders/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"87685c89e84078b3e134b89a928accf3d0f04a39", @"/Views/_ViewImports.cshtml")]
    public class Views_Orders_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<ResultGetOrderDto>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "D:\AspExample\OnlineShopEnglish\EndPoint.Site\Views\Orders\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<table>\r\n    <thead>\r\n        <tr>\r\n            <td>Number Of Order</td>\r\n            <td>Numer of Pay</td>\r\n            <td>Order Status</td>\r\n            <td>Products in Order</td>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 18 "D:\AspExample\OnlineShopEnglish\EndPoint.Site\Views\Orders\Index.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 22 "D:\AspExample\OnlineShopEnglish\EndPoint.Site\Views\Orders\Index.cshtml"
               Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 25 "D:\AspExample\OnlineShopEnglish\EndPoint.Site\Views\Orders\Index.cshtml"
               Write(item.PayId);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 28 "D:\AspExample\OnlineShopEnglish\EndPoint.Site\Views\Orders\Index.cshtml"
               Write(item.OrderStatus);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                 <table>\r\n");
#nullable restore
#line 32 "D:\AspExample\OnlineShopEnglish\EndPoint.Site\Views\Orders\Index.cshtml"
                      foreach (var orderDetail in item.OrderDetails)
                     {

#line default
#line hidden
#nullable disable
            WriteLiteral("                     <tr>\r\n                         <td>\r\n                             ");
#nullable restore
#line 36 "D:\AspExample\OnlineShopEnglish\EndPoint.Site\Views\Orders\Index.cshtml"
                        Write(orderDetail.ProductName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                         </td>\r\n                         <td>\r\n                             ");
#nullable restore
#line 39 "D:\AspExample\OnlineShopEnglish\EndPoint.Site\Views\Orders\Index.cshtml"
                        Write(orderDetail.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                         </td>\r\n                         <td>\r\n                             ");
#nullable restore
#line 42 "D:\AspExample\OnlineShopEnglish\EndPoint.Site\Views\Orders\Index.cshtml"
                        Write(orderDetail.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                         </td>\r\n                     </tr>\r\n");
#nullable restore
#line 45 "D:\AspExample\OnlineShopEnglish\EndPoint.Site\Views\Orders\Index.cshtml"
                     }

#line default
#line hidden
#nullable disable
            WriteLiteral("                 </table>\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 49 "D:\AspExample\OnlineShopEnglish\EndPoint.Site\Views\Orders\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<ResultGetOrderDto>> Html { get; private set; }
    }
}
#pragma warning restore 1591