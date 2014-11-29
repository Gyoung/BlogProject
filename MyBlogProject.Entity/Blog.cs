using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogProject.Entity
{
    /// <summary>
    /// 博客
    /// </summary>
    public class Blog : Entity
    {
        public Blog()
        {
            Comments = new HashSet<Comment>();
            Sorts=new HashSet<BlogSort>();
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        public Int64 BlogId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 博客内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///用户Id
        /// </summary>
        public Int64 UserId { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// 分类Id
        /// </summary>
        //public int SortId { get; set; }

        /// <summary>
        /// 浏览统计
        /// </summary>
        public Int64 VisitCount { get; set; }

        /// <summary>
        /// 博客分类
        /// </summary>
        public virtual ICollection<BlogSort> Sorts { get; set; }

        /// <summary>
        /// 博客评论列表
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
