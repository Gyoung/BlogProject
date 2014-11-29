using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlogProject.Models
{
    /// <summary>
    /// 网站的帮助类
    /// </summary>
    public class LoginHelper
    {
        /// <summary>
        /// 网站是否可编辑
        /// </summary>
        public static bool IsEditMode
        {
            //get { return HttpContext.Current.Request.Url.Host.StartsWith("zengjy"); }
            get { return true; }
        }
    }
}