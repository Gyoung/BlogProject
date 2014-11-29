using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBlogProject.DataCore.EntityFramework;

namespace MyBlogProject.Models
{
    public class ContextHelper
    {
        static BlogDbContext context = null;

        private ContextHelper()
        {
        }

        /// <summary>
        /// 单例模式 返回唯一的BlogDbContext
        /// </summary>
        public static BlogDbContext DbContext
        {
            get
            {
                if(context==null)
                    context=new BlogDbContext();
                return context;
            }
        }
    }
}