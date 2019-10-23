using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UmbracoTest_03.Models
{
    public class BlogPreview
    {
        public string Name { get; set; }
        public string Introduction { get; set; }
        public string ImageUrl { get; set; }
        public string LinkUrl { get; set; }

        public BlogPreview(string name, string introduction, string imageUrl, string linkUrl)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Introduction = introduction ?? throw new ArgumentNullException(nameof(introduction));
            ImageUrl = imageUrl ?? throw new ArgumentNullException(nameof(imageUrl));
            LinkUrl = linkUrl ?? throw new ArgumentNullException(nameof(linkUrl));
        }
    }
}