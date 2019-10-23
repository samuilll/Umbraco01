using UmbracoTest_03.Helpers;
using System.Collections.Generic;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using System.Linq;
using Umbraco.Web;
using UmbracoTest_03.Models;

namespace UMTest01.Controllers
{
    public class SiteLayoutController: SurfaceController
    {
        private const string VIEW_FOLDER_PATH = "~/Views/Partials/SiteLayout/";
        private const string EXCLUDE_FROM_TOP_NAVIGATION = "excludeFromTopNavigation";
 

        public ActionResult RenderFooter()
        {
            return PartialView($"{VIEW_FOLDER_PATH}_Footer.cshtml");
        }

        public ActionResult RenderTitleControls()
        {
            return PartialView($"{VIEW_FOLDER_PATH}_TitleControls.cshtml");
        }

        /// <summary>
        /// Renders the top navigation in the header partial
        /// </summary>
        /// <returns>Partial view with a model</returns>
        public ActionResult RenderHeader()
        {
            List<NavigationListItem> nav = Cache.GetObject<List<NavigationListItem>>("mainNav", 0, GetNavigationModelFromDatabase);
            return PartialView($"{VIEW_FOLDER_PATH}_Header.cshtml",nav);
        }

        /// <summary>
        /// Finds the home page and gets the navigation structure based on it and it's children
        /// </summary>
        /// <returns>A List of NavigationListItems, representing the structure of the site.</returns>
        private List<NavigationListItem> GetNavigationModelFromDatabase()
        {
            //const int HOME_PAGE_POSITION_IN_PATH = 1;
            //int homePageId = int.Parse(CurrentPage.Path.Split(',')[HOME_PAGE_POSITION_IN_PATH]);
            //IPublishedContent homePage = Umbraco.Content(homePageId);
           

            IPublishedContent homePage = Umbraco.ContentAtRoot().FirstOrDefault();
            List<NavigationListItem> nav = new List<NavigationListItem>();
            nav.Add(new NavigationListItem(new NavigationLink(homePage.Url, homePage.Name)));
            nav.AddRange(GetChildNavigationList(homePage));
            return nav;
        }

        /// <summary>
        /// Loops through the child pages of a given page and their children to get the structure of the site.
        /// </summary>
        /// <param name="page">The parent page which you want the child structure for</param>
        /// <returns>A List of NavigationListItems, representing the structure of the pages below a page.</returns>
        private List<NavigationListItem> GetChildNavigationList(IPublishedContent page)
        {
            List<NavigationListItem> listItems = null;
            List<IPublishedContent> childPages = page.Children.Where(x=>x.Level<=2).Where(x=>(!x.HasValue(EXCLUDE_FROM_TOP_NAVIGATION) || x.GetPropertyValue<bool>(EXCLUDE_FROM_TOP_NAVIGATION)!=true)).ToList();

            if (childPages != null && childPages.Any() && childPages.Count() > 0)
            {
                listItems = new List<NavigationListItem>();
                foreach (var childPage in childPages)
                {
                    NavigationListItem listItem = new NavigationListItem(new NavigationLink(childPage.Url, childPage.Name));
                    listItem.Items = GetChildNavigationList(childPage);
                    listItems.Add(listItem);
                }
            }
            return listItems;
        }
    }
}