using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MyBlogProject.DataCore.EntityFramework;
using MyBlogProject.Models;

namespace MyBlogProject
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        

        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //是否可编辑
            //LoginHelper.IsEditMode = Request.Url.Host.StartsWith("zengjy");
           
            //todo:如何简化你的代码？ 对比上面的代码
            //if (Request.Url.Host.StartsWith("zjy"))
            //{
            //    LoginHelper.IsEditMode = true;
            //}
            //else
            //{
            //    LoginHelper.IsEditMode = false;
            //}



        }
    }
}