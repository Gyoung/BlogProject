using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogProject.Entity
{
    /// <summary>
    /// 留言板
    /// </summary>
    public class MsgBoard : Entity
    {
        /// <summary>
        /// 留言板Id
        /// </summary>
        public Int64 MsgBoardId { get; set; }

        /// <summary>
        /// 留言板内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 留言人
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }
    }
}
