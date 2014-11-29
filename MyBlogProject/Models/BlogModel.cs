using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MyBlogProject.Entity;

namespace MyBlogProject.Models
{
    public class BlogModel
    {
        /// <summary>
        /// 获取博客列表
        /// </summary>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns></returns>
        public static List<Blog> GetBlogs(int pageIndex=1,int pageSize=10)
        {
           var blogs=  ContextHelper.DbContext.Blogs.OrderBy(p=>p.BlogId).Skip((pageIndex - 1)*pageSize).Take(pageSize);
           return blogs.ToList();
        }

        /// <summary>
        /// 根据博客标签来获取博客
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public static List<Blog> GetBlogs(int tagId)
        {
            var blogs = ContextHelper.DbContext.BlogSorts.FirstOrDefault(a => a.SortId == tagId).Blogs;
            return blogs.ToList();
        }

        public static void AddblBlog(Blog blog)
        {
            if (blog.BlogId == 0)
            {
                ContextHelper.DbContext.Blogs.Add(blog);
            }

            ContextHelper.DbContext.SaveChanges();
        }

        public static Blog GetBlog(Int64 blogId)
        {
            return ContextHelper.DbContext.Blogs.FirstOrDefault(a => a.BlogId == blogId);
        }

        public static int UpdateBlogVisit(Blog blog)
        {
            blog.VisitCount += 1;
            return ContextHelper.DbContext.SaveChanges();
        }

        /// <summary>
        /// 获取上一篇与下一篇文章的链接
        /// </summary>
        /// <param name="currentId"></param>
        /// <returns></returns>
        public static Dictionary<Int64, string> GetBortherPost(int currentId)
        {
            Dictionary<Int64, string> dicPost = new Dictionary<Int64, string>();
            var lastPost = ContextHelper.DbContext.Blogs.OrderByDescending(a => a.BlogId).Where(a => a.BlogId < currentId).
                Select(a =>new { a.BlogId,a.Title}).FirstOrDefault();

            if (lastPost != null)
            {
                dicPost[lastPost.BlogId] = lastPost.Title;
            }

            var nextPost = ContextHelper.DbContext.Blogs.OrderBy(a => a.BlogId).Where(a => a.BlogId > currentId).
               Select(a => new { a.BlogId, a.Title }).FirstOrDefault();
            if (nextPost != null)
            {
                dicPost[nextPost.BlogId] = nextPost.Title;
            }
            return dicPost;
        }


        #region Comment 评论

        /// <summary>
        /// 增加评论
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public static Comment AddComment(Comment comment)
        {
            ContextHelper.DbContext.Comments.Add(comment);
            ContextHelper.DbContext.SaveChanges();
            return comment;
        }

        /// <summary>
        /// 获取评论
        /// </summary>
        /// <param name="blogId">博客ID</param>
        /// <returns></returns>
        public static ICollection<Comment> GetComments(int blogId)
        {
            return ContextHelper.DbContext.Comments.Where(a => a.BlogId == blogId).ToList();
        }

        #endregion

        #region BlogSort

        /// <summary>
        /// 新增博客标签
        /// </summary>
        /// <param name="sortName"></param>
        /// <returns></returns>
        public static BlogSort AddBlogSort(string sortName)
        {
            BlogSort sort = new BlogSort()
            {
                SortName = sortName,
                IsDelete = 0,
                CreateTime = DateTime.Now,
            };
            ContextHelper.DbContext.BlogSorts.Add(sort);
            ContextHelper.DbContext.SaveChanges();
            return sort;
        }

        /// <summary>
        /// 获取博客标签列表
        /// </summary>
        /// <returns></returns>
        public static Dictionary<Int64, string> GetBlogSorts()
        {
            Dictionary<Int64, string> dicSort = new Dictionary<Int64, string>();
            var blogSorts = ContextHelper.DbContext.BlogSorts.Where(a=>a.IsDelete==0).Select(a =>new 
            {
                a.SortId,
                a.SortName
            });
            foreach (var sort in blogSorts)
            {
                dicSort[sort.SortId] = sort.SortName;
            }
            return dicSort;
        }

        /// <summary>
        /// 通过标签ID来获取标签
        /// </summary>
        /// <param name="sortId"></param>
        /// <returns></returns>
        public static BlogSort GetBlogSort(int sortId)
        {
            return ContextHelper.DbContext.BlogSorts.FirstOrDefault(a => a.SortId == sortId);
        }


        public static ICollection<BlogSort> GetBlogSorts(int blogId)
        {
            return ContextHelper.DbContext.Blogs.Where(a => a.BlogId == blogId).Select(a=>a.Sorts).FirstOrDefault();
        }


        #endregion

    }
}