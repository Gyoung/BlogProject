using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogProject.Entity
{
    /// <summary>
    /// 评论
    /// </summary>
    public class Comment : Entity
    {
        /// <summary>
        /// 评论Id
        /// </summary>
        public Int64 CommentId { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 评论者
        /// </summary>
        public string Commenter { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 博客Id
        /// </summary>
        public Int64 BlogId { get; set; }

        /// <summary>
        /// 博客
        /// </summary>
        public Blog Blog { get; set; }

        ///// <summary>
        ///// 用户Id
        ///// </summary>
        //public int UserId { get; set; }

        ///// <summary>
        ///// 评论者
        ///// </summary>
        //public User User { get; set; }

    }
}
