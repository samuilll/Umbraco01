using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using UmbracoTest_03.Models;
using Umbraco.Web;
using Umbraco.Core.Models;
using Archetype.Models;

namespace UmbracoTest_03.Controllers
{
    public class HomeController:SurfaceController
    {
        private const string PARTIAL_VIEW_FOLDER = "~/Views/Partials/Home/";
        public ActionResult RenderFeaturedSection()
        {
            IPublishedContent homePage = Umbraco.ContentAtRoot().FirstOrDefault();
            List<FeaturedItem> model = new List<FeaturedItem>();
            ArchetypeModel featuredItems = homePage.GetPropertyValue<ArchetypeModel>("featuredItems");

            foreach (ArchetypeFieldsetModel fieldSet in featuredItems)
            {
                string name = fieldSet.GetValue<string>("name");

                string category = fieldSet.GetValue<string>("category");

                IPublishedContent image = fieldSet.GetValue<IPublishedContent>("image");
                string imageUrl = image.Url;

                IPublishedContent relatedPage = fieldSet.GetValue<IPublishedContent>("page");
                string linkUrl = relatedPage.Url;

                model.Add(new FeaturedItem(name, category, imageUrl, linkUrl));
            }
            return PartialView($"{PARTIAL_VIEW_FOLDER}_Featured.cshtml",model);
        }
        public ActionResult RenderServicesSection()
        {
            return PartialView($"{PARTIAL_VIEW_FOLDER}_Services.cshtml");
        }
        public ActionResult RenderBlog()
        {
            IPublishedContent homePage = Umbraco.ContentAtRoot().FirstOrDefault();
            string title = homePage.GetPropertyValue<string>("latestBlogTitle");
            string introduction = homePage.GetPropertyValue<string>("latestBlogPostsIntroduction");

            LatestBlogPosts model = new LatestBlogPosts() { Title = title, Introduction = introduction };

            return PartialView($"{PARTIAL_VIEW_FOLDER}_Blog.cshtml",model);
        }
        public ActionResult RenderClients()
        {
            return PartialView($"{PARTIAL_VIEW_FOLDER}_Clients.cshtml");
        }
        public ActionResult RenderIntro()
        {
            return PartialView($"{PARTIAL_VIEW_FOLDER}_Intro.cshtml");
        }
    }
}