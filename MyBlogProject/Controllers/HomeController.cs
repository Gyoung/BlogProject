using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Mvc;
using MyBlogProject.Entity;
using MyBlogProject.Models;

namespace MyBlogProject.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index(int id=1)
        {
            List<Blog> blogs = BlogModel.GetBlogs(id, 20);
            return View(blogs);
        }


        public ActionResult Tag(int id)
        {
            List<Blog> blogs = BlogModel.GetBlogs(id);
            var tag = BlogModel.GetBlogSort(id);
            if (tag != null)
            {
                ViewData["tag"] = tag.SortName;
            }
           
            return View("Index", blogs);
        }

        public ActionResult Post(int id)
        {
            Blog blog = BlogModel.GetBlog(id);
            //更新点击次数
            BlogModel.UpdateBlogVisit(blog);
            //上一篇
            var bortherPost = BlogModel.GetBortherPost(id);
            ViewData["bortherPost"] = bortherPost;
            //标签
            ViewData["blogSort"] = BlogModel.GetBlogSorts(id);
            //评论
            ViewData["blogComments"] = BlogModel.GetComments(id);
            foreach (KeyValuePair<Int64, string> post in bortherPost)
            {
                if (post.Key > id)
                    ViewData["nextPost"] = post;
                if (post.Key < id)
                    ViewData["lastPost"] = post;
            }
            //下一篇
            return View(blog);
        }


        public ActionResult About()
        {
            return View();
        }

        public ActionResult Research()
        {
            return View();
        }


        [HttpPost]
        public ActionResult PasteImage(HttpPostedFileBase upload)
        {
            var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            var filePhysicalPath = Server.MapPath("~/upload/" + fileName);//我把它保存在网站根目录的 upload 文件夹
            var data1 = Request.Form.ToString();
            var data = data1.Replace("%2f", "/").Replace("%3d", "=");
            byte[] bytes1 = Convert.FromBase64String(data);
            MemoryStream memStream1 = new MemoryStream(bytes1);
            Image a = new Bitmap(memStream1);
  
            a.Save(filePhysicalPath);


            var url = "/upload/" + fileName;
            var CKEditorFuncNum = System.Web.HttpContext.Current.Request["CKEditorFuncNum"];

            //上传成功后，我们还需要通过以下的一个脚本把图片返回到第一个tab选项
            return Content("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\");</script>");
        }

    }
}
