using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using UmbracoTest_03.Models;

namespace UmbracoTest_03.Controllers
{
    public class BlogController: SurfaceController
    {
        private const string PARTIAL_VIEW_FOLDER = "~/Views/Partials/Blog/";

        public ActionResult RenderPostList(int numberOfitems = int.MaxValue)
        {
            List<BlogPreview> items = new List<BlogPreview>();

            IPublishedContent blogPage = CurrentPage.AncestorOrSelf(1).DescendantsOrSelf().Where(x=>x.DocumentTypeAlias=="blog").FirstOrDefault();
            List<IPublishedContent> blogChildren = blogPage.Children().Take(numberOfitems).ToList();

            foreach (var child in blogChildren)
            {
                string name = child.Name;

                string introduction = child.GetPropertyValue<string>("articleIntro");

                IPublishedContent image = child.GetPropertyValue<IPublishedContent>("articleImage");
                string imageUrl = image.Url;

                string linkUrl = child.Url;

                items.Add(new BlogPreview(name, introduction, imageUrl, linkUrl));
            }


            return PartialView(PARTIAL_VIEW_FOLDER + "_PostList.cshtml",items);
        }
    }
}