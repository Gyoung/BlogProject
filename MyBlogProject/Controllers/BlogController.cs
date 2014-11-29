using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlogProject.Entity;
using MyBlogProject.Models;

namespace MyBlogProject.Controllers
{
    public class BlogController : Controller
    {
        //
        // GET: /Blog/
        public ActionResult Add(int? id)
        {
            ViewData["blogSort"] = BlogModel.GetBlogSorts();
            if (LoginHelper.IsEditMode && id.HasValue)
            {
                Blog blog = BlogModel.GetBlog(id.Value);
                return View(blog);
            }
            return View();
        }

        public ActionResult Save()
        {
            Blog blog;
            var blogId = Request.Form["blogId"];
            if (blogId != "0")
            {
                blog = BlogModel.GetBlog(Convert.ToInt64(blogId));
            }
            else
            {
                blog=new Blog();
            }

            blog.Title= Request.Form["blogTitle"];
            List<int> sortInts=new List<int>();
            foreach (string key in Request.Form.AllKeys)
            {
                if (key.Contains("ck|")&&Request.Form[key]=="true,false")
                {
                    int id = Convert.ToInt32(key.Split('|')[1]);
                    sortInts.Add(id);
                    BlogSort sort = BlogModel.GetBlogSort(id);
                    blog.Sorts.Add(sort);
                }
            }
           
            blog.Content= Request.Unvalidated.Form["ckeditor"];
            blog.CreateTime = DateTime.Now;
            //TODO:标签与博客是多对多功能
            blog.UserId = 1;
            BlogModel.AddblBlog(blog);
            return RedirectToAction("Post","Home",new{id= blog.BlogId});
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            var fileName = System.IO.Path.GetFileName(upload.FileName);
            var filePhysicalPath = Server.MapPath("~/upload/" + fileName);//我把它保存在网站根目录的 upload 文件夹

            upload.SaveAs(filePhysicalPath);

            var url = "/upload/" + fileName;
            var CKEditorFuncNum = System.Web.HttpContext.Current.Request["CKEditorFuncNum"];

            //上传成功后，我们还需要通过以下的一个脚本把图片返回到第一个tab选项
            return Content("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\");</script>");
        }

        [HttpPost]
        public ActionResult AddTag()
        {
            var tagName = Request.Form["tag"];
            var sort = BlogModel.AddBlogSort(tagName);
            return Json(sort);
        }

        [HttpPost]
        public ActionResult AddComment()
        {
            var name = Request.Form["comName"];
            var email = Request.Form["comEmail"];
            var content = Request.Form["comContent"];
            var blogId =Convert.ToInt32(Request.Form["hidBlogId"]);
            Comment comment=new Comment()
            {
                Commenter = name,
                CreateTime = DateTime.Now,
                Content = content,
                Email = email,
                BlogId =blogId
            };
            BlogModel.AddComment(comment);
            return RedirectToAction("Post", "Home", new { id = comment.BlogId });
        }

    }
}
