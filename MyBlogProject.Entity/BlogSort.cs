using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogProject.Entity
{
    /// <summary>
    /// 博客分类
    /// </summary>
    public class BlogSort : Entity
    {
        public BlogSort()
        {
               Blogs=new HashSet<Blog>();
        }

        /// <summary>
        /// 分类Id
        /// </summary>
        public Int64 SortId { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string SortName { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }

    }
}
