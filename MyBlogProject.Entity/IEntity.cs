using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogProject.Entity
{
    /// <summary>
    /// 实体接口
    /// </summary>
    public interface  IEntity
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        int IsDelete { get; set; }
    }
}
