using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UmbracoTest_03.Models
{
    public class FeaturedItem
    {
        public FeaturedItem(string name, string category, string imageUrl, string linkUrl)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Category = category ?? throw new ArgumentNullException(nameof(category));
            ImageUrl = imageUrl ?? throw new ArgumentNullException(nameof(imageUrl));
            LinkUrl = linkUrl ?? throw new ArgumentNullException(nameof(linkUrl));
        }

        public string Name { get; set; }
        public string Category { get; set; }
        public string ImageUrl{ get; set; }
        public string LinkUrl { get; set; }
    }
}